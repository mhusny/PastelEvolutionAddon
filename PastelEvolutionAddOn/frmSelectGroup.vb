Imports Infragistics.Win.UltraWinGrid
Imports System.Data
Imports System.Data.SqlClient
Public Class frmSelectGroup

    Private Sub frmSelectGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objSQL As New clsSqlConn
        With objSQL
            SQL = " SELECT StGroup FROM GrpTbl "

            Dim DS = New DataSet
            DS = .Get_Data_Sql(SQL)


            cmbAuto.DataSource = DS.Tables(0)
            cmbAuto.ValueMember = "StGroup"
            cmbAuto.DisplayMember = "StGroup"
            cmbAuto.DisplayLayout.Bands(0).Columns("StGroup").Width = 500
        End With
    End Sub

    Private Sub cbAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAll.CheckedChanged
        If cbAll.Checked = True Then
            cmbAuto.Enabled = False
        End If
        If cbAll.Checked = False Then
            cmbAuto.Enabled = True
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim objRTN As New frmReturnSuplier
        With objRTN

            SQL = " SELECT   * FROM         _btblInvoiceLines INNER JOIN " & _
            "  StkItem ON _btblInvoiceLines.iStockCodeID = StkItem.StockLink INNER JOIN " & _
            "   _etblLotTracking ON StkItem.StockLink = _etblLotTracking.iStockID INNER JOIN " & _
            "  _etblLotTrackingQty ON _etblLotTracking.idLotTracking = _etblLotTrackingQty.iLotTrackingID INNER JOIN " & _
            "   InvNum ON _btblInvoiceLines.iInvoiceID = InvNum.AutoIndex " & _
            " WHERE     (_etblLotTracking.iLotStatusID = 4) AND (_etblLotTrackingQty.fQtyOnHand > 0) and  InvNum.DocType = 5 "

            Dim objSQL As New clsSqlConn

            With objSQL

                Try
                    DS = New DataSet
                    DS = .Get_Data_Sql(SQL)
                    For Each Dr In DS.Tables(0).Rows
                        Dim Row As UltraGridRow = objRTN.UG.DisplayLayout.Bands(0).AddNew
                        Row.Cells("Line").Value = Dr.Item("iLineID")

                        Row.Cells("Code").Value = Dr.Item("iStockCodeID")

                        Row.Cells("LineID").Value = Dr.Item("idInvoiceLines")

                        Row.Cells("Description_1").Value = Dr.Item("cDescription")
                        Row.Cells("Description_2").Value = Dr.Item("Code")
                        Row.Cells("Warehouse").Value = Dr.Item("iWarehouseID")

                        'quantity and group

                        Row.Cells("AvailableQty").Value = Get_Available_Qty("SELECT WHQtyOnHand FROM WhseStk WHERE WHStockLink =" & Dr.Item("iStockCodeID") & " AND WHWhseID =" & Dr.Item("iWarehouseID") & "")

                        If Row.Cells("AvailableQty").Value = 0 Then
                            Row.Cells("Quantity").Value = 0
                            Row.Cells("ConfirmQty").Value = 0
                        Else
                            Row.Cells("Quantity").Value = Dr.Item("fQuantity")
                            Row.Cells("ConfirmQty").Value = Dr.Item("fQuantity")
                        End If


                        Row.Cells("ProcessedQty").Value = 0

                        'If Dr.Item("fQtyLastProcess") <> 0 Then
                        '    Row.Cells("ConfirmQty").Value = Dr.Item("fQtyLastProcess")
                        'End If
                        'Row.Cells("ConfirmQty").Value = 0


                        Row.Cells("Price_Excl").Value = Math.Round(Dr.Item("fUnitPriceExcl"), 2, MidpointRounding.AwayFromZero)
                        Row.Cells("Price_Incl").Value = Math.Round(Dr.Item("fUnitPriceIncl"), 2, MidpointRounding.AwayFromZero)
                        Row.Cells("TaxType").Value = Dr.Item("iTaxTypeID")
                        Row.Cells("Discount").Value = Math.Round(Dr.Item("fLineDiscount"), 6, MidpointRounding.AwayFromZero)
                        Row.Cells("TaxRate").Value = Dr.Item("fTaxRate")
                        Row.Cells("StockID").Value = Dr.Item("iStockCodeID")
                        Row.Cells("IsWH").Value = IIf(Dr.Item("bIsWhseItem") = True, True, False)
                        Row.Cells("LineNote").Value = Dr.Item("cLineNotes")
                        'Row.Cells("IsSerialNo").Value = IIf(Dr.Item("bIsSerialItem") = True, True, False)


                        'SQL = "SELECT    iLotID,  cLotNumber,       dLotExpiryDate FROM         _btblInvoiceLines  " & _
                        '        " WHERE  iInvoiceID = " & UG.ActiveRow.Cells("AutoIndex").Value & " AND iStockCodeID = " & Dr.Item("iStockCodeID")

                        'DS1 = New DataSet
                        'DS1 = .Get_Data_Sql(SQL)

                        'Row.Cells("Lot").Value = DS1.Tables(0).Rows(0)(0).ToString()

                        Row.Cells("Lot").Value = Dr.Item("cLotDescription")

                    Next

                    For Each ugR In objRTN.UG.Rows
                        ugR.Expanded = False
                    Next

                    objRTN.Get_Total()

                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
                    Exit Sub
                Finally
                    .Con_Close()
                    objSQL = Nothing
                    'objSalesOrder = Nothing
                End Try

            End With
            .IsNew = False
            .Show()
        End With
        objRTN = Nothing
        Me.Close()
    End Sub
    Private Function Get_Available_Qty(ByVal SQL As String) As Decimal
        Try
            Dim oQty As Object
            Con1.ConnectionString = sConStr
            CMD = New SqlCommand(SQL, Con1)
            CMD.CommandType = CommandType.Text
            Con1.Open()
            oQty = CMD.ExecuteScalar
            Con1.Close()
            Return CDec(oQty)
        Catch ex As Exception
            Return 0
        End Try
    End Function
End Class