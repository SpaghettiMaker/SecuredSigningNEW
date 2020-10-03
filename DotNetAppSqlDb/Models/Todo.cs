using System;
using System.ComponentModel.DataAnnotations;

namespace DotNetAppSqlDb.Models
{
    public class Todo
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Full Address")]
        public string FullAddress { get; set; }
        [Display(Name = "Mailing Address")]
        public string MailingAddress { get; set; }
        [Display(Name = "As Above Address")]
        public bool AsAboveAddress { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public long PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Citizen Status")]
        public string CitizenStatus { get; set; }
        [Display(Name = "Employment Start Date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EmploymentStartDate { get; set; }
        [Required]
        [Display(Name = "Employment Type")]
        public string EmploymentType { get; set; }
        [Required]
        [Display(Name = "Position Title")]
        public string PositionTitle { get; set; }
        [Required]
        [Display(Name = "Emergency Contact Name")]
        public string EmergencyContactName { get; set; }
        [Required]
        [Display(Name = "Emergency Contact Relationship")]
        public string EmergencyContactRelationship { get; set; }
        [Required]
        [Display(Name = "Emergency Contact Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public long EmergencyContactPhoneNumber { get; set; }
        [UIHint("SignaturePad")]
        public byte[] EmployeeSignature { get; set; }
    }
}