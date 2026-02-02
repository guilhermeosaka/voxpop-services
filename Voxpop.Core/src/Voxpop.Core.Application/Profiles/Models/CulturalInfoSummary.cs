namespace Voxpop.Core.Application.Profiles.Models;

public record CulturalInfoSummary(
    ReferenceInfoSummary Religion,
    ReferenceInfoSummary Race,
    ReferenceInfoSummary Ethnicity);