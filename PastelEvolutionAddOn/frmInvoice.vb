Imports Infragistics.Win.UltraWinGrid
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Pastel.Evolution

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing


Public Class frmInvoice
    Dim sDeliveryNo As String
    Dim sInvoiceNo As String
    Dim dblMargin As Double
    Dim R As UltraGridRow
    Dim strCustomer As String
    Dim strProject As String
    Dim strSalesRep As String
    Dim sAccount As String
    Dim sAdd1 As String
    Dim sAdd2 As String
    Dim sAdd3 As String
    Dim sPAdd1 As String
    Dim sPAdd2 As String
    Dim sPAdd3 As String
    Dim sQUONo As String
    Dim sLTNo As String
    Dim iLTNo As Integer
    Dim bIsInclusive As Boolean = False
    Public bIsAllowOverrideCreditLimit As Boolean = False
    Public iWH As Integer = 0
    Public iSONo As Int64 = 0
    Public sSONo As String
    Public iOldSoNo As Integer = 0
    Public IsNew As Boolean = True
    Public iDocType As Integer
    Public intMstWH As Integer
    Public bValChange As Boolean = True
    'Public POLocation As Integer
    Public DS1 As DataSet
    Public clientWH As Integer

    Public Sub DeleteRows()
        Dim dr As UltraGridRow
        For Each dr In UG.Rows
            dr.Delete(False)
        Next
    End Sub

    Private Sub GET_DATA()
        sSQL = ""
        If sSQLSrvDataBase = "dbKelaniya_new" Then
            sSQL = "SELECT DCLink, Account, Name, Physical1, Physical2," & _
                   " Physical3, Post1, Post2, Post3 , Discount , cAccDescription, cIDNumber FROM Client WHERE cAccDescription like 'db%' Order By Name "
        Else
            sSQL = "SELECT DCLink, Account, Name, Physical1, Physical2," & _
                   " Physical3, Post1, Post2, Post3 , Discount , cAccDescription, cIDNumber FROM Client WHERE Post1 = 'IBT' Order By Name "
        End If
        sSQL += " SELECT idSalesRep, Code, Name FROM SalesRep WHERE Rep_On_Hold = 'N'  "
        sSQL += " SELECT ProjectLink, ProjectCode, ProjectName,ProjectDescription FROM Project"
        sSQL += " SELECT StatusCounter, StatusDescrip FROM OrdersSt"
        sSQL += " SELECT idIncidentPriority, cDescription FROM _rtblIncidentPriority"
        sSQL += " SELECT     StkItem.WhseItem, StkItem.iUOMStockingUnitID, StkItem.StockLink, StkItem.Code, StkItem.Description_1, StkItem.Description_2, StkItem.ItemGroup, " & _
         " StkItem.bLotItem AS LotItem, StkItem.LatUCst, StkItem.AveUCst, _etblUnits.iUnitCategoryID, StkItem.iItemCostingMethod, GrpTbl.fCostMargine, StkItem.cSimpleCode,  " & _
         " _btblBINLocation.cBinLocationName as Bin " & _
         " FROM         StkItem LEFT OUTER JOIN " & _
         " _btblBINLocation ON StkItem.iBinLocationID = _btblBINLocation.idBinLocation LEFT OUTER JOIN " & _
         " GrpTbl ON StkItem.ItemGroup = GrpTbl.StGroup LEFT OUTER JOIN " & _
         " _etblUnits ON StkItem.iUOMStockingUnitID = _etblUnits.idUnits " & _
         " WHERE     (StkItem.ServiceItem = 0) AND (StkItem.ItemActive = 1) " & _
         " ORDER BY StkItem.Description_1 "
        'sSQL += " SELECT StkItem.WhseItem, StkItem.iUOMStockingUnitID, StkItem.StockLink, StkItem.Code, StkItem.Description_1, StkItem.Description_2, StkItem.ItemGroup, StkItem.bLotItem as LotItem,  StkItem.LatUCst, " & _
        '        " StkItem.AveUCst, _etblUnits.iUnitCategoryID, StkItem.iItemCostingMethod, GrpTbl.fCostMargine, StkItem.cSimpleCode FROM StkItem LEFT OUTER JOIN " & _
        '        " GrpTbl ON StkItem.ItemGroup = GrpTbl.StGroup LEFT OUTER JOIN _etblUnits ON StkItem.iUOMStockingUnitID = _etblUnits.idUnits " & _
        '        " WHERE (StkItem.ServiceItem = 0) AND  ItemActive = 1 ORDER BY StkItem.Code"
        sSQL += " SELECT WhseLink, Code, Name, KnownAs FROM WhseMst"
        sSQL += " SELECT idTaxRate, Code, Description, TaxRate FROM TaxRate"
        sSQL += " SELECT idUnits, cUnitCode,iUnitCategoryID FROM   _etblUnits"
        sSQL += " SELECT bIsInclusive FROM StDfTbl"
        sSQL += " SELECT WhseLink FROM WhseMst WHERE DefaultWhse = 1"
        sSQL += " SELECT StGroup FROM GrpTbl "
        sSQL += " SELECT idAgents, cAgentName, iDefProjectID from _rtblAgents WHERE idAgents = " & iAgent & " "
        Try
            Con1.ConnectionString = sConStr
            CMD = New SqlCommand(sSQL, Con1)
            DS = New DataSet
            DA = New SqlDataAdapter(CMD)

            Con1.Open()
            DA.Fill(DS)
            Con1.Close()

            cmbCustomer.DataSource = DS.Tables(0)
            cmbCustomer.ValueMember = "DCLink"
            cmbCustomer.DisplayMember = "Name"
            cmbCustomer.DisplayLayout.Bands(0).Columns("DCLink").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Physical1").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Physical2").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Physical3").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Post1").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Post2").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Post3").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Discount").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("cAccDescription").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("cIDNumber").Hidden = True


            'Sales Rep
            cmbSaleRep.DataSource = DS.Tables(1)
            cmbSaleRep.ValueMember = "idSalesRep"
            cmbSaleRep.DisplayMember = "Name"
            cmbSaleRep.DisplayLayout.Bands(0).Columns("idSalesRep").Hidden = True

            'Projects
            cmbProject.DataSource = DS.Tables(2)
            cmbProject.ValueMember = "ProjectLink"
            cmbProject.DisplayMember = "ProjectName"
            cmbProject.DisplayLayout.Bands(0).Columns("ProjectLink").Hidden = True

            'Order Status
            cmbOrderStatus.DataSource = DS.Tables(3)
            cmbOrderStatus.ValueMember = "StatusCounter"
            cmbOrderStatus.DisplayMember = "StatusDescrip"
            cmbOrderStatus.DisplayLayout.Bands(0).Columns("StatusCounter").Hidden = True

            'Priority
            cmbPriority.DataSource = DS.Tables(4)
            cmbPriority.ValueMember = "idIncidentPriority"
            cmbPriority.DisplayMember = "cDescription"
            cmbPriority.DisplayLayout.Bands(0).Columns("idIncidentPriority").Hidden = True

            'Stock
            DDStock.DataSource = DS.Tables(5)
            DDStock.ValueMember = "StockLink"
            DDStock.DisplayMember = "Code"
            DDStock.DisplayLayout.Bands(0).Columns("LatUCst").Hidden = True
            DDStock.DisplayLayout.Bands(0).Columns("WhseItem").Hidden = True
            DDStock.DisplayLayout.Bands(0).Columns("iUOMStockingUnitID").Hidden = True
            DDStock.DisplayLayout.Bands(0).Columns("iUnitCategoryID").Hidden = True
            DDStock.DisplayLayout.Bands(0).Columns("iItemCostingMethod").Hidden = True
            DDStock.DisplayLayout.Bands(0).Columns("AveUCst").Hidden = True
            'DDStock.DisplayLayout.Bands(0).Columns("SerialItem").Hidden = True

            'Description
            DDDescription.DataSource = DS.Tables(5)
            DDDescription.ValueMember = "Description_1"
            DDDescription.DisplayMember = "Description_1"
            DDDescription.DisplayLayout.Bands(0).Columns("LatUCst").Hidden = True
            DDDescription.DisplayLayout.Bands(0).Columns("WhseItem").Hidden = True
            DDDescription.DisplayLayout.Bands(0).Columns("iUOMStockingUnitID").Hidden = True
            DDDescription.DisplayLayout.Bands(0).Columns("iUnitCategoryID").Hidden = True
            DDDescription.DisplayLayout.Bands(0).Columns("iItemCostingMethod").Hidden = True
            DDDescription.DisplayLayout.Bands(0).Columns("AveUCst").Hidden = True

            'WH
            DDWH.DataSource = DS.Tables(6)
            DDWH.ValueMember = "WhseLink"
            DDWH.DisplayMember = "Code"
            DDWH.DisplayLayout.Bands(0).Columns("WhseLink").Hidden = True

            'Tax
            DDTaxType.DataSource = DS.Tables(7)
            DDTaxType.ValueMember = "idTaxRate"
            DDTaxType.DisplayMember = "Code"
            DDTaxType.DisplayLayout.Bands(0).Columns("idTaxRate").Hidden = True

            DDUnit.DataSource = DS.Tables(8)
            DDUnit.ValueMember = "idUnits"
            DDUnit.DisplayMember = "cUnitCode"
            DDUnit.DisplayLayout.Bands(0).Columns("idUnits").Hidden = True
            DDUnit.DisplayLayout.Bands(0).Columns("iUnitCategoryID").Hidden = True
            DDUnit.DisplayLayout.Bands(0).Columns("cUnitCode").Header.Caption = "Unit"
            Dim Dr As DataRow
            For Each Dr In DS.Tables(9).Rows
                If Dr("bIsInclusive") = False Then
                    bIsInclusive = False
                ElseIf Dr("bIsInclusive") = True Then
                    bIsInclusive = True
                End If
            Next

            For Each Dr In DS.Tables(10).Rows
                iWH = CInt(Dr("WhseLink"))
            Next

            cmbAuto.DataSource = DS.Tables(11)
            cmbAuto.ValueMember = "StGroup"
            cmbAuto.DisplayMember = "StGroup"
            cmbAuto.DisplayLayout.Bands(0).Columns("StGroup").Width = 500
            'DDUnit.DisplayLayout.Bands(0).Columns("iUnitCategoryID").Hidden = True
            'DDUnit.DisplayLayout.Bands(0).Columns("cUnitCode").Header.Caption = "Unit"

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
            Exit Sub
        Finally
            If Con1.State = ConnectionState.Open Then Con1.Close()
        End Try


    End Sub
    Private Sub frmInvoice_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Discard()
    End Sub
    Private Sub Read_Company()
        Dim oReader As StreamReader
        oReader = File.OpenText(Application.StartupPath & "\LOC.txt")
        Do While oReader.Peek <> -1
            cmbGRVLoc.Items.Add(oReader.ReadLine)
        Loop

        oReader.Close()
    End Sub


    Private Sub frmSalesOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GET_DATA()

        If IsDBNull(DS.Tables(12).Rows(0)(2)) Then
            cmbProject.Value = 0
        Else
            cmbProject.Value = DS.Tables(12).Rows(0)(2)
        End If

        GET_NEXT_INVOICE_NO()
        getmstWH()
        Read_Company()

        dtpDeliDate.Value = Date.Now
        dtpDueDate.Value = Date.Now
        dtpInDate.Value = Date.Now
        dtpOrdDate.Value = Date.Now

        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Invoice.xml") Then
            UG.DisplayLayout.LoadFromXml(Application.StartupPath & "\Invoice.xml")
        End If

        If bIsInclusive = True Then
            cbInEx.Checked = True
            cbInEx_CheckedChanged(cbInEx, e)
        ElseIf bIsInclusive = False Then
            cbInEx.Checked = False
            cbInEx_CheckedChanged(cbInEx, e)
        End If
        For Each Dr In dsManu.Tables(0).Rows
            If 22233 = Dr("iManu") Then
                UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
                UG.DisplayLayout.Bands(0).Columns("Serial").Hidden = True
                'UG.DisplayLayout.Bands(0).Columns("IsLot").Hidden = True
                UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
                UG.DisplayLayout.Bands(0).Columns("fCostMargine").Hidden = True
                UG.DisplayLayout.Bands(0).Columns("fUnitCostMargine").Hidden = True
                'UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default
            ElseIf 22235 = Dr("iManu") Then
                dtpInDate.Enabled = False
            End If
        Next

        


        UG.DisplayLayout.Bands(0).Columns("AvailableQty").CellActivation = Activation.Disabled
        UG.DisplayLayout.Bands(0).Columns("BranchQty").CellActivation = Activation.Disabled
        'UG.DisplayLayout.Bands(0).Columns("WH").CellActivation = Activation.Disabled
        UG.DisplayLayout.Bands(0).Columns("WHMax_lvl").CellActivation = Activation.Disabled
        'UG.DisplayLayout.Bands(0).Columns("BranchQty").CellActivation = Activation.Disabled
        UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton


        UG.DisplayLayout.Bands(0).Columns("Lot").ValueList = DDLot

        If sSQLSrvDataBase <> "dbKelaniya_new" And sSQLSrvDataBase <> "dbUdawatta_new" Then
            'tsbSave.Visible = False
            tsbSave.Enabled = False
            'cmbCustomer.Value = 455
            'cmbCustomer.Text = "UDAWATTA MOTORS PVT LTD - PKW"
            'cmbCustomer.Enabled = False

        Else
            tsbValidate.Visible = True
        End If

        'If cmbGRVLoc.Text <> "dbUdawatta_new" Or cmbGRVLoc.Text <> "" Then
        '    UG.DisplayLayout.Bands(0).Columns("Quantity").CellActivation = Activation.Disabled
        'End If



    End Sub

    Private Sub getmstWH()

        SQL = " SELECT  WhseLink FROM WhseMst WHERE DefaultWhse = 1 "
        ', Code, Name

        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                Dim dr As DataRow
                DS = .Get_Data_Sql(SQL) ' this is just to take the value of feild
                For Each dr In DS.Tables(0).Rows
                    intMstWH = dr.Item(0)

                Next
                'cmbAccount.DataSource = DS.Tables(0)
                'cmbAccount.ValueMember = "DCLink"
                'cmbAccount.DisplayMember = "Name"
                'cmbAccount.DisplayLayout.Bands(0).Columns("DCLink").Hidden = True
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                Exit Sub
            Finally
                objSQL = Nothing

            End Try
        End With

    End Sub

    Private Sub DDStock_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDStock.Click
        Try
            If DDStock.ActiveRow.Activated = True Then

                UG.ActiveRow.Cells("StockID").Value = DDStock.ActiveRow.Cells("StockLink").Value
                UG.ActiveRow.Cells("Code").Value = DDStock.ActiveRow.Cells("Code").Value
                UG.ActiveRow.Cells("Description_1").Value = DDStock.ActiveRow.Cells("Description_1").Value
                UG.ActiveRow.Cells("Description_2").Value = DDStock.ActiveRow.Cells("Description_1").Value
                UG.ActiveRow.Cells("iUnit").Value = DDStock.ActiveRow.Cells("iUOMStockingUnitID").Value

                UG.ActiveRow.Cells("cSimpleCode").Value = DDStock.ActiveRow.Cells("cSimpleCode").Value
                UG.ActiveRow.Cells("Bin").Value = DDStock.ActiveRow.Cells("Bin").Value

                If DDStock.ActiveRow.Cells("iItemCostingMethod").Value = 0 Then
                    UG.ActiveRow.Cells("CostPrice").Value = Math.Round(DDStock.ActiveRow.Cells("AveUCst").Value, 2, MidpointRounding.AwayFromZero)
                ElseIf DDStock.ActiveRow.Cells("iItemCostingMethod").Value = 1 Then
                    UG.ActiveRow.Cells("CostPrice").Value = Math.Round(DDStock.ActiveRow.Cells("LatUCst").Value, 2, MidpointRounding.AwayFromZero)
                End If

                UG.ActiveRow.Cells("iUnitCate").Value = DDStock.ActiveRow.Cells("iUnitCategoryID").Value
                UG.ActiveRow.Cells("IsLot").Value = IIf(DDStock.ActiveRow.Cells("LotItem").Value = True, True, False)
                UG.ActiveRow.Cells("IsWH").Value = IIf(DDStock.ActiveRow.Cells("WhseItem").Value = True, True, False)
                UG.ActiveRow.Cells("fCostMargine").Value = IIf(DDStock.ActiveRow.Cells("fCostMargine").Value = True, True, False)

                If UG.ActiveRow.Cells("IsWH").Value = True Then
                    UG.ActiveRow.Cells("Warehouse").Activation = Activation.AllowEdit
                Else
                    UG.ActiveRow.Cells("Warehouse").Activation = Activation.Disabled
                End If

            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub


    Private Sub DDStock_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DDStock.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If DDStock.ActiveRow.Activated = True Then

                    UG.ActiveRow.Cells("StockID").Value = DDStock.ActiveRow.Cells("StockLink").Value
                    UG.ActiveRow.Cells("Code").Value = DDStock.ActiveRow.Cells("Code").Value
                    UG.ActiveRow.Cells("Description_1").Value = DDStock.ActiveRow.Cells("Description_1").Value
                    UG.ActiveRow.Cells("Description_2").Value = DDStock.ActiveRow.Cells("Description_1").Value
                    UG.ActiveRow.Cells("iUnit").Value = DDStock.ActiveRow.Cells("iUOMStockingUnitID").Value

                    UG.ActiveRow.Cells("cSimpleCode").Value = DDStock.ActiveRow.Cells("cSimpleCode").Value
                    UG.ActiveRow.Cells("Bin").Value = DDStock.ActiveRow.Cells("Bin").Value

                    If DDStock.ActiveRow.Cells("iItemCostingMethod").Value = 0 Then
                        UG.ActiveRow.Cells("CostPrice").Value = Math.Round(DDStock.ActiveRow.Cells("AveUCst").Value, 2, MidpointRounding.AwayFromZero)
                    ElseIf DDStock.ActiveRow.Cells("iItemCostingMethod").Value = 1 Then
                        UG.ActiveRow.Cells("CostPrice").Value = Math.Round(DDStock.ActiveRow.Cells("LatUCst").Value, 2, MidpointRounding.AwayFromZero)
                    End If

                    UG.ActiveRow.Cells("iUnitCate").Value = DDStock.ActiveRow.Cells("iUnitCategoryID").Value
                    UG.ActiveRow.Cells("IsLot").Value = IIf(DDStock.ActiveRow.Cells("LotItem").Value = True, True, False)
                    UG.ActiveRow.Cells("IsWH").Value = IIf(DDStock.ActiveRow.Cells("WhseItem").Value = True, True, False)
                    UG.ActiveRow.Cells("fCostMargine").Value = IIf(DDStock.ActiveRow.Cells("fCostMargine").Value = True, True, False)

                    If UG.ActiveRow.Cells("IsWH").Value = True Then
                        UG.ActiveRow.Cells("Warehouse").Activation = Activation.AllowEdit
                    Else
                        UG.ActiveRow.Cells("Warehouse").Activation = Activation.Disabled
                    End If
                End If
            End If


        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Private Sub DDDescription_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDDescription.Click
        Try
            If DDDescription.ActiveRow.Activated = True Then
                ' '' ''UG.ActiveRow.Cells("StockID").Value = DDDescription.ActiveRow.Cells("StockLink").Value
                ' '' ''UG.ActiveRow.Cells("Warehouse").Value = intMstWH
                ' '' ''UG.ActiveRow.Cells("Code").Value = DDDescription.ActiveRow.Cells("Code").Value
                ' '' ''UG.ActiveRow.Cells("Description_1").Value = DDDescription.ActiveRow.Cells("Description_1").Value
                ' '' ''UG.ActiveRow.Cells("Description_2").Value = DDDescription.ActiveRow.Cells("Description_1").Value
                ' '' ''UG.ActiveRow.Cells("iUnit").Value = DDDescription.ActiveRow.Cells("iUOMStockingUnitID").Value
                ' '' ''If DDStock.ActiveRow.Cells("iItemCostingMethod").Value = 0 Then
                ' '' ''    UG.ActiveRow.Cells("CostPrice").Value = Math.Round(DDDescription.ActiveRow.Cells("AveUCst").Value, 2, MidpointRounding.AwayFromZero)
                ' '' ''ElseIf DDStock.ActiveRow.Cells("iItemCostingMethod").Value = 1 Then
                ' '' ''    UG.ActiveRow.Cells("CostPrice").Value = Math.Round(DDDescription.ActiveRow.Cells("LatUCst").Value, 2, MidpointRounding.AwayFromZero)
                ' '' ''Else
                ' '' ''End If
                ' '' ''UG.ActiveRow.Cells("iUnitCate").Value = DDDescription.ActiveRow.Cells("iUnitCategoryID").Value
                ' '' ''UG.ActiveRow.Cells("IsSerialNo").Value = IIf(DDDescription.ActiveRow.Cells("SerialItem").Value = True, True, False)
                ' '' ''UG.ActiveRow.Cells("IsWH").Value = IIf(DDDescription.ActiveRow.Cells("WhseItem").Value = True, True, False)
                ' '' ''UG.ActiveRow.Cells("fCostMargine").Value = IIf(DDDescription.ActiveRow.Cells("fCostMargine").Value = True, True, False)

                ' '' ''If UG.ActiveRow.Cells("IsWH").Value = True Then
                ' '' ''    UG.ActiveRow.Cells("Warehouse").Activation = Activation.AllowEdit
                ' '' ''Else
                ' '' ''    UG.ActiveRow.Cells("Warehouse").Activation = Activation.Disabled
                ' '' ''End If

                UG.ActiveRow.Cells("StockID").Value = DDDescription.ActiveRow.Cells("StockLink").Value
                UG.ActiveRow.Cells("Warehouse").Value = intMstWH
                UG.ActiveRow.Cells("Line").Value = UG.ActiveRow.Index + 1

                UG.ActiveRow.Cells("Code").Value = DDDescription.ActiveRow.Cells("StockLink").Value
                UG.ActiveRow.Cells("Description_1").Value = DDDescription.ActiveRow.Cells("Description_1").Value
                UG.ActiveRow.Cells("Description_2").Value = DDDescription.ActiveRow.Cells("Description_1").Value
                UG.ActiveRow.Cells("iUnit").Value = DDDescription.ActiveRow.Cells("iUOMStockingUnitID").Value
                UG.ActiveRow.Cells("iUnitCate").Value = DDDescription.ActiveRow.Cells("iUnitCategoryID").Value
                UG.ActiveRow.Cells("IsLot").Value = IIf(DDDescription.ActiveRow.Cells("LotItem").Value = True, True, False)
                UG.ActiveRow.Cells("Bin").Value = DDDescription.ActiveRow.Cells("Bin").Value


            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Private Sub DDDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DDDescription.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If DDDescription.ActiveRow.Activated = True Then

                    UG.ActiveRow.Cells("StockID").Value = DDDescription.ActiveRow.Cells("StockLink").Value

                    UG.ActiveRow.Cells("Warehouse").Value = intMstWH

                    UG.ActiveRow.Cells("Line").Value = UG.ActiveRow.Index + 1

                    UG.ActiveRow.Cells("Code").Value = DDDescription.ActiveRow.Cells("Code").Value
                    UG.ActiveRow.Cells("Description_1").Value = DDDescription.ActiveRow.Cells("Description_1").Value
                    UG.ActiveRow.Cells("Description_2").Value = DDDescription.ActiveRow.Cells("Description_1").Value
                    UG.ActiveRow.Cells("iUnit").Value = DDDescription.ActiveRow.Cells("iUOMStockingUnitID").Value

                    'If DDStock.ActiveRow.Cells("iItemCostingMethod").Value = 0 Then
                    '    UG.ActiveRow.Cells("CostPrice").Value = Math.Round(DDDescription.ActiveRow.Cells("AveUCst").Value, 2, MidpointRounding.AwayFromZero)
                    'ElseIf DDStock.ActiveRow.Cells("iItemCostingMethod").Value = 1 Then
                    '    UG.ActiveRow.Cells("CostPrice").Value = Math.Round(DDDescription.ActiveRow.Cells("LatUCst").Value, 2, MidpointRounding.AwayFromZero)
                    'Else
                    'End If
                    'UG.ActiveRow.Cells("fCostMargine").Value = IIf(DDDescription.ActiveRow.Cells("fCostMargine").Value = True, True, False)
                    'UG.ActiveRow.Cells("IsWH").Value = IIf(DDDescription.ActiveRow.Cells("WhseItem").Value = True, True, False)

                    'If UG.ActiveRow.Cells("IsWH").Value = True Then
                    '    UG.ActiveRow.Cells("Warehouse").Activation = Activation.AllowEdit
                    'Else
                    '    UG.ActiveRow.Cells("Warehouse").Activation = Activation.Disabled
                    'End If

                    UG.ActiveRow.Cells("iUnitCate").Value = DDDescription.ActiveRow.Cells("iUnitCategoryID").Value
                    UG.ActiveRow.Cells("IsLot").Value = IIf(DDDescription.ActiveRow.Cells("LotItem").Value = True, True, False)
                    UG.ActiveRow.Cells("Bin").Value = DDDescription.ActiveRow.Cells("Bin").Value
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DDTaxType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDTaxType.Click
        Try
            If DDTaxType.ActiveRow.Activated = True Then
                UG.ActiveRow.Cells("TaxType").Value = DDTaxType.ActiveRow.Cells("Code").Value
                UG.ActiveRow.Cells("TaxRate").Value = DDTaxType.ActiveRow.Cells("TaxRate").Value
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Private Sub tsmToggleInclusiveExclusive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmToggleInclusiveExclusive.Click
        If cbInEx.Checked = True Then
            If MsgBox(" Change From Tax Inclusive To Tax Exclusive ? ", MsgBoxStyle.YesNo, "New Sales Order " & txtOrdNo.Text & "  " & txtInNo.Text) = MsgBoxResult.Yes Then
                cbInEx.Checked = False
                cbInEx_CheckedChanged(cbInEx, e)
            End If
        ElseIf cbInEx.Checked = False Then
            If MsgBox(" Change From Tax Excusive To Tax Inclusive ? ", MsgBoxStyle.YesNo, "New Sales Order " & txtOrdNo.Text & "  " & txtInNo.Text) = MsgBoxResult.Yes Then
                cbInEx.Checked = True
                cbInEx_CheckedChanged(cbInEx, e)
            End If
        End If
    End Sub
    Private Sub cbInEx_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbInEx.CheckedChanged
        'If cbInEx.Checked = True Then
        '    UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
        '    UG.DisplayLayout.Bands(0).Columns("OrderTotal_Excl").Hidden = True
        '    UG.DisplayLayout.Bands(0).Columns("LineTotal_Excl").Hidden = True
        '    UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = False
        '    UG.DisplayLayout.Bands(0).Columns("OrderTotal_Incl").Hidden = False
        '    UG.DisplayLayout.Bands(0).Columns("LineTotal_Incl").Hidden = False
        'ElseIf cbInEx.Checked = False Then
        '    UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
        '    UG.DisplayLayout.Bands(0).Columns("OrderTotal_Incl").Hidden = True
        '    UG.DisplayLayout.Bands(0).Columns("LineTotal_Incl").Hidden = True
        '    UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = False
        '    UG.DisplayLayout.Bands(0).Columns("OrderTotal_Excl").Hidden = False
        '    UG.DisplayLayout.Bands(0).Columns("LineTotal_Excl").Hidden = False
        'End If

    End Sub
    Private Function Get_Value(ByVal SQL As String) As Decimal
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

    Private Function Get_Branch_Qty(ByVal SQL As String) As Double
        Try
            Dim oQty As Object
            'Con1.ConnectionString = sConStr
            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= " & cmbGRVLoc.Text & "  ")
            CMD = New SqlCommand(SQL, Con2)
            CMD.CommandType = CommandType.Text
            Con2.Open()
            oQty = CMD.ExecuteScalar
            Con2.Close()
            Return CDbl(oQty)
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Function LineTotal_Incl(ByVal bInclusive As Boolean, ByVal Qty As Double, ByVal Discount As Double, ByVal PriceExcl As Double, ByVal PriceIncl As Double, ByVal TaxRate As Double) As Double
        Dim dblLineTotalIncl As Double = 0
        'round( [ConfirmQty]   * [Price_Incl] - ((  [ConfirmQty]   * [Price_Incl] )* [Discount] /100) , 2 , 0 )
        If Discount = 0 Then
            dblLineTotalIncl = Math.Round((Qty * PriceIncl), 2, MidpointRounding.AwayFromZero)
        ElseIf Discount > 0 Then
            dblLineTotalIncl = Math.Round((Qty * PriceIncl) - ((Qty * PriceIncl) * Discount / 100), 2, MidpointRounding.AwayFromZero)
        End If

        Return dblLineTotalIncl
    End Function

    Public Function LineTotal_Excl(ByVal bInclusive As Boolean, ByVal Qty As Double, ByVal Discount As Double, ByVal PriceExcl As Double, ByVal PriceIncl As Double, ByVal TaxRate As Double) As Double
        Dim dblLineTotalExcl As Double = 0
        'LineTotal_Excl = round( [ConfirmQty]   * [Price_Excl] - ((  [ConfirmQty]   * [Price_Excl] )* [Discount] /100) , 2 , 0 )
        dblLineTotalExcl = Math.Round((Qty * PriceExcl) - ((Qty * PriceExcl) * Discount / 100), 2, MidpointRounding.AwayFromZero)
        Return dblLineTotalExcl
    End Function
    Public Function LineTax(ByVal bInclusive As Boolean, ByVal Qty As Double, ByVal Discount As Double, ByVal PriceExcl As Double, ByVal PriceIncl As Double, ByVal TaxRate As Double) As Double
        Dim dblLineTax As Double = 0
        Dim dblLineTotalExcl As Double = 0
        Dim dblLineTotalInclDis As Double = 0
        ' round( ([ConfirmQty] * [Price_Excl]  - (([ConfirmQty] * [Price_Excl] )* [Discount] /100))* [TaxRate] /100 , 2 , 0 )
        If bInclusive = True Then
            If TaxRate > 0 Then
                If Discount > 0 Then
                    dblLineTotalExcl = Math.Round((Qty * PriceIncl) * 100 / (100 + TaxRate), 2, MidpointRounding.AwayFromZero)
                    dblLineTotalInclDis = dblLineTotalExcl - dblLineTotalExcl * Discount / 100
                    dblLineTax = dblLineTotalInclDis * TaxRate / 100
                Else
                    dblLineTax = Math.Round((Qty * PriceIncl) - ((Qty * PriceIncl) * 100 / (100 + TaxRate)), 2, MidpointRounding.AwayFromZero)
                End If

            ElseIf TaxRate = 0 Then
                dblLineTax = 0
            End If
        ElseIf bInclusive = False Then
            If TaxRate > 0 Then
                dblLineTax = Math.Round(((Qty * PriceExcl) - ((Qty * PriceExcl) * Discount / 100)) * TaxRate / 100, 2, MidpointRounding.AwayFromZero)
            ElseIf TaxRate = 0 Then
                dblLineTax = 0
            End If
        End If
        Return dblLineTax
    End Function
    Public Function LineDis_Excl(ByVal bInclusive As Boolean, ByVal Qty As Double, ByVal Discount As Double, ByVal PriceExcl As Double, ByVal PriceIncl As Double, ByVal TaxRate As Double) As Double
        Dim dblLineDisExcl As Double = 0
        ' round( ( [ConfirmQty] * [Price_Excl] )* [Discount] /100 , 2 , 0 )
        dblLineDisExcl = Math.Round((Qty * PriceExcl) * Discount / 100, 2, MidpointRounding.AwayFromZero)
        Return dblLineDisExcl
    End Function
    Public Function LineDis_Incl(ByVal bInclusive As Boolean, ByVal Qty As Double, ByVal Discount As Double, ByVal PriceExcl As Double, ByVal PriceIncl As Double, ByVal TaxRate As Double) As Double
        Dim dblLineDisIncl As Double = 0
        ' round( ([ConfirmQty] * [Price_Incl] )* [Discount] /100 , 2 , 0 )
        dblLineDisIncl = Math.Round((Qty * PriceIncl) * Discount / 100, 2, MidpointRounding.AwayFromZero)
        Return dblLineDisIncl
    End Function
    Public Function MarginAmount(ByVal CostMarginPer As Double, ByVal CostPrice As Double) As Double
        Dim dblMarginAmount As Double = 0
        dblMarginAmount = Math.Round(CostPrice * CostMarginPer / 100, 2, MidpointRounding.AwayFromZero)
        Return dblMarginAmount
    End Function
    Public Function CalPriceExcl(ByVal MarginAmount As Double, ByVal CostPrice As Double) As Double
        Dim dblCalPriceExcl As Double = 0
        dblCalPriceExcl = Math.Round(CostPrice - MarginAmount, 2, MidpointRounding.AwayFromZero)
        Return dblCalPriceExcl
    End Function
    Private Sub UG_AfterCellUpdate(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UG.AfterCellUpdate
        Try

            If e.Cell.Column.Key = "TaxType" Then
                For Each ugR1 As UltraGridRow In DDTaxType.Rows
                    If e.Cell.Row.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
                        bValChange = False
                        e.Cell.Row.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
                        bValChange = True
                        Exit For
                    End If
                Next
            ElseIf e.Cell.Column.Key = "Code" Then

                If e.Cell.Row.Cells("Code").Text = Nothing Then
                    e.Cell.Row.Cells("Code").Value = 0
                End If

                bValChange = False

                'SQL = " SELECT StkItem.WhseItem, StkItem.iUOMStockingUnitID, StkItem.StockLink, StkItem.Code, " & _
                '        " StkItem.Description_1, StkItem.Description_2, StkItem.ItemGroup, StkItem.bLotItem as LotItem, StkItem.LatUCst, " & _
                '        " StkItem.AveUCst, _etblUnits.iUnitCategoryID, StkItem.iItemCostingMethod, GrpTbl.fCostMargine FROM StkItem LEFT OUTER JOIN " & _
                '        " GrpTbl ON StkItem.ItemGroup = GrpTbl.StGroup LEFT OUTER JOIN _etblUnits ON StkItem.iUOMStockingUnitID = _etblUnits.idUnits " & _
                '        " WHERE  StkItem.ServiceItem = 0  and StkItem.StockLink  = " & e.Cell.Value & " ORDER BY StkItem.Code"

                'Dim lvl As String = ""
                'If clientWH = 3 Then 'nawinna
                '    lvl = "Max_Lvl"
                'ElseIf cmbGRVLoc.Text = "dbNawinna_new" And clientWH = 4 Then 'negambo
                '    lvl = "ufIINEGMAX"
                'ElseIf cmbGRVLoc.Text = "dbKurunegala_new" And clientWH = 4 Then 'negambo
                '    lvl = "Max_Lvl"
                'ElseIf clientWH = 2 Then 'panchkawatta
                '    lvl = "Max_Lvl"
                'ElseIf clientWH = 18 Then 'Kiribathgoda
                '    lvl = "Max_Lvl"
                'ElseIf clientWH = 1 Then 'matara
                '    lvl = "Max_Lvl"
                'End If

                'maxlvl in item table
                'SQL = " SELECT     StkItem.WhseItem, StkItem.iUOMStockingUnitID, StkItem.StockLink, StkItem.Code, StkItem.Description_1, StkItem.Description_2, StkItem.ItemGroup, " & _
                '      " StkItem.bLotItem AS LotItem, StkItem.LatUCst, StkItem.AveUCst, _etblUnits.iUnitCategoryID, StkItem.iItemCostingMethod, GrpTbl.fCostMargine, " & _
                '    "  _etblPriceListPrices.fExclPrice, StkItem.Max_Lvl FROM         StkItem INNER JOIN " & _
                '    "  _etblPriceListPrices ON StkItem.StockLink = _etblPriceListPrices.iStockID LEFT OUTER JOIN " & _
                '    "  GrpTbl ON StkItem.ItemGroup = GrpTbl.StGroup LEFT OUTER JOIN " & _
                '    "   _etblUnits ON StkItem.iUOMStockingUnitID = _etblUnits.idUnits " & _
                '    " WHERE(StkItem.ServiceItem = 0) and StkItem.StockLink  = " & e.Cell.Value & " And _etblPriceListPrices.iPriceListNameID = 1 ORDER BY StkItem.Code "

                'MaximizeBox lvl in WH table
                SQL = " SELECT     StkItem.WhseItem, StkItem.iUOMStockingUnitID, StkItem.StockLink, StkItem.Code, StkItem.Description_1, StkItem.Description_2, StkItem.ItemGroup, " & _
                      " StkItem.bLotItem AS LotItem, StkItem.LatUCst, StkItem.AveUCst, _etblUnits.iUnitCategoryID, StkItem.iItemCostingMethod, GrpTbl.fCostMargine, " & _
                    "  _etblPriceListPrices.fExclPrice, WhseStk.WHMax_Lvl FROM         StkItem INNER JOIN " & _
                " _etblPriceListPrices ON StkItem.StockLink = _etblPriceListPrices.iStockID INNER JOIN " & _
                " WhseStk ON StkItem.StockLink = WhseStk.WHStockLink LEFT OUTER JOIN " & _
                " GrpTbl ON StkItem.ItemGroup = GrpTbl.StGroup LEFT OUTER JOIN " & _
                " _etblUnits ON StkItem.iUOMStockingUnitID = _etblUnits.idUnits " & _
                " WHERE(StkItem.ServiceItem = 0) and StkItem.StockLink  = " & e.Cell.Value & " And _etblPriceListPrices.iPriceListNameID = 1 ORDER BY StkItem.Code "


                Dim objSQL As New clsSqlConn
                With objSQL

                    Dim DS1 = New DataSet
                    DS1 = .Get_Data_Sql(SQL)

                    Dim Dr1 As DataRow

                    If DS1.Tables(0).Rows.Count > 0 Then

                        For Each Dr1 In DS1.Tables(0).Rows
                            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            'sCode = ugR.Cells("Code").Value
                            'sDescription_1 = ugR.Cells("Description_1").Value
                            'bSerialItem = ugR.Cells("SerialItem").Value
                            'bWhseItem = ugR.Cells("WhseItem").Value

                            'If ugR.Cells("iItemCostingMethod").Value = 0 Then
                            '    fCostPrice = ugR.Cells("AveUCst").Value
                            'ElseIf ugR.Cells("iItemCostingMethod").Value = 1 Then
                            '    fCostPrice = ugR.Cells("LatUCst").Value
                            'End If

                            'iUnit = IIf(IsDBNull(ugR.Cells("iUOMStockingUnitID").Value) = True, 0, ugR.Cells("iUOMStockingUnitID").Value)
                            'iUnitCate = IIf(IsDBNull(ugR.Cells("iUnitCategoryID").Value) = True, 0, ugR.Cells("iUnitCategoryID").Value)
                            'fCostMargine = IIf(IsDBNull(ugR.Cells("fCostMargine").Value) = True, 0, ugR.Cells("fCostMargine").Value)

                            'e.Cell.Row.Cells("Line").Value = ugR2.Index + 1
                            'e.Cell.Row.Cells("Warehouse").Value = Dr("WhseItem")
                            'e.Cell.Row.Cells("StockID").Value = iStockLink
                            'e.Cell.Row.Cells("Code").Value = Dr("WhseItem")
                            e.Cell.Row.Cells("Description_1").Value = Dr1("Description_1")
                            e.Cell.Row.Cells("Description_2").Value = Dr1("Description_2")
                            e.Cell.Row.Cells("IsLot").Value = Dr1.Item("LotItem")
                            e.Cell.Row.Cells("IsWH").Value = Dr1.Item("WhseItem")






                            If e.Cell.Row.Cells("IsWH").Value = True Then
                                e.Cell.Row.Cells("Warehouse").Activation = Activation.AllowEdit
                            Else
                                e.Cell.Row.Cells("Warehouse").Activation = Activation.Disabled
                            End If


                            'to get correct ware house for ibt request
                            If sSQLSrvDataBase = "dbKelaniya_new" Then
                                getmstWH()
                                e.Cell.Row.Cells("Warehouse").Value = intMstWH
                            Else
                                Dim agent As New Agent(iAgent)

                                e.Cell.Row.Cells("Warehouse").Value = agent.Description
                            End If



                            e.Cell.Row.Cells("TaxType").Value = 1

                            For Each ugR1 As UltraGridRow In DDTaxType.Rows
                                If e.Cell.Row.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
                                    e.Cell.Row.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
                                End If
                            Next

                            e.Cell.Row.Cells("fCostMargine").Value = IIf(IsDBNull(Dr1.Item("fCostMargine")) = True, 0, Dr1.Item("fCostMargine"))
                            e.Cell.Row.Cells("fExclPrice").Value = IIf(IsDBNull(Dr1.Item("fExclPrice")) = True, 0, Dr1.Item("fExclPrice"))

                            If Dr1.Item("iItemCostingMethod") = 0 Then
                                e.Cell.Row.Cells("CostPrice").Value = Dr1.Item("AveUCst")
                            ElseIf Dr1.Item("iItemCostingMethod") = 1 Then
                                e.Cell.Row.Cells("CostPrice").Value = Dr1.Item("LatUCst")
                            End If

                        Next
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    End If
                End With
                '-------------------------------------------------------------




                '---------------------------------------------------------------
                'check branch epires items-----------------------------
                Try
                    'If IsNew = True Then
                    If sSQLSrvDataBase = "dbKelaniya_new" Then
                        Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & cmbGRVLoc.Text & "")
                    Else
                        Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & sSQLSrvDataBase & "")
                    End If

                    objSQL = New clsSqlConn

                    With objSQL
                        SQL = "SELECT     StkItem.Description_1, _etblLotTracking.cLotDescription, _etblLotTrackingQty.iWarehouseID " & _
                        " FROM         StkItem INNER JOIN   _etblLotTracking ON StkItem.StockLink = _etblLotTracking.iStockID INNER JOIN " & _
                        "   _etblLotTrackingQty ON _etblLotTracking.idLotTracking = _etblLotTrackingQty.iLotTrackingID " & _
                        " WHERE _etblLotTracking.iLotStatusID = 4 and StkItem.description_1 ='" & e.Cell.Row.Cells("description_1").Value & "' and StkItem.ItemActive = 1 AND iWarehouseID = " & clientWH & " AND _etblLotTrackingQty.fQtyOnHand > 0 "
                        CMD = New SqlCommand(SQL, Con2)
                        CMD.CommandType = CommandType.Text
                        DA = New SqlDataAdapter(CMD)
                        DS = New DataSet
                        Con2.Open()
                        DA.Fill(DS)
                        Con2.Close()
                        If DS.Tables(0).Rows.Count > 0 Then
                            MsgBox("Expired Lot found in branch", MsgBoxStyle.Information)
                            e.Cell.Row.Cells("Description_1").Value = 0
                            e.Cell.Row.Cells("Code").Value = 0
                        End If
                    End With
                Catch ex As Exception
                    'MsgBox("my error 7463")
                    Exit Sub
                Finally
                    If Con1.State = ConnectionState.Open Then Con1.Close()
                End Try

                'End If
                '---------------------------------------------------------------

                '--------------------------------------------------------------








                'check max reorder level-----------------------------
                Try
                    'If IsNew = True Then
                    If sSQLSrvDataBase = "dbKelaniya_new" Then
                        Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & cmbGRVLoc.Text & "")
                    Else
                        Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & sSQLSrvDataBase & "")
                    End If

                    objSQL = New clsSqlConn

                    'Dim lvl As String = "Max_Lvl"
                    'If clientWH = 3 Then 'nawinna
                    '    lvl = "Max_Lvl"
                    'ElseIf cmbGRVLoc.Text = "dbNawinna_new" And clientWH = 4 Then 'negambo
                    '    lvl = "WhseStk.WHMax_Lvl"
                    'ElseIf cmbGRVLoc.Text = "dbKurunegala_new" And clientWH = 4 Then 'kurunegala
                    '    lvl = "Max_Lvl"
                    'ElseIf clientWH = 2 Then 'panchkawatta
                    '    lvl = "Max_Lvl"
                    'ElseIf cmbGRVLoc.Text = "dbNawinna_new" And clientWH = 18 Then 'Kiribathgoda
                    '    lvl = "WhseStk.WHMax_Lvl"
                    'ElseIf clientWH = 1 Then 'matara
                    '    lvl = "Max_Lvl"
                    'End If

                    With objSQL
                        'SQL = "SELECT " & lvl & " FROM StkItem WHERE description_1 ='" & e.Cell.Row.Cells("description_1").Value & "' and ItemActive = 1 "
                        SQL = "SELECT    WhseStk.WHMax_Lvl   FROM         StkItem INNER JOIN  WhseStk ON StkItem.StockLink = WhseStk.WHStockLink WHERE StkItem.description_1 ='" & e.Cell.Row.Cells("description_1").Value & "' and StkItem.ItemActive = 1 AND WhseStk.WHWhseID = " & clientWH & " "
                        CMD = New SqlCommand(SQL, Con2)
                        CMD.CommandType = CommandType.Text
                        DA = New SqlDataAdapter(CMD)
                        DS = New DataSet
                        Con2.Open()
                        DA.Fill(DS)
                        Con2.Close()
                        If DS.Tables(0).Rows.Count > 0 Then
                            e.Cell.Row.Cells("WHMax_Lvl").Value = DS.Tables(0).Rows(0)(0)
                        End If
                    End With
                Catch ex As Exception
                    MsgBox("No Maximum level in branch for item: '" & e.Cell.Row.Cells("description_1").Value & "'")
                    Exit Sub
                Finally
                    If Con1.State = ConnectionState.Open Then Con1.Close()
                End Try

                'End If
                '---------------------------------------------------------------
                e.Cell.Row.Cells("AvailableQty").Value = Get_Available_Qty("SELECT WHQtyOnHand FROM WhseStk WHERE WHStockLink =" & e.Cell.Row.Cells("StockID").Value & " AND WHWhseID =" & e.Cell.Row.Cells("Warehouse").Value & "")
                If sSQLSrvDataBase = "dbKelaniya_new" Then
                    e.Cell.Row.Cells("BranchQty").Value = Get_Branch_Qty("SELECT     WhseStk.WHQtyOnHand + WhseStk.WHQtyOnPO   FROM         WhseStk INNER JOIN WhseMst ON WhseStk.WHWhseID = WhseMst.WhseLink INNER JOIN StkItem ON WhseStk.WHStockLink = StkItem.StockLink  WHERE cSimpleCode ='" & e.Cell.Row.Cells("cSimpleCode").Value & "' AND WhseLink = " & clientWH & " ")
                Else
                    e.Cell.Row.Cells("BranchQty").Value = Get_Branch_Qty("SELECT     WhseStk.WHQtyOnHand  FROM         WhseStk INNER JOIN WhseMst ON WhseStk.WHWhseID = WhseMst.WhseLink INNER JOIN StkItem ON WhseStk.WHStockLink = StkItem.StockLink  WHERE cSimpleCode ='" & e.Cell.Row.Cells("cSimpleCode").Value & "' AND DefaultWhse = 1 ")
                End If

                'e.Cell.Row.Cells("BranchQty").Value = Get_Branch_Qty("SELECT     WhseStk.WHQtyOnHand + StkItem.QtyOnPO  FROM         WhseStk INNER JOIN WhseMst ON WhseStk.WHWhseID = WhseMst.WhseLink INNER JOIN StkItem ON WhseStk.WHStockLink = StkItem.StockLink  WHERE cSimpleCode ='" & e.Cell.Row.Cells("cSimpleCode").Value & "' AND DefaultWhse = 1 ")

                bValChange = True

            ElseIf e.Cell.Column.Key = "Quantity" Or e.Cell.Column.Key = "Price_Excl" Or _
                e.Cell.Column.Key = "TaxRate" Or e.Cell.Column.Key = "Discount" Or e.Cell.Column.Key = "fUnitCostMargin" Or e.Cell.Column.Key = "CostPrice" Then
                If bValChange = True Then
                    With e.Cell.Row
                        bValChange = False
                        .Cells("ConfirmQty").Value = .Cells("Quantity").Value
                        '.Cells("fUnitCostMargine").Value = MarginAmount(.Cells("fCostMargine").Value, .Cells("fExclPrice").Value)
                        .Cells("fUnitCostMargine").Value = 0
                        .Cells("Price_Excl").Value = CalPriceExcl(.Cells("fUnitCostMargine").Value, .Cells("fExclPrice").Value)
                        .Cells("Price_Incl").Value = PriceIncl(.Cells("TaxRate").Value, .Cells("Price_Excl").Value)
                        .Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Cells("Quantity").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                        .Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Cells("Quantity").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                        .Cells("OrderTax").Value = LineTax(bIsInclusive, .Cells("Quantity").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                        .Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                        .Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                        .Cells("LineTax").Value = LineTax(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                        .Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                        .Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                        bValChange = True
                    End With
                End If
            End If

        Catch ex As Exception

            MsgBox(ex.Message)
            Exit Sub

        Finally

            If Con1.State = ConnectionState.Open Then Con1.Close()

        End Try
        'if it is a serial item disable qty and confirm qty
        If e.Cell.Column.Key = "Description_1" Then
            'If e.Cell.Row.Cells("IsLot").Value = True Then
            '    e.Cell.Row.Cells("ConfirmQty").Activation = Activation.Disabled
            '    e.Cell.Row.Cells("ConfirmQty").Appearance.BackColorDisabled = Color.Silver
            '    e.Cell.Row.Cells("Quantity").Activation = Activation.Disabled
            '    e.Cell.Row.Cells("Quantity").Appearance.BackColorDisabled = Color.Silver
            'Else
            '    e.Cell.Row.Cells("ConfirmQty").Activation = Activation.AllowEdit
            '    e.Cell.Row.Cells("ConfirmQty").Appearance.BackColorDisabled = Color.White
            '    e.Cell.Row.Cells("Quantity").Activation = Activation.AllowEdit
            '    e.Cell.Row.Cells("Quantity").Appearance.BackColorDisabled = Color.White
            'End If
        End If
    End Sub
    Private Sub UG_CellChange(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UG.CellChange
        Try
            If UG.ActiveCell.Column.Key = "Code" Then
                If DDStock.ActiveRow.Activated = True Then
                    UG.ActiveRow.Cells("StockID").Value = DDStock.ActiveRow.Cells("StockLink").Value
                    UG.ActiveRow.Cells("Code").Value = DDStock.ActiveRow.Cells("StockLink").Value
                    UG.ActiveRow.Cells("Description_1").Value = DDStock.ActiveRow.Cells("Description_1").Value
                    UG.ActiveRow.Cells("Description_2").Value = DDStock.ActiveRow.Cells("Description_2").Value
                    UG.ActiveRow.Cells("IsLot").Value = IIf(DDStock.ActiveRow.Cells("LotItem").Value = True, True, False)
                    UG.ActiveRow.Cells("IsWH").Value = IIf(DDStock.ActiveRow.Cells("WhseItem").Value = True, True, False)
                    UG.ActiveRow.Cells("Bin").Value = DDDescription.ActiveRow.Cells("Bin").Value

                    

                End If
            ElseIf UG.ActiveCell.Column.Key = "Description_1" Then
                If DDDescription.ActiveRow.Activated = True Then
                    'UG.ActiveRow.Cells("Description_1").CellDisplayStyle.
                    UG.ActiveRow.Cells("StockID").Value = DDDescription.ActiveRow.Cells("StockLink").Value
                    UG.ActiveRow.Cells("Code").Value = DDDescription.ActiveRow.Cells("StockLink").Value
                    'UG.ActiveRow.Cells("Code").Value = 0
                    UG.ActiveRow.Cells("Quantity").Value = 0
                    UG.ActiveRow.Cells("Description_1").Value = DDDescription.ActiveRow.Cells("Description_1").Value
                    UG.ActiveRow.Cells("Description_2").Value = DDDescription.ActiveRow.Cells("Description_2").Value
                    UG.ActiveRow.Cells("IsLot").Value = IIf(DDDescription.ActiveRow.Cells("LotItem").Value = True, True, False)
                    UG.ActiveRow.Cells("IsWH").Value = IIf(DDDescription.ActiveRow.Cells("WhseItem").Value = True, True, False)
                    UG.ActiveRow.Cells("Bin").Value = DDDescription.ActiveRow.Cells("Bin").Value

                    UG.ActiveRow.Cells("cSimpleCode").Value = DDDescription.ActiveRow.Cells("cSimpleCode").Value
                    If sSQLSrvDataBase = "dbKelaniya_new" Then
                        e.Cell.Row.Cells("BranchQty").Value = Get_Branch_Qty("SELECT     WhseStk.WHQtyOnHand + WhseStk.WHQtyOnPO   FROM         WhseStk INNER JOIN WhseMst ON WhseStk.WHWhseID = WhseMst.WhseLink INNER JOIN StkItem ON WhseStk.WHStockLink = StkItem.StockLink  WHERE cSimpleCode ='" & e.Cell.Row.Cells("cSimpleCode").Value & "' AND WhseLink = " & clientWH & " ")
                    Else
                        e.Cell.Row.Cells("BranchQty").Value = Get_Branch_Qty("SELECT     WhseStk.WHQtyOnHand                    FROM         WhseStk INNER JOIN WhseMst ON WhseStk.WHWhseID = WhseMst.WhseLink INNER JOIN StkItem ON WhseStk.WHStockLink = StkItem.StockLink  WHERE cSimpleCode ='" & e.Cell.Row.Cells("cSimpleCode").Value & "' AND DefaultWhse = 1 ")
                    End If


                    If (sSQLSrvDataBase = "dbKelaniya_new") Then 'loading IBT request to branches
                        If CInt(IIf(IsDBNull(UG.ActiveRow.Cells("WHMax_Lvl").Value), 0, UG.ActiveRow.Cells("WHMax_Lvl").Value)) - CInt(UG.ActiveRow.Cells("BranchQty").Value) < 0 Then
                            UG.ActiveRow.Cells("Quantity").Value = 0
                        Else
                            UG.ActiveRow.Cells("Quantity").Value = CInt(IIf(IsDBNull(UG.ActiveRow.Cells("WHMax_Lvl").Value), 0, UG.ActiveRow.Cells("WHMax_Lvl").Value)) - CInt(UG.ActiveRow.Cells("BranchQty").Value)
                        End If

                        If CInt(UG.ActiveRow.Cells("AvailableQty").Value) <= UG.ActiveRow.Cells("Quantity").Value Then
                            UG.ActiveRow.Cells("Quantity").Value = CInt(UG.ActiveRow.Cells("AvailableQty").Value)
                            'Else
                            '    Row.Cells("Quantity").Value = Dr.Item("fQuantity")
                        End If

                    Else 'loading automatic PO from branches for IBT request
                        If CInt(IIf(IsDBNull(UG.ActiveRow.Cells("WHMax_Lvl").Value), 0, UG.ActiveRow.Cells("WHMax_Lvl").Value)) - CInt(UG.ActiveRow.Cells("AvailableQty").Value) < 0 Then
                            UG.ActiveRow.Cells("Quantity").Value = 0
                        Else
                            UG.ActiveRow.Cells("Quantity").Value = CInt(IIf(IsDBNull(UG.ActiveRow.Cells("WHMax_Lvl").Value), 0, UG.ActiveRow.Cells("WHMax_Lvl").Value)) - CInt(UG.ActiveRow.Cells("AvailableQty").Value)
                        End If

                        If CInt(UG.ActiveRow.Cells("BranchQty").Value) <= UG.ActiveRow.Cells("Quantity").Value Then
                            UG.ActiveRow.Cells("Quantity").Value = CInt(UG.ActiveRow.Cells("BranchQty").Value)
                            'Else
                            '    Row.Cells("Quantity").Value = Dr.Item("fQuantity")
                        End If

                        'UG.DisplayLayout.Bands(0).Columns("DiscountA").CellActivation = Activation.Disabled
                        'Row.Cells("Quantity").Column.CellActivation = Activation.Disabled
                        'Row.Cells("Quantity").Column.CellClickAction = CellClickAction.CellSelect
                    End If


                    UG.ActiveRow.Cells("lot").Value = 0

                End If
            ElseIf UG.ActiveCell.Column.Key = "TaxType" Then
                If DDTaxType.ActiveRow.Activated = True Then
                    UG.ActiveRow.Cells("TaxType").Value = DDTaxType.ActiveRow.Cells("Code").Value
                    UG.ActiveRow.Cells("TaxRate").Value = DDTaxType.ActiveRow.Cells("TaxRate").Value
                End If
            ElseIf UG.ActiveCell.Column.Key = "Warehouse" Then  'WareHouse
                If DDWH.ActiveRow.Activated = True Then
                    UG.ActiveRow.Cells("Warehouse").Value = DDWH.ActiveRow.Cells("WhseLink").Value
                    UG.ActiveRow.Cells("AvailableQty").Value = Get_Available_Qty("SELECT WHQtyOnHand FROM WhseStk WHERE WHStockLink =" & e.Cell.Row.Cells("Code").Value & " AND WHWhseID =" & e.Cell.Row.Cells("Warehouse").Value & "")
                End If
            End If
        Catch ex As Exception
            'MsgBox(Err.Description)
            Exit Sub
        End Try
    End Sub
    Private Sub UG_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles UG.KeyDown
        Try

            'If IsDBNull(UG.ActiveCell.Column.Index) Then Exit Sub
            If UG.ActiveCell.Column.Index = 13 Then
                'If e.KeyValue = 9 Then
                '    If UG.ActiveRow.Cells("Code").Value <> 0 Then
                '        If UG.ActiveRow.HasNextSibling = True Then Exit Sub
                '        UG.ActiveRow.Cells("Line").Value = UG.ActiveRow.Index + 1
                '        R = UG.DisplayLayout.Bands(0).AddNew
                '        R.Cells("TaxType").Value = 1
                '        For Each ugR1 As UltraGridRow In DDTaxType.Rows
                '            If R.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
                '                R.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
                '            End If
                '        Next
                '        R.Cells(1).Activated = True
                '        UG.PerformAction(UltraGridAction.EnterEditMode, True, True)
                '    Else
                '        MsgBox("Item Code can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
                '        Exit Sub
                '    End If
                'End If
            ElseIf UG.ActiveCell.Column.Index = 2 Then
                If e.KeyValue = 13 Then
                    If DDDescription.ActiveRow.Activated = True Then
                        UG.ActiveRow.Cells("StockID").Value = DDDescription.ActiveRow.Cells("StockLink").Value
                        UG.ActiveRow.Cells("Line").Value = UG.ActiveRow.Index + 1

                        UG.ActiveRow.Cells("Code").Value = DDDescription.ActiveRow.Cells("StockLink").Value
                        UG.ActiveRow.Cells("Description_1").Value = DDDescription.ActiveRow.Cells("Description_1").Value
                        UG.ActiveRow.Cells("Description_2").Value = DDDescription.ActiveRow.Cells("Description_2").Value
                        UG.ActiveRow.Cells("iUnit").Value = DDDescription.ActiveRow.Cells("iUOMStockingUnitID").Value
                        UG.ActiveRow.Cells("iUnitCate").Value = DDDescription.ActiveRow.Cells("iUnitCategoryID").Value

                        UG.ActiveRow.Cells("cSimpleCode").Value = DDDescription.ActiveRow.Cells("cSimpleCode").Value
                        UG.ActiveRow.Cells("Bin").Value = DDDescription.ActiveRow.Cells("Bin").Value

                    End If

                ElseIf e.KeyCode = Keys.Back Then

                    UG.ActiveRow.Cells("Lot").Value = 0
                    UG.ActiveRow.Cells("Quantity").Value = 0
                End If
            ElseIf e.KeyCode = Keys.Tab Then
                If UG.ActiveCell.Column.Key = "Lot" Then
                    If UG.ActiveRow.Cells("Code").Value <> 0 Then
                        If UG.ActiveRow.HasNextSibling = True Then Exit Sub
                        If IsNew = True Then

                            Dim ugR As UltraGridRow
                            ugR = UG.DisplayLayout.Bands(0).AddNew
                            ugR.Cells("Line").Value = ugR.Index + 1
                            'ugR.Cells("Line").Selected = True

                            ugR.Cells("Warehouse").Value = iWH

                            ugR.Cells("Description_1").Activate()

                            If iDocType = DocType.SalesOrder Then
                                ugR.Cells("TaxType").Value = 1
                                For Each ugR1 As UltraGridRow In DDTaxType.Rows
                                    If ugR.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
                                        ugR.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
                                    End If
                                Next
                            ElseIf iDocType = DocType.PurchaseOrder Then
                                ugR.Cells("TaxType").Value = 3
                                For Each ugR1 As UltraGridRow In DDTaxType.Rows
                                    If ugR.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
                                        ugR.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
                                    End If
                                Next
                            End If
                        End If
                        'ugR.Cells("IsLot").Activate()
                    Else
                        MsgBox("Item Code can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
                        Exit Sub
                    End If

                End If
            
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Public Function Get_NextLocTransferNo() As String
        SQL = "SELECT     LT_NextNo FROM SageOrderDef "
        Con1.ConnectionString = sConStr
        CMD = New SqlCommand(SQL, Con1)
        CMD.CommandType = CommandType.Text
        Con1.Open()
        sqlDR = CMD.ExecuteReader
        If sqlDR.HasRows = True Then
            While sqlDR.Read
                iLTNo = sqlDR("LT_NextNo")
            End While
        End If
        sqlDR.Close()
        Con1.Close()
        sLTNo = "LT" & Format(iLTNo, "00000")
        Return sLTNo

    End Function
    Public Function Get_Next_No(ByVal SQL As String) As String

        Dim iNextNo As Integer = 0
        Dim sPreFix As String = ""
        Dim iPadTo As Integer = 0
        Dim sReturn As String = ""

        CMD = New SqlCommand(SQL, Con1)
        CMD.CommandType = CommandType.Text

        Con1.Open()
        sqlDR = CMD.ExecuteReader
        If sqlDR.HasRows = True Then
            While sqlDR.Read
                iNextNo = sqlDR(0)
                sPreFix = sqlDR(1)
                iPadTo = sqlDR(2)
            End While
        End If
        sqlDR.Close()
        Con1.Close()
        Select Case iPadTo
            Case 1
                sReturn = sPreFix & Format(iNextNo, "0")
            Case 2
                sReturn = sPreFix & Format(iNextNo, "00")
            Case 3
                sReturn = sPreFix & Format(iNextNo, "000")
            Case 4
                sReturn = sPreFix & Format(iNextNo, "0000")
            Case 5
                sReturn = sPreFix & Format(iNextNo, "00000")
            Case 6
                sReturn = sPreFix & Format(iNextNo, "000000")
            Case 7
                sReturn = sPreFix & Format(iNextNo, "0000000")
            Case 8
                sReturn = sPreFix & Format(iNextNo, "00000000")
        End Select

        Return sReturn
    End Function
    Public Sub GET_NEXT_INVOICE_NO()
        Try
            txtInNo.Text = Get_NextLocTransferNo()
        Catch ex As Exception
            Exit Sub
        Finally
            If Con1.State = ConnectionState.Open Then
                Con1.Close()
            End If
        End Try

    End Sub
    Public Sub Discard()

        Try

            iSONo = 0
            tsbUpdateSO.Enabled = True
            tsbValidate.Enabled = False
            cmbCustomer.ResetText()
            txtDeliveryAdd.ResetText()
            txtPostalAdd.ResetText()
            cmbOrderStatus.ResetText()
            cmbPriority.ResetText()
            cmbProject.ResetText()
            cmbSalesOpp.ResetText()
            txtOrdNo.ResetText()
            txtDelNote.ResetText()
            txtExtOrder.ResetText()

            cmbSaleRep.ResetText()

            DeleteRows()

        Catch ex As Exception

        End Try

    End Sub

    Public Sub Delete_Row()
        Dim R As UltraGridRow
        For Each R In UG.Rows
            If R.Cells("Description_1").Text = "" Then
                R.Delete(False)
                Exit Sub
            End If
        Next
    End Sub
    Public Sub ResetRow()
        'Dim R As UltraGridRow
        'If UG.Rows.Count > 0 Then
        '    For Each R In UG.Rows

        '        R.Delete(False)


        '    Next
        'End If
    End Sub
    Public Sub Validate_Line()
        Dim R As UltraGridRow
        For Each R In UG.Rows
            'If R.Cells("Description_1").Text = "" Or R.Cells("Lot").Text = "" Or R.Cells("Quantity").Text = 0 Then
            If R.Cells("Description_1").Text = "" Or R.Cells("Lot").Text = "" Then
                MessageBox.Show("Please fill all data", "Evolution AddOn")
                Exit Sub
            End If
        Next
    End Sub

    Private Sub UG_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles UG.Leave
        'Delete_Row()
        'Validate_Line()
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Me.Dispose()
    End Sub

    Private Sub cmbCustomer_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCustomer.ValueChanged
        Try
            'DeleteRows()
            'R = UG.DisplayLayout.Bands(0).AddNew
            'R.Cells("TaxType").Value = 1
            'For Each ugR1 As UltraGridRow In DDTaxType.Rows
            '    If R.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
            '        bValChange = False
            '        R.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
            '        bValChange = True
            '        Exit For
            '    End If
            'Next
            If cmbCustomer.ActiveRow.Activated = True Then
                sAccount = cmbCustomer.ActiveRow.Cells("Name").Value
                txtPostalAdd.Text = cmbCustomer.ActiveRow.Cells("Post1").Value & vbLf & cmbCustomer.ActiveRow.Cells("Post2").Value & vbLf & cmbCustomer.ActiveRow.Cells("Post3").Value
                sPAdd1 = cmbCustomer.ActiveRow.Cells("Post1").Value
                sPAdd2 = cmbCustomer.ActiveRow.Cells("Post2").Value
                sPAdd3 = cmbCustomer.ActiveRow.Cells("Post3").Value
                txtDeliveryAdd.Text = cmbCustomer.ActiveRow.Cells("Physical1").Value & vbLf & cmbCustomer.ActiveRow.Cells("Physical2").Value & vbLf & cmbCustomer.ActiveRow.Cells("Physical3").Value
                sAdd1 = cmbCustomer.ActiveRow.Cells("Physical1").Value
                sAdd2 = cmbCustomer.ActiveRow.Cells("Physical2").Value
                sAdd3 = cmbCustomer.ActiveRow.Cells("Physical3").Value
                dblMargin = cmbCustomer.ActiveRow.Cells("Discount").Value
                cmbGRVLoc.Text = cmbCustomer.ActiveRow.Cells("cAccDescription").Value
                'POLocation = cmbCustomer.ActiveRow.Cells("DCLink").Value
                clientWH = cmbCustomer.ActiveRow.Cells("cIDNumber").Value
            End If
        Catch ex As Exception
            txtPostalAdd.Text = Nothing
            txtDeliveryAdd.Text = Nothing
            Exit Sub
        End Try

        ResetRow()

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
    Public Function GET_MAX_NO(ByVal SQL As String) As Integer
        Try
            Dim objNo As Object
            CMD = New SqlCommand(SQL, Con1, Trans1)
            CMD.CommandType = CommandType.Text
            objNo = CMD.ExecuteScalar
            If IsDBNull(objNo) = True Then
                Return 1
            Else
                Return CInt(objNo)
            End If
        Catch ex As Exception
            Return -1
        End Try
    End Function
    Public Function SaveSalesOrder() As Integer
        Try
            DatabaseContext.BeginTran()
            Dim SalesOrder As New SalesOrder
            Dim SalesOrderDetail As OrderDetail
            Dim InventoryItem As InventoryItem
            Dim WH As Warehouse

            Dim Customer As New Customer(CInt(cmbCustomer.Value))
            Dim Representative As New SalesRepresentative(CInt(cmbSaleRep.Value))

            SalesOrder.OrderNo = CStr(txtInNo.Text)
            SalesOrder.Account = Customer
            SalesOrder.InvoiceTo = Customer.PostalAddress
            SalesOrder.DeliverTo = Customer.PhysicalAddress
            SalesOrder.ExternalOrderNo = txtExtOrder.Text.ToString
            SalesOrder.InvoiceDate = Format(dtpInDate.Value, "dd/MM/yyyy")
            SalesOrder.DeliveryDate = Format(dtpDeliDate.Value, "dd/MM/yyyy")
            SalesOrder.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")
            SalesOrder.Representative = Representative
            Dim ugR As UltraGridRow
            For Each ugR In UG.Rows
                SalesOrderDetail = New OrderDetail
                InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))

                SalesOrderDetail.InventoryItem = InventoryItem
                SalesOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                SalesOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                SalesOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)
                WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                If ugR.Cells("IsWH").Value = True Then
                    SalesOrderDetail.Warehouse = WH
                End If
                SalesOrderDetail.Description = CStr(ugR.Cells("Description_2").Value)
                SalesOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                SalesOrder.Detail.Add(SalesOrderDetail)
            Next
            SalesOrder.Process()
            MsgBox(SalesOrder.OrderNo, MsgBoxStyle.Information, "Pastel Evolution")
            DatabaseContext.CommitTran()
            Return SalesOrder.ID
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        Catch ex As Exception
            DatabaseContext.RollbackTran()
            Return 0
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        Finally
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        End Try
    End Function
    Private Function checkInactiveItems() As Integer
        Dim items As String = ""
        Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= '" & cmbGRVLoc.Text & "' ")

        For Each ugR In UG.Rows
            SQL = " SELECT cSimpleCode, ItemActive FROM StkItem where cSimpleCode = '" & ugR.Cells("cSimpleCode").Value & "' "
            CMD = New SqlCommand(SQL, Con2)
            CMD.CommandType = CommandType.Text
            DA = New SqlDataAdapter(CMD)
            DS1 = New DataSet
            Con2.Open()
            DA.Fill(DS1)
            Con2.Close()

            If DS1.Tables(0).Rows(0)(1) = 0 Then
                items = items + ":" + DS1.Tables(0).Rows(0)(0)
            End If
        Next
        If items.Length > 0 Then
            MessageBox.Show("Following items are inactive in branch" & items, "Pastel Evolution", MessageBoxButtons.OK)
            Return 1
        Else
            Return 0
        End If
        items = ""
    End Function
    Private Sub tsbSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSave.Click


        Dim R As UltraGridRow
        For Each R In UG.Rows
            'If R.Cells("Description_1").Text = "" Or R.Cells("Lot").Text = "" Or R.Cells("Quantity").Text = 0 Then
            If R.Cells("Description_1").Text = "" Then
                MessageBox.Show("Please fill all data", "Evolution AddOn")
                Exit Sub
            End If
        Next

        If checkInactiveItems() = 1 Then
            
            Exit Sub
        End If

        UG.PerformAction(UltraGridAction.CommitRow)

        UG_Leave(UG, e)

        Try

            Dim iInvoiceID As Integer = 0
            Dim sOrderNumber As String = ""
            Dim iCustomer As Integer = 0
            Dim iSupplier As Integer = 0
            Dim iIBTNo As Integer = 0
            Dim sError As String = ""
            Dim iToWH As Integer = 0
            ReDim lots(UG.Rows.Count)

            getDefaultwh(sError, iToWH)

            If sError <> "" Then
                Exit Sub
            End If

            'If sAgent = "Admin" Then
            '    If MsgBox("Do you want to process Sales Order", MsgBoxStyle.OkCancel, "Pastel Evolution") = True Then

            saveIBT(iSONo, True, iIBTNo)


            If iIBTNo = 0 Then 'if sales order didnt process
                Exit Sub
            End If
            '    End If
            'End If
            saveUPIBT(iSONo, True, iIBTNo)

            Dim InventoryItem As InventoryItem
            Dim WH As Warehouse

            'to find the mother database
            Dim MDB As String = ""
            If sSQLSrvDataBase = "dbNawinna_new" Then
                MDB = "UDA004"
            ElseIf sSQLSrvDataBase = "dbKurunegala_new" Then
                MDB = "UDA002"
            ElseIf sSQLSrvDataBase = "dbKiribathgoda_new" Then
                MDB = "UDA003"
            ElseIf sSQLSrvDataBase = "dbUdawatta_new" Then
                MDB = "UDA001"
            ElseIf sSQLSrvDataBase = "dbKelaniya_new" Then
                MDB = "UDA008"
            End If


            If cmbGRVLoc.Text.Trim = "dbMathara" Then
                MDB = "UDA001"
            End If

            If sSQLSrvDataBase = "dbUdawatta_test1" Then
                MDB = "UDA001"
            ElseIf sSQLSrvDataBase = "UDAWATTA_TEST2" Then
                MDB = "UDA002"
                'ElseIf sSQLSrvDataBase = "dbKiribathgoda_test" Then
                '    MDB = "UDA003"
                'ElseIf sSQLSrvDataBase = "UDAWATTA_TEST1" Then
                '    MDB = "UDA001"
            End If


            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

            DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
            DatabaseContext.SetLicense("DE09110022", "1428511")
            DatabaseContext.CreateConnection(sSQLSrvName, cmbGRVLoc.Text.Trim, sSQLSrvUserName, sSQLSrvPassword, False)
            'DatabaseContext.CreateConnection(sSQLSrvName, "UDAWATTA-TEST1", sSQLSrvUserName, sSQLSrvPassword, False)

            DatabaseContext.BeginTran()
            Dim PurchaseOrder As New PurchaseOrder
            Dim PurchaseOrderDetail As OrderDetail

            Dim Supplier As New Supplier(MDB)
            iSupplier = Supplier.ID
            PurchaseOrder.OrderNo = sOrderNumber
            PurchaseOrder.ExternalOrderNo = PurchaseOrder.OrderNo
            PurchaseOrder.Account = Supplier
            PurchaseOrder.InvoiceTo = Supplier.PostalAddress
            PurchaseOrder.DeliverTo = Supplier.PhysicalAddress
            PurchaseOrder.ExternalOrderNo = txtOrdNo.Text.ToString
            PurchaseOrder.InvoiceDate = Format(dtpInDate.Value, "dd/MM/yyyy")
            PurchaseOrder.DeliveryDate = Format(dtpDeliDate.Value, "dd/MM/yyyy")
            PurchaseOrder.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")
            PurchaseOrder.SupplierInvoiceNo = "" ' CStr(txtInNo.Text)


            'add orderstatus
            If clientWH = 3 Then 'nawinna
                Dim ordstatus As New OrderStatus("NAWINNA")
                PurchaseOrder.OrderStatus = ordstatus
            ElseIf cmbGRVLoc.Text = "dbNawinna_new" And clientWH = 4 Then 'negambo
                Dim ordstatus As New OrderStatus("NEGOMBO")
                PurchaseOrder.OrderStatus = ordstatus
            ElseIf cmbGRVLoc.Text = "dbUdawatta_new" And clientWH = 4 Then 'kurunegala
                Dim ordstatus As New OrderStatus("KURUNEGALA")
                PurchaseOrder.OrderStatus = ordstatus
            ElseIf clientWH = 2 Then 'panchkawatta
                Dim ordstatus As New OrderStatus("PANCHIKAWATTA")
                PurchaseOrder.OrderStatus = ordstatus
            ElseIf cmbGRVLoc.Text = "dbNawinna_new" And clientWH = 18 Then 'Kiribathgoda
                Dim ordstatus As New OrderStatus("KIRIBATHGODA")
                PurchaseOrder.OrderStatus = ordstatus
            ElseIf cmbGRVLoc.Text = "dbUdawatta_new" And clientWH = 18 Then 'matara
                Dim ordstatus As New OrderStatus("MATHARA")
                PurchaseOrder.OrderStatus = ordstatus
            End If



            If bIsInclusive = False Then
                PurchaseOrder.TaxMode = TaxMode.Exclusive
            ElseIf bIsInclusive = True Then
                PurchaseOrder.TaxMode = TaxMode.Inclusive
            End If

            Dim LotN As New Lot()
            For Each ugR In UG.Rows
                If CDbl(ugR.Cells("Quantity").Value) > 0 Then
                    PurchaseOrderDetail = New OrderDetail
                    InventoryItem = New InventoryItem(CStr(ugR.Cells("Code").Text))
                    PurchaseOrderDetail.InventoryItem = InventoryItem
                    If ugR.Cells("IsLot").Value = True Then
                        'If ugR.ChildBands.HasChildRows = False Then
                        '    MsgBox("Enter Lot Numbers", MsgBoxStyle.Exclamation, "Pastel Evolution")
                        '    DatabaseContext.RollbackTran()
                        '    If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
                        '    If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
                        '    Exit Sub
                        'End If
                        'Dim ugR2 As UltraGridRow
                        'For Each ugR2 In ugR.ChildBands(0).Rows
                        '    PurchaseOrderDetail.SerialNumbers.Add(CStr(ugR2.Cells("SerialNumber").Value))
                        'Next
                    End If



                    PurchaseOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                    If bIsInclusive = False Then
                        PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                        PurchaseOrderDetail.TaxMode = TaxMode.Exclusive
                        PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                    ElseIf bIsInclusive = True Then
                        If CDbl(ugR.Cells("Price_Incl").Value) = 0 Then
                            PurchaseOrderDetail.UnitSellingPrice = Get_Value(" SELECT fInclPrice FROM _etblPriceListPrices where iPriceListNameID = 1 and iStockID = " & CStr(ugR.Cells("StockID").Text) & " ")
                        Else
                            PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                        End If

                        PurchaseOrderDetail.TaxMode = TaxMode.Inclusive
                        PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                    End If


                    If IsDBNull(ugR.Cells("CostPrice").Value) Then
                        'PurchaseOrderDetail.UnitCostPrice = CDbl(ugR.Cells("CostPrice").Value)
                    Else
                        PurchaseOrderDetail.UnitCostPrice = CDbl(ugR.Cells("CostPrice").Value)
                    End If

                    PurchaseOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)

                    'warehouse
                    If ugR.Cells("IsWH").Value = True Then


                        WH = New Warehouse(clientWH)


                        PurchaseOrderDetail.Warehouse = WH


                    End If
                    '            Catch ex As Exception
                    'End Try

                    PurchaseOrderDetail.Description = CStr(ugR.Cells("Description_1").Value)
                    'PurchaseOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                    'PurchaseOrderDetail.LotID = CStr(ugR.Cells("Lot").Value)

                    If ec.Text <> Nothing Then
                        sSONo = ec.Text
                    End If


                    SQL = " select cLotDescription from _etblLotTracking where idLotTracking  = '" & ugR.Cells("Lot").Value & "' "
                    Dim objSQL As New clsSqlConn
                    With objSQL
                        Dim DS1 = New DataSet
                        DS1 = .Get_Data_Sql(SQL)
                        If DS1.Tables(0).Rows.Count > 0 Then
                            LotN = New Lot()
                            'sSONo = "KEL13578"
                            If InStr(DS1.Tables(0).Rows(0)(0).ToString(), "-") = 0 Then 'IF LOT NUMBER IS 001
                                LotN.Code = "K" & sSONo.Substring(3) & "-" & CStr(ugR.Cells("Description_1").Value)
                            Else
                                If cmbGRVLoc.Text = "dbKelaniya_new" Then 'if isuing to kalaniya database
                                    LotN.Code = DS1.Tables(0).Rows(0)(0).ToString()
                                Else
                                    'LotN.Code = sSONo.Substring(3) & "-" & DS1.Tables(0).Rows(0)(0).ToString().Substring(0, InStr(DS1.Tables(0).Rows(0)(0).ToString(), "-") - 1)
                                    'LotN.Code = "K" & sSONo.Substring(3) & "-" & DS1.Tables(0).Rows(0)(0).ToString().Substring(InStr(DS1.Tables(0).Rows(0)(0).ToString(), "-"))
                                    LotN.Code = "K" & sSONo.Substring(3) & "-" & DS1.Tables(0).Rows(0)(0).ToString() 'with kelaniya lot number
                                End If
                            End If
                            LotN.ExpiryDate = DateAdd(DateInterval.Day, 60, Date.Now)
                        End If
                    End With

                    PurchaseOrderDetail.Lot = LotN

                    PurchaseOrder.Detail.Add(PurchaseOrderDetail)
                End If
            Next

            PurchaseOrder.Save()

            DatabaseContext.CommitTran()

            'MemorizePOStock(PurchaseOrder.ID)

            PONo = PurchaseOrder.OrderNo.ToString()
            MsgBox(PurchaseOrder.OrderNo, MsgBoxStyle.Information, "Pastel Evolution")





            If MsgBox("Do you want to print IBT now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
                Dim objRep As New ReportDocument
                Dim s As String = Application.StartupPath

                'If iDocType_1 = 10 Then
                objRep.Load(Application.StartupPath & "\IBT.rpt")
                'ElseIf iDocType_1 = 8 Then 
                '    objRep.Load(Application.StartupPath & "\IBT_R.rpt")
                'End If




                Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= '" & cmbGRVLoc.Text & "' ")
                SQL = " SELECT OrderNum, DocState  from InvNum where ExtOrderNum = '" & sSONo & "' "
                CMD = New SqlCommand(SQL, Con2)
                CMD.CommandType = CommandType.Text
                DA = New SqlDataAdapter(CMD)
                DS1 = New DataSet
                Con2.Open()
                DA.Fill(DS1)
                Con2.Close()

                If DS1.Tables(0).Rows.Count > 0 Then
                    frmPrintPreview.vInvNo = DS1.Tables(0).Rows(0)(0)
                    If DS1.Tables(0).Rows(0)(1) = 4 Then
                        frmPrintPreview.vPState = "Processed"
                    Else
                        frmPrintPreview.vPState = "Unprocessed"
                    End If

                Else
                    frmPrintPreview.vInvNo = " "
                    frmPrintPreview.vPState = " "
                End If

                frmPrintPreview.vSelectionFormula = "{InvNum.AutoIndex}= " & iSONo & ""

                iRepoID = 2

                frmPrintPreview.ShowDialog()
                iRepoID = 0

                'Exit Sub


                '----------------------------Print to differnt floors-------------------------
                If sSQLSrvDataBase = "dbKelaniya_new" Then

                    Dim report As New ReportDocument

                    report.PrintOptions.PrinterName = "EPSON LQ-310 ESC/P2 WH"
                    report.Load(Application.StartupPath & "\F1.rpt", OpenReportMethod.OpenReportByDefault)

                    report.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iSONo & ""
                    ApplyLoginToTable(report)

                    report.PrintToPrinter(1, False, 0, 0)


                    '=========================================================================================

                    report = New ReportDocument

                    report.PrintOptions.PrinterName = "F2"
                    report.Load(Application.StartupPath & "\F2.rpt", OpenReportMethod.OpenReportByDefault)

                    report.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iSONo & ""
                    ApplyLoginToTable(report)

                    report.PrintToPrinter(1, False, 0, 0)

                    '==========================================================================================

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    'objRep.Load(Application.StartupPath & "\F3.rpt")
                    'objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iSONo & ""
                    'ApplyLoginToTable(objRep)
                    ''If MsgBox("Do you want to preview the sales order", MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.No Then

                    'doctoprint = New System.Drawing.Printing.PrintDocument()
                    'doctoprint.PrinterSettings.PrinterName = "F3"
                    'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    '    Dim rawKind As Integer
                    '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Inv" Or doctoprint.PrinterSettings.PaperSizes(i).PaperName = "INV" Then
                    '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                    '        objRep.PrintOptions.PaperSize = rawKind
                    '        objRep.PrintToPrinter(1, False, 0, 0)
                    '        Exit For
                    '    End If
                    'Next

                    'objRep.Load(Application.StartupPath & "\F4.rpt")
                    'objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iSONo & ""
                    'ApplyLoginToTable(objRep)
                    ''If MsgBox("Do you want to preview the sales order", MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.No Then

                    'doctoprint = New System.Drawing.Printing.PrintDocument()
                    'doctoprint.PrinterSettings.PrinterName = "F4"
                    'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    '    Dim rawKind As Integer
                    '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Inv" Or doctoprint.PrinterSettings.PaperSizes(i).PaperName = "INV" Then
                    '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                    '        objRep.PrintOptions.PaperSize = rawKind
                    '        objRep.PrintToPrinter(1, False, 0, 0)
                    '        Exit For
                    '    End If
                    'Next

                End If
            End If






            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

            'saveIBT(iSONo, True, iIBTNo)

            Call Discard()

        Catch ex As Exception
            'Trans1.Rollback()
            'If Con1.State = ConnectionState.Open Then Con1.Close()
            'Trans2.Rollback()
            'If Con2.State = ConnectionState.Open Then Con2.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Pastel Evolution")
            Exit Sub

        Finally

            'If Con1.State = ConnectionState.Open Then Con1.Close()
            If Con2.State = ConnectionState.Open Then Con2.Close()

        End Try

    End Sub

    Private Sub tsbApplySet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbApplySet.Click
        UG.DisplayLayout.SaveAsXml(Application.StartupPath & "\Invoice.xml")
    End Sub
    Private Sub tsbOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbOpen.Click
        'frmDocmentList.ShowDialog()
        bValChange = False
        frmOpen.Get_Customer()
        frmOpen.iDocType = DocType.Invoice
        frmOpen.iDocType_1 = DocType.Invoice
        frmOpen.POLocation = cmbCustomer.Value
        frmOpen.ShowDialog()
        'frmOpen.TopMost = True
        Me.Close()
        bValChange = True
    End Sub
    Private Sub txtBarCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBarCode.KeyDown

        If e.KeyCode = Keys.Enter Then

            Delete_Row() ' Remove Blank lines from  the grid

            Dim sBarCode As String = txtBarCode.Text.Trim

            '''''''''''''''Check whether same item repeat scaning by the user
            '''''''''''''''Then exit from the procedure

            For Each ugR In UG.Rows
                If ugR.ChildBands.HasChildRows = True Then
                    Dim ugR1 As UltraGridRow
                    For Each ugR1 In ugR.ChildBands(0).Rows
                        If ugR1.Cells("SerialNumber").Value = sBarCode Then
                            txtBarCode.ResetText()
                            txtBarCode.Focus()
                            Exit Sub
                        End If
                    Next
                End If
            Next

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim objSQL As New clsSqlConn

            With objSQL
                SQL = "SELECT SerialNumber, SNStockLink FROM SerialMF WHERE SerialNumber ='" & CStr(sBarCode) & "' AND CurrentLoc = 1"
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                If DS.Tables(0).Rows.Count > 0 Then
                    Dim Dr As DataRow

                    '########### Loop is runing only as it returns only one record

                    For Each Dr In DS.Tables(0).Rows

                        Dim iStockLink As Integer = 0
                        Dim sCode As String = ""
                        Dim sDescription_1 As String = ""
                        Dim bSerialItem As Boolean = False
                        Dim bWhseItem As Boolean = False
                        Dim fCostPrice As Double = 0
                        Dim iUnit As Integer = 0
                        Dim iUnitCate As Integer = 0
                        Dim fCostMargine As Double = 0

                        Dim ugR As UltraGridRow

                        'Check same item form the Stock Value list in the memory and take data in to variables

                        For Each ugR In DDStock.Rows

                            If ugR.Cells("StockLink").Value = Dr("SNStockLink") Then

                                iStockLink = ugR.Cells("StockLink").Value
                                sCode = ugR.Cells("Code").Value
                                sDescription_1 = ugR.Cells("Description_1").Value
                                bSerialItem = ugR.Cells("SerialItem").Value
                                bWhseItem = ugR.Cells("WhseItem").Value

                                If ugR.Cells("iItemCostingMethod").Value = 0 Then
                                    fCostPrice = ugR.Cells("AveUCst").Value
                                ElseIf ugR.Cells("iItemCostingMethod").Value = 1 Then
                                    fCostPrice = ugR.Cells("LatUCst").Value
                                End If

                                iUnit = IIf(IsDBNull(ugR.Cells("iUOMStockingUnitID").Value) = True, 0, ugR.Cells("iUOMStockingUnitID").Value)
                                iUnitCate = IIf(IsDBNull(ugR.Cells("iUnitCategoryID").Value) = True, 0, ugR.Cells("iUnitCategoryID").Value)
                                fCostMargine = IIf(IsDBNull(ugR.Cells("fCostMargine").Value) = True, 0, ugR.Cells("fCostMargine").Value)
                            End If
                        Next

                        Dim ugR1 As UltraGridRow
                        Dim ugR2 As UltraGridRow
                        Dim iCount As Integer = 0

                        '''''''''''' Check item exsist in the grid
                        ' then add a new child band
                        For Each ugR In UG.Rows
                            If ugR.Cells("StockID").Value = iStockLink Then
                                ugR.Activate()
                                iCount = iCount + 1
                                If UG.ActiveRow.Band.Index = 0 Then
                                    ugR1 = UG.ActiveRow.ChildBands(0).Band.AddNew
                                Else
                                    ugR1 = UG.DisplayLayout.Bands(1).AddNew
                                End If
                                ugR1.Cells("LN").Value = ugR1.Index + 1
                                ugR1.Cells("SerialNumber").Value = Dr("SerialNumber")
                                ugR1.Cells("SNStockLink").Value = iStockLink
                                ugR1.Cells("PrimaryLineID").Value = ugR.Cells("Line").Value
                            End If
                        Next

                        '''''' If item not in the grid then again add a new line to grid and add a child band
                        If iCount = 0 Then
                            ugR2 = UG.DisplayLayout.Bands(0).AddNew
                            ugR2.Cells("Line").Value = ugR2.Index + 1
                            ugR2.Cells("Warehouse").Value = iWH
                            ugR2.Cells("StockID").Value = iStockLink
                            ugR2.Cells("Code").Value = iStockLink
                            ugR2.Cells("Description_1").Value = sDescription_1
                            ugR2.Cells("IsLot").Value = bSerialItem
                            ugR2.Cells("IsWH").Value = bWhseItem
                            ugR2.Cells("CostPrice").Value = fCostPrice
                            ugR2.Cells("iUnit").Value = iUnit
                            ugR2.Cells("iUnitCate").Value = iUnitCate
                            ugR2.Cells("TaxType").Value = 1
                            ugR2.Cells("fCostMargine").Value = fCostMargine


                            For Each ugR3 As UltraGridRow In DDTaxType.Rows
                                If ugR2.Cells("TaxType").Value = ugR3.Cells("Code").Value Then
                                    ugR2.Cells("TaxRate").Value = ugR3.Cells("TaxRate").Value
                                End If
                            Next

                            ugR1 = ugR2.ChildBands(0).Band.AddNew()
                            ugR1.Cells("LN").Value = ugR1.Index + 1
                            ugR1.Cells("SerialNumber").Value = Dr("SerialNumber")
                            ugR1.Cells("SNStockLink").Value = iStockLink
                            ugR1.Cells("PrimaryLineID").Value = ugR2.Cells("Line").Value
                        End If
                    Next

                    '##########################################################################
                    For Each ugR In UG.Rows
                        If ugR.Cells("IsLot").Value = True Then
                            ugR.Cells("ConfirmQty").Value = ugR.ChildBands(0).Rows.Count
                            ugR.Cells("Quantity").Value = ugR.ChildBands(0).Rows.Count
                        End If
                    Next

                    'For Each ugR In UG.Rows
                    '    ugR.Cells("ConfirmQty").Value = ugR.ChildBands(0).Rows.Count
                    '    ugR.Cells("Quantity").Value = ugR.ChildBands(0).Rows.Count
                    'Next
                Else
                    MsgBox("can not find the Serial Number", MsgBoxStyle.Exclamation, "Pastel Evolution")
                    Exit Sub
                End If
            End With
            For Each ugR In UG.Rows
                ugR.Expanded = False
            Next
            txtBarCode.ResetText()
        End If
    End Sub
    Private Function PriceExcl(ByVal TaxRate As Double, ByVal PriceIncl As Double) As Double
        Dim dblPriceExcl As Double = 0
        dblPriceExcl = Math.Round(PriceIncl * 100 / (100 + TaxRate), 2, MidpointRounding.AwayFromZero)
        Return dblPriceExcl
    End Function
    Private Function PriceIncl(ByVal TaxRate As Double, ByVal PriceExcl As Double) As Double
        Dim dblPriceIncl As Double = 0
        dblPriceIncl = Math.Round(PriceExcl + (PriceExcl * TaxRate / 100), 2, MidpointRounding.AwayFromZero)
        Return dblPriceIncl
    End Function
    Private Sub UG_BeforeExitEditMode(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs) Handles UG.BeforeExitEditMode

        For Each ugR In UG.Rows
            If UG.ActiveCell.Column.Key = "Description_1" Then
                If ugR.Cells("StockID").Row.Index <> UG.ActiveRow.Cells("StockID").Row.Index Then
                    If ugR.Cells("StockID").Value = UG.ActiveRow.Cells("StockID").Value Then
                        MessageBox.Show("Stock Item " & ugR.Cells("Description_1").Value & " already added", "Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        UG.ActiveRow.Cells("Description_1").Value = ""
                        'UG.ActiveRow.Cells("Description_2").Value = ""
                        Exit Sub
                    End If
                End If
            End If
        Next


        If UG.ActiveCell.Column.Key = "Quantity" Then

            ''check max reorder level-----------------------------
            'Dim objSQL As New clsSqlConn

            'With objSQL
            '    SQL = "SELECT Max_Lvl FROM StkItem WHERE StockLink ='" & UG.ActiveCell.Row.Cells("StockID").Value & "'"
            '    DS = New DataSet
            '    DS = .Get_Data_Sql(SQL)
            '    If DS.Tables(0).Rows.Count > 0 Then
            '        If UG.ActiveCell.Row.Cells("Quantity").Value > DS.Tables(0).Rows(0)(0) Then
            '            MessageBox.Show("Maximum amount allowed is " & DS.Tables(0).Rows(0)(0) & "", "Pastel Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            Exit Sub
            '        End If
            '    End If
            'End With
            '----------------------------------------------------

            With UG.ActiveCell
                '.Value = .Text
                .Row.Cells("ConfirmQty").Value = .Row.Cells("Quantity").Value
                .Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTax").Value = LineTax(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTax").Value = LineTax(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
            End With
        ElseIf UG.ActiveCell.Column.Key = "Price_Excl" Then
            With UG.ActiveCell
                .Value = .Text
                .Row.Cells("Price_Incl").Value = PriceIncl(.Row.Cells("TaxRate").Value, .Value)
                .Row.Cells("ConfirmQty").Value = .Row.Cells("Quantity").Value
                .Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTax").Value = LineTax(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTax").Value = LineTax(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
            End With
        ElseIf UG.ActiveCell.Column.Key = "Price_Incl" Then
            With UG.ActiveCell
                .Value = .Text
                .Row.Cells("Price_Excl").Value = PriceExcl(.Row.Cells("TaxRate").Value, .Value)
                .Row.Cells("ConfirmQty").Value = .Row.Cells("Quantity").Value
                .Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTax").Value = LineTax(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTax").Value = LineTax(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
            End With
        ElseIf UG.ActiveCell.Column.Key = "TaxRate" Then
            With UG.ActiveCell
                .Value = .Text
                .Row.Cells("ConfirmQty").Value = .Row.Cells("Quantity").Value
                .Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTax").Value = LineTax(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTax").Value = LineTax(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
            End With
        ElseIf UG.ActiveCell.Column.Key = "Discount" Then
            With UG.ActiveCell
                .Value = .Text
                .Row.Cells("ConfirmQty").Value = .Row.Cells("Quantity").Value
                .Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTax").Value = LineTax(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTax").Value = LineTax(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                '.Row.Cells("fMarginAmt").Value = MarginAmount(.Row.Cells("fUnitCostMargin").Value, .Row.Cells("CostPrice").Value)
            End With
        ElseIf UG.ActiveCell.Column.Key = "fCostMargine" Then
            With UG.ActiveCell
                .Value = .Text
                .Row.Cells("ConfirmQty").Value = .Row.Cells("Quantity").Value
                '.Row.Cells("fUnitCostMargine").Value = MarginAmount(.Value, .Row.Cells("CostPrice").Value)
                .Row.Cells("fUnitCostMargine").Value = 0
                .Row.Cells("Price_Excl").Value = CalPriceExcl(.Row.Cells("fUnitCostMargine").Value, .Row.Cells("CostPrice").Value)
                .Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTax").Value = LineTax(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTax").Value = LineTax(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                '.Row.Cells("fCostMargine").Value = MarginAmount(UG.ActiveCell.Row.Cells("fUnitCostMargine").Value, .Row.Cells("CostPrice").Value)
            End With
        ElseIf UG.ActiveCell.Column.Key = "CostPrice" Then
            With UG.ActiveCell
                .Value = .Text
                .Row.Cells("ConfirmQty").Value = .Row.Cells("Quantity").Value
                '.Row.Cells("fUnitCostMargine").Value = MarginAmount(.Row.Cells("fCostMargine").Value, .Value)
                .Row.Cells("fUnitCostMargine").Value = 0
                .Row.Cells("Price_Excl").Value = CalPriceExcl(.Row.Cells("fUnitCostMargine").Value, .Row.Cells("CostPrice").Value)
                .Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTax").Value = LineTax(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTax").Value = LineTax(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
            End With
            'ElseIf UG.ActiveCell.Column.Key = "Lot" Then
            '    With UG.ActiveCell
            '        sSQL = " SELECT   _etblLotTracking.idLotTracking,_etblLotTracking.cLotDescription AS Description, _etblLotTracking.dExpiryDate AS [Expiry Date], _etblLotTrackingQty.fQtyOnHand AS [Qty On Hand], " & _
            '        " _etblLotTrackingQty.fQtyReserved AS [Qty Reserved]" & _
            '        " FROM         _etblLotTracking INNER JOIN " & _
            '        " _etblLotTrackingQty ON _etblLotTracking.idLotTracking = _etblLotTrackingQty.iLotTrackingID WHERE  _etblLotTracking.iStockID = " & UG.ActiveRow.Cells("StockID").Value & " AND _etblLotTrackingQty.fQtyOnHand > 0 "

            '        Try
            '            Con1.ConnectionString = sConStr
            '            CMD = New SqlCommand(sSQL, Con1)
            '            DS = New DataSet
            '            DA = New SqlDataAdapter(CMD)

            '            Con1.Open()
            '            DA.Fill(DS)
            '            Con1.Close()

            '            DDLot.DataSource = DS.Tables(0)
            '            DDLot.ValueMember = "idLotTracking"
            '            DDLot.DisplayMember = "Description"
            '            DDLot.DisplayLayout.Bands(0).Columns("idLotTracking").Hidden = True

            '        Catch ex As Exception
            '            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
            '            Exit Sub
            '        Finally
            '            If Con1.State = ConnectionState.Open Then Con1.Close()
            '        End Try


            '    End With
        End If
    End Sub



    Private Sub tsbDeleteLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbDeleteLine.Click
        If UG.Selected.Rows.Count > 0 Then
            Dim ugR As UltraGridRow
            For Each ugR In UG.Selected.Rows
                ugR.Delete(False)
            Next
        End If
    End Sub

    Private Sub tsbNewlIne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbNewlIne.Click
        Dim ugR As UltraGridRow
        ugR = UG.DisplayLayout.Bands(0).AddNew
        ugR.Cells("Line").Value = ugR.Index + 1
        ugR.Cells("Line").Selected = True
        'ugR.Cells("Warehouse").Value = iWH
        'If iDocType = DocType.SalesOrder Then
        '    ugR.Cells("TaxType").Value = 1
        '    For Each ugR1 As UltraGridRow In DDTaxType.Rows
        '        If ugR.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
        '            ugR.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
        '        End If
        '    Next
        'ElseIf iDocType = DocType.PurchaseOrder Then
        '    ugR.Cells("TaxType").Value = 3
        '    For Each ugR1 As UltraGridRow In DDTaxType.Rows
        '        If ugR.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
        '            ugR.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
        '        End If
        '    Next
        'End If
        UG.DisplayLayout.Bands(0).Columns("BranchQty").CellActivation = Activation.Disabled
        UG.DisplayLayout.Bands(0).Columns("Warehouse").CellActivation = Activation.Disabled
        UG.DisplayLayout.Bands(0).Columns("WHMax_lvl").CellActivation = Activation.Disabled
        'UG.DisplayLayout.Bands(0).Columns("Quantity").CellActivation = Activation.AllowEdit
    End Sub


    Private Sub txtBarCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBarCode.Click
        If UG.Selected.Rows.Count > 0 Then
            Dim ugR As UltraGridRow
            For Each ugR In UG.Selected.Rows
                ugR.Selected = False
            Next
        End If
    End Sub

    Private Sub UG_ClickCellButton(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UG.ClickCellButton
        If e.Cell.Column.Key = "Serial" Then
            If UG.ActiveCell.Row.Cells("IsLot").Value = False Then
                Exit Sub
            End If
            Dim b As Integer
            'If e.Cell.Row.Cells("ConfirmQty").Value = 0 Then
            '    MsgBox("Confirm Qty can not be Zero", MsgBoxStyle.Exclamation, "Pastel Evolution")
            '    Exit Sub
            'End If

            Dim objSQL As New clsSqlConn
            With objSQL
                Try
                    SQL = "SELECT SerialNumber, SNStockLink FROM SerialMF WHERE SNStockLink = " & e.Cell.Row.Cells("StockID").Value & " AND CurrentLoc = 1 and CurrentAccLink = " & iWH
                    DS = New DataSet
                    DS = .Get_Data_Sql(SQL)
                    If DS.Tables(0).Rows.Count > 0 Then
                        Dim dr As DataRow
                        Dim a As Boolean
                        'frmSelectSerialNo.lbSelected.Items.Clear()
                        For Each dr In DS.Tables(0).Rows
                            a = False
                            If e.Cell.Row.ChildBands(0).Rows.Count > 0 Then
                                For Each ugCR In e.Cell.Row.ChildBands(0).Rows
                                    If dr("SerialNumber") = ugCR.Cells("SerialNumber").Value Then
                                        a = True
                                        Exit For
                                    End If
                                Next
                                If Not a Then
                                    frmSelectSerialNo.lbSelected.Items.Add(CStr(dr("SerialNumber")))
                                End If
                            Else
                                frmSelectSerialNo.lbSelected.Items.Add(CStr(dr("SerialNumber")))
                            End If
                        Next


                        If e.Cell.Row.ChildBands.HasChildRows = True Then
                            Dim ugCR As UltraGridRow
                            frmSelectSerialNo.lbAdded.Items.Clear()
                            For Each ugCR In e.Cell.Row.ChildBands(0).Rows
                                frmSelectSerialNo.lbAdded.Items.Add(CStr(ugCR.Cells("SerialNumber").Value))
                                'b = b + 1
                            Next
                        End If



                    End If
                Catch ex As Exception
                    Exit Sub
                Finally
                    DS.Dispose()
                    objSQL = Nothing
                    .Con_Close()
                End Try
            End With

            frmSelectSerialNo.ShowDialog()

            With frmSelectSerialNo

                If e.Cell.Row.Band.Index = 0 Then
                    If e.Cell.Row.ChildBands.HasChildRows = True Then
                        Dim ugCR As UltraGridRow
                        Dim a As Integer
                        a = e.Cell.Row.Index
                        For Each ugCR In e.Cell.Row.ChildBands(0).Rows.All
                            ugCR.Delete(False)
                        Next
                    End If
                End If
                b = 0
                Dim ugR As UltraGridRow
                For Each ListItem In .lbAdded.Items
                    If ListItem.ToString <> "" Then
                        b = b + 1
                        If e.Cell.Row.Band.Index = 0 Then
                            ugR = e.Cell.Row.ChildBands(0).Band.AddNew
                        Else
                            ugR = UG.DisplayLayout.Bands(1).AddNew
                        End If
                        ugR.Cells("LN").Value = ugR.Index + 1
                        ugR.Cells("SerialNumber").Value = ListItem
                        ugR.Cells("SNStockLink").Value = ugR.ParentRow.Cells("StockID").Value
                        ugR.Cells("PrimaryLineID").Value = ugR.ParentRow.Cells("Line").Value
                    End If
                Next
                For Each ugR In UG.Rows
                    ugR.Expanded = False
                Next


                e.Cell.Row.Cells("ConfirmQty").Value = b
                e.Cell.Row.Cells("Quantity").Value = b


                .Dispose()




            End With
        Else
            Exit Sub
        End If
    End Sub

    Private Sub tsbUpdateSO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbUpdateSO.Click

        UG_Leave(UG, e)

        If IsNew Then

            saveIBT(iSONo, False, 0)

        Else

            'MsgBox("Please press update button to Process the IBT", MsgBoxStyle.Information, "Error")

            saveIBT(iSONo, False, 0)
        End If

    End Sub


    Private Sub saveIBT(ByVal IBTNo As Long, Optional ByRef bProcess As Boolean = False, Optional ByRef imyIBTno As Integer = 0, Optional ByRef sError As String = "")

        UG.PerformAction(UltraGridAction.CommitRow)

        If cmbCustomer.Text = "" Then
            MsgBox("Customer Name can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If UG.Rows.Count = 0 Then
            MsgBox("Enter Item Details", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbSaleRep.Value = Nothing Then
            MsgBox("Select Sales Person", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbGRVLoc.Text = Nothing Then
            cmbCustomer.ResetText()
            MsgBox("Select supplier Please", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        'Dim mUgR As UltraGridRow
        Dim iToWH As Integer = 0

        getDefaultwh(sError, iToWH)

        If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
        If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

        Dim iInvoiceID As Integer = 0
        Dim sOrderNumber As String = ""
        Dim iCustomer As Integer = 0
        Dim iSupplier As Integer = 0

        If CInt(IBTNo) = 0 Then
            iOldSoNo = 0
        Else
            iOldSoNo = IBTNo
        End If

        DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
        DatabaseContext.SetLicense("DE09110022", "1428511")
        DatabaseContext.CreateConnection(sSQLSrvName, sSQLSrvDataBase, sSQLSrvUserName, sSQLSrvPassword, False)

        Try
            DatabaseContext.BeginTran()

            Dim SalesOrder As SalesOrder
            If IsNew And iSONo = 0 Then
                SalesOrder = New SalesOrder()
            ElseIf iOldSoNo <> 0 And IsNew = False Then
                SalesOrder = New SalesOrder()
            Else
                Exit Sub
            End If

            Dim SalesOrderDetail As OrderDetail
            'Dim SerialNumberCollection As SerialNumberCollection

            Dim InventoryItem As InventoryItem
            Dim WH As Warehouse

            Dim Customer As Customer
            If cmbCustomer.Text.Length > 4 Then
                Customer = New Customer(CInt(cmbCustomer.Value))
            Else
                Customer = New Customer(CInt(cmbCustomer.Text))
            End If


            '-------------------------------------------------------------------------------------------
            Dim bIsCheckTerms As Boolean
            Dim dblAccountBalance As Double = 0
            Dim dblCreditLimit As Double = 0
            bIsCheckTerms = Customer.CheckTerms
            If bIsCheckTerms = True Then
                dblAccountBalance = Customer.AccountBalance
                'get_ConsolidatedCredit()
                'dblAccountBalance = conCLimit
                dblCreditLimit = Customer.CreditLimit
                If dblAccountBalance + txtConAmt.Value > dblCreditLimit And dblCreditLimit > 0.0 Then
                    If bIsAllowOverrideCreditLimit = False Then
                        MsgBox("Account is Over Credit Limit", MsgBoxStyle.Exclamation, "Pastel Evolution")
                        Exit Sub
                    End If

                    'Dim dd As Integer
                    Dim AccTrms As Integer
                    AccTrms = Customer.AccountTerms
                    Select Case AccTrms
                        Case 0
                            AccTrms = 0
                        Case 1
                            AccTrms = 30
                        Case 2
                            AccTrms = 60
                        Case 3
                            AccTrms = 90
                        Case 4
                            AccTrms = 120
                        Case 5
                            AccTrms = 150
                        Case 6
                            AccTrms = 180
                    End Select
                    'GLAccount.Find(Customer.AccountBalance)
                    'dd = DateDiff(DateInterval.Day, Now(), CDate("4/3/2011 6:00:00"))

                End If
            End If
            '-------------------------------------------------------------------------------------------



            iCustomer = Customer.ID

            Dim Representative As New SalesRepresentative(CInt(cmbSaleRep.Value))

            'SalesOrder.OrderNo = CStr(txtOrdNo.Text.Trim)
            'SalesOrder.DeliveryNote = CStr(txtDelNote.Text.Trim)
            SalesOrder.Account = Customer
            SalesOrder.InvoiceTo = Customer.PostalAddress
            SalesOrder.DeliverTo = Customer.PhysicalAddress
            SalesOrder.ExternalOrderNo = txtExtOrder.Text.ToString
            'SalesOrder.ExternalOrderNo = PONo
            SalesOrder.InvoiceDate = Format(dtpInDate.Value, "dd/MM/yyyy")
            SalesOrder.DeliveryDate = Format(dtpDeliDate.Value, "dd/MM/yyyy")
            SalesOrder.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")
            SalesOrder.Representative = Representative
            SalesOrder.ProjectID = cmbProject.Value

            If bIsInclusive = True Then
                SalesOrder.TaxMode = TaxMode.Inclusive
            ElseIf bIsInclusive = False Then
                SalesOrder.TaxMode = TaxMode.Exclusive
            End If

            SalesOrder.UserFields("uiIDSOrdSuppID") = CInt(4)
            SalesOrder.UserFields("ucIDSOrddbName") = cmbGRVLoc.Text.Trim

            Dim bLoop As Boolean = False

            Try

lbl1:           For Each SalesOrderDetail In SalesOrder.Detail
                    bLoop = False

                    SalesOrder.Detail.Remove(SalesOrderDetail)
                    bLoop = True
                    Exit For

                Next
                If bLoop Then
                    bLoop = False
                    GoTo lbl1
                End If

            Catch ex As Exception

            Finally

                bLoop = False
            End Try

            'Dim i As Integer = 0
            For Each ugR In UG.Rows
                'Dim i As Integer
                'i = 0
                'For Each SalesOrderDetail In SalesOrder.Detail
                '    'If ugR.Cells("LineID").Value = SalesOrderDetail.ID Then
                '    If CInt(ugR.Cells("StockID").Value) = SalesOrderDetail.InventoryItemID Then
                '        i = i + 1
                '        InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))
                '        SalesOrderDetail.InventoryItem = InventoryItem
                '        If ugR.Cells("IsLot").Value = True Then
                '            If ugR.ChildBands.HasChildRows = False Then
                '                MsgBox("Enter Serial Numbers", MsgBoxStyle.Exclamation, "Pastel Evolution")
                '                DatabaseContext.RollbackTran()
                '                If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
                '                If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
                '                Exit Sub
                '            End If

                '            For Each ugR2 In ugR.ChildBands(0).Rows
                '                SalesOrderDetail.SerialNumbers.Add(CStr(ugR2.Cells("SerialNumber").Value))
                '            Next

                '        End If

                '        SalesOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                '        If bIsInclusive = False Then
                '            SalesOrderDetail.TaxMode = TaxMode.Exclusive
                '            SalesOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                '            SalesOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                '        ElseIf bIsInclusive = True Then
                '            SalesOrderDetail.TaxMode = TaxMode.Inclusive
                '            SalesOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                '            SalesOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                '        End If
                '        SalesOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)

                '        If ugR.Cells("IsWH").Value = True Then
                '            WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                '            SalesOrderDetail.Warehouse = WH
                '        End If
                '        SalesOrderDetail.Description = CStr(ugR.Cells("Description_1").Value)
                '        SalesOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                '        'SalesOrder.Detail.Add(SalesOrderDetail)
                '        'SalesOrder.Detail.Remove(SalesOrderDetail)
                '    End If
                'Next

                ''Dim ugR As UltraGridRow
                ''For Each ugR In UG.Rows
                'If i = 0 Then

                If CDbl(ugR.Cells("Quantity").Value) > 0 Then 'process only items that have quenty
                    SalesOrderDetail = New OrderDetail

                    InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))
                    SalesOrderDetail.InventoryItem = InventoryItem
                    'SalesOrderDetail.
                    SalesOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                    SalesOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)
                    If bIsInclusive = False Then
                        SalesOrderDetail.TaxMode = TaxMode.Exclusive
                        SalesOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                        SalesOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                    ElseIf bIsInclusive = True Then
                        SalesOrderDetail.TaxMode = TaxMode.Inclusive
                        SalesOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                        SalesOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                    End If

                    If bProcess Then

                        'If ugR.Cells("IsSerialNo").Value = True Then
                        '    If ugR.ChildBands.HasChildRows = False Then
                        '        MsgBox("Enter Serial Numbers" & vbCrLf & " Item = " & ugR.Cells("Code").Value, MsgBoxStyle.Exclamation, "Pastel Evolution")
                        '        DatabaseContext.RollbackTran()
                        '        If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
                        '        If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
                        '        Exit Sub
                        '    End If
                        '    'If bProcess Then
                        '    Dim ugR2 As UltraGridRow
                        '    For Each ugR2 In ugR.ChildBands(0).Rows
                        '        'SalesOrderDetail.SerialNumbers.Add(CStr(ugR2.Cells("SerialNumber").Value))
                        '        Dim sn As String = CStr(ugR2.Cells("SerialNumber").Value)
                        '        SalesOrderDetail.SerialNumbers.Add(sn)
                        '    Next
                        '    'End If
                        'End If
                        If ugR.Cells("IsLot").Value = True Then
                            If ugR.Cells("Lot").Text = Nothing Then
                                MsgBox("Enter Lot Numbers" & vbCrLf & " Item = " & ugR.Cells("Code").Value, MsgBoxStyle.Exclamation, "Pastel Evolution")
                                DatabaseContext.RollbackTran()
                                If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
                                If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
                                Exit Sub
                            End If

                        End If
                    End If
                    If ugR.Cells("IsWH").Value = True Then
                        WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                        SalesOrderDetail.Warehouse = WH
                    End If

                    SalesOrderDetail.Description = CStr(ugR.Cells("Description_1").Value)
                    'SalesOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                    SalesOrderDetail.DiscountPercent = CDbl(ugR.Cells("Discount").Value)
                    SalesOrderDetail.LotID = CStr(ugR.Cells("Lot").Value)
                    SalesOrder.Detail.Add(SalesOrderDetail)
                    'SalesOrder.Detail.Remove 
                    'End If


                    'i = i + 1

                End If
            Next

            SalesOrder.Save()

            If bProcess Then
                SalesOrder.Process()
                txtOrdNo.Text = SalesOrder.Reference
            End If

abc:        DatabaseContext.CommitTran()

            iSONo = SalesOrder.ID
            sSONo = SalesOrder.Reference

            imyIBTno = SalesOrder.ID

            'Call MemorizeStock()

            If bProcess Then
                MsgBox("Invoice No = " & SalesOrder.Reference, MsgBoxStyle.Information, "Pastel Evolution")
            Else
                MsgBox("Sales Order No = " & SalesOrder.OrderNo, MsgBoxStyle.Information, "Pastel Evolution")
            End If

            If bProcess Then
                Dim iSalesOrder As Integer = iSONo
                ''MsgBox(SalesOrder.Reference, MsgBoxStyle.Information, "Pastel Evolution")
                ''DatabaseContext.CommitTran()
                'If MsgBox("Do you want to print now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
                '    Dim objRep As New ReportDocument
                '    Dim s As String = Application.StartupPath

                '    'If iDocType_1 = 10 Then
                '    objRep.Load(Application.StartupPath & "\IBT.rpt")
                '    'ElseIf iDocType_1 = 8 Then 
                '    '    objRep.Load(Application.StartupPath & "\IBT_R.rpt")
                '    'End If



                '    Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= '" & cmbGRVLoc.Text & "' ")
                '    SQL = " SELECT OrderNum, DocState  from InvNum where ExtOrderNum = '" & SalesOrder.Reference & "' "
                '    CMD = New SqlCommand(SQL, Con2)
                '    CMD.CommandType = CommandType.Text
                '    DA = New SqlDataAdapter(CMD)
                '    DS1 = New DataSet
                '    Con2.Open()
                '    DA.Fill(DS1)
                '    Con2.Close()

                '    If DS1.Tables(0).Rows.Count > 0 Then
                '        frmPrintPreview.vInvNo = DS1.Tables(0).Rows(0)(0)
                '        If DS1.Tables(0).Rows(0)(1) = 4 Then
                '            frmPrintPreview.vPState = "Processed"
                '        Else
                '            frmPrintPreview.vPState = "Unprocessed"
                '        End If

                '    Else
                '        frmPrintPreview.vInvNo = " "
                '        frmPrintPreview.vPState = " "
                '    End If

                '    frmPrintPreview.vSelectionFormula = "{InvNum.AutoIndex}= " & SalesOrder.ID & ""

                '    iRepoID = 2

                '    frmPrintPreview.ShowDialog()
                '    iRepoID = 0

                '    Exit Sub


                '    'objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iSalesOrder & ""

                '    'ApplyLoginToTable(objRep)

                '    'objRep.SetDatabaseLogon(sSQLSrvUserName, sSQLSrvPassword, sSQLSrvReportSrv, sSQLSrvDataBase)

                'End If
            End If

            If Not bProcess Then
                Discard()
            End If


        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Critical, "Pastel Evolution")
            sError = ex.Message
            imyIBTno = 0
            Exit Sub
            'GoTo abc

        Finally
            PONo = ""
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
            If Con2.State = ConnectionState.Open Then Con2.Close()

        End Try
    End Sub

    Private Sub saveUPIBT(ByVal IBTNo As Long, Optional ByRef bProcess As Boolean = False, Optional ByRef imyIBTno As Integer = 0, Optional ByRef sError As String = "")

        UG.PerformAction(UltraGridAction.CommitRow)

        If cmbCustomer.Text = "" Then
            MsgBox("Customer Name can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If UG.Rows.Count = 0 Then
            MsgBox("Enter Item Details", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbSaleRep.Value = Nothing Then
            MsgBox("Select Sales Person", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbGRVLoc.Text = Nothing Then
            cmbCustomer.ResetText()
            MsgBox("Select supplier Please", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        'Dim mUgR As UltraGridRow
        Dim iToWH As Integer = 0

        getDefaultwh(sError, iToWH)

        If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
        If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

        Dim iInvoiceID As Integer = 0
        Dim sOrderNumber As String = ""
        Dim iCustomer As Integer = 0
        Dim iSupplier As Integer = 0

        If CInt(IBTNo) = 0 Then
            iOldSoNo = 0
        Else
            iOldSoNo = IBTNo
        End If

        DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
        DatabaseContext.SetLicense("DE09110022", "1428511")
        DatabaseContext.CreateConnection(sSQLSrvName, sSQLSrvDataBase, sSQLSrvUserName, sSQLSrvPassword, False)

        Try
            DatabaseContext.BeginTran()

            Dim SalesOrder As SalesOrder
            If IsNew And iSONo = 0 Then
                SalesOrder = New SalesOrder()
            ElseIf iOldSoNo <> 0 And IsNew = False Then
                SalesOrder = New SalesOrder()
            Else
                Exit Sub
            End If

            Dim SalesOrderDetail As OrderDetail
            'Dim SerialNumberCollection As SerialNumberCollection

            Dim InventoryItem As InventoryItem
            Dim WH As Warehouse

            Dim Customer As Customer
            If cmbCustomer.Text.Length > 4 Then
                Customer = New Customer(CInt(cmbCustomer.Value))
            Else
                Customer = New Customer(CInt(cmbCustomer.Text))
            End If


            '-------------------------------------------------------------------------------------------
            Dim bIsCheckTerms As Boolean
            Dim dblAccountBalance As Double = 0
            Dim dblCreditLimit As Double = 0
            bIsCheckTerms = Customer.CheckTerms
            If bIsCheckTerms = True Then
                dblAccountBalance = Customer.AccountBalance
                'get_ConsolidatedCredit()
                'dblAccountBalance = conCLimit
                dblCreditLimit = Customer.CreditLimit
                If dblAccountBalance + txtConAmt.Value > dblCreditLimit And dblCreditLimit > 0.0 Then
                    If bIsAllowOverrideCreditLimit = False Then
                        MsgBox("Account is Over Credit Limit", MsgBoxStyle.Exclamation, "Pastel Evolution")
                        Exit Sub
                    End If

                    'Dim dd As Integer
                    Dim AccTrms As Integer
                    AccTrms = Customer.AccountTerms
                    Select Case AccTrms
                        Case 0
                            AccTrms = 0
                        Case 1
                            AccTrms = 30
                        Case 2
                            AccTrms = 60
                        Case 3
                            AccTrms = 90
                        Case 4
                            AccTrms = 120
                        Case 5
                            AccTrms = 150
                        Case 6
                            AccTrms = 180
                    End Select


                End If
            End If
            '-------------------------------------------------------------------------------------------



            iCustomer = Customer.ID

            Dim Representative As New SalesRepresentative(CInt(cmbSaleRep.Value))

            'SalesOrder.OrderNo = CStr(txtOrdNo.Text.Trim)
            'SalesOrder.DeliveryNote = CStr(txtDelNote.Text.Trim)
            SalesOrder.Account = Customer
            SalesOrder.InvoiceTo = Customer.PostalAddress
            SalesOrder.DeliverTo = Customer.PhysicalAddress
            SalesOrder.ExternalOrderNo = txtExtOrder.Text.ToString
            'SalesOrder.ExternalOrderNo = PONo
            SalesOrder.InvoiceDate = Format(dtpInDate.Value, "dd/MM/yyyy")
            SalesOrder.DeliveryDate = Format(dtpDeliDate.Value, "dd/MM/yyyy")
            SalesOrder.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")
            SalesOrder.Representative = Representative
            SalesOrder.Description = "Pending Requested IBT"

            If bIsInclusive = True Then
                SalesOrder.TaxMode = TaxMode.Inclusive
            ElseIf bIsInclusive = False Then
                SalesOrder.TaxMode = TaxMode.Exclusive
            End If

            SalesOrder.UserFields("uiIDSOrdSuppID") = CInt(4)
            SalesOrder.UserFields("ucIDSOrddbName") = cmbGRVLoc.Text.Trim

            Dim bLoop As Boolean = False

            Try

lbl1:           For Each SalesOrderDetail In SalesOrder.Detail
                    bLoop = False

                    SalesOrder.Detail.Remove(SalesOrderDetail)
                    bLoop = True
                    Exit For

                Next
                If bLoop Then
                    bLoop = False
                    GoTo lbl1
                End If

            Catch ex As Exception

            Finally

                bLoop = False
            End Try

            'Dim i As Integer = 0
            For Each ugR In UG.Rows


                If CDbl(ugR.Cells("Quantity").Value) = 0 Then 'process only items that have quenty
                    SalesOrderDetail = New OrderDetail

                    InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))
                    SalesOrderDetail.InventoryItem = InventoryItem
                    'SalesOrderDetail.
                    SalesOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                    SalesOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)
                    If bIsInclusive = False Then
                        SalesOrderDetail.TaxMode = TaxMode.Exclusive
                        SalesOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                        SalesOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                    ElseIf bIsInclusive = True Then
                        SalesOrderDetail.TaxMode = TaxMode.Inclusive
                        SalesOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                        SalesOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                    End If

                    If bProcess Then

                        If ugR.Cells("IsLot").Value = True Then
                            If ugR.Cells("Lot").Text = Nothing Then
                                MsgBox("Enter Lot Numbers" & vbCrLf & " Item = " & ugR.Cells("Code").Value, MsgBoxStyle.Exclamation, "Pastel Evolution")
                                DatabaseContext.RollbackTran()
                                If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
                                If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
                                Exit Sub
                            End If

                        End If
                    End If
                    If ugR.Cells("IsWH").Value = True Then
                        WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                        SalesOrderDetail.Warehouse = WH
                    End If

                    SalesOrderDetail.Description = CStr(ugR.Cells("Description_1").Value)
                    'SalesOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                    SalesOrderDetail.DiscountPercent = CDbl(ugR.Cells("Discount").Value)
                    SalesOrderDetail.LotID = CStr(ugR.Cells("Lot").Value)
                    SalesOrder.Detail.Add(SalesOrderDetail)
                    'SalesOrder.Detail.Remove 
                    'End If


                    'i = i + 1

                End If
            Next

            SalesOrder.Save()

            'If bProcess Then
            '    SalesOrder.Process()
            '    txtOrdNo.Text = SalesOrder.Reference
            'End If

abc:        DatabaseContext.CommitTran()

            'iSONo = SalesOrder.ID
            'sSONo = SalesOrder.Reference

            imyIBTno = SalesOrder.ID

            'Call MemorizeStock()

            If bProcess Then
                MsgBox("Invoice No = " & SalesOrder.Reference, MsgBoxStyle.Information, "Pastel Evolution")
            Else
                MsgBox("Sales Order No = " & SalesOrder.OrderNo, MsgBoxStyle.Information, "Pastel Evolution")
            End If

            If bProcess Then
                Dim iSalesOrder As Integer = iSONo

            End If

            If Not bProcess Then
                Discard()
            End If


        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Critical, "Pastel Evolution")
            sError = ex.Message
            imyIBTno = 0
            Exit Sub
            'GoTo abc

        Finally
            PONo = ""
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
            If Con2.State = ConnectionState.Open Then Con2.Close()

        End Try
    End Sub

    Private ReadOnly Property DefaultPrinterName() As String
        Get
            Dim ps As New PrinterSettings()
            Return ps.PrinterName
        End Get
    End Property
    Public Sub ApplyLoginToTable(ByVal ObjReport As ReportDocument)
        Dim ConInf As New TableLogOnInfo
        Dim crTables As Tables = ObjReport.Database.Tables
        For Each crTable As Table In crTables
            With ConInf.ConnectionInfo
                .ServerName = sSQLSrvReportSrv
                .DatabaseName = sSQLSrvDataBase
                .UserID = sSQLSrvUserName
                .Password = sSQLSrvPassword
            End With
            crTable.ApplyLogOnInfo(ConInf)
        Next
    End Sub

    Private Sub tsbValidate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbValidate.Click
        'Call validateSerials()



        Dim R As UltraGridRow
        For Each R In UG.Rows
            'If R.Cells("Description_1").Text = "" Or R.Cells("Quantity").Text = 0 Then
            If R.Cells("Description_1").Text = "" Then
                MessageBox.Show("Please fill all data", "Evolution AddOn")
                Exit Sub
            End If
        Next


        UG.PerformAction(UltraGridAction.CommitRow)

        UG_Leave(UG, e)

        Try

            Dim iInvoiceID As Integer = 0
            Dim sOrderNumber As String = ""
            Dim iCustomer As Integer = 0
            Dim iSupplier As Integer = 0
            Dim iIBTNo As Integer = 0
            Dim sError As String = ""
            Dim iToWH As Integer = 0
            ReDim lots(UG.Rows.Count)



            Dim agent = New Agent(iAgent)

            'getDefaultwh(sError, iToWH)

            If sError <> "" Then
                Exit Sub
            End If

            'If sAgent = "Admin" Then
            '    If MsgBox("Do you want to process Sales Order", MsgBoxStyle.OkCancel, "Pastel Evolution") = True Then

            'saveIBT(iSONo, True, iIBTNo)

            'If iIBTNo = 0 Then 'if sales order didnt process
            '    Exit Sub
            'End If
            '    End If
            'End If

            Dim InventoryItem As InventoryItem
            Dim WH As Warehouse

            'to find the mother database
            Dim CDB As String = ""
            If sSQLSrvDataBase = "dbNawinna_new" Then
                CDB = "UDA003"
            ElseIf sSQLSrvDataBase = "dbKurunegala_new" Then
                CDB = "UDA004"
            ElseIf sSQLSrvDataBase = "dbKiribathgoda_new" Then
                CDB = "UDA005"
            ElseIf sSQLSrvDataBase = "dbUdawatta_new" Then
                CDB = "UDA006"
            ElseIf sSQLSrvDataBase = "dbKelaniya_new" Then
                CDB = "UDA007"

            End If

            If sSQLSrvDataBase = "dbUdawatta_test1" Then
                CDB = "UDA001"
            ElseIf sSQLSrvDataBase = "UDAWATTA_TEST2" Then
                CDB = "UDA002"
                'ElseIf sSQLSrvDataBase = "dbKiribathgoda_test" Then
                '    MDB = "UDA003"
                'ElseIf sSQLSrvDataBase = "UDAWATTA_TEST1" Then
                '    MDB = "UDA001"
            End If


            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

            DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
            DatabaseContext.SetLicense("DE09110022", "1428511")


            DatabaseContext.CreateConnection(sSQLSrvName, cmbGRVLoc.Text, sSQLSrvUserName, sSQLSrvPassword, False)
            'DatabaseContext.CreateConnection(sSQLSrvName, "UDAWATTA-TEST1", sSQLSrvUserName, sSQLSrvPassword, False)

            DatabaseContext.BeginTran()
            Dim SalesOrder As New SalesOrder
            Dim SalesOrderDetail As OrderDetail

            Dim Customer As New Customer(CDB)
            iCustomer = Customer.ID
            SalesOrder.OrderNo = sOrderNumber
            SalesOrder.ExternalOrderNo = SalesOrder.OrderNo
            SalesOrder.Account = Customer
            SalesOrder.InvoiceTo = Customer.PostalAddress
            SalesOrder.DeliverTo = Customer.PhysicalAddress
            SalesOrder.ExternalOrderNo = txtOrdNo.Text.ToString
            SalesOrder.ExternalOrderNo = "IBT"
            SalesOrder.InvoiceDate = Format(dtpInDate.Value, "dd/MM/yyyy hh:mm")
            SalesOrder.DeliveryDate = Format(dtpDeliDate.Value, "dd/MM/yyyy hh:mm")
            SalesOrder.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy hh:mm")
            SalesOrder.Description = cmbProject.Text

            'SalesOrder.invoicen = "" ' CStr(txtInNo.Text)
            If bIsInclusive = False Then
                SalesOrder.TaxMode = TaxMode.Exclusive
            ElseIf bIsInclusive = True Then
                SalesOrder.TaxMode = TaxMode.Inclusive
            End If

            Dim LotN As New Lot()
            For Each ugR In UG.Rows
                SalesOrderDetail = New OrderDetail
                InventoryItem = New InventoryItem(CStr(ugR.Cells("Code").Text))
                SalesOrderDetail.InventoryItem = InventoryItem
                If ugR.Cells("IsLot").Value = True Then
                    'If ugR.ChildBands.HasChildRows = False Then
                    '    MsgBox("Enter Lot Numbers", MsgBoxStyle.Exclamation, "Pastel Evolution")
                    '    DatabaseContext.RollbackTran()
                    '    If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
                    '    If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
                    '    Exit Sub
                    'End If
                    'Dim ugR2 As UltraGridRow
                    'For Each ugR2 In ugR.ChildBands(0).Rows
                    '    PurchaseOrderDetail.SerialNumbers.Add(CStr(ugR2.Cells("SerialNumber").Value))
                    'Next
                End If



                SalesOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                If bIsInclusive = False Then
                    'SalesOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                    SalesOrderDetail.TaxMode = TaxMode.Exclusive
                    SalesOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                ElseIf bIsInclusive = True Then
                    'SalesOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                    SalesOrderDetail.TaxMode = TaxMode.Inclusive
                    SalesOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                End If
                SalesOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)

                If ugR.Cells("IsWH").Value = True Then
                    WH = New Warehouse(16)
                    SalesOrderDetail.Warehouse = WH
                End If
                SalesOrderDetail.Description = CStr(ugR.Cells("Description_1").Value)
                'PurchaseOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                'PurchaseOrderDetail.LotID = CStr(ugR.Cells("Lot").Value)

                If ec.Text <> Nothing Then
                    sSONo = ec.Text
                End If


                'SQL = " select cLotDescription from _etblLotTracking where idLotTracking  = '" & ugR.Cells("Lot").Value & "' "
                'Dim objSQL As New clsSqlConn
                'With objSQL
                '    Dim DS1 = New DataSet
                '    DS1 = .Get_Data_Sql(SQL)
                '    If DS1.Tables(0).Rows.Count > 0 Then
                '        LotN = New Lot()
                '        'sSONo = "PKW66295"
                '        If InStr(DS1.Tables(0).Rows(0)(0).ToString(), "-") = 0 Then
                '            LotN.Code = sSONo.Substring(3) & "-" & DS1.Tables(0).Rows(0)(0).ToString()
                '        Else
                '            LotN.Code = sSONo.Substring(3) & "-" & DS1.Tables(0).Rows(0)(0).ToString().Substring(0, InStr(DS1.Tables(0).Rows(0)(0).ToString(), "-") - 1)
                '        End If
                '        LotN.ExpiryDate = DateAdd(DateInterval.Day, 60, Date.Now)
                '    End If
                'End With

                'PurchaseOrderDetail.Lot = LotN

                SalesOrder.Detail.Add(SalesOrderDetail)
            Next

            SalesOrder.Save()

            DatabaseContext.CommitTran()

            'MemorizePOStock(PurchaseOrder.ID)

            PONo = SalesOrder.OrderNo.ToString()
            MsgBox(SalesOrder.OrderNo, MsgBoxStyle.Information, "Pastel Evolution")



            Dim iSalesOrder As Integer = SalesOrder.ID
            'MsgBox(SalesOrder.Reference, MsgBoxStyle.Information, "Pastel Evolution")
            DatabaseContext.CommitTran()
            'If MsgBox("Do you want to print now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
            Dim objRep As New ReportDocument
            Dim s As String = Application.StartupPath

            objRep.Load(s & "\IBTRequest.rpt")

            objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iSalesOrder & ""
            'ApplyLoginToTable(objRep)
            Dim ConInf As New TableLogOnInfo
            Dim crTables As Tables = objRep.Database.Tables
            For Each crTable As Table In crTables
                With ConInf.ConnectionInfo
                    .ServerName = cmbGRVLoc.Text.Trim ' "dbKelaniya_new"
                    .DatabaseName = cmbGRVLoc.Text.Trim ' "dbKelaniya_new"
                    .UserID = sSQLSrvUserName
                    .Password = sSQLSrvPassword
                End With
                crTable.ApplyLogOnInfo(ConInf)
            Next

            ''If MsgBox("Do you want to preview the sales order", MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.No Then
            'Dim DefaultPrinter As String = DefaultPrinterName
            'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
            'doctoprint.PrinterSettings.PrinterName = DefaultPrinter
            'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
            '    Dim rawKind As Integer
            '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Inv" Or doctoprint.PrinterSettings.PaperSizes(i).PaperName = "INV" Then
            '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
            '        objRep.PrintOptions.PaperSize = rawKind
            '        objRep.PrintToPrinter(1, False, 0, 0)
            '        Exit For
            '    End If
            'Next


            Dim DefaultPrinter As String = DefaultPrinterName
            Dim doctoprint As New System.Drawing.Printing.PrintDocument()
            'doctoprint.PrinterSettings.PrinterName = DefaultPrinter
            doctoprint.PrinterSettings.PrinterName = "IBT"
            For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                Dim rawKind As Integer
                If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Inv" Then
                    rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                    objRep.PrintOptions.PaperSize = rawKind
                    objRep.PrintToPrinter(1, False, 0, 0)
                    Exit For
                End If
            Next
            If MsgBox("Do you want to preview the sales order", MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.No Then
            Else
                Dim objCRV As New frmPrintPreview
                With objCRV
                    .CRV.ReportSource = objRep
                    .Text = "Sales Order Printing"
                    .Show()
                    .TopMost = True
                End With
            End If
            'End If



            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

            'saveIBT(iSONo, True, iIBTNo)








        Catch ex As Exception
            'Trans1.Rollback()
            'If Con1.State = ConnectionState.Open Then Con1.Close()
            'Trans2.Rollback()
            'If Con2.State = ConnectionState.Open Then Con2.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Pastel Evolution")
            Exit Sub

        Finally

            'If Con1.State = ConnectionState.Open Then Con1.Close()
            If Con2.State = ConnectionState.Open Then Con2.Close()

        End Try


        ''delete automatically geneated PO
        ''using SDK
        'Dim iso As Integer = iSONo
        'Dim SalesOrderP As New SalesOrder
        'SalesOrderP = New SalesOrder(iso)
        'SalesOrderP.Delete()

        ''using SQL

        'If IsNew = False Then

        '    Dim objSQL As New clsSqlConn
        '    With objSQL
        '        Try
        '            .Begin_Trans()
        '            SQL = " DELETE FROM InvNum WHERE AutoIndex = " & iSONo & " AND DocState = 1 AND DocType = 5 "
        '            If .Execute_Sql_Trans(SQL) = 0 Then
        '                .Rollback_Trans()
        '                Exit Sub
        '            ElseIf .Execute_Sql_Trans(SQL) = 1 Then 'check whether invoice exsist
        '                SQL = " DELETE FROM _btblInvoiceLines WHERE iInvoiceID = " & iSONo & " "
        '                .Execute_Sql_Trans(SQL)
        '            End If
        '            .Commit_Trans()
        '        Catch ex As Exception

        '        End Try
        '    End With
        'End If


        Call Discard()
    End Sub

    Sub validateSerials()

        Dim objSQL As New clsSqlConn

        Try
            With objSQL
                For Each ugR In UG.Rows

                    If ugR.Cells("IsSerialNo").Value = True Then
                        If ugR.ChildBands.HasChildRows = False Then

                            MsgBox("Enter Serial Numbers" & vbCrLf & " Item = " & ugR.Cells("Description_1").Value, MsgBoxStyle.Exclamation, "Pastel Evolution")

                            Exit Sub
                        End If

                        Dim ugR2 As UltraGridRow

                        For Each ugR2 In ugR.ChildBands(0).Rows

                            Dim sn As String = CStr(ugR2.Cells("SerialNumber").Value)

                            SQL = "select SerialCounter FROM SerialMF WHERE SerialNumber ='" & CStr(ugR2.Cells("SerialNumber").Value) & "' and CurrentLoc = 1 and CurrentAccLink = " & iWH
                            If .Check_Data(SQL) = 0 Then
                                MsgBox("item Code  = " & ugR.Cells("Description_1").Value & vbCrLf & "  Serial No = " & CStr(ugR2.Cells("SerialNumber").Value) & "Not belongs to this warehouse", MsgBoxStyle.Exclamation, "Pastel Evolution")
                                Exit Sub
                            End If

                        Next

                    End If
                Next

                MsgBox(" No Errors were found", MsgBoxStyle.Exclamation, "Pastel Evolution")


            End With

        Catch ex As Exception
            MsgBox(Err.Description)

        Finally
            objSQL = Nothing
            GC.Collect()
        End Try

    End Sub

    Sub getDefaultwh(ByRef sError As String, ByRef iToWH1 As Integer)

        Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & cmbGRVLoc.Text.Trim & "")

        Dim objSQL As New clsSqlConn
        Dim dr1 As DataRow
        For Each dr In DS.Tables(0).Rows
            Try

                DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
                DatabaseContext.SetLicense("DE09110022", "1428511")
                DatabaseContext.CreateConnection(sSQLSrvName, "dbUdawatta_new", sSQLSrvUserName, sSQLSrvPassword, False)

                ' this is connection to PO Database open is correct

                Con2.Open()

                CMD = New SqlCommand(SQL, Con2)
                CMD.CommandType = CommandType.Text
                CMD.ExecuteNonQuery()

                SQL = " SELECT WhseLink FROM WhseMst WHERE DefaultWhse = 1"

                CMD = New SqlCommand(SQL, Con2)
                CMD.CommandType = CommandType.Text
                Dim supDS As New DataSet
                Dim supDA As New SqlDataAdapter(CMD)

                supDA.Fill(supDS)

                For Each dr1 In supDS.Tables(0).Rows

                    iToWH1 = dr1.Item("WhseLink")

                Next

                sError = ""

            Catch ex As Exception
                MsgBox(ex.Message)
                iToWH1 = 0
                sError = ex.Message
                Exit Sub
            Finally
                If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
                If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
                If Con2.State = ConnectionState.Open Then Con2.Close()
                dr1 = Nothing
            End Try
        Next
    End Sub

    Sub MemorizeStock()
        Dim ugr As UltraGridRow
        'Dim intStockID As Integer
        'Dim sn As String
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                .Begin_Trans()

                ''''''''''''''''''''''''''''''''Delete all Previous data from the tables''''''''''''''''''''''''''''''
                'SQL = "delete FROM InvNum WHERE AutoIndex =" & CInt(iOldSoNo)
                'If .Execute_Sql_Trans(SQL) = 0 Then
                '    .Rollback_Trans()
                '    Exit Sub
                'End If

                'SQL = "delete FROM _btblInvoiceLines WHERE iInvoiceID =" & CInt(iOldSoNo)
                'If .Execute_Sql_Trans(SQL) = 0 Then
                '    .Rollback_Trans()
                '    Exit Sub
                'End If

                'SQL = "delete FROM _btblInvoiceLineSN WHERE iSerialInvoiceLineID =" & CInt(iOldSoNo)
                'If .Execute_Sql_Trans(SQL) = 0 Then
                '    .Rollback_Trans()
                '    Exit Sub
                'End If

                'SQL = "delete FROM sbSerialMF WHERE AutoIndex =" & CInt(iOldSoNo)
                'If .Execute_Sql_Trans(SQL) = 0 Then
                '    .Rollback_Trans()
                '    Exit Sub
                'End If

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                SQL = "delete FROM sbSerialMF WHERE AutoIndex =" & CInt(iSONo)
                If .Execute_Sql_Trans(SQL) = 0 Then
                    .Rollback_Trans()
                    Exit Sub
                End If

                For Each ugr In UG.Rows

                    If ugr.Cells("IsSerialNo").Value = True Then
                        If ugr.ChildBands.HasChildRows = True Then
                            Dim ugR2 As UltraGridRow
                            For Each ugR2 In ugr.ChildBands(0).Rows
                                SQL = "INSERT INTO sbSerialMF(AutoIndex, SNStockLink, SerialNumber, " & _
                                    " PrimaryLineID) VALUES (" & CInt(iSONo) & "," & CInt(ugr.Cells("StockID").Value) & ", " & _
                                    " '" & CStr(ugR2.Cells("SerialNumber").Value) & "' , " & ugR2.ParentRow.Cells("Line").Value & " )"
                                If .Execute_Sql_Trans(SQL) = 0 Then
                                    .Rollback_Trans()
                                    Exit Sub
                                End If
                            Next
                        End If

                    End If
                Next

                .Commit_Trans()

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
            Finally
                objSQL.Con_Close()
                GC.Collect()
            End Try

        End With

    End Sub

    Sub MemorizePOStock(ByRef iPONo As Integer)
        Dim InventoryItem As Pastel.Evolution.InventoryItem
        Dim stkLink As Integer

        Try
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & cmbGRVLoc.Text.Trim & "")

            CMD = New SqlCommand(SQL, Con2)
            CMD.CommandType = CommandType.Text
            Con2.Open()
            SQL = "delete FROM sbSerialMF WHERE AutoIndex =" & CInt(iPONo)
            CMD = New SqlCommand(SQL, Con2)
            CMD.CommandType = CommandType.Text
            CMD.ExecuteNonQuery()

            For Each ugr In UG.Rows
                If ugr.Cells("IsSerialNo").Value = True Then
                    If ugr.ChildBands.HasChildRows = True Then

                        InventoryItem = New InventoryItem(CStr(ugr.Cells("Code").Text))
                        stkLink = InventoryItem.ID

                        Dim ugR2 As UltraGridRow

                        For Each ugR2 In ugr.ChildBands(0).Rows
                            SQL = "INSERT INTO sbSerialMF(AutoIndex, SNStockLink, SerialNumber, " & _
                                " PrimaryLineID) VALUES (" & CInt(iPONo) & "," & stkLink & ", " & _
                                " '" & CStr(ugR2.Cells("SerialNumber").Value) & "' , " & ugR2.ParentRow.Cells("Line").Value & " )"
                            CMD = New SqlCommand(SQL, Con2)
                            CMD.CommandType = CommandType.Text
                            CMD.ExecuteNonQuery()
                        Next

                    End If
                End If
            Next
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Catch ex As Exception
            MsgBox(ex.Message)
            Exit Sub
        Finally
            Con2.Close()
        End Try

    End Sub


    Public Sub Get_Total()

        Dim ugR As UltraGridRow
        Dim dblOrderTax As Double = 0
        Dim dblLineTax As Double = 0
        Dim dblOrderTotal_Incl As Double = 0
        Dim dblLineTotal_Incl As Double = 0
        Dim dblLineTotal_Incl_Dis As Double = 0
        Dim dblLineTax_Dis As Double = 0
        For Each ugR In UG.Rows
            dblOrderTax = dblOrderTax + Math.Round(ugR.Cells("OrderTax").Value, 2, MidpointRounding.AwayFromZero)
            dblOrderTotal_Incl = dblOrderTotal_Incl + Math.Round(ugR.Cells("OrderTotal_Incl").Value, 2, MidpointRounding.AwayFromZero)
            dblLineTax = dblLineTax + Math.Round(ugR.Cells("LineTax").Value, 2, MidpointRounding.AwayFromZero)
            dblLineTotal_Incl = dblLineTotal_Incl + Math.Round(ugR.Cells("LineTotal_Incl").Value, 2, MidpointRounding.AwayFromZero)
        Next

        txtOrdTax.Value = Math.Round(dblOrderTax, 2, MidpointRounding.AwayFromZero)
        txtOrdAmt.Value = Math.Round(dblOrderTotal_Incl, 2, MidpointRounding.AwayFromZero)
        txtConTax.Value = Math.Round(dblLineTax, 2, MidpointRounding.AwayFromZero)
        txtConAmt.Value = Math.Round(dblLineTotal_Incl, 2, MidpointRounding.AwayFromZero)
        txtConAmt.Value = Math.Round(dblLineTotal_Incl_Dis, 2, MidpointRounding.AwayFromZero)

        If txtConTax.Value > 0 Then
            dblLineTax_Dis = Math.Round((dblLineTotal_Incl_Dis * 100 / 112), 2, MidpointRounding.AwayFromZero)
            dblLineTax_Dis = Math.Round(dblLineTotal_Incl_Dis - dblLineTax_Dis, 2, MidpointRounding.AwayFromZero)
            txtConTax.Value = Math.Round(dblLineTax_Dis, 2, MidpointRounding.AwayFromZero)
        End If

    End Sub


    Public Sub GET_NEXT_DELIVERY_NO()
        SQL = "SELECT  NextDNNo,DN_POPrefix, DNoPadLgth FROM OrdersDf WHERE DefaultCounter = 1"
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                If DS.Tables(0).Rows.Count = 0 Then
                    sDeliveryNo = "DNU"
                Else
                    For Each Dr In DS.Tables(0).Rows
                        Select Case Dr("DNoPadLgth")
                            Case 1
                                sDeliveryNo = Dr("DN_POPrefix") & Format(Dr("NextDNNo"), "0")
                            Case 2
                                sDeliveryNo = Dr("DN_POPrefix") & Format(Dr("NextDNNo"), "00")
                            Case 3
                                sDeliveryNo = Dr("DN_POPrefix") & Format(Dr("NextDNNo"), "000")
                            Case 4
                                sDeliveryNo = Dr("DN_POPrefix") & Format(Dr("NextDNNo"), "0000")
                            Case 5
                                sDeliveryNo = Dr("DN_POPrefix") & Format(Dr("NextDNNo"), "00000")
                            Case 6
                                sDeliveryNo = Dr("DN_POPrefix") & Format(Dr("NextDNNo"), "000000")
                            Case 7
                                sDeliveryNo = Dr("DN_POPrefix") & Format(Dr("NextDNNo"), "0000000")
                            Case 8
                                sDeliveryNo = Dr("DN_POPrefix") & Format(Dr("NextDNNo"), "00000000")
                            Case 9
                                sDeliveryNo = Dr("DN_POPrefix") & Format(Dr("NextDNNo"), "000000000")
                        End Select
                    Next
                End If
                txtDelNote.Text = sDeliveryNo
            Catch ex As Exception
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try
        End With
    End Sub





    Private Sub UG_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UG.InitializeLayout
        'e.Layout.Bands(0).Columns("Lot").ValueList = DDLot
    End Sub

    Private Sub DDDescription_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles DDDescription.InitializeLayout

    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Dim Item As Integer = 0
        Dim Line As Integer = 0
        Dim prvStk As Integer = 0


        Dim objSQL As New clsSqlConn
        With objSQL


            SQL = " SELECT DISTINCT ztable.[Part No], ztable.[System No], ztable.Cost, ztable.Price  FROM  ztable "




            'SQL = " SELECT DISTINCT " & _
            '"  _etblLotTracking.iStockID, WhseStk.WHStockLink, SUM(_etblLotTrackingQty.fQtyOnHand) AS LotQ,  WhseStk.WHQtyOnHand AS WHQ, " & _
            '"  MIN(_etblLotTrackingQty.iLotTrackingID) AS lot " & _
            '" FROM         WhseStk INNER JOIN " & _
            '"  _etblLotTracking ON WhseStk.WHStockLink = _etblLotTracking.iStockID INNER JOIN " & _
            '"  _etblLotTrackingQty ON _etblLotTracking.idLotTracking = _etblLotTrackingQty.iLotTrackingID AND " & _
            '"  WhseStk.WHWhseID = _etblLotTrackingQty.iWarehouseID " & _
            '" GROUP BY _etblLotTracking.iStockID, WhseStk.WHStockLink, WhseStk.WHQtyOnHand " & _
            '" ORDER BY _etblLotTracking.iStockID "

            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
            If DS.Tables(0).Rows.Count > 0 Then
                Dim dr As DataRow
                .Begin_Trans()
                For Each dr In DS.Tables(0).Rows

                    'SQL = " SELECT _btblInvoiceLines.iStockCodeID, StkItem.SerialItem " & _
                    '" FROM _btblInvoiceLines INNER JOIN " & _
                    '" StkItem ON _btblInvoiceLines.iStockCodeID = StkItem.StockLink where iInvoiceID = " & dr("AutoIndex") & ""
                    'SQL = " SELECT     StkItem.cSimpleCode, dbUdawatta.dbo._etblPriceListPrices.fInclPrice, dbUdawatta.dbo._etblPriceListPrices.fExclPrice " & _
                    '" FROM  dbUdawatta.dbo._etblPriceListPrices RIGHT OUTER JOIN " & _
                    '"                      StkItem ON dbUdawatta.dbo._etblPriceListPrices.iStockID = StkItem.StockLink  where StkItem.ItemActive = 1"

                    'DS1 = New DataSet
                    'DS1 = .Get_Data_Sql(SQL)
                    'If DS1.Tables(0).Rows.Count > 0 Then
                    '    Dim dr1 As DataRow
                    '    For Each dr1 In DS1.Tables(0).Rows
                    'Line += 1
                    'SQL = " Update sbSerialMF Set PrimaryLineID = " & Line & " Where AutoIndex = " & dr("AutoIndex") & " and SNStockLink = " & dr1("iStockCodeID") & "  "
                    'If dr1("ItemGroup") <> "" Then

                    'SQL = " SELECT StockLink from StkItem where csimplecode = '" & dr1("csimplecode") & "' "
                    'DS = New DataSet
                    'DS = .Get_Data_Sql(SQL)

                    'If DS.Tables(0).Rows(0)(0) <> "" Then
                    'Dim sa As Integer
                    'sa = 0


                    'SQL = " SELECT  StockLink, Description_2 FROM   StkItem WHERE StockLink = " & dr("StockLink") & " "
                    'DS3 = New DataSet
                    'DS3 = .Get_Data_Sql(SQL)

                    'If DS3.Tables(0).Rows.Count > 0 Then
                    '    Dim s As Integer
                    '    For Each dr3 In DS3.Tables(0).Rows
                    'SQL = " Update StkItem Set ItemGroup = '" & dr3("ItemGroup")  & "' where  StockLink = '" & dr3("StockLink") & "'"
                    'If dr("LotQ") < dr("WHQ") Then
                    'SQL = " update _etblLotTrackingQty set fQtyOnHand = '" & dr("WHQ")  & "' where  iLotTrackingID =  '" & dr("lot") & "'  "

                    'If IsDBNull(dr("Price")) Or IsDBNull(dr("StockLink")) Then
                    'Else
                    'SQL = " update StkItem set AveUCst = " & dr("Cost") & "  where StockLink = " & dr("System No") & " "
                    'SQL += " update _btblInvoiceLines set fUnitPriceExcl = " & dr("Cost") & ", fUnitPriceIncl = " & dr("Cost") & " where iInvoiceID = 16583 and iStockCodeID = " & dr("System No") & " "
                    SQL = " update _etblPriceListPrices set fExclPrice = " & dr("Price") & ", fInclPrice = " & dr("Price") & " , _etblPriceListPrices_iBranchID = 1  where iStockID = (SELECT  distinct   iStockID  FROM        StkItem INNER JOIN    _etblPriceListPrices ON StkItem.StockLink = _etblPriceListPrices.iStockID         where StkItem.Description_1 = '" & dr("Part No") & "') "

                    'SQL = " update StkItem set cRevision =  'FAST' where Description_1 = '" & dr("Part No") & "' "

                    'try to use price list updater forf method..........................................................
                    SQL = SQL + " update dbUdawatta_new.dbo._etblPriceListPrices set fExclPrice = " & dr("Price") & " , fInclPrice =  " & dr("Price") & " , _etblPriceListPrices_iBranchID = 1   where iStockID = (SELECT  distinct   iStockID  FROM         dbUdawatta_new.dbo.StkItem INNER JOIN     dbUdawatta_new.dbo._etblPriceListPrices ON dbUdawatta_new.dbo.StkItem.StockLink = dbUdawatta_new.dbo._etblPriceListPrices.iStockID         where StkItem.Description_1 = '" & dr("Part No") & "') "
                    SQL = SQL + " update dbNawinna_new.dbo._etblPriceListPrices set fExclPrice = " & dr("Price") & " , fInclPrice =  " & dr("Price") & " , _etblPriceListPrices_iBranchID = 1   where iStockID = (SELECT  distinct   iStockID  FROM         dbNawinna_new.dbo.StkItem INNER JOIN     dbNawinna_new.dbo._etblPriceListPrices ON dbNawinna_new.dbo.StkItem.StockLink = dbNawinna_new.dbo._etblPriceListPrices.iStockID         where StkItem.Description_1 = '" & dr("Part No") & "') "


                    'SQL = " update _etblPriceListPrices set fMarkupRate = 25, bUseMarkup = 1, fInclPrice = (" & dr("AveUCst") & " * .25) + " & dr("AveUCst") & ", fExclPrice = (" & dr("AveUCst") & " * .25) + " & dr("AveUCst") & "  where _etblPriceListPrices.iStockID =  '" & dr("StockLink") & "' "
                    'SQL = " delete from _etblInvJrBatchLines where iStockID = " & dr("WHStockLink") & " and iInvJrBatchID = 23 "

                    .Execute_Sql_Trans(SQL)

                    'Else
                    'SQL = " update _etblPriceListPrices set fExclPrice = '" & dr("fExclPrice") & "', fInclPrice = '" & dr("fInclPrice") & "' where  iStockID =  ( select  _etblPriceListPrices.iStockID FROM         StkItem INNER JOIN _etblPriceListPrices ON StkItem.StockLink = _etblPriceListPrices.iStockID where StkItem.cSimpleCode = '" & dr("cSimpleCode") & "' AND iPriceListNameID = 3  ) and iPriceListNameID = 3  "

                    '    .Execute_Sql_Trans(SQL)
                    'End If


                    'End If

                    's = s + 1
                    '    Next
                    'Else
                    'For s As Integer = 1 To 3
                    '    If IsDBNull(dr1("fExclPrice")) Then
                    '    Else
                    '        SQL = " insert into _etblPriceListPrices (iPriceListNameID,[iStockID]  ,[bUseMarkup]  ,[iMarkupOnCost]   ,[fMarkupRate]  ,[fExclPrice] ,[fInclPrice]) values (" & s & ",'" & DS.Tables(0).Rows(0)(0) & "', 0,0,0, " & dr1("fExclPrice") & ", " & dr1("fInclPrice") & " ) "
                    '        .Begin_Trans()
                    '        .Get_Data_Sql_Trans(SQL)
                    '        .Commit_Trans()
                    '        s = s + 1
                    '    End If
                    'Next
                    'End If
                    'End If
                Next
                .Commit_Trans()
                'Line = 0
            End If
            'Next
            'End If
        End With

    End Sub

    Private Sub cmbCustomer_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles cmbCustomer.InitializeLayout

    End Sub

    Private Sub txtBarCode_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBarCode.ValueChanged

    End Sub

    Private Sub txtOrdNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOrdNo.Leave
        txtExtOrder.Text = txtOrdNo.Text
    End Sub

    Private Sub DDLot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DDLot.Click
        Try
            If DDLot.ActiveRow.Activated = True Then
                If UG.ActiveRow.Cells("Quantity").Value > DDLot.ActiveRow.Cells("Qty On Hand").Value Then
                    MsgBox("Quantity not available in Lot", MsgBoxStyle.Information, "Pastel Evolution AddOn")
                    UG.ActiveRow.Cells("Quantity").Value = DDLot.ActiveRow.Cells("Qty On Hand").Value
                End If
                Lot = DDLot.ActiveRow.Cells("Description").Value
                UG.ActiveRow.Cells("Lot").Value = DDLot.ActiveRow.Cells("idLotTracking").Value
                Lot_Exp = DDLot.ActiveRow.Cells("Expiry Date").Value

                If sSQLSrvDataBase <> "dbKelaniya_new" Then
                    If UG.ActiveRow.Cells("Lot").Text.Substring(0, 1) = "K" Then
                        MsgBox("This lot cannot be issued to Kelaniya", MsgBoxStyle.Information, "Pastel Evolution AddOn")
                        UG.ActiveRow.Cells("Lot").Value = 0
                    End If
                End If
            End If
        Catch ex As Exception
            Exit Sub
        Finally
        End Try
    End Sub

    Private Sub UG_AfterEnterEditMode(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UG.AfterEnterEditMode
        If UG.ActiveCell.Column.Key = "Lot" Then
            With UG.ActiveCell
                sSQL = " SELECT   _etblLotTracking.idLotTracking,_etblLotTracking.cLotDescription AS Description, _etblLotTracking.dExpiryDate AS [Expiry Date], _etblLotTrackingQty.fQtyOnHand AS [Qty On Hand], " & _
                " _etblLotTrackingQty.fQtyReserved AS [Qty Reserved]" & _
                " FROM         _etblLotTracking INNER JOIN " & _
                " _etblLotTrackingQty ON _etblLotTracking.idLotTracking = _etblLotTrackingQty.iLotTrackingID WHERE  _etblLotTracking.iStockID = " & UG.ActiveRow.Cells("StockID").Value & " AND _etblLotTrackingQty.fQtyOnHand > 0 "

                Try
                    Con1.ConnectionString = sConStr
                    CMD = New SqlCommand(sSQL, Con1)
                    DS = New DataSet
                    DA = New SqlDataAdapter(CMD)

                    Con1.Open()
                    DA.Fill(DS)
                    Con1.Close()

                    DDLot.DataSource = DS.Tables(0)
                    DDLot.ValueMember = "idLotTracking"
                    DDLot.DisplayMember = "Description"
                    DDLot.DisplayLayout.Bands(0).Columns("idLotTracking").Hidden = True

                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                    Exit Sub
                Finally
                    If Con1.State = ConnectionState.Open Then Con1.Close()
                End Try


            End With
        End If
    End Sub

    Private Sub UG_AfterExitEditMode(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UG.AfterExitEditMode
        If UG.ActiveCell.Column.Key = "Quantity" Then

            'check max reorder level-----------------------------

            If sSQLSrvDataBase = "dbKelaniya_new" Then
                Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & cmbGRVLoc.Text & "")

                Dim objSQL As New clsSqlConn

                With objSQL

                    'bookmark
                    'SQL = "SELECT Max_Lvl FROM StkItem WHERE description_1 ='" & UG.ActiveCell.Row.Cells("description_1").Value & "' and ItemActive = 1 "
                    SQL = "SELECT    WhseStk.WHMax_Lvl  FROM         StkItem INNER JOIN  WhseStk ON StkItem.StockLink = WhseStk.WHStockLink WHERE StkItem.description_1 ='" & UG.ActiveCell.Row.Cells("description_1").Value & "' and StkItem.ItemActive = 1 AND WhseStk.WHWhseID = " & clientWH & " "
                    CMD = New SqlCommand(SQL, Con2)
                    CMD.CommandType = CommandType.Text
                    DA = New SqlDataAdapter(CMD)
                    DS = New DataSet
                    Con2.Open()
                    DA.Fill(DS)
                    Con2.Close()
                    'If cmbGRVLoc.Text <> "dbUdawatta_new" Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        If UG.ActiveCell.Row.Cells("Quantity").Value + UG.ActiveCell.Row.Cells("BranchQty").Value > DS.Tables(0).Rows(0)(0) Then
                            MessageBox.Show("Maximum Quantity allowed is " & DS.Tables(0).Rows(0)(0) & "", "Pastel Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            If DS.Tables(0).Rows(0)(0) - UG.ActiveCell.Row.Cells("BranchQty").Value < 0 Then
                                UG.ActiveCell.Row.Cells("Quantity").Value = 0
                            Else
                                UG.ActiveCell.Row.Cells("Quantity").Value = DS.Tables(0).Rows(0)(0) - UG.ActiveCell.Row.Cells("BranchQty").Value
                            End If
                            Exit Sub
                        End If
                    End If
                    'End If
                End With
                'comented becoz branches can send what ever they want irrespective of max level
                'Else
                '    Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & sSQLSrvDataBase & "")

                '    Dim objSQL As New clsSqlConn

                '    With objSQL
                '        SQL = "SELECT Max_Lvl FROM StkItem WHERE description_1 ='" & UG.ActiveCell.Row.Cells("description_1").Value & "' and ItemActive = 1 "
                '        CMD = New SqlCommand(SQL, Con2)
                '        CMD.CommandType = CommandType.Text
                '        DA = New SqlDataAdapter(CMD)
                '        DS = New DataSet
                '        Con2.Open()
                '        DA.Fill(DS)
                '        Con2.Close()
                '        'If cmbGRVLoc.Text <> "dbUdawatta_new" Then
                '        If DS.Tables(0).Rows.Count > 0 Then
                '            If UG.ActiveCell.Row.Cells("Quantity").Value + UG.ActiveCell.Row.Cells("AvailableQty").Value > DS.Tables(0).Rows(0)(0) Then
                '                MessageBox.Show("Maximum Quantity allowed is " & DS.Tables(0).Rows(0)(0) & "", "Pastel Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '                If DS.Tables(0).Rows(0)(0) - UG.ActiveCell.Row.Cells("AvailableQty").Value < 0 Then
                '                    UG.ActiveCell.Row.Cells("Quantity").Value = 0
                '                Else
                '                    UG.ActiveCell.Row.Cells("Quantity").Value = DS.Tables(0).Rows(0)(0) - UG.ActiveCell.Row.Cells("AvailableQty").Value
                '                End If
                '                Exit Sub
                '            End If
                '        End If
                '        'End If
                '    End With
            End If



            '----------------------------------------------------
        End If
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        If cmbCustomer.Value = Nothing Then
            MessageBox.Show("Please select a customer", "Pastel Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Panel1.Show()
        
    End Sub

    Private Sub tscmbIBT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        cmbCustomer.Enabled = False


        For Each ugR As UltraGridRow In UG.Rows
            ugR.Delete(False)
        Next

        Dim sError As String = ""
        Dim iToWH As Integer = 0
        Dim WHID As Integer = 0

        Dim agent = New Agent(iAgent)


        'getDefaultwh(sError, iToWH)


        If cbAll.Checked = True Then
            SQL = " SELECT     max(StkItem.StockLink) AS StockLink, StkItem.Description_1, StkItem.cSimpleCode, WhseStk.WHQtyOnHand, WhseStk.WHMax_Lvl FROM StkItem INNER JOIN  WhseStk ON StkItem.StockLink = WhseStk.WHStockLink WHERE WhseStk.WHQtyOnHand < WhseStk.WHRe_Ord_Lvl AND  WhseStk.WHQtyOnHand < WhseStk.WHMax_Lvl AND WhseStk.WHWhseID = '" & agent.Description & "' AND ItemActive = 1 Group by StkItem.Description_1,StkItem.cSimpleCode, WhseStk.WHQtyOnHand, WhseStk.WHMax_Lvl ORDER BY StkItem.Description_1 "
        Else
            SQL = " SELECT     max(StkItem.StockLink) AS StockLink, StkItem.Description_1, StkItem.cSimpleCode, WhseStk.WHQtyOnHand, WhseStk.WHMax_Lvl FROM StkItem INNER JOIN  WhseStk ON StkItem.StockLink = WhseStk.WHStockLink WHERE WhseStk.WHQtyOnHand < WhseStk.WHRe_Ord_Lvl AND  WhseStk.WHQtyOnHand < WhseStk.WHMax_Lvl AND WhseStk.WHWhseID = '" & agent.Description & "' AND ItemGroup = '" & cmbAuto.Value & "' AND ItemActive = 1 Group by StkItem.Description_1,StkItem.cSimpleCode, WhseStk.WHQtyOnHand, WhseStk.WHMax_Lvl ORDER BY StkItem.Description_1 "
        End If


        Dim objSQL As New clsSqlConn
        With objSQL

            DS1 = New DataSet
            DS1 = .Get_Data_Sql(SQL)
            If DS1.Tables(0).Rows.Count = 0 Then

            End If
        End With

        For Each drow In DS1.Tables(0).Rows

            Dim ugR As UltraGridRow
            ugR = UG.DisplayLayout.Bands(0).AddNew
            ugR.Cells("Line").Value = ugR.Index + 1
            ugR.Cells("Line").Selected = True
            ugR.Cells("cSimpleCode").Value = drow("cSimpleCode")
            ugR.Cells("StockID").Value = drow("StockLink")
            ugR.Cells("Code").Value = drow("StockLink")


            'ugR.Cells("Warehouse").Value = iWH
            ugR.Cells("Warehouse").Value = agent.Description

            ugR.Cells("AvailableQty").Value = Get_Available_Qty("SELECT WHQtyOnHand FROM WhseStk WHERE WHStockLink =" & ugR.Cells("StockID").Value & " AND WHWhseID =" & ugR.Cells("Warehouse").Value & "")
            ugR.Cells("WHMax_Lvl").Value = drow("WHMax_Lvl")
            If ugR.Cells("BranchQty").Value < drow("WHMax_Lvl") - drow("WHQtyOnHand") Then
                ugR.Cells("Quantity").Value = ugR.Cells("BranchQty").Value
            Else
                ugR.Cells("Quantity").Value = drow("WHMax_Lvl") - drow("WHQtyOnHand")
            End If

            If ugR.Cells("BranchQty").Value = 0 Or ugR.Cells("Quantity").Value = 0 Then
                ugR.Delete(False)
            End If

            If iDocType = DocType.SalesOrder Then
                ugR.Cells("TaxType").Value = 1
                For Each ugR1 As UltraGridRow In DDTaxType.Rows
                    If ugR.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
                        ugR.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
                    End If
                Next
            End If
        Next



        UG.DisplayLayout.Bands(0).Columns("BranchQty").CellActivation = Activation.Disabled
        UG.DisplayLayout.Bands(0).Columns("Warehouse").CellActivation = Activation.Disabled
        UG.DisplayLayout.Bands(0).Columns("WHMax_lvl").CellActivation = Activation.Disabled


        Panel1.Hide()
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAll.CheckedChanged
        If cbAll.Checked = True Then
            cmbAuto.Enabled = False
        End If
        If cbAll.Checked = False Then
            cmbAuto.Enabled = True
        End If
    End Sub
    
    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click

        '=========================another methid using PrintHandler.dll===========================
        'Dim aPrintObj As New PrintHandler

        'With aPrintObj
        '    .LineThreshold = 500
        '    .NumberOfColumns = aNumColumns
        '    .DataSetToPrint = myDataSet
        '    .ReportTitle = "Customer List"
        '    .DataSetToPrint = myDataSet
        '    .TableIndex = aTblIndex

        '    If blnPreview Then
        '        .PrintPreview()
        '    Else
        '        .Print()
        '    End If
        'End With

        'aPrintObj = Nothing
        '-=========================================================================================

        Dim prtDoc As New Printing.PrintDocument()
        'prtDoc.DefaultPageSettings.Landscape = True

        prtDoc.DefaultPageSettings.Margins.Left = 0
        prtDoc.DefaultPageSettings.Margins.Right = 0
        prtDoc.PrinterSettings.PrinterName = DefaultPrinterName
        'Me.UG.PrintPreview(Me.UG.DisplayLayout, prtDoc)
        prtDoc.DefaultPageSettings.PrinterSettings.PrintRange = PrintRange.CurrentPage

        Dim psz As New Printing.PaperSize
        With psz
            .RawKind = Printing.PaperKind.Custom
            .Width = 830
            .Height = 2000
        End With
        prtDoc.DefaultPageSettings.PaperSize = psz
        'prtDoc.DefaultPageSettings.PaperSize.PaperName = "Letter"

        Me.UG.Print(Me.UG.DisplayLayout, prtDoc)

        tsbSave.Enabled = True
        'Dim myFineName As String


        Try

            '    Dim fdlg As SaveFileDialog = New SaveFileDialog
            '    fdlg.Title = "Excel Data Importer"
            '    fdlg.InitialDirectory = "C:\"
            '    fdlg.Filter = "MS Excel (*.xls)|*.xls"

            '    fdlg.RestoreDirectory = True

            '    myFineName = ""

            '    If fdlg.ShowDialog() = DialogResult.OK Then
            '        myFineName = fdlg.FileName
            '    End If


            '    If myFineName = "" Then
            '        MsgBox("Please enter a file name", MsgBoxStyle.Information, "Validation")
            '        Exit Sub
            '    End If

            '    ugExcelExporter.Export(UG, myFineName)


            '    Dim Proc As New System.Diagnostics.Process
            '    Proc.StartInfo.FileName = myFineName
            'Proc.Start()

        Catch ex As Exception
            Exit Sub
        End Try



    End Sub

    Private Sub UG_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles UG.KeyPress

    End Sub

    Private Sub MenuStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub AaaToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AaaToolStripMenuItem1.Click
        Dim report As New ReportDocument

        report.PrintOptions.PrinterName = "F2"
        report.Load(Application.StartupPath & "\F2.rpt", OpenReportMethod.OpenReportByDefault)

        report.RecordSelectionFormula = "{InvNum.AutoIndex}=14991"
        ApplyLoginToTable(report)

        report.PrintToPrinter(1, False, 0, 0)
    End Sub

    Private Sub PrintSalesOrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintSalesOrderToolStripMenuItem.Click

    End Sub
End Class