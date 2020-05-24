using Microsoft.AspNetCore.Mvc;
using SVU.Shared.Enums;
using SVU.Shared.Static;
using SVU.Web.UI.ViewModels.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace SVU.Web.UI.ViewModels.Health
{
    public class HealthUserViewModel : BaseViewModel
    {
        #region Properties

        [Required]
        [StringLength(20)]
        [Display(Name = "Name")]
        [Remote(action: "VerifyUsername", controller: "AWPAccount", ErrorMessage = "Username already used!")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display]
        [Remote(action: "VerifyEmail", controller: "AWPAccount", ErrorMessage = "Email already used!")]
        public string Email { get; set; }

        [Required]
        [Display]
        [DataType(DataType.Password)]
        public string Passoword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(Passoword), ErrorMessage = "Passwords do not match")]
        public string PasswordConfirmation { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        [RegularExpression(RegexExpressions.PhoneNumber, ErrorMessage = "Invaild phone number format")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "Date of birth")]
        public DateTime DOB { get; set; }
        [Required]
        public Gender Gender { get; set; }

        [StringLength(200)]
        public string MedicalHistory { get; set; }

        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public HealthUserViewModel()
        {

        }
        #endregion
    }
}
