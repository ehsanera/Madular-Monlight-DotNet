using Module.Shared.Application.Core;

namespace Module.Shared.Application;

public interface IBaseService<CT, UT, T, ID> where CT : IBaseCreateDto
    where UT : IBaseUpdateDto
    where T : IBaseDto
{
    Task<Result<T?>> GetById(
        ID id
    );

    Task<Result<IEnumerable<T>>> GetAll();

    Task<Result<T>> AddAsync(
        CT t
    );

    Task<Result<T>> Update(
        ID id,
        UT t
    );

    Task<Result<bool>> Delete(
        T entity
    );
}