using MediTrackPlatform.API.Relatives.Domain.Model.Aggregates;
using MediTrackPlatform.API.Relatives.Domain.Model.Commands;

namespace MediTrackPlatform.API.Relatives.Domain.Services;

public interface IRelativeCommandService
{
    Task<Relative?> Handle(CreateRelativeCommand command);
}
