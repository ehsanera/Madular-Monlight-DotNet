using Microsoft.EntityFrameworkCore;

namespace Module.Shared.Application.Core;

public class Paginator<T> : List<T>
{
    private Paginator(
        IQueryable<T> query,
        int currentPageNumber,
        int itemsCountPerPage
    )
    {
        Query = query;
        CurrentPageNumber = currentPageNumber;
        ItemsCountPerPage = itemsCountPerPage;
    }

    public int TotalPagesCount { get; private set; }

    public int TotalItemsCount { get; private set; }

    public IQueryable<T> Query { get; }

    public int CurrentPageNumber { get; }

    public int ItemsCountPerPage { get; }

    public async Task Paginate()
    {
        TotalItemsCount = Query.Count();

        TotalPagesCount =
            TotalItemsCount / ItemsCountPerPage +
            (TotalItemsCount % ItemsCountPerPage == 0 ? 0 : 1);

        var currentPageData = await Query
            .Skip((CurrentPageNumber - 1) * ItemsCountPerPage)
            .Take(ItemsCountPerPage)
            .ToListAsync();

        Clear();

        AddRange(currentPageData);
    }

    public static async Task<Paginator<T>> CreatePaginator(
        IQueryable<T> query,
        int currentPageNumber,
        int itemsCountPerPage
    )
    {
        var paginator = new Paginator<T>(
            query,
            currentPageNumber,
            itemsCountPerPage
        );

        await paginator.Paginate();

        return paginator;
    }
}