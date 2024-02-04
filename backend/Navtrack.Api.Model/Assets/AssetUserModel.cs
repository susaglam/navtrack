using System.ComponentModel.DataAnnotations;
using Navtrack.DataAccess.Model.Assets;

namespace Navtrack.Api.Model.Assets;

public class AssetUserModel
{  
    [Required]
    public string Email { get; set; }
    
    [Required]
    public AssetRoleType Role { get; set; }

    [Required]
    public string UserId { get; set; }
}