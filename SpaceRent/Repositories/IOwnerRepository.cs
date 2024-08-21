using SpaceRent.Entities;

namespace SpaceRent.Repositories;

public interface IOwnerRepository : IBaseRepository<Owner>
{
    Owner GetByPropertyId(int propertyId);
}
