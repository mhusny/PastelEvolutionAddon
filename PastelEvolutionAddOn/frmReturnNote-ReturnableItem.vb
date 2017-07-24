Imports System.Data
Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid
Imports System.IO
Public Class frmReturnNote
    Public Sub Get_Company_2_Txn_Code()
        If cmbCom2.Text <> "" Then
            sSQL = "SELECT     idTrCodes, Code, Description FROM TrCodes WHERE iModule = 11"
            CMD = New SqlCommand(sSQL, Con2)
            CMD.CommandType = CommandType.Text
            DA = New SqlDataAdapter(CMD)
            DS = New DataSet
            Con2.Open()
            DA.Fill(DS)
            Con2.Close()
            cmbTxnTypeCom2.DataSource = DS.Tables(0)
            cmbTxnTypeCom2.ValueMember = "idTrCodes"
            cmbTxnTypeCom2.DisplayMember = "Code"
            cmbTxnTypeCom2.DisplayLayout.Bands(0).Columns("idTrCodes").Hidden = True
        End If
    End Sub
    Public Sub Get_Company_1_Txn_Code()
        sSQL = "SELECT     idTrCodes, Code, Description FROM TrCodes WHERE iModule = 11"
        CMD = New SqlCommand(sSQL, Con1)
        CMD.CommandType = CommandType.Text
        DA = New SqlDataAdapter(CMD)
        DS = New DataSet
        Con1.Open()
        DA.Fill(DS)
        Con1.Close()
        cmbTxnTypeCom1.DataSource = DS.Tables(0)
        cmbTxnTypeCom1.ValueMember = "idTrCodes"
        cmbTxnTypeCom1.DisplayMember = "Code"
        cmbTxnTypeCom1.DisplayLayout.Bands(0).Columns("idTrCodes").Hidden = True
    End Sub
    Public Sub Get_Data(ByVal ID As Integer)
        sSQL = "SELECT StkItem.Code, StkItem.Description_1, StkItem.cSimpleCode, Sage_btblInvoiceLines.fQuantity," & _
        " Sage_btblInvoiceLines.fUnitCost,Sage_btblInvoiceLines.fUnitPriceIncl, Sage_btblInvoiceLines.fUnitPriceExcl," & _
        " Sage_btblInvoiceLines.fUnitCostMargin,Sage_btblInvoiceLines.fUnitCostMarginAmt," & _
        " Sage_btblInvoiceLines.iInvoiceID,Sage_btblInvoiceLines.idInvoiceLines,Sage_btblInvoiceLines.iWarehouseID,Sage_btblInvoiceLines.iStockCodeID FROM Sage_btblInvoiceLines LEFT OUTER JOIN StkItem ON Sage_btblInvoiceLines.iStockCodeID = StkItem.StockLink WHERE Sage_btblInvoiceLines.iInvoiceID =" & ID & ""
        CMD = New SqlCommand(sSQL, Con1)
        CMD.CommandType = CommandType.Text
        DS = New DataSet
        DA = New SqlDataAdapter(CMD)
        Con1.Open()
        DA.Fill(DS)
        Con1.Close()
        UG.DataSource = DS.Tables(0)
        Dim band As UltraGridBand = UG.DisplayLayout.Bands(0)
        If Not band.Columns.Exists("RETURNQTY") Then
            band.Columns.Add("RETURNQTY")
            band.Columns("RETURNQTY").DataType = GetType(Decimal)
            band.Columns("RETURNQTY").Format = "0.00"
            band.Columns("RETURNQTY").CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right
            band.Columns("RETURNQTY").Header.Caption = "Return Qty"
            band.Columns("RETURNQTY").DefaultCellValue = 0.0
            band.Columns("RETURNQTY").NullText = "0.00"
            band.Columns("RETURNQTY").CellClickAction = CellClickAction.Edit
            band.Columns("RETURNQTY").CellActivation = Activation.AllowEdit
        End If
        UG.DisplayLayout.Bands(0).Columns("idInvoiceLines").Hidden = True
        UG.DisplayLayout.Bands(0).Columns("iInvoiceID").Hidden = True
        UG.DisplayLayout.Bands(0).Columns("iWarehouseID").Hidden = True
        UG.DisplayLayout.Bands(0).Columns("iStockCodeID").Hidden = True
        UG.DisplayLayout.Bands(0).Columns("fUnitCostMarginAmt").Header.Caption = "Margin Amt"
        UG.DisplayLayout.Bands(0).Columns("fUnitCostMarginAmt").CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right
        UG.DisplayLayout.Bands(0).Columns("fUnitCostMarginAmt").Format = "0.00"
        UG.DisplayLayout.Bands(0).Columns("fUnitCostMargin").Header.Caption = "Margin (%)"
        UG.DisplayLayout.Bands(0).Columns("fUnitCostMargin").CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right
        UG.DisplayLayout.Bands(0).Columns("fUnitCostMargin").Format = "0.00"
        UG.DisplayLayout.Bands(0).Columns("fUnitPriceExcl").Header.Caption = "Unit Price (Excl)"
        UG.DisplayLayout.Bands(0).Columns("fUnitPriceExcl").CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right
        UG.DisplayLayout.Bands(0).Columns("fUnitPriceExcl").Format = "0.00"
        UG.DisplayLayout.Bands(0).Columns("fUnitPriceIncl").Header.Caption = "Unit Print (Incl)"
        UG.DisplayLayout.Bands(0).Columns("fUnitPriceIncl").CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right
        UG.DisplayLayout.Bands(0).Columns("fUnitPriceIncl").Format = "0.00"
        UG.DisplayLayout.Bands(0).Columns("fUnitCost").Header.Caption = "Unit Cost"
        UG.DisplayLayout.Bands(0).Columns("fUnitCost").CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right
        UG.DisplayLayout.Bands(0).Columns("fUnitCost").Format = "0.00"
        UG.DisplayLayout.Bands(0).Columns("fQuantity").Header.Caption = "Qty"
        UG.DisplayLayout.Bands(0).Columns("fQuantity").CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right
        UG.DisplayLayout.Bands(0).Columns("fQuantity").Format = "0.00"
        UG.DisplayLayout.Bands(0).Columns("Description_1").Header.Caption = "Description"
        UG.DisplayLayout.Bands(0).Columns("cSimpleCode").Header.Caption = "Simple Code"
        UG.DisplayLayout.Bands(0).Columns("Code").Header.Caption = "Item Code"
    End Sub
    Private Sub Read_Company()
        Dim oReader As StreamReader
        oReader = File.OpenText(Application.StartupPath & "\LOC.txt")
        Do While oReader.Peek <> -1
            cmbCom2.Items.Add(oReader.ReadLine)
        Loop

        oReader.Close()
    End Sub
    Private Sub frmReturnNote_ReturnableItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Read_Company()
        Get_Company_1_Txn_Code()
    End Sub

    Public Function Exe_SQL_Trans1(ByVal SQL As String) As Integer
        Try
            CMD = New SqlCommand(SQL, Con1, Trans1)
            CMD.CommandType = CommandType.Text
            CMD.ExecuteNonQuery()
            Return 1
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Function Exe_SQL_Trans2(ByVal SQL As String) As Integer
        Try
            CMD = New SqlCommand(SQL, Con2, Trans2)
            CMD.CommandType = CommandType.Text
            CMD.ExecuteNonQuery()
            Return 1
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Private Sub tsbUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbUpdate.Click
        UG.PerformAction(UltraGridAction.CommitRow)
        If UG.Rows.Count = 0 Then
            MsgBox("Can not find the Material Issue Note Details", MsgBoxStyle.Exclamation, "Sage AddOn")
            Exit Sub
        End If
        Dim Count As Integer = 0

        If cmbCom2.Text.Trim.Length = 0 Then
            MsgBox("Select Loc 2", MsgBoxStyle.Exclamation, "Add On")
            Exit Sub
        End If

        If cmbTxnTypeCom1.Value = Nothing Or cmbTxnTypeCom2.Value = Nothing Then
            MsgBox("Select Inventory Transaction Types", MsgBoxStyle.Exclamation, "AddOn")
            Exit Sub
        End If
        If txtRef.Text.Trim.Length = 0 Then
            MsgBox("Enter Reference", MsgBoxStyle.Exclamation, "Add On")
            Exit Sub
        End If

        Dim Dr As UltraGridRow
        For Each Dr In UG.Rows
            If Dr.Cells("RETURNQTY").Value > 0 Then
                Count = Count + 1
            End If
            If Dr.Cells("RETURNQTY").Value > Dr.Cells("fQuantity").Value Then
                MsgBox("Return Qty should be less then Quantity Issue", MsgBoxStyle.Exclamation, "AddOn Module")
                Exit Sub
            End If
        Next
        If Count = 0 Then
            MsgBox("Return Qty can not be 0", MsgBoxStyle.Exclamation, "Sage AddOn")
            Exit Sub
        End If

        Try

            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & cmbCom2.Text.Trim & "")

            Con1.Open()
            Trans1 = Con1.BeginTransaction

            Con2.Open()
            Trans2 = Con2.BeginTransaction

            For Each Dr In UG.Rows
                If Dr.Cells("RETURNQTY").Value > 0 Then
                    Dim sRef As String = txtRef.Text
                    sSQL = "INSERT INTO _etblInvJrBatchLines(iInvJrBatchID, iStockID, iWarehouseID, dTrDate," & _
                    "iTrCodeID, cReference, cDescription,fQtyOut, fNewCost) VALUES ('1'," & Dr.Cells("iStockCodeID").Value & "," & Dr.Cells("iWarehouseID").Value & "," & _
                    " '" & Format(dtpDate.Value, "MM/dd/yyyy") & "'," & cmbTxnTypeCom1.Value & ",'" & frmDocmentList.UG.ActiveRow.Cells("ExtOrderNum").Value & "',' " & sRef & "'," & Dr.Cells("RETURNQTY").Value & "," & Dr.Cells("fUnitPriceExcl").Value - Dr.Cells("fUnitCostMarginAmt").Value & ")"
                    If Exe_SQL_Trans1(sSQL) = 0 Then
                        Trans1.Rollback()
                        If Con1.State = ConnectionState.Open Then Con1.Close()
                        MsgBox("Error Found while saving details", MsgBoxStyle.Exclamation, "Pastel Evolution")
                        Exit Sub
                    Else
                        sSQL = "INSERT INTO _etblInvJrBatchLines(iInvJrBatchID, iStockID, iWarehouseID, dTrDate," & _
                        "iTrCodeID, cReference, cDescription,fQtyIn, fNewCost) VALUES ('1'," & Dr.Cells("iStockCodeID").Value & "," & Dr.Cells("iWarehouseID").Value & "," & _
                        " '" & Format(dtpDate.Value, "MM/dd/yyyy") & "'," & cmbTxnTypeCom1.Value & ",'" & frmDocmentList.UG.ActiveRow.Cells("ExtOrderNum").Value & "',' " & sRef & "'," & Dr.Cells("RETURNQTY").Value & "," & Dr.Cells("fUnitPriceExcl").Value - Dr.Cells("fUnitCostMarginAmt").Value & ")"
                        If Exe_SQL_Trans2(sSQL) = 0 Then
                            Trans2.Rollback()
                            If Con2.State = ConnectionState.Open Then Con2.Close()
                            MsgBox("Error Found while saving details", MsgBoxStyle.Exclamation, "Pastel Evolution")
                            Exit Sub
                        End If
                    End If
                End If
            Next
            Trans1.Commit()
            Trans2.Commit()
            If Con1.State = ConnectionState.Open Then Con1.Close()
            If Con2.State = ConnectionState.Open Then Con2.Close()
            MsgBox("Updated", MsgBoxStyle.Information, "AddOn")
            cmbTxnTypeCom1.ResetText()
            cmbTxnTypeCom2.ResetText()
            txtRef.ResetText()
            UG.DataSource = Nothing
            Me.Close()
        Catch ex As Exception
            Trans1.Rollback()
            Trans2.Rollback()
            If Con1.State = ConnectionState.Open Then Con1.Close()
            If Con2.State = ConnectionState.Open Then Con2.Close()
            Exit Sub
        Finally
            If Con1.State = ConnectionState.Open Then Con1.Close()
            If Con2.State = ConnectionState.Open Then Con2.Close()
        End Try


    End Sub
    Private Sub cmbCom2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCom2.TextChanged
        If cmbCom2.Text <> "" Then
            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & cmbCom2.Text.Trim & "")
            Get_Company_2_Txn_Code()
        End If
    End Sub
End Class