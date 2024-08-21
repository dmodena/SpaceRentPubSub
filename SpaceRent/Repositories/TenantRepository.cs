using SpaceRent.Entities;

namespace SpaceRent.Repositories;

public class TenantRepository : ITenantRepository
{
    public Tenant GetById(int id) => GetValue(id);

    private static Tenant GetValue(int id)
    {
        return id switch
        {
            0 => new Tenant { Id = 0, FullName = "Sherry Burgess" },
            1 => new Tenant { Id = 1, FullName = "Gregoty Morris" },
            2 => new Tenant { Id = 2, FullName = "Kristi Anderson" },
            3 => new Tenant { Id = 3, FullName = "John Blackburn" },
            4 => new Tenant { Id = 4, FullName = "Victor Young" },
            _ => throw new ArgumentException($"Tenant id not found {id}")
        };
    }
}
