using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PipelineFeatureList.Models
{
    public class CheckBoxes
    {
        public bool Selected { get; set; }
        public int Value { get; set; }
        public string Text { get; set; }
    }

    public class GroupClassificationsModel
    {
        public GroupClassificationsModel(IList<GroupClassification> items)
        {
            this.GroupClassifications = items;
        }

        public IList<GroupClassification> GroupClassifications { get; set; }
    }
}