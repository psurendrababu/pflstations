<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportViewerUserControl.ascx.cs" Inherits="PipelineFeatureList.ReportViewerUserControl" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<script runat="server">
  private void Page_Load(object sender, System.EventArgs e)
  {
      //Copied from example, but this would only work if we were doing in-project, locally-rendered reports
      //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ConfirmationSummary.rdlc");
      //ReportViewer1.ServerReport.Refresh();
  }
</script>
<form id="Form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="false"></rsweb:ReportViewer>
</form>