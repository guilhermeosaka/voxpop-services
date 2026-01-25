using Voxpop.Profile.Domain.Abstractions;

namespace Voxpop.Profile.Domain.Models;

public class EducationLevel(Guid id, string code) : CodeBaseModel(id, code);