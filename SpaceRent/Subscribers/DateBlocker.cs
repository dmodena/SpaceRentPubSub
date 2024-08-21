using SpaceRent.Entities;

namespace SpaceRent.Subscribers;

public class DateBlocker : IDisposable
{
    private readonly Booking _booking;

    public DateBlocker(Booking booking)
    {
        _booking = booking;
        _booking.BookingCreated += BlockDate;
    }

    public void BlockDate(BookingData bookingData)
    {
        Console.WriteLine($"Blocking property {bookingData.Request.PropertyId} from date {bookingData.Request.RentStart} to date {bookingData.Request.RentEnd}...");
    }

    public void Dispose()
    {
        _booking.BookingCreated -= BlockDate;
    }
}
