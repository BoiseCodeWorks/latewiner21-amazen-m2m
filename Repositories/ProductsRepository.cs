using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using amazen.Models;
using Dapper;

namespace amazen.Repositories
{
  public class ProductsRepository
  {
    private readonly IDbConnection _db;
    public ProductsRepository(IDbConnection db)
    {
      _db = db;
    }


    internal IEnumerable<Product> GetAll()
    {
      string sql = @"
      SELECT 
      prod.*,
      prof.*
      FROM products prod
      JOIN profiles prof ON prod.creatorId = prof.id";
      return _db.Query<Product, Profile, Product>(sql, (product, profile) =>
      {
        product.Creator = profile;
        return product;
      }, splitOn: "id");
    }

    internal Product GetById(int id)
    {
      string sql = @" 
      SELECT 
      prod.*,
      prof.*
      FROM products prod
      JOIN profiles prof ON prod.creatorId = prof.id
      WHERE prod.id = @id";
      return _db.Query<Product, Profile, Product>(sql, (product, profile) =>
      {
        product.Creator = profile;
        return product;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }

    internal Product Create(Product newProd)
    {
      string sql = @"
      INSERT INTO products 
      (title, description, price, creatorId) 
      VALUES 
      (@Title, @Description, @Price, @creatorId);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newProd);
      newProd.Id = id;
      return newProd;
    }

    internal Product Edit(Product updated)
    {
      string sql = @"
        UPDATE products
        SET
         title = @Title,
         description = @Description,
         price = @Price
        WHERE id = @Id;";
      _db.Execute(sql, updated);
      return updated;
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM products WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }


    internal IEnumerable<WishListProductViewModel> GetProductsByListId(int id)
    {
      string sql = @"SELECT 
      p.*,
      wlp.id AS WishListProductId
      FROM wishlistproducts wlp
      JOIN products p ON p.id = wlp.productId
      WHERE wishlistId = @id;";
      return _db.Query<WishListProductViewModel>(sql, new { id });
    }
  }
}