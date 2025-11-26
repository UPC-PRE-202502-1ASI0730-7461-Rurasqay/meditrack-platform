namespace MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

public record OxygenThreshold
{
    public int Min { get; }

    // Constructor para EF Core y creación
    public OxygenThreshold(int min)
    {
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