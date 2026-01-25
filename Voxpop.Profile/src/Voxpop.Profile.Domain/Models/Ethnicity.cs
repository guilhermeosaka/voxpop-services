using Voxpop.Profile.Domain.Abstractions;

namespace Voxpop.Profile.Domain.Models;

public class Ethnicity(Guid id, string code) : CodeBaseModel(id, code);