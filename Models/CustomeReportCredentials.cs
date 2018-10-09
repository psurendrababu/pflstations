using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using Microsoft.Reporting.WebForms;

namespace PipelineFeatureList.Models
{
    public class CustomReportCredentials : Microsoft.Reporting.WebForms.IReportServerCredentials
    {
        //private string _UserName;
        //private string _PassWord;
        //private string _DomainName;

        //public CustomReportCredentials()
        //{
        //    _UserName = "3cadmin";
        //    _PassWord = "3c.Password1";
        //    _DomainName = "g2partnersllc.local";
        //}

        public bool GetFormsCredentials(out System.Net.Cookie authCookie, out string userName, out string password, out string authority)
        {
            //authCookie = null;
            //userName = "3cadmin";
            //password = "3c.Password1";
            //authority = "Admin"; //????

            //return false;

            // not use FormsCredentials unless you have implements a custom autentication.
            authCookie = null;
            userName = password = authority = null;
            return false;
        }

        public WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;  // not use ImpersonationUser
            }
        }

        public System.Net.ICredentials NetworkCredentials
        {
            get
            {
                string userName = "3cadmin";
                string domainName = "g2partnersllc.local";
                string password = "3c.Password1";

                //System.Net.NetworkCredential nc = new System.Net.NetworkCredential(userName, password, domainName);
                //return new System.Net.NetworkCredential(userName, password, domainName);

                 
                // use NetworkCredentials
                return new System.Net.NetworkCredential(userName, password, domainName);
            }
        }
    }
}