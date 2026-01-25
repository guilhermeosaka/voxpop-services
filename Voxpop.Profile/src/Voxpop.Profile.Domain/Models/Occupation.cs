using Voxpop.Profile.Domain.Abstractions;

namespace Voxpop.Profile.Domain.Models;

public class Occupation(Guid id, string code) : CodeBaseModel(id, code);