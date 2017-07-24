Imports System.Data
Imports System.Data.SqlClient
Public Class frmDocmentList
    Private Sub Get_Invoice_Data()
        sSQL = "SELECT SageInvNum.AutoIndex, SageInvNum.Description, SageInvNum.InvDate, SageInvNum.ExtOrderNum," & _
        " SageInvNum.DocType, SageInvNum.AccountID, Client.Account, Client.Name FROM SageInvNum LEFT OUTER JOIN" & _
        " Client ON SageInvNum.AccountID = Client.DCLink"
        sSQL += " SELECT Sage_btblInvoiceLines.iInvoiceID, Sage_btblInvoiceLines.iStockCodeID, " & _
        " Sage_btblInvoiceLines.cDescription, Sage_btblInvoiceLines.fQuantity, " & _
        " Sage_btblInvoiceLines.fUnitPriceExcl, Sage_btblInvoiceLines.fUnitPriceIncl, " & _
        " Sage_btblInvoiceLines.fUnitCost, Sage_btblInvoiceLines.fUnitCostMargin, " & _
        " Sage_btblInvoiceLines.fUnitCostMarginAmt, StkItem.Code, StkItem.Description_1" & _
        " FROM Sage_btblInvoiceLines LEFT OUTER JOIN StkItem ON Sage_btblInvoiceLines.iStockCodeID = StkItem.StockLink"
        Dim objSQL As New clsSqlConn
        With objSQL
            DS = New DataSet
            DS = .Get_Data_Sql(sSQL)
            DS.Relations.Add("MY", DS.Tables(0).Columns("AutoIndex"), DS.Tables(1).Columns("iInvoiceID"), False)
            UG.DataSource = DS.Tables(0)
        End With
    End Sub
    Private Sub frmDocmentList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Get_Invoice_Data()
    End Sub

    Private Sub tsbReturnNote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbReturnNote.Click
        If UG.Selected.Rows.Count > 0 Then
            If UG.ActiveRow.Band.Index = 2 Then Exit Sub
            frmReturnNote.cmbTxnTypeCom1.ResetText()
            frmReturnNote.cmbTxnTypeCom2.ResetText()
            frmReturnNote.txtRef.ResetText()
            frmReturnNote.UG.DataSource = Nothing
            frmReturnNote.dtpDate.Value = Date.Now
            frmReturnNote.Get_Data(UG.ActiveRow.Cells("AutoIndex").Value)
            frmReturnNote.ShowDialog()
        End If
    End Sub

    Private Sub UG_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UG.InitializeLayout

    End Sub
End Class