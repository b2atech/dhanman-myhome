namespace Dhanman.MyHome.Application.Contracts.Portfolios;

public sealed class PortfolioListResponse
{
    public PortfolioListResponse(IReadOnlyCollection<PortfolioResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }

    public IReadOnlyCollection<PortfolioResponse> Items { get; }
    public string Cursor { get; }
}