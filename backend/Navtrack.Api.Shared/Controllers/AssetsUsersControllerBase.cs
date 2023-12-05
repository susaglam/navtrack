using System.Threading.Tasks;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Navtrack.Api.Model;
using Navtrack.Api.Model.Assets;
using Navtrack.Api.Model.Common;
using Navtrack.Api.Services.ActionFilters;
using Navtrack.Api.Services.Assets;
using Navtrack.DataAccess.Model.Assets;

namespace Navtrack.Api.Shared.Controllers;

[ApiController]
[Authorize(IdentityServerConstants.LocalApi.PolicyName)]
public abstract class AssetsUsersControllerBase(IAssetService service) : ControllerBase
{
    [HttpGet(ApiPaths.AssetsAssetUsers)]
    [ProducesResponseType(typeof(ListModel<AssetUserModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [AuthorizeAsset(AssetRoleType.Owner)]
    public virtual async Task<JsonResult> GetAssetUsers([FromRoute] string assetId)
    {
        ListModel<AssetUserModel> assetUserList = await service.GetAssetUsers(assetId);

        return new JsonResult(assetUserList);
    }

    [HttpPost(ApiPaths.AssetsAssetUsers)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
    [AuthorizeAsset(AssetRoleType.Owner)]
    public virtual async Task<IActionResult> AddUserToAsset([FromRoute] string assetId, [FromBody] AddUserToAssetModel model)
    {
        await service.AddUserToAsset(assetId, model);

        return Ok();
    }

    [HttpDelete(ApiPaths.AssetsAssetUsersUser)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
    [AuthorizeAsset(AssetRoleType.Owner)]
    public virtual async Task<IActionResult> DeleteUserFromAsset([FromRoute] string assetId, [FromRoute] string userId)
    {
        await service.RemoveUserFromAsset(assetId, userId);

        return Ok();
    }
}