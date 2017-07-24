Imports Infragistics.Win.UltraWinGrid
Imports System.Data
Imports System.Data.SqlClient
Imports Pastel.Evolution
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing
Public Class frmIBTReceive
    Dim sQuoteNo As String
    Dim sOrderNo As String
    Dim sDeliveryNo As String
    Dim sInvoiceNo As String
    Dim sGrvNo As String
    Dim sPONo As String
    Public iPONo As Integer
    Dim mDr As DataRow
    Public IsNew As Boolean
    Public iDocType As Integer
    Public iDocState As Integer
    Public iCusPriceList As Integer = 0
    Public bIsInclusive As Boolean = False
    Public iWH As Integer = 0
    Public bIsAllowOverrideCreditLimit As Boolean = False
    Public bHideCost As Boolean = False
    Public DS1 As DataSet
    Public OEPrice As Double
    Public OIPrice As Double
    Public iRTNNo As Int64 = 0
    Public sSONo As String
    Public iOldRTNNo As Integer = 0
    Public clientWH As Integer
    Public iSONo As Int64 = 0

    Public Sub DeleteRows()
        Dim ugR As UltraGridRow
        For Each ugR In UG.Rows.All
            ugR.Delete(False)
        Next
    End Sub
    Public Sub GET_DATA()


        SQL = "SELECT DCLink, Account, Name, Physical1, Physical2," & _
                  " Physical3, Post1, Post2, Post3 , Discount , cAccDescription, cIDNumber FROM Vendor  Order By Name "

        SQL += " SELECT idSalesRep, Code, Name FROM SalesRep WHERE Rep_On_Hold = 'N'  Order By Name "
        SQL += " SELECT ProjectLink, ProjectCode, ProjectName,ProjectDescription FROM Project Order By projectName"
        SQL += " SELECT StatusCounter, StatusDescrip FROM OrdersSt"
        SQL += " SELECT idIncidentPriority, cDescription FROM _rtblIncidentPriority"
        SQL += " SELECT     StkItem.StockLink, StkItem.Code, StkItem.Description_1, StkItem.ItemGroup, StkItem.bLotItem AS LotItem, StkItem.WhseItem, StkItem.Description_2, " & _
        " StkItem.iUOMStockingUnitID, _etblInvSegValue.cValue " & _
        " FROM         StkItem INNER JOIN" & _
         "  _etblInvSegValue ON StkItem.iInvSegValue1ID = _etblInvSegValue.idInvSegValue " & _
         "  WHERE(StkItem.ItemActive = 1) " & _
        " ORDER BY StkItem.Description_1 "
        SQL += " SELECT WhseLink, Code, Name, KnownAs FROM WhseMst WHERE KnownAs = 'PKW1' OR KnownAs = 'PKW2'"
        SQL += " SELECT idTaxRate, Code, Description, TaxRate FROM TaxRate"
        SQL += " SELECT bIsInclusive FROM StDfTbl"
        SQL += " SELECT WhseLink FROM WhseMst WHERE DefaultWhse = 1"
        SQL += " SELECT idAreas, Code, Description FROM Areas"
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                'If DS.Tables(0).Rows.Count > 0 Then
                cmbCustomer.DataSource = DS.Tables(0)
                cmbCustomer.ValueMember = "DCLink"
                cmbCustomer.DisplayMember = "Name"
                'End If
                cmbCustomer.DisplayLayout.Bands(0).Columns("DCLink").Hidden = True
                cmbCustomer.DisplayLayout.Bands(0).Columns("Physical1").Hidden = True
                cmbCustomer.DisplayLayout.Bands(0).Columns("Physical2").Hidden = True
                cmbCustomer.DisplayLayout.Bands(0).Columns("Physical3").Hidden = True
                cmbCustomer.DisplayLayout.Bands(0).Columns("Post1").Hidden = True
                cmbCustomer.DisplayLayout.Bands(0).Columns("Post2").Hidden = True
                cmbCustomer.DisplayLayout.Bands(0).Columns("Post3").Hidden = True
                If iDocType = DocType.SalesOrder Then
                    'cmbCustomer.DisplayLayout.Bands(0).Columns("bCODAccount").Hidden = True
                End If

                'Sales Rep
                'If DS.Tables(1).Rows.Count > 0 Then
                cmbSaleRep.DataSource = DS.Tables(1)
                cmbSaleRep.ValueMember = "idSalesRep"
                cmbSaleRep.DisplayMember = "Name"
                'End If
                cmbSaleRep.DisplayLayout.Bands(0).Columns("idSalesRep").Hidden = True

                'Projects
                'If DS.Tables(2).Rows.Count > 0 Then
                cmbProject.DataSource = DS.Tables(2)
                cmbProject.ValueMember = "ProjectLink"
                cmbProject.DisplayMember = "ProjectName"
                'End If
                cmbProject.DisplayLayout.Bands(0).Columns("ProjectLink").Hidden = True

                'Order Status
                'If DS.Tables(3).Rows.Count > 0 Then
                cmbOrderStatus.DataSource = DS.Tables(3)
                cmbOrderStatus.ValueMember = "StatusCounter"
                cmbOrderStatus.DisplayMember = "StatusDescrip"
                'End If
                cmbOrderStatus.DisplayLayout.Bands(0).Columns("StatusCounter").Hidden = True

                'Priority
                'If DS.Tables(4).Rows.Count > 0 Then
                cmbPriority.DataSource = DS.Tables(4)
                cmbPriority.ValueMember = "idIncidentPriority"
                cmbPriority.DisplayMember = "cDescription"
                'End If
                cmbPriority.DisplayLayout.Bands(0).Columns("idIncidentPriority").Hidden = True

                'Stock
                'If DS.Tables(5).Rows.Count > 0 Then
                DDStock.DataSource = DS.Tables(5)
                DDStock.ValueMember = "StockLink"
                DDStock.DisplayMember = "Code"
                'End If
                DDStock.DisplayLayout.Bands(0).Columns("Description_1").Header.Caption = "Description"
                'If DS.Tables(5).Rows.Count > 0 Then
                DDDescription.DataSource = DS.Tables(5)
                DDDescription.ValueMember = "Description_1"
                DDDescription.DisplayMember = "Description_1"
                'End If
                DDDescription.DisplayLayout.Bands(0).Columns("Description_1").Header.Caption = "Description"

                'WH
                'If DS.Tables(6).Rows.Count > 0 Then
                DDWH.DataSource = DS.Tables(6)
                DDWH.ValueMember = "WhseLink"
                DDWH.DisplayMember = "Code"
                'End If
                DDWH.DisplayLayout.Bands(0).Columns("WhseLink").Hidden = True

                'Tax
                'If DS.Tables(7).Rows.Count > 0 Then
                DDTaxType.DataSource = DS.Tables(7)
                DDTaxType.ValueMember = "idTaxRate"
                DDTaxType.DisplayMember = "Code"
                'End If
                DDTaxType.DisplayLayout.Bands(0).Columns("idTaxRate").Hidden = True

                Dim Dr As DataRow
                For Each Dr In DS.Tables(8).Rows
                    If Dr("bIsInclusive") = False Then
                        bIsInclusive = False
                    ElseIf Dr("bIsInclusive") = True Then
                        bIsInclusive = True
                    End If
                Next
                For Each Dr In DS.Tables(9).Rows
                    iWH = CInt(Dr("WhseLink"))
                Next

                'Area
                'cmbArea.DataSource = DS.Tables(10)
                'cmbArea.ValueMember = "idAreas"
                'cmbArea.DisplayMember = "Description"
                ''End If
                'cmbArea.DisplayLayout.Bands(0).Columns("idAreas").Hidden = True
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                Exit Sub
            Finally
                objSQL = Nothing
            End Try
        End With
    End Sub
    Public Sub DocumentSettings(ByVal iDocType As Integer)
        Select Case iDocType
            Case DocType.PurchaseOrder
                lbl1.Text = " GRV Number"
                lbl2.Text = " Order Number"
                lbl3.Text = " Supplier Invoice"
                txtDelNote_SupInvNo.Enabled = True
                cmbSaleRep.Visible = False
                lbl4.Text = " Supplier"
                lbl5.Appearance.BackColor = Color.Maroon
                lbl5.Appearance.BackColor2 = Color.DarkRed
                lbl5.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50
                lbl6.Appearance.BackColor = Color.Maroon
                lbl6.Appearance.BackColor2 = Color.DarkRed
                lbl6.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50
                lblSalesRep.Visible = False
                lblBarcode.Visible = True
                txtBarCode.Visible = True
                lblOrderType.Visible = True
                cmbType.Visible = True
            Case DocType.SalesOrder Or DocType.Invoice
                lbl1.Text = " Invoice Number"
                lbl2.Text = " Order Number"
                lbl3.Text = " Delivery Note"
                txtDelNote_SupInvNo.Enabled = False
                cmbSaleRep.Visible = True
                lbl4.Text = " Customer"
                lbl5.Appearance.BackColor = Color.Green
                lbl5.Appearance.BackColor2 = Color.DarkGreen
                lbl5.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50
                lbl6.Appearance.BackColor = Color.Green
                lbl6.Appearance.BackColor2 = Color.DarkGreen
                lbl6.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50
                lblSalesRep.Visible = True
                lblBarcode.Visible = True
                txtBarCode.Visible = True
                lblOrderType.Visible = False
                cmbType.Visible = False
            Case DocType.IBTReceive
                lbl1.Text = " GRV Number"
                lbl2.Text = " Order Number"
                lbl3.Text = " Supplier Invoice"
                txtDelNote_SupInvNo.Enabled = True
                cmbSaleRep.Visible = False
                lbl4.Text = " Supplier"
                lbl5.Appearance.BackColor = Color.Maroon
                lbl5.Appearance.BackColor2 = Color.DarkRed
                lbl5.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50
                lbl6.Appearance.BackColor = Color.Maroon
                lbl6.Appearance.BackColor2 = Color.DarkRed
                lbl6.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50
                lblSalesRep.Visible = False
                lblBarcode.Visible = True
                txtBarCode.Visible = True
                lblOrderType.Visible = True
                cmbType.Visible = True

        End Select
    End Sub

    Private Sub frmSalesOrder_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Discard()
    End Sub
    Public Sub SetSatus()
        If IsNew = True Then
            lblStatus.Text = "NEW"
            If iDocType = DocType.SalesOrder Then
                tsbSaveSO.Enabled = True
                tsbUpdateSO.Enabled = True
                tsbSavePO.Visible = False
                tsbUpdatePO.Visible = False
                tsbProcessPO.Visible = False

            ElseIf iDocType = DocType.IBTReceive Then
                tsbSavePO.Enabled = True
                tsbUpdatePO.Enabled = False
                tsbProcessPO.Enabled = False
                tsbSaveSO.Visible = False
                tsbUpdateSO.Visible = False
            End If
        Else
            lblStatus.Text = "EDIT"
            If iDocType = DocType.SalesOrder Then
                tsbSaveSO.Enabled = True
                tsbUpdateSO.Enabled = True
                tsbSavePO.Visible = False
                tsbUpdatePO.Visible = False
                tsbProcessPO.Visible = False
                tsbSaveSO.Visible = True
                tsbUpdatePO.Visible = False
            ElseIf iDocType = DocType.IBTReceive Then
                tsbSavePO.Enabled = False
                tsbUpdatePO.Enabled = True
                tsbProcessPO.Enabled = True
                tsbSaveSO.Visible = False
                tsbUpdatePO.Visible = False
                tsbSavePO.Visible = True
                tsbUpdatePO.Visible = True
                tsbProcessPO.Visible = True
            End If
        End If
    End Sub

    Private Sub frmSalesOrder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyData = Keys.F9 Then
            If iDocType = DocType.SalesOrder Then
                tsbSaveSO_Click(tsbSaveSO, e)
            End If
        End If
    End Sub
    Private Sub frmSalesOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        If iDocType = DocType.SalesOrder Then

            If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sAgent & "IBTReceive.xml") Then
                UG.DisplayLayout.LoadFromXml(Application.StartupPath & "\" & sAgent & "IBTReceive.xml")
            End If

        ElseIf iDocType = DocType.PurchaseOrder Then

            'UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = False
            'UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = False
            'UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = False
            'UG.DisplayLayout.Bands(0).Columns("OrderTotal_incl").Hidden = False

            If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sAgent & "IBTReceive.xml") Then
                UG.DisplayLayout.LoadFromXml(Application.StartupPath & "\" & sAgent & "IBTReceive.xml")
            End If
        ElseIf iDocType = DocType.IBTReceive Then

            'UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = False
            'UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = False
            'UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = False
            'UG.DisplayLayout.Bands(0).Columns("OrderTotal_incl").Hidden = False

            If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sAgent & "IBTReceive.xml") Then
                UG.DisplayLayout.LoadFromXml(Application.StartupPath & "\" & sAgent & "IBTReceive.xml")
            End If



        End If

        If bIsInclusive = True Then
            cbInEx.Checked = True
            cbInEx_CheckedChanged(cbInEx, e)
        ElseIf bIsInclusive = False Then
            cbInEx.Checked = False
            cbInEx_CheckedChanged(cbInEx, e)
        End If

        'UG.DisplayLayout.c = Infragistics.Win.DefaultableBoolean.False

        Dim Dr As DataRow
        For Each Dr In dsManu.Tables(0).Rows
            If iDocType = DocType.IBTReceive Then
                If 22243 = Dr("iManu") Then
                    UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
                    UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
                    UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default
                ElseIf 22215 = Dr("iManu") Then
                    bIsAllowOverrideCreditLimit = True
                End If
                'ElseIf iDocType = DocType.PurchaseOrder Then

                'call me please

                If 22246 = Dr("iManu") Then
                    UG.DisplayLayout.Bands(0).Columns("SerialLot").Hidden = True
                End If
            End If
        Next

        'If sAgent <> "Admin" Then
        'UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
        'UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
        'UG.DisplayLayout.Bands(0).Columns("Price_Incl").CellActivation = Activation.Disabled
        'UG.DisplayLayout.Bands(0).Columns("Price_Incl").MaskInput = "***"
        'UG.DisplayLayout.Bands(0).Columns("Price_Incl").MaskDataMode = Infragistics.Win.UltraWinMaskedEdit.MaskMode.IncludeBoth
        'UG.DisplayLayout.Bands(0).Columns("Price_Incl").MaskClipMode = Infragistics.Win.UltraWinMaskedEdit.MaskMode.IncludeBoth
        'UG.DisplayLayout.Bands(0).Columns("Price_Incl").MaskDisplayMode = Infragistics.Win.UltraWinMaskedEdit.MaskMode.IncludeBoth
        'Dim aaa As String = UG.ActiveRow.Cells("Price_Incl").Value
        'Else
        'UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = False
        'UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = False
        'End If
        UG.DisplayLayout.Bands(0).Columns("DiscountA").Hidden = True
        UG.DisplayLayout.Bands(0).Columns("Discount").Hidden = True
        UG.DisplayLayout.Bands(0).Columns("lot_exp").Hidden = True
        UG.DisplayLayout.Bands(0).Columns("UOM").Hidden = True


        UG.DisplayLayout.Bands(0).Columns("Lot").ValueList = DDLot

        SetSatus()
    End Sub
    Private Sub DDStock_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDStock.Click
        Try
            If DDStock.ActiveRow.Activated = True Then
                UG.ActiveRow.Cells("StockID").Value = DDStock.ActiveRow.Cells("StockLink").Value
                UG.ActiveRow.Cells("Code").Value = DDStock.ActiveRow.Cells("StockLink").Value
                UG.ActiveRow.Cells("Description_1").Value = DDStock.ActiveRow.Cells("Description_1").Value
                UG.ActiveRow.Cells("Description_2").Value = DDStock.ActiveRow.Cells("Description_2").Value
                UG.ActiveRow.Cells("IsSerialNo").Value = IIf(DDStock.ActiveRow.Cells("SerialItem").Value = True, True, False)
                UG.ActiveRow.Cells("IsWH").Value = IIf(DDStock.ActiveRow.Cells("WhseItem").Value = True, True, False)
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Private Sub DDDescription_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDDescription.Click
        Try
            If DDDescription.ActiveRow.Activated = True Then
                UG.ActiveRow.Cells("StockID").Value = DDDescription.ActiveRow.Cells("StockLink").Value
                UG.ActiveRow.Cells("Code").Value = DDDescription.ActiveRow.Cells("StockLink").Value
                UG.ActiveRow.Cells("Description_1").Value = DDDescription.ActiveRow.Cells("Description_1").Value
                UG.ActiveRow.Cells("Description_2").Value = DDDescription.ActiveRow.Cells("Description_2").Value
                UG.ActiveRow.Cells("IsSerialNo").Value = IIf(DDDescription.ActiveRow.Cells("SerialItem").Value = True, True, False)
                UG.ActiveRow.Cells("IsWH").Value = IIf(DDDescription.ActiveRow.Cells("WhseItem").Value = True, True, False)
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Private Sub DDDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DDDescription.KeyDown
        'If e.KeyCode = Keys.Enter Then
        '    If DDDescription.ActiveRow.Activated = True Then
        '        UG.ActiveRow.Cells("StockID").Value = DDDescription.ActiveRow.Cells("StockLink").Value
        '        UG.ActiveRow.Cells("Code").Value = DDDescription.ActiveRow.Cells("StockLink").Value
        '        UG.ActiveRow.Cells("Description_1").Value = DDDescription.ActiveRow.Cells("Description_1").Value
        '        UG.ActiveRow.Cells("Description_2").Value = DDDescription.ActiveRow.Cells("Description_2").Value
        '        UG.ActiveRow.Cells("IsSerialNo").Value = IIf(DDDescription.ActiveRow.Cells("SerialItem").Value = True, True, False)
        '        UG.ActiveRow.Cells("IsWH").Value = IIf(DDDescription.ActiveRow.Cells("WhseItem").Value = True, True, False)

        '    End If
        'End If
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
                bIsInclusive = False
                cbInEx.Checked = False
                cbInEx_CheckedChanged(cbInEx, e)
            End If
        ElseIf cbInEx.Checked = False Then
            If MsgBox(" Change From Tax Excusive To Tax Inclusive ? ", MsgBoxStyle.YesNo, "New Sales Order " & txtOrdNo.Text & "  " & txtInNo.Text) = MsgBoxResult.Yes Then
                bIsInclusive = True
                cbInEx.Checked = True
                cbInEx_CheckedChanged(cbInEx, e)
            End If
        End If
    End Sub
    Private Sub cbInEx_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbInEx.CheckedChanged
        If cbInEx.Checked = True Then
            UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("OrderTotal_Excl").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("LineTotal_Excl").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = False
            UG.DisplayLayout.Bands(0).Columns("OrderTotal_Incl").Hidden = False
            UG.DisplayLayout.Bands(0).Columns("LineTotal_Incl").Hidden = False
            lblDis.Text = "Incl"
        ElseIf cbInEx.Checked = False Then
            UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("OrderTotal_Incl").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("LineTotal_Incl").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = False
            UG.DisplayLayout.Bands(0).Columns("OrderTotal_Excl").Hidden = False
            UG.DisplayLayout.Bands(0).Columns("LineTotal_Excl").Hidden = False
            lblDis.Text = "Excl"
        End If

    End Sub
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
    Private Sub UG_AfterCellUpdate(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs)
        
    End Sub

    Private Function Get_Available_Qty(ByVal SQL As String) As Double
        Try
            Dim oQty As Object
            Con1.ConnectionString = sConStr
            CMD = New SqlCommand(SQL, Con1)
            CMD.CommandType = CommandType.Text
            Con1.Open()
            oQty = CMD.ExecuteScalar
            Con1.Close()
            Return CDbl(oQty)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Sub UG_CellChange(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs)
        Try
            If UG.ActiveCell.Column.Key = "Code" Then
                If DDStock.ActiveRow.Activated = True Then
                    UG.ActiveRow.Cells("StockID").Value = DDStock.ActiveRow.Cells("StockLink").Value
                    UG.ActiveRow.Cells("Code").Value = DDStock.ActiveRow.Cells("StockLink").Value
                    UG.ActiveRow.Cells("Description_1").Value = DDStock.ActiveRow.Cells("Description_1").Value
                    UG.ActiveRow.Cells("Description_2").Value = DDStock.ActiveRow.Cells("Description_2").Value
                    UG.ActiveRow.Cells("IsSerialNo").Value = IIf(DDStock.ActiveRow.Cells("SerialItem").Value = True, True, False)
                    UG.ActiveRow.Cells("IsWH").Value = IIf(DDStock.ActiveRow.Cells("WhseItem").Value = True, True, False)
                End If
            ElseIf UG.ActiveCell.Column.Key = "Description_1" Then
                If DDDescription.ActiveRow.Activated = True Then
                    UG.ActiveRow.Cells("StockID").Value = DDDescription.ActiveRow.Cells("StockLink").Value
                    UG.ActiveRow.Cells("Code").Value = DDDescription.ActiveRow.Cells("StockLink").Value
                    UG.ActiveRow.Cells("Description_1").Value = DDDescription.ActiveRow.Cells("Description_1").Value
                    UG.ActiveRow.Cells("Description_2").Value = DDDescription.ActiveRow.Cells("Description_2").Value
                    UG.ActiveRow.Cells("IsSerialNo").Value = IIf(DDDescription.ActiveRow.Cells("SerialItem").Value = True, True, False)
                    UG.ActiveRow.Cells("IsWH").Value = IIf(DDDescription.ActiveRow.Cells("WhseItem").Value = True, True, False)
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
            ElseIf UG.ActiveCell.Column.Key = "Received" Then


                For Each ugR In UG.Rows
                    If ugR.ChildBands.HasChildRows = True Then
                        ugR.Cells("ConfirmQty").Value = 0
                        For Each ugR1 In ugR.ChildBands(0).Rows
                            If ugR1.Cells("Received").Value = True Then
                                ugR1.ParentRow.Cells("ConfirmQty").Value = ugR1.ParentRow.Cells("ConfirmQty").Value + 1
                                'Exit Sub
                            End If
                        Next
                    End If
                Next
            End If
        Catch ex As Exception
            'MsgBox(Err.Description)
            Exit Sub
        End Try
    End Sub
    
    Public Sub GET_NEXT_GRV_NO()
        SQL = "SELECT GrvPref, GrvNum, GRVPad FROM StDfTbl"
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                If DS.Tables(0).Rows.Count = 0 Then
                    sGrvNo = "DNU"
                Else
                    For Each Dr In DS.Tables(0).Rows
                        Select Case Dr("GRVPad")
                            Case 1
                                sGrvNo = Dr("GrvPref") & Format(CInt(Dr("GrvNum")), "0")
                            Case 2
                                sGrvNo = Dr("GrvPref") & Format(CInt(Dr("GrvNum")), "00")
                            Case 3
                                sGrvNo = Dr("GrvPref") & Format(CInt(Dr("GrvNum")), "000")
                            Case 4
                                sGrvNo = Dr("GrvPref") & Format(CInt(Dr("GrvNum")), "0000")
                            Case 5
                                sGrvNo = Dr("GrvPref") & Format(CInt(Dr("GrvNum")), "00000")
                            Case 6
                                sGrvNo = Dr("GrvPref") & Format(CInt(Dr("GrvNum")), "000000")
                            Case 7
                                sGrvNo = Dr("GrvPref") & Format(CInt(Dr("GrvNum")), "0000000")
                            Case 8
                                sGrvNo = Dr("GrvPref") & Format(CInt(Dr("GrvNum")), "00000000")
                            Case 9
                                sGrvNo = Dr("GrvPref") & Format(CInt(Dr("GrvNum")), "000000000")
                        End Select
                    Next
                End If
                txtInNo.Text = sGrvNo
            Catch ex As Exception
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try
        End With
    End Sub
    Public Sub GET_NEXT_INVOICE_NO()
        SQL = "SELECT InvNum, InvPref, InvPad FROM StDfTbl"
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                If DS.Tables(0).Rows.Count = 0 Then
                    sInvoiceNo = "DNU"
                Else
                    For Each Dr In DS.Tables(0).Rows
                        Select Case Dr("InvPad")
                            Case 1
                                sInvoiceNo = Dr("InvPref") & Format(CInt(Dr("InvNum")), "0")
                            Case 2
                                sInvoiceNo = Dr("InvPref") & Format(CInt(Dr("InvNum")), "00")
                            Case 3
                                sInvoiceNo = Dr("InvPref") & Format(CInt(Dr("InvNum")), "000")
                            Case 4
                                sInvoiceNo = Dr("InvPref") & Format(CInt(Dr("InvNum")), "0000")
                            Case 5
                                sInvoiceNo = Dr("InvPref") & Format(CInt(Dr("InvNum")), "00000")
                            Case 6
                                sInvoiceNo = Dr("InvPref") & Format(CInt(Dr("InvNum")), "000000")
                            Case 7
                                sInvoiceNo = Dr("InvPref") & Format(CInt(Dr("InvNum")), "0000000")
                            Case 8
                                sInvoiceNo = Dr("InvPref") & Format(CInt(Dr("InvNum")), "00000000")
                            Case 9
                                sInvoiceNo = Dr("InvPref") & Format(CInt(Dr("InvNum")), "000000000")
                        End Select
                    Next
                End If
                txtInNo.Text = sInvoiceNo
            Catch ex As Exception
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try
        End With
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
                txtDelNote_SupInvNo.Text = sDeliveryNo
            Catch ex As Exception
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try
        End With
    End Sub
    Public Sub GET_NEXT_PO_NO()
        SQL = "SELECT     OrderPrefix, NextCustNo, CNoPadLgth FROM OrdersDf WHERE DefaultCounter = 2"
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                If DS.Tables(0).Rows.Count = 0 Then
                    sPONo = "DNU"   'Default Not Updated
                Else
                    For Each Dr In DS.Tables(0).Rows
                        Select Case Dr("CNoPadLgth")
                            Case 1
                                sPONo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "0")
                            Case 2
                                sPONo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "00")
                            Case 3
                                sPONo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "000")
                            Case 4
                                sPONo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "0000")
                            Case 5
                                sPONo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "00000")
                            Case 6
                                sPONo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "000000")
                            Case 7
                                sPONo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "0000000")
                            Case 8
                                sPONo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "00000000")
                            Case 9
                                sPONo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "000000000")
                        End Select
                    Next
                End If
                txtOrdNo.Text = sPONo
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try
        End With

    End Sub
    Public Sub GET_NEXT_ORDER_NO()
        SQL = "SELECT  NextCustNo,OrderPrefix, CNoPadLgth FROM OrdersDf WHERE DefaultCounter = 1"
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                If DS.Tables(0).Rows.Count = 0 Then
                    sOrderNo = "DNU"   'Default Not Updated
                Else
                    For Each Dr In DS.Tables(0).Rows
                        Select Case Dr("CNoPadLgth")
                            Case 1
                                sOrderNo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "0")
                            Case 2
                                sOrderNo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "00")
                            Case 3
                                sOrderNo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "000")
                            Case 4
                                sOrderNo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "0000")
                            Case 5
                                sOrderNo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "00000")
                            Case 6
                                sOrderNo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "000000")
                            Case 7
                                sOrderNo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "0000000")
                            Case 8
                                sOrderNo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "00000000")
                            Case 9
                                sOrderNo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "000000000")
                        End Select
                    Next
                End If
                txtOrdNo.Text = sOrderNo
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try
        End With

    End Sub
    Public Sub GET_NEXT_QUO_NO()
        SQL = "SELECT NextQuoteNo, QuotePrefix, PadQuoteLngth, DefaultCounter FROM OrdersDf WHERE DefaultCounter = 1"
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                If DS.Tables(0).Rows.Count = 0 Then
                    sQuoteNo = "DNU"   'Default Not Updated
                Else
                    For Each Dr In DS.Tables(0).Rows
                        Select Case Dr("PadQuoteLngth")
                            Case 1
                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "0")
                            Case 2
                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "00")
                            Case 3
                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "000")
                            Case 4
                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "0000")
                            Case 5
                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "00000")
                            Case 6
                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "000000")
                            Case 7
                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "0000000")
                            Case 8
                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "00000000")
                            Case 9
                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "000000000")
                        End Select
                    Next
                End If
            Catch ex As Exception
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try

        End With
    End Sub
    Public Sub Discard()
        cmbCustomer.Text = Nothing
        cmbCustomer.Enabled = True
        cmbOrderStatus.Text = Nothing
        cmbPriority.Text = Nothing
        cmbProject.Text = Nothing
        cmbSalesOpp.Text = Nothing
        txtExtOrder.Text = Nothing
        txtDelNote_SupInvNo.Text = Nothing
        txtOrdNo.Text = Nothing
        txtInNo.Text = Nothing
        cmbType.ResetText()
        dtpDeliDate.Value = Format(Date.Now, "dd/MM/yyyy")
        dtpDueDate.Value = Format(Date.Now, "dd/MM/yyyy")
        dtpInDate.Value = Format(Date.Now, "dd/MM/yyyy")
        dtpOrdDate.Value = Format(Date.Now, "dd/MM/yyyy")
        txtDAdd1.ResetText()
        txtDAdd2.ResetText()
        txtDAdd3.ResetText()
        txtDAdd4.ResetText()
        txtDAdd5.ResetText()
        txtPAdd1.ResetText()
        txtPAdd2.ResetText()
        txtPAdd3.ResetText()
        txtPAdd4.ResetText()
        txtPAdd5.ResetText()
        IsNew = True
        iPONo = 0
        If iDocType = DocType.SalesOrder Then
            GET_NEXT_GRV_NO()
            GET_NEXT_PO_NO()
            GET_DATA()
        End If
        DeleteRows()
        Get_Total()
    End Sub
    Private Sub tsbPlaceOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSOPlaceOrder.Click
        If cmbCustomer.Text = "" Then
            MsgBox("Customer Name can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If
        If UG.Rows.Count = 0 Then
            MsgBox("Enter Item Details", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                .Begin_Trans()

                .Commit_Trans()
                Discard()
            Catch ex As Exception
                .Rollback_Trans()
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Pastel Evolution")
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try
        End With
        GET_NEXT_ORDER_NO()

    End Sub
    Public Sub Delete_Row()
        Dim R As UltraGridRow
        For Each R In UG.Rows.All
            If R.Cells("Line").Value = 0 Or R.Cells("StockID").Value = 0 Then
                R.Delete(False)
                Exit Sub
            End If
        Next
    End Sub
    Private Sub UG_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        Delete_Row()
    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Me.Close()
    End Sub

    Private Sub cmbCustomer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbCustomer.KeyDown
        If e.KeyData = Keys.F9 Then
            If iDocType = DocType.SalesOrder Then
                tsbSaveSO_Click(tsbSaveSO, e)
            End If
        End If
    End Sub

    Private Sub cmbCustomer_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCustomer.Leave

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
                'sAccount = cmbCustomer.ActiveRow.Cells("Name").Value
                'txtPostalAdd.Text = cmbCustomer.ActiveRow.Cells("Post1").Value & vbLf & cmbCustomer.ActiveRow.Cells("Post2").Value & vbLf & cmbCustomer.ActiveRow.Cells("Post3").Value
                'sPAdd1 = cmbCustomer.ActiveRow.Cells("Post1").Value
                'sPAdd2 = cmbCustomer.ActiveRow.Cells("Post2").Value
                'sPAdd3 = cmbCustomer.ActiveRow.Cells("Post3").Value
                'txtDeliveryAdd.Text = cmbCustomer.ActiveRow.Cells("Physical1").Value & vbLf & cmbCustomer.ActiveRow.Cells("Physical2").Value & vbLf & cmbCustomer.ActiveRow.Cells("Physical3").Value
                'sAdd1 = cmbCustomer.ActiveRow.Cells("Physical1").Value
                'sAdd2 = cmbCustomer.ActiveRow.Cells("Physical2").Value
                'sAdd3 = cmbCustomer.ActiveRow.Cells("Physical3").Value
                'dblMargin = cmbCustomer.ActiveRow.Cells("Discount").Value
                cmbGRVLoc.Text = cmbCustomer.ActiveRow.Cells("cAccDescription").Value
                'POLocation = cmbCustomer.ActiveRow.Cells("DCLink").Value
                clientWH = cmbCustomer.ActiveRow.Cells("cIDNumber").Value
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Private Sub tsbQuote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSOQuote.Click

        If cmbCustomer.Text = "" Then
            MsgBox("Customer Name can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If UG.Rows.Count = 0 Then
            MsgBox("Enter Item Details", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        Dim Dr As DataRow
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                .Begin_Trans()
                If IsNew = True Then
                    SQL = "SELECT NextQuoteNo, QuotePrefix, PadQuoteLngth, DefaultCounter FROM OrdersDf WHERE DefaultCounter = 1"
                    DS = New DataSet
                    DS = .Get_Data_Sql_Trans(SQL)
                    If DS.Tables(0).Rows.Count = 0 Then
                        MsgBox("Please check your Order Default Settings", MsgBoxStyle.Exclamation, "Pastel Evolution")    'Default Not Updated
                        .Rollback_Trans()
                        Exit Sub
                    Else
                        For Each Dr In DS.Tables(0).Rows
                            Select Case Dr("PadQuoteLngth")
                                Case 1
                                    sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "0")
                                Case 2
                                    sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "00")
                                Case 3
                                    sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "000")
                                Case 4
                                    sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "0000")
                                Case 5
                                    sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "00000")
                                Case 6
                                    sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "000000")
                                Case 7
                                    sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "0000000")
                                Case 8
                                    sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "00000000")
                                Case 9
                                    sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "000000000")
                            End Select
                        Next
                    End If
                    SQL = "UPDATE OrdersDf SET NextQuoteNo =NextQuoteNo+1  WHERE DefaultCounter = 1"
                    If .Execute_Sql_Trans(SQL) = 0 Then
                        .Rollback_Trans()
                        Exit Sub
                    End If

                End If


                SQL = "INSERT INTO sbInvNum(DocType, DocVersion, DocState, DocFlag, OrigDocID, " & _
                " GrvID,InvNumber, GrvNumber, AccountID, Description, InvDate, OrderDate, DueDate, " & _
                " DeliveryDate, TaxInclusive, Email_Sent, Address1, Address2, Address3, " & _
                " PAddress1, PAddress2, PAddress3,DelMethodID, DocRepID,OrderNum,DeliveryNote,  ProjectID," & _
                " TillID, ExtOrderNum, InvTotExclDEx, InvTotTaxDEx, InvTotInclDEx, InvTotExcl, InvTotTax, " & _
                " InvTotIncl,OrdTotExclDEx, OrdTotTaxDEx, OrdTotInclDEx, OrdTotExcl, OrdTotTax, OrdTotIncl, " & _
                " cAccountName, iOpportunityID, bInvRounding,InvTotInclExRounding, " & _
                " OrdTotInclExRounding,OrderStatusID,OrderPriorityID) VALUES (" & DocType.SalesOrder & ",'1'," & DocState.Quote & ",'0','0','0','" & sQuoteNo & "',''," & cmbCustomer.Value & "," & _
                " '" & txtDescription.Text.ToString.Replace("'", "") & "','" & Format(dtpInDate.Value, "MM/dd/yyyy") & "','" & Format(dtpOrdDate.Value, "MM/dd/yyyy") & "','" & Format(dtpDueDate.Value, "MM/dd/yyyy") & "'," & _
                " '" & Format(dtpDeliDate.Value, "MM/dd/yyyy") & "'," & IIf(cbInEx.Checked = True, 1, 0) & "," & _
                " '1','" & txtDAdd1.Text.ToString.Replace("'", "") & "','" & txtDAdd2.Text.ToString.Replace("'", "") & "','" & txtDAdd3.Text.ToString.Replace("'", "") & "','" & txtPAdd1.Text.ToString.Replace("'", "") & "','" & txtPAdd2.Text.ToString.Replace("'", "") & "','" & txtPAdd3.Text.ToString.Replace("'", "") & "'," & _
                " '0'," & cmbSaleRep.Value & ",'Quote',''," & IIf(cmbProject.Value = Nothing, 0, cmbProject.Value) & "," & _
                " '0','" & txtExtOrder.Text & "'," & txtConAmt.Value - txtConTax.Value & "," & _
                " " & txtConTax.Value & "," & txtConAmt.Value & "," & txtConAmt.Value - txtConTax.Value & "," & _
                " " & txtConTax.Value & "," & txtConAmt.Value & "," & txtOrdAmt.Value - txtOrdTax.Value & "," & _
                " " & txtOrdTax.Value & "," & txtOrdAmt.Value & "," & txtOrdAmt.Value - txtOrdTax.Value & "," & txtOrdTax.Value & "," & _
                " " & txtOrdAmt.Value & ",'" & cmbCustomer.Text & "'," & IIf(cmbSalesOpp.Value = Nothing, 0, cmbSalesOpp.Value) & ",'1'," & txtConAmt.Value & "," & txtOrdAmt.Value & "," & _
                " " & IIf(cmbOrderStatus.Value = Nothing, 0, cmbOrderStatus.Value) & "," & IIf(cmbPriority.Value = Nothing, 0, cmbPriority.Value) & " )"
                If .Execute_Sql_Trans(SQL) = 0 Then
                    .Rollback_Trans()
                    Exit Sub
                End If

                Dim iInvoiceID As Integer
                SQL = "SELECT MAX(AutoIndex) FROM sbInvNum"
                iInvoiceID = .Get_Max_No(SQL)
                If iInvoiceID = -1 Then
                    .Rollback_Trans()
                    Exit Sub
                End If

                Dim ugR As UltraGridRow
                For Each ugR In UG.Rows
                    SQL = "INSERT INTO sb_btblInvoiceLines(iInvoiceID, iOrigLineID, iGrvLineID, cDescription, " & _
                    " iUnitsOfMeasureStockingID, iUnitsOfMeasureCategoryID, iUnitsOfMeasureID, fQuantity, " & _
                    " fQtyToProcess, fUnitPriceExcl, fUnitPriceIncl, fLineDiscount, fTaxRate, bIsWhseItem," & _
                    " iStockCodeID, iWarehouseID, iTaxTypeID, iPriceListNameID, fQuantityLineTotIncl," & _
                    " fQuantityLineTotExcl, fQuantityLineTotInclNoDisc, fQuantityLineTotExclNoDisc," & _
                    " fQuantityLineTaxAmount, fQuantityLineTaxAmountNoDisc, fQtyToProcessLineTotIncl," & _
                    " fQtyToProcessLineTotExcl, fQtyToProcessLineTotInclNoDisc, fQtyToProcessLineTotExclNoDisc," & _
                    " fQtyToProcessLineTaxAmount, fQtyToProcessLineTaxAmountNoDisc, iLineRepID, iLineProjectID, " & _
                    " bChargeCom,iLineID,fUnitCost,fQtyLastProcess,fQtyProcessed,fQtyReserved,cLineNotes,fAddCost,iJobID,fQtyChange," & _
                    " fQtyChangeLineTotIncl,fQtyChangeLineTotExcl,fQtyChangeLineTotInclNoDisc," & _
                    " fQtyChangeLineTotExclNoDisc,fQtyChangeLineTaxAmount,fQtyChangeLineTaxAmountNoDisc) VALUES  (" & iInvoiceID & ",'0','0','" & ugR.Cells("Description_1").Value & "', 0, 0, " & _
                    " 0," & ugR.Cells("Quantity").Value & "," & ugR.Cells("ConfirmQty").Value & "," & _
                    " " & ugR.Cells("Price_Excl").Value & "," & ugR.Cells("Price_Incl").Value & " ," & _
                    " " & ugR.Cells("Discount").Value & "," & ugR.Cells("TaxRate").Value & " ," & _
                    " " & IIf(ugR.Cells("Warehouse").Value = 0, 0, 1) & "," & ugR.Cells("Code").Value & "," & _
                    " " & CInt(IIf(ugR.Cells("Warehouse").Value = Nothing, 0, ugR.Cells("Warehouse").Value)) & "," & IIf(IsDBNull(ugR.Cells("TaxType").Value) = True, 0, ugR.Cells("TaxType").Value) & ", " & _
                    " 1," & ugR.Cells("OrderTotal_Incl").Value & "," & ugR.Cells("OrderTotal_Excl").Value & "," & _
                    " " & ugR.Cells("OrderTotal_Incl").Value + ugR.Cells("Order_Dis_Incl").Value & "," & _
                    " " & ugR.Cells("OrderTotal_Excl").Value + ugR.Cells("Order_Dis_Excl").Value & "," & _
                    " " & ugR.Cells("OrderTax").Value & "," & ugR.Cells("Ord_Tax_NoDis_Excl").Value & "," & _
                    " " & ugR.Cells("LineTotal_Incl").Value & "," & ugR.Cells("LineTotal_Excl").Value & "," & _
                    " " & ugR.Cells("LineTotal_Incl").Value + ugR.Cells("Line_Dis_Incl").Value & "," & _
                    " " & ugR.Cells("LineTotal_Excl").Value + ugR.Cells("Line_Dis_Excl").Value & "," & _
                    " " & ugR.Cells("LineTax").Value & "," & ugR.Cells("Line_Tax_NoDis_Excl").Value & "," & IIf(cmbSaleRep.Value = Nothing, 0, cmbSaleRep.Value) & "," & IIf(cmbProject.Value = Nothing, 0, cmbProject.Value) & "," & _
                    " '1'," & ugR.Cells("Line").Value & ",0,0,0,0,'',0,0," & ugR.Cells("Quantity").Value & "," & _
                    " " & ugR.Cells("OrderTotal_Incl").Value & "," & ugR.Cells("OrderTotal_Excl").Value & "," & _
                    " " & ugR.Cells("OrderTotal_Incl").Value + ugR.Cells("Order_Dis_Incl").Value & "," & _
                    " " & ugR.Cells("OrderTotal_Excl").Value + ugR.Cells("Order_Dis_Excl").Value & "," & _
                    " " & ugR.Cells("OrderTax").Value & "," & ugR.Cells("Ord_Tax_NoDis_Excl").Value & ")"
                    If .Execute_Sql_Trans(SQL) = 0 Then
                        .Rollback_Trans()
                        Exit Sub
                    End If

                Next
                .Commit_Trans()
                Discard()
            Catch ex As Exception
                .Rollback_Trans()
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Pastel Evolution")
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try
        End With
    End Sub
    Private Sub SaveMySettingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveMySettingToolStripMenuItem.Click
        Try
            If iDocType = DocType.SalesOrder Then
                UG.DisplayLayout.SaveAsXml(Application.StartupPath & "\" & sAgent & "SO.xml")
            ElseIf iDocType = DocType.PurchaseOrder Then
                UG.DisplayLayout.SaveAsXml(Application.StartupPath & "\" & sAgent & "PO.xml")
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub tsbOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbOpen.Click
        'If iDocType = DocType.SalesOrder Then
        '    frmOpen.iDocType_1 = 0
        '    frmOpen.Get_Customer()
        '    frmOpen.iDocType = DocType.SalesOrder
        'ElseIf iDocType = DocType.PurchaseOrder Then
        '    frmOpen.iDocType_1 = 0
        '    frmOpen.Get_Supplier()
        '    frmOpen.iDocType = DocType.PurchaseOrder
        'ElseIf iDocType = DocType.Invoice Then
        '    frmOpen.iDocType_1 = 10
        '    frmOpen.Get_Customer()
        '    frmOpen.iDocType = DocType.SalesOrder
        'ElseIf iDocType = DocType.IBTReceive Then
        frmOpen.iDocType_1 = 8
        frmOpen.Get_Supplier()
        frmOpen.iDocType = DocType.RTN
        'End If
        frmOpen.ShowDialog()
        frmOpen.TopMost = True
        Me.Close()
    End Sub
    Private Sub tsbSaveSO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSaveSO.Click

        Delete_Row()

        UG.PerformAction(UltraGridAction.CommitRow)

        If cmbCustomer.Value = Nothing Then
            MsgBox("Customer can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbCustomer.Text.Trim.Length = 0 Then
            MsgBox("Customer can not be left balnk", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        Dim ugR As UltraGridRow
        For Each ugR In UG.Rows
            If ugR.Cells("IsWH").Value = True Then
                If ugR.Cells("Warehouse").Value = Nothing Then
                    MsgBox("Warehouse can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
                    Exit Sub
                End If
            End If
            If ugR.Cells("Quantity").Value = 0 Then
                MsgBox("Quantity can not zero", MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            End If
            If ugR.Cells("ConfirmQty").Value = 0 Then
                MsgBox("Confirm Quantity can not be zero", MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            End If
        Next

        If cmbSaleRep.Value = 0 Then
            MsgBox("Select Sales Person", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If UG.Rows.Count = 0 Then
            MsgBox("Please enter item details", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
        DatabaseContext.SetLicense("DE09110022", "1428511")
        DatabaseContext.CreateConnection(sSQLSrvName, sSQLSrvDataBase, sSQLSrvUserName, sSQLSrvPassword, False)

        Try
            DatabaseContext.BeginTran()
            Dim SalesOrder As New SalesOrder
            Dim SalesOrderDetail As OrderDetail
            Dim InventoryItem As InventoryItem
            Dim WH As Warehouse

            Dim Customer As New Customer(CInt(cmbCustomer.Value))
            Dim bIsCheckTerms As Boolean
            Dim dblAccountBalance As Double = 0
            Dim dblCreditLimit As Double = 0
            bIsCheckTerms = Customer.CheckTerms
            If bIsCheckTerms = True Then
                dblAccountBalance = Customer.AccountBalance
                dblCreditLimit = Customer.CreditLimit
                If dblAccountBalance + txtConAmt.Value > dblCreditLimit Then
                    If bIsAllowOverrideCreditLimit = False Then
                        MsgBox("Account is Over Credit Limit", MsgBoxStyle.Exclamation, "Pastel Evolution")
                    End If
                End If
            End If

            Dim Representative As New SalesRepresentative(CInt(cmbSaleRep.Value))
            SalesOrder.Account = Customer
            SalesOrder.InvoiceTo = Customer.PostalAddress
            SalesOrder.DeliverTo = Customer.PhysicalAddress
            SalesOrder.ExternalOrderNo = txtExtOrder.Text.ToString
            SalesOrder.InvoiceDate = Format(dtpInDate.Value, "dd/MM/yyyy")
            SalesOrder.DeliveryDate = Format(dtpDeliDate.Value, "dd/MM/yyyy")
            SalesOrder.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")
            SalesOrder.Representative = Representative
            SalesOrder.DiscountPercent = txtDisPer.Value


            If bIsInclusive = True Then
                SalesOrder.TaxMode = TaxMode.Inclusive
            ElseIf bIsInclusive = False Then
                SalesOrder.TaxMode = TaxMode.Exclusive
            End If

            For Each ugR In UG.Rows
                SalesOrderDetail = New OrderDetail
                InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))

                SalesOrderDetail.InventoryItem = InventoryItem
                SalesOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                SalesOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)
                If bIsInclusive = True Then
                    SalesOrderDetail.TaxMode = TaxMode.Inclusive
                    SalesOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                    SalesOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                ElseIf bIsInclusive = False Then
                    SalesOrderDetail.TaxMode = TaxMode.Exclusive
                    SalesOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                    SalesOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                End If
                If ugR.Cells("IsSerialNo").Value = True Then
                    If ugR.ChildBands.HasChildRows = False Then
                        MsgBox("Duplicate Serial No" & vbCrLf & "Item Code - " & ugR.Cells("Description_1").Value & vbCrLf & "Line No - " & ugR.Index + 1, MsgBoxStyle.Exclamation, "Pastel Evolution")
                        'MsgBox("Enter Serial Numbers" & vbCrLf & "Item Code - " & ugR.Cells("Description_1").Value & vbCrLf & ugR.Index + 1, MsgBoxStyle.Exclamation, "Pastel Evolution")
                        DatabaseContext.RollbackTran()
                        If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
                        If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
                        Exit Sub
                    End If
                    Dim ugR2 As UltraGridRow
                    For Each ugR2 In ugR.ChildBands(0).Rows
                        'If ugR2.Cells("Received").Value = True Then
                        Dim sn As String = CStr(ugR2.Cells("SerialNumber").Value)
                        SalesOrderDetail.SerialNumbers.Add(sn)
                        'End If
                    Next
                End If

                If ugR.Cells("IsWH").Value = True Then
                    WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                    If ugR.Cells("IsWH").Value = True Then
                        SalesOrderDetail.Warehouse = WH
                    End If
                End If

                SalesOrderDetail.Description = CStr(ugR.Cells("Description_1").Value)
                SalesOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                SalesOrderDetail.DiscountPercent = CDbl(ugR.Cells("Discount").Value)
                SalesOrder.Detail.Add(SalesOrderDetail)
            Next

            SalesOrder.Save()
            SalesOrder.Process()
            Dim iSalesOrder As Integer = SalesOrder.ID
            MsgBox(SalesOrder.Reference, MsgBoxStyle.Information, "Pastel Evolution")
            DatabaseContext.CommitTran()
            If MsgBox("Do you want to print now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
                Dim objRep As New ReportDocument
                objRep.Load(Application.StartupPath & "\SalesOrder.rpt")
                objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iSalesOrder & ""
                ApplyLoginToTable(objRep)
                objRep.PrintToPrinter(1, False, 0, 0)
            End If
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
            Discard()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
            DatabaseContext.RollbackTran()
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        Finally
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        End Try
    End Sub
    Private Sub stbUpdateSO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbUpdateSO.Click
        Delete_Row()

        UG.PerformAction(UltraGridAction.CommitRow)

        If cmbCustomer.Value = Nothing Then
            MsgBox("Customer can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbCustomer.Text.Trim.Length = 0 Then
            MsgBox("Customer can not be left balnk", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If UG.Rows.Count = 0 Then
            MsgBox("Please enter item details", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        Dim ugR As UltraGridRow
        For Each ugR In UG.Rows
            If ugR.Cells("IsWH").Value = True Then
                If ugR.Cells("Warehouse").Value = Nothing Then
                    MsgBox("Warehouse can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
                    Exit Sub
                End If
            End If
            If ugR.Cells("Quantity").Value = 0 Then
                MsgBox("Quantity can not zero", MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            End If
            If ugR.Cells("ConfirmQty").Value = 0 Then
                MsgBox("Confirm Quantity can not be zero", MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            End If
        Next

        If cmbSaleRep.Value = Nothing Then
            MsgBox("Select Sales Person", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
        DatabaseContext.SetLicense("DE09110022", "1428511")
        DatabaseContext.CreateConnection(sSQLSrvName, sSQLSrvDataBase, sSQLSrvUserName, sSQLSrvPassword, False)

        Try

            DatabaseContext.BeginTran()
            Dim SalesOrder As New SalesOrder
            Dim SalesOrderDetail As OrderDetail
            Dim InventoryItem As InventoryItem
            Dim WH As Warehouse

            Dim Customer As New Customer(CInt(cmbCustomer.Value))
            Dim Representative As New SalesRepresentative(CInt(cmbSaleRep.Value))

            SalesOrder.Account = Customer
            SalesOrder.InvoiceTo = Customer.PostalAddress
            SalesOrder.DeliverTo = Customer.PhysicalAddress
            SalesOrder.ExternalOrderNo = txtExtOrder.Text.ToString
            SalesOrder.InvoiceDate = Format(dtpInDate.Value, "dd/MM/yyyy")
            SalesOrder.DeliveryDate = Format(dtpDeliDate.Value, "dd/MM/yyyy")
            SalesOrder.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")
            SalesOrder.Representative = Representative
            SalesOrder.DiscountPercent = txtDisPer.Value


            If bIsInclusive = True Then
                SalesOrder.TaxMode = TaxMode.Inclusive
            ElseIf bIsInclusive = False Then
                SalesOrder.TaxMode = TaxMode.Exclusive
            End If

            For Each ugR In UG.Rows
                SalesOrderDetail = New OrderDetail
                InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))

                SalesOrderDetail.InventoryItem = InventoryItem
                SalesOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                SalesOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)
                If bIsInclusive = True Then
                    SalesOrderDetail.TaxMode = TaxMode.Inclusive
                    SalesOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                    SalesOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                ElseIf bIsInclusive = False Then
                    SalesOrderDetail.TaxMode = TaxMode.Exclusive
                    SalesOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                    SalesOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                End If
                If ugR.Cells("IsWH").Value = True Then
                    WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                    If ugR.Cells("IsWH").Value = True Then
                        SalesOrderDetail.Warehouse = WH
                    End If
                End If
                SalesOrderDetail.Description = CStr(ugR.Cells("Description_1").Value)
                SalesOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                SalesOrderDetail.DiscountPercent = CDbl(ugR.Cells("Discount").Value)
                SalesOrder.Detail.Add(SalesOrderDetail)
            Next
            SalesOrder.Save()
            Dim iSalesOrder As Integer = SalesOrder.ID
            MsgBox(SalesOrder.OrderNo, MsgBoxStyle.Information, "Pastel Evolution")
            DatabaseContext.CommitTran()
            If MsgBox("Do you want to print now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
                Dim ps As New PrinterSettings

                Dim objRep As New ReportDocument
                objRep.Load(Application.StartupPath & "\IBT.rpt")
                objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iSalesOrder & ""
                ApplyLoginToTable(objRep)
                'objRep.SetDatabaseLogon(sSQLSrvUserName, sSQLSrvPassword, sSQLSrvReportSrv, sSQLSrvDataBase)
                Dim objCRV As New frmPrintPreview
                With objCRV
                    .CRV.ReportSource = objRep
                    .Text = "Sales Order Printing"
                    .ShowDialog()
                End With
            End If
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
            Discard()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
            DatabaseContext.RollbackTran()
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        Finally
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        End Try
    End Sub

    Private Sub tsbSavePO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSavePO.Click

        Delete_Row()

        UG.PerformAction(UltraGridAction.CommitRow)

        If cmbCustomer.Value = Nothing Then
            MsgBox("Supplier can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbCustomer.Text.Trim.Length = 0 Then
            MsgBox("Supplier can not be left balnk", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If UG.Rows.Count = 0 Then
            MsgBox("Please enter item details", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbType.Text.Trim.Length = 0 Then
            MsgBox("Enter Purchase Order Type", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        Dim ugR As UltraGridRow
        For Each ugR In UG.Rows
            If ugR.Cells("IsWH").Value = True Then
                If ugR.Cells("Warehouse").Value = Nothing Then
                    MsgBox("Warehouse can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
                    Exit Sub
                End If
            End If
        Next
        DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
        DatabaseContext.SetLicense("DE09110022", "1428511")
        DatabaseContext.CreateConnection(sSQLSrvName, sSQLSrvDataBase, sSQLSrvUserName, sSQLSrvPassword, False)
        Try

            DatabaseContext.BeginTran()
            Dim PurchaseOrder As New PurchaseOrder
            Dim PurchaseOrderDetail As OrderDetail
            Dim InventoryItem As InventoryItem
            Dim WH As Warehouse

            Dim Supplier As New Supplier(CInt(cmbCustomer.Value))

            PurchaseOrder.Account = Supplier
            PurchaseOrder.InvoiceTo = Supplier.PostalAddress
            PurchaseOrder.DeliverTo = Supplier.PhysicalAddress
            PurchaseOrder.ExternalOrderNo = txtExtOrder.Text.ToString
            PurchaseOrder.InvoiceDate = Format(dtpInDate.Value, "dd/MM/yyyy")
            PurchaseOrder.DeliveryDate = Format(dtpDeliDate.Value, "dd/MM/yyyy")
            PurchaseOrder.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")
            PurchaseOrder.SupplierInvoiceNo = txtDelNote_SupInvNo.Text.ToString
            PurchaseOrder.UserFields("ulIDPOrdType") = cmbType.Value
            If bIsInclusive = True Then
                PurchaseOrder.TaxMode = TaxMode.Inclusive
            ElseIf bIsInclusive = False Then
                PurchaseOrder.TaxMode = TaxMode.Exclusive
            End If

            For Each ugR In UG.Rows
                PurchaseOrderDetail = New OrderDetail
                InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))
                PurchaseOrderDetail.InventoryItem = InventoryItem
                PurchaseOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)

                If bIsInclusive = True Then
                    PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                    PurchaseOrderDetail.TaxMode = TaxMode.Inclusive
                    PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                ElseIf bIsInclusive = False Then
                    PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                    PurchaseOrderDetail.TaxMode = TaxMode.Exclusive
                    PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                End If

                If ugR.Cells("IsWH").Value = True Then
                    WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                    PurchaseOrderDetail.Warehouse = WH
                End If

                PurchaseOrderDetail.Description = CStr(ugR.Cells("Description_1").Value)
                PurchaseOrderDetail.DiscountPercent = CDbl(ugR.Cells("Discount").Value)
                PurchaseOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                PurchaseOrder.Detail.Add(PurchaseOrderDetail)

            Next

            PurchaseOrder.Save()


            DatabaseContext.CommitTran()
            MsgBox(PurchaseOrder.OrderNo, MsgBoxStyle.Information, "Pastel Evolution")

            Dim iPO As Integer = PurchaseOrder.ID

            If MsgBox("Do you want to print now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
                Dim objRep As New ReportDocument
                objRep.Load(Application.StartupPath & "\PO.rpt")
                objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iPO & ""
                ApplyLoginToTable(objRep)
                'objRep.SetDatabaseLogon(sSQLSrvUserName, sSQLSrvPassword, sSQLSrvReportSrv, sSQLSrvDataBase)
                Dim objCRV As New frmPrintPreview
                With objCRV
                    .CRV.ReportSource = objRep
                    .Text = "Sales Order Printing"
                    .ShowDialog()
                End With
            End If

            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
            Discard()
        Catch ex As Exception
            DatabaseContext.RollbackTran()
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
            MsgBox(Err.Description)
        Finally
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        End Try
    End Sub

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

    Private Sub tsbUpdatePO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbUpdatePO.Click

        Delete_Row()

        If cmbCustomer.Value = Nothing Then
            MsgBox("Supplier can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbCustomer.Text.Trim.Length = 0 Then
            MsgBox("Supplier can not be left balnk", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbType.Text.Trim.Length = 0 Then
            MsgBox("Enter Purchase Order Type", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If UG.Rows.Count = 0 Then
            MsgBox("Please enter item details", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        Dim ugR As UltraGridRow
        For Each ugR In UG.Rows
            If ugR.Cells("IsWH").Value = True Then
                If ugR.Cells("Warehouse").Value = Nothing Then
                    MsgBox("Warehouse can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
                    Exit Sub
                End If
            End If
        Next

        DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
        DatabaseContext.SetLicense("DE09110022", "1428511")
        DatabaseContext.CreateConnection(sSQLSrvName, sSQLSrvDataBase, sSQLSrvUserName, sSQLSrvPassword, False)
        Try
            If iPONo = 0 Then
                MsgBox("save the purchase order before processed", MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            End If

            DatabaseContext.BeginTran()
            Dim PurchaseOrder As New PurchaseOrder(CInt(iPONo))
            Dim PurchaseOrderDetail As OrderDetail
            Dim InventoryItem As InventoryItem
            Dim WH As Warehouse

            Dim Supplier As New Supplier(CInt(cmbCustomer.Value))

            PurchaseOrder.Account = Supplier
            PurchaseOrder.InvoiceTo = Supplier.PostalAddress
            PurchaseOrder.DeliverTo = Supplier.PhysicalAddress
            PurchaseOrder.ExternalOrderNo = txtExtOrder.Text.ToString
            PurchaseOrder.InvoiceDate = Format(dtpInDate.Value, "dd/MM/yyyy")
            PurchaseOrder.DeliveryDate = Format(dtpDeliDate.Value, "dd/MM/yyyy")
            PurchaseOrder.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")
            PurchaseOrder.SupplierInvoiceNo = txtDelNote_SupInvNo.Text.ToString
            PurchaseOrder.UserFields("ulIDPOrdType") = cmbType.Value
            If bIsInclusive = True Then
                PurchaseOrder.TaxMode = TaxMode.Inclusive
            ElseIf bIsInclusive = False Then
                PurchaseOrder.TaxMode = TaxMode.Exclusive
            End If

            Dim bLoop As Boolean = False

            Try

lbl1:           For Each PurchaseOrderDetail In PurchaseOrder.Detail
                    bLoop = False
                    PurchaseOrder.Detail.Remove(PurchaseOrderDetail)
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


            For Each ugR In UG.Rows
                Dim i As Integer = 0
                'For Each PurchaseOrderDetail In PurchaseOrder.Detail
                '    If ugR.Cells("LineID").Value = PurchaseOrderDetail.ID Then
                '        i = i + 1
                '        InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))
                '        PurchaseOrderDetail.InventoryItem = InventoryItem
                '        PurchaseOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                '        PurchaseOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)
                '        If bIsInclusive = True Then
                '            PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                '            PurchaseOrderDetail.TaxMode = TaxMode.Inclusive
                '            PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                '        ElseIf bIsInclusive = False Then
                '            PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                '            PurchaseOrderDetail.TaxMode = TaxMode.Exclusive
                '            PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                '        End If
                '        If ugR.Cells("IsWH").Value = True Then
                '            WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                '            PurchaseOrderDetail.Warehouse = WH
                '        End If
                '        PurchaseOrderDetail.Description = CStr(ugR.Cells("Description_2").Value)
                '        PurchaseOrderDetail.DiscountPercent = CDbl(ugR.Cells("Discount").Value)
                '        PurchaseOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                '        Exit For
                '    End If
                'Next

                If i = 0 Then
                    PurchaseOrderDetail = New OrderDetail
                    InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))
                    PurchaseOrderDetail.InventoryItem = InventoryItem
                    PurchaseOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                    PurchaseOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)

                    If bIsInclusive = True Then
                        PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                        PurchaseOrderDetail.TaxMode = TaxMode.Inclusive
                        PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                    ElseIf bIsInclusive = False Then
                        PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                        PurchaseOrderDetail.TaxMode = TaxMode.Exclusive
                        PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                    End If

                    If ugR.Cells("IsWH").Value = True Then
                        WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                        PurchaseOrderDetail.Warehouse = WH
                    End If

                    PurchaseOrderDetail.Description = CStr(ugR.Cells("Description_1").Value)
                    PurchaseOrderDetail.DiscountPercent = CDbl(ugR.Cells("Discount").Value)
                    PurchaseOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                    PurchaseOrder.Detail.Add(PurchaseOrderDetail)
                End If
            Next

            PurchaseOrder.Save()

            Call MemorizeStock()

            MsgBox(PurchaseOrder.OrderNo, MsgBoxStyle.Information, "Pastel Evolution")

            Dim iPO As Integer = PurchaseOrder.ID
            DatabaseContext.CommitTran()
            If MsgBox("Do you want to print now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
                Dim objRep As New ReportDocument
                objRep.Load(Application.StartupPath & "\IBT_R.rpt")
                objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iPO & ""
                ApplyLoginToTable(objRep)
                'objRep.SetDatabaseLogon(sSQLSrvUserName, sSQLSrvPassword, sSQLSrvReportSrv, sSQLSrvDataBase)
                Dim objCRV As New frmPrintPreview
                With objCRV
                    .CRV.ReportSource = objRep
                    .Text = "Sales Order Printing"
                    .ShowDialog()
                End With
            End If

            Discard()

            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

        Catch ex As Exception
            DatabaseContext.RollbackTran()
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        Finally
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        End Try
    End Sub

    Private Sub tsbProcessPO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbProcessPO.Click
        Delete_Row()
        If cmbCustomer.Value = Nothing Then
            MsgBox("Supplier can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbCustomer.Text.Trim.Length = 0 Then
            MsgBox("Supplier can not be left balnk", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If
        If UG.Rows.Count = 0 Then
            MsgBox("Please enter item details", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbType.Text.Trim.Length = 0 Then
            MsgBox("Enter Purchase Order Type", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        Dim ugR As UltraGridRow

        'Update confirm quantity
        For Each ugR In UG.Rows
            If ugR.ChildBands.HasChildRows = True Then
                ugR.Cells("ConfirmQty").Value = 0
                For Each ugR1 In ugR.ChildBands(0).Rows
                    If ugR1.Cells("Received").Value = True Then
                        ugR1.ParentRow.Cells("ConfirmQty").Value = ugR1.ParentRow.Cells("ConfirmQty").Value + 1
                        'Exit Sub
                    End If
                Next
            End If
        Next

        'Check warehouses
        For Each ugR In UG.Rows
            If ugR.Cells("IsWH").Value = True Then
                If ugR.Cells("Warehouse").Value = Nothing Then
                    MsgBox("Warehouse can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
                    Exit Sub
                End If
            End If
            If ugR.Cells("Price_Excl").Value = 0 Then
                'MsgBox("Price(Excl) can not be zero", MsgBoxStyle.Exclamation, "Pastel Evoltion")
                'Exit Sub
            End If
        Next

        'Check for duplicate s/n s
        For Each ugR In UG.Rows
            If ugR.Cells("IsSerialNo").Value = True Then
                Dim ugR2 As UltraGridRow
                For Each ugR2 In ugR.ChildBands(0).Rows
                    Dim sn As String = CStr(ugR2.Cells("SerialNumber").Value)
                    SQL = "SELECT * FROM SerialMF WHERE SerialNumber ='" & sn & "' and CurrentLoc = 1"
                    DS = New DataSet
                    Dim OBJsql As New clsSqlConn
                    With OBJsql
                        DS = .Get_Data_Sql(SQL)
                        If DS.Tables(0).Rows.Count > 0 Then
                            'MsgBox("Duplicate Serial No", MsgBoxStyle.Exclamation, "Pastel Evolution")
                            MsgBox("Duplicate Serial No" & vbCrLf & "Item Code - " & ugR.Cells("Description_1").Value & vbCrLf & "Line No - " & ugR.Index + 1 & vbCrLf & "Serial No - " & ugR2.Cells("SerialNumber").Value, MsgBoxStyle.Exclamation, "Pastel Evolution")
                            Exit Sub
                        End If
                    End With
                    OBJsql = Nothing
                    DS.Dispose()
                Next
            End If
        Next

        DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
        DatabaseContext.SetLicense("DE09110022", "1428511")
        DatabaseContext.CreateConnection(sSQLSrvName, sSQLSrvDataBase, sSQLSrvUserName, sSQLSrvPassword, False)

        Try

            If iPONo = 0 Then
                MsgBox("save the purchase order before processed", MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            End If

            DatabaseContext.BeginTran()

            Dim PurchaseOrder As New PurchaseOrder(CInt(iPONo))
            Dim PurchaseOrderDetail As OrderDetail
            Dim InventoryItem As InventoryItem
            Dim WH As Warehouse
            Dim Supplier As New Supplier(CInt(cmbCustomer.Value))
            PurchaseOrder.Account = Supplier
            PurchaseOrder.InvoiceTo = Supplier.PostalAddress
            PurchaseOrder.DeliverTo = Supplier.PhysicalAddress
            PurchaseOrder.ExternalOrderNo = txtExtOrder.Text.ToString
            PurchaseOrder.InvoiceDate = Format(dtpInDate.Value, "dd/MM/yyyy")
            PurchaseOrder.DeliveryDate = Format(dtpDeliDate.Value, "dd/MM/yyyy")
            PurchaseOrder.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")
            PurchaseOrder.SupplierInvoiceNo = txtDelNote_SupInvNo.Text.ToString
            PurchaseOrder.Description = txtDescription.Text.ToString
            PurchaseOrder.UserFields("ulIDPOrdType") = cmbType.Value

            If bIsInclusive = True Then
                PurchaseOrder.TaxMode = TaxMode.Inclusive
            ElseIf bIsInclusive = False Then
                PurchaseOrder.TaxMode = TaxMode.Exclusive
            End If

            Dim bLoop As Boolean = False

            Try

lbl1:           For Each PurchaseOrderDetail In PurchaseOrder.Detail
                    bLoop = False
                    PurchaseOrder.Detail.Remove(PurchaseOrderDetail)
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

            For Each ugR In UG.Rows
                Dim i As Integer = 0
                'For Each PurchaseOrderDetail In PurchaseOrder.Detail
                '    If ugR.Cells("LineID").Value = PurchaseOrderDetail.ID Then
                '        i = i + 1
                '        InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))
                '        PurchaseOrderDetail.InventoryItem = InventoryItem
                '        PurchaseOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                '        PurchaseOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)

                '        If PurchaseOrder.TaxMode = TaxMode.Inclusive Then
                '            PurchaseOrderDetail.TaxMode = TaxMode.Inclusive
                '            PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                '            PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                '        ElseIf PurchaseOrder.TaxMode = TaxMode.Exclusive Then
                '            PurchaseOrderDetail.TaxMode = TaxMode.Exclusive
                '            PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                '            PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                '        End If

                '        If ugR.Cells("IsSerialNo").Value = True Then
                '            If ugR.ChildBands.HasChildRows = False Then
                '                'MsgBox("Enter Serial Numbers", MsgBoxStyle.Exclamation, "Pastel Evolution")
                '                MsgBox("Enter Serial Numbers" & vbCrLf & "Item Code - " & ugR.Cells("Description_1").Value & vbCrLf & "Line No - " & ugR.Index + 1, MsgBoxStyle.Exclamation, "Pastel Evolution")
                '                DatabaseContext.RollbackTran()
                '                If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
                '                If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
                '                Exit Sub
                '            End If
                '            Dim ugR2 As UltraGridRow
                '            For Each ugR2 In ugR.ChildBands(0).Rows
                '                If ugR2.Cells("Received").Value = True Then

                '                    Dim sn As String = CStr(ugR2.Cells("SerialNumber").Value)
                '                    PurchaseOrderDetail.SerialNumbers.Add(sn)

                '                End If
                '            Next
                '        End If
                '        If ugR.Cells("IsWH").Value = True Then
                '            WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                '            PurchaseOrderDetail.Warehouse = WH
                '        End If
                '        PurchaseOrderDetail.Description = CStr(ugR.Cells("Description_1").Value)
                '        PurchaseOrderDetail.DiscountPercent = CDbl(ugR.Cells("Discount").Value)
                '        PurchaseOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)

                '    End If
                'Next

                If i = 0 Then
                    PurchaseOrderDetail = New OrderDetail
                    InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))
                    PurchaseOrderDetail.InventoryItem = InventoryItem
                    PurchaseOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                    PurchaseOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)

                    If PurchaseOrder.TaxMode = TaxMode.Inclusive Then
                        PurchaseOrderDetail.TaxMode = TaxMode.Inclusive
                        PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                        PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                    ElseIf PurchaseOrder.TaxMode = TaxMode.Exclusive Then
                        PurchaseOrderDetail.TaxMode = TaxMode.Exclusive
                        PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                        PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                    End If
                    If ugR.Cells("IsSerialNo").Value = True Then
                        If ugR.ChildBands.HasChildRows = False Then
                            'MsgBox("Enter Serial Numbers", MsgBoxStyle.Exclamation, "Pastel Evolution")
                            MsgBox("Enter Serial Numbers" & vbCrLf & "Item Code - " & ugR.Cells("Description_1").Value & vbCrLf & ugR.Index + 1, MsgBoxStyle.Exclamation, "Pastel Evolution")

                            DatabaseContext.RollbackTran()
                            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
                            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
                            Exit Sub
                        End If
                        Dim ugR2 As UltraGridRow
                        For Each ugR2 In ugR.ChildBands(0).Rows
                            If ugR2.Cells("Received").Value = True Then
                                Dim sn As String = CStr(ugR2.Cells("SerialNumber").Value)
                                PurchaseOrderDetail.SerialNumbers.Add(sn)
                            End If

                        Next
                    End If
                    If ugR.Cells("IsWH").Value = True Then
                        WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                        PurchaseOrderDetail.Warehouse = WH
                    End If
                    PurchaseOrderDetail.Description = CStr(ugR.Cells("Description_1").Value)
                    PurchaseOrderDetail.DiscountPercent = CDbl(ugR.Cells("Discount").Value)
                    PurchaseOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                    PurchaseOrder.Detail.Add(PurchaseOrderDetail)
                End If
            Next

            'Dim ugrow As New UltraGridRow
            For Each ugR In UG.Rows
                If ugR.Cells("ConfirmQty").Value <> ugR.Cells("Quantity").Value Then
                    MsgBox("Total quantity has not been received " & vbNewLine & "Please check received quantity", MsgBoxStyle.Information)
                    Exit Sub
                End If
            Next



            PurchaseOrder.Save()
            PurchaseOrder.ProcessStock()
            DatabaseContext.CommitTran()

            Dim iPO As Integer = PurchaseOrder.ID
            Call MemorizeStock()

            MsgBox(PurchaseOrder.Reference, MsgBoxStyle.Information, "Pastel Evolution")

            If MsgBox("Do you want to print now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
                Dim objRep As New ReportDocument
                objRep.Load(Application.StartupPath & "\IBT_R.rpt")
                objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iPO & ""
                ApplyLoginToTable(objRep)
                'objRep.SetDatabaseLogon(sSQLSrvUserName, sSQLSrvPassword, sSQLSrvReportSrv, sSQLSrvDataBase)
                Dim objCRV As New frmPrintPreview
                With objCRV
                    .CRV.ReportSource = objRep
                    .Text = "Sales Order Printing"
                    .ShowDialog()
                End With
            End If

            Discard()
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

        Catch ex As Exception

            DatabaseContext.RollbackTran()
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

            MsgBox(Err.Description)

        Finally
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        End Try

    End Sub

    Private Sub UG_ClickCellButton(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs)


        If e.Cell.Column.Key = "SerialLot" Then

            'If e.Cell.Row.Cells("ConfirmQty").Value = 0 Then
            '    MsgBox("Confirm Qty can not be Zero", MsgBoxStyle.Exclamation, "Pastel Evolution")
            '    Exit Sub
            'End If

            frmSerialNo.intItem = e.Cell.Row.Cells("StockID").Value
            frmSerialNo.lbSelected.Items.Clear()
            frmSerialNo.txtNumber.Value = 0
            frmSerialNo.txtPad.Value = 0
            frmSerialNo.txtString.Text = Nothing
            frmSerialNo.txtGenerate.Value = e.Cell.Row.Cells("ConfirmQty").Value
            frmSerialNo.txtGenerate.Enabled = False

            If e.Cell.Row.Band.Index = 0 Then
                If e.Cell.Row.ChildBands.HasChildRows = True Then
                    Dim ugCR As UltraGridRow
                    frmSerialNo.lbSelected.Items.Clear()
                    For Each ugCR In e.Cell.Row.ChildBands(0).Rows
                        frmSerialNo.lbSelected.Items.Add(CStr(ugCR.Cells("SerialNumber").Value))
                    Next
                End If
            End If

            frmSerialNo.ShowDialog()

            With frmSerialNo

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

                Dim ugR As UltraGridRow
                For Each ListItem In .lbSelected.Items
                    If e.Cell.Row.Band.Index = 0 Then
                        ugR = e.Cell.Row.ChildBands(0).Band.AddNew
                    Else
                        ugR = UG.DisplayLayout.Bands(1).AddNew
                    End If
                    ugR.Cells("LN").Value = ugR.Index + 1
                    ugR.Cells("SerialNumber").Value = ListItem
                    ugR.Cells("SNStockLink").Value = ugR.ParentRow.Cells("StockID").Value
                    ugR.Cells("PrimaryLineID").Value = ugR.ParentRow.Cells("Line").Value
                Next
                For Each ugR In UG.Rows
                    ugR.Expanded = False
                Next
            End With
        Else
            Exit Sub
        End If
    End Sub

    Private Sub txtBarCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBarCode.KeyDown
        'Dim ugR1 As UltraGridRow
        If e.KeyCode = Keys.Enter Then
            Delete_Row()
            Dim sBarCode As String = txtBarCode.Text.Trim
            txtBarCode.Text = ""
            'For Each ugR In UG.Rows
            '    If ugR.ChildBands.HasChildRows = True Then
            '        Dim ugR1 As UltraGridRow
            '        For Each ugR1 In ugR.ChildBands(0).Rows
            '            If ugR1.Cells("SerialNumber").Value = sBarCode Then
            '                txtBarCode.ResetText()
            '                txtBarCode.Focus()
            '                Exit Sub
            '            End If
            '        Next
            '    End If
            'Next

            'Dim objSQL As New clsSqlConn
            'With objSQL
            'SQL = "SELECT SerialNumber, SNStockLink FROM SerialMF WHERE SerialNumber ='" & CStr(sBarCode) & "' AND CurrentLoc = 1 and CurrentAccLink = " & iWH
            'DS = New DataSet
            'DS = .Get_Data_Sql(SQL)
            'If DS.Tables(0).Rows.Count > 0 Then
            '    Dim Dr As DataRow
            '    For Each Dr In DS.Tables(0).Rows
            '        'StockLink, Code, Description_1, ItemGroup, SerialItem,WhseItem
            '        Dim iStockLink As Integer = 0
            '        Dim sCode As String = ""
            '        Dim sDescription_1, sDescription_2 As String
            '        sDescription_1 = ""
            '        sDescription_2 = ""

            '        Dim bSerialItem As Boolean = False
            '        Dim bWhseItem As Boolean = False

            'Dim ugR As UltraGridRow
            'For Each ugR In DDStock.Rows
            '    If ugR.Cells("StockLink").Value = Dr("SNStockLink") Then
            '        iStockLink = ugR.Cells("StockLink").Value
            '        sCode = ugR.Cells("Code").Value
            '        sDescription_1 = ugR.Cells("Description_1").Value
            '        sDescription_2 = ugR.Cells("Description_2").Value
            '        bSerialItem = ugR.Cells("SerialItem").Value
            '        bWhseItem = ugR.Cells("WhseItem").Value
            '    End If
            'Next

            Dim ugR1 As UltraGridRow
            'Dim ugR2 As UltraGridRow
            Dim iCount As Integer = 0

            For Each ugR In UG.Rows
                If ugR.ChildBands.HasChildRows = True Then
                    For Each ugR1 In ugR.ChildBands(0).Rows
                        If ugR1.Cells("SerialNumber").Value = CStr(sBarCode) Then
                            ugR1.Cells("Received").Value = True
                            iCount = iCount + 1
                            Exit For
                        End If
                    Next
                End If
            Next

            If iCount = 0 Then
                MsgBox("can not find the Serial Number = " & CStr(sBarCode), MsgBoxStyle.Exclamation, "Pastel Evolution")
            End If

            For Each ugR In UG.Rows
                If ugR.ChildBands.HasChildRows = True Then
                    ugR.Cells("ConfirmQty").Value = 0
                    For Each ugR1 In ugR.ChildBands(0).Rows
                        If ugR1.Cells("Received").Value = True Then
                            ugR1.ParentRow.Cells("ConfirmQty").Value = ugR1.ParentRow.Cells("ConfirmQty").Value + 1
                            'Exit Sub
                        End If
                    Next
                End If
            Next

            'For Each ugR In UG.Rows
            '    If ugR.Cells("StockID").Value = iStockLink Then
            '        ugR1 = ugR
            '        ugR1.Activated = True
            '        iCount = iCount + 1
            '        If ugR1.ChildBands.HasChildRows = True Then
            '            ugR1 = UG.DisplayLayout.Bands(1).AddNew

            '        Else
            '            ugR1 = UG.ActiveRow.ChildBands(0).Band.AddNew
            '        End If
            '        ugR1.Cells("LN").Value = ugR1.Index + 1
            '        ugR1.Cells("SerialNumber").Value = Dr("SerialNumber")
            '        ugR1.Cells("SNStockLink").Value = iStockLink
            '        ugR1.Cells("PrimaryLineID").Value = ugR.Cells("Line").Value
            '    End If
            'Next
            'If iCount = 0 Then
            '    ugR2 = UG.DisplayLayout.Bands(0).AddNew
            '    ugR2.Cells("Line").Value = ugR2.Index + 1
            '    ugR2.Cells("Warehouse").Value = iWH
            '    ugR2.Cells("StockID").Value = iStockLink
            '    ugR2.Cells("Code").Value = iStockLink
            '    ugR2.Cells("Description_1").Value = sDescription_1
            '    ugR2.Cells("Description_2").Value = sDescription_2
            '    ugR2.Cells("IsSerialNo").Value = bSerialItem
            '    ugR2.Cells("IsWH").Value = bWhseItem
            '    'ugR2.Cells("Warehouse").Value = iWH
            '    If iDocType = DocType.SalesOrder Then
            '        ugR2.Cells("TaxType").Value = 1
            '        For Each ugR3 As UltraGridRow In DDTaxType.Rows
            '            If ugR2.Cells("TaxType").Value = ugR3.Cells("Code").Value Then
            '                ugR2.Cells("TaxRate").Value = ugR3.Cells("TaxRate").Value
            '            End If
            '        Next
            '    ElseIf iDocType = DocType.PurchaseOrder Then
            '        ugR2.Cells("TaxType").Value = 3
            '        For Each ugR3 As UltraGridRow In DDTaxType.Rows
            '            If ugR2.Cells("TaxType").Value = ugR3.Cells("Code").Value Then
            '                ugR2.Cells("TaxRate").Value = ugR3.Cells("TaxRate").Value
            '            End If
            '        Next
            '    End If
            '    ugR1 = ugR2.ChildBands(0).Band.AddNew()
            '    ugR1.Cells("LN").Value = ugR1.Index + 1
            '    ugR1.Cells("SerialNumber").Value = Dr("SerialNumber")
            '    ugR1.Cells("SNStockLink").Value = iStockLink
            '    ugR1.Cells("PrimaryLineID").Value = ugR2.Cells("Line").Value
            'End If
            'Next
            'For Each ugR In UG.Rows
            '    If ugR.Cells("IsSerialNo").Value = True Then
            '        ugR.Cells("ConfirmQty").Value = ugR.ChildBands(0).Rows.Count
            '        ugR.Cells("Quantity").Value = ugR.ChildBands(0).Rows.Count
            '    End If
            'Next
            'Get_Total()
            'Else
            'MsgBox("can not find the Serial Number", MsgBoxStyle.Exclamation, "Pastel Evolution")
            '    Exit Sub
            'End If
            '    End With
            'For Each ugR In UG.Rows
            '    ugR.Expanded = False
            'Next

            txtBarCode.ResetText()

        End If

    End Sub


    Private Sub UG_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        'UG.PerformAction(UltraGridAction.CommitRow)
        UG.PerformAction(UltraGridAction.ExitEditMode)

        For Each ugR In UG.Rows
            If ugR.ChildBands.HasChildRows = True Then
                ugR.Cells("ConfirmQty").Value = 0
                For Each ugR1 In ugR.ChildBands(0).Rows
                    If ugR1.Cells("Received").Value = True Then
                        ugR1.ParentRow.Cells("ConfirmQty").Value = ugR1.ParentRow.Cells("ConfirmQty").Value + 1
                        'Exit Sub
                    End If
                Next
            End If
        Next

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
        DatabaseContext.CreateConnection("UMSERVER", sSQLSrvDataBase, sSQLSrvUserName, sSQLSrvPassword, False)

        If iDocType = DocType.SalesOrder Then
            If cmbCustomer.Value = Nothing Then
                MessageBox.Show("Please select a customer", "Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        Dim ugR As UltraGridRow
        ugR = UG.DisplayLayout.Bands(0).AddNew
        ugR.Cells("Line").Value = ugR.Index + 1
        ugR.Cells("Line").Selected = True
        'ugR.Cells("islot").Value = 0


        Dim agent = New Agent(iAgent)

        'If agent.Description = "4" Then
        '    ugR.Cells("Warehouse").Value = 4
        'Else
        'ugR.Cells("Warehouse").Value = iWH
        ugR.Cells("Warehouse").Value = agent.Description
        'End If


        If iDocType = DocType.SalesOrder Then

            If sSQLSrvDataBase = "dbUdawatta_new" Then
                UG.DisplayLayout.Bands(0).Columns("Warehouse").CellActivation = Activation.AllowEdit
            Else
                UG.DisplayLayout.Bands(0).Columns("Warehouse").CellActivation = Activation.Disabled
            End If
            'If iCusPriceList = 2 Or iCusGroup <> 1 And iCusGroup <> 6 Then
            UG.DisplayLayout.Bands(0).Columns("Discount").CellActivation = Activation.Disabled
            UG.DisplayLayout.Bands(0).Columns("DiscountA").CellActivation = Activation.Disabled
            'Else
            '    UG.DisplayLayout.Bands(0).Columns("Discount").CellActivation = Activation.AllowEdit
            '    UG.DisplayLayout.Bands(0).Columns("DiscountA").CellActivation = Activation.AllowEdit
            'End If

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
    End Sub

    Private Sub txtBarCode_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBarCode.ValueChanged

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
        dblLineTotal_Incl_Dis = txtConAmt.Value - txtDisAmount.Value
        txtConAmt.Value = Math.Round(dblLineTotal_Incl_Dis, 2, MidpointRounding.AwayFromZero)
        If txtConTax.Value > 0 Then
            dblLineTax_Dis = Math.Round((dblLineTotal_Incl_Dis * 100 / 112), 2, MidpointRounding.AwayFromZero)
            dblLineTax_Dis = Math.Round(dblLineTotal_Incl_Dis - dblLineTax_Dis, 2, MidpointRounding.AwayFromZero)
            txtConTax.Value = Math.Round(dblLineTax_Dis, 2, MidpointRounding.AwayFromZero)
        End If

    End Sub
    Private Sub UG_BeforeExitEditMode(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs)

    End Sub
    Public Function DiscountA(ByVal bInclusive As Boolean, ByVal Qty As Double, ByVal Discount As Double, ByVal PriceExcl As Double, ByVal PriceIncl As Double, ByVal TaxRate As Double, ByVal DicountA As Double) As Double
        Dim dblDiscountA As Double = 0
        If bInclusive = True Then
            If Discount <> 0 Then
                dblDiscountA = Math.Round((Discount * (Qty * PriceIncl)) / 100, 6, MidpointRounding.AwayFromZero)
            End If
        ElseIf bInclusive = False Then
            If Discount <> 0 Then
                dblDiscountA = Math.Round((Discount * (Qty * PriceExcl)) / 100, 6, MidpointRounding.AwayFromZero)
            End If
        End If
        Return dblDiscountA
    End Function
    Public Function DiscountP(ByVal bInclusive As Boolean, ByVal Qty As Double, ByVal Discount As Double, ByVal PriceExcl As Double, ByVal PriceIncl As Double, ByVal TaxRate As Double, ByVal DicountA As Double) As Double
        Dim dblDiscount As Double = 0
        If bInclusive = True Then
            If DicountA <> 0 Then
                dblDiscount = Math.Round(DicountA / (Qty * PriceIncl) * 100, 6, MidpointRounding.AwayFromZero)
            End If
        ElseIf bInclusive = False Then
            If DicountA <> 0 Then
                dblDiscount = Math.Round(DicountA / (Qty * PriceExcl) * 100, 6, MidpointRounding.AwayFromZero)
            End If
        End If
        Return dblDiscount
    End Function

    Private Sub txtDisAmount_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDisAmount.Leave
        Dim ugR As UltraGridRow
        Dim dblOrderTax As Double = 0
        Dim dblLineTax As Double = 0
        Dim dblOrderTotal_Incl As Double = 0
        Dim dblLineTotal_Incl As Double = 0
        Dim dblLineTotal_Incl_Dis As Double = 0
        Dim dblLineTax_Dis As Double = 0
        For Each ugR In UG.Rows
            dblOrderTax = dblOrderTax + ugR.Cells("OrderTax").Value
            dblOrderTotal_Incl = dblOrderTotal_Incl + ugR.Cells("OrderTotal_Incl").Value
            dblLineTax = dblLineTax + ugR.Cells("LineTax").Value
            dblLineTotal_Incl = dblLineTotal_Incl + ugR.Cells("LineTotal_Incl").Value
        Next

        txtOrdTax.Value = Math.Round(dblOrderTax, 2, MidpointRounding.AwayFromZero)
        txtOrdAmt.Value = Math.Round(dblOrderTotal_Incl, 2, MidpointRounding.AwayFromZero)
        txtConTax.Value = Math.Round(dblLineTax, 2, MidpointRounding.AwayFromZero)
        txtConAmt.Value = Math.Round(dblLineTotal_Incl, 2, MidpointRounding.AwayFromZero)
        txtDisPer.Value = Math.Round(txtDisAmount.Value / dblLineTotal_Incl * 100, 6, MidpointRounding.AwayFromZero)
        dblLineTotal_Incl_Dis = txtConAmt.Value - txtDisAmount.Value
        txtConAmt.Value = Math.Round(dblLineTotal_Incl_Dis, 2, MidpointRounding.AwayFromZero)
        If txtConTax.Value > 0 Then
            dblLineTax_Dis = Math.Round((dblLineTotal_Incl_Dis * 100 / 112), 2, MidpointRounding.AwayFromZero)
            dblLineTax_Dis = Math.Round(dblLineTotal_Incl_Dis - dblLineTax_Dis, 2, MidpointRounding.AwayFromZero)
            txtConTax.Value = Math.Round(dblLineTax_Dis, 2, MidpointRounding.AwayFromZero)
        End If
    End Sub

    Private Sub txtDisPer_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDisPer.Leave
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
        txtDisAmount.Value = Math.Round(dblLineTotal_Incl * txtDisPer.Value / 100, 2, MidpointRounding.AwayFromZero)
        dblLineTotal_Incl_Dis = txtConAmt.Value - txtDisAmount.Value
        txtConAmt.Value = Math.Round(dblLineTotal_Incl_Dis, 2, MidpointRounding.AwayFromZero)

        If txtConTax.Value > 0 Then
            dblLineTax_Dis = Math.Round((dblLineTotal_Incl_Dis * 100 / 112), 2, MidpointRounding.AwayFromZero)
            dblLineTax_Dis = Math.Round(dblLineTotal_Incl_Dis - dblLineTax_Dis, 2, MidpointRounding.AwayFromZero)
            txtConTax.Value = Math.Round(dblLineTax_Dis, 2, MidpointRounding.AwayFromZero)
        End If

    End Sub

    Private Sub cmbSaleRep_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbSaleRep.KeyDown
        If e.KeyData = Keys.F9 Then
            If iDocType = DocType.SalesOrder Then
                tsbSaveSO_Click(tsbSaveSO, e)
            End If
        End If
    End Sub

    Private Sub txtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDescription.KeyDown
        If e.KeyData = Keys.F9 Then
            If iDocType = DocType.SalesOrder Then
                tsbSaveSO_Click(tsbSaveSO, e)
            End If
        End If
    End Sub

    Private Sub txtExtOrder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtExtOrder.KeyDown
        If e.KeyData = Keys.F9 Then
            If iDocType = DocType.SalesOrder Then
                tsbSaveSO_Click(tsbSaveSO, e)
            End If
        End If
    End Sub


    Sub MemorizeStock()

        Dim InventoryItem As Pastel.Evolution.InventoryItem
        Dim stkLink As Integer

        Dim ugr As UltraGridRow

        Dim objSQL As New clsSqlConn
        With objSQL

            Try

                .Begin_Trans()

                SQL = "delete FROM sbSerialMF WHERE AutoIndex =" & CInt(iPONo)
                If .Execute_Sql_Trans(SQL) = 0 Then
                    .Rollback_Trans()
                    Exit Sub
                End If

                For Each ugr In UG.Rows

                    If ugr.Cells("IsSerialNo").Value = True Then
                        If ugr.ChildBands.HasChildRows = True Then

                            InventoryItem = New InventoryItem(CStr(ugr.Cells("Code").Text))
                            stkLink = InventoryItem.ID


                            Dim ugR2 As UltraGridRow
                            For Each ugR2 In ugr.ChildBands(0).Rows
                                SQL = "INSERT INTO sbSerialMF(AutoIndex, SNStockLink, SerialNumber, " & _
                                    " PrimaryLineID,Received) VALUES (" & CInt(iPONo) & "," & stkLink & ", " & _
                                    " '" & CStr(ugR2.Cells("SerialNumber").Value) & "' , " & ugR2.ParentRow.Cells("Line").Value & " , " & IIf(ugR2.Cells("Received").Value = True, 1, 0) & " )"
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
                '.Rollback_Trans()
            Finally
                objSQL.Con_Close()
                GC.Collect()
            End Try

        End With

    End Sub


    Private Sub tsbSaveStock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSaveStock.Click
        Try
            If iDocType = DocType.SalesOrder Then
                UG.DisplayLayout.SaveAsXml(Application.StartupPath & "\" & sAgent & "IBTReceive.xml")
            ElseIf iDocType = DocType.PurchaseOrder Then
                UG.DisplayLayout.SaveAsXml(Application.StartupPath & "\" & sAgent & "IBTReceive.xml")
            ElseIf iDocType = DocType.IBTReceive Then
                UG.DisplayLayout.SaveAsXml(Application.StartupPath & "\" & sAgent & "IBTReceive.xml")
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub UG_CellDataError(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellDataErrorEventArgs)
        'e.StayInEditMode = True
        'e.RaiseErrorEvent = False
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        UG.DisplayLayout.Bands(0).SortedColumns.Clear()
        UG.DisplayLayout.Bands(0).Columns("Line").SortIndicator = SortIndicator.Ascending
    End Sub

    Private Sub UG_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs)

    End Sub

    Private Sub UG_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub

    Private Sub UG_AfterCellUpdate1(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UG.AfterCellUpdate
        If IsNew = True Then
            If e.Cell.Column.Key = "StockID" Then
                If iDocType = DocType.SalesOrder Then
                    'note pricelist set to one
                    SQL = "SELECT     fExclPrice, fInclPrice FROM _etblPriceListPrices WHERE iPriceListNameID =1 AND iStockID =" & e.Cell.Value & ""
                    Dim objSQL As New clsSqlConn
                    With objSQL
                        DS = New DataSet
                        DS = .Get_Data_Sql(SQL)
                        If DS.Tables(0).Rows.Count > 0 Then
                            Dim Dr As DataRow
                            For Each Dr In DS.Tables(0).Rows
                                e.Cell.Row.Cells("Price_Excl").Value = Dr("fExclPrice")
                                e.Cell.Row.Cells("Price_Incl").Value = Dr("fInclPrice")
                            Next
                        Else
                            e.Cell.Row.Cells("Price_Excl").Value = 0
                            e.Cell.Row.Cells("Price_Incl").Value = 0
                        End If
                    End With
                End If
                e.Cell.Row.Cells("AvailableQty").Value = Get_Available_Qty("SELECT WHQtyOnHand FROM WhseStk WHERE WHStockLink =" & e.Cell.Value & " AND WHWhseID =" & e.Cell.Row.Cells("Warehouse").Value & "")

                OEPrice = UG.ActiveRow.Cells("Price_Excl").Value
                OIPrice = UG.ActiveRow.Cells("Price_Incl").Value
            ElseIf e.Cell.Column.Key = "Code" Then
                'bValChange = False

                SQL = " SELECT StkItem.WhseItem, StkItem.iUOMStockingUnitID, StkItem.StockLink, StkItem.Code, " & _
                        " StkItem.Description_1, StkItem.Description_2, StkItem.ItemGroup, StkItem.bLotItem as LotItem , StkItem.LatUCst, " & _
                        " StkItem.AveUCst, _etblUnits.iUnitCategoryID, StkItem.iItemCostingMethod, GrpTbl.fCostMargine, StkItem.ServiceItem FROM StkItem LEFT OUTER JOIN " & _
                        " GrpTbl ON StkItem.ItemGroup = GrpTbl.StGroup LEFT OUTER JOIN _etblUnits ON StkItem.iUOMStockingUnitID = _etblUnits.idUnits " & _
                        " WHERE  StkItem.StockLink  = " & e.Cell.Value & " ORDER BY StkItem.Code"

                Dim objSQL As New clsSqlConn
                With objSQL

                    Dim DS1 = New DataSet
                    DS1 = .Get_Data_Sql(SQL)

                    Dim Dr1 As DataRow

                    If DS1.Tables(0).Rows(0)("ServiceItem") = 1 Then
                        e.Cell.Row.Cells("IsLot").Value = 0
                    Else
                        If DS1.Tables(0).Rows.Count > 0 Then

                            For Each Dr1 In DS1.Tables(0).Rows
                                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                e.Cell.Row.Cells("IsLot").Value = Dr1.Item("LotItem")
                                e.Cell.Row.Cells("IsWH").Value = Dr1.Item("WhseItem")
                            Next
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        End If
                    End If

                End With


            ElseIf e.Cell.Column.Key = "Quantity" Or e.Cell.Column.Key = "Price_Excl" Or e.Cell.Column.Key = "Price_Incl" Or _
                e.Cell.Column.Key = "TaxRate" Or e.Cell.Column.Key = "Discount" Then
                With e.Cell.Row
                    If iDocType = DocType.SalesOrder Then
                        .Cells("ConfirmQty").Value = .Cells("Quantity").Value
                    End If
                    .Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Cells("Quantity").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    .Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Cells("Quantity").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    .Cells("OrderTax").Value = LineTax(bIsInclusive, .Cells("Quantity").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    .Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    .Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    .Cells("LineTax").Value = LineTax(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    .Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    .Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                End With


            End If

            'if it is a serial item disable qty and confirm qty
            'If iDocType = DocType.SalesOrder Then
            '    If e.Cell.Column.Key = "Description_1" Then
            '        If e.Cell.Row.Cells("IsLot").Value = True Then
            '            e.Cell.Row.Cells("ConfirmQty").Activation = Activation.Disabled
            '            e.Cell.Row.Cells("ConfirmQty").Appearance.BackColorDisabled = Color.Silver
            '            e.Cell.Row.Cells("Quantity").Activation = Activation.Disabled
            '            e.Cell.Row.Cells("Quantity").Appearance.BackColorDisabled = Color.Silver
            '        Else
            '            e.Cell.Row.Cells("ConfirmQty").Activation = Activation.AllowEdit
            '            e.Cell.Row.Cells("ConfirmQty").Appearance.BackColorDisabled = Color.White
            '            e.Cell.Row.Cells("Quantity").Activation = Activation.AllowEdit
            '            e.Cell.Row.Cells("Quantity").Appearance.BackColorDisabled = Color.White
            '        End If
            '    End If
            'End If
        Else



            If e.Cell.Column.Key = "Discount" Then
                With e.Cell.Row
                    '.Value = .Text
                    If iDocType = DocType.SalesOrder Then
                        e.Cell.Row.Cells("ConfirmQty").Value = e.Cell.Row.Cells("Quantity").Value
                    End If
                    e.Cell.Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("OrderTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("LineTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                End With
            ElseIf e.Cell.Column.Key = "Price_Incl" Then
                With e.Cell.Row
                    '.Value = .Text
                    If iDocType = DocType.SalesOrder Then
                        e.Cell.Row.Cells("ConfirmQty").Value = e.Cell.Row.Cells("Quantity").Value
                    End If
                    e.Cell.Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("OrderTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("LineTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                End With
            ElseIf e.Cell.Column.Key = "StockID" Then
                With e.Cell.Row


                    If iDocType = DocType.SalesOrder Then
                        SQL = "SELECT     fExclPrice, fInclPrice FROM _etblPriceListPrices WHERE iPriceListNameID =" & iCusPriceList & " AND iStockID =" & e.Cell.Value & ""
                        Dim objSQL As New clsSqlConn
                        With objSQL
                            DS = New DataSet
                            DS = .Get_Data_Sql(SQL)
                            If DS.Tables(0).Rows.Count > 0 Then
                                Dim Dr As DataRow
                                For Each Dr In DS.Tables(0).Rows
                                    e.Cell.Row.Cells("Price_Excl").Value = Dr("fExclPrice")
                                    e.Cell.Row.Cells("Price_Incl").Value = Dr("fInclPrice")
                                Next
                            Else
                                e.Cell.Row.Cells("Price_Excl").Value = 0
                                e.Cell.Row.Cells("Price_Incl").Value = 0
                            End If
                        End With
                    End If



                    '.Value = .Text
                    If iDocType = DocType.SalesOrder Then
                        e.Cell.Row.Cells("ConfirmQty").Value = e.Cell.Row.Cells("Quantity").Value
                    End If
                    e.Cell.Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("OrderTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("LineTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)

                    e.Cell.Row.Cells("AvailableQty").Value = Get_Available_Qty("SELECT WHQtyOnHand FROM WhseStk WHERE WHStockLink =" & e.Cell.Value & " AND WHWhseID =" & e.Cell.Row.Cells("Warehouse").Value & "")

                End With
            ElseIf e.Cell.Column.Key = "LineTax" Then
                With e.Cell.Row
                    '.Value = .Text
                    If iDocType = DocType.SalesOrder Then
                        e.Cell.Row.Cells("ConfirmQty").Value = e.Cell.Row.Cells("Quantity").Value
                    End If
                    e.Cell.Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("OrderTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("LineTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                End With

            ElseIf e.Cell.Column.Key = "Quantity" Or e.Cell.Column.Key = "Price_Excl" Or e.Cell.Column.Key = "Price_Incl" Or _
            e.Cell.Column.Key = "TaxRate" Or e.Cell.Column.Key = "Discount" Then
                With e.Cell.Row
                    If iDocType = DocType.SalesOrder Then
                        .Cells("ConfirmQty").Value = .Cells("Quantity").Value
                    End If
                    .Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Cells("Quantity").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    .Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Cells("Quantity").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    .Cells("OrderTax").Value = LineTax(bIsInclusive, .Cells("Quantity").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    .Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    .Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    .Cells("LineTax").Value = LineTax(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    .Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    .Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                End With

            End If

        End If
    End Sub

    Private Sub UG_AfterEnterEditMode(ByVal sender As Object, ByVal e As System.EventArgs) Handles UG.AfterEnterEditMode
        If UG.ActiveCell.Column.Key = "Lot" Then
            With UG.ActiveCell
                sSQL = " SELECT   _etblLotTracking.idLotTracking,_etblLotTracking.cLotDescription AS Description, _etblLotTracking.dExpiryDate AS [Expiry Date], _etblLotTrackingQty.fQtyOnHand AS [Qty On Hand], " & _
                " _etblLotTrackingQty.fQtyReserved AS [Qty Reserved]" & _
                " FROM         _etblLotTracking INNER JOIN " & _
                " _etblLotTrackingQty ON _etblLotTracking.idLotTracking = _etblLotTrackingQty.iLotTrackingID WHERE  _etblLotTracking.iStockID = " & UG.ActiveRow.Cells("StockID").Value & " AND _etblLotTrackingQty.fQtyOnHand > 0 AND _etblLotTrackingQty.iWarehouseID =  " & UG.ActiveRow.Cells("warehouse").Value & " "

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
        ElseIf UG.ActiveCell.Column.Key = "UOM" Then
            With UG.ActiveCell

                'With conversion-----------------------------------------------------------------------------------------
                'sSQL = "   SELECT DISTINCT _etblUnits.idUnits, _etblUnits.cUnitCode, _etblUnits.cUnitDescription " & _
                '        " FROM         _etblUnits CROSS JOIN    _etblUnitConversion " & _
                '        " where _etblUnits.iUnitCategoryID IN  " & _
                '        " (SELECT DISTINCT  _etblUnitCategory.idUnitCategory " & _
                '        " FROM         StkItem INNER JOIN " & _
                '        "    _etblUnits ON StkItem.iUOMStockingUnitID = _etblUnits.idUnits INNER JOIN " & _
                '        "   _etblUnitCategory ON _etblUnits.iUnitCategoryID = _etblUnitCategory.idUnitCategory CROSS JOIN " & _
                '        "   _etblUnitConversion " & _
                '        " WHERE StkItem.StockLink = " & UG.ActiveRow.Cells("StockID").Value & ")  "

                sSQL = " SELECT DISTINCT _etblUnits.idUnits, _etblUnits.cUnitCode, _etblUnits.cUnitDescription " & _
                " FROM         _etblUnits INNER JOIN " & _
                " StkItem ON _etblUnits.idUnits = StkItem.iUOMDefSellUnitID CROSS JOIN " & _
                "  _etblUnitConversion   WHERE StkItem.StockLink = " & UG.ActiveRow.Cells("StockID").Value & "  "

                Try
                    Con1.ConnectionString = sConStr
                    CMD = New SqlCommand(sSQL, Con1)
                    DS = New DataSet
                    DA = New SqlDataAdapter(CMD)

                    Con1.Open()
                    DA.Fill(DS)
                    Con1.Close()

                    DDUnit.DataSource = DS.Tables(0)
                    DDUnit.ValueMember = "idUnits"
                    DDUnit.DisplayMember = "cUnitCode"
                    DDUnit.DisplayLayout.Bands(0).Columns("idUnits").Hidden = True

                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                    Exit Sub
                Finally
                    If Con1.State = ConnectionState.Open Then Con1.Close()
                End Try


            End With
        End If
    End Sub

    Private Sub UG_BeforeExitEditMode1(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs) Handles UG.BeforeExitEditMode
        If UG.ActiveCell.Column.Key = "Quantity" Then
            With UG.ActiveCell
                .Value = .Text
                If iDocType = DocType.SalesOrder Then
                    .Row.Cells("ConfirmQty").Value = .Row.Cells("Quantity").Value
                End If
                '.Row.Cells("Discount").Value = DiscountGrp(bIsInclusive, .Row.Cells("Quantity").Value, 0, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value, .Row.Cells("StockID").Value, .Value)
                '.Row.Cells("DiscountA").Value = DiscountAGrp(bIsInclusive, .Row.Cells("Quantity").Value, 0, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value, .Row.Cells("StockID").Value, .Value)
                .Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTax").Value = LineTax(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTax").Value = LineTax(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
            End With
        ElseIf UG.ActiveCell.Column.Key = "ConfirmQty" Then
            With UG.ActiveCell
                .Value = .Text
                If iDocType = DocType.SalesOrder Then
                    .Row.Cells("ConfirmQty").Value = .Row.Cells("Quantity").Value
                End If
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
                '.Row.Cells("DiscountA").Value = DiscountA(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value, .Row.Cells("StockID").Value, .Value)
                '.Row.Cells("DiscountA").Value = DiscountA(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value, .Row.Cells("StockID").Value, .Value)
                If iDocType = DocType.SalesOrder Then
                    '.Row.Cells("Discount").Value = DiscountPP(.Row.Cells("Discount").Value, .Row.Cells("StockID").Value)
                End If
                .Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTax").Value = LineTax(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTax").Value = LineTax(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
            End With
        ElseIf UG.ActiveCell.Column.Key = "DiscountA" Then
            With UG.ActiveCell
                .Value = .Text
                '.Row.Cells("Discount").Value = DiscountP(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value, .Row.Cells("StockID").Value, .Value)
                .Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTax").Value = LineTax(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTax").Value = LineTax(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
            End With

            'ElseIf UG.ActiveCell.Column.Key = "Description_1" Then
            '    With UG.ActiveCell
            '        sSQL = " SELECT   _etblLotTracking.idLotTracking,_etblLotTracking.cLotDescription AS Description, _etblLotTracking.dExpiryDate AS [Expiry Date], _etblLotTrackingQty.fQtyOnHand AS [Qty On Hand], " & _
            '        " _etblLotTrackingQty.fQtyReserved AS [Qty Reserved]" & _
            '        " FROM         _etblLotTracking INNER JOIN " & _
            '        " _etblLotTrackingQty ON _etblLotTracking.idLotTracking = _etblLotTrackingQty.iLotTrackingID WHERE  _etblLotTracking.iStockID = " & .Row.Cells("StockID").Value & " AND _etblLotTrackingQty.fQtyOnHand > 0 "

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
            '            DDLot.DisplayLayout.Bands(0).("idColumnsLotTracking").Hidden = True

            '        Catch ex As Exception
            '            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
            '            Exit Sub
            '        Finally
            '            If Con1.State = ConnectionState.Open Then Con1.Close()
            '        End Try


            '    End With
        End If
        Get_Total()
    End Sub

    Private Sub UG_CellChange1(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UG.CellChange
        Try
            If UG.ActiveCell.Column.Index = 1 Then  'Code
                If DDStock.ActiveRow.Activated = True Then
                    UG.ActiveRow.Cells("StockID").Value = DDStock.ActiveRow.Cells("StockLink").Value
                    UG.ActiveRow.Cells("Code").Value = DDStock.ActiveRow.Cells("StockLink").Value
                    UG.ActiveRow.Cells("Description_1").Value = DDStock.ActiveRow.Cells("Description_1").Value
                    UG.ActiveRow.Cells("Description_2").Value = DDStock.ActiveRow.Cells("Code").Value
                    UG.ActiveRow.Cells("iUnit").Value = DDStock.ActiveRow.Cells("iUOMStockingUnitID").Value
                    UG.ActiveRow.Cells("ItemGroup").Value = DDStock.ActiveRow.Cells("ItemGroup").Value
                    UG.ActiveRow.Cells("cValue").Value = DDStock.ActiveRow.Cells("cValue").Value

                    If DDStock.ActiveRow.Cells("iItemCostingMethod").Value = 0 Then
                        UG.ActiveRow.Cells("CostPrice").Value = Math.Round(DDStock.ActiveRow.Cells("AveUCst").Value, 2, MidpointRounding.AwayFromZero)
                    ElseIf DDStock.ActiveRow.Cells("iItemCostingMethod").Value = 1 Then
                        UG.ActiveRow.Cells("CostPrice").Value = Math.Round(DDStock.ActiveRow.Cells("LatUCst").Value, 2, MidpointRounding.AwayFromZero)
                    Else
                    End If
                    UG.ActiveRow.Cells("Line").Value = UG.ActiveRow.Index + 1
                    UG.ActiveRow.Cells("iUnitCate").Value = DDStock.ActiveRow.Cells("iUnitCategoryID").Value
                    UG.ActiveRow.Cells("IsSerialNo").Value = IIf(DDStock.ActiveRow.Cells("SerialItem").Value = True, True, False)
                    UG.ActiveRow.Cells("IsWH").Value = IIf(DDStock.ActiveRow.Cells("WhseItem").Value = True, True, False)
                    UG.ActiveRow.Cells("fCostMargine").Value = DDStock.ActiveRow.Cells("fCostMargine").Value

                    If UG.ActiveRow.Cells("IsWH").Value = True And sSQLSrvDataBase = "dbUdawatta_new" Then
                        UG.ActiveRow.Cells("Warehouse").Activation = Activation.AllowEdit
                    Else
                        UG.ActiveRow.Cells("Warehouse").Activation = Activation.Disabled
                    End If


                End If
            ElseIf UG.ActiveCell.Column.Index = 2 Then  'Description_1
                If DDDescription.ActiveRow.Activated = True Then
                    UG.ActiveRow.Cells("Quantity").Value = 0
                    UG.ActiveRow.Cells("StockID").Value = DDDescription.ActiveRow.Cells("StockLink").Value
                    UG.ActiveRow.Cells("Code").Value = DDDescription.ActiveRow.Cells("StockLink").Value
                    UG.ActiveRow.Cells("Description_1").Value = DDDescription.ActiveRow.Cells("Description_1").Value
                    UG.ActiveRow.Cells("Description_2").Value = DDDescription.ActiveRow.Cells("Code").Value
                    UG.ActiveRow.Cells("iUnit").Value = DDDescription.ActiveRow.Cells("iUOMStockingUnitID").Value
                    UG.ActiveRow.Cells("ItemGroup").Value = DDDescription.ActiveRow.Cells("ItemGroup").Value
                    UG.ActiveRow.Cells("cValue").Value = DDDescription.ActiveRow.Cells("cValue").Value

                    'Commented by Husny coz error
                    'If DDDescription.ActiveRow.Cells("iItemCostingMethod").Value = 0 Then
                    '    UG.ActiveRow.Cells("CostPrice").Value = Math.Round(DDDescription.ActiveRow.Cells("AveUCst").Value, 2, MidpointRounding.AwayFromZero)
                    'ElseIf DDDescription.ActiveRow.Cells("iItemCostingMethod").Value = 1 Then
                    '    UG.ActiveRow.Cells("CostPrice").Value = Math.Round(DDDescription.ActiveRow.Cells("LatUCst").Value, 2, MidpointRounding.AwayFromZero)
                    'Else
                    'End If

                    If DDDescription.ActiveRow.Cells("iUOMStockingUnitID").Value = 0 Then
                        UG.ActiveRow.Cells("UOM").Value = 0
                        UG.ActiveRow.Cells("UOM").Activated = False
                        UG.ActiveRow.Cells("UOM").Appearance.BackColorDisabled = Color.Gray
                        'Else
                        '    UG.ActiveRow.Cells("UOM").Activated = True
                        '    UG.ActiveRow.Cells("UOM").Appearance.BackColorDisabled = Color.White
                    End If

                    'UG.ActiveRow.Cells("iUnit").Value = DDDescription.ActiveRow.Cells("iUOMStockingUnitID").Value
                    'UG.ActiveRow.Cells("IsSerialNo").Value = IIf(DDDescription.ActiveRow.Cells("SerialItem").Value = True, True, False)
                    UG.ActiveRow.Cells("IsWH").Value = IIf(DDDescription.ActiveRow.Cells("WhseItem").Value = True, True, False)
                    '-----------important-----------------------------------------------------------------------------------
                    'UG.ActiveRow.Cells("fCostMargine").Value = DDDescription.ActiveRow.Cells("fCostMargine").Value         |
                    '-------------------------------------------------------------------------------------------------------
                    If UG.ActiveRow.Cells("IsWH").Value = True And sSQLSrvDataBase = "dbUdawatta_new" Then
                        UG.ActiveRow.Cells("Warehouse").Activation = Activation.AllowEdit
                    Else
                        UG.ActiveRow.Cells("Warehouse").Activation = Activation.Disabled
                    End If
                    UG.ActiveRow.Cells("Line").Value = UG.ActiveRow.Index + 1

                    UG.ActiveRow.Cells("lot").Value = 0

                End If
            ElseIf UG.ActiveCell.Column.Index = 11 Then 'TaxType
                If DDTaxType.ActiveRow.Activated = True Then
                    UG.ActiveRow.Cells("TaxType").Value = DDTaxType.ActiveRow.Cells("Code").Value
                    UG.ActiveRow.Cells("TaxRate").Value = DDTaxType.ActiveRow.Cells("TaxRate").Value
                End If
            ElseIf UG.ActiveCell.Column.Index = 5 Then  'WareHouse
                If DDWH.ActiveRow.Activated = True Then
                    UG.ActiveRow.Cells("Warehouse").Value = DDWH.ActiveRow.Cells("WhseLink").Value
                    UG.ActiveRow.Cells("AvailableQty").Value = Get_Available_Qty("SELECT WHQtyOnHand FROM WhseStk WHERE WHStockLink =" & e.Cell.Row.Cells("Code").Value & " AND WHWhseID =" & e.Cell.Row.Cells("Warehouse").Value & "")
                    UG.ActiveRow.Cells("Lot").Value = ""
                End If


            ElseIf UG.ActiveCell.Column.Index = 36 Then  'Lot
                If DDLot.ActiveRow.Activated = True Then
                    UG.ActiveRow.Cells("Lot").Value = DDLot.ActiveRow.Cells("idLotTracking").Value
                    'UG.ActiveRow.Cells("AvailableQty").Value = Get_Available_Qty("SELECT WHQtyOnHand FROM WhseStk WHERE WHStockLink =" & e.Cell.Row.Cells("Code").Value & " AND WHWhseID =" & e.Cell.Row.Cells("Warehouse").Value & "")
                End If
                If UG.ActiveRow.Cells("Lot").Text.Substring(0, 3) = "AIR" Then
                    UG.ActiveRow.Cells("Discount").Value = 0
                End If
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub UG_InitializeLayout_1(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UG.InitializeLayout

    End Sub

    Private Sub UG_KeyDown1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles UG.KeyDown
        Try
            If iDocType = DocType.SalesOrder Then
                If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then

                    Select Case (e.KeyCode)

                        Case Keys.Up
                            UG.PerformAction(UltraGridAction.ExitEditMode, False, False)
                            UG.PerformAction(UltraGridAction.AboveRow, False, False)
                            e.Handled = True
                            UG.ActiveRow.Cells("Code").Activate()


                            UG.PerformAction(UltraGridAction.BelowCell, False, False)
                            UG.PerformAction(UltraGridAction.EnterEditMode, False, False)
                            Exit Select
                        Case Keys.Down
                            If UG.ActiveCell.Column.Key <> "Lot" Then
                                UG.PerformAction(UltraGridAction.ExitEditMode, False, False)
                                UG.PerformAction(UltraGridAction.BelowRow, False, False)
                                e.Handled = True
                                'UG.PerformAction(UltraGridAction.EnterEditMode, False, False)
                                UG.ActiveRow.Cells("Code").Activate()
                                'UG.ActiveRow.Cells("Code").Value = ""
                                'UG.ActiveRow.Cells("Description_1").
                                UG.PerformAction(UltraGridAction.BelowCell, False, False)
                                UG.PerformAction(UltraGridAction.EnterEditMode, False, False)

                                'nwe line code---------------------------------------------------------------------
                                'Dim ugR As UltraGridRow
                                'If UG.DisplayLayout.Rows(UG.ActiveRow.Index + 1) = Nothing Then

                                'End If
                                'ugR = UG.DisplayLayout.Bands(0).AddNew
                                'ugR.Cells("Line").Value = ugR.Index + 1
                                'ugR.Cells("Line").Selected = True
                                ''ugR.Cells("islot").Value = 0
                                'ugR.Cells("Warehouse").Value = iWH
                                'If iDocType = DocType.SalesOrder Then
                                '    UG.DisplayLayout.Bands(0).Columns("Warehouse").CellActivation = Activation.Disabled
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
                                '---------------------------------------------------------------------------------
                            End If
                            Exit Select
                        Case Keys.Right
                            UG.PerformAction(UltraGridAction.ExitEditMode, False, False)
                            UG.PerformAction(UltraGridAction.NextCellByTab, False, False)
                            e.Handled = True
                            UG.PerformAction(UltraGridAction.EnterEditMode, False, False)
                            Exit Select
                        Case Keys.Left
                            UG.PerformAction(UltraGridAction.ExitEditMode, False, False)
                            UG.PerformAction(UltraGridAction.PrevCellByTab, False, False)
                            e.Handled = True
                            UG.PerformAction(UltraGridAction.EnterEditMode, False, False)
                            Exit Select
                    End Select


                End If
            End If

            If iDocType = DocType.PurchaseOrder Then
                If UG.ActiveCell.Column.Key = "SerialLot" Then
                    If e.KeyCode = Keys.Enter Then
                        With frmCreateLot
                            .Label3.Text = UG.ActiveRow.Cells("StockID").Value.ToString
                        End With
                        frmCreateLot.txtLot.Text = txtDelNote_SupInvNo.Text + "-" + UG.ActiveRow.Cells("Description_1").Value
                        frmCreateLot.ShowDialog()


                        If LotCN = True Then
                            UG.ActiveRow.Cells("SerialLot").Value = frmCreateLot.txtLot.Text
                        Else
                            UG.ActiveRow.Cells("SerialLot").Value = frmCreateLot.cmbLot.Text
                            'e.Cell.Row.Cells("SerialLot"). = frmCreateLot.cmbLot.Text
                        End If

                        UG.ActiveRow.Cells("Lot_Exp").Value = frmCreateLot.dtpExpDate.Value


                    End If
                End If
            End If








            If e.KeyCode = Keys.Tab Then
                Dim Col As String = ""
                If UG.DisplayLayout.Bands(0).Columns("Discount").CellActivation = Activation.Disabled Then
                    Col = "Quantity"
                Else
                    Col = "Discount"
                End If
                If UG.ActiveCell.Column.Key = Col And iDocType = DocumentType.SalesOrder Then
                    If UG.ActiveRow.Cells("Code").Value <> 0 Then
                        If UG.ActiveRow.HasNextSibling = True Then Exit Sub
                        Dim ugR As UltraGridRow
                        ugR = UG.DisplayLayout.Bands(0).AddNew
                        ugR.Cells("Line").Value = ugR.Index + 1
                        ugR.Cells("Line").Selected = True


                        Dim agent = New Agent(iAgent)

                        'If agent.Description = "4" Then
                        '    ugR.Cells("Warehouse").Value = 4
                        'Else
                        ugR.Cells("Warehouse").Value = agent.Description
                        'End If


                        ugR.Cells(1).Activate()
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
                    Else
                        MsgBox("Item Code can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
                        Exit Sub
                    End If
                ElseIf UG.ActiveCell.Column.Key = "Discount" And iDocType = DocumentType.PurchaseOrder Then
                    If UG.ActiveRow.Cells("Code").Value <> 0 Then
                        If UG.ActiveRow.HasNextSibling = True Then Exit Sub
                        Dim ugR As UltraGridRow
                        ugR = UG.DisplayLayout.Bands(0).AddNew
                        ugR.Cells("Line").Value = ugR.Index + 1
                        'ugR.Cells("Line").Selected = True


                        Dim agent = New Agent(iAgent)

                        'If agent.Description = "4" Then
                        '    ugR.Cells("Warehouse").Value = 4
                        'Else
                        ugR.Cells("Warehouse").Value = agent.Description
                        'End If


                        'ugR.Cells("Code").Activate()

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

                        ugR.Cells("Description_1").Activate()
                        ugR.Cells("Description_1").Value = ""
                    Else
                        MsgBox("Item Code can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
                        Exit Sub
                    End If
                End If
            ElseIf e.KeyData = Keys.F9 Then
                If iDocType = DocType.SalesOrder Then
                    tsbSaveSO_Click(tsbSaveSO, e)
                End If
            End If
        Catch ex As Exception
            Exit Sub

        End Try
    End Sub

    Private Sub UG_Leave1(ByVal sender As Object, ByVal e As System.EventArgs) Handles UG.Leave
        Delete_Row()
    End Sub

    Private Sub DDLot_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLot.Click
        If DDLot.ActiveRow.Activated = True Then
            If UG.ActiveRow.Cells("iUnit").Value = 0 Then 'if not a UOM item
                If UG.ActiveRow.Cells("Quantity").Value > DDLot.ActiveRow.Cells("Qty On Hand").Value Then
                    MsgBox("Quantity not available in Lot", MsgBoxStyle.Information, "Pastel Evolution AddOn")
                    UG.ActiveRow.Cells("Quantity").Value = DDLot.ActiveRow.Cells("Qty On Hand").Value
                    UG.ActiveRow.Cells("Lot").Value = 0
                End If
                Lot = DDLot.ActiveRow.Cells("Description").Value
                Lot_Exp = DDLot.ActiveRow.Cells("Expiry Date").Value
            End If
        End If
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        UG.PerformAction(UltraGridAction.CommitRow)

        Try



            'getDefaultwh(sError, iToWH)

            'If sError <> "" Then
            '    Exit Sub
            'End If

            processRTN(iRTNNo, True, iRTNNo)

            If iRTNNo = 0 Then 'if sales order didnt process
                Exit Sub
            End If


            UG.DisplayLayout.Bands(0).SortedColumns.Clear()
            UG.DisplayLayout.Bands(0).Columns("Line").SortIndicator = SortIndicator.Ascending

            'ProcessCRN()
            'ProcessPO()
            ProcessManualPO()

            If MsgBox("Do you want to print IBT now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
                Dim objRep As New ReportDocument
                Dim s As String = Application.StartupPath

                'If iDocType_1 = 10 Then
                objRep.Load(Application.StartupPath & "\RTN.rpt")
                'ElseIf iDocType_1 = 8 Then 
                '    objRep.Load(Application.StartupPath & "\IBT_R.rpt")
                'End If

                Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= dbKelaniya_new ")
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

                frmPrintPreview.vSelectionFormula = "{InvNum.AutoIndex}= " & iRTNNo & ""

                iRepoID = 2

                frmPrintPreview.ShowDialog()
                iRepoID = 0

            End If

            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()


            Call Discard()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Pastel Evolution")
            Exit Sub

        Finally

            If Con2.State = ConnectionState.Open Then Con2.Close()

        End Try

        'tsbSave.Enabled = False
        Me.Close()

    End Sub

    Private Sub ProcessManualPO()

        Dim objSQL As New clsSqlConn
        With objSQL

            SQL = "SELECT 5,1,1,1,0,0,2,'Purchase Order',InvDate,OrderDate,DueDate " & _
                   " ,DeliveryDate,1,1,'IBT',0,0,OrderNum,0,0,0,0,0,0,0,0,1,0,ExtOrderNum " & _
                   " ,0,0,0,0,0,0,0,0,0,0,0 " & _
                   " ,OrdTotExclDEx,OrdTotTaxDEx,OrdTotInclDEx,OrdTotExcl,OrdTotTax " & _
                   " ,OrdTotIncl,bUseFixedPrices,0,0,1,0,0,0,0,0,0,0,0,0,0,0 " & _
                   " ,fOrdTotExclDExForeign,fOrdTotTaxDExForeign,fOrdTotInclDExForeign " & _
                   " ,fOrdTotExclForeign,fOrdTotTaxForeign,fOrdTotInclForeign,'UDAWATTA MOTORS PVT LTD - PKW' " & _
                   " ,0,0,0,0,0,0,0,0,0,0,0,0,OrdTotInclExRounding,0,fOrdTotInclForeignExRounding " & _
                   " ,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0 " & _
                   " FROM " & sSQLSrvDataBase & ".dbo.InvNum WHERE AutoIndex = " & iRTNNo & " "

            DS = New DataSet
            DS = .Get_Data_Sql(SQL)

            'End With


            Con2 = New SqlConnection("Server= UMdbSERVER  ;User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= '" & cmbGRVLoc.Text & "' ")

            'objSQL = New clsSqlConn
            'With objSQL
            '.Begin_Trans()
            SQL = " INSERT INTO [InvNum]([DocType],[DocVersion],[DocState],[DocFlag],[OrigDocID],[GrvID] " & _
               " ,[AccountID],[Description],[InvDate],[OrderDate],[DueDate],[DeliveryDate],[TaxInclusive],[Email_Sent]" & _
               " ,[PAddress1],[DelMethodID],[DocRepID],[OrderNum],[InvDisc],[InvDiscReasonID],[ProjectID],[TillID]" & _
               " ,[POSAmntTendered],[POSChange],[GrvSplitFixedCost],[GrvSplitFixedAmnt],[OrderStatusID],[OrderPriorityID]" & _
               " ,[ExtOrderNum],[ForeignCurrencyID],[InvDiscAmnt],[InvDiscAmntEx],[InvTotExclDEx],[InvTotTaxDEx]" & _
               " ,[InvTotInclDEx],[InvTotExcl],[InvTotTax],[InvTotIncl],[OrdDiscAmnt],[OrdDiscAmntEx],[OrdTotExclDEx]" & _
               " ,[OrdTotTaxDEx],[OrdTotInclDEx],[OrdTotExcl],[OrdTotTax],[OrdTotIncl],[bUseFixedPrices],[iDocPrinted]" & _
               " ,[iINVNUMAgentID],[fExchangeRate],[fGrvSplitFixedAmntForeign],[fInvDiscAmntForeign],[fInvDiscAmntExForeign]" & _
               " ,[fInvTotExclDExForeign],[fInvTotTaxDExForeign],[fInvTotInclDExForeign],[fInvTotExclForeign]" & _
               " ,[fInvTotTaxForeign],[fInvTotInclForeign],[fOrdDiscAmntForeign],[fOrdDiscAmntExForeign],[fOrdTotExclDExForeign]" & _
               " ,[fOrdTotTaxDExForeign],[fOrdTotInclDExForeign],[fOrdTotExclForeign],[fOrdTotTaxForeign],[fOrdTotInclForeign]" & _
               " ,[cAccountName],[iProspectID],[iOpportunityID],[InvTotRounding],[OrdTotRounding],[fInvTotForeignRounding]" & _
               " ,[fOrdTotForeignRounding],[bInvRounding],[iInvSettlementTermsID],[iOrderCancelReasonID],[iLinkedDocID]" & _
               " ,[bLinkedTemplate],[InvTotInclExRounding],[OrdTotInclExRounding],[fInvTotInclForeignExRounding]" & _
               " ,[fOrdTotInclForeignExRounding],[iEUNoTCID],[iPOAuthStatus],[iPOIncidentID],[iSupervisorID],[iMergedDocID]" & _
               " ,[InvNum_iBranchID],[InvNum_iCreatedBranchID],[InvNum_iModifiedBranchID],[InvNum_iCreatedAgentID]" & _
               " ,[InvNum_iModifiedAgentID],[InvNum_iChangeSetID],[iDocEmailed],[fDepositAmountForeign],[fRefundAmount]" & _
               " ,[bTaxPerLine],[fDepositAmountTotal],[fDepositAmountUnallocated],[fDepositAmountNew]" & _
               " ,[fDepositAmountTotalForeign],[fDepositAmountUnallocatedForeign],[fRefundAmountForeign]) " & _
     " VALUES (5,1,1,1,0,0,2,'Purchase Order'," & DS.Tables(0).Rows(0)("InvDate") & "," & DS.Tables(0).Rows(0)("OrderDate") & "," & DS.Tables(0).Rows(0)("DueDate") & " " & _
               " ," & DS.Tables(0).Rows(0)("DeliveryDate") & ",1,1,'IBT',0,0,'" & DS.Tables(0).Rows(0)("OrderNum") & "',0,0,0,0,0,0,0,0,1,0,'" & DS.Tables(0).Rows(0)("ExtOrderNum") & "' " & _
               " ,0,0,0,0,0,0,0,0,0,0,0 " & _
               " ," & DS.Tables(0).Rows(0)("OrdTotExclDEx") & "," & DS.Tables(0).Rows(0)("OrdTotTaxDEx") & "," & DS.Tables(0).Rows(0)("OrdTotInclDEx") & "," & DS.Tables(0).Rows(0)("OrdTotExcl") & "," & DS.Tables(0).Rows(0)("OrdTotTax") & " " & _
               " ," & DS.Tables(0).Rows(0)("OrdTotIncl") & ",0,0,0,1,0,0,0,0,0,0,0,0,0,0,0 " & _
               " ," & DS.Tables(0).Rows(0)("fOrdTotExclDExForeign") & "," & DS.Tables(0).Rows(0)("fOrdTotTaxDExForeign") & "," & DS.Tables(0).Rows(0)("fOrdTotInclDExForeign") & " " & _
               " ," & DS.Tables(0).Rows(0)("fOrdTotExclForeign") & "," & DS.Tables(0).Rows(0)("fOrdTotTaxForeign") & "," & DS.Tables(0).Rows(0)("fOrdTotInclForeign") & ",'UDAWATTA MOTORS PVT LTD - PKW' " & _
               " ,0,0,0,0,0,0,0,0,0,0,0,0," & DS.Tables(0).Rows(0)("OrdTotInclExRounding") & ",0," & DS.Tables(0).Rows(0)("fOrdTotInclForeignExRounding") & " " & _
               " ,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0) "


            '(" & DS.Tables(0).Rows(0)("InvDate") & ")"


            Con2.Open()

            CMD = New SqlCommand(SQL, Con2)
            CMD.CommandType = CommandType.Text
            CMD.ExecuteNonQuery()

            Con2.Close()
            'If .Execute_Sql_Trans(SQL) = 0 Then
            '    .Rollback_Trans()
            '    Exit Sub
            'End If
            '.Commit_Trans()

        End With

        'inserting invoice lines----------------------------------------------------------------------------------

        Con2 = New SqlConnection("Server= UMSERVER  ;User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= '" & sSQLSrvDataBase & "' ")


        objSQL = New clsSqlConn
        With objSQL

            SQL = "  Select " & _
                   "  iInvoiceID,0,0,0, " & _
                   "  cDescription,1,1,1, " & _
                   "  fQuantity,0, " & _
                   "  fQtyToProcess,0,0,0,0,'', " & _
                   "  fUnitPriceExcl, " & _
                   "  fUnitPriceIncl,0,0,0,0,0,11,0,1,0,'', " & _
                   "  iStockCodeID,0, " & _
                   "  iWarehouseID,1,0, " & _
                   "  fQuantityLineTotIncl, " & _
                   "  fQuantityLineTotExcl,  " & _
                   "  fQuantityLineTotInclNoDisc,  " & _
                   "  fQuantityLineTotExclNoDisc,  " & _
                   "  fQuantityLineTaxAmount,  " & _
                   "  fQuantityLineTaxAmountNoDisc, 0,0,0,0,0,0,0,0,0,0, " & _
                   "  fQtyToProcessLineTaxAmount,0,0,0,0,0,0,0,0,0,0,0,0,0, " & _
                   "  fUnitPriceExclForeign,  " & _
                   "  fUnitPriceInclForeign,0,0, " & _
                   "  fQuantityLineTotInclForeign, " & _
                   " fQuantityLineTotExclForeign, " & _
                   " fQuantityLineTotInclNoDiscForeign, " & _
                   " fQuantityLineTotExclNoDiscForeign, " & _
                   " fQuantityLineTaxAmountForeign,  " & _
                   " fQuantityLineTaxAmountNoDiscForeign, 0,0,0,0,0,0,0,0,0,0, " & _
                   " fQtyToProcessLineTaxAmountForeign,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0, " & _
                   " cLotNumber,  " & _
                   " dLotExpiryDate,  " & _
                   " iLineID,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,'',0,0 " & _
                   " from _btblInvoiceLines where iInvoiceID = " & iRTNNo & " "

            DS = New DataSet
            DS = .Get_Data_Sql(SQL)


            Con2 = New SqlConnection("Server= UMdbSERVER  ;User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= '" & cmbGRVLoc.Text & "' ")

            For Each i As Integer In DS.Tables(0).Rows

                SQL = " INSERT INTO [_btblInvoiceLines] " & _
                 " ([iInvoiceID],[iOrigLineID],[iGrvLineID],[iLineDocketMode],[cDescription],[iUnitsOfMeasureStockingID] " & _
                 " ,[iUnitsOfMeasureCategoryID],[iUnitsOfMeasureID],[fQuantity],[fQtyChange],[fQtyToProcess],[fQtyLastProcess] " & _
                 " ,[fQtyProcessed],[fQtyReserved],[fQtyReservedChange],[cLineNotes],[fUnitPriceExcl],[fUnitPriceIncl] " & _
                 " ,[iUnitPriceOverrideReasonID],[fUnitCost],[fLineDiscount],[iLineDiscountReasonID],[iReturnReasonID] " & _
                 " ,[fTaxRate],[bIsSerialItem],[bIsWhseItem],[fAddCost],[cTradeinItem],[iStockCodeID],[iJobID],[iWarehouseID] " & _
                 " ,[iTaxTypeID],[iPriceListNameID],[fQuantityLineTotIncl],[fQuantityLineTotExcl],[fQuantityLineTotInclNoDisc] " & _
                 " ,[fQuantityLineTotExclNoDisc],[fQuantityLineTaxAmount],[fQuantityLineTaxAmountNoDisc],[fQtyChangeLineTotIncl] " & _
                 " ,[fQtyChangeLineTotExcl],[fQtyChangeLineTotInclNoDisc],[fQtyChangeLineTotExclNoDisc],[fQtyChangeLineTaxAmount] " & _
                 " ,[fQtyChangeLineTaxAmountNoDisc],[fQtyToProcessLineTotIncl],[fQtyToProcessLineTotExcl],[fQtyToProcessLineTotInclNoDisc] " & _
                 " ,[fQtyToProcessLineTotExclNoDisc],[fQtyToProcessLineTaxAmount],[fQtyToProcessLineTaxAmountNoDisc] " & _
                 " ,[fQtyLastProcessLineTotIncl],[fQtyLastProcessLineTotExcl],[fQtyLastProcessLineTotInclNoDisc] " & _
                 " ,[fQtyLastProcessLineTotExclNoDisc],[fQtyLastProcessLineTaxAmount],[fQtyLastProcessLineTaxAmountNoDisc] " & _
                 " ,[fQtyProcessedLineTotIncl],[fQtyProcessedLineTotExcl],[fQtyProcessedLineTotInclNoDisc] " & _
                 " ,[fQtyProcessedLineTotExclNoDisc],[fQtyProcessedLineTaxAmount],[fQtyProcessedLineTaxAmountNoDisc] " & _
                 " ,[fUnitPriceExclForeign],[fUnitPriceInclForeign],[fUnitCostForeign],[fAddCostForeign],[fQuantityLineTotInclForeign] " & _
                 " ,[fQuantityLineTotExclForeign],[fQuantityLineTotInclNoDiscForeign],[fQuantityLineTotExclNoDiscForeign] " & _
                 " ,[fQuantityLineTaxAmountForeign],[fQuantityLineTaxAmountNoDiscForeign],[fQtyChangeLineTotInclForeign] " & _
                 " ,[fQtyChangeLineTotExclForeign],[fQtyChangeLineTotInclNoDiscForeign],[fQtyChangeLineTotExclNoDiscForeign] " & _
                 " ,[fQtyChangeLineTaxAmountForeign],[fQtyChangeLineTaxAmountNoDiscForeign],[fQtyToProcessLineTotInclForeign] " & _
                 " ,[fQtyToProcessLineTotExclForeign],[fQtyToProcessLineTotInclNoDiscForeign],[fQtyToProcessLineTotExclNoDiscForeign] " & _
                 " ,[fQtyToProcessLineTaxAmountForeign],[fQtyToProcessLineTaxAmountNoDiscForeign],[fQtyLastProcessLineTotInclForeign] " & _
                 " ,[fQtyLastProcessLineTotExclForeign],[fQtyLastProcessLineTotInclNoDiscForeign],[fQtyLastProcessLineTotExclNoDiscForeign] " & _
                 " ,[fQtyLastProcessLineTaxAmountForeign],[fQtyLastProcessLineTaxAmountNoDiscForeign],[fQtyProcessedLineTotInclForeign] " & _
                 " ,[fQtyProcessedLineTotExclForeign],[fQtyProcessedLineTotInclNoDiscForeign],[fQtyProcessedLineTotExclNoDiscForeign] " & _
                 " ,[fQtyProcessedLineTaxAmountForeign],[fQtyProcessedLineTaxAmountNoDiscForeign],[iLineRepID],[iLineProjectID] " & _
                 " ,[iLedgerAccountID],[iModule],[bChargeCom],[bIsLotItem],[iLotID],[cLotNumber],[dLotExpiryDate],[iMFPID] " & _
                 " ,[iLineID],[iLinkedLineID],[fQtyLinkedUsed],[_btblInvoiceLines_iBranchID],[_btblInvoiceLines_iCreatedBranchID] " & _
                 " ,[_btblInvoiceLines_iModifiedBranchID],[_btblInvoiceLines_iCreatedAgentID],[_btblInvoiceLines_iModifiedAgentID] " & _
                 " ,[fUnitPriceInclOrig],[fUnitPriceExclOrig],[fUnitPriceInclForeignOrig],[fUnitPriceExclForeignOrig] " & _
                 " ,[iDeliveryMethodID],[fQtyDeliver],[iDeliveryStatus],[fQtyForDelivery],[bPromotionApplied],[fPromotionPriceExcl] " & _
                 " ,[fPromotionPriceIncl],[cPromotionCode],[iSOLinkedPOLineID]) " & _
            "  Values (" & DS.Tables(0).Rows(i)("iInvoiceID") & ",0,0,0, " & _
                      " " & DS.Tables(0).Rows(i)("cDescription") & ",1,1,1, " & _
                      " " & DS.Tables(0).Rows(i)("fQuantity") & ",0, " & _
                      " " & DS.Tables(0).Rows(i)("fQtyToProcess") & ",0,0,0,0,'', " & _
                      " " & DS.Tables(0).Rows(i)("fUnitPriceExcl") & ", " & _
                      " " & DS.Tables(0).Rows(i)("fUnitPriceIncl") & ",0,0,0,0,0,11,0,1,0,'', " & _
                      " " & DS.Tables(0).Rows(i)("iStockCodeID") & ",0, " & _
                      " " & DS.Tables(0).Rows(i)("iWarehouseID") & ",1,0, " & _
                      " " & DS.Tables(0).Rows(i)("fQuantityLineTotIncl") & ", " & _
                      " " & DS.Tables(0).Rows(i)("fQuantityLineTotExcl") & ",  " & _
                      " " & DS.Tables(0).Rows(i)("fQuantityLineTotInclNoDisc") & ",  " & _
                      " " & DS.Tables(0).Rows(i)("fQuantityLineTotExclNoDisc") & ",  " & _
                      " " & DS.Tables(0).Rows(i)("fQuantityLineTaxAmount") & ",  " & _
                      " " & DS.Tables(0).Rows(i)("fQuantityLineTaxAmountNoDisc") & ", 0,0,0,0,0,0,0,0,0,0, " & _
                      " " & DS.Tables(0).Rows(i)("fQtyToProcessLineTaxAmount") & ",0,0,0,0,0,0,0,0,0,0,0,0,0, " & _
                      " " & DS.Tables(0).Rows(i)("fUnitPriceExclForeign") & ",  " & _
                      " " & DS.Tables(0).Rows(i)("fUnitPriceInclForeign") & ",0,0, " & _
                      " " & DS.Tables(0).Rows(i)("fQuantityLineTotInclForeign") & ", " & _
                      " " & DS.Tables(0).Rows(i)("fQuantityLineTotExclForeign") & ", " & _
                      " " & DS.Tables(0).Rows(i)("fQuantityLineTotInclNoDiscForeign") & ", " & _
                      " " & DS.Tables(0).Rows(i)("fQuantityLineTotExclNoDiscForeign") & ", " & _
                      " " & DS.Tables(0).Rows(i)("fQuantityLineTaxAmountForeign") & ",  " & _
                      " " & DS.Tables(0).Rows(i)("fQuantityLineTaxAmountNoDiscForeign") & ", 0,0,0,0,0,0,0,0,0,0, " & _
                      " " & DS.Tables(0).Rows(i)("fQtyToProcessLineTaxAmountForeign") & ",0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0, " & _
                      " " & DS.Tables(0).Rows(i)("cLotNumber") & ",  " & _
                      " " & DS.Tables(0).Rows(i)("dLotExpiryDate") & ",  " & _
                      " " & DS.Tables(0).Rows(i)("iLineID") & ",0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,'',0,0 " & _
                      "  from " & sSQLSrvDataBase & "._btblInvoiceLines where iInvoiceID = " & iRTNNo & " "

                If .Execute_Sql_Trans(SQL) = 0 Then
                    .Rollback_Trans()
                    Exit Sub
                End If
                .Commit_Trans()
            Next
        End With

    End Sub



    Private Sub ProcessPO()
        Try
            Dim iInvoiceID As Integer = 0
            Dim sOrderNumber As String = ""
            Dim iCustomer As Integer = 0
            Dim iSupplier As Integer = 0
            Dim iIBTNo As Integer = 0
            Dim sError As String = ""
            Dim iToWH As Integer = 0

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
            DatabaseContext.CreateConnection("UMSERVER", cmbGRVLoc.Text.Trim, sSQLSrvUserName, sSQLSrvPassword, False)
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
            ElseIf clientWH = 18 Then 'Kiribathgoda
                Dim ordstatus As New OrderStatus("KIRIBATHGODA")
                PurchaseOrder.OrderStatus = ordstatus
            ElseIf cmbGRVLoc.Text = "dbMathara" And clientWH = 18 Then 'matara
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
                    InventoryItem = New InventoryItem(CStr(ugR.Cells("Description_2").Text))
                    PurchaseOrderDetail.InventoryItem = InventoryItem
                    'If ugR.Cells("IsLot").Value = True Then
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
                    'End If



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
                    PurchaseOrderDetail.ToProcess = CDbl(ugR.Cells("Quantity").Value)

                    'ware house
                    If ugR.Cells("IsWH").Value = True Then

                        WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))

                        PurchaseOrderDetail.Warehouse = WH
                    End If
                    '            Catch ex As Exception
                    'End Try


                    If CStr(ugR.Cells("Description_1").Value).Length > 99 Then
                        PurchaseOrderDetail.Description = CStr(ugR.Cells("Description_1").Value).Substring(0, 99)
                    Else
                        PurchaseOrderDetail.Description = CStr(ugR.Cells("Description_1").Value)
                    End If

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
                            'sSONo = "KEL2038"
                            'If InStr(DS1.Tables(0).Rows(0)(0).ToString(), "-") = 0 Then 'IF LOT NUMBER IS 001
                            '    LotN.Code = "K" & sSONo.Substring(3) & "-" & CStr(ugR.Cells("Description_1").Value)
                            'Else
                            If cmbGRVLoc.Text = "dbKelaniya_new" Then 'if isuing to kalaniya database
                                'LotN.Code = DS1.Tables(0).Rows(0)(0).ToString()
                                LotN.Code = sSONo.Substring(3) & "-" & DS1.Tables(0).Rows(0)(0).ToString().Substring(InStr(DS1.Tables(0).Rows(0)(0).ToString(), "-"))
                            Else
                                'LotN.Code = sSONo.Substring(3) & "-" & DS1.Tables(0).Rows(0)(0).ToString().Substring(0, InStr(DS1.Tables(0).Rows(0)(0).ToString(), "-") - 1)
                                LotN.Code = sSONo.Substring(3) & "-" & DS1.Tables(0).Rows(0)(0).ToString().Substring(InStr(DS1.Tables(0).Rows(0)(0).ToString(), "-"))
                            End If
                            'End If
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





            'If MsgBox("Do you want to print IBT now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
            '    Dim objRep As New ReportDocument
            '    Dim s As String = Application.StartupPath

            '    If iDocType_1 = 10 Then
            '        objRep.Load(Application.StartupPath & "\IBT.rpt")
            '    ElseIf iDocType_1 = 8 Then
            '        objRep.Load(Application.StartupPath & "\IBT_R.rpt")
            '    End If




            '    Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= '" & cmbGRVLoc.Text & "' ")
            '    SQL = " SELECT OrderNum, DocState  from InvNum where ExtOrderNum = '" & sSONo & "' "
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

            '    frmPrintPreview.vSelectionFormula = "{InvNum.AutoIndex}= " & iSONo & ""

            '    iRepoID = 2

            '    frmPrintPreview.ShowDialog()
            '    iRepoID = 0

            '    Exit Sub



            'End If






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
    Private Sub ProcessCRN()

        Dim iInvoiceID As Integer = 0
        Dim sOrderNumber As String = ""
        Dim iCustomer As Integer = 0
        Dim iRTNNo As Integer = 0
        Dim sError As String = ""
        Dim iToWH As Integer = 0


        ReDim lots(UG.Rows.Count)
        'to find the client database
        Dim CDB As String = ""
        If sSQLSrvDataBase = "dbNawinna_new" Then
            CDB = "UDA003"
        ElseIf sSQLSrvDataBase = "dbKurunegala_new" Then
            CDB = "UDA004"
        ElseIf sSQLSrvDataBase = "dbKiribathgoda_new" Then
            CDB = "UDA005"
        ElseIf sSQLSrvDataBase = "dbUdawatta_new" Then
            CDB = "UDA006"
        ElseIf sSQLSrvDataBase = "dbMathara" Then
            CDB = "UDA008"
        End If

        getDefaultwh(sError, iToWH)

        If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
        If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

        DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
        DatabaseContext.SetLicense("DE09110022", "1428511")
        DatabaseContext.CreateConnection(sSQLSrvName, "dbKelaniya_new", sSQLSrvUserName, sSQLSrvPassword, False)

        DatabaseContext.BeginTran()
        Dim CreditNote As New CreditNote
        Dim CRNDetail As OrderDetail
        Dim InventoryItem As InventoryItem
        Dim WH As Warehouse
        Dim LotN As Lot

        Dim Customer As New Customer(CDB)
        iCustomer = Customer.ID
        CreditNote.OrderNo = sOrderNumber
        CreditNote.ExternalOrderNo = CreditNote.OrderNo
        CreditNote.Account = Customer
        CreditNote.InvoiceTo = Customer.PostalAddress
        CreditNote.DeliverTo = Customer.PhysicalAddress
        CreditNote.ExternalOrderNo = txtOrdNo.Text.ToString
        CreditNote.InvoiceDate = Format(Now.Date, "dd/MM/yyyy")
        CreditNote.DeliveryDate = Format(Now.Date, "dd/MM/yyyy")
        CreditNote.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")
        CreditNote.OrderNo = CStr(txtOrdNo.Text)
        'CreditNote.SupplierInvoiceNo = "" ' CStr(txtInNo.Text)
        If bIsInclusive = False Then
            CreditNote.TaxMode = TaxMode.Exclusive
        ElseIf bIsInclusive = True Then
            CreditNote.TaxMode = TaxMode.Inclusive
        End If


        For Each ugR In UG.Rows
            If CDbl(ugR.Cells("Quantity").Value) > 0 Then
                CRNDetail = New OrderDetail
                InventoryItem = New InventoryItem(CStr(ugR.Cells("Code").Text))
                CRNDetail.InventoryItem = InventoryItem




                CRNDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                If bIsInclusive = False Then
                    CRNDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                    CRNDetail.TaxMode = TaxMode.Exclusive
                    CRNDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                ElseIf bIsInclusive = True Then
                    If CDbl(ugR.Cells("Price_Incl").Value) = 0 Then
                        CRNDetail.UnitSellingPrice = Get_Value(" SELECT fInclPrice FROM _etblPriceListPrices where iPriceListNameID = 1 and iStockID = " & CStr(ugR.Cells("StockID").Text) & " ")
                    Else
                        CRNDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                    End If

                    CRNDetail.TaxMode = TaxMode.Inclusive
                    CRNDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                End If
                CRNDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)

                If ugR.Cells("IsWH").Value = True Then
                    WH = New Warehouse(iToWH)
                    CRNDetail.Warehouse = WH
                End If
                CRNDetail.Description = CStr(ugR.Cells("Description_1").Value)
                'PurchaseOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                'CRNDetail.LotID = CStr(ugR.Cells("Lot").Value)

                'to get the kelaniya lot numbers wchiich is different from branch numberws
                SQL = " SELECT   _btblInvoiceLines.cLotNumber FROM _btblInvoiceLines INNER JOIN " & _
                " InvNum ON _btblInvoiceLines.iInvoiceID = InvNum.AutoIndex " & _
                " where   InvNum.InvNumber = '" & txtExtOrder.Text.ToString() & "' and  _btblInvoiceLines.cDescription = '" & ugR.Cells("description_1").Text & "' "
                CMD = New SqlCommand(SQL, Con2)
                CMD.CommandType = CommandType.Text
                DA = New SqlDataAdapter(CMD)
                DS1 = New DataSet
                Con2.Open()
                DA.Fill(DS1)
                Con2.Close()

                LotN = New Lot()
                LotN.Code = DS1.Tables(0).Rows(0)(0)
                'LotN.ExpiryDate = ugR.Cells("Lot_Exp").Value
                CRNDetail.Lot = LotN

                If ec.Text <> Nothing Then
                    sSONo = ec.Text
                End If


                CreditNote.Detail.Add(CRNDetail)
            End If
        Next

        CreditNote.Save()

        DatabaseContext.CommitTran()


        CRNNo = CreditNote.Reference.ToString()
        'MsgBox(CreditNote.Reference, MsgBoxStyle.Information, "Pastel Evolution")
        MsgBox("Posted to Kelaniya", MsgBoxStyle.Information, "Pastel Evolution")





    End Sub
    Sub getDefaultwh(ByRef sError As String, ByRef iToWH1 As Integer)

        Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database = dbKelaniya_new")

        Dim objSQL As New clsSqlConn
        Dim dr1 As DataRow
        For Each dr In DS.Tables(0).Rows
            Try

                'DatabaseContext.CreateCommonDBConnection(sSQLSrvName, sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
                'DatabaseContext.SetLicense("DE09110022", "1428511")
                'DatabaseContext.CreateConnection(sSQLSrvName, "dbUdawatta_new", sSQLSrvUserName, sSQLSrvPassword, False)

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
    Private Sub ProcessCRNDB()

        Dim iInvoiceID As Integer = 0
        Dim sOrderNumber As String = ""
        Dim iCustomer As Integer = 0
        Dim iRTNNo As Integer = 0
        Dim sError As String = ""
        Dim iToWH As Integer = 0


        ReDim lots(UG.Rows.Count)
        'to find the client database
        Dim CDB As String = ""
        If sSQLSrvDataBase = "dbNawinna_new" Then
            CDB = "UDA003"
        ElseIf sSQLSrvDataBase = "dbKurunegala_new" Then
            CDB = "UDA004"
        ElseIf sSQLSrvDataBase = "dbKiribathgoda_new" Then
            CDB = "UDA005"
        ElseIf sSQLSrvDataBase = "dbUdawatta_new" Then
            CDB = "UDA006"
        ElseIf sSQLSrvDataBase = "dbMathara" Then
            CDB = "UDA008"
        End If

        getDefaultwh(sError, iToWH)

        If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
        If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

        DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
        DatabaseContext.SetLicense("DE09110022", "1428511")
        DatabaseContext.CreateConnection("UMdbSERVER\SQLExpress", "dbKelaniya_new", sSQLSrvUserName, sSQLSrvPassword, False)

        DatabaseContext.BeginTran()
        Dim CreditNote As New CreditNote
        Dim CRNDetail As OrderDetail
        Dim InventoryItem As InventoryItem
        Dim WH As Warehouse
        Dim LotN As Lot

        Dim Customer As New Customer(CDB)
        iCustomer = Customer.ID
        CreditNote.OrderNo = sOrderNumber
        CreditNote.ExternalOrderNo = CreditNote.OrderNo
        CreditNote.Account = Customer
        CreditNote.InvoiceTo = Customer.PostalAddress
        CreditNote.DeliverTo = Customer.PhysicalAddress
        CreditNote.ExternalOrderNo = txtOrdNo.Text.ToString
        CreditNote.InvoiceDate = Format(Now.Date, "dd/MM/yyyy")
        CreditNote.DeliveryDate = Format(Now.Date, "dd/MM/yyyy")
        CreditNote.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")
        CreditNote.OrderNo = CStr(txtOrdNo.Text)
        'CreditNote.SupplierInvoiceNo = "" ' CStr(txtInNo.Text)
        If bIsInclusive = False Then
            CreditNote.TaxMode = TaxMode.Exclusive
        ElseIf bIsInclusive = True Then
            CreditNote.TaxMode = TaxMode.Inclusive
        End If


        For Each ugR In UG.Rows
            If CDbl(ugR.Cells("Quantity").Value) > 0 Then
                CRNDetail = New OrderDetail
                InventoryItem = New InventoryItem(CStr(ugR.Cells("Code").Text))
                CRNDetail.InventoryItem = InventoryItem




                CRNDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                If bIsInclusive = False Then
                    CRNDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                    CRNDetail.TaxMode = TaxMode.Exclusive
                    CRNDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                ElseIf bIsInclusive = True Then
                    If CDbl(ugR.Cells("Price_Incl").Value) = 0 Then
                        CRNDetail.UnitSellingPrice = Get_Value(" SELECT fInclPrice FROM _etblPriceListPrices where iPriceListNameID = 1 and iStockID = " & CStr(ugR.Cells("StockID").Text) & " ")
                    Else
                        CRNDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                    End If

                    CRNDetail.TaxMode = TaxMode.Inclusive
                    CRNDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                End If
                CRNDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)

                If ugR.Cells("IsWH").Value = True Then
                    WH = New Warehouse(iToWH)
                    CRNDetail.Warehouse = WH
                End If
                CRNDetail.Description = CStr(ugR.Cells("Description_1").Value)
                'PurchaseOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                'CRNDetail.LotID = CStr(ugR.Cells("Lot").Value)

                'to get the kelaniya lot numbers wchiich is different from branch numberws
                SQL = " SELECT   _btblInvoiceLines.cLotNumber FROM _btblInvoiceLines INNER JOIN " & _
                " InvNum ON _btblInvoiceLines.iInvoiceID = InvNum.AutoIndex " & _
                " where   InvNum.InvNumber = '" & txtExtOrder.Text.ToString() & "' and  _btblInvoiceLines.cDescription = '" & ugR.Cells("description_1").Text & "' "
                CMD = New SqlCommand(SQL, Con2)
                CMD.CommandType = CommandType.Text
                DA = New SqlDataAdapter(CMD)
                DS1 = New DataSet
                Con2.Open()
                DA.Fill(DS1)
                Con2.Close()

                LotN = New Lot()
                LotN.Code = DS1.Tables(0).Rows(0)(0)
                'LotN.ExpiryDate = ugR.Cells("Lot_Exp").Value
                CRNDetail.Lot = LotN

                If ec.Text <> Nothing Then
                    sSONo = ec.Text
                End If


                CreditNote.Detail.Add(CRNDetail)
            End If
        Next

        CreditNote.Save()

        DatabaseContext.CommitTran()


        CRNNo = CreditNote.Reference.ToString()
        'MsgBox(CreditNote.Reference, MsgBoxStyle.Information, "Pastel Evolution")
        MsgBox("Posted to Kelaniya", MsgBoxStyle.Information, "Pastel Evolution")





    End Sub
    Private Sub processRTN(ByVal IBTNo As Long, Optional ByRef bProcess As Boolean = False, Optional ByRef imyIBTno As Integer = 0, Optional ByRef sError As String = "")
        UG.PerformAction(UltraGridAction.CommitRow)

        If cmbCustomer.Text = "" Then
            MsgBox("Customer Name can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If UG.Rows.Count = 0 Then
            MsgBox("Enter Item Details", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If


        Dim iToWH As Integer = 0

        getDefaultwh(sError, iToWH)

        If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
        If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

        Dim iInvoiceID As Integer = 0
        Dim sOrderNumber As String = ""
        Dim iCustomer As Integer = 0
        Dim iSupplier As Integer = 0

        If CInt(IBTNo) = 0 Then
            iOldRTNNo = 0
        Else
            iOldRTNNo = IBTNo
        End If

        DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
        DatabaseContext.SetLicense("DE09110022", "1428511")
        DatabaseContext.CreateConnection(sSQLSrvName, sSQLSrvDataBase, sSQLSrvUserName, sSQLSrvPassword, False)

        Try
            DatabaseContext.BeginTran()

            Dim ReturnSupplier As ReturnToSupplier

            ReturnSupplier = New ReturnToSupplier()


            Dim RTNDetail As OrderDetail

            Dim InventoryItem As InventoryItem
            Dim WH As Warehouse

            Dim Supplier As Supplier
            If cmbCustomer.Text.Length > 4 Then
                Supplier = New Supplier(CInt(cmbCustomer.Value))
            Else
                Supplier = New Supplier(CInt(cmbCustomer.Text))
            End If



            iSupplier = Supplier.ID

            'Dim Representative As New SalesRepresentative(CInt(cmbSaleRep.Value))

            'SalesOrder.OrderNo = CStr(txtOrdNo.Text.Trim)
            'SalesOrder.DeliveryNote = CStr(txtDelNote.Text.Trim)
            ReturnSupplier.Account = Supplier
            ReturnSupplier.InvoiceTo = Supplier.PostalAddress
            ReturnSupplier.DeliverTo = Supplier.PhysicalAddress
            ReturnSupplier.ExternalOrderNo = txtExtOrder.Text.ToString
            'SalesOrder.ExternalOrderNo = PONo
            ReturnSupplier.InvoiceDate = Format(Now.Date, "dd/MM/yyyy")
            ReturnSupplier.DeliveryDate = Format(Now.Date, "dd/MM/yyyy")
            ReturnSupplier.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")


            If bIsInclusive = True Then
                ReturnSupplier.TaxMode = TaxMode.Inclusive
            ElseIf bIsInclusive = False Then
                ReturnSupplier.TaxMode = TaxMode.Exclusive
            End If

            'ReturnSupplier.UserFields("uiIDSOrdSuppID") = CInt(4)
            'ReturnSupplier.UserFields("ucIDSOrddbName") = cmbGRVLoc.Text.Trim

            Dim bLoop As Boolean = False

            Try

lbl1:           For Each RTNDetail In ReturnSupplier.Detail
                    bLoop = False

                    ReturnSupplier.Detail.Remove(RTNDetail)
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


            For Each ugR In UG.Rows



                If CDbl(ugR.Cells("Quantity").Value) > 0 Then 'process only items that have quenty
                    RTNDetail = New OrderDetail

                    InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))
                    RTNDetail.InventoryItem = InventoryItem
                    RTNDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                    RTNDetail.ToProcess = CDbl(ugR.Cells("Quantity").Value)

                    If bIsInclusive = False Then
                        RTNDetail.TaxMode = TaxMode.Exclusive
                        RTNDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                        RTNDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                    ElseIf bIsInclusive = True Then
                        RTNDetail.TaxMode = TaxMode.Inclusive
                        RTNDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                        RTNDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                    End If


                    'Dim LotN As New Lot()
                    'LotN.Code = ugR.Cells("Lot").Text
                    ''LotN.ExpiryDate = ugR.Cells("Lot_Exp").Value
                    'RTNDetail.Lot = LotN
                    RTNDetail.LotID = CStr(ugR.Cells("Lot").Value)



                    If ugR.Cells("IsWH").Value = True Then
                        WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                        RTNDetail.Warehouse = WH
                    End If

                    RTNDetail.Description = CStr(ugR.Cells("Description_1").Value)
                    RTNDetail.DiscountPercent = CDbl(ugR.Cells("Discount").Value)
                    'RTNDetail.LotID = CStr(ugR.Cells("Lot").Value)
                    ReturnSupplier.Detail.Add(RTNDetail)


                End If
            Next

            ReturnSupplier.Save()

            If bProcess Then
                ReturnSupplier.Process()
                txtOrdNo.Text = ReturnSupplier.Reference
            End If

abc:        DatabaseContext.CommitTran()

            iRTNNo = ReturnSupplier.ID
            sSONo = ReturnSupplier.Reference

            imyIBTno = ReturnSupplier.ID

            'Call MemorizeStock()

            If bProcess Then
                MsgBox("Invoice No = " & ReturnSupplier.Reference, MsgBoxStyle.Information, "Pastel Evolution")
            Else
                MsgBox("Sales Order No = " & ReturnSupplier.OrderNo, MsgBoxStyle.Information, "Pastel Evolution")
            End If

            If bProcess Then
                Dim iSalesOrder As Integer = iRTNNo
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
End Class