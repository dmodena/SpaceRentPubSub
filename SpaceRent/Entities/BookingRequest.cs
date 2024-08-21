namespace SpaceRent.Entities;

public record BookingRequest(int TenantId, int PropertyId, DateTime RentStart, DateTime RentEnd);
