using System;
using System.Collections.Generic;
using amazen.Models;
using amazen.Repositories;

namespace amazen.Services
{
  public class ProductsService
  {
    private readonly ProductsRepository _repo;

    public ProductsService(ProductsRepository repo)
    {
      _repo = repo;
    }

    internal IEnumerable<Product> GetAll()
    {
      return _repo.GetAll();
    }

    internal Product GetById(int id)
    {
      var data = _repo.GetById(id);
      if (data == null)
      {
        throw new Exception("Invalid Id");
      }
      return data;
    }

    internal Product Create(Product newProd)
    {
      return _repo.Create(newProd);
    }

    internal Product Edit(Product updated)
    {
      var original = GetById(updated.Id);
      if (original.CreatorId != updated.CreatorId)
      {
        throw new Exception("Invalid Edit Permissions");
      }
      updated.Description = updated.Description != null ? updated.Description : original.Description;
      updated.Title = updated.Title != null && updated.Title.Length > 2 ? updated.Title : original.Title;
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

    internal IEnumerable<WishListProductViewModel> GetProductsByListId(int id)
    {
      return _repo.GetProductsByListId(id);
    }
  }
}