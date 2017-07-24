Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Public Class frmPrintPreview
    Public vReportPath As String
    Public vSelectionFormula As String
    Public vInvNo As String
    Public vPState As String
    Public vParams As New ParameterFields
    Public vDisValues As ParameterDiscreteValue


    Private Sub CRV_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CRV.Load
        If iRepoID = 1 Then
            CRV.ReportSource = Nothing
            ViewReport()
        ElseIf iRepoID = 2 Then
            CRV.ReportSource = Nothing
            ViewReport_1()
        ElseIf iRepoID = 2 Then
            CRV.ReportSource = Nothing
            ViewReport_svp()
        End If
    End Sub

    Private Sub frmPrintPreview_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Me.CRV.RefreshReport()
        Me.RV.RefreshReport()
    End Sub
    Public Sub Set_Parameters()
        vDisValues = New ParameterDiscreteValue
        'Dim pDateFrom As New ParameterField
        Dim pDateTo As New ParameterField
        Dim pAccount As New ParameterField
        Dim pAd1 As New ParameterField
        Dim pAd2 As New ParameterField
        Dim pAd3 As New ParameterField

        vParams = New ParameterFields

        'pDateFrom.ParameterFieldName = "DateFrom"
        'vDisValues.Value = frmCriteria.dtpFrom.Value
        'pDateFrom.CurrentValues.Add(vDisValues)

        vDisValues = New ParameterDiscreteValue

        pDateTo.ParameterFieldName = "DateTo"
        vDisValues.Value = frmAuditRpt.dtpInDate.Value
        pDateTo.CurrentValues.Add(vDisValues)

        vDisValues = New ParameterDiscreteValue

        pAccount.ParameterFieldName = "Account"
        vDisValues.Value = IIf(frmAuditRpt.cmbAgent.Text = Nothing, "-", frmAuditRpt.cmbAgent.Text)
        pAccount.CurrentValues.Add(vDisValues)

        vDisValues = New ParameterDiscreteValue

        pAd1.ParameterFieldName = "Ad1"
        vDisValues.Value = frmAuditRpt.Ad1.Text
        pAd1.CurrentValues.Add(vDisValues)

        vDisValues = New ParameterDiscreteValue

        pAd2.ParameterFieldName = "Ad2"
        vDisValues.Value = frmAuditRpt.Ad2.Text
        pAd2.CurrentValues.Add(vDisValues)

        vDisValues = New ParameterDiscreteValue

        pAd3.ParameterFieldName = "Ad3"
        vDisValues.Value = frmAuditRpt.Ad3.Text
        pAd3.CurrentValues.Add(vDisValues)

        'vParams.AddRange(New ParameterField() {pDateFrom, pDateTo, pAccount, pCompany})
        vParams.AddRange(New ParameterField() {pDateTo, pAccount, pAd1, pAd2, pAd3})
        CRV.ParameterFieldInfo = vParams
    End Sub
    Public Sub Set_Parameters_1()

        vDisValues = New ParameterDiscreteValue
        Dim pBPO As New ParameterField
        Dim pPState As New ParameterField

        vParams = New ParameterFields

        vDisValues = New ParameterDiscreteValue

        pBPO.ParameterFieldName = "BPO"
        vDisValues.Value = vInvNo
        pBPO.CurrentValues.Add(vDisValues)

        vDisValues = New ParameterDiscreteValue

        pPState.ParameterFieldName = "PState"
        vDisValues.Value = vPState
        pPState.CurrentValues.Add(vDisValues)


        vParams.AddRange(New ParameterField() {pBPO, pPState})
        CRV.ParameterFieldInfo = vParams
    End Sub
    Public Sub Set_Parameters_svp()
        vDisValues = New ParameterDiscreteValue
        Dim pDateFrom As New ParameterField
        Dim pDateTo As New ParameterField
        'Dim pAccount As New ParameterField
        'Dim pAd1 As New ParameterField
        'Dim pAd2 As New ParameterField
        'Dim pAd3 As New ParameterField

        vParams = New ParameterFields

        pDateFrom.ParameterFieldName = "DateFrom"
        'vDisValues.Value = frmCriteria.dtpFrom.Value----------------------------
        pDateFrom.CurrentValues.Add(vDisValues)

        vDisValues = New ParameterDiscreteValue

        pDateTo.ParameterFieldName = "DateTo"
        'vDisValues.Value = frmAudictRpt.dtpInDate.Value--------------------------
        pDateTo.CurrentValues.Add(vDisValues)

        'vDisValues = New ParameterDiscreteValue

        'pAccount.ParameterFieldName = "Account"
        'vDisValues.Value = IIf(frmAuditRpt.cmbAgent.Text = Nothing, "-", frmAuditRpt.cmbAgent.Text)
        'pAccount.CurrentValues.Add(vDisValues)

        'vDisValues = New ParameterDiscreteValue

        'pAd1.ParameterFieldName = "Ad1"
        'vDisValues.Value = frmAuditRpt.Ad1.Text
        'pAd1.CurrentValues.Add(vDisValues)

        'vDisValues = New ParameterDiscreteValue

        'pAd2.ParameterFieldName = "Ad2"
        'vDisValues.Value = frmAuditRpt.Ad2.Text
        'pAd2.CurrentValues.Add(vDisValues)

        'vDisValues = New ParameterDiscreteValue

        'pAd3.ParameterFieldName = "Ad3"
        'vDisValues.Value = frmAuditRpt.Ad3.Text
        'pAd3.CurrentValues.Add(vDisValues)

        vParams.AddRange(New ParameterField() {pDateFrom, pDateTo})
        'vParams.AddRange(New ParameterField() {pDateTo, pAccount, pAd1, pAd2, pAd3})
        CRV.ParameterFieldInfo = vParams
    End Sub
    Private Sub ViewReport()
        Dim i As Integer
        Dim rc As New ReportDocument
        Dim objConnectionInfo As New CrystalDecisions.Shared.TableLogOnInfo

        rc.Load(Path.GetFullPath("Audit.rpt"))
        Set_Parameters()
        CRV.SelectionFormula = "{_bvARTransactionsFull.AccountLink} =" & frmAuditRpt.cmbAgent.Value & " AND {_bvARTransactionsFull.TxDate} < Date(" & frmAuditRpt.dtpInDate.Value.Year & "," & frmAuditRpt.dtpInDate.Value.Month & "," & frmAuditRpt.dtpInDate.Value.Day & ") "
        'CRV.SelectionFormula = "{_bvARTransactionsFull.AccountLink} =" & frmAuditRpt.cmbAgent.Value & ""

        objConnectionInfo.ConnectionInfo.DatabaseName = sSQLSrvDataBase
        objConnectionInfo.ConnectionInfo.ServerName = sSQLSrvReportSrv
        objConnectionInfo.ConnectionInfo.Password = sSQLSrvPassword
        objConnectionInfo.ConnectionInfo.UserID = sSQLSrvUserName

        For i = 0 To rc.Database.Tables.Count - 1
            rc.Database.Tables(0).ApplyLogOnInfo(objConnectionInfo)
        Next
        CRV.ReportSource = Nothing
        CRV.ReportSource = rc
    End Sub
    Private Sub ViewReport_1()
        Dim i As Integer
        Dim rc As New ReportDocument
        Dim objConnectionInfo As New CrystalDecisions.Shared.TableLogOnInfo

        rc.Load(Path.GetFullPath("IBT.rpt"))
        Set_Parameters_1()
        CRV.SelectionFormula = vSelectionFormula
        'CRV.SelectionFormula = "{_bvARTransactionsFull.AccountLink} =" & frmAuditRpt.cmbAgent.Value & ""

        objConnectionInfo.ConnectionInfo.DatabaseName = sSQLSrvDataBase
        objConnectionInfo.ConnectionInfo.ServerName = sSQLSrvReportSrv
        objConnectionInfo.ConnectionInfo.Password = sSQLSrvPassword
        objConnectionInfo.ConnectionInfo.UserID = sSQLSrvUserName

        For i = 0 To rc.Database.Tables.Count - 1
            rc.Database.Tables(0).ApplyLogOnInfo(objConnectionInfo)
        Next
        CRV.ReportSource = Nothing
        CRV.ReportSource = rc
    End Sub
    Private Sub ViewReport_svp()
        Dim i As Integer
        Dim rc As New ReportDocument
        Dim objConnectionInfo As New CrystalDecisions.Shared.TableLogOnInfo

        rc.Load(Path.GetFullPath("SalesVsPurchaces.rpt"))
        Set_Parameters_svp()
        CRV.SelectionFormula = vSelectionFormula
        'CRV.SelectionFormula = "{_bvARTransactionsFull.AccountLink} =" & frmAuditRpt.cmbAgent.Value & ""

        objConnectionInfo.ConnectionInfo.DatabaseName = sSQLSrvDataBase
        objConnectionInfo.ConnectionInfo.ServerName = sSQLSrvReportSrv
        objConnectionInfo.ConnectionInfo.Password = sSQLSrvPassword
        objConnectionInfo.ConnectionInfo.UserID = sSQLSrvUserName

        For i = 0 To rc.Database.Tables.Count - 1
            rc.Database.Tables(0).ApplyLogOnInfo(objConnectionInfo)
        Next
        CRV.ReportSource = Nothing
        CRV.ReportSource = rc
    End Sub
End Class