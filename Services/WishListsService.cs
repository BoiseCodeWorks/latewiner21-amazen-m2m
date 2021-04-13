using System;
using System.Collections.Generic;
using amazen.Models;
using amazen.Repositories;

namespace amazen.Services
{
  public class WishListsService
  {
    private readonly WishListsRepository _repo;

    public WishListsService(WishListsRepository repo)
    {
      _repo = repo;
    }

    internal IEnumerable<WishList> GetAll()
    {
      return _repo.GetAll();
    }

    internal WishList GetById(int id)
    {
      var data = _repo.GetById(id);
      if (data == null)
      {
        throw new Exception("Invalid Id");
      }
      return data;
    }

    internal WishList Create(WishList newProd)
    {
      return _repo.Create(newProd);
    }

    internal WishList Edit(WishList updated)
    {
      var original = GetById(updated.Id);
      if (original.CreatorId != updated.CreatorId)
      {
        throw new Exception("Invalid Edit Permissions");
      }
      updated.Title = updated.Title != null ? updated.Title : original.Title;
      return _repo.Edit(updated);
    }


    internal string Delete(int id, string userId)
    {
      var original = GetById(id);
      if (original.CreatorId != userId)
      {
        throw new Exception("Invalid Delete Permissions");
      }
      _repo.Delete(id);
      return "delorted";
    }
  }
}