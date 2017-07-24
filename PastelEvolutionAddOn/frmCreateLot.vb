Imports Infragistics.Win.UltraWinGrid
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Pastel.Evolution

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing
Public Class frmCreateLot
    Dim ExpDate As Date
    Dim StockID As Integer

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub UltraButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraButton2.Click
        Me.Close()
    End Sub

    Private Sub frmCreateLot_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        dtpExpDate.Value = DateAdd(DateInterval.Day, 60, Date.Now)
        StockID = Integer.Parse(Label3.Text)

        sSQL = " SELECT   _etblLotTracking.idLotTracking,_etblLotTracking.cLotDescription AS Description, _etblLotTracking.dExpiryDate AS [Expiry Date], _etblLotTrackingQty.fQtyOnHand AS [Qty On Hand], " & _
                " _etblLotTrackingQty.fQtyReserved AS [Qty Reserved]" & _
                " FROM         _etblLotTracking INNER JOIN " & _
                " _etblLotTrackingQty ON _etblLotTracking.idLotTracking = _etblLotTrackingQty.iLotTrackingID WHERE  _etblLotTracking.iStockID = " & StockID & " AND _etblLotTrackingQty.fQtyOnHand > 0 "

        Try
            Con1.ConnectionString = sConStr
            CMD = New SqlCommand(sSQL, Con1)
            DS = New DataSet
            DA = New SqlDataAdapter(CMD)

            Con1.Open()
            DA.Fill(DS)
            Con1.Close()

            cmbLot.DataSource = DS.Tables(0)
            cmbLot.ValueMember = "idLotTracking"
            cmbLot.DisplayMember = "Description"
            cmbLot.DisplayLayout.Bands(0).Columns("idLotTracking").Hidden = True

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
            Exit Sub
        Finally
            If Con1.State = ConnectionState.Open Then Con1.Close()
        End Try
        txtLot.Focus()
    End Sub

    Private Sub rbCN_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbCN.CheckedChanged
        If rbCN.Checked = True Then
            cmbLot.Visible = False
            txtLot.Visible = True
            dtpExpDate.DateTime = Now.Date
            LotCN = True
            dtpExpDate.Enabled = True
        ElseIf rbUE.Checked = True Then
            txtLot.Visible = False
            cmbLot.Visible = True
            LotCN = False
            dtpExpDate.Enabled = False
        End If
    End Sub

    Private Sub cmbLot_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'dtpExpDate = cmbLot.
    End Sub

    Private Sub UltraButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraButton1.Click
        Me.Close()

        'Lot_Exp = dtpExpDate.Value
        'If rbCN.Checked = True Then
        '    Lot = txtLot.Text
        'ElseIf rbUE.Checked = True Then
        '    Lot = cmbLot.Text
        'End If


    End Sub

    Private Sub cmbLot_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLot.Leave
        Try
            dtpExpDate.Value = cmbLot.ActiveRow.Cells("Expiry Date").Value
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
            Exit Sub
        End Try
    End Sub

    Private Sub rbUE_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbUE.CheckedChanged

    End Sub
End Class