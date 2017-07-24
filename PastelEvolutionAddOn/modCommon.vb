Imports System.Data
Imports System.Data.SqlClient
Module modCommon
    Public sConStr As String
    Public sSQLSrvName As String
    Public sSQLSrvUserName As String
    Public sSQLSrvPassword As String
    Public sSQLSrvDataBase As String
    Public sSQLSrvCommonDB As String
    Public sSQLSrvReportSrv As String
    Public sCompanyName As String
    Public comDocState As String
    Public sAgent As String
    Public iAgent As Integer
    Public iRepoID As Integer
    Public PONo As String
    Public CRNNo As String
    Public Lot As String
    Public Lot_Exp As Date
    Public LotCN As Boolean
    Public lots() As String


    Public SQL As String
    Public sSQL As String
    Public DS As DataSet
    Public DS3 As DataSet
    Public dsManu As DataSet

    Public Con1 As New SqlConnection
    Public Con2 As New SqlConnection

    Public Trans1 As SqlTransaction
    Public Trans2 As SqlTransaction
    Public CMD As SqlCommand
    Public DA As SqlDataAdapter
    Public sqlDR As SqlDataReader
    Public Enum DocType As Integer
        Invoice = 10
        CreditNote = 1
        GRV = 2
        RTN = 3
        SalesOrder = 4
        PurchaseOrder = 5
        POSInv = 6
        PODCrn = 7
        IBTReceive = 8
        Open = 0
        SupplierInvoice = 9
        ReOrder = 11
    End Enum
    Public Enum DocState As Integer
        Unkown = 0
        Unprocessed = 1
        Quote = 2
        PartialProcessed = 3
        Archived = 4
        Template = 5
        CancelledOrder = 7
    End Enum
    Public Enum DocFlag As Integer
        NotGrvOrSupplierInvoice = 0
        GoodsReceivedVoucher = 1
        SupplierInvoice = 2
    End Enum
End Module
