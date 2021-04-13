using System.Collections.Generic;
using System.Data;
using System.Linq;
using amazen.Models;
using Dapper;

namespace amazen.Repositories
{
  public class WishListsRepository
  {
    private readonly IDbConnection _db;
    public WishListsRepository(IDbConnection db)
    {
      _db = db;
    }


    internal IEnumerable<WishList> GetAll()
    {
      string sql = @"
      SELECT 
      wlist.*,
      prof.*
      FROM wishlists wlist
      JOIN profiles prof ON wlist.creatorId = prof.id";
      return _db.Query<WishList, Profile, WishList>(sql, (wishlist, profile) =>
      {
        wishlist.Creator = profile;
        return wishlist;
      }, splitOn: "id");
    }
    //      1 SELECT 
    //      4 wlist.*,
    //      6 prof.*
    //      2 FROM wishlists wlist
    //      5 JOIN profiles prof ON wlist.creatorId = prof.id
    //      3 WHERE wlist.id = @id";
    internal WishList GetById(int id)
    {
      string sql = @" 
      SELECT 
      wlist.*,
      prof.*
      FROM wishlists wlist
      JOIN profiles prof ON wlist.creatorId = prof.id
      WHERE wlist.id = @id";
      return _db.Query<WishList, Profile, WishList>(sql, (wishlist, profile) =>
      {
        wishlist.Creator = profile;
        return wishlist;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }

    internal WishList Create(WishList newWList)
    {
      string sql = @"
      INSERT INTO wishlists 
      (title, creatorId) 
      VALUES 
      (@Title, @creatorId);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newWList);
      newWList.Id = id;
      return newWList;
    }

    internal WishList Edit(WishList updated)
    {
      string sql = @"
        UPDATE wishlists
        SET
         title = @Title
        WHERE id = @Id;";
      _db.Execute(sql, updated);
      return updated;
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM wishlists WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}