using System;
using amazen.Models;
using amazen.Repositories;

namespace amazen.Services
{
  public class WishListProductsService
  {
    private readonly WishListProductsRepository _repo;

    public WishListProductsService(WishListProductsRepository repo)
    {
      _repo = repo;
    }

    internal WishListProduct Create(WishListProduct newWLP)
    {
      //TODO if they are creating a wishlistproduct, make sure they are the creator of the list
      return _repo.Create(newWLP);

    }

    internal void Delete(int id)
    {
      //NOTE getbyid to validate its valid and you are the creator
      _repo.Delete(id);
    }
  }
}