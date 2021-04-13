using System;
using System.Collections.Generic;
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
  public class WishListsController : ControllerBase
  {
    private readonly WishListsService _service;
    private readonly ProductsService _pserv;

    public WishListsController(WishListsService service, ProductsService pserv)
    {
      _service = service;
      _pserv = pserv;
    }

    [HttpGet]
    public ActionResult<IEnumerable<WishList>> Get()
    {
      try
      {
        return Ok(_service.GetAll());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]  // NOTE '{}' signifies a var parameter
    public ActionResult<WishList> Get(int id)
    {
      try
      {
        return Ok(_service.GetById(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpPost]
    [Authorize]
    // NOTE ANYTIME you need to use Async/Await you will return a Task
    public async Task<ActionResult<WishList>> Create([FromBody] WishList newWList)
    {
      try
      {
        // NOTE HttpContext == 'req'
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newWList.CreatorId = userInfo.Id;
        newWList.Creator = userInfo;
        return Ok(_service.Create(newWList));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<WishList>> Edit([FromBody] WishList updated, int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        //NOTE attach creatorId so you can validate they are the creator of the original
        updated.CreatorId = userInfo.Id;
        updated.Creator = userInfo;
        updated.Id = id;
        return Ok(_service.Edit(updated));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<WishList>> Delete(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        //NOTE send userinfo.id so you can validate they are the creator of the original

        return Ok(_service.Delete(id, userInfo.Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }



    //api/wishlists/4/products

    [HttpGet("{id}/products")]  // NOTE '{}' signifies a var parameter
    public ActionResult<IEnumerable<WishListProductViewModel>> GetProductsByListId(int id)
    {
      try
      {
        return Ok(_pserv.GetProductsByListId(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}