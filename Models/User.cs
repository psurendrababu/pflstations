using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class User
    {
        [DisplayName("ID")]
        public Int64 UserID { get; set; }
        [Required]
        [DisplayName("Login")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }
        [DisplayName("Active/Inactive")]
        public bool Active { get; set; }
        [DisplayName("Created By")]
        public Int64 CreatedBy_UserID { get; set; }
        [DisplayName("Created Date/Time")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("Modified By")]
        public Int64 ModifiedBy_UserID { get; set; }
        [DisplayName("Modified Date/Time")]
        public DateTime ModifiedOn { get; set; }
    }
}