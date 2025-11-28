namespace MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

public record OxygenThreshold
{
    public int Min { get; }

    // Constructor para EF Core y creación
    public OxygenThreshold(int min)
    {
        // Handle default values from persistence (e.g. 0)
        if (min == 0)
        {
            Min = 90;
            return;
        }
        Min = min;
    }

    /// <summary>
    /// Lógica de negocio encapsulada.
    /// Determina si un valor está FUERA del rango seguro.
    /// </summary>
    public bool IsViolatedBy(int value)
    {
        return value < Min;
    }
}