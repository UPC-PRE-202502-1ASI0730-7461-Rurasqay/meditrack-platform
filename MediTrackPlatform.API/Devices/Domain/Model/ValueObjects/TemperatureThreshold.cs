namespace MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

public record TemperatureThreshold
{
    public int Min { get; }
    public int Max { get; }

    // Constructor para EF Core y creación
    public TemperatureThreshold(int min, int max)
    {
        if (min >= max)
            throw new ArgumentException("El umbral mínimo no puede ser mayor o igual al máximo.");
        
        Min = min;
        Max = max;
    }

    /// <summary>
    /// Lógica de negocio encapsulada.
    /// Determina si un valor está FUERA del rango seguro.
    /// </summary>
    public bool IsViolatedBy(int value)
    {
        return value < Min || value > Max;
    }
}