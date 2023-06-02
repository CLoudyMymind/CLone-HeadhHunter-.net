using System.ComponentModel.DataAnnotations;

namespace HeadHunterVer1._0.Models;

public class Category
{
    [Key]
    public string Id { get; set; }

    public string Name { get; set; }
}