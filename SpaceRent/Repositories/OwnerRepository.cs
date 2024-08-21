using SpaceRent.Entities;

namespace SpaceRent.Repositories;

public class OwnerRepository : IOwnerRepository
{
    public Owner GetById(int id) => GetValue(id);
    public Owner GetByPropertyId(int propertyId) => GetValue(propertyId);

    private static Owner GetValue(int id)
    {
        return id switch
        {
            0 => new Owner { Id = 0, FullName = "James Padgett" },
            1 => new Owner { Id = 1, FullName = "Janet McCann" },
            2 => new Owner { Id = 2, FullName = "Timothy Thomas" },
            3 => new Owner { Id = 3, FullName = "Barbara Graves" },
            4 => new Owner { Id = 4, FullName = "Susan Fender" },
            _ => throw new ArgumentException($"Owner id not found {id}")
        };
    }
}
