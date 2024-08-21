This is a .Net 8 solution with an example implementation of the Publish/Subscriber pattern.

For this application, the solution was designed using purely C#, but I also implemented another solution using Azure Service Bus topics, which can be found [here](https://github.com/dmodena/SpaceRentServiceBus).

My intent was to mimic a possible workflow for private property renting, with the following steps:
1. A user (tenant) books a stay for a particular property
    - Granted that all information was already validated, and the rent paid, the system receives a BookingRequest through an API
2. The payload received is enriched with information from the owner and the tenant
    - I simulate retrieving data from the database to create a new object, BookingData
3. A new event is raised with the contents of this object
    - Since there is no actual database, nothing is saved, only the event itself is raised
    - Another class, Booking, is responsible for dealing with the event and subscriptions
4. Two separate services are subscribed and react to this event
    1. The first is called DateBlocker, and it simulates blocking the dates for that particular property, to prevent other users from trying to book the same dates
    2. The second is called OwnerNotifier, and it mimics a service that could send an email to notify the owner of the property about the rental

This has all been created in a single project, and there is a second project in the solution for the unit tests.

Author: Douglas Modena  
License: MIT  
2024
