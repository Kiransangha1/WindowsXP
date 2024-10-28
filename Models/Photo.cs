#pragma warning disable CS8618

namespace music.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class Photo
{

    [Key]
    public int PhotoId { get;set; }

    [Required]
    [MinLength(2)]
    [MaxLength(30)]
    public string Title { get;set; }

    [DisplayName("Image URL")]
    public string? ImgURL { get;set; }

    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;

    public int UserId { get;set; }

    public User? ReportingUser { get;set; }
}