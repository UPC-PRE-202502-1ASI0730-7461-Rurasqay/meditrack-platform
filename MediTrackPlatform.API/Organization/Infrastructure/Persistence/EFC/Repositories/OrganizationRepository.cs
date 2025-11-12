using MediTrackPlatform.API.Organization.Domain.Repositories;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace MediTrackPlatform.API.Organization.Infrastructure.Persistence.EFC.Repositories;

public class OrganizationRepository(AppDbContext context)
    : BaseRepository<Domain.Model.Aggregates.Organization>(context), IOrganizationRepository;