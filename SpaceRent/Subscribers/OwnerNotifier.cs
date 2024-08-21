using SpaceRent.Entities;

namespace SpaceRent.Subscribers;

public class OwnerNotifier : IDisposable
{
    private readonly Booking _booking;

    public OwnerNotifier(Booking booking)
    {
        _booking = booking;
        _booking.BookingCreated += NotifyOwner;
    }

    public void NotifyOwner(BookingData bookingData)
    {
        Console.WriteLine($"Notifying owner {bookingData.OwnerName} from property {bookingData.Request.PropertyId}...");
    }

    public void Dispose()
    {
        _booking.BookingCreated -= NotifyOwner;
    }
}
