namespace SpaceRent.Repositories;

public interface IBaseRepository<T>
{
    T GetById(int id);
}
