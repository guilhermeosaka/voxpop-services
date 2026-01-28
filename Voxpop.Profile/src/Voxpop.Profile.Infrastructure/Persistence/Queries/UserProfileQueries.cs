using Dapper;
using Voxpop.Profile.Application.Dtos;
using Voxpop.Profile.Application.Interfaces;
using Voxpop.Profile.Infrastructure.Persistence.Dapper;
using Voxpop.Profile.Infrastructure.Persistence.Queries.Results;

namespace Voxpop.Profile.Infrastructure.Persistence.Queries;

public class UserProfileQueries(ISqlConnectionFactory connectionFactory) : IUserProfileQueries
{
    public async Task<UserProfileDto?> GetByUserId(Guid userId)
    {
        var db = connectionFactory.CreateConnection();

        const string sql = """
                           select up.date_of_birth as "DateOfBirth",
                                  g.id as "GenderId",
                                  g.code as "GenderName",
                                  ci.id as "CityId",
                                  ci.name as "CityName",
                                  s.id as "StateId",
                                  s.code as "StateName",
                                  c.id as "CountryId",
                                  c.code as "CountryName",
                                  co.id as "ContinentId",
                                  co.code as "ContinentName",
                                  el.id as "EducationLevelId",
                                  el.code as "EducationLevelName",
                                  oc.id as "OccupationId",
                                  oc.code as "OccupationName",
                                  rl.id as "ReligionId",
                                  rl.code as "ReligionName",
                                  r.id as "RaceId",
                                  r.code as "RaceName",
                                  e.id as "EthnicityId",
                                  e.code as "EthnicityName"
                           from user_profiles up
                           left join genders g on g.id = up.gender_id
                           left join cities ci on ci.id = up.city_id
                           left join states s on s.id = up.state_id
                           left join countries c on c.id = up.country_id
                           left join continents co on co.id = c.continent_id
                           left join education_levels el on el.id = up.education_level_id
                           left join occupations oc on oc.id = up.occupation_id
                           left join religions rl on rl.id = up.religion_id
                           left join races r on r.id = up.race_id
                           left join ethnicities e on e.id = up.ethnicity_id
                           where up.user_id = @UserId
                           """;

        var result = await db.QueryFirstOrDefaultAsync<UserProfileQueriesResult>(
            sql,
            new { UserId = userId }
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