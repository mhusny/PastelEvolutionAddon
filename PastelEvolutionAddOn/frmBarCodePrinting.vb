Imports Infragistics.Win.UltraWinGrid
Imports WolfSoftware.Library_NET
'Imports Lesnikowski.Barcode
Imports System.Drawing
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing.Imaging.ImageFormat
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmBarCodePrinting
    Private Sub Get_SerialNo()
        Dim objSQL As New clsSqlConn
        With objSQL
            'If iAgent = 9 Then

            SQL = " SELECT     StkItem.Code, StkItem.Description_1 AS Description, StkItem.cSimpleCode AS SimpleCode, WhseMst.Name AS WH,  _etblLotTracking.cLotDescription AS LotNumber, _etblLotTrackingTx.fLTTxQty AS Quantity, " & _
            "  _etblLotTrackingTx.cLTTxReference, _etblLotTrackingTx.dLTTxDate " & _
            " FROM    _etblLotTrackingTx INNER JOIN " & _
            "   _etblLotTracking ON _etblLotTrackingTx.iLotTrackingID = _etblLotTracking.idLotTracking INNER JOIN " & _
            "  StkItem ON _etblLotTracking.iStockID = StkItem.StockLink LEFT OUTER JOIN " & _
            "   WhseMst ON _etblLotTrackingTx.iLTTxWarehouseID = WhseMst.WhseLink " & _
            " GROUP BY StkItem.Code, StkItem.Description_1 , StkItem.cSimpleCode , WhseMst.Name , _etblLotTracking.cLotDescription, " & _
            " _etblLotTrackingTx.cLTTxReference, _etblLotTrackingTx.dLTTxDate, _etblLotTrackingTx.fLTTxQty " & _
            "  ORDER BY _etblLotTracking.cLotDescription "

            'SQL = " SELECT     StkItem.Code, StkItem.Description_1 AS Description, StkItem.cSimpleCode AS SimpleCode, WhseMst.Name AS WH, _etblLotTracking.cLotDescription, " & _
            '"_etblLotTrackingTx.cLTTxReference, _etblLotTrackingTx.dLTTxDate " & _
            '" FROM _etblLotTrackingTx INNER JOIN " & _
            '"   _etblLotTracking ON _etblLotTrackingTx.iLotTrackingID = _etblLotTracking.idLotTracking INNER JOIN " & _
            '"    StkItem ON _etblLotTracking.iStockID = StkItem.StockLink LEFT OUTER JOIN " & _
            '"  WhseMst ON _etblLotTrackingTx.iLTTxWarehouseID = WhseMst.WhseLink " & _
            '" ORDER BY _etblLotTracking.cLotDescription "


            'Else
            'SQL = "SELECT SerialTX.SNTxCounter, SerialTX.SNLink, SerialTX.SNTxDate AS TxnDate," & _
            '           " SerialTX.SNTxReference As Reference, SerialMF.SerialNumber, StkItem.Code, StkItem.Description_1 As Description, " & _
            '           " StkItem.cSimpleCode As SimpleCode, WhseMst.Name AS WH,  SerialMF.bPrinted as Printed FROM SerialTX INNER JOIN" & _
            '           " SerialMF ON SerialTX.SNLink = SerialMF.SerialCounter INNER JOIN" & _
            '           " StkItem ON SerialMF.SNStockLink = StkItem.StockLink LEFT OUTER JOIN" & _
            '           " WhseMst ON SerialTX.SNWarehouseID = WhseMst.WhseLink" & _
            '           " WHERE SerialMF.CurrentLoc = 1 order by SerialMF.SerialNumber"

            'End If


            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
            UG1.DataSource = DS.Tables(0)
            'UG1.DisplayLayout.Bands(0).Columns("_etblLotTrackingTx").Hidden = True
            'UG1.DisplayLayout.Bands(0).Columns("idLotTrackingTx").Hidden = True
            Dim Band As UltraGridBand = UG1.DisplayLayout.Bands(0)
            If Band.Columns.Exists("Select") = False Then
                Band.Columns.Add("Select")
                Band.Columns("Select").Header.Caption = "Select"
                Band.Columns("Select").DataType = GetType(Boolean)
                Band.Columns("Select").CellClickAction = CellClickAction.Edit
                Band.Columns("Select").CellActivation = Activation.AllowEdit
                Band.Columns("Select").DefaultCellValue = False
            End If


        End With
    End Sub

    Private Sub Get_SerialNo_PO()

        Dim objSQL As New clsSqlConn

        With objSQL

            'SQL = " SELECT     InvNum.AutoIndex, InvNum.DocType, InvNum.AccountID, InvNum.Description, InvNum.ExtOrderNum, InvNum.InvDate, InvNum.OrderNum, " & _
            '        " CASE DocState WHEN 1 THEN 'Unprocessed' WHEN 2 THEN 'Quote' WHEN 3 THEN 'Partial Processed' WHEN 4 THEN 'Archived' END AS Status,  " & _
            '        " InvNum.DocState, StkItem.Code, StkItem.Description_1, sbSerialMF.SerialNumber, sbSerialMF.bPrinted as Printed " & _
            '        " FROM         sbSerialMF LEFT OUTER JOIN " & _
            '        " StkItem ON sbSerialMF.SNStockLink = StkItem.StockLink RIGHT OUTER JOIN " & _
            '        " _btblInvoiceLines ON sbSerialMF.AutoIndex = _btblInvoiceLines.iInvoiceID " & _
            '        " AND sbSerialMF.SNStockLink = _btblInvoiceLines.iStockCodeID RIGHT OUTER JOIN " & _
            '        " InvNum ON _btblInvoiceLines.iInvoiceID = InvNum.AutoIndex " & _
            '        " WHERE     (InvNum.DocFlag <> 2) AND (InvNum.DocType = 5) AND " & _
            '        " (InvNum.DocState = 1 OR InvNum.DocState = 2 OR InvNum.DocState = 4 OR InvNum.DocState = 3)  AND (StkItem.SerialItem = 1)  order by sbSerialMF.SerialNumber   "

            SQL = " SELECT     InvNum.AutoIndex, InvNum.DocType, InvNum.AccountID, InvNum.Description, InvNum.ExtOrderNum, InvNum.InvDate, InvNum.OrderNum, " & _
            " CASE DocState WHEN 1 THEN 'Unprocessed' WHEN 2 THEN 'Quote' WHEN 3 THEN 'Partial Processed' WHEN 4 THEN 'Archived' END AS Status, " & _
            " InvNum.DocState, StkItem.Code, StkItem.Description_1, _btblInvoiceLines.iStockCodeID, _btblInvoiceLines.cLotNumber as LotNumber, " & _
            " _btblInvoiceLines.dLotExpiryDate,  _btblInvoiceLines.fQuantity as Quantity " & _
            " FROM  StkItem INNER JOIN " & _
            " _btblInvoiceLines ON StkItem.StockLink = _btblInvoiceLines.iStockCodeID RIGHT OUTER JOIN " & _
            " InvNum ON _btblInvoiceLines.iInvoiceID = InvNum.AutoIndex " & _
            " WHERE(InvNum.DocFlag <> 2) And (InvNum.DocType = 5) And (InvNum.DocState = 1 Or " & _
            " InvNum.DocState = 2 Or " & _
            " InvNum.DocState = 4 Or " & _
            " InvNum.DocState = 3) And (StkItem.bLotItem = 1)  "

            'SQL = "SELECT SerialTX.SNTxCounter, SerialTX.SNLink, SerialTX.SNTxDate AS TxnDate," & _
            '" SerialTX.SNTxReference As Reference, SerialMF.SerialNumber, StkItem.Code, StkItem.Description_1 As Description, " & _
            '" StkItem.cSimpleCode As SimpleCode, WhseMst.Name AS WH FROM SerialTX INNER JOIN" & _
            '" SerialMF ON SerialTX.SNLink = SerialMF.SerialCounter INNER JOIN" & _
            '" StkItem ON SerialMF.SNStockLink = StkItem.StockLink LEFT OUTER JOIN" & _
            '" WhseMst ON SerialTX.SNWarehouseID = WhseMst.WhseLink" & _
            '" WHERE SerialMF.CurrentLoc = 1 order by SerialMF.SerialNumber"

            UG1.DataSource = Nothing
            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
            UG1.DataSource = DS.Tables(0)
            'UG1.DisplayLayout.Bands(0).Columns("SNLink").Hidden = True
            'UG1.DisplayLayout.Bands(0).Columns("SNTxCounter").Hidden = True
            Dim Band As UltraGridBand = UG1.DisplayLayout.Bands(0)
            If Band.Columns.Exists("Select") = False Then
                Band.Columns.Add("Select")
                Band.Columns("Select").Header.Caption = "Select"
                Band.Columns("Select").DataType = GetType(Boolean)
                Band.Columns("Select").CellClickAction = CellClickAction.Edit
                Band.Columns("Select").CellActivation = Activation.AllowEdit
                Band.Columns("Select").DefaultCellValue = False
            End If

        End With
    End Sub


    Dim DS_BAR As DataSet
    Dim DA_BAR As SqlDataAdapter
    Dim CMD_BAR As SqlCommand
    Dim CON_BAR As New SqlConnection(sConStr)
    Private Sub Get_WH()
        SQL = "SELECT DISTINCT WhseMst.WhseLink, WhseMst.Code, WhseMst.Name " & _
              " FROM         _btblInvoiceLines INNER JOIN " & _
              " WhseMst ON _btblInvoiceLines.iWarehouseID = WhseMst.WhseLink "

        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                'If DS.Tables(0).Rows.Count > 0 Then
                cmbWarehouse.DataSource = DS.Tables(0)
                cmbWarehouse.ValueMember = "WhseLink"
                cmbWarehouse.DisplayMember = "Name"
                'End If
                cmbWarehouse.DisplayLayout.Bands(0).Columns("WhseLink").Hidden = True
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                Exit Sub
            End Try
        End With

    End Sub
    Private Sub Get_Group()
        SQL = "SELECT StGroup from dbo.GrpTbl "

        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                'If DS.Tables(0).Rows.Count > 0 Then
                cmbGroup.DataSource = DS.Tables(0)
                cmbGroup.ValueMember = "StGroup"
                cmbGroup.DisplayMember = "StGroup"
                'End If
                'cmbGroup.DisplayLayout.Bands(0).Columns("WhseLink").Hidden = True
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                Exit Sub
            End Try
        End With

    End Sub
    Private Sub Get_PO()
        SQL = " SELECT AutoIndex,OrderNum FROM InvNum WHERE DocType = 5  "
        'SQL = " SELECT AutoIndex,OrderNum FROM InvNum WHERE DocType = 5 and DocState = 1 "
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                'If DS.Tables(0).Rows.Count > 0 Then
                cmbPO.DataSource = DS.Tables(0)
                cmbPO.ValueMember = "AutoIndex"
                cmbPO.DisplayMember = "OrderNum"
                'End If
                'cmbGroup.DisplayLayout.Bands(0).Columns("WhseLink").Hidden = True
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                Exit Sub
            End Try
        End With

    End Sub
    Private Sub Get_Bar()
        SQL = "SELECT COL1, COL2, COL3, COL4, COL5, COL6, COL7, COL8  FROM sbBarCode"
        CMD_BAR = New SqlCommand(SQL, CON_BAR)
        CMD_BAR.CommandType = CommandType.Text
        DS_BAR = New DataSet
        DA_BAR = New SqlDataAdapter(CMD_BAR)
        CON_BAR.Open()
        DA_BAR.Fill(DS_BAR)
        CON_BAR.Close()
        UG2.DataSource = DS_BAR.Tables(0)
    End Sub
    Public Sub DeleteData()
        SQL = "DELETE FROM sbBarCode"
        Dim objSQL As New clsSqlConn
        With objSQL
            If .Execute_Sql(SQL) = 0 Then
                MsgBox("Error While Deleting Data", MsgBoxStyle.Exclamation, "Pastel Evolution")
            End If
        End With
    End Sub
    Private Sub frmBarCodePrinting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Get_WH()
        Get_Group()
        Get_PO()

        rbPO.Checked = True
        cmbGroup.Text = ""
        cmbPO.Text = ""
    End Sub
    Private Sub UltraButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraButton2.Click
        DeleteRow()
        Get_SerialNo()
        DeleteData()
        Get_Bar()
    End Sub
    Private Sub DeleteRow()
        Dim ugR As UltraGridRow
        For Each ugR In UG1.Rows.All
            ugR.Delete(False)
        Next
    End Sub
    Private Sub UltraButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraButton1.Click
        DeleteRow()
        Get_SerialNo_PO()
        DeleteData()
        Get_Bar()
    End Sub
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try


            If PageSize.Value < Column.Value Then
                MsgBox("Wrong Page Size", MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            End If

            Dim ugR As UltraGridRow
            For Each ugR In UG2.Rows.All
                ugR.Delete(False)
            Next

            Dim ugR1 As UltraGridRow

            Dim i As Integer = 0
            While i < Row.Value
                ugR1 = UG2.DisplayLayout.Bands(0).AddNew
                i = i + 1
            End While

            Dim iC As Integer = Column.Value
            Dim iR As Integer = Row.Value

            'UG1.DisplayLayout.Bands(0).Columns("SerialNumber").SortIndicator = SortIndicator.Ascending
            'Dim SRow As Integer
            'SRow = 1
            For Each ugR In UG1.Rows
                NoofRows.Value = 0
                If ugR.Cells("Select").Value = True Then
                    If NoofRows.Value = 0 Then
                        NoofRows.Value = ugR.Cells("Quantity").Value
                    End If
                    For x As Integer = 1 To NoofRows.Value
                        'Dim b As BaseBarcode
                        'b = BarcodeFactory.GetBarcode(Symbology.Code128)

                        'b.NarrowBarWidth = 1
                        'b.Height = 60
                        'b.FontHeight = 0.15F
                        'b.IsNumberVisible = True

                        'b.Number = ugR.Cells("SerialNumber").Value

                        'Dim binBcode() As Byte

                        'Dim ms As New MemoryStream
                        'b.Save(ms, ImageType.Bmp, 96, 96)
                        'binBcode = ms.GetBuffer()
                        'ms.Close()

                        Dim BarCodeContoral As New BarcodeControl
                        BarCodeContoral.Unlock("Phantom 2008", "WSFCX-0100-100883561")
                        BarCodeContoral.DataToEncode = ugR.Cells("LotNumber").Value
                        ''Save printed barcode-----------------------------------------------------------------------------
                        'Dim objSQL As New clsSqlConn
                        'With objSQL
                        '    .Begin_Trans()
                        '    SQL = " Update SerialMF SET bPrinted = 1 where SerialNumber = '" & ugR.Cells("SerialNumber").Value & "' "
                        '    SQL += " Update sbSerialMF SET bPrinted = 1 where SerialNumber = '" & ugR.Cells("SerialNumber").Value & "' "
                        '    If .Execute_Sql_Trans(SQL) = 0 Then
                        '        .Rollback_Trans()
                        '        Exit Sub
                        '    End If
                        '    .Commit_Trans()
                        'End With
                        ''-----------------------------------------------------------------------------------------------
                        BarCodeContoral.HumanReadableSize = 11
                        ''BarCodeContoral.Font.Bold = F

                        BarCodeContoral.MarginX = 0.5
                        BarCodeContoral.MarginY = 0.5
                        BarCodeContoral.Reduction = 10
                        BarCodeContoral.ModuleHeight = System.Convert.ToSingle(15)
                        BarCodeContoral.ModuleWidth = System.Convert.ToSingle(0.2)
                        BarCodeContoral.Ratio = System.Convert.ToSingle(3)
                        BarCodeContoral.CurrentCode = 1006

                        'BarCodeContoral.Font = Font. 
                        'BarCodeContoral.Font.
                        Dim bByte() As Byte
                        Dim ms As New MemoryStream
                        Dim Code As Bitmap
                        Code = BarCodeContoral.GetCode(500.0F)
                        Code.Save(ms, Imaging.ImageFormat.Jpeg)
                        bByte = ms.GetBuffer()
                        ms.Close()
                        UG2.Rows(iR - 1).Cells(iC - 1).Value = bByte


                        If iC >= PageSize.Value Then
                            iC = 1
                            iR = iR + 1
                            Dim ugR2 As UltraGridRow
                            ugR2 = UG2.DisplayLayout.Bands(0).AddNew
                        Else
                            iC = iC + 1
                        End If
                    Next

                End If

            Next

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        Finally
            GC.Collect()

        End Try
        MsgBox("Successfully Updated", MsgBoxStyle.Information, "Pasterl Evolution")
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            UG2.PerformAction(UltraGridAction.CommitRow)
            UG2.PerformAction(UltraGridAction.ExitEditMode)
            Dim CB_BAR As New SqlCommandBuilder(DA_BAR)
            DA_BAR.Update(DS_BAR)
            Dim objRep As New ReportDocument

            If PageSize.Value = 6 Then
                objRep.Load(Application.StartupPath & "\BarCode_6.rpt")
            Else
                objRep.Load(Application.StartupPath & "\BarCode_5.rpt")
            End If


            objRep.SetDatabaseLogon(sSQLSrvUserName, sSQLSrvPassword, sSQLSrvReportSrv, sSQLSrvDataBase)
            Dim objCRV As New frmPrintPreview
            With objCRV
                .CRV.ReportSource = objRep
                '.RV.ReportSource = objRep
                .Text = "BarCode Printing"
                .ShowDialog()
            End With

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel evolution")
            Exit Sub
        End Try
    End Sub

    Private Sub cbSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSelectAll.CheckedChanged

        Dim ugR, ugR1 As UltraGridRow
        Dim icount, a, line As Integer
        line = 0
        icount = NoofRows.Value
        If icount > 0 Then
            If cbSelectAll.Checked = True Then
                line = UG1.ActiveRow.Index
                For Each ugR In UG1.Rows
                    If ugR.Cells("Select").Value = True Then
                        icount = icount - 1
                        a = ugR.VisibleIndex

                        For Each ugR1 In UG1.Rows
                            If ugR1.VisibleIndex > line + 1 And icount > 0 Then
                                ugR1.Cells("Select").Value = True
                                icount = icount - 1
                            End If
                        Next
                        Exit For
                    End If

                Next
            ElseIf cbSelectAll.Checked = False Then
                For Each ugR In UG1.Rows
                    'ugR.Cells("Select").Value = False
                Next
            End If

        Else
            If cbSelectAll.Checked = True Then
                For Each ugR In UG1.Rows
                    If ugR.VisibleIndex > 0 Then
                        ugR.Cells("Select").Value = True
                    End If
                Next
            ElseIf cbSelectAll.Checked = False Then
                For Each ugR In UG1.Rows
                    'ugR.Cells("Select").Value = False
                Next
            End If
        End If
        line = 0
    End Sub

    Private Sub frmBarCodePrinting_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub NoofRows_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NoofRows.ValueChanged
        cbSelectAll.Checked = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If rbPO.Checked = True Then
            SQL = " SELECT     CAST(0 AS BIT) as [SELECT], StkItem.Description_1, _btblInvoiceLines.cLotNumber as [cLotDescription], _btblBINLocation.cBinLocationName, _btblInvoiceLines.fQuantity as [fQtyOnHand], _etblInvSegValue.cDescription " & _
            " FROM         _btblInvoiceLines INNER JOIN " & _
            "                      StkItem ON _btblInvoiceLines.iStockCodeID = StkItem.StockLink INNER JOIN " & _
            "                      _etblInvSegValue ON StkItem.iInvSegValue2ID = _etblInvSegValue.idInvSegValue INNER JOIN " & _
            "                      _btblBINLocation ON StkItem.iBinLocationID = _btblBINLocation.idBinLocation " & _
            "            WHERE(_btblInvoiceLines.iInvoiceID = '" & cmbPO.Value & "') "

        ElseIf rbGroup.Checked = True Then
            SQL = "SELECT   CAST(0 AS BIT) as [SELECT],  StkItem.Description_1, _etblLotTracking.cLotDescription, _btblBINLocation.cBinLocationName , _etblLotTrackingQty.fQtyOnHand , _etblInvSegValue.cDescription " & _
            " FROM         _etblLotTracking INNER JOIN " & _
            "                      _etblLotTrackingQty ON _etblLotTracking.idLotTracking = _etblLotTrackingQty.iLotTrackingID INNER JOIN " & _
            "                      StkItem ON _etblLotTracking.iStockID = StkItem.StockLink INNER JOIN " & _
            "                      WhseMst ON _etblLotTrackingQty.iWarehouseID = WhseMst.WhseLink INNER JOIN " & _
            "                      _btblBINLocation ON StkItem.iBinLocationID = _btblBINLocation.idBinLocation INNER JOIN " & _
            "     _etblInvSegValue ON StkItem.iInvSegValue2ID = _etblInvSegValue.idInvSegValue " & _
            " WHERE _etblLotTrackingQty.fQtyOnHand > 0 AND  StkItem.ItemGroup =  '" & cmbGroup.Text & "' AND WhseMst.WhseLink = " & cmbWarehouse.Value & " " & _
            " ORDER BY StkItem.Description_1 "
        End If

        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                UG.DataSource = DS.Tables(0)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                Exit Sub
            End Try

            'UG.DisplayLayout.Bands(0).Columns("Select").DataType = GetType(Boolean)
            UG.DisplayLayout.Bands(0).Columns("Select").CellClickAction = CellClickAction.Edit
            UG.DisplayLayout.Bands(0).Columns("Select").CellActivation = Activation.AllowEdit
            UG.DisplayLayout.Bands(0).Columns("fQtyOnHand").CellClickAction = CellClickAction.Edit
            UG.DisplayLayout.Bands(0).Columns("fQtyOnHand").CellActivation = Activation.AllowEdit
            'UG.DisplayLayout.Bands(0).Columns("Select").DefaultCellValue = False
        End With
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If UG.Selected.Rows.Count > 0 Then
            Dim ugR As UltraGridRow
            For Each ugR In UG.Selected.Rows
                ugR.Delete(False)
            Next

            'For Each ugR In UG.Rows
            '    ugR.Expanded = False
            '    If ugR.ChildBands.HasChildRows = True Then
            '        ugR.CellAppearance.BackColor = Color.Yellow
            '    Else
            '        ugR.CellAppearance.BackColor = Color.White
            '    End If
            'Next

        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        Dim ItemCode As String
        Dim Lot As String
        Dim bin As String
        Dim group As String

        Dim Qty As Double
        Dim serial As Integer
        Dim price As Double
        Dim ugr As UltraGridRow
        Dim Desc1 As String
        Dim Desc2 As String
        Dim SN As String

        Dim file As System.IO.StreamWriter

        'file = My.Computer.FileSystem.OpenTextFileWriter("item.txt", True)



        'clear text file
        Dim fs = New FileStream("D:\item.txt", FileMode.Truncate)
        fs.Close()



        file = My.Computer.FileSystem.OpenTextFileWriter("D:\item.txt", True)

        For Each ugr In UG.Rows
            If ugr.Cells("SELECT").Value = True Then
                ItemCode = ugr.Cells("Description_1").Text
                Lot = ugr.Cells("cLotDescription").Text
                bin = ugr.Cells("cBinLocationName").Text
                'group = ugr.Cells("ItemGroup").Text
                Qty = ugr.Cells("fQtyOnHand").Value
                
                For serial = txtStartSerial.Text To Qty

                    'create inventory

                    'Desc1 = Mid(ItemCode, 1, ItemCode.IndexOf("/"))
                    'Desc2 = Mid(ItemCode, ItemCode.IndexOf("/") + 1)

                    'SN = Desc1 + "-" + Lot.ToString() + "-" + i.ToString()
                    'Desc1 = Desc1.ToString()



                    file.WriteLine(ItemCode + "-" + serial.ToString() + "," + Lot + "," + bin)


                Next

            End If
        Next
        file.Close()
        txtStartSerial.Text = 1



        Dim p = New Process()
        p.StartInfo = New ProcessStartInfo("print.bat")
        p.Start()
        p.WaitForExit()
    End Sub
End Class