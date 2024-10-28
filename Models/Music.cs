// #pragma warning disable CS8618

// namespace music.Models;

// using System.ComponentModel;
// using System.ComponentModel.DataAnnotations;

// public class Music
// {

//     [Key]
//     public int MusicId { get;set; }

//     [Required]
//     [MinLength(2)]
//     [MaxLength(30)]
//     public string Title { get;set; }

//     [DisplayName("Video URL")]
//     public string? VidURL { get;set; }

//     public DateTime CreatedAt {get;set;} = DateTime.Now;
//     public DateTime UpdatedAt {get;set;} = DateTime.Now;

//     public int UserId { get;set; }

//     public User? ReportingUser { get;set; }
// }