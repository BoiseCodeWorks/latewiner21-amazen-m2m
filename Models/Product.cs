using System.ComponentModel.DataAnnotations;

namespace amazen.Models
{
  // NOTE if you forget to add "public" is "Inconsistent Accesibilty"
  public class Product
  {
    public int Id { get; set; }
    [Required]
    [MinLength(3)]
    public string Title { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
  }

  //this will be used to get products by listId
  public class WishListProductViewModel : Product
  {
    public int WishListProductId { get; set; }

  }

}