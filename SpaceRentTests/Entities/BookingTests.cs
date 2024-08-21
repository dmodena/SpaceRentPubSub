using SpaceRent.Entities;

namespace SpaceRentTests.Entities;

public class BookingTests
{
    private readonly BookingData _bookingData = new()
    {
        Request = new BookingRequest(0, 0, DateTime.Now, DateTime.Now.AddDays(1)),
        OwnerName = "John Doe",
        TenantName = "Jane Doe"
    };

    [Fact]
    public void Save_RaisesEvent()
    {
        var sut = new Booking();
        var eventRaised = false;
        sut.BookingCreated += _ => eventRaised = true;

        sut.Save(_bookingData);

        Assert.True(eventRaised);
    }
}
