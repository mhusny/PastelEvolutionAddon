Imports System.Data
Imports System.Data.SqlClient
Public Class frmUpdateDefault
    Public Sub Get_Ordering(ByVal idWH As Integer, ByVal WH As Integer, ByVal ST As Integer)
        Try
            sSQL = "SELECT WHRe_Ord_Lvl, WHRe_Ord_Qty, WHMin_Lvl," & _
            " WHMax_Lvl,IdWhseStk FROM WhseStk WHERE  IdWhseStk =" & idWH & " AND WHWhseID = " & WH & " AND WHStockLink =" & ST & ""
            CMD = New SqlCommand(sSQL, Con1)
            CMD.CommandType = CommandType.Text
            DA = New SqlDataAdapter(CMD)
            DS = New DataSet
            Con1.Open()
            DA.Fill(DS)
            Con1.Close()
            UG.DataSource = DS.Tables(0)
            UG.DisplayLayout.Bands(0).Columns("WHRe_Ord_Lvl").Header.Caption = "ReOrder Level"
            UG.DisplayLayout.Bands(0).Columns("WHRe_Ord_Lvl").Format = "0.00"
            UG.DisplayLayout.Bands(0).Columns("WHRe_Ord_Lvl").CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right
            UG.DisplayLayout.Bands(0).Columns("WHRe_Ord_Qty").Header.Caption = "ReOrder Qty"
            UG.DisplayLayout.Bands(0).Columns("WHRe_Ord_Qty").Format = "0.00"
            UG.DisplayLayout.Bands(0).Columns("WHRe_Ord_Qty").CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right
            UG.DisplayLayout.Bands(0).Columns("WHMin_Lvl").Header.Caption = "Minimum"
            UG.DisplayLayout.Bands(0).Columns("WHMin_Lvl").Format = "0.00"
            UG.DisplayLayout.Bands(0).Columns("WHMin_Lvl").CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right
            UG.DisplayLayout.Bands(0).Columns("WHMax_Lvl").Header.Caption = "Maximum"
            UG.DisplayLayout.Bands(0).Columns("WHMax_Lvl").Format = "0.00"
            UG.DisplayLayout.Bands(0).Columns("WHMax_Lvl").CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right
            UG.DisplayLayout.Bands(0).Columns("IdWhseStk").Hidden = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Add On Module")
        Finally
            If Con1.State = ConnectionState.Open Then Con1.Close()
        End Try

    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        DA.Dispose()
        DS.Dispose()
        Me.Close()
    End Sub
    Private Sub tsbSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSave.Click
        Try
            UG.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.CommitRow)
            Dim CB As New SqlCommandBuilder(DA)
            DA.Update(DS)
            If MsgBox("Update Successfully Completed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Add On Module") = MsgBoxResult.Ok Then
                frmStockMov.Get_Data()
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Error")
            If Con1.State = ConnectionState.Open Then Con1.Close()
        End Try
    End Sub

    Private Sub frmUpdateDefault_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        DA.Dispose()
        DS.Dispose()
    End Sub
End Class