using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PipelineFeatureList.Controllers
{
    public partial class AppLibrary
    {
        //public void InsertWorkHistory(ValveSection valvesection, int origStatusID, int actionID, int newsStatusID)
        //{
        //    PipelineFeatureListDBContext db1 = new PipelineFeatureListDBContext();

        //    WorkflowHistory wf = new WorkflowHistory();
        //    wf.ValveSectionID = Convert.ToInt16(valvesection.ValveSectionID);
        //    wf.ChangedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
        //    wf.ChangedOn = DateTime.Now;
        //    wf.Old_WorkflowStatusID = origStatusID;
        //    wf.WorkflowActionID = actionID;
        //    wf.New_WorkflowStatusID = newsStatusID;

        //    db1.WorkflowHistories.Add(wf);
        //    db1.SaveChanges();
        //}
    }
}
