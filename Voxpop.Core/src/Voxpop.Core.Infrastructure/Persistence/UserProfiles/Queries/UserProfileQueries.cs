using Dapper;
using Voxpop.Core.Application.ReferenceData.Dtos;
using Voxpop.Core.Application.UserProfiles.Dtos;
using Voxpop.Core.Application.UserProfiles.Interfaces;
using Voxpop.Core.Domain.Common;
using Voxpop.Core.Infrastructure.Persistence.Common.Dapper;
using Voxpop.Core.Infrastructure.Persistence.UserProfiles.Queries.Results;

namespace Voxpop.Core.Infrastructure.Persistence.UserProfiles.Queries;

public class UserProfileQueries(ISqlConnectionFactory connectionFactory) : IUserProfileQueries
{   
    public async Task<UserProfileDto?> GetByUserId(Guid userId, string language)
    {
        var db = connectionFactory.CreateConnection();

        const string sql = """
                           select up.date_of_birth as "DateOfBirth",
                                  coalesce(g.id, g.id) as "GenderId",
                                  coalesce(g.name, g_en.name) as "GenderName",
                                  coalesce(ci.id, ci.id) as "CityId",
                                  coalesce(ci.name, ci_en.name) as "CityName",
                                  coalesce(st.id, st.id) as "StateId",
                                  coalesce(st.name, st_en.name) as "StateName",
                                  coalesce(c.id, c.id) as "CountryId",
                                  coalesce(ct.name, ct_en.name) as "CountryName",
                                  coalesce(co.id, co.id) as "ContinentId",
                                  coalesce(co.name, co_en.name) as "ContinentName",
                                  coalesce(el.id, el.id) as "EducationLevelId",
                                  coalesce(el.name, el_en.name) as "EducationLevelName",
                                  coalesce(oc.id, oc.id) as "OccupationId",
                                  coalesce(oc.name, oc_en.name) as "OccupationName",
                                  coalesce(rl.id, rl.id) as "ReligionId",
                                  coalesce(rl.name, rl_en.name) as "ReligionName",
                                  coalesce(r.id, r.id) as "RaceId",
                                  coalesce(r.name, r_en.name) as "RaceName",
                                  coalesce(e.id, e.id) as "EthnicityId",
                                  coalesce(e.name, e_en.name) as "EthnicityName"
                           from user_profiles up
                           left join gender_translations g on g.id = up.gender_id and g.language = @Language
                           left join gender_translations g_en on g_en.id = up.gender_id and g_en.language = @DefaultLanguage
                           left join city_translations ci on ci.id = up.city_id and ci.language = @Language
                           left join city_translations ci_en on ci_en.id = up.city_id and ci_en.language = @DefaultLanguage
                           left join state_translations st on st.id = up.state_id and st.language = @Language
                           left join state_translations st_en on st_en.id = up.state_id and st_en.language = @DefaultLanguage
                           left join countries c on c.id = up.country_id
                           left join country_translations ct on ct.id = c.id and ct.language = @Language
                           left join country_translations ct_en on ct_en.id = c.id and ct_en.language = @DefaultLanguage
                           left join continent_translations co on co.id = c.continent_id and co.language = @Language
                           left join continent_translations co_en on co_en.id = c.continent_id and co_en.language = @DefaultLanguage
                           left join education_level_translations el on el.id = up.education_level_id and el.language = @Language
                           left join education_level_translations el_en on el_en.id = up.education_level_id and el_en.language = @DefaultLanguage
                           left join occupation_translations oc on oc.id = up.occupation_id and oc.language = @Language
                           left join occupation_translations oc_en on oc_en.id = up.occupation_id and oc_en.language = @DefaultLanguage
                           left join religion_translations rl on rl.id = up.religion_id and rl.language = @Language
                           left join religion_translations rl_en on rl_en.id = up.religion_id and rl_en.language = @DefaultLanguage
                           left join race_translations r on r.id = up.race_id and r.language = @Language
                           left join race_translations r_en on r_en.id = up.race_id and r_en.language = @DefaultLanguage
                           left join ethnicity_translations e on e.id = up.ethnicity_id and e.language = @Language
                           left join ethnicity_translations e_en on e_en.id = up.ethnicity_id and e_en.language = @DefaultLanguage
                           where up.user_id = @UserId
                           """;

        var result = await db.QueryFirstOrDefaultAsync<UserProfileQueriesResult>(
            sql,
            new { UserId = userId, Language = language.ToLower(), Constants.DefaultLanguage }
        );
        
        if (result == null) return null;
        
        var personalInfo = new PersonalInfoDto(
            result.DateOfBirth,
            new ReferenceDto(result.GenderId, result.GenderName)
        );
        
        var locationInfo = new LocationInfoDto(
            new ReferenceDto(result.CityId, result.CityName),
            new ReferenceDto(result.StateId, result.StateName),
            new ReferenceDto(result.CountryId, result.CountryName),
            new ReferenceDto(result.ContinentId, result.ContinentName)
        );
        
        var professionalInfo = new ProfessionalInfoDto(
            new ReferenceDto(result.EducationLevelId, result.EducationLevelName),
            new ReferenceDto(result.OccupationId, result.OccupationName)
        );
        
        var culturalInfo = new CulturalInfoDto(
            new ReferenceDto(result.ReligionId, result.ReligionName),
            new ReferenceDto(result.RaceId, result.RaceName),
            new ReferenceDto(result.EthnicityId, result.EthnicityName)
        );
        
        return new UserProfileDto(personalInfo, locationInfo, professionalInfo, culturalInfo);
    }
}