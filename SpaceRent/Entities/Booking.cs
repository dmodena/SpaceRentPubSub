namespace SpaceRent.Entities;

public class Booking
{
    public delegate void BookingCreatedHandler(BookingData bookingData);
    public event BookingCreatedHandler? BookingCreated;

    public virtual void Save(BookingData bookingData)
    {
        OnBookingCreated(bookingData);
    }
    private void OnBookingCreated(BookingData bookingData)
    {
        BookingCreated?.Invoke(bookingData);
    }
}
