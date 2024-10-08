using System.Runtime.CompilerServices;
using SpaceRent.Entities;
using SpaceRent.Repositories;
using SpaceRent.Subscribers;

[assembly: InternalsVisibleTo("SpaceRentTests")]

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<ITenantRepository, TenantRepository>();
builder.Services.AddSingleton<Booking, Booking>();
builder.Services.AddSingleton<DateBlocker, DateBlocker>();
builder.Services.AddSingleton<OwnerNotifier, OwnerNotifier>();

var app = builder.Build();

app.Services.GetRequiredService<DateBlocker>();
app.Services.GetRequiredService<OwnerNotifier>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/bookingrequests", IResult (BookingRequest bookingRequest, IOwnerRepository ownerRepository, ITenantRepository tenantRepository, Booking booking) =>
    {
        try
        {
            var owner = ownerRepository.GetByPropertyId(bookingRequest.PropertyId);
            var tenant = tenantRepository.GetById(bookingRequest.TenantId);
            var bookingData = new BookingData
            {
                Request = bookingRequest,
                OwnerName = owner.FullName,
                TenantName = tenant.FullName
            };

            booking.Save(bookingData);
            Console.WriteLine("Booking created.");

            return Results.Created();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    })
    .WithName("Bookings")
    .WithOpenApi();

app.Run();
