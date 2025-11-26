namespace MediTrackPlatform.API.Devices.Interfaces.ACL;

/// <summary>
/// Facade for the Devices bounded context
/// Anti-Corruption Layer interface for external contexts to interact with the Devices bounded context
/// </summary>
public interface IDevicesContextFacade
{
    /// <summary>
    /// Create a new device
    /// </summary>
    /// <param name="model">The device model</param>
    /// <param name="holderId">The holder ID (SeniorCitizen ID)</param>
    /// <returns>The device ID, or 0 if creation failed</returns>
    Task<int> CreateDevice(string model, int holderId);
    
    /// <summary>
    /// Fetch a device ID by holder ID
    /// </summary>
    /// <param name="holderId">The holder ID</param>
    /// <returns>The device ID, or 0 if not found</returns>
    Task<int> FetchDeviceIdByHolderId(int holderId);
    
    /// <summary>
    /// Check if a device exists
    /// </summary>
    /// <param name="deviceId">The device ID</param>
    /// <returns>true if the device exists, false otherwise</returns>
    Task<bool> DeviceExists(int deviceId);
}

