namespace Module.Shared.Application;

public interface IBaseService<CT, UT, T, ID> where CT : IBaseCreateDto
    where UT : IBaseUpdateDto
    where T : IBaseDto
{
    Task<T?> GetById(ID id);

    IEnumerable<T> GetAll();

    Task<T> AddAsync(CT t);

    Task<T> Update(ID id, UT t);

    void Delete(ID id);
}