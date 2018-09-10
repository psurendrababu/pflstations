using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class UsersData
    {
        public User UserData { get; set; }
        [DisplayName("Group Classification")]
        public List<CheckBoxes> UserTypesCheckBoxes { get; set; }
        [DisplayName("Group Classification")]
        public string UserTypes { get; set; }
    }
}