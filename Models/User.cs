#pragma warning disable CS8618
namespace music.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{        
    [Key]        
    public int UserId { get; set; }

    [Required]
    public string? Image { get;set; }

    [Required]        
    [Display(Name ="Username")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
    public string Name { get; set; }        
    
    [Required]
    [EmailAddress]
    [UniqueEmail]
    public string Email { get; set; }        
    
    [Required]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    public string Password { get; set; }          
    
    public DateTime CreatedAt {get;set;} = DateTime.Now;        
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    
    [NotMapped]
    [DataType(DataType.Password)]
    [Compare("Password")]
    [Display(Name ="Confirm Password")]
    public string ConfirmPassword { get; set; }

    public List<Photo> ReportedPhotos { get;set; } = new();

}

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
    	// Though we have Required as a validation, sometimes we make it here anyways
    	// In which case we must first verify the value is not null before we proceed
        if(value == null)
        {
    	    // If it was, return the required error
            return new ValidationResult("Email is required!");
        }
    
    	// This will connect us to our database since we are not in our Controller
        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));
        // Check to see if there are any records of this email in our database
    	if(_context.Users.Any(e => e.Email == value.ToString()))
        {
    	    // If yes, throw an error
            return new ValidationResult("Email must be unique!");
        } else {
    	    // If no, proceed
            return ValidationResult.Success;
        }
    }
}