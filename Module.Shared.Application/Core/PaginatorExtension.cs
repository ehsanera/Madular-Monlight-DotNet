namespace Module.Shared.Application.Core;

public static class PaginatorExtension
{
    public static async Task<Paginator<T>> Paginate<T>(
        this IQueryable<T> query,
        int currentPageNumber,
        int itemsCountPerPage
    )
    {
        return await Paginator<T>.CreatePaginator(
            query,
            currentPageNumber,
            itemsCountPerPage
        );
    }
}