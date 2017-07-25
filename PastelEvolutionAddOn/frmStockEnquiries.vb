Imports System.Data
Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid
Imports Pastel.Evolution


Public Class frmStockEnquiries

    Dim aDataBase As ArrayList
    Public iDocType As Integer

    Private Sub GetDatabaseName()
        aDataBase = New ArrayList()
        aDataBase.Add("dbUdawatta_new")
        'aDataBase.Add("dbKiribathgoda_new")
        'aDataBase.Add("dbKurunegala_new")
        aDataBase.Add("dbNawinna_new")
        aDataBase.Add("dbKelaniya_new")
        aDataBase.Add("dbMathara")
    End Sub

    'Public Sub Get_Data()
    '    SQL = "SELECT     StockLink, Code, Description_1, ItemGroup, Pack, Bar_Code, AveUCst, LatUCst, LowUCst, HigUCst," & _
    '    " StdUCst, Qty_On_Hand, ServiceItem, ItemActive, ReservedQty, QtyOnPO, QtyOnSO, WhseItem, SerialItem, DuplicateSN," & _
    '    " StrictSN, JobQty, iBinLocationID, dStkitemTimeStamp,cExtDescription, cSimpleCode As SimpleCode, bCommissionItem, MFPQty," & _
    '    " bLotItem, iLotStatus, bLotMustExpire,  ItemCost, CostingMethodDescription, CostingMethod," & _
    '    " BarCode, ItemGroupDescription, PackSize, PackDescription, BinLocationName, Bin, QtyInStock, " & _
    '    " cInvSegValue1Value AS [Part Number], cInvSegValue2Value AS Description, cInvSegValue3Value AS Model," & _
    '    " cInvSegValue4Value AS Brand,cInvSegValue5Value AS Genuine, cInvSegValue6Value AS [Group], cInvSegValue7Value AS Type," & _
    '    " LotStatusDescription, UOMStockingUnitCode,UOMDefPurchaseUnitCode, UOMDefSellingUnitCode, DefaultPriceListName," & _
    '    " DefaultPriceExcl, DefaultPriceIncl, ExPr1, InPr1, PriceListName1, ExPr2, InPr2, PriceListName2, ExPr3, InPr3," & _
    '    " PriceListName3 FROM _bvStockFull"
    '    Dim objSQL As New clsSqlConn
    '    With objSQL
    '        DS = New DataSet
    '        DS = .Get_Data_Sql(SQL)
    '        UG.DataSource = DS.Tables(0)

    '        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sAgent & "ItemExquiries.xml") Then
    '            UG.DisplayLayout.LoadFromXml(Application.StartupPath & "\" & sAgent & "ItemExquiries.xml")
    '        End If
    '    End With
    'End Sub
    Private Sub frmStockEnquiries_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Get_Data()
        Get_Stock_Item()

        'cutom rights
        'Dim agent As New Agent(iAgent)

        'If sSQLSrvDataBase = "dbNawinna_new" And agent.Description = 4 Then
        '    tsbUpdate.Enabled = True
        'Else
        '    tsbUpdate.Enabled = False
        'End If


        'Dim Dr As DataRow
        'For Each Dr In dsManu.Tables(0).Rows
        '    If 22251 = Dr("iManu") Then
        tsbUpdate.Enabled = True
        ''UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
        ''UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default
        '    End If
        'Next



    End Sub
    Private Sub Get_Stock_Item()
        SQL = "SELECT     StockLink,cSimpleCode As SimpleCode,('('+ cSimpleCode +') ' + Code) As ToShow,Code   FROM _bvStockFull"
        Dim objSQL As New clsSqlConn
        With objSQL
            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
            cmbInventoryItem.DataSource = DS.Tables(0)
            cmbInventoryItem.ValueMember = "SimpleCode"
            cmbInventoryItem.DisplayMember = "SimpleCode"
            cmbInventoryItem.DisplayLayout.Bands(0).Columns("StockLink").Hidden = True
            cmbInventoryItem.DisplayLayout.Bands(0).Columns("Code").Hidden = True
        End With
    End Sub

    Private Sub tsbExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbExport.Click
        Dim myFineName As String


        Try

            Dim fdlg As SaveFileDialog = New SaveFileDialog
            fdlg.Title = "Excel Data Importer"
            fdlg.InitialDirectory = "C:\"
            fdlg.Filter = "MS Excel (*.xls)|*.xls"

            fdlg.RestoreDirectory = True

            myFineName = ""

            If fdlg.ShowDialog() = DialogResult.OK Then
                myFineName = fdlg.FileName
            End If


            If myFineName = "" Then
                MsgBox("Please enter a file name", MsgBoxStyle.Information, "Validation")
                Exit Sub
            End If

            ugExcelExporter.Export(UG, myFineName)


            Dim Proc As New System.Diagnostics.Process
            Proc.StartInfo.FileName = myFineName
            Proc.Start()

        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub tsbSaveMySetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSaveMySetting.Click
        Try
            UG.DisplayLayout.SaveAsXml(Application.StartupPath & "\" & sAgent & "ItemExquiries.xml")
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub tsbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
    End Sub

    Private Sub UG_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs)

    End Sub

    Private Sub tsbConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbConnect.Click
        Try
            'Dim sDB As String
            'Dim Obj As Object
            'Dim ugR1 As UltraGridRow
            'Dim ugR2 As UltraGridRow
            'Dim Dr As DataRow
            If cmbInventoryItem.Value.ToString.Trim.Length = 0 Then
                MsgBox("Select Item Code", MsgBoxStyle.Exclamation, "Pastel Evoltuion")
                Exit Sub
            End If
            For Each ugR In UG.Rows.All
                ugR.Delete(False)
            Next

            Dim DS1 As New DataSet

            'For Each Obj In aDataBase
            '    sDB = Obj.ToString
            '    'sDB = "dbUdawatta"
            'Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & sDB & "")

            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= dbNawinna_new  ")
            SQL = " SELECT  'dbNawinna_new' AS [Database], 'Nawinna' AS Branch, '3' AS WHID, _bvStockFull.StockLink, _bvStockFull.Code, WhseStk.WHQtyOnHand as Qty_On_Hand, WhseStk.WHQtyOnPO as QtyOnPO, WhseStk.WHMax_Lvl as Max_Lvl, WhseStk.WHRe_Ord_Qty as Re_Ord_Qty, WhseStk.WHRe_Ord_Lvl as Re_Ord_Lvl FROM _bvStockFull INNER JOIN  WhseStk ON _bvStockFull.StockLink = WhseStk.WHStockLink WHERE cSimpleCode = '" & cmbInventoryItem.Value & "' AND WhseStk.WHWhseID = 3 And ItemActive = 1"
            CMD = New SqlCommand(SQL, Con2)
            CMD.CommandType = CommandType.Text
            DA = New SqlDataAdapter(CMD)
            DS1 = New DataSet
            Con2.Open()
            DA.Fill(DS1)
            Con2.Close()


            'DS1.Tables(0).Merge(DS.Tables(0), False, MissingSchemaAction.Add)
            'DS1.Merge(DS.Tables(0), False, MissingSchemaAction.Add)

            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= dbNawinna_new")
            SQL = " SELECT  'dbNawinna_new' AS [Database], 'Kiribathgoda' AS Branch, '18' AS WHID, _bvStockFull.StockLink, _bvStockFull.Code, WhseStk.WHQtyOnHand as Qty_On_Hand, WhseStk.WHQtyOnPO as QtyOnPO, WhseStk.WHMax_Lvl as Max_Lvl, WhseStk.WHRe_Ord_Qty as Re_Ord_Qty, WhseStk.WHRe_Ord_Lvl as Re_Ord_Lvl FROM _bvStockFull INNER JOIN  WhseStk ON _bvStockFull.StockLink = WhseStk.WHStockLink  WHERE cSimpleCode = '" & cmbInventoryItem.Value & "' And WhseStk.WHWhseID = 18 And ItemActive = 1 "
            CMD = New SqlCommand(SQL, Con2)
            CMD.CommandType = CommandType.Text
            DA = New SqlDataAdapter(CMD)
            DS = New DataSet
            Con2.Open()
            DA.Fill(DS)
            Con2.Close()

            DS1.Tables(0).Merge(DS.Tables(0), False, MissingSchemaAction.Add)


            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= dbNawinna_new ")
            SQL = " SELECT  'dbNawinna_new' AS [Database], 'Negombo' AS Branch, '4' AS WHID, _bvStockFull.StockLink, _bvStockFull.Code, WhseStk.WHQtyOnHand as Qty_On_Hand, WhseStk.WHQtyOnPO as QtyOnPO, WhseStk.WHMax_Lvl as Max_Lvl, WhseStk.WHRe_Ord_Qty as Re_Ord_Qty, WhseStk.WHRe_Ord_Lvl as Re_Ord_Lvl FROM _bvStockFull INNER JOIN  WhseStk ON _bvStockFull.StockLink = WhseStk.WHStockLink WHERE cSimpleCode = '" & cmbInventoryItem.Value & "' And ItemActive = 1 AND WhseStk.WHWhseID = 4 "


            CMD = New SqlCommand(SQL, Con2)
            CMD.CommandType = CommandType.Text
            DA = New SqlDataAdapter(CMD)
            DS = New DataSet
            Con2.Open()
            DA.Fill(DS)
            Con2.Close()


            DS1.Tables(0).Merge(DS.Tables(0), False, MissingSchemaAction.Add)



            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database = dbUdawatta_new ")
            SQL = "  SELECT  'dbUdawatta_new' AS [Database], 'Kurunegala' AS Branch, '4' AS WHID, _bvStockFull.StockLink, _bvStockFull.Code, WhseStk.WHQtyOnHand as Qty_On_Hand, WhseStk.WHQtyOnPO as QtyOnPO, WhseStk.WHMax_Lvl as Max_Lvl, WhseStk.WHRe_Ord_Qty as Re_Ord_Qty, WhseStk.WHRe_Ord_Lvl as Re_Ord_Lvl FROM _bvStockFull INNER JOIN  WhseStk ON _bvStockFull.StockLink = WhseStk.WHStockLink WHERE cSimpleCode = '" & cmbInventoryItem.Value & "' And ItemActive = 1 AND WhseStk.WHWhseID = 4 "
            CMD = New SqlCommand(SQL, Con2)
            CMD.CommandType = CommandType.Text
            DA = New SqlDataAdapter(CMD)
            DS = New DataSet
            Con2.Open()
            DA.Fill(DS)
            Con2.Close()

            DS1.Merge(DS.Tables(0), False, MissingSchemaAction.Add)

            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= dbUdawatta_new ")
            SQL = "  SELECT  'dbUdawatta_new' AS [Database], 'Panchikawatta' AS Branch, '2' AS WHID, _bvStockFull.StockLink, _bvStockFull.Code, WhseStk.WHQtyOnHand as Qty_On_Hand, WhseStk.WHQtyOnPO as QtyOnPO, WhseStk.WHMax_Lvl as Max_Lvl, WhseStk.WHRe_Ord_Qty as Re_Ord_Qty, WhseStk.WHRe_Ord_Lvl as Re_Ord_Lvl FROM _bvStockFull INNER JOIN  WhseStk ON _bvStockFull.StockLink = WhseStk.WHStockLink WHERE cSimpleCode = '" & cmbInventoryItem.Value & "' And ItemActive = 1 AND WhseStk.WHWhseID = 2 "


            CMD = New SqlCommand(SQL, Con2)
            CMD.CommandType = CommandType.Text
            DA = New SqlDataAdapter(CMD)
            DS = New DataSet
            Con2.Open()
            DA.Fill(DS)
            Con2.Close()


            DS1.Tables(0).Merge(DS.Tables(0), False, MissingSchemaAction.Add)


            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= dbKelaniya_new ")
            SQL = "  SELECT  'dbKelaniya_new' AS [Database], 'Kelaniya' AS Branch, '16' AS WHID, _bvStockFull.StockLink, _bvStockFull.Code, WhseStk.WHQtyOnHand - _bvStockFull.JobQty as Qty_On_Hand, WhseStk.WHQtyOnPO as QtyOnPO, WhseStk.WHMax_Lvl as Max_Lvl, WhseStk.WHRe_Ord_Qty as Re_Ord_Qty, WhseStk.WHRe_Ord_Lvl as Re_Ord_Lvl FROM _bvStockFull INNER JOIN  WhseStk ON _bvStockFull.StockLink = WhseStk.WHStockLink WHERE cSimpleCode = '" & cmbInventoryItem.Value & "' And ItemActive = 1 AND WhseStk.WHWhseID = 16 "


            CMD = New SqlCommand(SQL, Con2)
            CMD.CommandType = CommandType.Text
            DA = New SqlDataAdapter(CMD)
            DS = New DataSet
            Con2.Open()
            DA.Fill(DS)
            Con2.Close()


            DS1.Tables(0).Merge(DS.Tables(0), False, MissingSchemaAction.Add)




            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= dbUdawatta_new ")
            SQL = "  SELECT  'dbUdawatta_new' AS [Database], 'Matara' AS Branch, '18' AS WHID, _bvStockFull.StockLink, _bvStockFull.Code, WhseStk.WHQtyOnHand as Qty_On_Hand, WhseStk.WHQtyOnPO as QtyOnPO, WhseStk.WHMax_Lvl as Max_Lvl, WhseStk.WHRe_Ord_Qty as Re_Ord_Qty, WhseStk.WHRe_Ord_Lvl as Re_Ord_Lvl FROM _bvStockFull INNER JOIN  WhseStk ON _bvStockFull.StockLink = WhseStk.WHStockLink WHERE cSimpleCode = '" & cmbInventoryItem.Value & "' And ItemActive = 1 AND WhseStk.WHWhseID = 18 "


            CMD = New SqlCommand(SQL, Con2)
            CMD.CommandType = CommandType.Text
            DA = New SqlDataAdapter(CMD)
            DS = New DataSet
            Con2.Open()
            DA.Fill(DS)
            Con2.Close()


            DS1.Tables(0).Merge(DS.Tables(0), False, MissingSchemaAction.Add)


            


            UG.DataSource = DS1.Tables(0)
            UG.DisplayLayout.Bands(0).Columns("StockLink").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("Database").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("WHID").Hidden = True



            'If DS.Tables(0).Rows.Count > 0 Then
            '    ugR1 = UG.DisplayLayout.Bands(0).AddNew
            '    ugR1.Cells("ID").Value = ugR1.Index + 1
            '    ugR1.Cells("DatabaseName").Value = sDB
            '    ugR1.Activated = True
            '    For Each Dr In DS.Tables(0).Rows
            '        If ugR1.ChildBands.HasChildRows = False Then
            '            ugR2 = UG.ActiveRow.ChildBands(0).Band.AddNew
            '            ugR2.Cells("ID").Value = ugR2.Index + 1
            '            ugR2.Cells("Code").Value = Dr("Code")
            '            ugR2.Cells("Qty").Value = Dr("Qty_On_Hand")
            '            ugR2.Cells("Slink").Value = Dr("StockLink")
            '        Else
            '            ugR2 = ugR1.ChildBands(0).Band.AddNew
            '            ugR2.Cells("ID").Value = ugR2.Index + 1
            '            ugR2.Cells("ID").Value = ugR2.Index + 1
            '            ugR2.Cells("Code").Value = Dr("Code")
            '            ugR2.Cells("Qty").Value = Dr("Qty_On_Hand")
            '            ugR2.Cells("Slink").Value = Dr("StockLink")
            '        End If
            '    Next
            'Else
            'MsgBox(cmbInventoryItem.Value & " can not find from database " & sDB & vbLf & "Please check item code from that database before changes update", MsgBoxStyle.Exclamation, "Pastel Evolution")
            'Exit Sub
            'End If
            'Next
            'MsgBox("Item Load Completed", MsgBoxStyle.Information, "Pastel Evolution")
        Catch ex As Exception
            MsgBox("Error found while loading item from databases", MsgBoxStyle.Exclamation, "Pastel Evolution")
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
        Finally
            DS.Dispose()
            DA.Dispose()
            CMD.Dispose()
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        frmStockCount.Show()
    End Sub

    Private Sub cmbInventoryItem_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles cmbInventoryItem.InitializeLayout

    End Sub

    Private Sub tsbUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbUpdate.Click
        UG.PerformAction(UltraGridAction.CommitRow)
        If txtItem.ToString.Trim.Length = 0 Then
            MsgBox("Select Item Code", MsgBoxStyle.Exclamation, "Pastel Evoltuion")
            Exit Sub
        End If

        Dim ugR1 As UltraGridRow
        Dim ugR2 As UltraGridRow
        Dim sDB As String
        Dim agent As New Agent(iAgent)
        'For Each Obj In aDataBase
        '    sDB = Obj.ToString

        For Each ugR1 In UG.Rows
            If ugR1.ChildBands.HasChildRows = True Then
                For Each ugR2 In ugR1.ChildBands(0).Rows
                    '        For Each ugR3 In UG.Rows
                    '            If ugR3.ChildBands.HasChildRows = True Then
                    '                For Each ugR4 In ugR3.ChildBands(0).Rows
                    '                    If ugR2.Cells("ID").Value = ugR4.Cells("ID").Value Then

                    sDB = DirectCast(ugR2.Cells.All(0), Infragistics.Win.UltraWinGrid.UltraGridCell).Text



                    'If ugR1.Cells("Database").Value = "dbNawinna_new" Then
                    '    Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= dbNawinna_new")
                    'Else
                    Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & ugR2.Cells("Database").Value & "")
                    'End If



                    SQL = " SELECT StockLink FROM dbo.StkItem WHERE Bar_Code  =  (SELECT Bar_Code FROM dbo.StkItem WHERE StockLink = " & ugR2.Cells("StockLink").Value & ")"
                    CMD = New SqlCommand(SQL, Con2)
                    CMD.CommandType = CommandType.Text
                    DA = New SqlDataAdapter(CMD)
                    DS = New DataSet
                    Con2.Open()
                    DA.Fill(DS)
                    Con2.Close()
                    If DS.Tables(0).Rows.Count > 0 Then
                        For Each gdr In DS.Tables(0).Rows
                            SQL = "UPDATE WhseStk SET WHMax_Lvl = " & ugR2.Cells("Max_Lvl").Value & ", WHRe_Ord_Qty = " & IIf(IsDBNull(ugR2.Cells("Re_Ord_Qty").Value), 0, ugR2.Cells("Re_Ord_Qty").Value) & ", WHRe_Ord_Lvl = " & IIf(IsDBNull(ugR2.Cells("Re_Ord_Lvl").Value), 0, ugR2.Cells("Re_Ord_Lvl").Value) & "   WHERE WHStockLink = '" & gdr("StockLink") & "' AND WHWhseID = '" & ugR2.Cells("WHID").Value & "' "

                            CMD = New SqlCommand(SQL, Con2)
                            CMD.CommandType = CommandType.Text
                            Con2.Open()
                            CMD.ExecuteNonQuery()
                            Con2.Close()
                        Next
                    End If

                    'If (sSQLSrvDataBase = "dbNawinna_new" And agent.Description = 4 And ugR1.Cells("Branch").Value = "Negombo") Then
                    '    SQL = "UPDATE WhseStk SET WHRe_Ord_Qty = " & IIf(IsDBNull(ugR1.Cells("Re_Ord_Qty").Value), 0, ugR1.Cells("Re_Ord_Qty").Value) & ", WHRe_Ord_Lvl = " & IIf(IsDBNull(ugR1.Cells("Re_Ord_Lvl").Value), 0, ugR1.Cells("Re_Ord_Lvl").Value) & "   WHERE WHStockLink = '" & ugR1.Cells("StockLink").Value & "' AND WHWhseID = '" & ugR1.Cells("WHID").Value & "' "
                    'End If
                Next
            End If
            Exit For
        Next
                    '                End If
                    '            Next
                    '        Next
                    'Exit For
                    '    End If
                    '

    End Sub

    Private Sub cmbInventoryItem_ItemNotInList(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinEditors.ValidationErrorEventArgs) Handles cmbInventoryItem.ItemNotInList

    End Sub

    Private Sub cmbInventoryItem_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbInventoryItem.KeyDown
        'If e.KeyCode = Keys.Enter Then
        '    Try
        '        If cmbInventoryItem.Value.ToString.Trim.Length = 0 Then
        '            MsgBox("Select Item Code", MsgBoxStyle.Exclamation, "Pastel Evoltuion")
        '            Exit Sub
        '        End If
        '        For Each ugR In UG.Rows.All
        '            ugR.Delete(False)
        '        Next

        '        Dim DS1 As New DataSet

        '        If (txtItem.Text.Trim.Length = 0) Then
        '            Interaction.MsgBox("Select Item Code", MsgBoxStyle.Exclamation, "Pastel Evoltuion")
        '        Else
        '            Me.UG.DataSource = Nothing
        '            Me.UG.Layouts.Clear()
        '            Dim str As String = ""
        '            Dim ch As Char
        '            For Each ch In Me.txtItem.Text.ToCharArray
        '                If Char.IsDigit(ch) Then
        '                    str = (str & ch.ToString)
        '                End If
        '            Next



        '            'For Each Obj In aDataBase
        '            '    sDB = Obj.ToString
        '            '    'sDB = "dbUdawatta"
        '            'Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & sDB & "")

        '            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= dbNawinna_new  ")
        '            SQL = " SELECT  'dbNawinna_new' AS [Database], 'Nawinna' AS Branch, '3' AS WHID, _bvStockFull.StockLink, _bvStockFull.Code, WhseStk.WHQtyOnHand as Qty_On_Hand, WhseStk.WHQtyOnPO as QtyOnPO, WhseStk.WHMax_Lvl as Max_Lvl, WhseStk.WHRe_Ord_Qty as Re_Ord_Qty, WhseStk.WHRe_Ord_Lvl as Re_Ord_Lvl FROM _bvStockFull INNER JOIN  WhseStk ON _bvStockFull.StockLink = WhseStk.WHStockLink WHERE Bar_Code = '" + str() + "' AND WhseStk.WHWhseID = 3 And ItemActive = 1"
        '            CMD = New SqlCommand(SQL, Con2)
        '            CMD.CommandType = CommandType.Text
        '            DA = New SqlDataAdapter(CMD)
        '            DS1 = New DataSet
        '            Con2.Open()
        '            DA.Fill(DS1)
        '            Con2.Close()

        '            'DS1.Tables(0).Merge(DS.Tables(0), False, MissingSchemaAction.Add)
        '            'DS1.Merge(DS.Tables(0), False, MissingSchemaAction.Add)

        '            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= dbNawinna_new")
        '        SQL = " SELECT  'dbNawinna_new' AS [Database], 'Kiribathgoda' AS Branch, '18' AS WHID, _bvStockFull.StockLink, _bvStockFull.Code, WhseStk.WHQtyOnHand as Qty_On_Hand, WhseStk.WHQtyOnPO as QtyOnPO, WhseStk.WHMax_Lvl as Max_Lvl, WhseStk.WHRe_Ord_Qty as Re_Ord_Qty, WhseStk.WHRe_Ord_Lvl as Re_Ord_Lvl FROM _bvStockFull INNER JOIN  WhseStk ON _bvStockFull.StockLink = WhseStk.WHStockLink  WHERE Bar_Code = '" + str + "' And WhseStk.WHWhseID = 18 And ItemActive = 1 ";
        '            CMD = New SqlCommand(SQL, Con2)
        '            CMD.CommandType = CommandType.Text
        '            DA = New SqlDataAdapter(CMD)
        '            DS = New DataSet
        '            Con2.Open()
        '            DA.Fill(DS)
        '            Con2.Close()

        '            DS1.Tables(0).Merge(DS.Tables(0), False, MissingSchemaAction.Add)


        '            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= dbNawinna_new ")
        '            SQL = (" SELECT  'dbNawinna_new' AS [Database], 'Negombo' AS Branch, '4' AS WHID, _bvStockFull.StockLink, _bvStockFull.Code, WhseStk.WHQtyOnHand as Qty_On_Hand, WhseStk.WHQtyOnPO as QtyOnPO, WhseStk.WHMax_Lvl as Max_Lvl, WhseStk.WHRe_Ord_Qty as Re_Ord_Qty, WhseStk.WHRe_Ord_Lvl as Re_Ord_Lvl FROM _bvStockFull INNER JOIN  WhseStk ON _bvStockFull.StockLink = WhseStk.WHStockLink WHERE Bar_Code = '" & str & "' And ItemActive = 1 AND WhseStk.WHWhseID = 4 ")
        '            CMD = New SqlCommand(SQL, Con2)
        '            CMD.CommandType = CommandType.Text
        '            DA = New SqlDataAdapter(CMD)
        '            DS = New DataSet
        '            Con2.Open()
        '            DA.Fill(DS)
        '            Con2.Close()


        '            DS1.Tables(0).Merge(DS.Tables(0), False, MissingSchemaAction.Add)


        '            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database = dbUdawatta_new ")
        '        SQL ="  SELECT  'dbUdawatta_new' AS [Database], 'Kurunegala' AS Branch, '4' AS WHID, _bvStockFull.StockLink, _bvStockFull.Code, WhseStk.WHQtyOnHand as Qty_On_Hand, WhseStk.WHQtyOnPO as QtyOnPO, WhseStk.WHMax_Lvl as Max_Lvl, WhseStk.WHRe_Ord_Qty as Re_Ord_Qty, WhseStk.WHRe_Ord_Lvl as Re_Ord_Lvl FROM _bvStockFull INNER JOIN  WhseStk ON _bvStockFull.StockLink = WhseStk.WHStockLink WHERE Bar_Code = '" & str & "' And ItemActive = 1 AND WhseStk.WHWhseID = 4 ")
        '            CMD = New SqlCommand(SQL, Con2)
        '            CMD.CommandType = CommandType.Text
        '            DA = New SqlDataAdapter(CMD)
        '            DS = New DataSet
        '            Con2.Open()
        '            DA.Fill(DS)
        '            Con2.Close()

        '            DS1.Merge(DS.Tables(0), False, MissingSchemaAction.Add)

        '            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= dbUdawatta_new ")
        '            SQL = ("  SELECT  'dbUdawatta_new' AS [Database], 'Panchikawatta' AS Branch, '2' AS WHID, _bvStockFull.StockLink, _bvStockFull.Code, WhseStk.WHQtyOnHand as Qty_On_Hand, WhseStk.WHQtyOnPO as QtyOnPO, WhseStk.WHMax_Lvl as Max_Lvl, WhseStk.WHRe_Ord_Qty as Re_Ord_Qty, WhseStk.WHRe_Ord_Lvl as Re_Ord_Lvl FROM _bvStockFull INNER JOIN  WhseStk ON _bvStockFull.StockLink = WhseStk.WHStockLink WHERE Bar_Code = '" & str & "' And ItemActive = 1 AND WhseStk.WHWhseID = 2 ")
        '            CMD = New SqlCommand(SQL, Con2)
        '            CMD.CommandType = CommandType.Text
        '            DA = New SqlDataAdapter(CMD)
        '            DS = New DataSet
        '            Con2.Open()
        '            DA.Fill(DS)
        '            Con2.Close()


        '            DS1.Tables(0).Merge(DS.Tables(0), False, MissingSchemaAction.Add)


        '            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= dbKelaniya_new ")
        '            SQL = ("  SELECT  'dbUdawatta_new' AS [Database], 'Panchikawatta' AS Branch, '2' AS WHID, _bvStockFull.StockLink, _bvStockFull.Code, WhseStk.WHQtyOnHand as Qty_On_Hand, WhseStk.WHQtyOnPO as QtyOnPO, WhseStk.WHMax_Lvl as Max_Lvl, WhseStk.WHRe_Ord_Qty as Re_Ord_Qty, WhseStk.WHRe_Ord_Lvl as Re_Ord_Lvl FROM _bvStockFull INNER JOIN  WhseStk ON _bvStockFull.StockLink = WhseStk.WHStockLink WHERE Bar_Code = '" & str & "' And ItemActive = 1 AND WhseStk.WHWhseID = 2 ")
        '            CMD = New SqlCommand(SQL, Con2)
        '            CMD.CommandType = CommandType.Text
        '            DA = New SqlDataAdapter(CMD)
        '            DS = New DataSet
        '            Con2.Open()
        '            DA.Fill(DS)
        '            Con2.Close()


        '            DS1.Tables(0).Merge(DS.Tables(0), False, MissingSchemaAction.Add)




        '            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= dbUdawatta_new ")
        '            SQL = ("  SELECT  'dbUdawatta_new' AS [Database], 'Matara' AS Branch, '18' AS WHID, _bvStockFull.StockLink, _bvStockFull.Code, WhseStk.WHQtyOnHand as Qty_On_Hand, WhseStk.WHQtyOnPO as QtyOnPO, WhseStk.WHMax_Lvl as Max_Lvl, WhseStk.WHRe_Ord_Qty as Re_Ord_Qty, WhseStk.WHRe_Ord_Lvl as Re_Ord_Lvl FROM _bvStockFull INNER JOIN  WhseStk ON _bvStockFull.StockLink = WhseStk.WHStockLink WHERE Bar_Code = '" & str & "' And ItemActive = 1 AND WhseStk.WHWhseID = 18 ")


        '            CMD = New SqlCommand(SQL, Con2)
        '            CMD.CommandType = CommandType.Text
        '            DA = New SqlDataAdapter(CMD)
        '            DS = New DataSet
        '            Con2.Open()
        '            DA.Fill(DS)
        '            Con2.Close()


        '            DS1.Tables(0).Merge(DS.Tables(0), False, MissingSchemaAction.Add)





        '            UG.DataSource = DS1.Tables(0)
        '            UG.DisplayLayout.Bands(0).Columns("StockLink").Hidden = True
        '            UG.DisplayLayout.Bands(0).Columns("Database").Hidden = True
        '            UG.DisplayLayout.Bands(0).Columns("WHID").Hidden = True


        '            'If DS.Tables(0).Rows.Count > 0 Then
        '            '    ugR1 = UG.DisplayLayout.Bands(0).AddNew
        '            '    ugR1.Cells("ID").Value = ugR1.Index + 1
        '            '    ugR1.Cells("DatabaseName").Value = sDB
        '            '    ugR1.Activated = True
        '            '    For Each Dr In DS.Tables(0).Rows
        '            '        If ugR1.ChildBands.HasChildRows = False Then
        '            '            ugR2 = UG.ActiveRow.ChildBands(0).Band.AddNew
        '            '            ugR2.Cells("ID").Value = ugR2.Index + 1
        '            '            ugR2.Cells("Code").Value = Dr("Code")
        '            '            ugR2.Cells("Qty").Value = Dr("Qty_On_Hand")
        '            '            ugR2.Cells("Slink").Value = Dr("StockLink")
        '            '        Else
        '            '            ugR2 = ugR1.ChildBands(0).Band.AddNew
        '            '            ugR2.Cells("ID").Value = ugR2.Index + 1
        '            '            ugR2.Cells("ID").Value = ugR2.Index + 1
        '            '            ugR2.Cells("Code").Value = Dr("Code")
        '            '            ugR2.Cells("Qty").Value = Dr("Qty_On_Hand")
        '            '            ugR2.Cells("Slink").Value = Dr("StockLink")
        '            '        End If
        '            '    Next
        '            'Else
        '            'MsgBox(cmbInventoryItem.Value & " can not find from database " & sDB & vbLf & "Please check item code from that database before changes update", MsgBoxStyle.Exclamation, "Pastel Evolution")
        '            'Exit Sub
        '            'End If
        '            'Next
        '            'MsgBox("Item Load Completed", MsgBoxStyle.Information, "Pastel Evolution")
        '    Catch ex As Exception
        '        MsgBox("Error found while loading item from databases", MsgBoxStyle.Exclamation, "Pastel Evolution")
        '        MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
        '    Finally
        '        DS.Dispose()
        '        DA.Dispose()
        '        CMD.Dispose()
        '    End Try
        'End If
    End Sub

    Private Sub UG_InitializeLayout_1(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs)
        e.Layout.Bands.Item(0).SortedColumns.Add("Code", False, True)
    End Sub

    Private Sub UG_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        ' perform action needed to move cursor
        Select Case e.KeyValue

            Case Keys.Up

                UG.PerformAction(UltraGridAction.ExitEditMode, False, False)
                UG.PerformAction(UltraGridAction.AboveCell, False, False)
                e.Handled = True
                UG.PerformAction(UltraGridAction.EnterEditMode, False, False)

            Case Keys.Down

                UG.PerformAction(UltraGridAction.ExitEditMode, False, False)
                UG.PerformAction(UltraGridAction.BelowCell, False, False)
                e.Handled = True
                UG.PerformAction(UltraGridAction.EnterEditMode, False, False)

            Case Keys.Right

                UG.PerformAction(UltraGridAction.ExitEditMode, False, False)
                UG.PerformAction(UltraGridAction.NextCellByTab, False, False)
                e.Handled = True
                UG.PerformAction(UltraGridAction.EnterEditMode, False, False)

            Case Keys.Left

                UG.PerformAction(UltraGridAction.ExitEditMode, False, False)
                UG.PerformAction(UltraGridAction.PrevCellByTab, False, False)
                e.Handled = True
                UG.PerformAction(UltraGridAction.EnterEditMode, False, False)
        End Select
    End Sub

    Private Sub txtItem_KeyDown(sender As Object, e As KeyEventArgs) Handles txtItem.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            Try
                If (Me.txtItem.Text.Trim.Length = 0) Then
                    Interaction.MsgBox("Select Item Code", MsgBoxStyle.Exclamation, "Pastel Evoltuion")
                Else
                    Me.UG.DataSource = Nothing
                    Me.UG.Layouts.Clear()
                    Dim str As String = ""
                    Dim ch As Char
                    For Each ch In Me.txtItem.Text.ToCharArray
                        If Char.IsDigit(ch) Then
                            str = (str & ch.ToString)
                        End If
                    Next
                    Dim dataSet As New DataSet
                    Con2 = New SqlConnection(String.Concat(New String() {"Server=", sSQLSrvName, ";User ID=", sSQLSrvUserName, ";Password=", sSQLSrvPassword, ";Database= dbNawinna_new  "}))
                    SQL = (" SELECT  'dbNawinna_new' AS [Database], 'Nawinna' AS Branch, '3' AS WHID, _bvStockFull.StockLink, _bvStockFull.Code, WhseStk.WHQtyOnHand as Qty_On_Hand, WhseStk.WHQtyOnPO as QtyOnPO, WhseStk.WHMax_Lvl as Max_Lvl, WhseStk.WHRe_Ord_Qty as Re_Ord_Qty, WhseStk.WHRe_Ord_Lvl as Re_Ord_Lvl FROM _bvStockFull INNER JOIN  WhseStk ON _bvStockFull.StockLink = WhseStk.WHStockLink WHERE Bar_Code = '" & str & "' AND WhseStk.WHWhseID = 3 And ItemActive = 1")
                    CMD = New SqlCommand(SQL, Con2)
                    CMD.CommandType = CommandType.Text
                    DA = New SqlDataAdapter(CMD)
                    dataSet = New DataSet
                    Con2.Open()
                    DA.Fill(dataSet)
                    Con2.Close()
                    Con2 = New SqlConnection(String.Concat(New String() {"Server=", sSQLSrvName, ";User ID=", sSQLSrvUserName, ";Password=", sSQLSrvPassword, ";Database= dbNawinna_new"}))
                    SQL = (" SELECT  'dbNawinna_new' AS [Database], 'Kiribathgoda' AS Branch, '18' AS WHID, _bvStockFull.StockLink, _bvStockFull.Code, WhseStk.WHQtyOnHand as Qty_On_Hand, WhseStk.WHQtyOnPO as QtyOnPO, WhseStk.WHMax_Lvl as Max_Lvl, WhseStk.WHRe_Ord_Qty as Re_Ord_Qty, WhseStk.WHRe_Ord_Lvl as Re_Ord_Lvl FROM _bvStockFull INNER JOIN  WhseStk ON _bvStockFull.StockLink = WhseStk.WHStockLink  WHERE Bar_Code = '" & str & "' And WhseStk.WHWhseID = 18 And ItemActive = 1 ")
                    CMD = New SqlCommand(SQL, Con2)
                    CMD.CommandType = CommandType.Text
                    DA = New SqlDataAdapter(CMD)
                    DS = New DataSet
                    Con2.Open()
                    DA.Fill(DS)
                    Con2.Close()
                    dataSet.Tables.Item(0).Merge(DS.Tables.Item(0), False, MissingSchemaAction.Add)
                    Con2 = New SqlConnection(String.Concat(New String() {"Server=", sSQLSrvName, ";User ID=", sSQLSrvUserName, ";Password=", sSQLSrvPassword, ";Database= dbNawinna_new "}))
                    SQL = (" SELECT  'dbNawinna_new' AS [Database], 'Negombo' AS Branch, '4' AS WHID, _bvStockFull.StockLink, _bvStockFull.Code, WhseStk.WHQtyOnHand as Qty_On_Hand, WhseStk.WHQtyOnPO as QtyOnPO, WhseStk.WHMax_Lvl as Max_Lvl, WhseStk.WHRe_Ord_Qty as Re_Ord_Qty, WhseStk.WHRe_Ord_Lvl as Re_Ord_Lvl FROM _bvStockFull INNER JOIN  WhseStk ON _bvStockFull.StockLink = WhseStk.WHStockLink WHERE Bar_Code = '" & str & "' And ItemActive = 1 AND WhseStk.WHWhseID = 4 ")
                    CMD = New SqlCommand(SQL, Con2)
                    CMD.CommandType = CommandType.Text
                    DA = New SqlDataAdapter(CMD)
                    DS = New DataSet
                    Con2.Open()
                    DA.Fill(DS)
                    Con2.Close()
                    dataSet.Tables.Item(0).Merge(DS.Tables.Item(0), False, MissingSchemaAction.Add)
                    Con2 = New SqlConnection(String.Concat(New String() {"Server=", sSQLSrvName, ";User ID=", sSQLSrvUserName, ";Password=", sSQLSrvPassword, ";Database = dbUdawatta_new "}))
                    SQL = ("  SELECT  'dbUdawatta_new' AS [Database], 'Kurunegala' AS Branch, '4' AS WHID, _bvStockFull.StockLink, _bvStockFull.Code, WhseStk.WHQtyOnHand as Qty_On_Hand, WhseStk.WHQtyOnPO as QtyOnPO, WhseStk.WHMax_Lvl as Max_Lvl, WhseStk.WHRe_Ord_Qty as Re_Ord_Qty, WhseStk.WHRe_Ord_Lvl as Re_Ord_Lvl FROM _bvStockFull INNER JOIN  WhseStk ON _bvStockFull.StockLink = WhseStk.WHStockLink WHERE Bar_Code = '" & str & "' And ItemActive = 1 AND WhseStk.WHWhseID = 4 ")
                    CMD = New SqlCommand(SQL, Con2)
                    CMD.CommandType = CommandType.Text
                    DA = New SqlDataAdapter(CMD)
                    DS = New DataSet
                    Con2.Open()
                    DA.Fill(DS)
                    Con2.Close()
                    dataSet.Merge(DS.Tables.Item(0), False, MissingSchemaAction.Add)
                    Con2 = New SqlConnection(String.Concat(New String() {"Server=", sSQLSrvName, ";User ID=", sSQLSrvUserName, ";Password=", sSQLSrvPassword, ";Database= dbUdawatta_new "}))
                    SQL = ("  SELECT  'dbUdawatta_new' AS [Database], 'Panchikawatta' AS Branch, '2' AS WHID, _bvStockFull.StockLink, _bvStockFull.Code, WhseStk.WHQtyOnHand as Qty_On_Hand, WhseStk.WHQtyOnPO as QtyOnPO, WhseStk.WHMax_Lvl as Max_Lvl, WhseStk.WHRe_Ord_Qty as Re_Ord_Qty, WhseStk.WHRe_Ord_Lvl as Re_Ord_Lvl FROM _bvStockFull INNER JOIN  WhseStk ON _bvStockFull.StockLink = WhseStk.WHStockLink WHERE Bar_Code = '" & str & "' And ItemActive = 1 AND WhseStk.WHWhseID = 2 ")
                    CMD = New SqlCommand(SQL, Con2)
                    CMD.CommandType = CommandType.Text
                    DA = New SqlDataAdapter(CMD)
                    DS = New DataSet
                    Con2.Open()
                    DA.Fill(DS)
                    Con2.Close()
                    dataSet.Tables.Item(0).Merge(DS.Tables.Item(0), False, MissingSchemaAction.Add)
                    Con2 = New SqlConnection(String.Concat(New String() {"Server=", sSQLSrvName, ";User ID=", sSQLSrvUserName, ";Password=", sSQLSrvPassword, ";Database= dbKelaniya_new "}))
                    SQL = ("  SELECT  'dbKelaniya_new' AS [Database], 'Kelaniya' AS Branch, '16' AS WHID, _bvStockFull.StockLink, _bvStockFull.Code, WhseStk.WHQtyOnHand - _bvStockFull.JobQty as Qty_On_Hand, WhseStk.WHQtyOnPO as QtyOnPO, WhseStk.WHMax_Lvl as Max_Lvl, WhseStk.WHRe_Ord_Qty as Re_Ord_Qty, WhseStk.WHRe_Ord_Lvl as Re_Ord_Lvl FROM _bvStockFull INNER JOIN  WhseStk ON _bvStockFull.StockLink = WhseStk.WHStockLink WHERE Bar_Code = '" & str & "' And ItemActive = 1 AND WhseStk.WHWhseID = 16 ")
                    CMD = New SqlCommand(SQL, Con2)
                    CMD.CommandType = CommandType.Text
                    DA = New SqlDataAdapter(CMD)
                    DS = New DataSet
                    Con2.Open()
                    DA.Fill(DS)
                    Con2.Close()
                    dataSet.Tables.Item(0).Merge(DS.Tables.Item(0), False, MissingSchemaAction.Add)
                    Con2 = New SqlConnection(String.Concat(New String() {"Server=", sSQLSrvName, ";User ID=", sSQLSrvUserName, ";Password=", sSQLSrvPassword, ";Database= dbUdawatta_new "}))
                    SQL = ("  SELECT  'dbUdawatta_new' AS [Database], 'Matara' AS Branch, '18' AS WHID, _bvStockFull.StockLink, _bvStockFull.Code, WhseStk.WHQtyOnHand as Qty_On_Hand, WhseStk.WHQtyOnPO as QtyOnPO, WhseStk.WHMax_Lvl as Max_Lvl, WhseStk.WHRe_Ord_Qty as Re_Ord_Qty, WhseStk.WHRe_Ord_Lvl as Re_Ord_Lvl FROM _bvStockFull INNER JOIN  WhseStk ON _bvStockFull.StockLink = WhseStk.WHStockLink WHERE Bar_Code = '" & str & "' And ItemActive = 1 AND WhseStk.WHWhseID = 18 ")
                    CMD = New SqlCommand(SQL, Con2)
                    CMD.CommandType = CommandType.Text
                    DA = New SqlDataAdapter(CMD)
                    DS = New DataSet
                    Con2.Open()
                    DA.Fill(DS)
                    Con2.Close()
                    dataSet.Tables.Item(0).Merge(DS.Tables.Item(0), False, MissingSchemaAction.Add)
                    Me.UG.DataSource = dataSet.Tables.Item(0)
                    Me.UG.DisplayLayout.Bands.Item(0).Columns.Item("StockLink").Hidden = True
                    Me.UG.DisplayLayout.Bands.Item(0).Columns.Item("Database").Hidden = True
                    Me.UG.DisplayLayout.Bands.Item(0).Columns.Item("WHID").Hidden = True
                    Me.UG.DisplayLayout.Bands.Item(0).Columns.Item(4).SortIndicator = SortIndicator.Ascending
                    Me.UG.Rows.ExpandAll(True)
                End If
            Catch exception1 As Exception

                Dim exception As Exception = exception1
                Interaction.MsgBox("Error found while loading item from databases", MsgBoxStyle.Exclamation, "Pastel Evolution")
                Interaction.MsgBox(exception.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")

            Finally
                DS.Dispose()
                DA.Dispose()
                CMD.Dispose()
            End Try
        End If

    End Sub

    Private Sub UG_InitializeLayout_2(sender As Object, e As InitializeLayoutEventArgs) Handles UG.InitializeLayout
        e.Layout.Bands.Item(0).SortedColumns.Add("Code", False, True)
    End Sub
End Class