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
  public class ProductsController : ControllerBase
  {
    private readonly ProductsService _service;
    public ProductsController(ProductsService ps)
    {
      _service = ps;
    }

    [HttpGet]
    public ActionResult<Product> Get()
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
    public ActionResult<Product> Get(int id)
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
    public async Task<ActionResult<Product>> Create([FromBody] Product newProd)
    {
      try
      {
        // NOTE HttpContext == 'req'
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newProd.CreatorId = userInfo.Id;
        newProd.Creator = userInfo;
        return Ok(_service.Create(newProd));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Product>> Edit([FromBody] Product updated, int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
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
    public async Task<ActionResult<Product>> Delete(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_service.Delete(id, userInfo.Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}