namespace MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

public record BloodPressureThreshold
{
    public int Min { get; }
    public int Max { get; }
    
    public BloodPressureThreshold(int min, int max)
    {
        // Handle default values from persistence (e.g. 0, 0)
        if (min == 0 && max == 0)
        {
            Min = 90;
            Max = 180;
            return;
        }

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