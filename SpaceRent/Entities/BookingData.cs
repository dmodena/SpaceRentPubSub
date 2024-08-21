namespace SpaceRent.Entities;

public class BookingData
{
    public int? Id { get; set; }
    public required BookingRequest Request { get; set; }
    public required string OwnerName { get; set; }
    public required string TenantName { get; set; }
}
