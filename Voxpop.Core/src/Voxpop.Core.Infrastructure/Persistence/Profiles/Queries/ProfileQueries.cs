using Dapper;
using Voxpop.Core.Application.Profiles.Dtos;
using Voxpop.Core.Application.Profiles.Queries;
using Voxpop.Core.Application.ReferenceData.Dtos;
using Voxpop.Core.Domain.Common;
using Voxpop.Core.Infrastructure.Persistence.Common.Dapper;
using Voxpop.Core.Infrastructure.Persistence.Profiles.Queries.Results;

namespace Voxpop.Core.Infrastructure.Persistence.Profiles.Queries;

public class ProfileQueries(ISqlConnectionFactory connectionFactory) : IProfileQueries
{   
    public async Task<ProfileDto?> GetByUserIdAsync(Guid userId, string language)
    {
        var db = connectionFactory.CreateConnection();

        const string sql = """
                           SELECT up.date_of_birth AS "DateOfBirth",
                                  COALESCE(g.id, g.id) AS "GenderId",
                                  COALESCE(g.name, g_en.name) AS "GenderName",
                                  COALESCE(ci.id, ci.id) AS "CityId",
                                  COALESCE(ci.name, ci_en.name) AS "CityName",
                                  COALESCE(st.id, st.id) AS "StateId",
                                  COALESCE(st.name, st_en.name) AS "StateName",
                                  COALESCE(c.id, c.id) AS "CountryId",
                                  COALESCE(ct.name, ct_en.name) AS "CountryName",
                                  COALESCE(co.id, co.id) AS "ContinentId",
                                  COALESCE(co.name, co_en.name) AS "ContinentName",
                                  COALESCE(el.id, el.id) AS "EducationLevelId",
                                  COALESCE(el.name, el_en.name) AS "EducationLevelName",
                                  COALESCE(oc.id, oc.id) AS "OccupationId",
                                  COALESCE(oc.name, oc_en.name) AS "OccupationName",
                                  COALESCE(rl.id, rl.id) AS "ReligionId",
                                  COALESCE(rl.name, rl_en.name) AS "ReligionName",
                                  COALESCE(r.id, r.id) AS "RaceId",
                                  COALESCE(r.name, r_en.name) AS "RaceName",
                                  COALESCE(e.id, e.id) AS "EthnicityId",
                                  COALESCE(e.name, e_en.name) AS "EthnicityName"
                           FROM user_profiles up
                           LEFT JOIN gender_translations g ON g.id = up.gender_id AND g.language = @Language
                           LEFT JOIN gender_translations g_en ON g_en.id = up.gender_id AND g_en.language = @DefaultLanguage
                           LEFT JOIN city_translations ci ON ci.id = up.city_id AND ci.language = @Language
                           LEFT JOIN city_translations ci_en ON ci_en.id = up.city_id AND ci_en.language = @DefaultLanguage
                           LEFT JOIN state_translations st ON st.id = up.state_id AND st.language = @Language
                           LEFT JOIN state_translations st_en ON st_en.id = up.state_id AND st_en.language = @DefaultLanguage
                           LEFT JOIN countries c ON c.id = up.country_id
                           LEFT JOIN country_translations ct ON ct.id = c.id AND ct.language = @Language
                           LEFT JOIN country_translations ct_en ON ct_en.id = c.id AND ct_en.language = @DefaultLanguage
                           LEFT JOIN continent_translations co ON co.id = c.continent_id AND co.language = @Language
                           LEFT JOIN continent_translations co_en ON co_en.id = c.continent_id AND co_en.language = @DefaultLanguage
                           LEFT JOIN education_level_translations el ON el.id = up.education_level_id AND el.language = @Language
                           LEFT JOIN education_level_translations el_en ON el_en.id = up.education_level_id AND el_en.language = @DefaultLanguage
                           LEFT JOIN occupation_translations oc ON oc.id = up.occupation_id AND oc.language = @Language
                           LEFT JOIN occupation_translations oc_en ON oc_en.id = up.occupation_id AND oc_en.language = @DefaultLanguage
                           LEFT JOIN religion_translations rl ON rl.id = up.religion_id AND rl.language = @Language
                           LEFT JOIN religion_translations rl_en ON rl_en.id = up.religion_id AND rl_en.language = @DefaultLanguage
                           LEFT JOIN race_translations r ON r.id = up.race_id AND r.language = @Language
                           LEFT JOIN race_translations r_en ON r_en.id = up.race_id AND r_en.language = @DefaultLanguage
                           LEFT JOIN ethnicity_translations e ON e.id = up.ethnicity_id AND e.language = @Language
                           LEFT JOIN ethnicity_translations e_en ON e_en.id = up.ethnicity_id AND e_en.language = @DefaultLanguage
                           WHERE up.user_id = @UserId
                           """;

        var result = await db.QueryFirstOrDefaultAsync<GetByUserIdResult>(
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
        
        return new ProfileDto(personalInfo, locationInfo, professionalInfo, culturalInfo);
    }
}