<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdatePriceList
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpdatePriceList))
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ID")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("DatabaseName")
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Band 1")
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand2 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 1", 0)
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ID")
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("IDPriceListPrices")
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("cName")
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("bDefault")
        Dim UltraGridColumn8 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("fExclPrice")
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn9 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("fInclPrice")
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn10 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("cSimpleCode")
        Dim UltraGridColumn11 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("dExcPrice1")
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn12 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("dIncPrice1")
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn13 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("dExcPrice2")
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn14 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("dIncPrice2")
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraDataBand1 As Infragistics.Win.UltraWinDataSource.UltraDataBand = New Infragistics.Win.UltraWinDataSource.UltraDataBand("Band 1")
        Dim UltraDataColumn1 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("ID")
        Dim UltraDataColumn2 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("IDPriceListPrices")
        Dim UltraDataColumn3 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("cName")
        Dim UltraDataColumn4 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("bDefault")
        Dim UltraDataColumn5 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("fExclPrice")
        Dim UltraDataColumn6 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("fInclPrice")
        Dim UltraDataColumn7 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("cSimpleCode")
        Dim UltraDataColumn8 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("dExcPrice1")
        Dim UltraDataColumn9 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("dIncPrice1")
        Dim UltraDataColumn10 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("dExcPrice2")
        Dim UltraDataColumn11 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("dIncPrice2")
        Dim UltraDataColumn12 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("ID")
        Dim UltraDataColumn13 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("DatabaseName")
        Dim UltraDataBand2 As Infragistics.Win.UltraWinDataSource.UltraDataBand = New Infragistics.Win.UltraWinDataSource.UltraDataBand("Band 1")
        Dim UltraDataColumn14 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("ID")
        Dim UltraDataColumn15 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("IDPriceListPrices")
        Dim UltraDataColumn16 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("cName")
        Dim UltraDataColumn17 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("bDefault")
        Dim UltraDataColumn18 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("fExclPrice")
        Dim UltraDataColumn19 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("fInclPrice")
        Dim UltraDataColumn20 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("cSimpleCode")
        Dim UltraDataColumn21 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("dExcPrice1")
        Dim UltraDataColumn22 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("dIncPrice1")
        Dim UltraDataColumn23 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("dExcPrice2")
        Dim UltraDataColumn24 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("dIncPrice2")
        Dim UltraDataColumn25 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("ID")
        Dim UltraDataColumn26 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("DatabaseName")
        Dim UltraDataBand3 As Infragistics.Win.UltraWinDataSource.UltraDataBand = New Infragistics.Win.UltraWinDataSource.UltraDataBand("Band 1")
        Dim UltraDataColumn27 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("ID")
        Dim UltraDataColumn28 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("IDPriceListPrices")
        Dim UltraDataColumn29 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("cName")
        Dim UltraDataColumn30 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("bDefault")
        Dim UltraDataColumn31 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("fExclPrice")
        Dim UltraDataColumn32 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("fInclPrice")
        Dim UltraDataColumn33 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("cSimpleCode")
        Dim UltraDataColumn34 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("ID")
        Dim UltraDataColumn35 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("DatabaseName")
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraDataBand4 As Infragistics.Win.UltraWinDataSource.UltraDataBand = New Infragistics.Win.UltraWinDataSource.UltraDataBand("Band 1")
        Dim UltraDataColumn36 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("ID")
        Dim UltraDataColumn37 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("IDPriceListPrices")
        Dim UltraDataColumn38 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("cName")
        Dim UltraDataColumn39 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("bDefault")
        Dim UltraDataColumn40 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("fExclPrice")
        Dim UltraDataColumn41 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("fInclPrice")
        Dim UltraDataColumn42 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("cSimpleCode")
        Dim UltraDataColumn43 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("dExcPrice1")
        Dim UltraDataColumn44 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("dIncPrice1")
        Dim UltraDataColumn45 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("dExcPrice2")
        Dim UltraDataColumn46 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("dIncPrice2")
        Dim UltraDataColumn47 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("ID")
        Dim UltraDataColumn48 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("DatabaseName")
        Dim UltraDataBand5 As Infragistics.Win.UltraWinDataSource.UltraDataBand = New Infragistics.Win.UltraWinDataSource.UltraDataBand("Band 1")
        Dim UltraDataColumn49 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("ID")
        Dim UltraDataColumn50 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("IDPriceListPrices")
        Dim UltraDataColumn51 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("cName")
        Dim UltraDataColumn52 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("bDefault")
        Dim UltraDataColumn53 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("fExclPrice")
        Dim UltraDataColumn54 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("fInclPrice")
        Dim UltraDataColumn55 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("cSimpleCode")
        Dim UltraDataColumn56 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("dExcPrice1")
        Dim UltraDataColumn57 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("dIncPrice1")
        Dim UltraDataColumn58 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("ID")
        Dim UltraDataColumn59 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("DatabaseName")
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand3 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn15 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Line")
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn16 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Code", -1, "DDStock")
        Dim UltraGridColumn17 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Description_1", -1, "DDDescription")
        Dim UltraGridColumn18 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Description_2")
        Dim UltraGridColumn19 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Warehouse")
        Dim UltraGridColumn20 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Quantity")
        Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn21 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("iUnit", -1, "DDUnit")
        Dim UltraGridColumn22 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("iUnitCate")
        Dim Appearance25 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn23 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ProcessedQty")
        Dim Appearance26 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn24 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ConfirmQty")
        Dim Appearance27 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn25 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Price_Excl")
        Dim Appearance28 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn26 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Price_Incl")
        Dim Appearance29 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn27 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("TaxType", -1, "DDTaxType")
        Dim Appearance30 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn28 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("TaxRate")
        Dim Appearance31 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn29 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Discount")
        Dim Appearance32 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn30 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("OrderTotal_Excl")
        Dim Appearance33 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn31 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("OrderTotal_Incl")
        Dim Appearance34 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn32 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("OrderTax")
        Dim Appearance35 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn33 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("LineTax")
        Dim Appearance36 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn34 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("LineTotal_Excl")
        Dim Appearance37 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn35 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("LineTotal_Incl")
        Dim Appearance38 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn36 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Line_Dis_Excl")
        Dim Appearance39 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn37 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Line_Dis_Incl")
        Dim Appearance40 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn38 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Order_Dis_Excl")
        Dim UltraGridColumn39 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Order_Dis_Incl")
        Dim UltraGridColumn40 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Line_Tax_NoDis_Excl")
        Dim UltraGridColumn41 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Line_Tax_NoDis_Incl")
        Dim UltraGridColumn42 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Ord_Tax_NoDis_Excl")
        Dim UltraGridColumn43 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Ord_Tax_NoDis_Incl")
        Dim UltraGridColumn44 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("StockID")
        Dim UltraGridColumn45 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("LineNote")
        Dim UltraGridColumn46 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("IsLot")
        Dim UltraGridColumn47 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("IsWH")
        Dim UltraGridColumn48 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("LineID")
        Dim UltraGridColumn49 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("SerialLot")
        Dim UltraGridColumn50 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("fUnitCostMargine")
        Dim Appearance41 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn51 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("fCostMargine")
        Dim Appearance42 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn52 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("CostPrice")
        Dim Appearance43 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn53 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("AvailableQty")
        Dim Appearance44 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn54 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("BranchQty")
        Dim Appearance45 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn55 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Lot", -1, "DDLot")
        Dim UltraGridColumn56 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("fExclPrice")
        Dim UltraGridColumn57 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Max_Lvl")
        Dim UltraGridColumn58 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("cSimpleCode")
        Dim UltraGridColumn59 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Band 1")
        Dim UltraGridColumn60 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Serial", 0)
        Dim UltraGridBand4 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 1", 0)
        Dim UltraGridColumn61 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("LN")
        Dim UltraGridColumn62 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("SerialNumber")
        Dim UltraGridColumn63 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("SNStockLink")
        Dim UltraGridColumn64 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("PrimaryLineID")
        Dim Appearance46 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance47 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.tsbConnect = New System.Windows.Forms.ToolStripButton()
        Me.tsbUpdate = New System.Windows.Forms.ToolStripButton()
        Me.ultraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmbInventoryItem = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UG = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraDataSource5 = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.UltraDataSource3 = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.UltraDataSource1 = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.txtSimpleCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txtCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.lbl6 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraDataSource2 = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.UltraDataSource4 = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.cmbGroup = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtTo = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.btnAutoUpdate = New System.Windows.Forms.ToolStripButton()
        Me.toolStrip1.SuspendLayout()
        CType(Me.cmbInventoryItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraDataSource5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraDataSource3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraDataSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSimpleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraDataSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraDataSource4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'toolStrip1
        '
        Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbExit, Me.tsbConnect, Me.tsbUpdate, Me.btnAutoUpdate})
        Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.toolStrip1.Name = "toolStrip1"
        Me.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.toolStrip1.Size = New System.Drawing.Size(961, 25)
        Me.toolStrip1.TabIndex = 0
        Me.toolStrip1.Text = "ToolStrip1"
        '
        'tsbExit
        '
        Me.tsbExit.Image = CType(resources.GetObject("tsbExit.Image"), System.Drawing.Image)
        Me.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbExit.Name = "tsbExit"
        Me.tsbExit.Size = New System.Drawing.Size(45, 22)
        Me.tsbExit.Text = "Exit"
        '
        'tsbConnect
        '
        Me.tsbConnect.Image = CType(resources.GetObject("tsbConnect.Image"), System.Drawing.Image)
        Me.tsbConnect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbConnect.Name = "tsbConnect"
        Me.tsbConnect.Size = New System.Drawing.Size(148, 22)
        Me.tsbConnect.Text = "Connect to All Databases"
        '
        'tsbUpdate
        '
        Me.tsbUpdate.Image = CType(resources.GetObject("tsbUpdate.Image"), System.Drawing.Image)
        Me.tsbUpdate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbUpdate.Name = "tsbUpdate"
        Me.tsbUpdate.Size = New System.Drawing.Size(130, 22)
        Me.tsbUpdate.Text = "Update All Databases"
        '
        'ultraLabel1
        '
        Appearance1.TextVAlignAsString = "Middle"
        Me.ultraLabel1.Appearance = Appearance1
        Me.ultraLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ultraLabel1.Location = New System.Drawing.Point(12, 47)
        Me.ultraLabel1.Name = "ultraLabel1"
        Me.ultraLabel1.Size = New System.Drawing.Size(100, 22)
        Me.ultraLabel1.TabIndex = 1
        Me.ultraLabel1.Text = " Inventory Item "
        '
        'cmbInventoryItem
        '
        Me.cmbInventoryItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Appearance2.BorderColor = System.Drawing.Color.Silver
        Me.cmbInventoryItem.DisplayLayout.Appearance = Appearance2
        Me.cmbInventoryItem.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn
        Appearance3.BackColor = System.Drawing.Color.PapayaWhip
        Me.cmbInventoryItem.DisplayLayout.Override.RowAlternateAppearance = Appearance3
        Me.cmbInventoryItem.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.[Default]
        Me.cmbInventoryItem.Location = New System.Drawing.Point(118, 47)
        Me.cmbInventoryItem.Name = "cmbInventoryItem"
        Me.cmbInventoryItem.Size = New System.Drawing.Size(436, 22)
        Me.cmbInventoryItem.TabIndex = 2
        '
        'UG
        '
        Me.UG.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UG.DataSource = Me.UltraDataSource5
        Appearance4.BorderColor = System.Drawing.Color.Silver
        Me.UG.DisplayLayout.Appearance = Appearance4
        Me.UG.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Width = 180
        Appearance5.FontData.BoldAsString = "True"
        UltraGridColumn2.CellAppearance = Appearance5
        UltraGridColumn2.Header.Caption = "Database Name"
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn2.Width = 741
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3})
        Appearance6.BackColor = System.Drawing.Color.NavajoWhite
        UltraGridBand1.Override.RowAppearance = Appearance6
        UltraGridColumn4.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled
        Appearance7.BackColorDisabled = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Appearance7.FontData.BoldAsString = "True"
        Appearance7.ForeColorDisabled = System.Drawing.Color.Gainsboro
        Appearance7.TextHAlignAsString = "Center"
        UltraGridColumn4.CellAppearance = Appearance7
        UltraGridColumn4.Header.VisiblePosition = 0
        UltraGridColumn4.Width = 85
        UltraGridColumn5.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        UltraGridColumn5.Header.Caption = "Price List ID"
        UltraGridColumn5.Header.VisiblePosition = 1
        UltraGridColumn5.Hidden = True
        UltraGridColumn5.Width = 86
        UltraGridColumn6.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        UltraGridColumn6.Header.Caption = "Price List Name"
        UltraGridColumn6.Header.VisiblePosition = 2
        UltraGridColumn6.Width = 119
        UltraGridColumn7.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        UltraGridColumn7.Header.Caption = "Default"
        UltraGridColumn7.Header.VisiblePosition = 3
        UltraGridColumn7.Width = 71
        Appearance8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Appearance8.TextHAlignAsString = "Right"
        UltraGridColumn8.CellAppearance = Appearance8
        UltraGridColumn8.Format = "0.00"
        UltraGridColumn8.Header.Caption = "Price (Excl)"
        UltraGridColumn8.Header.VisiblePosition = 4
        UltraGridColumn8.Width = 98
        Appearance9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Appearance9.TextHAlignAsString = "Right"
        UltraGridColumn9.CellAppearance = Appearance9
        UltraGridColumn9.Format = "0.00"
        UltraGridColumn9.Header.Caption = "Price (Incl)"
        UltraGridColumn9.Header.VisiblePosition = 5
        UltraGridColumn9.Width = 85
        UltraGridColumn10.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        UltraGridColumn10.Header.Caption = "Simple Code"
        UltraGridColumn10.Header.VisiblePosition = 6
        UltraGridColumn10.Width = 106
        Appearance10.TextHAlignAsString = "Right"
        UltraGridColumn11.CellAppearance = Appearance10
        UltraGridColumn11.Header.Caption = "ExcPrice_1"
        UltraGridColumn11.Header.VisiblePosition = 7
        UltraGridColumn11.Width = 75
        Appearance11.TextHAlignAsString = "Right"
        UltraGridColumn12.CellAppearance = Appearance11
        UltraGridColumn12.Header.Caption = "IncPrice_1"
        UltraGridColumn12.Header.VisiblePosition = 8
        UltraGridColumn12.Width = 73
        Appearance12.TextHAlignAsString = "Right"
        UltraGridColumn13.CellAppearance = Appearance12
        UltraGridColumn13.Header.Caption = "ExcPrice_2"
        UltraGridColumn13.Header.VisiblePosition = 9
        UltraGridColumn13.Width = 95
        Appearance13.TextHAlignAsString = "Right"
        UltraGridColumn14.CellAppearance = Appearance13
        UltraGridColumn14.Header.Caption = "IncPrice_2"
        UltraGridColumn14.Header.VisiblePosition = 10
        UltraGridColumn14.Width = 95
        UltraGridBand2.Columns.AddRange(New Object() {UltraGridColumn4, UltraGridColumn5, UltraGridColumn6, UltraGridColumn7, UltraGridColumn8, UltraGridColumn9, UltraGridColumn10, UltraGridColumn11, UltraGridColumn12, UltraGridColumn13, UltraGridColumn14})
        Appearance14.BackColor = System.Drawing.Color.MistyRose
        UltraGridBand2.Override.RowAppearance = Appearance14
        Me.UG.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.UG.DisplayLayout.BandsSerializer.Add(UltraGridBand2)
        Me.UG.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UG.Location = New System.Drawing.Point(0, 123)
        Me.UG.Name = "UG"
        Me.UG.Size = New System.Drawing.Size(961, 369)
        Me.UG.TabIndex = 3
        '
        'UltraDataSource5
        '
        UltraDataColumn2.DataType = GetType(Long)
        UltraDataColumn4.DataType = GetType(Boolean)
        UltraDataColumn5.DataType = GetType(Double)
        UltraDataColumn6.DataType = GetType(Double)
        UltraDataColumn8.DataType = GetType(Double)
        UltraDataColumn9.DataType = GetType(Double)
        UltraDataColumn10.DataType = GetType(Double)
        UltraDataColumn11.DataType = GetType(Double)
        UltraDataBand1.Columns.AddRange(New Object() {UltraDataColumn1, UltraDataColumn2, UltraDataColumn3, UltraDataColumn4, UltraDataColumn5, UltraDataColumn6, UltraDataColumn7, UltraDataColumn8, UltraDataColumn9, UltraDataColumn10, UltraDataColumn11})
        Me.UltraDataSource5.Band.ChildBands.AddRange(New Object() {UltraDataBand1})
        UltraDataColumn12.DataType = GetType(Long)
        Me.UltraDataSource5.Band.Columns.AddRange(New Object() {UltraDataColumn12, UltraDataColumn13})
        Me.UltraDataSource5.UseBindingSource = True
        '
        'UltraDataSource3
        '
        UltraDataColumn15.DataType = GetType(Long)
        UltraDataColumn17.DataType = GetType(Boolean)
        UltraDataColumn18.DataType = GetType(Double)
        UltraDataColumn19.DataType = GetType(Double)
        UltraDataColumn21.DataType = GetType(Double)
        UltraDataColumn22.DataType = GetType(Double)
        UltraDataColumn23.DataType = GetType(Double)
        UltraDataColumn23.ReadOnly = Infragistics.Win.DefaultableBoolean.[True]
        UltraDataColumn24.DataType = GetType(Double)
        UltraDataColumn24.ReadOnly = Infragistics.Win.DefaultableBoolean.[True]
        UltraDataBand2.Columns.AddRange(New Object() {UltraDataColumn14, UltraDataColumn15, UltraDataColumn16, UltraDataColumn17, UltraDataColumn18, UltraDataColumn19, UltraDataColumn20, UltraDataColumn21, UltraDataColumn22, UltraDataColumn23, UltraDataColumn24})
        Me.UltraDataSource3.Band.ChildBands.AddRange(New Object() {UltraDataBand2})
        UltraDataColumn25.DataType = GetType(Long)
        Me.UltraDataSource3.Band.Columns.AddRange(New Object() {UltraDataColumn25, UltraDataColumn26})
        Me.UltraDataSource3.UseBindingSource = True
        '
        'UltraDataSource1
        '
        UltraDataColumn28.DataType = GetType(Long)
        UltraDataColumn29.DefaultValue = ""
        UltraDataColumn30.DataType = GetType(Boolean)
        UltraDataColumn31.DataType = GetType(Double)
        UltraDataColumn31.DefaultValue = 0.0R
        UltraDataColumn32.DataType = GetType(Double)
        UltraDataColumn32.DefaultValue = 0.0R
        UltraDataColumn33.DefaultValue = "0"
        UltraDataBand3.Columns.AddRange(New Object() {UltraDataColumn27, UltraDataColumn28, UltraDataColumn29, UltraDataColumn30, UltraDataColumn31, UltraDataColumn32, UltraDataColumn33})
        Me.UltraDataSource1.Band.ChildBands.AddRange(New Object() {UltraDataBand3})
        UltraDataColumn34.DataType = GetType(Long)
        Me.UltraDataSource1.Band.Columns.AddRange(New Object() {UltraDataColumn34, UltraDataColumn35})
        Me.UltraDataSource1.UseBindingSource = True
        '
        'UltraLabel2
        '
        Appearance15.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance15
        Me.UltraLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel2.Location = New System.Drawing.Point(12, 70)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(100, 21)
        Me.UltraLabel2.TabIndex = 4
        Me.UltraLabel2.Text = " Simple Code"
        '
        'UltraLabel3
        '
        Appearance16.TextVAlignAsString = "Middle"
        Me.UltraLabel3.Appearance = Appearance16
        Me.UltraLabel3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel3.Location = New System.Drawing.Point(12, 92)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(100, 21)
        Me.UltraLabel3.TabIndex = 5
        Me.UltraLabel3.Text = " Code"
        '
        'txtSimpleCode
        '
        Appearance17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSimpleCode.Appearance = Appearance17
        Me.txtSimpleCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSimpleCode.Location = New System.Drawing.Point(118, 70)
        Me.txtSimpleCode.Name = "txtSimpleCode"
        Me.txtSimpleCode.ReadOnly = True
        Me.txtSimpleCode.Size = New System.Drawing.Size(436, 21)
        Me.txtSimpleCode.TabIndex = 6
        '
        'txtCode
        '
        Appearance18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCode.Appearance = Appearance18
        Me.txtCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.Location = New System.Drawing.Point(118, 92)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.ReadOnly = True
        Me.txtCode.Size = New System.Drawing.Size(436, 21)
        Me.txtCode.TabIndex = 7
        '
        'lbl6
        '
        Appearance19.BackColor = System.Drawing.Color.Maroon
        Appearance19.BackColor2 = System.Drawing.Color.DarkRed
        Appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50
        Appearance19.FontData.BoldAsString = "True"
        Appearance19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Appearance19.TextVAlignAsString = "Middle"
        Me.lbl6.Appearance = Appearance19
        Me.lbl6.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(177, Byte))
        Me.lbl6.Location = New System.Drawing.Point(0, 25)
        Me.lbl6.Name = "lbl6"
        Me.lbl6.Size = New System.Drawing.Size(961, 18)
        Me.lbl6.TabIndex = 8
        Me.lbl6.Text = "Price List Update"
        '
        'UltraDataSource2
        '
        UltraDataColumn37.DataType = GetType(Long)
        UltraDataColumn39.DataType = GetType(Boolean)
        UltraDataColumn40.DataType = GetType(Double)
        UltraDataColumn41.DataType = GetType(Double)
        UltraDataColumn43.DataType = GetType(Double)
        UltraDataColumn44.DataType = GetType(Double)
        UltraDataColumn45.DataType = GetType(Double)
        UltraDataColumn46.DataType = GetType(Double)
        UltraDataBand4.Columns.AddRange(New Object() {UltraDataColumn36, UltraDataColumn37, UltraDataColumn38, UltraDataColumn39, UltraDataColumn40, UltraDataColumn41, UltraDataColumn42, UltraDataColumn43, UltraDataColumn44, UltraDataColumn45, UltraDataColumn46})
        Me.UltraDataSource2.Band.ChildBands.AddRange(New Object() {UltraDataBand4})
        UltraDataColumn47.DataType = GetType(Long)
        Me.UltraDataSource2.Band.Columns.AddRange(New Object() {UltraDataColumn47, UltraDataColumn48})
        Me.UltraDataSource2.UseBindingSource = True
        '
        'UltraDataSource4
        '
        UltraDataColumn50.DataType = GetType(Long)
        UltraDataColumn52.DataType = GetType(Boolean)
        UltraDataColumn53.DataType = GetType(Double)
        UltraDataColumn54.DataType = GetType(Double)
        UltraDataColumn56.DataType = GetType(Double)
        UltraDataColumn57.DataType = GetType(Double)
        UltraDataBand5.Columns.AddRange(New Object() {UltraDataColumn49, UltraDataColumn50, UltraDataColumn51, UltraDataColumn52, UltraDataColumn53, UltraDataColumn54, UltraDataColumn55, UltraDataColumn56, UltraDataColumn57})
        Me.UltraDataSource4.Band.ChildBands.AddRange(New Object() {UltraDataBand5})
        UltraDataColumn58.DataType = GetType(Long)
        Me.UltraDataSource4.Band.Columns.AddRange(New Object() {UltraDataColumn58, UltraDataColumn59})
        Me.UltraDataSource4.UseBindingSource = True
        '
        'cmbGroup
        '
        Me.cmbGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Appearance20.BorderColor = System.Drawing.Color.Silver
        Me.cmbGroup.DisplayLayout.Appearance = Appearance20
        Me.cmbGroup.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn
        Appearance21.BackColor = System.Drawing.Color.PapayaWhip
        Me.cmbGroup.DisplayLayout.Override.RowAlternateAppearance = Appearance21
        Me.cmbGroup.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.[Default]
        Me.cmbGroup.Location = New System.Drawing.Point(390, 91)
        Me.cmbGroup.Name = "cmbGroup"
        Me.cmbGroup.Size = New System.Drawing.Size(164, 22)
        Me.cmbGroup.TabIndex = 9
        Me.cmbGroup.Visible = False
        '
        'dtFrom
        '
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFrom.Location = New System.Drawing.Point(141, 12)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(90, 20)
        Me.dtFrom.TabIndex = 0
        '
        'dtTo
        '
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTo.Location = New System.Drawing.Point(259, 12)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(90, 20)
        Me.dtTo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Change expiry date below"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(237, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(16, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "to"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(218, 37)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(131, 23)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "Update all databases"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtTo)
        Me.GroupBox1.Controls.Add(Me.dtFrom)
        Me.GroupBox1.Location = New System.Drawing.Point(575, 49)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(359, 64)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Update Expiry Date"
        Me.GroupBox1.Visible = False
        '
        'UltraGrid1
        '
        Me.UltraGrid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance22.BorderColor = System.Drawing.Color.Silver
        Me.UltraGrid1.DisplayLayout.Appearance = Appearance22
        Me.UltraGrid1.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns
        UltraGridColumn15.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance23.BackColorDisabled = System.Drawing.Color.Silver
        Appearance23.FontData.BoldAsString = "True"
        Appearance23.ForeColor = System.Drawing.Color.Black
        Appearance23.ForeColorDisabled = System.Drawing.Color.Black
        UltraGridColumn15.CellAppearance = Appearance23
        UltraGridColumn15.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn15.DefaultCellValue = "0"
        UltraGridColumn15.Header.VisiblePosition = 0
        UltraGridColumn15.Width = 8
        UltraGridColumn16.DefaultCellValue = "0"
        UltraGridColumn16.Header.Caption = "Item "
        UltraGridColumn16.Header.VisiblePosition = 2
        UltraGridColumn16.Nullable = Infragistics.Win.UltraWinGrid.Nullable.Disallow
        UltraGridColumn16.NullText = "0"
        UltraGridColumn16.Width = 129
        UltraGridColumn17.AutoEdit = False
        UltraGridColumn17.DefaultCellValue = ""
        UltraGridColumn17.Header.Caption = "Item Description"
        UltraGridColumn17.Header.VisiblePosition = 1
        UltraGridColumn17.NullText = "-"
        UltraGridColumn17.Width = 40
        UltraGridColumn18.DefaultCellValue = ""
        UltraGridColumn18.Header.Caption = "Description"
        UltraGridColumn18.Header.VisiblePosition = 3
        UltraGridColumn18.NullText = "-"
        UltraGridColumn18.Width = 45
        UltraGridColumn19.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        UltraGridColumn19.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn19.DefaultCellValue = "0"
        UltraGridColumn19.Header.Caption = "WH"
        UltraGridColumn19.Header.VisiblePosition = 5
        UltraGridColumn19.Hidden = True
        UltraGridColumn19.NullText = "0"
        UltraGridColumn19.Width = 46
        Appearance24.TextHAlignAsString = "Right"
        UltraGridColumn20.CellAppearance = Appearance24
        UltraGridColumn20.DefaultCellValue = "0"
        UltraGridColumn20.Format = "0.00"
        UltraGridColumn20.Header.VisiblePosition = 8
        UltraGridColumn20.NullText = "0"
        UltraGridColumn20.Width = 26
        UltraGridColumn21.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        UltraGridColumn21.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn21.Header.Caption = "Unit"
        UltraGridColumn21.Header.VisiblePosition = 6
        UltraGridColumn21.Hidden = True
        UltraGridColumn21.Width = 45
        UltraGridColumn22.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance25.BackColorDisabled = System.Drawing.Color.Silver
        UltraGridColumn22.CellAppearance = Appearance25
        UltraGridColumn22.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn22.Header.VisiblePosition = 9
        UltraGridColumn22.Hidden = True
        UltraGridColumn22.Width = 29
        UltraGridColumn23.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance26.BackColorDisabled = System.Drawing.Color.Silver
        Appearance26.ForeColorDisabled = System.Drawing.Color.Black
        Appearance26.TextHAlignAsString = "Right"
        UltraGridColumn23.CellAppearance = Appearance26
        UltraGridColumn23.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn23.Format = "0.00"
        UltraGridColumn23.Header.VisiblePosition = 10
        UltraGridColumn23.Width = 25
        UltraGridColumn24.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance27.TextHAlignAsString = "Right"
        UltraGridColumn24.CellAppearance = Appearance27
        UltraGridColumn24.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn24.DefaultCellValue = "0"
        UltraGridColumn24.Format = "0.00"
        UltraGridColumn24.Header.VisiblePosition = 14
        UltraGridColumn24.NullText = "0"
        UltraGridColumn24.Width = 35
        UltraGridColumn25.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance28.BackColorDisabled = System.Drawing.Color.Silver
        Appearance28.ForeColorDisabled = System.Drawing.Color.Black
        Appearance28.TextHAlignAsString = "Right"
        UltraGridColumn25.CellAppearance = Appearance28
        UltraGridColumn25.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn25.DefaultCellValue = "0"
        UltraGridColumn25.Format = "0.00"
        UltraGridColumn25.Header.Caption = "Price (Excl)"
        UltraGridColumn25.Header.VisiblePosition = 15
        UltraGridColumn25.Hidden = True
        UltraGridColumn25.NullText = "0"
        UltraGridColumn25.Width = 25
        UltraGridColumn26.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance29.BackColorDisabled = System.Drawing.Color.Silver
        Appearance29.ForeColorDisabled = System.Drawing.Color.Black
        Appearance29.TextHAlignAsString = "Right"
        UltraGridColumn26.CellAppearance = Appearance29
        UltraGridColumn26.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn26.DefaultCellValue = "0"
        UltraGridColumn26.Format = "0.00"
        UltraGridColumn26.Header.Caption = "Price (Incl)"
        UltraGridColumn26.Header.VisiblePosition = 16
        UltraGridColumn26.Hidden = True
        UltraGridColumn26.NullText = "0"
        UltraGridColumn26.Width = 52
        UltraGridColumn27.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance30.TextHAlignAsString = "Right"
        UltraGridColumn27.CellAppearance = Appearance30
        UltraGridColumn27.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn27.DefaultCellValue = "0"
        UltraGridColumn27.Header.VisiblePosition = 17
        UltraGridColumn27.Hidden = True
        UltraGridColumn27.NullText = "0"
        UltraGridColumn27.Width = 17
        UltraGridColumn28.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance31.BackColorDisabled = System.Drawing.Color.Silver
        Appearance31.FontData.BoldAsString = "True"
        Appearance31.ForeColor = System.Drawing.Color.Black
        Appearance31.ForeColorDisabled = System.Drawing.Color.Black
        Appearance31.TextHAlignAsString = "Right"
        UltraGridColumn28.CellAppearance = Appearance31
        UltraGridColumn28.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn28.DefaultCellValue = "0"
        UltraGridColumn28.Format = "0.00"
        UltraGridColumn28.Header.VisiblePosition = 18
        UltraGridColumn28.Hidden = True
        UltraGridColumn28.NullText = "0"
        UltraGridColumn28.Width = 9
        UltraGridColumn29.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance32.TextHAlignAsString = "Right"
        UltraGridColumn29.CellAppearance = Appearance32
        UltraGridColumn29.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn29.DefaultCellValue = "0"
        UltraGridColumn29.Format = "0.00"
        UltraGridColumn29.Header.VisiblePosition = 19
        UltraGridColumn29.Hidden = True
        UltraGridColumn29.NullText = "0"
        UltraGridColumn29.Width = 27
        UltraGridColumn30.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance33.BackColorDisabled = System.Drawing.Color.Silver
        Appearance33.FontData.BoldAsString = "True"
        Appearance33.ForeColor = System.Drawing.Color.Black
        Appearance33.ForeColorDisabled = System.Drawing.Color.Black
        Appearance33.TextHAlignAsString = "Right"
        UltraGridColumn30.CellAppearance = Appearance33
        UltraGridColumn30.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn30.DefaultCellValue = "0"
        UltraGridColumn30.Format = "0.00"
        UltraGridColumn30.Header.Caption = "OrderTotal (Excl)"
        UltraGridColumn30.Header.VisiblePosition = 20
        UltraGridColumn30.Hidden = True
        UltraGridColumn30.NullText = "0"
        UltraGridColumn30.Width = 16
        UltraGridColumn31.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance34.BackColorDisabled = System.Drawing.Color.Silver
        Appearance34.FontData.BoldAsString = "True"
        Appearance34.ForeColor = System.Drawing.Color.Black
        Appearance34.ForeColorDisabled = System.Drawing.Color.Black
        Appearance34.TextHAlignAsString = "Right"
        UltraGridColumn31.CellAppearance = Appearance34
        UltraGridColumn31.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn31.DefaultCellValue = "0"
        UltraGridColumn31.Format = "0.00"
        UltraGridColumn31.Header.Caption = "OrderTotal (Incl)"
        UltraGridColumn31.Header.VisiblePosition = 21
        UltraGridColumn31.Hidden = True
        UltraGridColumn31.NullText = "0"
        UltraGridColumn31.Width = 16
        UltraGridColumn32.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance35.BackColorDisabled = System.Drawing.Color.Silver
        Appearance35.FontData.BoldAsString = "True"
        Appearance35.ForeColor = System.Drawing.Color.Black
        Appearance35.ForeColorDisabled = System.Drawing.Color.Black
        Appearance35.TextHAlignAsString = "Right"
        UltraGridColumn32.CellAppearance = Appearance35
        UltraGridColumn32.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn32.DefaultCellValue = "0"
        UltraGridColumn32.Format = "0.00"
        UltraGridColumn32.Header.VisiblePosition = 22
        UltraGridColumn32.Hidden = True
        UltraGridColumn32.NullText = "0"
        UltraGridColumn32.Width = 21
        UltraGridColumn33.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance36.BackColorDisabled = System.Drawing.Color.Silver
        Appearance36.FontData.BoldAsString = "True"
        Appearance36.ForeColor = System.Drawing.Color.Black
        Appearance36.ForeColorDisabled = System.Drawing.Color.Black
        Appearance36.TextHAlignAsString = "Right"
        UltraGridColumn33.CellAppearance = Appearance36
        UltraGridColumn33.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn33.DefaultCellValue = "0"
        UltraGridColumn33.Format = "0.00"
        UltraGridColumn33.Header.VisiblePosition = 23
        UltraGridColumn33.Hidden = True
        UltraGridColumn33.NullText = "0"
        UltraGridColumn33.Width = 16
        UltraGridColumn34.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance37.BackColorDisabled = System.Drawing.Color.Silver
        Appearance37.FontData.BoldAsString = "True"
        Appearance37.ForeColor = System.Drawing.Color.Black
        Appearance37.ForeColorDisabled = System.Drawing.Color.Black
        Appearance37.TextHAlignAsString = "Right"
        UltraGridColumn34.CellAppearance = Appearance37
        UltraGridColumn34.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn34.DefaultCellValue = "0"
        UltraGridColumn34.Format = "0.00"
        UltraGridColumn34.Header.Caption = "LineTotal (Excl)"
        UltraGridColumn34.Header.VisiblePosition = 24
        UltraGridColumn34.Hidden = True
        UltraGridColumn34.NullText = "0"
        UltraGridColumn34.Width = 16
        UltraGridColumn35.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance38.BackColorDisabled = System.Drawing.Color.Silver
        Appearance38.FontData.BoldAsString = "True"
        Appearance38.ForeColor = System.Drawing.Color.Black
        Appearance38.ForeColorDisabled = System.Drawing.Color.Black
        Appearance38.TextHAlignAsString = "Right"
        UltraGridColumn35.CellAppearance = Appearance38
        UltraGridColumn35.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn35.DefaultCellValue = "0"
        UltraGridColumn35.Format = "0.00"
        UltraGridColumn35.Header.Caption = "LineTotal (Incl)"
        UltraGridColumn35.Header.VisiblePosition = 25
        UltraGridColumn35.Hidden = True
        UltraGridColumn35.NullText = "0"
        UltraGridColumn35.Width = 43
        UltraGridColumn36.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance39.TextHAlignAsString = "Right"
        UltraGridColumn36.CellAppearance = Appearance39
        UltraGridColumn36.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn36.DefaultCellValue = "0"
        UltraGridColumn36.Header.VisiblePosition = 26
        UltraGridColumn36.Hidden = True
        UltraGridColumn36.Width = 56
        UltraGridColumn37.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance40.TextHAlignAsString = "Right"
        UltraGridColumn37.CellAppearance = Appearance40
        UltraGridColumn37.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn37.DefaultCellValue = "0"
        UltraGridColumn37.Header.VisiblePosition = 27
        UltraGridColumn37.Hidden = True
        UltraGridColumn37.Width = 47
        UltraGridColumn38.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        UltraGridColumn38.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn38.DefaultCellValue = "0"
        UltraGridColumn38.Header.VisiblePosition = 28
        UltraGridColumn38.Hidden = True
        UltraGridColumn38.Width = 86
        UltraGridColumn39.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        UltraGridColumn39.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn39.DefaultCellValue = "0"
        UltraGridColumn39.Header.VisiblePosition = 29
        UltraGridColumn39.Hidden = True
        UltraGridColumn39.Width = 87
        UltraGridColumn40.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        UltraGridColumn40.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn40.DefaultCellValue = "0"
        UltraGridColumn40.Header.VisiblePosition = 30
        UltraGridColumn40.Hidden = True
        UltraGridColumn40.Width = 87
        UltraGridColumn41.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        UltraGridColumn41.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn41.DefaultCellValue = "0"
        UltraGridColumn41.Header.VisiblePosition = 31
        UltraGridColumn41.Hidden = True
        UltraGridColumn41.Width = 88
        UltraGridColumn42.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        UltraGridColumn42.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn42.DefaultCellValue = "0"
        UltraGridColumn42.Header.VisiblePosition = 33
        UltraGridColumn42.Hidden = True
        UltraGridColumn42.Width = 88
        UltraGridColumn43.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        UltraGridColumn43.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn43.DefaultCellValue = "0"
        UltraGridColumn43.Header.VisiblePosition = 34
        UltraGridColumn43.Hidden = True
        UltraGridColumn43.Width = 89
        UltraGridColumn44.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        UltraGridColumn44.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn44.DefaultCellValue = "0"
        UltraGridColumn44.Header.VisiblePosition = 35
        UltraGridColumn44.Hidden = True
        UltraGridColumn44.NullText = "0"
        UltraGridColumn44.Width = 89
        UltraGridColumn45.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        UltraGridColumn45.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn45.Header.VisiblePosition = 36
        UltraGridColumn45.Hidden = True
        UltraGridColumn45.Width = 76
        UltraGridColumn46.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        UltraGridColumn46.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn46.Header.VisiblePosition = 32
        UltraGridColumn46.Hidden = True
        UltraGridColumn46.Width = 54
        UltraGridColumn47.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        UltraGridColumn47.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn47.Header.VisiblePosition = 4
        UltraGridColumn47.Hidden = True
        UltraGridColumn47.Width = 44
        UltraGridColumn48.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        UltraGridColumn48.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn48.Header.VisiblePosition = 37
        UltraGridColumn48.Hidden = True
        UltraGridColumn48.Width = 102
        UltraGridColumn49.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        UltraGridColumn49.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn49.Header.VisiblePosition = 39
        UltraGridColumn49.Hidden = True
        UltraGridColumn49.Width = 70
        UltraGridColumn50.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance41.TextHAlignAsString = "Right"
        UltraGridColumn50.CellAppearance = Appearance41
        UltraGridColumn50.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn50.Header.Caption = "Margin(%)"
        UltraGridColumn50.Header.VisiblePosition = 12
        UltraGridColumn50.Hidden = True
        UltraGridColumn50.Width = 81
        UltraGridColumn51.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance42.TextHAlignAsString = "Right"
        UltraGridColumn51.CellAppearance = Appearance42
        UltraGridColumn51.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn51.Format = "0.00"
        UltraGridColumn51.Header.Caption = "Margin "
        UltraGridColumn51.Header.VisiblePosition = 13
        UltraGridColumn51.Hidden = True
        UltraGridColumn51.Width = 88
        UltraGridColumn52.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance43.TextHAlignAsString = "Right"
        UltraGridColumn52.CellAppearance = Appearance43
        UltraGridColumn52.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn52.Format = "0.00"
        UltraGridColumn52.Header.VisiblePosition = 11
        UltraGridColumn52.Hidden = True
        UltraGridColumn52.Width = 163
        UltraGridColumn53.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance44.FontData.BoldAsString = "True"
        UltraGridColumn53.CellAppearance = Appearance44
        UltraGridColumn53.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn53.Header.VisiblePosition = 7
        UltraGridColumn53.Width = 25
        UltraGridColumn54.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        Appearance45.FontData.BoldAsString = "True"
        UltraGridColumn54.CellAppearance = Appearance45
        UltraGridColumn54.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn54.Header.VisiblePosition = 40
        UltraGridColumn54.Hidden = True
        UltraGridColumn54.Width = 97
        UltraGridColumn55.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        UltraGridColumn55.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn55.DefaultCellValue = "0"
        UltraGridColumn55.Header.VisiblePosition = 41
        UltraGridColumn55.NullText = " "
        UltraGridColumn55.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate
        UltraGridColumn55.Width = 61
        UltraGridColumn56.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        UltraGridColumn56.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn56.Header.VisiblePosition = 42
        UltraGridColumn56.Hidden = True
        UltraGridColumn56.Width = 75
        UltraGridColumn57.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        UltraGridColumn57.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn57.Header.VisiblePosition = 38
        UltraGridColumn57.Hidden = True
        UltraGridColumn57.Width = 113
        UltraGridColumn58.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        UltraGridColumn58.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn58.Header.VisiblePosition = 43
        UltraGridColumn58.Hidden = True
        UltraGridColumn58.Width = 91
        UltraGridColumn59.Header.VisiblePosition = 44
        UltraGridColumn60.ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always
        UltraGridColumn60.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
        UltraGridColumn60.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
        UltraGridColumn60.Header.VisiblePosition = 45
        UltraGridColumn60.Hidden = True
        UltraGridColumn60.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button
        UltraGridColumn60.Width = 53
        UltraGridBand3.Columns.AddRange(New Object() {UltraGridColumn15, UltraGridColumn16, UltraGridColumn17, UltraGridColumn18, UltraGridColumn19, UltraGridColumn20, UltraGridColumn21, UltraGridColumn22, UltraGridColumn23, UltraGridColumn24, UltraGridColumn25, UltraGridColumn26, UltraGridColumn27, UltraGridColumn28, UltraGridColumn29, UltraGridColumn30, UltraGridColumn31, UltraGridColumn32, UltraGridColumn33, UltraGridColumn34, UltraGridColumn35, UltraGridColumn36, UltraGridColumn37, UltraGridColumn38, UltraGridColumn39, UltraGridColumn40, UltraGridColumn41, UltraGridColumn42, UltraGridColumn43, UltraGridColumn44, UltraGridColumn45, UltraGridColumn46, UltraGridColumn47, UltraGridColumn48, UltraGridColumn49, UltraGridColumn50, UltraGridColumn51, UltraGridColumn52, UltraGridColumn53, UltraGridColumn54, UltraGridColumn55, UltraGridColumn56, UltraGridColumn57, UltraGridColumn58, UltraGridColumn59, UltraGridColumn60})
        UltraGridColumn61.Header.VisiblePosition = 0
        UltraGridColumn61.Width = 204
        UltraGridColumn62.Header.VisiblePosition = 1
        UltraGridColumn62.Width = 302
        UltraGridColumn63.Header.VisiblePosition = 2
        UltraGridColumn63.Width = 225
        UltraGridColumn64.Header.VisiblePosition = 3
        UltraGridColumn64.Width = 238
        UltraGridBand4.Columns.AddRange(New Object() {UltraGridColumn61, UltraGridColumn62, UltraGridColumn63, UltraGridColumn64})
        UltraGridBand4.Hidden = True
        Me.UltraGrid1.DisplayLayout.BandsSerializer.Add(UltraGridBand3)
        Me.UltraGrid1.DisplayLayout.BandsSerializer.Add(UltraGridBand4)
        Me.UltraGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance46.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.UltraGrid1.DisplayLayout.Override.CellAppearance = Appearance46
        Me.UltraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit
        Me.UltraGrid1.DisplayLayout.Override.DefaultRowHeight = 0
        Appearance47.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.UltraGrid1.DisplayLayout.Override.RowAppearance = Appearance47
        Me.UltraGrid1.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton
        Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[True]
        Me.UltraGrid1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraGrid1.Location = New System.Drawing.Point(478, 160)
        Me.UltraGrid1.Name = "UltraGrid1"
        Me.UltraGrid1.Size = New System.Drawing.Size(415, 238)
        Me.UltraGrid1.TabIndex = 48
        Me.UltraGrid1.Visible = False
        '
        'btnAutoUpdate
        '
        Me.btnAutoUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnAutoUpdate.Image = CType(resources.GetObject("btnAutoUpdate.Image"), System.Drawing.Image)
        Me.btnAutoUpdate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAutoUpdate.Name = "btnAutoUpdate"
        Me.btnAutoUpdate.Size = New System.Drawing.Size(23, 22)
        Me.btnAutoUpdate.Text = "ToolStripButton1"
        '
        'frmUpdatePriceList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(961, 492)
        Me.Controls.Add(Me.UltraGrid1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmbGroup)
        Me.Controls.Add(Me.lbl6)
        Me.Controls.Add(Me.txtCode)
        Me.Controls.Add(Me.txtSimpleCode)
        Me.Controls.Add(Me.UltraLabel3)
        Me.Controls.Add(Me.UltraLabel2)
        Me.Controls.Add(Me.UG)
        Me.Controls.Add(Me.cmbInventoryItem)
        Me.Controls.Add(Me.ultraLabel1)
        Me.Controls.Add(Me.toolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmUpdatePriceList"
        Me.Text = "Update Price List"
        Me.toolStrip1.ResumeLayout(False)
        Me.toolStrip1.PerformLayout()
        CType(Me.cmbInventoryItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraDataSource5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraDataSource3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraDataSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSimpleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraDataSource2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraDataSource4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents toolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbUpdate As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbConnect As System.Windows.Forms.ToolStripButton
    Friend WithEvents ultraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmbInventoryItem As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UG As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtSimpleCode As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txtCode As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraDataSource1 As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents lbl6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraDataSource2 As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents UltraDataSource3 As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents UltraDataSource5 As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents UltraDataSource4 As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents cmbGroup As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents UltraGrid1 As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents btnAutoUpdate As System.Windows.Forms.ToolStripButton
End Class
