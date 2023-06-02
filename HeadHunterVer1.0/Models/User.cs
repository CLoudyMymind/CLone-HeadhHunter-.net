using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace HeadHunterVer1._0.Models;

public class User : IdentityUser
{
    public string PathFile { get; set; }
}