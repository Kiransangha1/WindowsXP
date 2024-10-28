#pragma warning disable CS8618
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace music.Models;
public class LogUser
{           
    public int UserId { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    [DisplayName("Password")]
    public string LogPassword { get; set; }
}