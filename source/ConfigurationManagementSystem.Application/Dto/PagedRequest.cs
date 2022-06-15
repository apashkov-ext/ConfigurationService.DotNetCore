namespace ConfigurationManagementSystem.Application.Dto;

public class PagedRequest
{
    public string Name { get; set; }
    public int? Offset { get; set; }
    public int? Limit { get; set; }
}
