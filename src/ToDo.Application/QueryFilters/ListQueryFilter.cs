
namespace ToDo.Application.QueryFilters;

public class ListQueryFilter
{
    public string? Search { get; set; }
    public int Page { get; set; } = 1;
    public int Take { get; set; } = 10;
}