<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStockCount
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
        Me.components = New System.ComponentModel.Container
        Dim UltraDataBand1 As Infragistics.Win.UltraWinDataSource.UltraDataBand = New Infragistics.Win.UltraWinDataSource.UltraDataBand("Band 1")
        Dim UltraDataColumn1 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("LN")
        Dim UltraDataColumn2 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("SerialNumber")
        Dim UltraDataColumn3 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("SNStockLink")
        Dim UltraDataColumn4 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("PrimaryLineID")
        Dim UltraDataColumn5 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Line")
        Dim UltraDataColumn6 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("StockID")
        Dim UltraDataColumn7 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Code")
        Dim UltraDataColumn8 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Description_1")
        Dim UltraDataColumn9 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Description_2")
        Dim UltraDataColumn10 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Quantity")
        Dim UltraDataColumn11 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("AvailableQty")
        Dim UltraDataColumn12 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Warehouse")
        Dim UltraDataColumn13 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Unit")
        Dim UltraDataColumn14 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Price_Excl")
        Dim UltraDataColumn15 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Price_Incl")
        Dim UltraDataColumn16 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("LineNote")
        Dim UltraDataColumn17 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("IsSerialNo")
        Dim UltraDataColumn18 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("IsWH")
        Dim UltraDataColumn19 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("LineID")
        Dim UltraDataColumn20 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("SerialLot")
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraDataBand2 As Infragistics.Win.UltraWinDataSource.UltraDataBand = New Infragistics.Win.UltraWinDataSource.UltraDataBand("Band 1")
        Dim UltraDataColumn21 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("LN")
        Dim UltraDataColumn22 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("SerialNumber")
        Dim UltraDataColumn23 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("SNStockLink")
        Dim UltraDataColumn24 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("PrimaryLineID")
        Dim UltraDataColumn25 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Line")
        Dim UltraDataColumn26 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("StockID")
        Dim UltraDataColumn27 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Code")
        Dim UltraDataColumn28 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Description_1")
        Dim UltraDataColumn29 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Description_2")
        Dim UltraDataColumn30 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("AvailableQty")
        Dim UltraDataColumn31 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Warehouse")
        Dim UltraDataColumn32 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Unit")
        Dim UltraDataColumn33 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Price_Excl")
        Dim UltraDataColumn34 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Price_Incl")
        Dim UltraDataColumn35 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("LineNote")
        Dim UltraDataColumn36 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("IsSerialNo")
        Dim UltraDataColumn37 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("IsWH")
        Dim UltraDataColumn38 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("LineID")
        Dim UltraDataColumn39 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("SerialLot")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStockCount))
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraDataBand3 As Infragistics.Win.UltraWinDataSource.UltraDataBand = New Infragistics.Win.UltraWinDataSource.UltraDataBand("Band 1")
        Dim UltraDataColumn40 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("LN")
        Dim UltraDataColumn41 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("SerialNumber")
        Dim UltraDataColumn42 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("SNStockLink")
        Dim UltraDataColumn43 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("PrimaryLineID")
        Dim UltraDataColumn44 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Line")
        Dim UltraDataColumn45 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("StockID")
        Dim UltraDataColumn46 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Code")
        Dim UltraDataColumn47 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Description_1")
        Dim UltraDataColumn48 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Description_2")
        Dim UltraDataColumn49 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Quantity")
        Dim UltraDataColumn50 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("AvailableQty")
        Dim UltraDataColumn51 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Warehouse")
        Dim UltraDataColumn52 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Unit")
        Dim UltraDataColumn53 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Price_Excl")
        Dim UltraDataColumn54 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Price_Incl")
        Dim UltraDataColumn55 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("LineNote")
        Dim UltraDataColumn56 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("IsSerialNo")
        Dim UltraDataColumn57 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("IsWH")
        Dim UltraDataColumn58 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("LineID")
        Dim UltraDataColumn59 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("SerialLot")
        Dim UltraDataBand4 As Infragistics.Win.UltraWinDataSource.UltraDataBand = New Infragistics.Win.UltraWinDataSource.UltraDataBand("Band 1")
        Dim UltraDataColumn60 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("LN")
        Dim UltraDataColumn61 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("SerialNumber")
        Dim UltraDataColumn62 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("SNStockLink")
        Dim UltraDataColumn63 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("PrimaryLineID")
        Dim UltraDataColumn64 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Line")
        Dim UltraDataColumn65 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("StockID")
        Dim UltraDataColumn66 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Code")
        Dim UltraDataColumn67 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Description_1")
        Dim UltraDataColumn68 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Description_2")
        Dim UltraDataColumn69 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Quantity")
        Dim UltraDataColumn70 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("AvailableQty")
        Dim UltraDataColumn71 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Warehouse")
        Dim UltraDataColumn72 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Unit")
        Dim UltraDataColumn73 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Price_Excl")
        Dim UltraDataColumn74 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Price_Incl")
        Dim UltraDataColumn75 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("LineNote")
        Dim UltraDataColumn76 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("IsSerialNo")
        Dim UltraDataColumn77 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("IsWH")
        Dim UltraDataColumn78 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("LineID")
        Dim UltraDataColumn79 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("SerialLot")
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Line")
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("StockID")
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Code", -1, Nothing, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, False)
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Description_1", -1, "DDDescription")
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Description_2")
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Quantity")
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("AvailableQty")
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridColumn8 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Warehouse", -1, "DDWH")
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridColumn9 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Unit")
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridColumn10 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Price_Excl")
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridColumn11 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Price_Incl")
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridColumn12 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("LineNote")
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridColumn13 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("IsSerialNo")
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridColumn14 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("IsWH")
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridColumn15 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("LineID")
        Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridColumn16 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("SerialLot")
        Dim Appearance25 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridColumn17 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Band 1")
        Dim UltraGridBand2 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 1", 0)
        Dim UltraGridColumn18 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("SerialNumber")
        Dim UltraGridColumn19 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("PrimaryLineID")
        Dim UltraGridColumn20 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("BIN")
        Dim UltraDataBand5 As Infragistics.Win.UltraWinDataSource.UltraDataBand = New Infragistics.Win.UltraWinDataSource.UltraDataBand("Band 1")
        Dim UltraDataColumn80 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("SerialNumber")
        Dim UltraDataColumn81 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("PrimaryLineID")
        Dim UltraDataColumn82 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("BIN")
        Dim UltraDataColumn83 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Line")
        Dim UltraDataColumn84 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("StockID")
        Dim UltraDataColumn85 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Code")
        Dim UltraDataColumn86 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Description_1")
        Dim UltraDataColumn87 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Description_2")
        Dim UltraDataColumn88 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Quantity")
        Dim UltraDataColumn89 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("AvailableQty")
        Dim UltraDataColumn90 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Warehouse")
        Dim UltraDataColumn91 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Unit")
        Dim UltraDataColumn92 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Price_Excl")
        Dim UltraDataColumn93 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Price_Incl")
        Dim UltraDataColumn94 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("LineNote")
        Dim UltraDataColumn95 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("IsSerialNo")
        Dim UltraDataColumn96 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("IsWH")
        Dim UltraDataColumn97 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("LineID")
        Dim UltraDataColumn98 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("SerialLot")
        Dim UltraDataBand6 As Infragistics.Win.UltraWinDataSource.UltraDataBand = New Infragistics.Win.UltraWinDataSource.UltraDataBand("Band 1")
        Dim UltraDataColumn99 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("SerialNumber")
        Dim UltraDataColumn100 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("PrimaryLineID")
        Dim UltraDataColumn101 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Line")
        Dim UltraDataColumn102 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("StockID")
        Dim UltraDataColumn103 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Code")
        Dim UltraDataColumn104 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Description_1")
        Dim UltraDataColumn105 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Description_2")
        Dim UltraDataColumn106 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Quantity")
        Dim UltraDataColumn107 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("AvailableQty")
        Dim UltraDataColumn108 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Warehouse")
        Dim UltraDataColumn109 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Unit")
        Dim UltraDataColumn110 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Price_Excl")
        Dim UltraDataColumn111 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Price_Incl")
        Dim UltraDataColumn112 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("LineNote")
        Dim UltraDataColumn113 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("IsSerialNo")
        Dim UltraDataColumn114 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("IsWH")
        Dim UltraDataColumn115 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("LineID")
        Dim UltraDataColumn116 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("SerialLot")
        Dim UltraDataBand7 As Infragistics.Win.UltraWinDataSource.UltraDataBand = New Infragistics.Win.UltraWinDataSource.UltraDataBand("Band 1")
        Dim UltraDataColumn117 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("SerialNumber")
        Dim UltraDataColumn118 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("PrimaryLineID")
        Dim UltraDataColumn119 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Line")
        Dim UltraDataColumn120 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("StockID")
        Dim UltraDataColumn121 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Code")
        Dim UltraDataColumn122 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Description_1")
        Dim UltraDataColumn123 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Description_2")
        Dim UltraDataColumn124 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Quantity")
        Dim UltraDataColumn125 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("AvailableQty")
        Dim UltraDataColumn126 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Warehouse")
        Dim UltraDataColumn127 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Unit")
        Dim UltraDataColumn128 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Price_Excl")
        Dim UltraDataColumn129 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Price_Incl")
        Dim UltraDataColumn130 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("LineNote")
        Dim UltraDataColumn131 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("IsSerialNo")
        Dim UltraDataColumn132 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("IsWH")
        Dim UltraDataColumn133 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("LineID")
        Dim UltraDataColumn134 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("SerialLot")
        Dim Appearance26 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance27 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance28 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance29 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance30 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Me.UltraDataSource2 = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.txtBarCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.UltraDataSource1 = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.tsbOpen = New System.Windows.Forms.ToolStripButton
        Me.tsbUpdate = New System.Windows.Forms.ToolStripButton
        Me.tsbUpdateSO = New System.Windows.Forms.ToolStripButton
        Me.tsbProcessPO = New System.Windows.Forms.ToolStripButton
        Me.tsbDeleteLine = New System.Windows.Forms.ToolStripButton
        Me.tsbNewlIne = New System.Windows.Forms.ToolStripButton
        Me.tsbSaveStock = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton
        Me.DDDescription = New Infragistics.Win.UltraWinGrid.UltraDropDown
        Me.cmbInventoryItem = New Infragistics.Win.UltraWinGrid.UltraCombo
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel
        Me.txtQty = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel
        Me.UltraDataSource3 = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.UltraDataSource4 = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.UG = New Infragistics.Win.UltraWinGrid.UltraGrid
        Me.UltraDataSource7 = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.UltraDataSource6 = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.UltraDataSource5 = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.DDWH = New Infragistics.Win.UltraWinGrid.UltraDropDown
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel
        Me.txtBin = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Button1 = New System.Windows.Forms.Button
        CType(Me.UltraDataSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBarCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraDataSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.DDDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbInventoryItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraDataSource3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraDataSource4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraDataSource7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraDataSource6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraDataSource5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DDWH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UltraDataSource2
        '
        UltraDataBand1.Columns.AddRange(New Object() {UltraDataColumn1, UltraDataColumn2, UltraDataColumn3, UltraDataColumn4})
        Me.UltraDataSource2.Band.ChildBands.AddRange(New Object() {UltraDataBand1})
        Me.UltraDataSource2.Band.Columns.AddRange(New Object() {UltraDataColumn5, UltraDataColumn6, UltraDataColumn7, UltraDataColumn8, UltraDataColumn9, UltraDataColumn10, UltraDataColumn11, UltraDataColumn12, UltraDataColumn13, UltraDataColumn14, UltraDataColumn15, UltraDataColumn16, UltraDataColumn17, UltraDataColumn18, UltraDataColumn19, UltraDataColumn20})
        Me.UltraDataSource2.UseBindingSource = True
        '
        'txtBarCode
        '
        Appearance1.TextHAlignAsString = "Center"
        Me.txtBarCode.Appearance = Appearance1
        Me.txtBarCode.AutoSize = False
        Me.txtBarCode.Location = New System.Drawing.Point(85, 66)
        Me.txtBarCode.Name = "txtBarCode"
        Me.txtBarCode.Size = New System.Drawing.Size(126, 22)
        Me.txtBarCode.TabIndex = 18
        '
        'UltraDataSource1
        '
        UltraDataBand2.Columns.AddRange(New Object() {UltraDataColumn21, UltraDataColumn22, UltraDataColumn23, UltraDataColumn24})
        Me.UltraDataSource1.Band.ChildBands.AddRange(New Object() {UltraDataBand2})
        Me.UltraDataSource1.Band.Columns.AddRange(New Object() {UltraDataColumn25, UltraDataColumn26, UltraDataColumn27, UltraDataColumn28, UltraDataColumn29, UltraDataColumn30, UltraDataColumn31, UltraDataColumn32, UltraDataColumn33, UltraDataColumn34, UltraDataColumn35, UltraDataColumn36, UltraDataColumn37, UltraDataColumn38, UltraDataColumn39})
        Me.UltraDataSource1.UseBindingSource = True
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripSeparator2, Me.tsbOpen, Me.tsbUpdate, Me.tsbUpdateSO, Me.tsbProcessPO, Me.tsbDeleteLine, Me.tsbNewlIne, Me.tsbSaveStock, Me.ToolStripButton2, Me.ToolStripButton3})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip1.Size = New System.Drawing.Size(730, 25)
        Me.ToolStrip1.TabIndex = 19
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(59, 22)
        Me.ToolStripButton1.Text = " Close"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'tsbOpen
        '
        Me.tsbOpen.Image = CType(resources.GetObject("tsbOpen.Image"), System.Drawing.Image)
        Me.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbOpen.Name = "tsbOpen"
        Me.tsbOpen.Size = New System.Drawing.Size(56, 22)
        Me.tsbOpen.Tag = "22214"
        Me.tsbOpen.Text = "Open"
        '
        'tsbUpdate
        '
        Me.tsbUpdate.Image = CType(resources.GetObject("tsbUpdate.Image"), System.Drawing.Image)
        Me.tsbUpdate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbUpdate.Name = "tsbUpdate"
        Me.tsbUpdate.Size = New System.Drawing.Size(65, 22)
        Me.tsbUpdate.Tag = "22224"
        Me.tsbUpdate.Text = "Update"
        Me.tsbUpdate.Visible = False
        '
        'tsbUpdateSO
        '
        Me.tsbUpdateSO.Image = CType(resources.GetObject("tsbUpdateSO.Image"), System.Drawing.Image)
        Me.tsbUpdateSO.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbUpdateSO.Name = "tsbUpdateSO"
        Me.tsbUpdateSO.Size = New System.Drawing.Size(51, 22)
        Me.tsbUpdateSO.Tag = "22212"
        Me.tsbUpdateSO.Text = "Save"
        Me.tsbUpdateSO.Visible = False
        '
        'tsbProcessPO
        '
        Me.tsbProcessPO.Image = CType(resources.GetObject("tsbProcessPO.Image"), System.Drawing.Image)
        Me.tsbProcessPO.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbProcessPO.Name = "tsbProcessPO"
        Me.tsbProcessPO.Size = New System.Drawing.Size(99, 22)
        Me.tsbProcessPO.Tag = "22224"
        Me.tsbProcessPO.Text = "Process Stock"
        Me.tsbProcessPO.Visible = False
        '
        'tsbDeleteLine
        '
        Me.tsbDeleteLine.Image = CType(resources.GetObject("tsbDeleteLine.Image"), System.Drawing.Image)
        Me.tsbDeleteLine.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbDeleteLine.Name = "tsbDeleteLine"
        Me.tsbDeleteLine.Size = New System.Drawing.Size(85, 22)
        Me.tsbDeleteLine.Text = "Delete Line"
        Me.tsbDeleteLine.Visible = False
        '
        'tsbNewlIne
        '
        Me.tsbNewlIne.Image = CType(resources.GetObject("tsbNewlIne.Image"), System.Drawing.Image)
        Me.tsbNewlIne.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbNewlIne.Name = "tsbNewlIne"
        Me.tsbNewlIne.Size = New System.Drawing.Size(76, 22)
        Me.tsbNewlIne.Text = "New Line"
        Me.tsbNewlIne.Visible = False
        '
        'tsbSaveStock
        '
        Me.tsbSaveStock.Image = CType(resources.GetObject("tsbSaveStock.Image"), System.Drawing.Image)
        Me.tsbSaveStock.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSaveStock.Name = "tsbSaveStock"
        Me.tsbSaveStock.Size = New System.Drawing.Size(96, 22)
        Me.tsbSaveStock.Tag = "22224"
        Me.tsbSaveStock.Text = "Save Settings"
        Me.tsbSaveStock.Visible = False
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton2.Text = "Save Stock Records"
        Me.ToolStripButton2.Visible = False
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton3.Text = "Adjust Stock"
        Me.ToolStripButton3.Visible = False
        '
        'DDDescription
        '
        Appearance2.BorderColor = System.Drawing.Color.Silver
        Me.DDDescription.DisplayLayout.Appearance = Appearance2
        Me.DDDescription.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DDDescription.DisplayLayout.Override.CellAppearance = Appearance3
        Appearance4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DDDescription.DisplayLayout.Override.RowAppearance = Appearance4
        Me.DDDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DDDescription.Location = New System.Drawing.Point(35, 245)
        Me.DDDescription.Name = "DDDescription"
        Me.DDDescription.Size = New System.Drawing.Size(273, 98)
        Me.DDDescription.TabIndex = 26
        Me.DDDescription.Text = "UltraDropDown1"
        Me.DDDescription.Visible = False
        '
        'cmbInventoryItem
        '
        Me.cmbInventoryItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbInventoryItem.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn
        Me.cmbInventoryItem.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.[Default]
        Me.cmbInventoryItem.Location = New System.Drawing.Point(455, 66)
        Me.cmbInventoryItem.Name = "cmbInventoryItem"
        Me.cmbInventoryItem.Size = New System.Drawing.Size(163, 22)
        Me.cmbInventoryItem.TabIndex = 27
        '
        'UltraLabel2
        '
        Appearance5.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance5
        Me.UltraLabel2.AutoSize = True
        Me.UltraLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel2.Location = New System.Drawing.Point(13, 71)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(52, 14)
        Me.UltraLabel2.TabIndex = 28
        Me.UltraLabel2.Text = "Bar Code"
        '
        'UltraLabel1
        '
        Appearance6.TextVAlignAsString = "Middle"
        Me.UltraLabel1.Appearance = Appearance6
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel1.Location = New System.Drawing.Point(423, 71)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(26, 14)
        Me.UltraLabel1.TabIndex = 29
        Me.UltraLabel1.Text = "Item"
        '
        'txtQty
        '
        Appearance7.TextHAlignAsString = "Center"
        Me.txtQty.Appearance = Appearance7
        Me.txtQty.AutoSize = False
        Me.txtQty.Location = New System.Drawing.Point(670, 66)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(48, 22)
        Me.txtQty.TabIndex = 30
        Me.txtQty.Text = "1"
        '
        'UltraLabel3
        '
        Appearance8.TextVAlignAsString = "Middle"
        Me.UltraLabel3.Appearance = Appearance8
        Me.UltraLabel3.AutoSize = True
        Me.UltraLabel3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel3.Location = New System.Drawing.Point(642, 71)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(22, 14)
        Me.UltraLabel3.TabIndex = 31
        Me.UltraLabel3.Text = "Qty"
        '
        'UltraDataSource3
        '
        UltraDataBand3.Columns.AddRange(New Object() {UltraDataColumn40, UltraDataColumn41, UltraDataColumn42, UltraDataColumn43})
        Me.UltraDataSource3.Band.ChildBands.AddRange(New Object() {UltraDataBand3})
        Me.UltraDataSource3.Band.Columns.AddRange(New Object() {UltraDataColumn44, UltraDataColumn45, UltraDataColumn46, UltraDataColumn47, UltraDataColumn48, UltraDataColumn49, UltraDataColumn50, UltraDataColumn51, UltraDataColumn52, UltraDataColumn53, UltraDataColumn54, UltraDataColumn55, UltraDataColumn56, UltraDataColumn57, UltraDataColumn58, UltraDataColumn59})
        Me.UltraDataSource3.UseBindingSource = True
        '
        'UltraDataSource4
        '
        UltraDataBand4.Columns.AddRange(New Object() {UltraDataColumn60, UltraDataColumn61, UltraDataColumn62, UltraDataColumn63})
        Me.UltraDataSource4.Band.ChildBands.AddRange(New Object() {UltraDataBand4})
        Me.UltraDataSource4.Band.Columns.AddRange(New Object() {UltraDataColumn64, UltraDataColumn65, UltraDataColumn66, UltraDataColumn67, UltraDataColumn68, UltraDataColumn69, UltraDataColumn70, UltraDataColumn71, UltraDataColumn72, UltraDataColumn73, UltraDataColumn74, UltraDataColumn75, UltraDataColumn76, UltraDataColumn77, UltraDataColumn78, UltraDataColumn79})
        Me.UltraDataSource4.UseBindingSource = True
        '
        'UG
        '
        Me.UG.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UG.DataSource = Me.UltraDataSource7
        Appearance9.BorderColor = System.Drawing.Color.Silver
        Me.UG.DisplayLayout.Appearance = Appearance9
        Me.UG.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns
        UltraGridColumn1.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled
        Appearance10.BackColorDisabled = System.Drawing.Color.Silver
        Appearance10.BorderColor = System.Drawing.Color.DimGray
        Appearance10.FontData.BoldAsString = "True"
        Appearance10.ForeColor = System.Drawing.Color.Black
        Appearance10.ForeColorDisabled = System.Drawing.Color.Black
        UltraGridColumn1.CellAppearance = Appearance10
        UltraGridColumn1.DefaultCellValue = "0"
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.RowLayoutColumnInfo.OriginX = 0
        UltraGridColumn1.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn1.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(36, 0)
        UltraGridColumn1.RowLayoutColumnInfo.SpanX = 2
        UltraGridColumn1.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn1.Width = 8
        UltraGridColumn2.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled
        Appearance11.BackColorDisabled = System.Drawing.Color.Silver
        Appearance11.BorderColor = System.Drawing.Color.DimGray
        Appearance11.ForeColorDisabled = System.Drawing.Color.Black
        UltraGridColumn2.CellAppearance = Appearance11
        UltraGridColumn2.DefaultCellValue = "0"
        UltraGridColumn2.Header.VisiblePosition = 10
        UltraGridColumn2.Hidden = True
        UltraGridColumn2.NullText = "0"
        UltraGridColumn2.RowLayoutColumnInfo.OriginX = 60
        UltraGridColumn2.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn2.RowLayoutColumnInfo.SpanX = 2
        UltraGridColumn2.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn2.Width = 89
        UltraGridColumn3.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled
        Appearance12.BackColorDisabled = System.Drawing.Color.Silver
        Appearance12.BorderColor = System.Drawing.Color.DimGray
        Appearance12.ForeColorDisabled = System.Drawing.Color.Black
        UltraGridColumn3.CellAppearance = Appearance12
        UltraGridColumn3.DefaultCellValue = ""
        UltraGridColumn3.Header.Caption = "Item "
        UltraGridColumn3.Header.VisiblePosition = 1
        UltraGridColumn3.NullText = "0"
        UltraGridColumn3.RowLayoutColumnInfo.OriginX = 2
        UltraGridColumn3.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn3.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(292, 0)
        UltraGridColumn3.RowLayoutColumnInfo.SpanX = 5
        UltraGridColumn3.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn3.TabIndex = 1
        UltraGridColumn3.Width = 55
        UltraGridColumn4.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled
        Appearance13.BackColorDisabled = System.Drawing.Color.Silver
        Appearance13.BorderColor = System.Drawing.Color.DimGray
        Appearance13.ForeColorDisabled = System.Drawing.Color.Black
        UltraGridColumn4.CellAppearance = Appearance13
        UltraGridColumn4.DefaultCellValue = ""
        UltraGridColumn4.Header.Caption = "Item Description"
        UltraGridColumn4.Header.VisiblePosition = 2
        UltraGridColumn4.NullText = "-"
        UltraGridColumn4.RowLayoutColumnInfo.OriginX = 7
        UltraGridColumn4.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn4.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(47, 0)
        UltraGridColumn4.RowLayoutColumnInfo.SpanX = 4
        UltraGridColumn4.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn4.TabIndex = 2
        UltraGridColumn4.Width = 71
        UltraGridColumn5.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled
        Appearance14.BackColorDisabled = System.Drawing.Color.Silver
        Appearance14.BorderColor = System.Drawing.Color.DimGray
        Appearance14.ForeColorDisabled = System.Drawing.Color.Black
        UltraGridColumn5.CellAppearance = Appearance14
        UltraGridColumn5.DefaultCellValue = ""
        UltraGridColumn5.Header.Caption = "Description"
        UltraGridColumn5.Header.VisiblePosition = 3
        UltraGridColumn5.NullText = "-"
        UltraGridColumn5.RowLayoutColumnInfo.OriginX = 11
        UltraGridColumn5.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn5.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(75, 0)
        UltraGridColumn5.RowLayoutColumnInfo.SpanX = 5
        UltraGridColumn5.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn5.TabIndex = 3
        UltraGridColumn5.Width = 71
        UltraGridColumn6.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled
        Appearance15.BackColorDisabled = System.Drawing.Color.Silver
        Appearance15.BorderColor = System.Drawing.Color.DimGray
        Appearance15.ForeColorDisabled = System.Drawing.Color.Black
        Appearance15.TextHAlignAsString = "Right"
        UltraGridColumn6.CellAppearance = Appearance15
        UltraGridColumn6.Header.VisiblePosition = 5
        UltraGridColumn6.RowLayoutColumnInfo.OriginX = 24
        UltraGridColumn6.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn6.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(77, 0)
        UltraGridColumn6.RowLayoutColumnInfo.SpanX = 2
        UltraGridColumn6.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn6.TabIndex = 4
        UltraGridColumn7.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled
        Appearance16.BackColorDisabled = System.Drawing.Color.Silver
        Appearance16.BorderColor = System.Drawing.Color.DimGray
        Appearance16.ForeColorDisabled = System.Drawing.Color.Black
        Appearance16.TextHAlignAsString = "Right"
        UltraGridColumn7.CellAppearance = Appearance16
        UltraGridColumn7.Format = "0.00"
        UltraGridColumn7.Header.VisiblePosition = 15
        UltraGridColumn7.RowLayoutColumnInfo.OriginX = 23
        UltraGridColumn7.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn7.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(69, 0)
        UltraGridColumn7.RowLayoutColumnInfo.SpanX = 1
        UltraGridColumn7.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn7.TabIndex = 5
        UltraGridColumn8.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled
        Appearance17.BackColorDisabled = System.Drawing.Color.Silver
        Appearance17.BorderColor = System.Drawing.Color.DimGray
        Appearance17.ForeColorDisabled = System.Drawing.Color.Black
        UltraGridColumn8.CellAppearance = Appearance17
        UltraGridColumn8.DefaultCellValue = "0"
        UltraGridColumn8.Header.VisiblePosition = 4
        UltraGridColumn8.Hidden = True
        UltraGridColumn8.NullText = "0"
        UltraGridColumn8.RowLayoutColumnInfo.OriginX = 16
        UltraGridColumn8.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn8.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(60, 0)
        UltraGridColumn8.RowLayoutColumnInfo.SpanX = 7
        UltraGridColumn8.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn8.TabIndex = 6
        UltraGridColumn8.Width = 38
        UltraGridColumn9.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled
        Appearance18.BackColorDisabled = System.Drawing.Color.Silver
        Appearance18.BorderColor = System.Drawing.Color.DimGray
        Appearance18.ForeColorDisabled = System.Drawing.Color.Black
        UltraGridColumn9.CellAppearance = Appearance18
        UltraGridColumn9.Header.VisiblePosition = 6
        UltraGridColumn9.RowLayoutColumnInfo.OriginX = 27
        UltraGridColumn9.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn9.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(70, 0)
        UltraGridColumn9.RowLayoutColumnInfo.SpanX = 6
        UltraGridColumn9.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn9.TabIndex = 7
        UltraGridColumn9.Width = 18
        UltraGridColumn10.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled
        Appearance19.BackColorDisabled = System.Drawing.Color.Silver
        Appearance19.BorderColor = System.Drawing.Color.DimGray
        Appearance19.ForeColorDisabled = System.Drawing.Color.Black
        Appearance19.TextHAlignAsString = "Right"
        UltraGridColumn10.CellAppearance = Appearance19
        UltraGridColumn10.DefaultCellValue = "0"
        UltraGridColumn10.Format = "0.00"
        UltraGridColumn10.Header.Caption = "Price (Excl)"
        UltraGridColumn10.Header.VisiblePosition = 7
        UltraGridColumn10.Hidden = True
        UltraGridColumn10.NullText = "0"
        UltraGridColumn10.RowLayoutColumnInfo.OriginX = 34
        UltraGridColumn10.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn10.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(38, 0)
        UltraGridColumn10.RowLayoutColumnInfo.SpanX = 2
        UltraGridColumn10.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn10.TabIndex = 8
        UltraGridColumn10.Width = 45
        UltraGridColumn11.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled
        Appearance20.BackColorDisabled = System.Drawing.Color.Silver
        Appearance20.BorderColor = System.Drawing.Color.DimGray
        Appearance20.ForeColorDisabled = System.Drawing.Color.Black
        Appearance20.TextHAlignAsString = "Right"
        UltraGridColumn11.CellAppearance = Appearance20
        UltraGridColumn11.DefaultCellValue = "0"
        UltraGridColumn11.Format = "0.00"
        UltraGridColumn11.Header.Caption = "Price (Incl)"
        UltraGridColumn11.Header.VisiblePosition = 8
        UltraGridColumn11.Hidden = True
        UltraGridColumn11.NullText = "0"
        UltraGridColumn11.RowLayoutColumnInfo.OriginX = 36
        UltraGridColumn11.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn11.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(33, 0)
        UltraGridColumn11.RowLayoutColumnInfo.SpanX = 2
        UltraGridColumn11.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn11.TabIndex = 9
        UltraGridColumn11.Width = 98
        UltraGridColumn12.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled
        Appearance21.BackColorDisabled = System.Drawing.Color.Silver
        Appearance21.BorderColor = System.Drawing.Color.DimGray
        Appearance21.ForeColorDisabled = System.Drawing.Color.Black
        UltraGridColumn12.CellAppearance = Appearance21
        UltraGridColumn12.CellMultiLine = Infragistics.Win.DefaultableBoolean.[True]
        UltraGridColumn12.Header.VisiblePosition = 9
        UltraGridColumn12.RowLayoutColumnInfo.AllowCellSizing = Infragistics.Win.UltraWinGrid.RowLayoutSizing.Both
        UltraGridColumn12.RowLayoutColumnInfo.LabelPosition = Infragistics.Win.UltraWinGrid.LabelPosition.None
        UltraGridColumn12.RowLayoutColumnInfo.OriginX = 0
        UltraGridColumn12.RowLayoutColumnInfo.OriginY = 2
        UltraGridColumn12.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(666, 0)
        UltraGridColumn12.RowLayoutColumnInfo.SpanX = 60
        UltraGridColumn12.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn12.TabIndex = 10
        UltraGridColumn12.Width = 90
        UltraGridColumn13.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled
        Appearance22.BackColorDisabled = System.Drawing.Color.Silver
        Appearance22.BorderColor = System.Drawing.Color.DimGray
        Appearance22.ForeColorDisabled = System.Drawing.Color.Black
        UltraGridColumn13.CellAppearance = Appearance22
        UltraGridColumn13.Header.VisiblePosition = 11
        UltraGridColumn13.RowLayoutColumnInfo.OriginX = 10
        UltraGridColumn13.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn13.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(47, 0)
        UltraGridColumn13.RowLayoutColumnInfo.SpanX = 2
        UltraGridColumn13.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn13.TabIndex = 11
        UltraGridColumn14.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled
        Appearance23.BackColorDisabled = System.Drawing.Color.Silver
        Appearance23.BorderColor = System.Drawing.Color.DimGray
        Appearance23.ForeColorDisabled = System.Drawing.Color.Black
        UltraGridColumn14.CellAppearance = Appearance23
        UltraGridColumn14.Header.VisiblePosition = 12
        UltraGridColumn14.Hidden = True
        UltraGridColumn14.RowLayoutColumnInfo.OriginX = 14
        UltraGridColumn14.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn14.RowLayoutColumnInfo.SpanX = 2
        UltraGridColumn14.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn14.TabIndex = 12
        UltraGridColumn15.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled
        Appearance24.BackColorDisabled = System.Drawing.Color.Silver
        Appearance24.BorderColor = System.Drawing.Color.DimGray
        Appearance24.ForeColorDisabled = System.Drawing.Color.Black
        UltraGridColumn15.CellAppearance = Appearance24
        UltraGridColumn15.Header.VisiblePosition = 13
        UltraGridColumn15.Hidden = True
        UltraGridColumn15.RowLayoutColumnInfo.OriginX = 16
        UltraGridColumn15.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn15.RowLayoutColumnInfo.SpanX = 2
        UltraGridColumn15.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn15.TabIndex = 13
        UltraGridColumn16.ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always
        UltraGridColumn16.CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled
        Appearance25.BackColorDisabled = System.Drawing.Color.Silver
        Appearance25.BorderColor = System.Drawing.Color.DimGray
        Appearance25.ForeColorDisabled = System.Drawing.Color.Black
        UltraGridColumn16.CellAppearance = Appearance25
        UltraGridColumn16.Header.VisiblePosition = 14
        UltraGridColumn16.Hidden = True
        UltraGridColumn16.RowLayoutColumnInfo.OriginX = 38
        UltraGridColumn16.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn16.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(8, 0)
        UltraGridColumn16.RowLayoutColumnInfo.SpanX = 1
        UltraGridColumn16.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn16.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button
        UltraGridColumn16.TabIndex = 14
        UltraGridColumn17.Header.VisiblePosition = 16
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4, UltraGridColumn5, UltraGridColumn6, UltraGridColumn7, UltraGridColumn8, UltraGridColumn9, UltraGridColumn10, UltraGridColumn11, UltraGridColumn12, UltraGridColumn13, UltraGridColumn14, UltraGridColumn15, UltraGridColumn16, UltraGridColumn17})
        UltraGridBand1.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        UltraGridBand1.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.SeparateElement
        UltraGridBand1.UseRowLayout = True
        UltraGridColumn18.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        UltraGridColumn18.Header.VisiblePosition = 0
        UltraGridColumn18.Width = 268
        UltraGridColumn19.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        UltraGridColumn19.Header.VisiblePosition = 1
        UltraGridColumn19.Width = 292
        UltraGridColumn20.Header.VisiblePosition = 2
        UltraGridColumn20.Width = 87
        UltraGridBand2.Columns.AddRange(New Object() {UltraGridColumn18, UltraGridColumn19, UltraGridColumn20})
        Me.UG.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.UG.DisplayLayout.BandsSerializer.Add(UltraGridBand2)
        Me.UG.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UG.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.WithinGroup
        Me.UG.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free
        Me.UG.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle
        Me.UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton
        Me.UG.Location = New System.Drawing.Point(12, 94)
        Me.UG.Name = "UG"
        Me.UG.Size = New System.Drawing.Size(706, 409)
        Me.UG.TabIndex = 16
        Me.UG.TabStop = False
        '
        'UltraDataSource7
        '
        UltraDataBand5.Columns.AddRange(New Object() {UltraDataColumn80, UltraDataColumn81, UltraDataColumn82})
        Me.UltraDataSource7.Band.ChildBands.AddRange(New Object() {UltraDataBand5})
        UltraDataColumn83.DataType = GetType(Short)
        Me.UltraDataSource7.Band.Columns.AddRange(New Object() {UltraDataColumn83, UltraDataColumn84, UltraDataColumn85, UltraDataColumn86, UltraDataColumn87, UltraDataColumn88, UltraDataColumn89, UltraDataColumn90, UltraDataColumn91, UltraDataColumn92, UltraDataColumn93, UltraDataColumn94, UltraDataColumn95, UltraDataColumn96, UltraDataColumn97, UltraDataColumn98})
        Me.UltraDataSource7.UseBindingSource = True
        '
        'UltraDataSource6
        '
        UltraDataBand6.Columns.AddRange(New Object() {UltraDataColumn99, UltraDataColumn100})
        Me.UltraDataSource6.Band.ChildBands.AddRange(New Object() {UltraDataBand6})
        UltraDataColumn101.DataType = GetType(Short)
        Me.UltraDataSource6.Band.Columns.AddRange(New Object() {UltraDataColumn101, UltraDataColumn102, UltraDataColumn103, UltraDataColumn104, UltraDataColumn105, UltraDataColumn106, UltraDataColumn107, UltraDataColumn108, UltraDataColumn109, UltraDataColumn110, UltraDataColumn111, UltraDataColumn112, UltraDataColumn113, UltraDataColumn114, UltraDataColumn115, UltraDataColumn116})
        Me.UltraDataSource6.UseBindingSource = True
        '
        'UltraDataSource5
        '
        UltraDataBand7.Columns.AddRange(New Object() {UltraDataColumn117, UltraDataColumn118})
        Me.UltraDataSource5.Band.ChildBands.AddRange(New Object() {UltraDataBand7})
        Me.UltraDataSource5.Band.Columns.AddRange(New Object() {UltraDataColumn119, UltraDataColumn120, UltraDataColumn121, UltraDataColumn122, UltraDataColumn123, UltraDataColumn124, UltraDataColumn125, UltraDataColumn126, UltraDataColumn127, UltraDataColumn128, UltraDataColumn129, UltraDataColumn130, UltraDataColumn131, UltraDataColumn132, UltraDataColumn133, UltraDataColumn134})
        Me.UltraDataSource5.UseBindingSource = True
        '
        'DDWH
        '
        Appearance26.BorderColor = System.Drawing.Color.Silver
        Me.DDWH.DisplayLayout.Appearance = Appearance26
        Me.DDWH.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance27.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DDWH.DisplayLayout.Override.CellAppearance = Appearance27
        Appearance28.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DDWH.DisplayLayout.Override.RowAppearance = Appearance28
        Me.DDWH.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DDWH.Location = New System.Drawing.Point(249, 245)
        Me.DDWH.Name = "DDWH"
        Me.DDWH.Size = New System.Drawing.Size(333, 98)
        Me.DDWH.TabIndex = 32
        Me.DDWH.Text = "UltraDropDown1"
        Me.DDWH.Visible = False
        '
        'UltraLabel4
        '
        Appearance29.TextVAlignAsString = "Middle"
        Me.UltraLabel4.Appearance = Appearance29
        Me.UltraLabel4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel4.Location = New System.Drawing.Point(12, 42)
        Me.UltraLabel4.Name = "UltraLabel4"
        Me.UltraLabel4.Size = New System.Drawing.Size(67, 14)
        Me.UltraLabel4.TabIndex = 34
        Me.UltraLabel4.Text = "Bin Location"
        '
        'txtBin
        '
        Appearance30.TextHAlignAsString = "Center"
        Me.txtBin.Appearance = Appearance30
        Me.txtBin.AutoSize = False
        Me.txtBin.Enabled = False
        Me.txtBin.Location = New System.Drawing.Point(85, 38)
        Me.txtBin.Name = "txtBin"
        Me.txtBin.Size = New System.Drawing.Size(126, 22)
        Me.txtBin.TabIndex = 33
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(217, 38)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(81, 22)
        Me.Button1.TabIndex = 35
        Me.Button1.Text = "New Bin"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmStockCount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(730, 515)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.UltraLabel4)
        Me.Controls.Add(Me.txtBin)
        Me.Controls.Add(Me.DDWH)
        Me.Controls.Add(Me.UltraLabel3)
        Me.Controls.Add(Me.txtQty)
        Me.Controls.Add(Me.UltraLabel1)
        Me.Controls.Add(Me.UltraLabel2)
        Me.Controls.Add(Me.cmbInventoryItem)
        Me.Controls.Add(Me.DDDescription)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.txtBarCode)
        Me.Controls.Add(Me.UG)
        Me.Name = "frmStockCount"
        Me.Text = "Stock Count"
        CType(Me.UltraDataSource2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBarCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraDataSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.DDDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbInventoryItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraDataSource3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraDataSource4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraDataSource7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraDataSource6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraDataSource5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DDWH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtBarCode As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraDataSource1 As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents UltraDataSource2 As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbOpen As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbUpdateSO As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbProcessPO As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbDeleteLine As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbNewlIne As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSaveStock As System.Windows.Forms.ToolStripButton
    Friend WithEvents DDDescription As Infragistics.Win.UltraWinGrid.UltraDropDown
    Friend WithEvents cmbInventoryItem As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtQty As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraDataSource4 As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents UltraDataSource3 As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents UG As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents DDWH As Infragistics.Win.UltraWinGrid.UltraDropDown
    Friend WithEvents UltraDataSource5 As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents tsbUpdate As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraDataSource6 As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtBin As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents UltraDataSource7 As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
End Class
