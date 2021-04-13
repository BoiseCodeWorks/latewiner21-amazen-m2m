using System;
using System.Data;
using amazen.Models;
using Dapper;

namespace amazen.Repositories
{
  public class WishListProductsRepository
  {
    private readonly IDbConnection _db;
    public WishListProductsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal WishListProduct Create(WishListProduct newWLP)
    {
      string sql = @"
      INSERT INTO wishlistproducts 
      (productId, wishlistId, creatorId) 
      VALUES 
      (@ProductId, @WishListId, @CreatorId);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newWLP);
      newWLP.Id = id;
      return newWLP;
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM wishlistproducts WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });

    }
  }
}