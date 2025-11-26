using MediTrackPlatform.API.Devices.Application.ACL;
using MediTrackPlatform.API.Devices.Application.CommandServices;
using MediTrackPlatform.API.Devices.Application.QueryServices;
using MediTrackPlatform.API.Devices.Domain.Repositories;
using MediTrackPlatform.API.Devices.Domain.Services;
using MediTrackPlatform.API.Devices.Infrastructure.Persistence.EFC.Repositories;
using MediTrackPlatform.API.Devices.Interfaces.ACL;

namespace MediTrackPlatform.API.Devices.Infrastructure.Interfaces.ASP.Configuration;

public static class WebApplicationBuilderExtensions
{
    public static void AddDevicesContextServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAlertRepository, AlertRepository>();
        builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
        builder.Services.AddScoped<IAlertCommandService, AlertCommandService>();
        builder.Services.AddScoped<IAlertQueryService, AlertQueryService>();
        builder.Services.AddScoped<IDeviceCommandService, DeviceCommandService>();
        builder.Services.AddScoped<IDeviceQueryService, DeviceQueryService>();
        
        // ACL - Anti-Corruption Layer
        builder.Services.AddScoped<IDevicesContextFacade, DevicesContextFacade>();
    }
}