using Syncfusion.Maui.DataForm;
using System.ComponentModel.DataAnnotations;

namespace SamsTimer.Shared.Models
{
    public class Member
    {
        [Display(Name = "Voornaam")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Voornaam is verplicht")]
        public string FirstName { get; set; }

        [Display(Name = "Achternaam")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Achternaam is verplicht")]
        public string LastName { get; set; }

        [Display(Name = "Adres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Adres is verplicht")]
        public string Address { get; set; }

        [Display(Name = "Postcode")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Postcode is verplicht")]
        [DataType(DataType.PostalCode)]
        public string Zipcode { get; set; }

        [Display(Name = "Woonplaats")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Woonplaats is verplicht")]
        public string City { get; set; }

        [Display(Name = "Telefoonnummer")]
        [Required(ErrorMessage = "Telefoonnummer is verplicht")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Geboortedatum")]
        [Required(ErrorMessage = "Geboortedatum is verplicht")]
        [DataFormDateRange(MinimumDate = "01/01/1900", ErrorMessage = "Geboortedatum is ongeldig")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Wachtwoord")]
        [DataFormDisplayOptions(ValidMessage = "Wachtwoord is sterk")]
        [Required(ErrorMessage = "Kies een wachtwoord")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])[a-zA-Z\d]{8,}$", ErrorMessage = "Een wachtwoord van minimaal 8 tekens moet een combinatie van hoofdletters en kleine letters bevatten.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}