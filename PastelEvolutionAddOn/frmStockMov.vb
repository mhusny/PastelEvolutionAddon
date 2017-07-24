Imports System.Data
Imports System.Data.SqlClient
Public Class frmStockMov

    Public Sub Get_WH()
        Con1.ConnectionString = sConStr
        sSQL = "SELECT WhseLink, Code, Name, KnownAs FROM WhseMst"
        CMD = New SqlCommand(sSQL, Con1)
        CMD.CommandType = CommandType.Text
        DS = New DataSet
        DA = New SqlDataAdapter(CMD)
        Con1.Open()
        DA.Fill(DS)
        Con1.Close()
        cmbWH.DataSource = DS.Tables(0)
        cmbWH.DisplayMember = "Name"
        cmbWH.ValueMember = "WhseLink"
        cmbWH.DisplayLayout.Bands(0).Columns("WhseLink").Hidden = True
        cmbWH.DisplayLayout.Bands(0).Columns("Code").Header.Caption = "Code"
        cmbWH.DisplayLayout.Bands(0).Columns("Name").Header.Caption = "Name"
        cmbWH.DisplayLayout.Bands(0).Columns("KnownAs").Header.Caption = "Description"
    End Sub
    Private Sub frmStockMov_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Get_WH()
    End Sub
    Public Sub Get_Data()
        Try
            If cmbWH.Value = Nothing Then Exit Sub
            sSQL = "SELECT PostST.AccountLink, SUM(PostST.Quantity) AS InvQty, " & _
            " WhseStk.WHQtyOnHand, WhseStk.WHRe_Ord_Lvl, WhseStk.WHRe_Ord_Qty, " & _
            " WhseStk.WHMin_Lvl, WhseStk.WHMax_Lvl, StkItem.Code, StkItem.Description_1,WhseStk.IdWhseStk" & _
            " FROM PostST INNER JOIN StkItem ON PostST.AccountLink = StkItem.StockLink INNER JOIN" & _
            " WhseStk ON PostST.WarehouseID = WhseStk.WHWhseID AND PostST.AccountLink = WhseStk.WHStockLink WHERE (PostST.Id = 'Inv' or PostST.Id='OInv') AND PostST.WarehouseID =" & cmbWH.Value & " AND PostST.TxDate Between '" & Format(dtpFrom.Value, "MM/dd/yyyy") & "' AND '" & Format(dtpTo.Value, "MM/dd/yyyy") & "' GROUP BY PostST.AccountLink, WhseStk.WHQtyOnHand, WhseStk.WHRe_Ord_Lvl, WhseStk.WHRe_Ord_Qty, WhseStk.WHMin_Lvl, WhseStk.WHMax_Lvl, StkItem.Code, StkItem.Description_1,WhseStk.IdWhseStk"
            Con1.ConnectionString = sConStr
            CMD = New SqlCommand(sSQL, Con1)
            CMD.CommandType = CommandType.Text
            DA = New SqlDataAdapter(CMD)
            DS = New DataSet
            Con1.Open()
            DA.Fill(DS)
            Con1.Close()
            UG.DataSource = DS.Tables(0)
            If My.Computer.FileSystem.FileExists(Application.StartupPath & "\StockView.xml") Then
                UG.DisplayLayout.LoadFromXml(Application.StartupPath & "\StockView.xml")
            End If
            UG.DisplayLayout.Bands(0).Columns("AccountLink").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("AccountLink").Header.Caption = "ItemID"
            UG.DisplayLayout.Bands(0).Columns("InvQty").Header.Caption = "Inv Qty"
            UG.DisplayLayout.Bands(0).Columns("InvQty").Format = "0.00"
            UG.DisplayLayout.Bands(0).Columns("InvQty").CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right
            UG.DisplayLayout.Bands(0).Columns("WHQtyOnHand").Header.Caption = "Qty On Hand"
            UG.DisplayLayout.Bands(0).Columns("WHQtyOnHand").Format = "0.00"
            UG.DisplayLayout.Bands(0).Columns("WHQtyOnHand").CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right
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
            UG.DisplayLayout.Bands(0).Columns("Code").Header.Caption = "Item Code"
            UG.DisplayLayout.Bands(0).Columns("Description_1").Header.Caption = "Item Description"
            UG.DisplayLayout.Bands(0).Columns("IdWhseStk").Header.Caption = "IdWhse"
            UG.DisplayLayout.Bands(0).Columns("IdWhseStk").Hidden = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Add On Module")
        Finally
            If Con1.State = ConnectionState.Open Then Con1.Close()
        End Try
    End Sub
    Private Sub tsbView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbView.Click
        Get_Data()
    End Sub

    Private Sub tsbSaveSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSaveSetting.Click
        UG.DisplayLayout.SaveAsXml(Application.StartupPath & "\StockView.xml")
    End Sub
    Private Sub cmbWH_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbWH.ValueChanged
        Get_Data()
    End Sub

    Private Sub dtpTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpTo.ValueChanged
        Get_Data()
    End Sub

    Private Sub dtpFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFrom.ValueChanged
        Get_Data()
    End Sub

    Private Sub tsbSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSave.Click
        If UG.ActiveRow.Selected = True Then
            frmUpdateDefault.Get_Ordering(UG.ActiveRow.Cells("IdWhseStk").Value, cmbWH.Value, UG.ActiveRow.Cells("AccountLink").Value)
            frmUpdateDefault.ShowDialog()
        End If
    End Sub
End Class