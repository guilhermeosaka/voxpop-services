using Voxpop.Profile.Domain.Abstractions;

namespace Voxpop.Profile.Domain.Models;

public class Religion(Guid id, string code) : CodeBaseModel(id, code);