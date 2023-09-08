namespace DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;

public class UserInformation
{
    [Key]
    public int Id { get; set;  }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}