using System.Threading.Tasks;
using amazen.Models;
using amazen.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace amazen.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class WishListProductsController : ControllerBase
  {
    private readonly WishListProductsService _service;

    public WishListProductsController(WishListProductsService service)
    {
      _service = service;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<WishListProduct>> CreateAsync([FromBody] WishListProduct newWLP)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newWLP.CreatorId = userInfo.Id;
        return Ok(_service.Create(newWLP));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpDelete("{id}")]
    public ActionResult<string> Delete(int id)
    {
      try
      {
        _service.Delete(id);
        return Ok("deleted");
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}