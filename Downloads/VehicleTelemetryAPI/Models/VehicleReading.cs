public class VehicleReading
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public float Speed { get; set; }
    public float EngineTemperature { get; set; }
    public DateTime Timestamp { get; set; }

    public Vehicle Vehicle { get; set; }
}