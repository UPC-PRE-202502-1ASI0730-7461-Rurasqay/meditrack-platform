using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Repositories;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace MediTrackPlatform.API.Organization.Infrastructure.Persistence.EFC.Repositories;

public class DoctorRepository(AppDbContext context) 
    : BaseRepository<Doctor>(context), IDoctorRepository;