using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Voxpop.Profile.Domain.Common;
using Voxpop.Profile.Domain.ReferenceData;
using Voxpop.Profile.Domain.ReferenceData.Translations;
using Voxpop.Profile.Infrastructure.Persistence.Migrations.Dtos;

namespace Voxpop.Profile.Infrastructure.Persistence.Migrations;

public class Migrator(IServiceScopeFactory scopeFactory, ILogger<Migrator> logger)
{
    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public async Task MigrateAsync(CancellationToken ct = default)
    {
        await ApplyMigrationsAsync(ct);

        logger.LogInformation("Started seeding...");
        await SeedContinentsAsync("continents.json", ct);
        await SeedAsync<EducationLevel, EducationLevelTranslation>(
            "education_levels.json",
            el => EducationLevel.Create(el.Code),
            EducationLevelTranslation.Create,
            ct: ct);
        await SeedAsync<Ethnicity, EthnicityTranslation>(
            "ethnicities.json",
            el => Ethnicity.Create(el.Code),
            EthnicityTranslation.Create,
            ct: ct);
        await SeedAsync<Gender, GenderTranslation>(
            "genders.json",
            el => Gender.Create(el.Code),
            GenderTranslation.Create,
            ct: ct);
        await SeedAsync<Occupation, OccupationTranslation>(
            "occupations.json",
            el => Occupation.Create(el.Code),
            OccupationTranslation.Create,
            ct: ct);
        await SeedAsync<Race, RaceTranslation>(
            "races.json",
            el => Race.Create(el.Code),
            RaceTranslation.Create,
            ct: ct);
        await SeedAsync<Religion, ReligionTranslation>(
            "religions.json",
            el => Religion.Create(el.Code),
            ReligionTranslation.Create,
            ct: ct);
        logger.LogInformation("Seeding is finished.");
    }

    private async Task ApplyMigrationsAsync(CancellationToken ct)
    {
        logger.LogInformation("Applying migrations...");
        using var scope = scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ProfileDbContext>();
        await dbContext.Database.MigrateAsync(ct);
        logger.LogInformation("Migrations applied.");
    }

    private async Task SeedContinentsAsync(string fileName, CancellationToken ct)
    {
        logger.LogInformation("Seeding {Continent}, {Country}, {State} and {City} started.",
            nameof(Continent), nameof(Country), nameof(State), nameof(City));

        using var scope = scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ProfileDbContext>();
        var continentsSeed = await GetSeedAsync<ContinentSeed>(fileName, ct);

        var continentCodes =
            await dbContext.Set<Continent>().ToDictionaryAsync(m => m.Code, m => m.Id, cancellationToken: ct);

        var newContinentsCount = 0;
        var newCountriesCount = 0;
        var newStatesCount = 0;
        var newCitiesCount = 0;
        foreach (var continentSeed in continentsSeed)
        {
            if (!continentCodes.TryGetValue(continentSeed.Code, out var continentId))
            {
                var continent = Continent.Create(continentSeed.Code);
                continentId = continent.Id;
                await dbContext.Set<Continent>().AddAsync(continent, ct);
                newContinentsCount++;
            }

            foreach (var translationItem in continentSeed.Translations)
            {
                var translation = await dbContext.Set<ContinentTranslation>()
                    .FirstOrDefaultAsync(t => t.Id == continentId && t.Language == translationItem.Key, ct);

                if (translation == null)
                {
                    translation = ContinentTranslation.Create(continentId, translationItem.Key, translationItem.Value);
                    await dbContext.Set<ContinentTranslation>().AddAsync(translation, ct);
                }

                translation.UpdateName(translationItem.Value);
            }

            var (newCountriesCountTemp, newStatesCountTemp, newCitiesCountTemp) =
                await SeedCountriesAsync(dbContext, continentId, continentSeed.Countries, ct);
            newCountriesCount += newCountriesCountTemp;
            newStatesCount += newStatesCountTemp;
            newCitiesCount += newCitiesCountTemp;
        }

        logger.LogInformation("Seeding {Name} with {Count} new items...", nameof(Continent), newContinentsCount);
        logger.LogInformation("Seeding {Name} with {Count} new items...", nameof(Country), newCountriesCount);
        logger.LogInformation("Seeding {Name} with {Count} new items...", nameof(State), newStatesCount);
        logger.LogInformation("Seeding {Name} with {Count} new items...", nameof(City), newCitiesCount);

        await dbContext.SaveChangesAsync(ct);

        logger.LogInformation("Seeding Continents, Countries, States and Cities completed.");
    }

    private async Task<(int, int, int)> SeedCountriesAsync(
        ProfileDbContext dbContext,
        Guid continentId,
        CountrySeed[] countriesSeed,
        CancellationToken ct)
    {
        var countryCodes = await dbContext.Set<Country>().Where(c => c.ContinentId == continentId)
            .ToDictionaryAsync(m => m.Code, m => m.Id, cancellationToken: ct);

        var newCountriesCount = 0;
        var newStatesCount = 0;
        var newCitiesCount = 0;
        foreach (var countrySeed in countriesSeed)
        {
            if (!countryCodes.TryGetValue(countrySeed.Code, out var countryId))
            {
                var country = Country.Create(countrySeed.Code, continentId);
                countryId = country.Id;
                await dbContext.Set<Country>().AddAsync(country, ct);
                newCountriesCount++;
            }

            foreach (var translationItem in countrySeed.Translations)
            {
                var translation = await dbContext.Set<CountryTranslation>()
                    .FirstOrDefaultAsync(t => t.Id == countryId && t.Language == translationItem.Key, ct);

                if (translation == null)
                {
                    translation = CountryTranslation.Create(countryId, translationItem.Key, translationItem.Value);
                    await dbContext.Set<CountryTranslation>().AddAsync(translation, ct);
                }

                translation.UpdateName(translationItem.Value);
            }

            var (newStatesCountTemp, newCitiesCountTemp) =
                await SeedStatesAsync(dbContext, countryId, countrySeed.States, ct);
            newStatesCount += newStatesCountTemp;
            newCitiesCount += newCitiesCountTemp;
        }

        return (newCountriesCount, newStatesCount, newCitiesCount);
    }

    private async Task<(int, int)> SeedStatesAsync(
        ProfileDbContext dbContext,
        Guid countryId,
        StateSeed[] statesSeed,
        CancellationToken ct)
    {
        var stateCodes = await dbContext.Set<State>().Where(c => c.CountryId == countryId)
            .ToDictionaryAsync(m => m.Code, m => m.Id, cancellationToken: ct);

        var newStatesCount = 0;
        var newCitiesCount = 0;
        foreach (var stateSeed in statesSeed)
        {
            if (!stateCodes.TryGetValue(stateSeed.Code, out var stateId))
            {
                var state = State.Create(stateSeed.Code, countryId);
                stateId = state.Id;
                await dbContext.Set<State>().AddAsync(state, ct);
                newStatesCount++;
            }

            foreach (var translationItem in stateSeed.Translations)
            {
                var translation = await dbContext.Set<StateTranslation>()
                    .FirstOrDefaultAsync(t => t.Id == stateId && t.Language == translationItem.Key, ct);

                if (translation == null)
                {
                    translation = StateTranslation.Create(stateId, translationItem.Key, translationItem.Value);
                    await dbContext.Set<StateTranslation>().AddAsync(translation, ct);
                }

                translation.UpdateName(translationItem.Value);
            }

            newCitiesCount += await SeedCitiesAsync(dbContext, stateId, stateSeed.Cities, ct);
        }

        return (newStatesCount, newCitiesCount);
    }

    private async Task<int> SeedCitiesAsync(
        ProfileDbContext dbContext,
        Guid stateId,
        CitySeed[] citiesSeed,
        CancellationToken ct)
    {
        var cityCodes = await dbContext.Set<City>().Where(c => c.StateId == stateId)
            .ToDictionaryAsync(m => m.Code, m => m.Id, cancellationToken: ct);

        var newCitiesCount = 0;
        foreach (var citySeed in citiesSeed)
        {
            if (!cityCodes.TryGetValue(citySeed.Code, out var cityId))
            {
                var city = City.Create(citySeed.Code, stateId);
                cityId = city.Id;
                await dbContext.Set<City>().AddAsync(city, ct);
                newCitiesCount++;
            }

            foreach (var translationItem in citySeed.Translations)
            {
                var translation = await dbContext.Set<CityTranslation>()
                    .FirstOrDefaultAsync(t => t.Id == cityId && t.Language == translationItem.Key, ct);

                if (translation == null)
                {
                    translation = CityTranslation.Create(cityId, translationItem.Key, translationItem.Value);
                    await dbContext.Set<CityTranslation>().AddAsync(translation, ct);
                }

                translation.UpdateName(translationItem.Value);
            }
        }

        return newCitiesCount;
    }

    private async Task<TSeed[]> GetSeedAsync<TSeed>(string fileName, CancellationToken ct)
    {
        var filePath = Path.Combine(AppContext.BaseDirectory, "Data", "Seed", fileName);
        var json = await File.ReadAllTextAsync(filePath, ct);
        return JsonSerializer.Deserialize<TSeed[]>(json, _options)!;
    }

    private async Task SeedAsync<TModel, TSeed>(
        string fileName,
        Func<TSeed, TModel?> map,
        CancellationToken ct = default)
        where TModel : ReferenceEntity
        where TSeed : BaseCodeSeed
    {
        logger.LogInformation("Seeding {Name} with {FileName} started.", typeof(TModel).Name, fileName);

        using var scope = scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ProfileDbContext>();

        var filePath = Path.Combine(AppContext.BaseDirectory, "Data", "Seed", fileName);

        if (!File.Exists(filePath))
        {
            logger.LogWarning("Seeding file {FileName} not found.", fileName);
            return;
        }

        var json = await File.ReadAllTextAsync(filePath, ct);
        var items = JsonSerializer.Deserialize<TSeed[]>(json, _options)!;

        var existingCodes = dbContext.Set<TModel>().Select(e => e.Code).ToArray();

        var newItems = items.Where(dto => !existingCodes.Contains(dto.Code)).ToArray();

        logger.LogInformation("Seeding {Name} with {Count} new items...", typeof(TModel).Name, newItems.Length);
        await dbContext.Set<TModel>().AddRangeAsync(newItems.Select(map).Where(m => m != null)!, ct);
        await dbContext.SaveChangesAsync(ct);

        logger.LogInformation("Seeding {Name} with {FileName} completed.", typeof(TModel).Name, fileName);
    }

    private async Task SeedAsync<TModel>(
        string fileName,
        Func<BaseCodeSeed, TModel?> map,
        CancellationToken ct = default)
        where TModel : ReferenceEntity => await SeedAsync<TModel, BaseCodeSeed>(fileName, map, ct);

    private async Task SeedAsync<TModel, TTranslationModel>(
        string fileName,
        Func<BaseCodeSeed, TModel> map,
        Func<Guid, string, string, TTranslationModel> mapTranslation,
        CancellationToken ct = default)
        where TModel : ReferenceEntity
        where TTranslationModel : TranslationEntity
    {
        logger.LogInformation("Seeding {Name} with {FileName} started.", typeof(TModel).Name, fileName);

        using var scope = scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ProfileDbContext>();

        var filePath = Path.Combine(AppContext.BaseDirectory, "Data", "Seed", fileName);

        if (!File.Exists(filePath))
        {
            logger.LogWarning("Seeding file {FileName} not found.", fileName);
            return;
        }

        var json = await File.ReadAllTextAsync(filePath, ct);
        var items = JsonSerializer.Deserialize<BaseCodeSeed[]>(json, _options)!;

        var existingReferences = dbContext.Set<TModel>().ToArray();
        var existingTranslations = dbContext.Set<TTranslationModel>().ToArray();

        var newReferencesCount = 0;
        var newTranslationsCount = 0;

        foreach (var item in items)
        {
            var reference = existingReferences.FirstOrDefault(e => e.Code == item.Code);
            if (reference == null)
            {
                reference = map(item);
                await dbContext.Set<TModel>().AddAsync(reference, ct);
                newReferencesCount++;
            }

            foreach (var translationItem in item.Translations)
            {
                var translation =
                    existingTranslations.FirstOrDefault(t => t.Id == reference.Id && t.Language == translationItem.Key);

                if (translation == null)
                {
                    translation = mapTranslation(reference.Id, translationItem.Key, translationItem.Value);
                    await dbContext.Set<TTranslationModel>().AddAsync(translation, ct);
                    newTranslationsCount++;
                }

                translation.UpdateName(translationItem.Value);
            }
        }

        logger.LogInformation("Seeding {Name} with {Count} new items...", typeof(TModel).Name, newReferencesCount);
        logger.LogInformation("Seeding {Name} with {Count} new items...", typeof(TTranslationModel).Name,
            newTranslationsCount);
        await dbContext.SaveChangesAsync(ct);

        logger.LogInformation("Seeding {Name} with {FileName} completed.", typeof(TModel).Name, fileName);
    }
}