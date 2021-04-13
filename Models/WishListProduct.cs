namespace amazen.Models
{
  public class WishListProduct
  {
    public int Id { get; set; }
    public int WishListId { get; set; }
    public int ProductId { get; set; }
    public string CreatorId { get; set; }
  }
}