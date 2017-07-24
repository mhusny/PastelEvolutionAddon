Imports System.Data
Imports System.Data.SqlClient
Public Class clsGRV
    Inherits clsSqlConn

#Region "Property 'DocID'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intDocID As Integer
    ''' <summary>
    '''     TODO: Document ID  Same As Goods Receive Note No .
    ''' </summary>
    Public Property DocID() As Integer
        Get
            Return m_intDocID
        End Get
        Set(ByVal Value As Integer)
            m_intDocID = Value
        End Set
    End Property

#End Region 'Property 'DocID'
#Region "Property 'DocType'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intDocType As Integer
    ''' <summary>
    '''     TODO: Document Type 0-Invoice,1-Credit Note,2-GRV,3-RTNS,4-Sales Order,.
    ''' </summary>
    Public Property DocType() As Integer
        Get
            Return m_intDocType
        End Get
        Set(ByVal Value As Integer)
            m_intDocType = Value
        End Set
    End Property

#End Region 'Property 'DocType'
#Region "Property 'DocStatus'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intDocStatus As Integer
    ''' <summary>
    '''     TODO: Document Status.
    ''' </summary>
    Public Property DocStatus() As Integer
        Get
            Return m_intDocStatus
        End Get
        Set(ByVal Value As Integer)
            m_intDocStatus = Value
        End Set
    End Property

#End Region 'Property 'DocStatus'
#Region "Property 'DocVersion'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intDocVersion As Integer
    ''' <summary>
    '''     TODO: Document Version.
    ''' </summary>
    Public Property DocVersion() As Integer
        Get
            Return m_intDocVersion
        End Get
        Set(ByVal Value As Integer)
            m_intDocVersion = Value
        End Set
    End Property

#End Region 'Property 'DocVersion'
#Region "Property 'DocState'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intDocState As Integer
    ''' <summary>
    '''     TODO: Document State 1-Unprocessed,3-Partial Processed,4-Archived.
    ''' </summary>
    Public Property DocState() As Integer
        Get
            Return m_intDocState
        End Get
        Set(ByVal Value As Integer)
            m_intDocState = Value
        End Set
    End Property

#End Region 'Property 'DocState'
#Region "Property 'DocFlag'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intDocFlag As Integer
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property DocFlag() As Integer
        Get
            Return m_intDocFlag
        End Get
        Set(ByVal Value As Integer)
            m_intDocFlag = Value
        End Set
    End Property

#End Region 'Property 'DocFlag'
#Region "Property 'OriginalDocID'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intOriginalDocID As Integer
    ''' <summary>
    '''     TODO: Original Document ID ---Same As GrvID.
    ''' </summary>
    Public Property OriginalDocID() As Integer
        Get
            Return m_intOriginalDocID
        End Get
        Set(ByVal Value As Integer)
            m_intOriginalDocID = Value
        End Set
    End Property

#End Region 'Property 'OriginalDocID'
#Region "Property 'AccountID'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intAccountID As Integer
    ''' <summary>
    '''     TODO: Supplier Name .
    ''' </summary>
    Public Property AccountID() As Integer
        Get
            Return m_intAccountID
        End Get
        Set(ByVal Value As Integer)
            m_intAccountID = Value
        End Set
    End Property

#End Region 'Property 'AccountID'
#Region "Property 'ProspectID'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intProspectID As Integer
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property ProspectID() As Integer
        Get
            Return m_intProspectID
        End Get
        Set(ByVal Value As Integer)
            m_intProspectID = Value
        End Set
    End Property

#End Region 'Property 'ProspectID'
#Region "Property 'IsProspect'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_bIsProspect As Boolean
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property IsProspect() As Boolean
        Get
            Return m_bIsProspect
        End Get
        Set(ByVal Value As Boolean)
            m_bIsProspect = Value
        End Set
    End Property

#End Region 'Property 'IsProspect'
#Region "Property 'OrderNum'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_strOrderNum As String
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property OrderNum() As String
        Get
            Return m_strOrderNum
        End Get
        Set(ByVal Value As String)
            m_strOrderNum = Value
        End Set
    End Property

#End Region 'Property 'OrderNum'
#Region "Property 'OrderDate'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_datOrderDate As Date
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property OrderDate() As Date
        Get
            Return m_datOrderDate
        End Get
        Set(ByVal Value As Date)
            m_datOrderDate = Value
        End Set
    End Property

#End Region 'Property 'OrderDate'
#Region "Property 'InvoiceNumber'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_strInvoiceNumber As String = ""
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property InvoiceNumber() As String
        Get
            Return m_strInvoiceNumber
        End Get
        Set(ByVal Value As String)
            m_strInvoiceNumber = Value
        End Set
    End Property

#End Region 'Property 'InvoiceNumber'
#Region "Property 'InvoiceDate'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_datInvoiceDate As Date
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property InvoiceDate() As Date
        Get
            Return m_datInvoiceDate
        End Get
        Set(ByVal Value As Date)
            m_datInvoiceDate = Value
        End Set
    End Property

#End Region 'Property 'InvoiceDate'
#Region "Property 'DueDate'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_datDueDate As Date
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property DueDate() As Date
        Get
            Return m_datDueDate
        End Get
        Set(ByVal Value As Date)
            m_datDueDate = Value
        End Set
    End Property

#End Region 'Property 'DueDate'
#Region "Property 'DeliveryDate'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_datDeliveryDate As Date
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property DeliveryDate() As Date
        Get
            Return m_datDeliveryDate
        End Get
        Set(ByVal Value As Date)
            m_datDeliveryDate = Value
        End Set
    End Property

#End Region 'Property 'DeliveryDate'
#Region "Property 'DeliveryAddress1'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_strDeliveryAddress1 As String
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property DeliveryAddress1() As String
        Get
            Return m_strDeliveryAddress1
        End Get
        Set(ByVal Value As String)
            m_strDeliveryAddress1 = Value
        End Set
    End Property

#End Region 'Property 'DeliveryAddress1'
#Region "Property 'DeliveryAddress2'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_strDeliveryAddress2 As String
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property DeliveryAddress2() As String
        Get
            Return m_strDeliveryAddress2
        End Get
        Set(ByVal Value As String)
            m_strDeliveryAddress2 = Value
        End Set
    End Property

#End Region 'Property 'DeliveryAddress2'
#Region "Property 'DeliveryAddress3'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_strDeliveryAddress3 As String
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property DeliveryAddress3() As String
        Get
            Return m_strDeliveryAddress3
        End Get
        Set(ByVal Value As String)
            m_strDeliveryAddress3 = Value
        End Set
    End Property

#End Region 'Property 'DeliveryAddress3'
#Region "Property 'DeliveryAddress4'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_strDeliveryAddress4 As String
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property DeliveryAddress4() As String
        Get
            Return m_strDeliveryAddress4
        End Get
        Set(ByVal Value As String)
            m_strDeliveryAddress4 = Value
        End Set
    End Property

#End Region 'Property 'DeliveryAddress4'
#Region "Property 'DeliveryAddress5'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_strDeliveryAddress5 As String
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property DeliveryAddress5() As String
        Get
            Return m_strDeliveryAddress5
        End Get
        Set(ByVal Value As String)
            m_strDeliveryAddress5 = Value
        End Set
    End Property

#End Region 'Property 'DeliveryAddress5'
#Region "Property 'PhysicalAddress1'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_strPhysicalAddress1 As String
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property PhysicalAddress1() As String
        Get
            Return m_strPhysicalAddress1
        End Get
        Set(ByVal Value As String)
            m_strPhysicalAddress1 = Value
        End Set
    End Property

#End Region 'Property 'PhysicalAddress1'
#Region "Property 'PhysicalAddress2'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_strPhysicalAddress2 As String
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property PhysicalAddress2() As String
        Get
            Return m_strPhysicalAddress2
        End Get
        Set(ByVal Value As String)
            m_strPhysicalAddress2 = Value
        End Set
    End Property

#End Region 'Property 'PhysicalAddress2'
#Region "Property 'PhysicalAddress3'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_strPhysicalAddress3 As String
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property PhysicalAddress3() As String
        Get
            Return m_strPhysicalAddress3
        End Get
        Set(ByVal Value As String)
            m_strPhysicalAddress3 = Value
        End Set
    End Property

#End Region 'Property 'PhysicalAddress3'
#Region "Property 'PhysicalAddress4'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_strPhysicalAddress4 As String
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property PhysicalAddress4() As String
        Get
            Return m_strPhysicalAddress4
        End Get
        Set(ByVal Value As String)
            m_strPhysicalAddress4 = Value
        End Set
    End Property

#End Region 'Property 'PhysicalAddress4'
#Region "Property 'PhysicalAddress5'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_strPhysicalAddress5 As String
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property PhysicalAddress5() As String
        Get
            Return m_strPhysicalAddress5
        End Get
        Set(ByVal Value As String)
            m_strPhysicalAddress5 = Value
        End Set
    End Property

#End Region 'Property 'PhysicalAddress5'
#Region "Property 'DeliveryPostelCode'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_strDeliveryPostelCode As String
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property DeliveryPostelCode() As String
        Get
            Return m_strDeliveryPostelCode
        End Get
        Set(ByVal Value As String)
            m_strDeliveryPostelCode = Value
        End Set
    End Property

#End Region 'Property 'DeliveryPostelCode'
#Region "Property 'PhysicalPostelCode'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_strPhysicalPostelCode As String
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property PhysicalPostelCode() As String
        Get
            Return m_strPhysicalPostelCode
        End Get
        Set(ByVal Value As String)
            m_strPhysicalPostelCode = Value
        End Set
    End Property

#End Region 'Property 'PhysicalPostelCode'
#Region "Property 'DeliveryMethod'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_strDeliveryMethod As String = ""
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property DeliveryMethod() As String
        Get
            Return m_strDeliveryMethod
        End Get
        Set(ByVal Value As String)
            m_strDeliveryMethod = Value
        End Set
    End Property

#End Region 'Property 'DeliveryMethod'
#Region "Property 'SalesRep'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intSalesRep As Integer
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property SalesRep() As Integer
        Get
            Return m_intSalesRep
        End Get
        Set(ByVal Value As Integer)
            m_intSalesRep = Value
        End Set
    End Property

#End Region 'Property 'SalesRep'
#Region "Property 'Comment'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_strComment As String = ""
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property Comment() As String
        Get
            Return m_strComment
        End Get
        Set(ByVal Value As String)
            m_strComment = Value
        End Set
    End Property

#End Region 'Property 'Comment'
#Region "Property 'ProjectID'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intProjectID As Integer
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property ProjectID() As Integer
        Get
            Return m_intProjectID
        End Get
        Set(ByVal Value As Integer)
            m_intProjectID = Value
        End Set
    End Property

#End Region 'Property 'ProjectID'
#Region "Property 'WareHouseID'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intWareHouseID As Integer = 0
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property WareHouseID() As Integer
        Get
            Return m_intWareHouseID
        End Get
        Set(ByVal Value As Integer)
            m_intWareHouseID = Value
        End Set
    End Property

#End Region 'Property 'WareHouseID'
#Region "Property 'OrderPriorityID'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intOrderPriorityID As Integer = 0
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property OrderPriorityID() As Integer
        Get
            Return m_intOrderPriorityID
        End Get
        Set(ByVal Value As Integer)
            m_intOrderPriorityID = Value
        End Set
    End Property

#End Region 'Property 'OrderPriorityID'
#Region "Property 'ExtOrderNum'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_strExtOrderNum As String
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property ExtOrderNum() As String
        Get
            Return m_strExtOrderNum
        End Get
        Set(ByVal Value As String)
            m_strExtOrderNum = Value
        End Set
    End Property

#End Region 'Property 'ExtOrderNum'
#Region "Property 'DiscountPercent'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decDiscountPercent As Decimal
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property DiscountPercent() As Decimal
        Get
            Return m_decDiscountPercent
        End Get
        Set(ByVal Value As Decimal)
            m_decDiscountPercent = Value
        End Set
    End Property

#End Region 'Property 'DiscountPercent'
#Region "Property 'DiscountAmount'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decDiscountAmount As Decimal
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property DiscountAmount() As Decimal
        Get
            Return m_decDiscountAmount
        End Get
        Set(ByVal Value As Decimal)
            m_decDiscountAmount = Value
        End Set
    End Property

#End Region 'Property 'DiscountAmount'
#Region "Property 'TotalExcl'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decTotalExcl As Decimal
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property TotalExcl() As Decimal
        Get
            Return m_decTotalExcl
        End Get
        Set(ByVal Value As Decimal)
            m_decTotalExcl = Value
        End Set
    End Property

#End Region 'Property 'TotalExcl'
#Region "Property 'TotalTax'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decTotalTax As Decimal
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property TotalTax() As Decimal
        Get
            Return m_decTotalTax
        End Get
        Set(ByVal Value As Decimal)
            m_decTotalTax = Value
        End Set
    End Property

#End Region 'Property 'TotalTax'
#Region "Property 'TotalIncl'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decTotalIncl As Decimal
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property TotalIncl() As Decimal
        Get
            Return m_decTotalIncl
        End Get
        Set(ByVal Value As Decimal)
            m_decTotalIncl = Value
        End Set
    End Property

#End Region 'Property 'TotalIncl'
#Region "Property 'ItemType'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intItemType As Integer
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property ItemType() As Integer
        Get
            Return m_intItemType
        End Get
        Set(ByVal Value As Integer)
            m_intItemType = Value
        End Set
    End Property

#End Region 'Property 'ItemType'
#Region "Property 'LineID'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intLineID As Integer
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property LineID() As Integer
        Get
            Return m_intLineID
        End Get
        Set(ByVal Value As Integer)
            m_intLineID = Value
        End Set
    End Property

#End Region 'Property 'LineID'
#Region "Property 'LinkLineID'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intLinkLineID As Integer
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property LinkLineID() As Integer
        Get
            Return m_intLinkLineID
        End Get
        Set(ByVal Value As Integer)
            m_intLinkLineID = Value
        End Set
    End Property

#End Region 'Property 'LinkLineID'
#Region "Property 'OriginalLineID'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intOriginalLineID As Integer
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property OriginalLineID() As Integer
        Get
            Return m_intOriginalLineID
        End Get
        Set(ByVal Value As Integer)
            m_intOriginalLineID = Value
        End Set
    End Property

#End Region 'Property 'OriginalLineID'
#Region "Property 'StockLinkID'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intStockLinkID As Integer
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property StockLinkID() As Integer
        Get
            Return m_intStockLinkID
        End Get
        Set(ByVal Value As Integer)
            m_intStockLinkID = Value
        End Set
    End Property

#End Region 'Property 'StockLinkID'
#Region "Property 'SubStockLinkID'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intSubStockLinkID As Integer = 0
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property SubStockLinkID() As Integer
        Get
            Return m_intSubStockLinkID
        End Get
        Set(ByVal Value As Integer)
            m_intSubStockLinkID = Value
        End Set
    End Property

#End Region 'Property 'SubStockLinkID'
#Region "Property 'Hight'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decHight As Decimal
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property Hight() As Decimal
        Get
            Return m_decHight
        End Get
        Set(ByVal Value As Decimal)
            m_decHight = Value
        End Set
    End Property

#End Region 'Property 'Hight'
#Region "Property 'Width'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decWidth As Decimal
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property Width() As Decimal
        Get
            Return m_decWidth
        End Get
        Set(ByVal Value As Decimal)
            m_decWidth = Value
        End Set
    End Property

#End Region 'Property 'Width'
#Region "Property 'Volume'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decVolume As Decimal
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property Volume() As Decimal
        Get
            Return m_decVolume
        End Get
        Set(ByVal Value As Decimal)
            m_decVolume = Value
        End Set
    End Property

#End Region 'Property 'Volume'
#Region "Property 'Thickness'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decThickness As Decimal
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property Thickness() As Decimal
        Get
            Return m_decThickness
        End Get
        Set(ByVal Value As Decimal)
            m_decThickness = Value
        End Set
    End Property

#End Region 'Property 'Thickness'
#Region "Property 'Toughened'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_bToughened As Boolean
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property Toughened() As Boolean
        Get
            Return m_bToughened
        End Get
        Set(ByVal Value As Boolean)
            m_bToughened = Value
        End Set
    End Property

#End Region 'Property 'Toughened'
#Region "Property 'Description'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_strDescription As String
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property Description() As String
        Get
            Return m_strDescription
        End Get
        Set(ByVal Value As String)
            m_strDescription = Value
        End Set
    End Property

#End Region 'Property 'Description'
#Region "Property 'Quantity'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decQuantity As Decimal
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property Quantity() As Decimal
        Get
            Return m_decQuantity
        End Get
        Set(ByVal Value As Decimal)
            m_decQuantity = Value
        End Set
    End Property

#End Region 'Property 'Quantity'
#Region "Property 'QuantityBalance'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decQuantityBalance As Decimal = 0
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property QuantityBalance() As Decimal
        Get
            Return m_decQuantityBalance
        End Get
        Set(ByVal Value As Decimal)
            m_decQuantityBalance = Value
        End Set
    End Property

#End Region 'Property 'QuantityBalance'
#Region "Property 'QuantityReceive'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decQuantityReceive As Decimal = 0
    ''' <summary>
    '''     Get/Set Quantity Receive
    ''' </summary>
    Public Property QuantityReceive() As Decimal
        Get
            Return m_decQuantityReceive
        End Get
        Set(ByVal Value As Decimal)
            m_decQuantityReceive = Value
        End Set
    End Property

#End Region 'Property 'QuantityReceive'
#Region "Property 'UnitPrice'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decUnitPrice As Decimal
    ''' <summary>
    '''     Get/Set Unit Price
    ''' </summary>
    Public Property UnitPrice() As Decimal
        Get
            Return m_decUnitPrice
        End Get
        Set(ByVal Value As Decimal)
            m_decUnitPrice = Value
        End Set
    End Property

#End Region 'Property 'UnitPrice'
#Region "Property 'UnitCost'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decUnitCost As Decimal
    ''' <summary>
    '''     Get/Set Unit Cost
    ''' </summary>
    Public Property UnitCost() As Decimal
        Get
            Return m_decUnitCost
        End Get
        Set(ByVal Value As Decimal)
            m_decUnitCost = Value
        End Set
    End Property

#End Region 'Property 'UnitCost'
#Region "Property 'TaxRate'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decTaxRate As Decimal
    ''' <summary>
    '''     Get/Set Tax Rate
    ''' </summary>
    Public Property TaxRate() As Decimal
        Get
            Return m_decTaxRate
        End Get
        Set(ByVal Value As Decimal)
            m_decTaxRate = Value
        End Set
    End Property

#End Region 'Property 'TaxRate'
#Region "Property 'LineDiscountPercen'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decLineDiscountPercen As Decimal
    ''' <summary>
    '''     Get/Set Line Discount (%)
    ''' </summary>
    Public Property LineDiscountPercen() As Decimal
        Get
            Return m_decLineDiscountPercen
        End Get
        Set(ByVal Value As Decimal)
            m_decLineDiscountPercen = Value
        End Set
    End Property

#End Region 'Property 'LineDiscountPercen'
#Region "Property 'LineDiscountAmount'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decLineDiscountAmount As Decimal
    ''' <summary>
    '''     Get/Set Line Discount Amount
    ''' </summary>
    Public Property LineDiscountAmount() As Decimal
        Get
            Return m_decLineDiscountAmount
        End Get
        Set(ByVal Value As Decimal)
            m_decLineDiscountAmount = Value
        End Set
    End Property

#End Region 'Property 'LineDiscountAmount'
#Region "Property 'LineTotal'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decLineTotal As Decimal
    ''' <summary>
    '''     Get/Set Line Total
    ''' </summary>
    Public Property LineTotal() As Decimal
        Get
            Return m_decLineTotal
        End Get
        Set(ByVal Value As Decimal)
            m_decLineTotal = Value
        End Set
    End Property

#End Region 'Property 'LineTotal'
#Region "Property 'LineTax'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decLineTax As Decimal
    ''' <summary>
    '''    Get/Set Line Tax Amount.
    ''' </summary>
    Public Property LineTax() As Decimal
        Get
            Return m_decLineTax
        End Get
        Set(ByVal Value As Decimal)
            m_decLineTax = Value
        End Set
    End Property

#End Region 'Property 'LineTax'
#Region "Property 'LineNet'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decLineNet As Decimal
    ''' <summary>
    '''     Get/Set Line Tax Amount
    ''' </summary>
    Public Property LineNet() As Decimal
        Get
            Return m_decLineNet
        End Get
        Set(ByVal Value As Decimal)
            m_decLineNet = Value
        End Set
    End Property

#End Region 'Property 'LineNet'
#Region "Property 'IsWarehouseItem'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_bIsWarehouseItem As Boolean
    ''' <summary>
    '''     Get/Set Is Warehouse Item
    ''' </summary>
    Public Property IsWarehouseItem() As Boolean
        Get
            Return m_bIsWarehouseItem
        End Get
        Set(ByVal Value As Boolean)
            m_bIsWarehouseItem = Value
        End Set
    End Property

#End Region 'Property 'IsWarehouseItem'
#Region "Property 'BinID'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intBinID As Integer=0
    ''' <summary>
    '''     Get/Set Bin Id
    ''' </summary>
    Public Property BinID() As Integer
        Get
            Return m_intBinID
        End Get
        Set(ByVal Value As Integer)
            m_intBinID = Value
        End Set
    End Property

#End Region 'Property 'BinID'
#Region "Property 'PackID'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intPackID As Integer = 0
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property PackID() As Integer
        Get
            Return m_intPackID
        End Get
        Set(ByVal Value As Integer)
            m_intPackID = Value
        End Set
    End Property

#End Region 'Property 'PackID'
#Region "Property 'NoOfPack'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decNoOfPack As Decimal = 0
    ''' <summary>
    '''     Get/Set Pack Id
    ''' </summary>
    Public Property NoOfPack() As Decimal
        Get
            Return m_decNoOfPack
        End Get
        Set(ByVal Value As Decimal)
            m_decNoOfPack = Value
        End Set
    End Property

#End Region 'Property 'NoOfPack'
#Region "Property 'PackQty'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decPackQty As Decimal = 0
    ''' <summary>
    '''     Get/Set Pack Quantity
    ''' </summary>
    Public Property PackQty() As Decimal
        Get
            Return m_decPackQty
        End Get
        Set(ByVal Value As Decimal)
            m_decPackQty = Value
        End Set
    End Property

#End Region 'Property 'PackQty'
#Region "Property 'AgentID'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_intAgentID As Integer = 0
    ''' <summary>
    '''     Get/Set Agent ID.
    ''' </summary>
    Public Property AgentID() As Integer
        Get
            Return m_intAgentID
        End Get
        Set(ByVal Value As Integer)
            m_intAgentID = Value
        End Set
    End Property

#End Region 'Property 'AgentID'
#Region "Property 'AgentName'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_strAgentName As String = " "
    ''' <summary>
    '''     Get/Set Agent Name.
    ''' </summary>
    Public Property AgentName() As String
        Get
            Return m_strAgentName
        End Get
        Set(ByVal Value As String)
            m_strAgentName = Value
        End Set
    End Property

#End Region 'Property 'AgentName'
#Region "Property 'IsApproved'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_bIsApproved As Boolean = 0
    ''' <summary>
    '''     Get/Set Is Approved
    ''' </summary>
    Public Property IsApproved() As Boolean
        Get
            Return m_bIsApproved
        End Get
        Set(ByVal Value As Boolean)
            m_bIsApproved = Value
        End Set
    End Property

#End Region 'Property 'IsApproved'
#Region "Property 'LineNote'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_strLineNote As String = """"
    ''' <summary>
    '''     Get/Set Line Note.
    ''' </summary>
    Public Property LineNote() As String
        Get
            Return m_strLineNote
        End Get
        Set(ByVal Value As String)
            m_strLineNote = Value
        End Set
    End Property

#End Region 'Property 'LineNote'
#Region "Property 'OriginalPrice'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decOriginalPrice As Decimal = 0
    ''' <summary>
    '''     Get/Set Original Price.
    ''' </summary>
    Public Property OriginalPrice() As Decimal
        Get
            Return m_decOriginalPrice
        End Get
        Set(ByVal Value As Decimal)
            m_decOriginalPrice = Value
        End Set
    End Property

#End Region 'Property 'OriginalPrice'
#Region "Property 'StandardCost'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_decStandardCost As Decimal = 0
    ''' <summary>
    '''     TODO: Describe this property here.
    ''' </summary>
    Public Property StandardCost() As Decimal
        Get
            Return m_decStandardCost
        End Get
        Set(ByVal Value As Decimal)
            m_decStandardCost = Value
        End Set
    End Property

#End Region 'Property 'StandardCost'
#Region "Method 'Saving Goods Receive Note Header'"
    ''' <summary>
    '''     This Methos Is Used To Save Goods Receive Note Header,Method Return Value 0- Not Success , 1- Success 
    ''' </summary>
    Public Function SaveGrvHeader() As Integer
        Try
            If (m_bIsBatchProcess = False And m_bIsDirectGrv = True) Or m_bIsBatchProcess = True Then
                m_intOriginalDocID = 0
            End If
           
            If m_bIsNew = False Then
                SQL = "Delete from spilPOHeader where iDocID = " & m_intDocID & "' AND iDocTypeID=" & m_intDocType & ""
                CMD = New SqlCommand(SQL, Con, Trans)
                CMD.CommandType = CommandType.Text
                CMD.ExecuteNonQuery()
            End If
            SQL = "INSERT INTO spilPOHeader (iDocID, iDocTypeID, iDocState, iOrigDocID,AccountID, " & _
            "  OrderNum , OrderDate,DeliveryDate,DueDate,InvNumber,  ExtOrderNum , " & _
            " Address1,Address2,Address3,Address4,Address5,  iAgentID , OrderPriorityID , " & _
            " DelMethod,Comment,PostAdd1,PostAdd2,PostAdd3,PostAdd4,PostAdd5, " & _
            " TotExcl,TotTax, TotIncl,AgentID,InvDate, Approved)  " & _
            " VALUES (" & m_intDocID & " ," & m_intDocType & "," & m_intDocState & "," & m_intOriginalDocID & "," & m_intAccountID & ",'" & m_strOrderNum & "', " & _
            "'" & Format(m_datOrderDate, "MM/dd/yyyy") & "','" & Format(m_datDeliveryDate, "MM/dd/yyyy") & "','" & Format(m_datDueDate, "MM/dd/yyyy") & "','" & m_strInvoiceNumber & "', " & _
            "'" & m_strExtOrderNum & "','" & m_strDeliveryAddress1 & "','" & m_strDeliveryAddress2 & "', " & _
            "'" & m_strDeliveryAddress3 & "','" & m_strDeliveryAddress4 & "','" & m_strDeliveryAddress5 & "'," & m_intAgentID & ", " & _
            "'" & m_intOrderPriorityID & "',' " & m_strDeliveryMethod & " ','" & m_strComment & "','" & m_strPhysicalAddress1 & "', " & _
            "'" & m_strPhysicalAddress2 & "','" & m_strPhysicalAddress3 & "','" & m_strPhysicalAddress4 & "','" & m_strPhysicalAddress5 & "'," & _
            " " & m_decTotalExcl & "," & m_decTotalTax & "" & _
            " ," & m_decTotalIncl & ",'" & m_strAgentName & "','" & Format(m_datInvoiceDate, "MM/dd/yyyy") & "'," & IIf(m_bIsApproved = True, 1, 0) & ")"
            CMD = New SqlCommand(SQL, Con, Trans)
            CMD.CommandType = CommandType.Text
            CMD.ExecuteNonQuery()
            Return 1
        Catch ex As Exception
            Return 0
        End Try
    End Function
#End Region
#Region "Methos 'Saving Goods Receive Note Details'"
    ''' <summary>
    '''  This Methos Is Used To Save Goods Receive Note Details,Method Return Value 0- Not Success , 1- Success 
    ''' </summary>
    Public Function SaveGrvDetail() As Integer
        Try
            If m_intItemType = 1 Then 'Check For Item Type 1-Glass,2-Other
                m_intSubStockLinkID = GetStockSubLink()
            Else
                m_intSubStockLinkID = 0
            End If
            If m_bIsNew = False Then
                SQL = "Delete from spilPOLines where iDocID = " & m_intDocID & "' AND iDocTypeID=" & m_intDocType & ""
                CMD = New SqlCommand(SQL, Con, Trans)
                CMD.CommandType = CommandType.Text
                CMD.ExecuteNonQuery()
            End If
            If m_bIsDirectGrv = False And (m_bIsBatchProcess = False Or m_bIsBatchProcess = True) Then
                SQL = "UPDATE spilPOLines SET QtyReceived =" & m_decQuantityReceive & ", QtyBalance =" & m_decQuantityBalance & " WHERE    idInvoiceLines =" & m_intOriginalLineID & ""
                CMD = New SqlCommand(SQL, Con, Trans)
                CMD.CommandType = CommandType.Text
                CMD.ExecuteNonQuery()
            End If
            SQL = "INSERT INTO spilPOLines " & _
            "(iDocID,iDocTypeID, iLineID,ItemType,iOrigLineID,  iStockCodeID, iSubStockCodeID , cDescription, " & _
            " Quantity,iHeight,iWidth, " & _
            " fVolume,fThickness, bToughened, " & _
            " UnitPrice,fUnitCost,fTaxRate,fItem_Net, " & _
            " fItem_tax, fItem_Gross, cLineNotes , " & _
            " fOriginal_Price,fDiscount_Amount,fSTD_COST,fDiscount_Percen, bIsWhseItem, ibin, iWarehouseID,idPckTbl,fNoOfPck,fPckQty) " & _
            " VALUES (" & m_intDocID & "," & m_intDocType & "," & m_intLineID & "," & m_intItemType & "," & m_intOriginalLineID & "," & m_intStockLinkID & "," & m_intSubStockLinkID & ", '" & m_strDescription & "'," & _
            "" & m_decQuantity & "," & m_decHight & "," & m_decWidth & "," & _
            "" & m_decVolume & ", " & m_decThickness & "," & IIf(m_bToughened = True, 1, 0) & "," & _
            "" & m_decUnitPrice & "," & m_decUnitCost & ", " & m_decTaxRate & "," & m_decLineNet & ", " & _
            "" & m_decLineTax & "," & m_decLineTotal & ",'" & m_strLineNote & "' ," & _
            "" & m_decOriginalPrice & "," & m_decDiscountAmount & "," & m_decStandardCost & "," & m_decDiscountPercent & "," & IIf(m_bIsWarehouseItem = True, 1, 0) & ", " & m_intBinID & ", " & m_intWareHouseID & "," & m_intPackID & "," & m_decNoOfPack & "," & m_decPackQty & ")"
            CMD = New SqlCommand(SQL, Con, Trans)
            CMD.CommandType = CommandType.Text
            CMD.ExecuteNonQuery()
            Return 1
        Catch ex As Exception
            Return 0
        End Try
    End Function
#End Region
#Region "Methos 'Get Sub Stock Link ID'"
    ''' <summary>
    ''' This Method is used to get link Sub Item Id
    ''' </summary>
    Public Function GetStockSubLink() As Integer
        Try
            Dim iSubStockLink As Integer
            SQL = " Select Stock_Sub_Link FROM spilStkSubItem " & _
            " WHERE (Height = " & m_decHight & ") AND (Width = " & m_decWidth & ") AND (StockLink = " & m_intStockLinkID & ")"
            CMD = New SqlCommand(SQL, Con, Trans)
            CMD.CommandType = CommandType.Text
            iSubStockLink = CMD.ExecuteScalar
            Return iSubStockLink
        Catch ex As Exception
            Return 0
        End Try
    End Function
#End Region
    
#Region "Property 'IsBatchProcess'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_bIsBatchProcess As Boolean = False

    ''' <summary>
    '''     Get/Set Is Batch Process If Ture "Multiple PO Selected" False "Only One PO Selected"
    ''' </summary>
    Public Property IsBatchProcess() As Boolean
        Get
            Return m_bIsBatchProcess
        End Get
        Set(ByVal Value As Boolean)
            m_bIsBatchProcess = Value
        End Set
    End Property

#End Region 'Property 'IsBatchProcess'
#Region "Property 'IsDirectGrv'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_bIsDirectGrv As Boolean = False
    ''' <summary>
    '''     Get/Set Is Direct Grv Or Not
    ''' </summary>
    Public Property IsDirectGrv() As Boolean
        Get
            Return m_bIsDirectGrv
        End Get
        Set(ByVal Value As Boolean)
            m_bIsDirectGrv = Value
        End Set
    End Property

#End Region 'Property 'IsDirectGrv'
#Region "Property 'IsSave'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_bIsSave As Boolean = False
    ''' <summary>
    '''     Get/Set Is Save For Generate New Goods Receive Note.
    ''' </summary>
    Public Property IsSave() As Boolean
        Get
            Return m_bIsSave
        End Get
        Set(ByVal Value As Boolean)
            m_bIsSave = Value
        End Set
    End Property

#End Region 'Property 'IsSave'
#Region "Property 'IsNew'"
    ''' <summary>
    '''     TODO: Describe this variable here.
    ''' </summary>
    Private m_bIsNew As Boolean = False
    ''' <summary>
    '''     Get/Set Is New Goods Receive Note
    ''' </summary>
    Public Property IsNew() As Boolean
        Get
            Return m_bIsNew
        End Get
        Set(ByVal Value As Boolean)
            m_bIsNew = Value
        End Set
    End Property

#End Region 'Property 'IsNew'
End Class
