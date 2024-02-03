using Syncfusion.Maui.DataForm;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SamsTimer.Shared.Models
{
    public class Member : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _firstName;
        private string _lastName;
        private string _address;
        private string _zipcode;
        private string _city;
        private string _phone;
        private DateTime _dateOfBirth;
        private string _email;
        private string _password;

        [Display(Name = "Voornaam")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Voornaam is verplicht")]
        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    var args = new PropertyChangedEventArgs(nameof(FirstName));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        [Display(Name = "Achternaam")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Achternaam is verplicht")]
        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    var args = new PropertyChangedEventArgs(nameof(LastName));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        [Display(Name = "Adres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Adres is verplicht")]
        public string Address
        {
            get
            {
                return _address;
            }

            set
            {
                if (_address != value)
                {
                    _address = value;
                    var args = new PropertyChangedEventArgs(nameof(Address));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        [Display(Name = "Postcode")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Postcode is verplicht")]
        [DataType(DataType.PostalCode)]
        public string ZipCode
        {
            get
            {
                return _zipcode;
            }

            set
            {
                if (_zipcode != value)
                {
                    _zipcode = value;
                    var args = new PropertyChangedEventArgs(nameof(ZipCode));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        [Display(Name = "Woonplaats")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Woonplaats is verplicht")]
        public string City
        {
            get
            {
                return _city;
            }

            set
            {
                if (_city != value)
                {
                    _city = value;
                    var args = new PropertyChangedEventArgs(nameof(City));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        [Display(Name = "Telefoonnummer")]
        [Required(ErrorMessage = "Telefoonnummer is verplicht")]
        public string Phone
        {
            get
            {
                return _phone;
            }

            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    var args = new PropertyChangedEventArgs(nameof(Phone));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        [Display(Name = "Geboortedatum")]
        [Required(ErrorMessage = "Geboortedatum is verplicht")]
        [DataFormDateRange(MinimumDate = "01/01/1900", ErrorMessage = "Geboortedatum is ongeldig")]
        public DateTime DateOfBirth
        {
            get
            {
                return _dateOfBirth;
            }

            set
            {
                if (_dateOfBirth != value)
                {
                    _dateOfBirth = value;
                    var args = new PropertyChangedEventArgs(nameof(DateOfBirth));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        [Required]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                if (_email != value)
                {
                    _email = value;
                    var args = new PropertyChangedEventArgs(nameof(Email));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        [Display(Name = "Wachtwoord")]
        [DataFormDisplayOptions(ValidMessage = "Wachtwoord is sterk")]
        [Required(ErrorMessage = "Kies een wachtwoord")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])[a-zA-Z\d]{8,}$", ErrorMessage = "Een wachtwoord van minimaal 8 tekens moet een combinatie van hoofdletters en kleine letters bevatten.")]
        [DataType(DataType.Password)]
        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                if (_password != value)
                {
                    _password = value;
                    var args = new PropertyChangedEventArgs(nameof(Password));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }
    }
}