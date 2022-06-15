namespace ConfigurationManagementSystem.Application.Dto;

public class OptionGroupDto
{
    public string Id { get; set; }
    public string ParentId { get; set; }
    public string ConfigurationId { get; set; }
    public string Name { get; set; }
    public bool Root { get; set; }
}
