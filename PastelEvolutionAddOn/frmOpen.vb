Imports Infragistics.Win.UltraWinGrid
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports Pastel.Evolution
Imports System.Drawing.Printing
Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Public Class frmOpen
    Public iDocType As Integer
    Public iDocState As Integer
    Public iDocType_1 As Integer
    Public DS1 As DataSet
    Private intMstWH As Integer
    Public Inv_No As String
    Public POLocation As Integer
    Public clientWH As Integer

    Private Sub tsbClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbClose.Click
        Me.Close()
    End Sub
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        cmbOrdStatus.SelectedIndex = 0
        cmbAccount.Text = Nothing
        dtpOrdDateFrom.Value = Date.Now
        dtpOrdDateTo.Value = Date.Now
    End Sub
    Private Sub cb_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb.CheckedChanged
        If cb.Checked = True Then
            dtpOrdDateFrom.Enabled = True
            dtpOrdDateTo.Enabled = True
        Else
            dtpOrdDateFrom.Enabled = False
            dtpOrdDateTo.Enabled = False
        End If
    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        If cmbOrdStatus.SelectedItem = "Quotation" Then
            comDocState = DocState.Quote
        Else
            comDocState = 0
        End If
        If cmbAccount.Text = Nothing Then

            If cmbOrdType.Text = "Supplier Invoice" And CB2.Checked = True Then
                iDocType = DocType.SupplierInvoice
            End If

            DeleteRow()


            'SQL = "Set dateformat dmy SELECT AutoIndex, DocType, DocVersion, DocState, InvNumber,GrvNumber, " & _
            '" AccountID, cAccountName, Description,ExtOrderNum,DeliveryNote, InvDate, InvTotIncl, " & _
            '" DocRepID, OrderNum,  iDocPrinted," & _
            '" bUseFixedPrices,ulIDPOrdType As Type , uiIDSOrdSuppID , ucIDSOrddbName , CASE DocState WHEN 1 THEN 'Unprocessed' WHEN 2 THEN 'Quote' WHEN 3 THEN 'Partial Processed' WHEN 4 THEN 'Archived' END As Status , ubIDPOrdCheckIN as CheckIN , OrderDate, DeliveryDate FROM InvNum "

            SQL = " Set dateformat dmy SELECT     InvNum.AutoIndex, InvNum.DocType, InvNum.DocVersion, InvNum.DocState, InvNum.InvNumber, InvNum.GrvNumber, InvNum.AccountID, InvNum.cAccountName,  InvNum.Description, InvNum.ExtOrderNum, InvNum.DeliveryNote, InvNum.InvDate, InvNum.InvTotIncl, InvNum.DocRepID, InvNum.OrderNum, InvNum.iDocPrinted,   InvNum.bUseFixedPrices, InvNum.ulIDPOrdType AS Type, InvNum.uiIDSOrdSuppID, InvNum.ucIDSOrddbName,  CASE DocState WHEN 1 THEN 'Unprocessed' WHEN 2 THEN 'Quote' WHEN 3 THEN 'Partial Processed' WHEN 4 THEN 'Archived' END AS Status,  InvNum.ubIDPOrdCheckIN AS CheckIN, InvNum.OrderDate, InvNum.DeliveryDate, OrdersSt.StatusDescrip , InvNum.ProjectID  FROM         InvNum LEFT OUTER JOIN  OrdersSt ON InvNum.OrderStatusID = OrdersSt.StatusCounter     "


            If iDocType = DocType.SalesOrder Or iDocType = DocType.Invoice Then
                If cb.Checked = True Then
                    If cmbOrdStatus.SelectedIndex = 0 Then
                        SQL = SQL & " WHERE (DocType=4) AND (DocState=1 Or DocState=2 Or DocState=4 Or DocState=3)   AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "' "
                    ElseIf cmbOrdStatus.SelectedIndex = 1 Or cmbOrdStatus.SelectedIndex = 2 Then
                        SQL = SQL & " WHERE (DocType=4) AND DocState=1  AND  OrderNum<>'Quote' AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    ElseIf cmbOrdStatus.SelectedIndex = 3 Then
                        SQL = SQL & " WHERE (DocType=4) AND DocState=2  AND  OrderNum='Quote' AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    ElseIf cmbOrdStatus.SelectedIndex = 4 Then
                        SQL = SQL & " WHERE (DocType=4 AND DocState=4  AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    End If
                Else
                    If cmbOrdStatus.SelectedIndex = 0 Then
                        SQL = SQL & " WHERE (DocType=4) AND (DocState=1 Or DocState=2 Or DocState=4 Or DocState=3)  "
                    ElseIf cmbOrdStatus.SelectedIndex = 1 Or cmbOrdStatus.SelectedIndex = 2 Then
                        SQL = SQL & " WHERE (DocType=4) AND DocState=1  AND  OrderNum<>'Quote' "
                    ElseIf cmbOrdStatus.SelectedIndex = 3 Then
                        SQL = SQL & " WHERE (DocType=4) AND DocState=2 AND  OrderNum='Quote'  "
                    ElseIf cmbOrdStatus.SelectedIndex = 4 Then
                        SQL = SQL & " WHERE (DocType=4) AND DocState=4  "
                    End If
                End If

            ElseIf iDocType = DocType.CreditNote Then
                If cb.Checked = True Then
                    If cmbOrdStatus.SelectedIndex = 0 Then
                        SQL = SQL & " WHERE (DocType=1) AND (DocState=1 Or DocState=2 Or DocState=4 Or DocState=3)   AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "' "
                    ElseIf cmbOrdStatus.SelectedIndex = 1 Or cmbOrdStatus.SelectedIndex = 2 Then
                        SQL = SQL & " WHERE (DocType=1) AND DocState=1  AND  OrderNum<>'Quote' AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    ElseIf cmbOrdStatus.SelectedIndex = 3 Then
                        SQL = SQL & " WHERE (DocType=1) AND DocState=2  AND  OrderNum='Quote' AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    ElseIf cmbOrdStatus.SelectedIndex = 4 Then
                        SQL = SQL & " WHERE (DocType=1) AND DocState=4  AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    End If
                Else
                    If cmbOrdStatus.SelectedIndex = 0 Then
                        SQL = SQL & " WHERE (DocType=1) AND (DocState=1 Or DocState=2 Or DocState=4 Or DocState=3)  "
                    ElseIf cmbOrdStatus.SelectedIndex = 1 Or cmbOrdStatus.SelectedIndex = 2 Then
                        SQL = SQL & " WHERE (DocType=1) AND DocState=1  AND  OrderNum<>'Quote' "
                    ElseIf cmbOrdStatus.SelectedIndex = 3 Then
                        SQL = SQL & " WHERE (DocType=1) AND DocState=2 AND  OrderNum='Quote'  "
                    ElseIf cmbOrdStatus.SelectedIndex = 4 Then
                        SQL = SQL & " WHERE (DocType=1) AND DocState=4  "
                    End If
                End If

            ElseIf iDocType = DocType.PurchaseOrder Then

                'SQL = "Set dateformat dmy SELECT AutoIndex, DocType, DocVersion, DocState, InvNumber,GrvNumber, " & _
                '" AccountID,cAccountName, Description,ExtOrderNum,DeliveryNote, InvDate, OrderDate, DueDate, DeliveryDate, " & _
                '" DocRepID, OrderNum, ProjectID, iProspectID, iOpportunityID, iDocPrinted," & _
                '" bUseFixedPrices,ulIDPOrdType As Type, CASE DocState WHEN 1 THEN 'Unprocessed' WHEN 2 THEN 'Quote' WHEN 3 THEN 'Partial Processed' WHEN 4 THEN 'Archived' END As Status ,  ubIDPOrdCheckIN as CheckIN  FROM InvNum"

                SQL = "Set dateformat dmy SELECT     InvNum.AutoIndex, InvNum.DocType, InvNum.DocVersion, InvNum.DocState, InvNum.InvNumber, InvNum.GrvNumber, InvNum.AccountID, InvNum.cAccountName,  InvNum.Description, InvNum.ExtOrderNum, InvNum.DeliveryNote, InvNum.InvDate, InvNum.InvTotIncl, InvNum.DocRepID, InvNum.OrderNum, InvNum.iDocPrinted,   InvNum.bUseFixedPrices, InvNum.ulIDPOrdType AS Type, InvNum.uiIDSOrdSuppID, InvNum.ucIDSOrddbName,  CASE DocState WHEN 1 THEN 'Unprocessed' WHEN 2 THEN 'Quote' WHEN 3 THEN 'Partial Processed' WHEN 4 THEN 'Archived' END AS Status,  InvNum.ubIDPOrdCheckIN AS CheckIN, InvNum.OrderDate, InvNum.DeliveryDate, OrdersSt.StatusDescrip  , InvNum.ProjectID  FROM         InvNum LEFT OUTER JOIN  OrdersSt ON InvNum.OrderStatusID = OrdersSt.StatusCounter     "


                'SQL = "Set dateformat dmy SELECT AutoIndex, InvDate , OrderNum ,InvNumber, DocType,  DocState,   " & _
                '"  ulIDPOrdType As Type ,  CASE DocState WHEN 1 THEN 'Unprocessed' WHEN 2 THEN 'Quote' WHEN 3 THEN 'Partial Processed' WHEN 4 THEN 'Archived' END As Status , ubIDPOrdCheckIN as CheckIN FROM InvNum"


                If cb.Checked = True Then
                    If cmbOrdStatus.SelectedIndex = 0 Then
                        SQL = SQL & " WHERE DocType=5 AND (DocState=1 Or DocState=2 Or DocState=4 Or DocState=3)  AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & dtpOrdDateTo.Value & "' "
                    ElseIf cmbOrdStatus.SelectedIndex = 1 Or cmbOrdStatus.SelectedIndex = 2 Then
                        SQL = SQL & " WHERE DocType=5 AND DocState=1 AND  OrderNum<>'Quote' AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    ElseIf cmbOrdStatus.SelectedIndex = 3 Then
                        SQL = SQL & " WHERE DocType=5 AND DocState=2  AND  OrderNum='Quote' AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    ElseIf cmbOrdStatus.SelectedIndex = 4 Then
                        SQL = SQL & " WHERE DocType=5 AND DocState=4  AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    End If
                Else
                    If cmbOrdStatus.SelectedIndex = 0 Then
                        SQL = SQL & " WHERE DocType=5 AND (DocState=1 Or DocState=2 Or DocState=4 Or DocState=3) "
                    ElseIf cmbOrdStatus.SelectedIndex = 1 Or cmbOrdStatus.SelectedIndex = 2 Then
                        SQL = SQL & " WHERE DocType=5 AND DocState=1  AND  OrderNum<>'Quote' "
                    ElseIf cmbOrdStatus.SelectedIndex = 3 Then
                        SQL = SQL & " WHERE DocType=5 AND DocState=2  AND  OrderNum='Quote'  "
                    ElseIf cmbOrdStatus.SelectedIndex = 4 Then
                        SQL = SQL & " WHERE DocType=5 AND DocState=4 "
                    End If
                End If
            ElseIf iDocType = DocType.SupplierInvoice Or iDocType = DocType.RTN Then
                'ElseIf iDocType = CB2.Checked = True Then
                If cb.Checked = True Then
                    If cmbOrdStatus.SelectedIndex = 0 Then
                        SQL = SQL & " WHERE  (DocType=5 OR DocType=3) AND (DocState=1 Or DocState=2 Or DocState=4 Or DocState=3)  AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "' "
                    ElseIf cmbOrdStatus.SelectedIndex = 1 Or cmbOrdStatus.SelectedIndex = 2 Then
                        SQL = SQL & " WHERE  (DocType=5 OR DocType=3) AND DocState=1  AND  OrderNum<>'Quote' AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    ElseIf cmbOrdStatus.SelectedIndex = 3 Then
                        SQL = SQL & " WHERE  (DocType=5 OR DocType=3) AND DocState=2 AND  OrderNum='Quote' AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    ElseIf cmbOrdStatus.SelectedIndex = 4 Then
                        SQL = SQL & " WHERE  (DocType=5 OR DocType=3) AND DocState=4  AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    End If
                Else
                    If cmbOrdStatus.SelectedIndex = 0 Then
                        SQL = SQL & " WHERE DocFlag=2 AND DocType=5 AND (DocState=1 Or DocState=2 Or DocState=4 Or DocState=3)  "
                    ElseIf cmbOrdStatus.SelectedIndex = 1 Or cmbOrdStatus.SelectedIndex = 2 Then
                        SQL = SQL & " WHERE DocFlag=2 AND DocType=5 AND DocState=1  AND  OrderNum<>'Quote' "
                    ElseIf cmbOrdStatus.SelectedIndex = 3 Then
                        SQL = SQL & " WHERE DocFlag=2 AND DocType=5 AND DocState=2  AND  OrderNum='Quote'  "
                    ElseIf cmbOrdStatus.SelectedIndex = 4 Then
                        SQL = SQL & " WHERE DocFlag=2 AND DocType=5 AND DocState=4  "
                    End If
                End If
            End If

            If iDocType = 9 Then
                iDocType = DocType.SalesOrder ' coz this is a salesorder
            End If


            'MsgBox("Account Name can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
            'Exit Sub

        Else 'no customer selected
            If cmbOrdType.Text = "Supplier Invoice" And CB2.Checked = True Then
                iDocType = DocType.SupplierInvoice
            End If
            DeleteRow()



            SQL = "Set dateformat dmy SELECT AutoIndex, DocType, DocVersion, DocState, InvNumber,GrvNumber, " & _
            " AccountID,cAccountName, Description,ExtOrderNum,DeliveryNote, InvDate, OrderDate, DueDate, DeliveryDate, " & _
            " DocRepID, OrderNum, ProjectID, iProspectID, iOpportunityID, iDocPrinted," & _
            " bUseFixedPrices,ulIDPOrdType As Type , uiIDSOrdSuppID , ucIDSOrddbName , CASE DocState WHEN 1 THEN 'Unprocessed' WHEN 2 THEN 'Quote' WHEN 3 THEN 'Partial Processed' WHEN 4 THEN 'Archived' END As Status ,  ubIDPOrdCheckIN as CheckIN  FROM InvNum"

            If iDocType = DocType.SalesOrder Or iDocType = DocType.Invoice Then
                If cb.Checked = True Then
                    If cmbOrdStatus.SelectedIndex = 0 Then
                        SQL = SQL & " WHERE (DocType=4 Or DocType=1) AND (DocState=1 Or DocState=2 Or DocState=4 Or DocState=3)  AND AccountID=" & cmbAccount.Value & " AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "' "
                    ElseIf cmbOrdStatus.SelectedIndex = 1 Or cmbOrdStatus.SelectedIndex = 2 Then
                        SQL = SQL & " WHERE (DocType=4 Or DocType=1) AND DocState=1 AND   OrderNum<>'Quote' AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    ElseIf cmbOrdStatus.SelectedIndex = 3 Then
                        SQL = SQL & " WHERE (DocType=4 Or DocType=1) AND DocState=2 AND  OrderNum='Quote' AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    ElseIf cmbOrdStatus.SelectedIndex = 4 Then
                        SQL = SQL & " WHERE (DocType=4 Or DocType=1) AND DocState=4 AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    End If
                Else
                    If cmbOrdStatus.SelectedIndex = 0 Then
                        SQL = SQL & " WHERE (DocType=4 Or DocType=1) AND (DocState=1 Or DocState=2 Or DocState=4 Or DocState=3) AND AccountID=" & cmbAccount.Value & " "
                    ElseIf cmbOrdStatus.SelectedIndex = 1 Or cmbOrdStatus.SelectedIndex = 2 Then
                        SQL = SQL & " WHERE (DocType=4 Or DocType=1) AND DocState=1 AND  OrderNum<>'Quote'  AND AccountID=" & cmbAccount.Value & " "
                    ElseIf cmbOrdStatus.SelectedIndex = 3 Then
                        SQL = SQL & " WHERE (DocType=4 Or DocType=1) AND DocState=2 AND  OrderNum='Quote'  AND AccountID=" & cmbAccount.Value & "  "
                    ElseIf cmbOrdStatus.SelectedIndex = 4 Then
                        SQL = SQL & " WHERE (DocType=4 Or DocType=1) AND DocState=4 AND  AND AccountID=" & cmbAccount.Value & " "
                    End If
                End If

            ElseIf iDocType = DocType.CreditNote Then
                If cb.Checked = True Then
                    If cmbOrdStatus.SelectedIndex = 0 Then
                        SQL = SQL & " WHERE (DocType=1) AND (DocState=1 Or DocState=2 Or DocState=4 Or DocState=3)   AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "' "
                    ElseIf cmbOrdStatus.SelectedIndex = 1 Or cmbOrdStatus.SelectedIndex = 2 Then
                        SQL = SQL & " WHERE (DocType=1) AND DocState=1  AND  OrderNum<>'Quote' AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    ElseIf cmbOrdStatus.SelectedIndex = 3 Then
                        SQL = SQL & " WHERE (DocType=1) AND DocState=2  AND  OrderNum='Quote' AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    ElseIf cmbOrdStatus.SelectedIndex = 4 Then
                        SQL = SQL & " WHERE (DocType=1) AND DocState=4  AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    End If
                Else
                    If cmbOrdStatus.SelectedIndex = 0 Then
                        SQL = SQL & " WHERE (DocType=1) AND (DocState=1 Or DocState=2 Or DocState=4 Or DocState=3)  "
                    ElseIf cmbOrdStatus.SelectedIndex = 1 Or cmbOrdStatus.SelectedIndex = 2 Then
                        SQL = SQL & " WHERE (DocType=1) AND DocState=1  AND  OrderNum<>'Quote' "
                    ElseIf cmbOrdStatus.SelectedIndex = 3 Then
                        SQL = SQL & " WHERE (DocType=1) AND DocState=2 AND  OrderNum='Quote'  "
                    ElseIf cmbOrdStatus.SelectedIndex = 4 Then
                        SQL = SQL & " WHERE (DocType=1) AND DocState=4  "
                    End If
                End If

            ElseIf iDocType = DocType.PurchaseOrder Or iDocType = DocType.RTN Then
                If cb.Checked = True Then
                    If cmbOrdStatus.SelectedIndex = 0 Then
                        SQL = SQL & " WHERE DocFlag<>2 AND (DocType=5 OR DocType=3) AND (DocState=1 Or DocState=2 Or DocState=4 Or DocState=3)  AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "' "
                    ElseIf cmbOrdStatus.SelectedIndex = 1 Or cmbOrdStatus.SelectedIndex = 2 Then
                        SQL = SQL & " WHERE DocFlag<>2 AND (DocType=5 OR DocType=3) AND DocState=1  AND  OrderNum<>'Quote' AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    ElseIf cmbOrdStatus.SelectedIndex = 3 Then
                        SQL = SQL & " WHERE DocFlag<>2 AND (DocType=5 OR DocType=3) AND DocState=2  AND  OrderNum='Quote' AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    ElseIf cmbOrdStatus.SelectedIndex = 4 Then
                        SQL = SQL & " WHERE DocFlag<>2 AND (DocType=5 OR DocType=3) AND DocState=4  AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    End If
                Else
                    If cmbOrdStatus.SelectedIndex = 0 Then
                        SQL = SQL & " WHERE DocFlag<>2 AND DocType=5 AND (DocState=1 Or DocState=2 Or DocState=4 Or DocState=3) AND AccountID=" & cmbAccount.Value & " "
                    ElseIf cmbOrdStatus.SelectedIndex = 1 Or cmbOrdStatus.SelectedIndex = 2 Then
                        SQL = SQL & " WHERE DocFlag<>2 AND DocType=5 AND DocState=1 AND  OrderNum<>'Quote' AND AccountID=" & cmbAccount.Value & ""
                    ElseIf cmbOrdStatus.SelectedIndex = 3 Then
                        SQL = SQL & " WHERE DocFlag<>2 AND DocType=5 AND DocState=2 AND  OrderNum='Quote' AND AccountID=" & cmbAccount.Value & " "
                    ElseIf cmbOrdStatus.SelectedIndex = 4 Then
                        SQL = SQL & " WHERE DocFlag<>2 AND DocType=5 AND DocState=4 AND "
                    End If
                End If
            ElseIf iDocType = DocType.SupplierInvoice Then
                If cb.Checked = True Then
                    If cmbOrdStatus.SelectedIndex = 0 Then
                        SQL = SQL & " WHERE DocFlag=2 AND DocType=5 AND (DocState=1 Or DocState=2 Or DocState=4 Or DocState=3)   AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "' "
                    ElseIf cmbOrdStatus.SelectedIndex = 1 Or cmbOrdStatus.SelectedIndex = 2 Then
                        SQL = SQL & " WHERE DocFlag=2 AND DocType=5 AND DocState=1  AND  OrderNum<>'Quote' AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    ElseIf cmbOrdStatus.SelectedIndex = 3 Then
                        SQL = SQL & " WHERE DocFlag=2 AND DocType=5 AND DocState=2  AND  OrderNum='Quote' AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    ElseIf cmbOrdStatus.SelectedIndex = 4 Then
                        SQL = SQL & " WHERE DocFlag=2 AND DocType=5 AND DocState=4  AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    End If
                Else
                    If cmbOrdStatus.SelectedIndex = 0 Then
                        SQL = SQL & " WHERE DocFlag=2 AND DocType=5 AND (DocState=1 Or DocState=2 Or DocState=4 Or DocState=3) "
                    ElseIf cmbOrdStatus.SelectedIndex = 1 Or cmbOrdStatus.SelectedIndex = 2 Then
                        SQL = SQL & " WHERE DocFlag=2 AND DocType=5 AND DocState=1 AND  OrderNum<>'Quote' "
                    ElseIf cmbOrdStatus.SelectedIndex = 3 Then
                        SQL = SQL & " WHERE DocFlag=2 AND DocType=5 AND DocState=2 AND  OrderNum='Quote'  "
                    ElseIf cmbOrdStatus.SelectedIndex = 4 Then
                        SQL = SQL & " WHERE DocFlag=2 AND DocType=5 AND DocState=4 "
                    End If
                End If

            End If
        End If
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                UG.DataSource = DS.Tables(0)
                If iDocType = DocType.SalesOrder Then
                    If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sAgent & "OPENSO.xml") Then
                        UG.DisplayLayout.LoadFromXml(Application.StartupPath & "\" & sAgent & "OPENSO.xml")
                    End If
                ElseIf iDocType = DocType.PurchaseOrder Then
                    If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sAgent & "OPENPO.xml") Then
                        UG.DisplayLayout.LoadFromXml(Application.StartupPath & "\" & sAgent & "OPENPO.xml")
                    End If
                ElseIf iDocType = DocType.Open Then
                    If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sAgent & "OPENPO.xml") Then
                        UG.DisplayLayout.LoadFromXml(Application.StartupPath & "\" & sAgent & "OPEN.xml")
                    End If
                End If
                'Dim Dr As DataRow
                'For Each Dr In DS.Tables(0).Rows
                '    Dim R As UltraGridRow = UG.DisplayLayout.Bands(0).AddNew
                '    R.Cells("AutoIndex").Value = Dr.Item("AutoIndex")
                '    R.Cells("DocType").Value = Dr.Item("DocType")
                '    R.Cells("DocVersion").Value = Dr.Item("DocVersion")
                '    R.Cells("DocState").Value = Dr.Item("DocState")
                '    R.Cells("InvNumber").Value = IIf(IsDBNull(Dr.Item("InvNumber")) = True, "", Dr.Item("InvNumber"))
                '    R.Cells("GrvNumber").Value = IIf(IsDBNull(Dr.Item("GrvNumber")) = True, "", Dr.Item("GrvNumber"))
                '    R.Cells("AccountID").Value = Dr.Item("AccountID")
                '    R.Cells("Description").Value = IIf(IsDBNull(Dr.Item("Description")) = True, "", Dr.Item("Description"))
                '    R.Cells("InvDate").Value = Dr.Item("InvDate")
                '    R.Cells("OrderDate").Value = Dr.Item("OrderDate")
                '    R.Cells("DueDate").Value = Dr.Item("DueDate")
                '    R.Cells("DeliveryDate").Value = Dr.Item("DeliveryDate")
                '    R.Cells("DocRepID").Value = IIf(IsDBNull(Dr.Item("DocRepID")) = True, 0, Dr.Item("DocRepID"))
                '    R.Cells("OrderNum").Value = IIf(IsDBNull(Dr.Item("OrderNum")) = True, "", Dr.Item("OrderNum"))
                '    R.Cells("ProjectID").Value = IIf(IsDBNull(Dr.Item("ProjectID")) = True, 0, Dr.Item("ProjectID"))
                '    R.Cells("iProspectID").Value = IIf(IsDBNull(Dr.Item("iProspectID")) = True, 0, Dr.Item("iProspectID"))
                '    R.Cells("iOpportunityID").Value = IIf(IsDBNull(Dr.Item("iOpportunityID")) = True, 0, Dr.Item("iOpportunityID"))
                '    R.Cells("iDocPrinted").Value = Dr.Item("iDocPrinted")
                '    R.Cells("bUseFixedPrices").Value = Dr.Item("bUseFixedPrices")
                '    R.Cells("ExtOrderNum").Value = IIf(IsDBNull(Dr.Item("ExtOrderNum")) = True, "", Dr.Item("ExtOrderNum"))
                '    R.Cells("DeliveryNote").Value = IIf(IsDBNull(Dr.Item("DeliveryNote")) = True, "", Dr.Item("DeliveryNote"))
                '    If Dr.Item("OrderNum") = "Quote" Then
                '        R.Cells("Status").Value = "Quotation"
                '    Else
                '        If Dr.Item("DocState") = 4 Then
                '            R.Cells("Status").Value = "Archived"
                '        Else
                '            R.Cells("Status").Value = "Unprocessed"
                '        End If

                '    End If
                'Next
                'UG.DisplayLayout.Bands(0).Columns("InvTotIncl").CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right
                'UG.DisplayLayout.Override.CellAppearance    .Bands(0).Columns("InvTotIncl").
                'UG.DisplayLayout.Bands(0).Columns("Code").CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect



                UG.DisplayLayout.Bands(0).Columns("DocType").Hidden = True
                UG.DisplayLayout.Bands(0).Columns("DocVersion").Hidden = True
                UG.DisplayLayout.Bands(0).Columns("DocState").Hidden = True
                UG.DisplayLayout.Bands(0).Columns("AccountID").Hidden = True
                UG.DisplayLayout.Bands(0).Columns("DueDate").Hidden = True
                UG.DisplayLayout.Bands(0).Columns("DeliveryDate").Hidden = True
                UG.DisplayLayout.Bands(0).Columns("DocRepID").Hidden = True
                UG.DisplayLayout.Bands(0).Columns("bUseFixedPrices").Hidden = True
                UG.DisplayLayout.Bands(0).Columns("ProjectID").Hidden = True
                UG.DisplayLayout.Bands(0).Columns("iProspectID").Hidden = True
                UG.DisplayLayout.Bands(0).Columns("iOpportunityID").Hidden = True
                UG.DisplayLayout.Bands(0).Columns("iDocPrinted").Hidden = True
                'UG.DisplayLayout.Bands(0).Columns("iDocPrinted").Hidden = True
                'UG.DisplayLayout.Bands(0).Columns("iDocPrinted").Hidden = True
                'UG.DisplayLayout.Bands(0).Columns("iDocPrinted").Hidden = True
                'UG.DisplayLayout.Bands(0).Columns("iDocPrinted").Hidden = True
                'UG.DisplayLayout.Bands(0).Columns("iDocPrinted").Hidden = True
                'UG.DisplayLayout.Bands(0).Columns("iDocPrinted").Hidden = True

            Catch ex As Exception
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try
        End With
    End Sub
    Private Sub cmbAccount_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAccount.ValueChanged
        DeleteRow()
    End Sub
    Private Sub cmbOrdStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbOrdStatus.SelectedIndexChanged
        DeleteRow()
    End Sub
    Private Sub DeleteRow()
        Dim ugR As UltraGridRow
        For Each ugR In UG.Rows.All
            ugR.Delete(False)
        Next
    End Sub
    Private Sub dtpOrdDateFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpOrdDateFrom.ValueChanged
        DeleteRow()
    End Sub

    Private Sub UG_DoubleClickRow(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs) Handles UG.DoubleClickRow
        If iDocType_1 = 10 Then
            Set_IBT_Data()
        ElseIf iDocType_1 = 8 Then
            Set_IBTReceive_Data()
        ElseIf iDocType_1 = 3 Then
            Set_RTN_Data()
            'Update_PO_SN()
        ElseIf iDocType_1 = 1 Then
            Set_CRN_Data()
        Else
            Set_Data()
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If iDocType_1 = 10 Then
            Set_IBT_Data()
        ElseIf iDocType_1 = 8 Then
            Set_IBTReceive_Data()
        ElseIf iDocType_1 = 3 Then
            Set_RTN_Data()
            'Update_PO_SN()
        ElseIf iDocType_1 = 1 Then
            Set_CRN_Data()
        Else
            Set_Data()
        End If
    End Sub
    Private Sub Set_CRN_Data()


        Dim bHideCost As Boolean = False

        If UG.ActiveRow.Selected = True Then


            Dim objRTN As New frmCreditNote


            With objRTN


                If iDocType = DocType.CreditNote Then
                    .iDocType = DocType.CreditNote
                    .Discard()
                    '.DocumentSettings(DocType.RTN)
                    .Text = "Credit Note"
                    .IsNew = True

                    


                    'Access rights
                    'Dim Dr As DataRow
                    'For Each Dr In dsManu.Tables(0).Rows
                    '    If .tsbSavePO.Tag = Dr("iManu") Then
                    '        .tsbSavePO.Visible = True
                    '    ElseIf .tsbOpen.Tag = Dr("iManu") Then
                    '        .tsbOpen.Visible = True
                    '    ElseIf .tsbUpdatePO.Tag = Dr("iManu") Then
                    '        .tsbUpdatePO.Visible = True
                    '    ElseIf .tsbProcessPO.Tag = Dr("iManu") Then
                    '        .tsbProcessPO.Visible = True
                    '    ElseIf 22243 = Dr("iManu") Then
                    '        .UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
                    '        .UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
                    '        .UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default
                    '    End If
                    'Next

                    bHideCost = True

                    .Show()

                End If

                'If UG.ActiveRow.Cells("Status").Value = "Archived" Then
                '    .IsNew = True
                'Else
                .IsNew = False
                'End If

                .lblState.Text = UG.ActiveRow.Cells("Status").Value
                .lblAutoID.Text = UG.ActiveRow.Cells("AutoIndex").Value
                .iSONo = UG.ActiveRow.Cells("AutoIndex").Value
                .cmbCustomer.Value = UG.ActiveRow.Cells("AccountID").Value
                '.cmbProject.Value = UG.ActiveRow.Cells("ProjectID").Value
                .txtDescription.Text = UG.ActiveRow.Cells("Description").Value
                .txtExtOrder.Text = UG.ActiveRow.Cells("ExtOrderNum").Value
                .dtpInDate.Value = UG.ActiveRow.Cells("InvDate").Value
                .dtpOrdDate.Value = UG.ActiveRow.Cells("OrderDate").Value
                .dtpDeliDate.Value = UG.ActiveRow.Cells("DeliveryDate").Value
                .cmbSaleRep.Value = UG.ActiveRow.Cells("DocRepID").Value
                .txtOrdNo.Text = UG.ActiveRow.Cells("OrderNum").Value
                .txtInNo.Text = UG.ActiveRow.Cells("InvNumber").Value

                '.bHideCost = IIf(IsDBNull(UG.ActiveRow.Cells("CheckIN").Value), False, UG.ActiveRow.Cells("CheckIN").Value)

                .DeleteRows()

                If Not IsDBNull(UG.ActiveRow.Cells("CheckIN").Value) Then
                    If UG.ActiveRow.Cells("CheckIN").Value = True And sAgent.ToUpper <> "ADMIN" Then
                        objRTN.UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
                        objRTN.UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
                        objRTN.UG.DisplayLayout.Bands(0).Columns("OrderTotal_Excl").Hidden = True
                        objRTN.UG.DisplayLayout.Bands(0).Columns("OrderTotal_incl").Hidden = True
                        objRTN.UG.DisplayLayout.Bands(0).Columns("LineTotal_Excl").Hidden = True
                        objRTN.UG.DisplayLayout.Bands(0).Columns("LineTotal_incl").Hidden = True
                        objRTN.UG.DisplayLayout.Bands(0).Columns("OrderTax").Hidden = True
                        objRTN.UG.DisplayLayout.Bands(0).Columns("LineTax").Hidden = True
                        objRTN.UG.DisplayLayout.Bands(1).Columns("Received").Hidden = True
                        objRTN.UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default
                    End If
                End If

                SQL = "SELECT     _btblInvoiceLines.*, StkItem.Code FROM         _btblInvoiceLines INNER JOIN StkItem ON _btblInvoiceLines.iStockCodeID = StkItem.StockLink WHERE iInvoiceID =" & UG.ActiveRow.Cells("AutoIndex").Value & ""
                Dim objSQL As New clsSqlConn

                With objSQL
                    Try
                        DS = New DataSet
                        DS = .Get_Data_Sql(SQL)
                        For Each Dr In DS.Tables(0).Rows
                            Dim Row As UltraGridRow = objRTN.UG.DisplayLayout.Bands(0).AddNew
                            Row.Cells("Line").Value = Dr.Item("iLineID")

                            Row.Cells("Code").Value = Dr.Item("Code")
                            'Row.Cells("StockID").Value = Dr.Item("iStockCodeID")
                            Row.Cells("LineID").Value = Dr.Item("idInvoiceLines")

                            Row.Cells("Description_1").Value = Dr.Item("cDescription")
                            Row.Cells("Description_2").Value = Dr.Item("cDescription")
                            Row.Cells("Warehouse").Value = Dr.Item("iWarehouseID")



                            Row.Cells("AvailableQty").Value = Get_Available_Qty("SELECT WHQtyOnHand FROM WhseStk WHERE WHStockLink =" & Dr.Item("iStockCodeID") & " AND WHWhseID =" & Dr.Item("iWarehouseID") & "")

                            'If Row.Cells("AvailableQty").Value = 0 Then
                            'Row.Cells("Quantity").Value = 0
                            'Row.Cells("ConfirmQty").Value = 0
                            'Else
                            Row.Cells("Quantity").Value = Dr.Item("fQuantity")
                            Row.Cells("ConfirmQty").Value = Dr.Item("fQuantity")
                            'End If


                            Row.Cells("ProcessedQty").Value = 0

                            'If Dr.Item("fQtyLastProcess") <> 0 Then
                            '    Row.Cells("ConfirmQty").Value = Dr.Item("fQtyLastProcess")
                            'End If
                            'Row.Cells("ConfirmQty").Value = 0


                            Row.Cells("Price_Excl").Value = Math.Round(Dr.Item("fUnitPriceExcl"), 2, MidpointRounding.AwayFromZero)
                            Row.Cells("Price_Incl").Value = Math.Round(Dr.Item("fUnitPriceIncl"), 2, MidpointRounding.AwayFromZero)
                            Row.Cells("TaxType").Value = Dr.Item("iTaxTypeID")
                            Row.Cells("Discount").Value = Math.Round(Dr.Item("fUnitPriceIncl"), 2, MidpointRounding.AwayFromZero) * (Math.Round(Dr.Item("fLineDiscount"), 6, MidpointRounding.AwayFromZero) / 100)
                            Row.Cells("TaxRate").Value = Dr.Item("fTaxRate")
                            Row.Cells("StockID").Value = Dr.Item("iStockCodeID")
                            Row.Cells("IsWH").Value = IIf(Dr.Item("bIsWhseItem") = True, True, False)
                            Row.Cells("LineNote").Value = Dr.Item("cLineNotes")
                            'Row.Cells("IsSerialNo").Value = IIf(Dr.Item("bIsSerialItem") = True, True, False)


                            SQL = "SELECT      cLotNumber,       dLotExpiryDate FROM         _btblInvoiceLines  " & _
                                    " WHERE  iInvoiceID = " & UG.ActiveRow.Cells("AutoIndex").Value & " AND iStockCodeID = " & Dr.Item("iStockCodeID")

                            DS1 = New DataSet
                            DS1 = .Get_Data_Sql(SQL)


                            If DS1.Tables(0).Rows(0)(0).ToString().Length > 0 Then
                                Row.Cells("IsLot").Value = 1
                                Row.Cells("Lot").Value = DS1.Tables(0).Rows(0)(0).ToString()
                            Else
                                Row.Cells("IsLot").Value = 0
                            End If
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

                .SetSatus()


                If UG.ActiveRow.Cells("DocType").Value = 3 Then  'Return to Supplir
                    If UG.ActiveRow.Cells("DocState").Value = 4 Then
                        '.tsbProcessPO.Enabled = False
                        '.tsbSavePO.Enabled = False
                        '.tsbUpdatePO.Enabled = False
                        '.UG.Enabled = False
                    Else
                        .UG.Enabled = True
                    End If
                    If UG.ActiveRow.Cells("InvNumber").Value = "" Then
                        '.GET_NEXT_GRV_NO()
                    End If
                End If
                'If UG.ActiveRow.Cells("DocType").Value = 5 Then  'Purchase Order
                '    If UG.ActiveRow.Cells("DocState").Value = 4 Then
                '        .tsbProcessPO.Enabled = False
                '        .tsbSavePO.Enabled = False
                '        .tsbUpdatePO.Enabled = False
                '        '.UG.Enabled = False
                '    Else
                '        .UG.Enabled = True
                '    End If
                '    If UG.ActiveRow.Cells("InvNumber").Value = "" Then
                '        .GET_NEXT_GRV_NO()
                '    End If
                'ElseIf UG.ActiveRow.Cells("DocType").Value = 4 Then 'Sales Order
                '    If UG.ActiveRow.Cells("DocState").Value = 4 Then
                '        .tsbSaveSO.Enabled = False
                '        .tsbSOPlaceOrder.Enabled = False
                '        .tsbSOQuote.Enabled = False
                '        .tsbUpdateSO.Enabled = False
                '        '.UG.Enabled = False
                '    Else
                '        .UG.Enabled = True
                '        .GET_NEXT_DELIVERY_NO()
                '        .GET_NEXT_INVOICE_NO()
                '    End If
                '    .cmbCustomer.Enabled = False
                'End If


                If UG.ActiveRow.Cells("DocState").Value = 4 Then
                    .tsbSave.Enabled = False
                Else
                    .tsbSave.Enabled = True
                End If



                .Show()
                '.tsbSave.Enabled = True
                .Delete_Row()
            End With

            objRTN = Nothing
            Me.Close()
        End If

    End Sub
    Private Sub Set_RTN_Data()

        Dim dr2 As DataRow
        Dim i As Integer
        Dim bHideCost As Boolean = False

        If UG.ActiveRow.Selected = True Then


            Dim objRTN As New frmReturnSuplier


            With objRTN


                If iDocType = DocType.RTN Then
                    .iDocType = DocType.RTN
                    .Discard()
                    '.DocumentSettings(DocType.RTN)
                    .Text = "Return to Supplier"
                    .IsNew = True


                    'Access rights
                    'Dim Dr As DataRow
                    'For Each Dr In dsManu.Tables(0).Rows
                    '    If .tsbSavePO.Tag = Dr("iManu") Then
                    '        .tsbSavePO.Visible = True
                    '    ElseIf .tsbOpen.Tag = Dr("iManu") Then
                    '        .tsbOpen.Visible = True
                    '    ElseIf .tsbUpdatePO.Tag = Dr("iManu") Then
                    '        .tsbUpdatePO.Visible = True
                    '    ElseIf .tsbProcessPO.Tag = Dr("iManu") Then
                    '        .tsbProcessPO.Visible = True
                    '    ElseIf 22243 = Dr("iManu") Then
                    '        .UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
                    '        .UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
                    '        .UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default
                    '    End If
                    'Next

                    bHideCost = True

                    .Show()

                End If



                .IsNew = False

                .lblAutoID.Text = UG.ActiveRow.Cells("AutoIndex").Value
                .iPONo = UG.ActiveRow.Cells("AutoIndex").Value
                .cmbSupplier.Value = UG.ActiveRow.Cells("AccountID").Value
                '.cmbProject.Value = UG.ActiveRow.Cells("ProjectID").Value
                .txtDescription.Text = UG.ActiveRow.Cells("Description").Value
                .txtExtOrder.Text = UG.ActiveRow.Cells("ExtOrderNum").Value
                .dtpInDate.Value = UG.ActiveRow.Cells("InvDate").Value
                .dtpOrdDate.Value = UG.ActiveRow.Cells("OrderDate").Value
                .dtpDeliDate.Value = UG.ActiveRow.Cells("DeliveryDate").Value
                .cmbSaleRep.Value = UG.ActiveRow.Cells("DocRepID").Value
                .txtOrdNo.Text = UG.ActiveRow.Cells("OrderNum").Value
                .txtInNo.Text = UG.ActiveRow.Cells("InvNumber").Value

                '.bHideCost = IIf(IsDBNull(UG.ActiveRow.Cells("CheckIN").Value), False, UG.ActiveRow.Cells("CheckIN").Value)

                .DeleteRows()

                If Not IsDBNull(UG.ActiveRow.Cells("CheckIN").Value) Then
                    If UG.ActiveRow.Cells("CheckIN").Value = True And sAgent.ToUpper <> "ADMIN" Then
                        objRTN.UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
                        objRTN.UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
                        objRTN.UG.DisplayLayout.Bands(0).Columns("OrderTotal_Excl").Hidden = True
                        objRTN.UG.DisplayLayout.Bands(0).Columns("OrderTotal_incl").Hidden = True
                        objRTN.UG.DisplayLayout.Bands(0).Columns("LineTotal_Excl").Hidden = True
                        objRTN.UG.DisplayLayout.Bands(0).Columns("LineTotal_incl").Hidden = True
                        objRTN.UG.DisplayLayout.Bands(0).Columns("OrderTax").Hidden = True
                        objRTN.UG.DisplayLayout.Bands(0).Columns("LineTax").Hidden = True
                        objRTN.UG.DisplayLayout.Bands(1).Columns("Received").Hidden = True
                        objRTN.UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default
                    End If
                End If

                SQL = "SELECT    *, StkItem.Code FROM _btblInvoiceLines INNER JOIN StkItem ON _btblInvoiceLines.iStockCodeID = StkItem.StockLink WHERE iInvoiceID =" & UG.ActiveRow.Cells("AutoIndex").Value & ""
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


                            SQL = "SELECT    iLotID,  cLotNumber,       dLotExpiryDate FROM         _btblInvoiceLines  " & _
                                    " WHERE  iInvoiceID = " & UG.ActiveRow.Cells("AutoIndex").Value & " AND iStockCodeID = " & Dr.Item("iStockCodeID")

                            DS1 = New DataSet
                            DS1 = .Get_Data_Sql(SQL)



                            Row.Cells("Lot").Value = DS1.Tables(0).Rows(0)(0).ToString()

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

                .SetSatus()


                If UG.ActiveRow.Cells("DocType").Value = 3 Then  'Return to Supplir
                    If UG.ActiveRow.Cells("DocState").Value = 4 Then
                        '.tsbProcessPO.Enabled = False
                        '.tsbSavePO.Enabled = False
                        '.tsbUpdatePO.Enabled = False
                        '.UG.Enabled = False
                    Else
                        .UG.Enabled = True
                    End If
                    If UG.ActiveRow.Cells("InvNumber").Value = "" Then
                        '.GET_NEXT_GRV_NO()
                    End If
                End If
                'If UG.ActiveRow.Cells("DocType").Value = 5 Then  'Purchase Order
                '    If UG.ActiveRow.Cells("DocState").Value = 4 Then
                '        .tsbProcessPO.Enabled = False
                '        .tsbSavePO.Enabled = False
                '        .tsbUpdatePO.Enabled = False
                '        '.UG.Enabled = False
                '    Else
                '        .UG.Enabled = True
                '    End If
                '    If UG.ActiveRow.Cells("InvNumber").Value = "" Then
                '        .GET_NEXT_GRV_NO()
                '    End If
                'ElseIf UG.ActiveRow.Cells("DocType").Value = 4 Then 'Sales Order
                '    If UG.ActiveRow.Cells("DocState").Value = 4 Then
                '        .tsbSaveSO.Enabled = False
                '        .tsbSOPlaceOrder.Enabled = False
                '        .tsbSOQuote.Enabled = False
                '        .tsbUpdateSO.Enabled = False
                '        '.UG.Enabled = False
                '    Else
                '        .UG.Enabled = True
                '        .GET_NEXT_DELIVERY_NO()
                '        .GET_NEXT_INVOICE_NO()
                '    End If
                '    .cmbCustomer.Enabled = False
                'End If

                .cmbSupplier.Enabled = False
                .txtExtOrder.Enabled = False
                .Show()

                'COMENTED COZ NEED TO REURN PO
                'If UG.ActiveRow.Cells("DocState").Value = 4 Then
                '    .tsbSave.Enabled = False
                'Else
                .tsbSave.Enabled = True
                'End If

                .Delete_Row()
            End With

            objRTN = Nothing
            Me.Close()
        End If

    End Sub
    Public Sub Update_PO_SN()
        Dim inv As String
        Dim Index As Integer
        If UG.ActiveRow.Selected = True Then
            inv = UG.ActiveRow.Cells("AutoIndex").Value

            'SQL = " SELECT SerialMF.SerialNumber	FROM SerialMF INNER JOIN " & _
            '" SerialTX ON SerialMF.SerialCounter = SerialTX.SNLink INNER JOIN " & _
            '" InvNum ON SerialTX.SNTxReference = InvNum.InvNumber " & _
            '" WHERE (SerialTX.SNTxReference <> '') AND SerialTX.SNTrCodeID = 33 AND SerialTX.SNTxReference = '" & sbIndex & "' "

            SQL = " SELECT     AutoIndex  FROM  InvNum  WHERE     AutoIndex = '" & inv & "'  "
            Dim objSQL1 As New clsSqlConn
            With objSQL1
                Try
                    .Begin_Trans()
                    Index = .Get_Max_No(SQL)
                    .Commit_Trans()
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                    Exit Sub
                Finally
                    objSQL1 = Nothing
                End Try
            End With


            SQL = " SELECT _btblInvoiceLineSN.cSerialNumber	FROM _btblInvoiceLineSN " & _
            " WHERE  _btblInvoiceLineSN.iSerialInvoiceID = '" & inv & "' "

            Dim objSQL As New clsSqlConn
            With objSQL
                Try
                    DS = New DataSet
                    DS = .Get_Data_Sql(SQL)

                    Dim x As Integer
                    Dim DR As DataRow
                    .Begin_Trans()
                    For Each DR In DS.Tables(0).Rows
                        'SQL = " update  top(1) sbSerialMF set AutoIndex = (SELECT     AutoIndex  FROM  InvNum  WHERE     InvNumber = '" & inv & "' ) where SerialNumber = '" & Mid(DR.Item("cSerialNumber"), 1, IIf(InStr(DR.Item("cSerialNumber").ToString, "!") = 0, DR.Item("cSerialNumber").ToString.Length, InStr(DR.Item("cSerialNumber").ToString, "!"))) & "'  "
                        SQL = ""
                        If .Execute_Sql_Trans(SQL) = 0 Then
                            .Rollback_Trans()
                            Exit Sub
                        End If
                    Next
                    .Commit_Trans()
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                    Exit Sub
                Finally
                    objSQL = Nothing
                End Try
            End With
        End If


    End Sub

    Public Sub Get_Customer()
        SQL = "SELECT DCLink, Account, Name   FROM Client  Order By Name "
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                cmbAccount.DataSource = DS.Tables(0)
                cmbAccount.ValueMember = "DCLink"
                cmbAccount.DisplayMember = "Name"
                cmbAccount.DisplayLayout.Bands(0).Columns("DCLink").Hidden = True
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                Exit Sub
            Finally
                objSQL = Nothing
            End Try
        End With
    End Sub

    Public Sub Get_Supplier()
        SQL = "SELECT DCLink, Account, Name FROM Vendor Order By Name"
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                cmbAccount.DataSource = DS.Tables(0)
                cmbAccount.ValueMember = "DCLink"
                cmbAccount.DisplayMember = "Name"
                cmbAccount.DisplayLayout.Bands(0).Columns("DCLink").Hidden = True
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                Exit Sub
            Finally
                objSQL = Nothing
            End Try
        End With
    End Sub
    Private Sub frmOpen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        cmbOrdStatus.SelectedIndex = 0
        dtpOrdDateFrom.Value = Date.Now
        dtpOrdDateTo.Value = Date.Now
        tsbCheckIN.Visible = False
        Dim dr As DataRow

        For Each Dr In dsManu.Tables(0).Rows
            If 22226 = dr("iManu") Then
                tsbCheckIN.Visible = True
            ElseIf 22222 = dr("iManu") Then
                'tsbPrint.Enabled = False
            End If
        Next

       



        Try
            If iDocType = DocType.SalesOrder Then
                If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sAgent & "OPENSO.xml") Then
                    UG.DisplayLayout.LoadFromXml(Application.StartupPath & "\" & sAgent & "OPENSO.xml")
                End If
            ElseIf iDocType = DocType.PurchaseOrder Then
                If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sAgent & "OPENPO.xml") Then
                    UG.DisplayLayout.SaveAsXml(Application.StartupPath & "\" & sAgent & "OPENPO.xml")
                End If
            ElseIf iDocType = DocType.Open Then
                If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sAgent & "OPEN.xml") Then
                    UG.DisplayLayout.SaveAsXml(Application.StartupPath & "\" & sAgent & "OPEN.xml")
                End If
            End If

        Catch ex As Exception
            Exit Sub
        End Try


        'Dim con As New SqlConnection
        'Dim cmd As New SqlCommand("Update_PO_SN", con)
        'CMD.CommandType = CommandType.StoredProcedure
        'cmd.ExecuteNonQuery()
        'con.Close()

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
    Private Sub tsbPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbPrint.Click
        Dim Customer As New Customer(CInt(UG.ActiveRow.Cells("AccountID").Value))
        If UG.Selected.Rows.Count = 0 Then Exit Sub
        'If MsgBox("Do you want to print now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
        Dim objRep As New ReportDocument
        'objRep.Load(Application.StartupPath & "\SalesOrder.rpt")
        'objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & UG.Selected.Rows(0).Cells("AutoIndex").Value & ""
        'ApplyLoginToTable(objRep)
        'If MsgBox("Do you want to print now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
        'Dim objRep As New ReportDocument

        If iDocType_1 = 10 Then
            'objRep.Load(Application.StartupPath & "\IBT.rpt")

            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= '" & UG.ActiveRow.Cells("ucIDSOrddbName").Value & "' ")
            SQL = " SELECT OrderNum, DocState  from InvNum where ExtOrderNum = '" & UG.ActiveRow.Cells("InvNumber").Value & "' "
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

            frmPrintPreview.vSelectionFormula = "{InvNum.AutoIndex}= " & UG.ActiveRow.Cells("AutoIndex").Value & ""

            iRepoID = 2

            frmPrintPreview.ShowDialog()
            iRepoID = 0

            Exit Sub

        ElseIf iDocType_1 = 8 Then
            objRep.Load(Application.StartupPath & "\IBT_R.rpt")
        Else
            If UG.ActiveRow.Cells("DocState").Value = 2 Then
                If sSQLSrvDataBase = "dbNawinna_new" Then
                    objRep.Load(Application.StartupPath & "\SalesOrderQN.rpt")
                Else
                    objRep.Load(Application.StartupPath & "\SalesOrderQ.rpt")
                End If
                'objRep.Load(Application.StartupPath & "\SalesOrderQ.rpt")
            Else
                If iDocType = 5 Then
                    objRep.Load(Application.StartupPath & "\PO.rpt")
                Else
                    If Customer.TaxNumber = Nothing Then
                        objRep.Load(Application.StartupPath & "\SalesOrder.rpt")
                    Else
                        objRep.Load(Application.StartupPath & "\SalesOrder_VAT.rpt")
                    End If
                End If
            End If
        End If

        'objRep.Load(Application.StartupPath & "\SalesOrder.rpt")
        objRep.RecordSelectionFormula = "{InvNum.AutoIndex}= " & UG.ActiveRow.Cells("AutoIndex").Value & ""
        ApplyLoginToTable(objRep)
        'objRep.PrintToPrinter(1, False, 0, 0)
        'End If
        objRep.SetDatabaseLogon(sSQLSrvUserName, sSQLSrvPassword, sSQLSrvReportSrv, sSQLSrvDataBase)


        'Dim ps As PaperSize
        'ps.DefaultPaperSize()
        'Dim pps As PrintPreviewControl
        'pps.Size = Printing.PaperSize("PageSize", 850, 2500)

        'PrintDocument1.DefaultPageSettings.PaperSize = New
        'Printing.PaperSize("PageSize", 850, 2500)



        'Dim DefaultPrinter As String = DefaultPrinterName
        'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
        'doctoprint.PrinterSettings.PrinterName = DefaultPrinter
        'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
        '    Dim rawKind As Integer
        '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Inv" Then
        '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
        '        objRep.PrintOptions.PaperSize = rawKind
        '        objRep.PrintToPrinter(1, False, 0, 0)
        '        Exit For
        '    End If
        'Next

        If MsgBox("Do you want to preview the sales order", MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.No Then
            Dim DefaultPrinter As String = DefaultPrinterName
            Dim doctoprint As New System.Drawing.Printing.PrintDocument()
            doctoprint.PrinterSettings.PrinterName = DefaultPrinter
            For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                Dim rawKind As Integer
                If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Inv" Then
                    rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                    objRep.PrintOptions.PaperSize = rawKind
                    objRep.PrintToPrinter(1, False, 0, 0)
                    Exit For
                End If
            Next
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
    End Sub
    Private ReadOnly Property DefaultPrinterName() As String
        Get
            Dim ps As New PrinterSettings()
            Return ps.PrinterName
        End Get
    End Property

    Private Sub tsbSaveMySettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSaveMySettings.Click
        Try
            If iDocType = DocType.SalesOrder Then
                UG.DisplayLayout.SaveAsXml(Application.StartupPath & "\" & sAgent & "OPENSO.xml")
            ElseIf iDocType = DocType.PurchaseOrder Then
                UG.DisplayLayout.SaveAsXml(Application.StartupPath & "\" & sAgent & "OPENPO.xml")
            ElseIf iDocType = DocType.Open Then
                UG.DisplayLayout.SaveAsXml(Application.StartupPath & "\" & sAgent & "OPEN.xml")
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub tsbCheckIN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbCheckIN.Click
        If UG.ActiveRow.Selected = False Then Exit Sub

        If MsgBox("Check-In will not allow other users to veiw or update Price Details of this document" & vbCrLf & "Do you want to continue?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.No Then Exit Sub

        Dim objSQL As New clsSqlConn
        Try

            With objSQL

                SQL = "UPDATE  InvNum   SET  ubIDPOrdCheckIN = 1 where AutoIndex = " & UG.ActiveRow.Cells("AutoIndex").Value

                If .Execute_Sql(SQL) = 1 Then
                    MsgBox("PO Updated for Costing", MsgBoxStyle.Information, "Evolution")
                    btnFind_Click(sender, e)
                End If

            End With

        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            objSQL = Nothing
        End Try
    End Sub

    Private Sub Set_Data()

        Dim dr2 As DataRow
        Dim i As Integer
        Dim bHideCost As Boolean = False

        If UG.ActiveRow.Selected = True Then

            'If iDocType = DocType.SalesOrder Then
            Dim objSalesOrder As New frmSalesOrder
            'Else



            'End If

            With objSalesOrder
                If UG.ActiveRow.Cells("DocState").Value = DocState.Quote Then
                    objSalesOrder.isQuote = True
                    objSalesOrder.index = UG.ActiveRow.Cells("AutoIndex").Value
                Else
                    objSalesOrder.isQuote = False
                End If
                If iDocType = DocType.SalesOrder Then
                    .iDocType = DocType.SalesOrder
                    .Discard()
                    .DocumentSettings(DocType.SalesOrder)
                    .Text = "Sales Order"
                    .IsNew = True
                    Dim Dr As DataRow
                    For Each Dr In dsManu.Tables(0).Rows
                        If .tsbSaveSO.Tag = Dr("iManu") Then
                            .tsbSaveSO.Visible = True
                        ElseIf .tsbOpen.Tag = Dr("iManu") Then
                            .tsbOpen.Visible = True
                        ElseIf .tsbUpdateSO.Tag = Dr("iManu") Then
                            .tsbUpdateSO.Visible = True
                        ElseIf 22213 = Dr("iManu") Then
                            .UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
                            .UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
                            .UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default
                        End If
                    Next
                    .lblBarcode.Visible = True
                    .txtBarCode.Visible = True
                    bHideCost = False

                    'If DocState.Quote = True Then
                    '    .dtpInDate.Enabled = True
                    'End If

                    .Show()
                ElseIf iDocType = DocType.PurchaseOrder Then
                    .iDocType = DocType.PurchaseOrder
                    .Discard()
                    .DocumentSettings(DocType.PurchaseOrder)
                    .Text = "Purchase Order"
                    .IsNew = True
                    Dim Dr As DataRow
                    For Each Dr In dsManu.Tables(0).Rows
                        If .tsbSavePO.Tag = Dr("iManu") Then
                            .tsbSavePO.Visible = True
                        ElseIf .tsbOpen.Tag = Dr("iManu") Then
                            .tsbOpen.Visible = True
                        ElseIf .tsbUpdatePO.Tag = Dr("iManu") Then
                            .tsbUpdatePO.Visible = True
                        ElseIf .tsbProcessPO.Tag = Dr("iManu") Then
                            .tsbProcessPO.Visible = True
                        ElseIf 22223 = Dr("iManu") Then
                            .UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
                            .UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
                            .UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default

                            '22228
                        End If
                    Next

                    .lblBarcode.Visible = True
                    .txtBarCode.Visible = True

                    bHideCost = True
                    .Show()

                End If

                .IsNew = False
                .lblAutoID.Text = UG.ActiveRow.Cells("AutoIndex").Value
                .iPONo = UG.ActiveRow.Cells("AutoIndex").Value
                .cmbCustomer.Value = UG.ActiveRow.Cells("AccountID").Value
                '.cmbProject.Value = UG.ActiveRow.Cells("ProjectID").Value
                .txtDescription.Text = UG.ActiveRow.Cells("Description").Value
                .txtExtOrder.Text = UG.ActiveRow.Cells("ExtOrderNum").Value
                .dtpInDate.Value = Date.Now
                .dtpOrdDate.Value = UG.ActiveRow.Cells("OrderDate").Value
                .dtpDeliDate.Value = UG.ActiveRow.Cells("DeliveryDate").Value
                .cmbSaleRep.Value = UG.ActiveRow.Cells("DocRepID").Value
                '.cmbSalesOpp.Value = UG.ActiveRow.Cells("iOpportunityID").Value
                .txtOrdNo.Text = UG.ActiveRow.Cells("OrderNum").Value
                .txtInNo.Text = UG.ActiveRow.Cells("InvNumber").Value
                .txtDelNote_SupInvNo.Text = UG.ActiveRow.Cells("DeliveryNote").Value
                .cmbType.Value = UG.ActiveRow.Cells("Type").Value
                .bHideCost = IIf(IsDBNull(UG.ActiveRow.Cells("CheckIN").Value), False, UG.ActiveRow.Cells("CheckIN").Value)

                .cmbCustomer.Enabled = False

                .DeleteRows()

                If Not IsDBNull(UG.ActiveRow.Cells("CheckIN").Value) And Not IsDBNull(UG.ActiveRow.Cells("Type").Value) Then
                    If UG.ActiveRow.Cells("CheckIN").Value = True And sAgent.ToUpper <> "ADMIN" And UG.ActiveRow.Cells("Type").Value = "Import" Then

                        objSalesOrder.UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
                        objSalesOrder.UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
                        objSalesOrder.UG.DisplayLayout.Bands(0).Columns("OrderTotal_Excl").Hidden = True
                        objSalesOrder.UG.DisplayLayout.Bands(0).Columns("OrderTotal_incl").Hidden = True
                        objSalesOrder.UG.DisplayLayout.Bands(0).Columns("LineTotal_Excl").Hidden = True
                        objSalesOrder.UG.DisplayLayout.Bands(0).Columns("LineTotal_incl").Hidden = True

                        objSalesOrder.UG.DisplayLayout.Bands(0).Columns("OrderTax").Hidden = True
                        objSalesOrder.UG.DisplayLayout.Bands(0).Columns("LineTax").Hidden = True

                        objSalesOrder.UG.DisplayLayout.Bands(0).Columns("TaxType").Hidden = True
                        objSalesOrder.UG.DisplayLayout.Bands(0).Columns("TaxRate").Hidden = True

                        objSalesOrder.UG.DisplayLayout.Bands(0).Columns("Discount").Hidden = True
                        objSalesOrder.UG.DisplayLayout.Bands(0).Columns("DiscountA").Hidden = True

                        objSalesOrder.UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default

                        objSalesOrder.UltraGroupBox2.Visible = False

                    End If
                End If

                SQL = " SELECT  *  FROM         _btblInvoiceLines INNER JOIN " & _
                      " StkItem ON _btblInvoiceLines.iStockCodeID = StkItem.StockLink INNER JOIN " & _
                      " _etblInvSegValue ON StkItem.iInvSegValue1ID = _etblInvSegValue.idInvSegValue LEFT OUTER JOIN " & _
         " _btblBINLocation ON StkItem.iBinLocationID = _btblBINLocation.idBinLocation WHERE iInvoiceID =" & UG.ActiveRow.Cells("AutoIndex").Value & ""
                Dim objSQL As New clsSqlConn

                With objSQL
                    Try
                        DS = New DataSet
                        DS = .Get_Data_Sql(SQL)
                        For Each Dr In DS.Tables(0).Rows
                            Dim Row As UltraGridRow = objSalesOrder.UG.DisplayLayout.Bands(0).AddNew
                            Row.Cells("Line").Value = Dr.Item("iLineID")

                            Row.Cells("Code").Value = Dr.Item("iStockCodeID")

                            Row.Cells("LineID").Value = Dr.Item("idInvoiceLines")

                            Row.Cells("Description_1").Value = Dr.Item("cDescription")
                            Row.Cells("Description_2").Value = Dr.Item("cDescription")
                            Row.Cells("Warehouse").Value = Dr.Item("iWarehouseID")
                            Row.Cells("Quantity").Value = Dr.Item("fQuantity")
                            Row.Cells("cValue").Value = Dr.Item("cValue")
                            Row.Cells("cBin").Value = Dr.Item("cBinLocationName")

                            Row.Cells("ConfirmQty").Value = Dr.Item("fQtyToProcess")
                            Row.Cells("ProcessedQty").Value = Dr.Item("fQtyProcessed")
                            If Dr.Item("fQtyLastProcess") <> 0 Then
                                Row.Cells("ConfirmQty").Value = Dr.Item("fQtyLastProcess")
                            End If
                            Row.Cells("Price_Excl").Value = Math.Round(Dr.Item("fUnitPriceExcl"), 2, MidpointRounding.AwayFromZero)
                            If objSalesOrder.isQuote = True Then
                                Row.Cells("Discount").Value = 0
                            Else
                                Row.Cells("Discount").Value = Math.Round(Dr.Item("fLineDiscount"), 6, MidpointRounding.AwayFromZero)
                            End If

                            Row.Cells("Price_Incl").Value = Math.Round(Dr.Item("fUnitPriceIncl"), 2, MidpointRounding.AwayFromZero)
                            Row.Cells("TaxType").Value = Dr.Item("iTaxTypeID")
                            Row.Cells("TaxRate").Value = Dr.Item("fTaxRate")
                            Row.Cells("StockID").Value = Dr.Item("iStockCodeID")
                            Row.Cells("ConfirmQty").Value = Dr.Item("fQtyToProcess")
                            Row.Cells("IsWH").Value = IIf(Dr.Item("bIsWhseItem") = True, True, False)
                            Row.Cells("LineNote").Value = Dr.Item("cLineNotes")
                            'Row.Cells("IsSerialNo").Value = IIf(Dr.Item("bIsSerialItem") = True, True, False)
                            Row.Cells("IsLot").Value = IIf(Dr.Item("bisLotItem") = True, True, False)
                            If objSalesOrder.isQuote = True Then
                                Row.Cells("Discount").Value = 0
                            Else
                                Row.Cells("Discount").Value = Math.Round(Dr.Item("fLineDiscount"), 6, MidpointRounding.AwayFromZero)
                            End If
                            'SQL = "SELECT      cLotNumber,       dLotExpiryDate FROM         _btblInvoiceLines  " & _
                            '        " WHERE  iInvoiceID = " & UG.ActiveRow.Cells("AutoIndex").Value & " AND iStockCodeID = " & Dr.Item("iStockCodeID")


                            Row.Cells("iUnit").Value = Dr.Item("iUnitsOfMeasureID")
                            Row.Cells("UOM").Value = Dr.Item("iUnitsOfMeasureID")

                            'DS1 = New DataSet
                            'DS1 = .Get_Data_Sql(SQL)
                            'Row.Cells("Lot").Value = Dr.Item("cLotNumber")
                            Row.Cells("Lot").Value = Dr.Item("iLotID")
                            Row.Cells("SerialLot").Value = Dr.Item("cLotNumber")
                            Row.Cells("Lot_Exp").Value = Dr.Item("dLotExpiryDate")


                            'Row.Cells("Discount").SelectAll()
                            'Row.Cells("LineTotal_Excl").Value = Math.Round(((Row.Cells("Quantity").Value * Row.Cells("Price_Excl").Value) - ((Row.Cells("Quantity").Value * Row.Cells("Price_Excl").Value)) * Row.Cells("Discount").Value / 100), 2, MidpointRounding.AwayFromZero)
                            'Row.Cells("LineTotal_Incl").Value = Math.Round(((Row.Cells("Quantity").Value * Row.Cells("Price_Incl").Value) - ((Row.Cells("Quantity").Value * Row.Cells("Price_Incl").Value)) * Row.Cells("Discount").Value / 100), 2, MidpointRounding.AwayFromZero)

                            'SQL = "SELECT     sbSerialMF.* FROM sbSerialMF  " & _
                            '        " WHERE  AutoIndex = " & UG.ActiveRow.Cells("AutoIndex").Value & " AND SNStockLink = " & Dr.Item("iStockCodeID")

                            'DS1 = New DataSet
                            'DS1 = .Get_Data_Sql(SQL)

                            'Dim ugR As UltraGridRow

                            'i = 0

                            'For Each dr2 In DS1.Tables(0).Rows

                            '    If Row.Band.Index = 0 Then
                            '        ugR = Row.ChildBands(0).Band.AddNew
                            '    Else
                            '        ugR = UG.DisplayLayout.Bands(1).AddNew
                            '    End If
                            '    ugR.Cells("LN").Value = i + 1
                            '    ugR.Cells("SerialNumber").Value = dr2("SerialNumber")
                            '    ugR.Cells("SNStockLink").Value = Dr.Item("iStockCodeID")
                            '    ugR.Cells("PrimaryLineID").Value = ugR.ParentRow.Cells("Line").Value
                            '    i = i + 1
                            'Next



                            If Dr.Item("fQtyReserved") > 0 Then
                                Row.Activation = Activation.NoEdit
                                Row.Cells("Lot").Activation = Activation.AllowEdit
                            End If

                            Dim Dr1 As DataRow
                            For Each Dr1 In dsManu.Tables(0).Rows

                                If 22228 = Dr1("iManu") Then
                                    Row.Activation = Activation.NoEdit
                                End If
                            Next
                        Next

                        For Each ugR In objSalesOrder.UG.Rows

                            ugR.Expanded = False

                            'If ugR.ChildBands.HasChildRows = True Then
                            '    ugR.CellAppearance.BackColor = Color.Yellow
                            'Else
                            '    ugR.CellAppearance.BackColor = Color.White
                            'End If

                        Next

                        objSalesOrder.Get_Total()

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

                .SetSatus()
                If UG.ActiveRow.Cells("DocType").Value = 5 Then  'Purchase Order
                    If UG.ActiveRow.Cells("DocState").Value = 4 Then
                        .tsbProcessPO.Enabled = False
                        .tsbSavePO.Enabled = False
                        .tsbUpdatePO.Enabled = False
                        '.UG.Enabled = False
                    Else
                        .UG.Enabled = True
                    End If
                    If UG.ActiveRow.Cells("InvNumber").Value = "" Then
                        .GET_NEXT_GRV_NO()
                    End If
                ElseIf UG.ActiveRow.Cells("DocType").Value = 4 Then 'Sales Order
                    If UG.ActiveRow.Cells("DocState").Value = 4 Then
                        .tsbSaveSO.Enabled = False
                        .tsbSOPlaceOrder.Enabled = False
                        .tsbSOQuote.Enabled = False
                        .tsbUpdateSO.Enabled = False
                        'UG.DisplayLayout.Bands(0).Override.SelectTypeRow 
                        '.UG.Enabled = False
                    Else
                        .UG.Enabled = True
                        .GET_NEXT_DELIVERY_NO()
                        .GET_NEXT_INVOICE_NO()
                    End If
                    .cmbCustomer.Enabled = False
                End If
                .tsbSaveSO.Enabled = True
                .Show()
                .Delete_Row()
            End With

            objSalesOrder = Nothing
            Me.Close()
        End If
    End Sub
    Private Sub Set_IBTReceive_Data()

        Dim dr2 As DataRow
        Dim i As Integer
        Dim bHideCost As Boolean = False

        If UG.ActiveRow.Selected = True Then


            Dim objSalesOrder As New frmIBTReceive


            With objSalesOrder

                'If iDocType = DocType.SalesOrder Then
                '    .iDocType = DocType.SalesOrder
                '    .Discard()
                '    .DocumentSettings(DocType.SalesOrder)
                '    .Text = "Sales Order"
                '    .IsNew = True
                '    Dim Dr As DataRow
                '    For Each Dr In dsManu.Tables(0).Rows
                '        If .tsbSaveSO.Tag = Dr("iManu") Then
                '            .tsbSaveSO.Visible = True
                '        ElseIf .tsbOpen.Tag = Dr("iManu") Then
                '            .tsbOpen.Visible = True
                '        ElseIf .tsbUpdateSO.Tag = Dr("iManu") Then
                '            .tsbUpdateSO.Visible = True
                '        ElseIf 22213 = Dr("iManu") Then
                '            .UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
                '            .UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
                '            .UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default
                '        End If
                '    Next
                '    .lblBarcode.Visible = True
                '    .txtBarCode.Visible = True
                '    bHideCost = False

                '    .Show()
                If iDocType = DocType.PurchaseOrder Then
                    .iDocType = DocType.IBTReceive
                    .Discard()
                    .DocumentSettings(DocType.PurchaseOrder)
                    .Text = "IBT Receive"
                    .IsNew = True
                    Dim Dr As DataRow
                    For Each Dr In dsManu.Tables(0).Rows
                        If .tsbSavePO.Tag = Dr("iManu") Then
                            .tsbSavePO.Visible = True
                        ElseIf .tsbOpen.Tag = Dr("iManu") Then
                            .tsbOpen.Visible = True
                        ElseIf .tsbUpdatePO.Tag = Dr("iManu") Then
                            .tsbUpdatePO.Visible = True
                        ElseIf .tsbProcessPO.Tag = Dr("iManu") Then
                            .tsbProcessPO.Visible = True
                        ElseIf 22243 = Dr("iManu") Then
                            .UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
                            .UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
                            .UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default
                        End If
                    Next

                    .lblBarcode.Visible = True

                    .txtBarCode.Visible = True

                    bHideCost = True

                    .Show()

                End If

                .IsNew = False

                .lblAutoID.Text = UG.ActiveRow.Cells("AutoIndex").Value
                .iPONo = UG.ActiveRow.Cells("AutoIndex").Value
                '.cmbCustomer.Value = UG.ActiveRow.Cells("AccountID").Value
                .cmbCustomer.Value = 279 'RTSAM supplier
                '.cmbProject.Value = UG.ActiveRow.Cells("ProjectID").Value
                .txtDescription.Text = UG.ActiveRow.Cells("Description").Value
                .txtExtOrder.Text = UG.ActiveRow.Cells("ExtOrderNum").Value
                .dtpInDate.Value = UG.ActiveRow.Cells("InvDate").Value
                .dtpOrdDate.Value = UG.ActiveRow.Cells("OrderDate").Value
                .dtpDeliDate.Value = UG.ActiveRow.Cells("DeliveryDate").Value
                .cmbSaleRep.Value = UG.ActiveRow.Cells("DocRepID").Value
                '.cmbSalesOpp.Value = UG.ActiveRow.Cells("iOpportunityID").Value
                .txtOrdNo.Text = UG.ActiveRow.Cells("OrderNum").Value
                .txtInNo.Text = UG.ActiveRow.Cells("InvNumber").Value
                .txtDelNote_SupInvNo.Text = UG.ActiveRow.Cells("DeliveryNote").Value
                .cmbType.Value = UG.ActiveRow.Cells("Type").Value

                .bHideCost = IIf(IsDBNull(UG.ActiveRow.Cells("CheckIN").Value), False, UG.ActiveRow.Cells("CheckIN").Value)

                .DeleteRows()

                If Not IsDBNull(UG.ActiveRow.Cells("CheckIN").Value) Then
                    If UG.ActiveRow.Cells("CheckIN").Value = True And sAgent.ToUpper <> "ADMIN" Then
                        objSalesOrder.UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
                        objSalesOrder.UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
                        objSalesOrder.UG.DisplayLayout.Bands(0).Columns("OrderTotal_Excl").Hidden = True
                        objSalesOrder.UG.DisplayLayout.Bands(0).Columns("OrderTotal_incl").Hidden = True
                        objSalesOrder.UG.DisplayLayout.Bands(0).Columns("LineTotal_Excl").Hidden = True
                        objSalesOrder.UG.DisplayLayout.Bands(0).Columns("LineTotal_incl").Hidden = True
                        objSalesOrder.UG.DisplayLayout.Bands(0).Columns("OrderTax").Hidden = True
                        objSalesOrder.UG.DisplayLayout.Bands(0).Columns("LineTax").Hidden = True
                        objSalesOrder.UG.DisplayLayout.Bands(1).Columns("Received").Hidden = True
                        objSalesOrder.UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default
                    End If
                End If

                SQL = "SELECT    *, StkItem.Code FROM _btblInvoiceLines INNER JOIN StkItem ON _btblInvoiceLines.iStockCodeID = StkItem.StockLink WHERE iInvoiceID =" & UG.ActiveRow.Cells("AutoIndex").Value & ""
                Dim objSQL As New clsSqlConn

                With objSQL
                    Try
                        DS = New DataSet
                        DS = .Get_Data_Sql(SQL)
                        For Each Dr In DS.Tables(0).Rows
                            Dim Row As UltraGridRow = objSalesOrder.UG.DisplayLayout.Bands(0).AddNew
                            Row.Cells("Line").Value = Dr.Item("iLineID")

                            Row.Cells("Code").Value = Dr.Item("iStockCodeID")

                            Row.Cells("LineID").Value = Dr.Item("idInvoiceLines")

                            Row.Cells("Description_1").Value = Dr.Item("cDescription")
                            Row.Cells("Description_2").Value = Dr.Item("Code")
                            Row.Cells("Warehouse").Value = Dr.Item("iWarehouseID")
                            Row.Cells("Quantity").Value = Dr.Item("fQuantity")
                            Row.Cells("ConfirmQty").Value = Dr.Item("fQtyToProcess")
                            Row.Cells("ConfirmQty").Value = IIf(Dr.Item("bIsSerialItem") = True, 0, Dr.Item("fQtyToProcess"))

                            Row.Cells("ProcessedQty").Value = Dr.Item("fQtyProcessed")

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
                            'Row.Cells("LineNote").Value = Dr.Item("cLineNotes")
                            'Row.Cells("IsSerialNo").Value = IIf(Dr.Item("bIsSerialItem") = True, True, False)


                            SQL = "SELECT    iLotID,  cLotNumber,       dLotExpiryDate FROM         _btblInvoiceLines  " & _
                                   " WHERE  iInvoiceID = " & UG.ActiveRow.Cells("AutoIndex").Value & " AND iStockCodeID = " & Dr.Item("iStockCodeID")

                            DS1 = New DataSet
                            DS1 = .Get_Data_Sql(SQL)

                            Row.Cells("Lot").Value = DS1.Tables(0).Rows(0)(0).ToString()

                            'Row.Cells("SerialLot").Value = DS1.Tables(0).Rows(0)(1)
                            'Row.Cells("Lot_Exp").Value = DS1.Tables(0).Rows(0)(2)


                            '----------------------------------------------------------------------------------------------------
                            'SQL = "SELECT     sbSerialMF.* FROM sbSerialMF  " & _
                            '        " WHERE  AutoIndex = " & UG.ActiveRow.Cells("AutoIndex").Value & " AND SNStockLink = " & Dr.Item("iStockCodeID") ' & " and PrimaryLineID = " & Row.Cells("Line").Value

                            'DS1 = New DataSet
                            'DS1 = .Get_Data_Sql(SQL)

                            'Dim ugR As UltraGridRow

                            'i = 0

                            'For Each dr2 In DS1.Tables(0).Rows

                            '    If Row.Band.Index = 0 Then
                            '        ugR = Row.ChildBands(0).Band.AddNew
                            '    Else
                            '        ugR = UG.DisplayLayout.Bands(1).AddNew
                            '    End If

                            '    ugR.Cells("LN").Value = i + 1
                            '    ugR.Cells("SerialNumber").Value = dr2("SerialNumber")
                            '    ugR.Cells("SNStockLink").Value = Dr.Item("iStockCodeID")
                            '    ugR.Cells("PrimaryLineID").Value = ugR.ParentRow.Cells("Line").Value
                            '    'ugR.Cells("Received").Value = IIf(dr2("Received") = True, 1, 0)
                            '    ugR.Cells("Received").Value = 1
                            '    'ugR.ParentRow.Cells("ConfirmQty").Value = ugR.ParentRow.Cells("ConfirmQty").Value + IIf(dr2("Received") = True, 1, 0)

                            '    i = i + 1

                            'Next
                            '---------------------------------------------------------------------------------------------------------------------
                        Next

                        For Each ugR In objSalesOrder.UG.Rows
                            ugR.Expanded = False
                        Next

                        objSalesOrder.Get_Total()

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

                .SetSatus()

                If UG.ActiveRow.Cells("DocType").Value = 5 Then  'Purchase Order
                    If UG.ActiveRow.Cells("DocState").Value = 4 Then
                        .tsbProcessPO.Enabled = False
                        .tsbSavePO.Enabled = False
                        .tsbUpdatePO.Enabled = False
                        '.UG.Enabled = False
                    Else
                        .UG.Enabled = True
                    End If
                    If UG.ActiveRow.Cells("InvNumber").Value = "" Then
                        .GET_NEXT_GRV_NO()
                    End If
                ElseIf UG.ActiveRow.Cells("DocType").Value = 4 Then 'Sales Order
                    If UG.ActiveRow.Cells("DocState").Value = 4 Then
                        .tsbSaveSO.Enabled = False
                        .tsbSOPlaceOrder.Enabled = False
                        .tsbSOQuote.Enabled = False
                        .tsbUpdateSO.Enabled = False
                        '.UG.Enabled = False
                    Else
                        .UG.Enabled = True
                        .GET_NEXT_DELIVERY_NO()
                        .GET_NEXT_INVOICE_NO()
                    End If
                    .cmbCustomer.Enabled = False
                End If

                .Show()
                .Delete_Row()
            End With

            objSalesOrder = Nothing
            Me.Close()
        End If
    End Sub

    Private Sub Set_IBT_Data()

        Dim dr2 As DataRow
        Dim i As Integer
        Dim bHideCost As Boolean = False

        If UG.ActiveRow.Selected = True Then
            Dim objSalesOrder As New frmInvoice
            With objSalesOrder

                If iDocType = DocType.SalesOrder Then
                    .bValChange = False
                    .iDocType = DocType.Invoice
                    .Discard()
                    .tsbUpdateSO.Enabled = False
                    .tsbValidate.Enabled = True
                    '.DocumentSettings(DocType.SalesOrder)
                    .Text = "IBT"
                    .IsNew = True
                    Dim Dr As DataRow
                    For Each Dr In dsManu.Tables(0).Rows
                        'If .tsbSaveSO.Tag = Dr("iManu") Then
                        '    .tsbSaveSO.Visible = True
                        'Else
                        If .tsbOpen.Tag = Dr("iManu") Then
                            .tsbOpen.Visible = True
                        ElseIf .tsbUpdateSO.Tag = Dr("iManu") Then
                            .tsbUpdateSO.Visible = True
                        ElseIf 22213 = Dr("iManu") Then
                            .UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
                            .UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
                            .UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default
                        End If
                    Next
                    .lblBarcode.Visible = True
                    .txtBarCode.Visible = True
                    bHideCost = False


                    '.UG.DisplayLayout.Bands(0).Columns("Quantity").CellActivation = Activation.Disabled
                    .Show()

                End If
                .tsbNewlIne.Enabled = False

                If CB2.Checked = True Then
                    .tsbDeleteLine.Enabled = True
                Else
                    .tsbDeleteLine.Enabled = False
                End If

                .IsNew = False
                .lblAutoID.Text = UG.ActiveRow.Cells("AutoIndex").Value
                .iSONo = UG.ActiveRow.Cells("AutoIndex").Value
                If CB2.Checked = False Then
                    .cmbCustomer.Value = UG.ActiveRow.Cells("AccountID").Value
                Else
                    .cmbCustomer.Value = POLocation
                End If

                '.cmbProject.Value = UG.ActiveRow.Cells("ProjectID").Value
                .txtDescription.Text = UG.ActiveRow.Cells("Description").Value
                .txtExtOrder.Text = UG.ActiveRow.Cells("ExtOrderNum").Value
                .dtpInDate.Value = UG.ActiveRow.Cells("InvDate").Value
                .dtpOrdDate.Value = UG.ActiveRow.Cells("OrderDate").Value
                .dtpDeliDate.Value = UG.ActiveRow.Cells("DeliveryDate").Value
                .cmbSaleRep.Value = UG.ActiveRow.Cells("DocRepID").Value
                '.cmbSalesOpp.Value = UG.ActiveRow.Cells("iOpportunityID").Value
                .txtOrdNo.Text = UG.ActiveRow.Cells("OrderNum").Value
                .txtInNo.Text = UG.ActiveRow.Cells("InvNumber").Value


                If UG.ActiveRow.Cells("AccountID").Value <> 452 And UG.ActiveRow.Cells("AccountID").Value <> 453 And UG.ActiveRow.Cells("AccountID").Value <> 454 And UG.ActiveRow.Cells("AccountID").Value <> 455 Then
                    UG.ActiveRow.Cells("AccountID").Value = POLocation
                    .cmbCustomer.Text = POLocation
                End If


                If UG.ActiveRow.Cells("AccountID").Value = 452 Then 'nawinna
                    .cmbGRVLoc.Text = "dbNawinna_new"
                    clientWH = 3
                ElseIf UG.ActiveRow.Cells("AccountID").Value = 453 Then 'kurunegala
                    .cmbGRVLoc.Text = "dbUdawatta_new"
                    clientWH = 4
                ElseIf UG.ActiveRow.Cells("AccountID").Value = 454 Then 'kiribathgoda
                    .cmbGRVLoc.Text = "dbNawinna_new"
                    clientWH = 18
                ElseIf UG.ActiveRow.Cells("AccountID").Value = 455 Then
                    .cmbGRVLoc.Text = "dbUdawatta_new"
                    clientWH = 2
                ElseIf UG.ActiveRow.Cells("cAccountName").Value = "UDAWATTA MOTORS PVT LTD - KEL" Then
                    .cmbGRVLoc.Text = "dbKelaniya_new"
                    clientWH = 0
                ElseIf UG.ActiveRow.Cells("AccountID").Value = 633 Then 'matara
                    .cmbGRVLoc.Text = "dbUdawatta_new"
                    clientWH = 18
                ElseIf UG.ActiveRow.Cells("AccountID").Value = 640 Then 'negombo
                    .cmbGRVLoc.Text = "dbNawinna_new"
                    clientWH = 4
                End If




                '' '' ''.cmbGRVLoc.Text = IIf(IsDBNull(UG.ActiveRow.Cells("ucIDSOrddbName").Value), "aa", UG.ActiveRow.Cells("ucIDSOrddbName").Value)
                '.txtDelNote_SupInvNo.Text = UG.ActiveRow.Cells("DeliveryNote").Value
                '.cmbType.Value = UG.ActiveRow.Cells("Type").Value
                '.bHideCost = IIf(IsDBNull(UG.ActiveRow.Cells("CheckIN").Value), False, UG.ActiveRow.Cells("CheckIN").Value)

                .DeleteRows()

                'SQL = "SELECT     _btblInvoiceLines.*, StkItem.cSimpleCode FROM _btblInvoiceLines INNER JOIN StkItem ON _btblInvoiceLines.iStockCodeID = StkItem.StockLink WHERE iInvoiceID =" & UG.ActiveRow.Cells("AutoIndex").Value & ""
                SQL = " SELECT  _btblInvoiceLines.* , StkItem.cSimpleCode ,   WhseStk.WHMax_Lvl FROM         _btblInvoiceLines INNER JOIN    StkItem ON _btblInvoiceLines.iStockCodeID = StkItem.StockLink INNER JOIN   WhseStk ON StkItem.StockLink = WhseStk.WHStockLink  WHERE iInvoiceID =" & UG.ActiveRow.Cells("AutoIndex").Value & " and WhseStk.WHWhseID = " & clientWH & ""

                Dim objSQL As New clsSqlConn

                With objSQL
                    Try
                        DS = New DataSet
                        DS = .Get_Data_Sql(SQL)
                        For Each Dr In DS.Tables(0).Rows
                            Dim Row As UltraGridRow = objSalesOrder.UG.DisplayLayout.Bands(0).AddNew


                            'Row.Cells("Warehouse").Value = Dr.Item("iWarehouseID")
                            Row.Cells("Warehouse").Value = clientWH


                            Row.Cells("cSimpleCode").Value = Dr.Item("cSimpleCode")
                            Row.Cells("Line").Value = Dr.Item("iLineID")
                            Row.Cells("StockID").Value = Dr.Item("iStockCodeID")
                            objSalesOrder.bValChange = True
                            Row.Cells("Code").Value = Dr.Item("iStockCodeID")
                            objSalesOrder.bValChange = False

                            Row.Cells("LineID").Value = Dr.Item("idInvoiceLines")
                            Row.Cells("Description_1").Value = Dr.Item("cDescription")
                            Row.Cells("Description_2").Value = Dr.Item("cDescription")


                            objSalesOrder.bValChange = True

                            Row.Cells("TaxType").Value = Dr.Item("iTaxTypeID")

                            objSalesOrder.bValChange = False

                            Row.Cells("AvailableQty").Value = Get_Available_Qty("SELECT WHQtyOnHand FROM WhseStk WHERE WHStockLink =" & Row.Cells("StockID").Value & " AND WHWhseID =" & Row.Cells("Warehouse").Value & "")

                            Dim lotsql As String = "SELECT     _etblLotTrackingQty.fQtyOnHand " & _
                                                    " FROM         StkItem INNER JOIN   _etblLotTracking ON StkItem.StockLink = _etblLotTracking.iStockID INNER JOIN " & _
                                                    "  _etblLotTrackingQty ON _etblLotTracking.idLotTracking = _etblLotTrackingQty.iLotTrackingID " & _
                                                    " where StkItem.StockLink = " & Row.Cells("StockID").Value & " and  _etblLotTracking.idLotTracking = " & Dr.Item("iLotID") & " and  _etblLotTrackingQty.iWarehouseID = " & Row.Cells("Warehouse").Value & ""
                            Dim lotqty As Integer = Get_Available_Qty(lotsql)



                            'check max reorder level-----------------------------
                            Try
                                'If IsNew = True Then
                                If sSQLSrvDataBase = "dbKelaniya_new" Then
                                    Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & objSalesOrder.cmbGRVLoc.Text & "")
                                Else
                                    Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & sSQLSrvDataBase & "")
                                End If

                                objSQL = New clsSqlConn


                                With objSQL
                                    'SQL = "SELECT " & lvl & " FROM StkItem WHERE description_1 ='" & e.Cell.Row.Cells("description_1").Value & "' and ItemActive = 1 "
                                    SQL = "SELECT    WhseStk.WHMax_Lvl  FROM         StkItem INNER JOIN  WhseStk ON StkItem.StockLink = WhseStk.WHStockLink WHERE StkItem.description_1 ='" & Row.Cells("description_1").Value & "' and StkItem.ItemActive = 1 AND WhseStk.WHWhseID = " & clientWH & " "
                                    CMD = New SqlCommand(SQL, Con2)
                                    CMD.CommandType = CommandType.Text
                                    DA = New SqlDataAdapter(CMD)
                                    DS = New DataSet
                                    Con2.Open()
                                    DA.Fill(DS)
                                    Con2.Close()
                                    If DS.Tables(0).Rows.Count > 0 Then
                                        Row.Cells("WHMax_Lvl").Value = DS.Tables(0).Rows(0)(0)
                                    End If
                                End With
                            Catch ex As Exception
                                MsgBox("No Maximum level in branch for item: '" & Row.Cells("description_1").Value & "'")
                                Exit Sub
                            Finally
                                If Con1.State = ConnectionState.Open Then Con1.Close()
                            End Try

                            'End If
                            '---------------------------------------------------------------


                            'Row.Cells("cSimpleCode").Value = Dr.Item("cSimpleCode").Value
                            If sSQLSrvDataBase = "dbKelaniya_new" Then
                                Row.Cells("BranchQty").Value = Get_Open_Branch_Qty(objSalesOrder.cmbGRVLoc.Text, "SELECT     WhseStk.WHQtyOnHand + WhseStk.WHQtyOnPO   FROM         WhseStk INNER JOIN WhseMst ON WhseStk.WHWhseID = WhseMst.WhseLink INNER JOIN StkItem ON WhseStk.WHStockLink = StkItem.StockLink  WHERE cSimpleCode ='" & Row.Cells("cSimpleCode").Value & "' AND WhseLink = " & clientWH & " ")
                            Else
                                Row.Cells("BranchQty").Value = Get_Open_Branch_Qty(objSalesOrder.cmbGRVLoc.Text, "SELECT     WhseStk.WHQtyOnHand                    FROM         WhseStk INNER JOIN WhseMst ON WhseStk.WHWhseID = WhseMst.WhseLink INNER JOIN StkItem ON WhseStk.WHStockLink = StkItem.StockLink  WHERE cSimpleCode ='" & Row.Cells("cSimpleCode").Value & "' AND DefaultWhse = 1 ")
                            End If




                            If CB2.Checked = True And (sSQLSrvDataBase = "dbKelaniya_new") Then 'loading IMP PO to branches
                                If CInt(IIf(IsDBNull(Row.Cells("WHMax_Lvl").Value), 0, Row.Cells("WHMax_Lvl").Value)) - CInt(Row.Cells("BranchQty").Value) < 0 Then
                                    Row.Cells("Quantity").Value = 0
                                Else
                                    If CInt(IIf(IsDBNull(Row.Cells("WHMax_Lvl").Value), 0, Row.Cells("WHMax_Lvl").Value)) - CInt(Row.Cells("BranchQty").Value) > lotqty Then
                                        Row.Cells("Quantity").Value = lotqty
                                    Else
                                        Row.Cells("Quantity").Value = CInt(IIf(IsDBNull(Row.Cells("WHMax_Lvl").Value), 0, Row.Cells("WHMax_Lvl").Value)) - CInt(Row.Cells("BranchQty").Value)
                                    End If

                                End If
                            ElseIf CB2.Checked = False And (sSQLSrvDataBase = "dbKelaniya_new") Then 'loading IBT request to branches
                                If CInt(IIf(IsDBNull(Row.Cells("WHMax_Lvl").Value), 0, Row.Cells("WHMax_Lvl").Value)) - CInt(Row.Cells("BranchQty").Value) < 0 Then
                                    Row.Cells("Quantity").Value = 0
                                Else
                                    Row.Cells("Quantity").Value = CInt(IIf(IsDBNull(Row.Cells("WHMax_Lvl").Value), 0, Row.Cells("WHMax_Lvl").Value)) - CInt(Row.Cells("BranchQty").Value)
                                End If

                                If CInt(Row.Cells("AvailableQty").Value) <= Row.Cells("Quantity").Value Then
                                    Row.Cells("Quantity").Value = CInt(Row.Cells("AvailableQty").Value)
                                    'Else
                                    '    Row.Cells("Quantity").Value = Dr.Item("fQuantity")
                                End If

                            Else 'loading automatic PO from branches for IBT request
                                If CInt(IIf(IsDBNull(Row.Cells("WHMax_Lvl").Value), 0, Row.Cells("WHMax_Lvl").Value)) - CInt(Row.Cells("AvailableQty").Value) < 0 Then
                                    Row.Cells("Quantity").Value = 0
                                Else
                                    Row.Cells("Quantity").Value = CInt(IIf(IsDBNull(Row.Cells("WHMax_Lvl").Value), 0, Row.Cells("WHMax_Lvl").Value)) - CInt(Row.Cells("AvailableQty").Value)
                                End If

                                If CInt(Row.Cells("BranchQty").Value) <= Row.Cells("Quantity").Value Then
                                    Row.Cells("Quantity").Value = CInt(Row.Cells("BranchQty").Value)
                                    'Else
                                    '    Row.Cells("Quantity").Value = Dr.Item("fQuantity")
                                End If

                                'UG.DisplayLayout.Bands(0).Columns("DiscountA").CellActivation = Activation.Disabled
                                'Row.Cells("Quantity").Column.CellActivation = Activation.Disabled
                                'Row.Cells("Quantity").Column.CellClickAction = CellClickAction.CellSelect
                            End If

                            'If Row.Cells("Quantity").Value > lotqty Then
                            '    Dim aaa As Integer
                            '    aaa = 234
                            'End If



                            Row.Cells("ConfirmQty").Value = Row.Cells("Quantity").Value 'need same no of quantity
                            'Row.Cells("ConfirmQty").Value = Dr.Item("fQtyToProcess")
                            Row.Cells("ProcessedQty").Value = Dr.Item("fQtyProcessed")
                            If Dr.Item("fQtyLastProcess") <> 0 Then
                                Row.Cells("ConfirmQty").Value = Row.Cells("Quantity").Value
                                'Row.Cells("ConfirmQty").Value = Dr.Item("fQtyLastProcess")
                            End If
                            'Row.Cells("Price_Excl").Value = Math.Round(Dr.Item("fUnitPriceExcl"), 2, MidpointRounding.AwayFromZero)
                            'Row.Cells("Price_Incl").Value = Math.Round(Dr.Item("fUnitPriceIncl"), 2, MidpointRounding.AwayFromZero)
                            'Row.Cells("Discount").Value = Math.Round(Dr.Item("fLineDiscount"), 6, MidpointRounding.AwayFromZero)
                            'Row.Cells("TaxRate").Value = Dr.Item("fTaxRate")

                            Row.Cells("IsWH").Value = IIf(Dr.Item("bIsWhseItem") = True, True, False)
                            'Row.Cells("LineNote").Value = Dr.Item("cLineNotes")
                            'Row.Cells("IsLotItem").Value = IIf(Dr.Item("bIsLotItem") = True, True, False)

                            objSalesOrder.bValChange = True

                            'Row.Cells("Quantity").Value = Dr.Item("fQuantity")
                            Row.Cells("Lot").Value = Dr.Item("iLotID")

                            'Row.Cells("SerialLot").Value = Dr.Item("cLotNumber")
                            'Row.Cells("Lot_Exp").Value = Dr.Item("dLotExpiryDate")
                            objSalesOrder.bValChange = False


                            '' '' ''check max reorder level-----------------------------

                            ' '' ''Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & frmInvoice.cmbGRVLoc.Text & "")

                            ' '' ''objSQL = New clsSqlConn

                            ' '' ''With objSQL
                            ' '' ''    SQL = "SELECT Max_Lvl FROM StkItem WHERE description_1 ='" & UG.ActiveCell.Row.Cells("description_1").Value & "' and ItemActive = 1 "
                            ' '' ''    CMD = New SqlCommand(SQL, Con2)
                            ' '' ''    CMD.CommandType = CommandType.Text
                            ' '' ''    DA = New SqlDataAdapter(CMD)
                            ' '' ''    DS = New DataSet
                            ' '' ''    Con2.Open()
                            ' '' ''    DA.Fill(DS)
                            ' '' ''    Con2.Close()

                            ' '' ''    If DS.Tables(0).Rows.Count > 0 Then
                            ' '' ''        If UG.ActiveCell.Row.Cells("Quantity").Value + UG.ActiveCell.Row.Cells("BranchQty").Value > DS.Tables(0).Rows(0)(0) Then
                            ' '' ''            'MessageBox.Show("Maximum Quantity allowed is " & DS.Tables(0).Rows(0)(0) & "", "Pastel Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            ' '' ''            If DS.Tables(0).Rows(0)(0) - UG.ActiveCell.Row.Cells("BranchQty").Value < 0 Then
                            ' '' ''                UG.ActiveCell.Row.Cells("Quantity").Value = 0
                            ' '' ''            Else
                            ' '' ''                UG.ActiveCell.Row.Cells("Quantity").Value = DS.Tables(0).Rows(0)(0) - UG.ActiveCell.Row.Cells("BranchQty").Value
                            ' '' ''            End If
                            ' '' ''            Exit Sub
                            ' '' ''        End If
                            ' '' ''    End If

                            ' '' ''End With
                            '' '' ''----------------------------------------------------
                            '' '' ''check max reorder level-----------------------------



                            ' '' ''Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & frmInvoice.cmbGRVLoc.Text & "")

                            ' '' ''objSQL = New clsSqlConn

                            ' '' ''With objSQL
                            ' '' ''    SQL = "SELECT Max_Lvl FROM StkItem WHERE description_1 ='" & UG.ActiveCell.Row.Cells("description_1").Value & "' and ItemActive = 1 "
                            ' '' ''    CMD = New SqlCommand(SQL, Con2)
                            ' '' ''    CMD.CommandType = CommandType.Text
                            ' '' ''    DA = New SqlDataAdapter(CMD)
                            ' '' ''    DS = New DataSet
                            ' '' ''    Con2.Open()
                            ' '' ''    DA.Fill(DS)
                            ' '' ''    Con2.Close()
                            ' '' ''    If DS.Tables(0).Rows.Count > 0 Then
                            ' '' ''        UG.ActiveCell.Row.Cells("Max_Lvl").Value = DS.Tables(0).Rows(0)(0)
                            ' '' ''    End If
                            ' '' ''End With


                            '' '' ''---------------------------------------------------------------
                            'Row.Cells("BranchQty").Value = Get_Branch_Qty("SELECT     WhseStk.WHQtyOnHand + WhseStk.WHQtyOnPO FROM         WhseStk INNER JOIN WhseMst ON WhseStk.WHWhseID = WhseMst.WhseLink INNER JOIN StkItem ON WhseStk.WHStockLink = StkItem.StockLink  WHERE cSimpleCode ='" & Row.Cells("cSimpleCode").Value & "' AND DefaultWhse = 1 ")


                            '    SQL = "SELECT  sbSerialMF.* FROM sbSerialMF  " & _
                            '            " WHERE  AutoIndex = " & UG.ActiveRow.Cells("AutoIndex").Value & " AND SNStockLink = " & Dr.Item("iStockCodeID")

                            '    DS1 = New DataSet
                            '    DS1 = .Get_Data_Sql(SQL)

                            '    Dim ugR As UltraGridRow

                            '    i = 0

                            '    'UG.ActiveRow.Cells("SerialLot").ToolTipText = UG.HasChildren.

                            '    For Each dr2 In DS1.Tables(0).Rows

                            '        If Row.Band.Index = 0 Then
                            '            ugR = Row.ChildBands(0).Band.AddNew
                            '        Else
                            '            ugR = UG.DisplayLayout.Bands(1).AddNew
                            '        End If
                            '        ugR.Cells("LN").Value = i + 1
                            '        'ugR.Cells("SerialNumber").Value = dr2("SerialNumber")
                            '        ugR.Cells("SNStockLink").Value = Dr.Item("iStockCodeID")
                            '        ugR.Cells("PrimaryLineID").Value = ugR.ParentRow.Cells("Line").Value
                            '        i = i + 1
                            '    Next

                            'Next

                            'For Each ugR In objSalesOrder.UG.Rows
                            '    ugR.Expanded = False
                            If Row.Cells("Quantity").Value = 0 Then
                                Row.Appearance.BackColor = Color.Yellow
                            End If

                            If Row.Cells("Quantity").Value = 0 And (sSQLSrvDataBase <> "dbKelaniya_new") Then
                                Row.Delete(False)
                            End If


                        Next

                        objSalesOrder.bValChange = False

                        objSalesOrder.Get_Total()

                        objSalesOrder.bValChange = True

                        If UG.ActiveRow.Cells("DocType").Value = 4 Then 'IBT Issue
                            If UG.ActiveRow.Cells("DocState").Value = 4 Then
                                'objSalesOrder.tsbSave.Enabled = False
                                'objSalesOrder.tsbSOPlaceOrder.Enabled = False
                                'objSalesOrder.tsbSOQuote.Enabled = False
                                objSalesOrder.tsbUpdateSO.Enabled = False
                                'objSalesOrder.UG.Enabled = False
                            Else
                                objSalesOrder.UG.Enabled = True
                                objSalesOrder.GET_NEXT_DELIVERY_NO()
                                objSalesOrder.GET_NEXT_INVOICE_NO()
                            End If
                            'objSalesOrder.cmbCustomer.Enabled = False
                        End If


                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
                        Exit Sub
                    Finally
                        .Con_Close()
                        objSQL = Nothing
                    End Try

                End With
                .IsNew = False
                '.SetSatus()

                .Show()
                .Delete_Row()
                .cmbCustomer.Enabled = False
            End With
            Me.Close()
            objSalesOrder = Nothing
        End If
         
    End Sub
    Private Function Get_Branch_Qty(ByVal SQL As String) As Double
        Try
            Dim oQty As Object
            'Con1.ConnectionString = sConStr
            Dim fibt As New frmInvoice
            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= " & fibt.cmbGRVLoc.Text & "  ")
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
    Private Function Get_Open_Branch_Qty(ByVal loc As String, ByVal SQL As String) As Double
        Try
            Dim oQty As Object
            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= " & loc & "  ")
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

    Private Sub frmOpen_MaximumSizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MaximumSizeChanged

    End Sub

    Private Sub UG_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UG.InitializeLayout

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB2.CheckedChanged
        If frmInvoice.cmbCustomer.Value = 0 And CB2.Checked = True Then
            MessageBox.Show("Please select customer", "Evolution AddOn", MessageBoxButtons.OK)
            CB2.Checked = False
        End If
        If CB2.Checked = True Then
            cmbOrdType.Enabled = True
        Else
            cmbOrdType.Enabled = False
        End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsSI.Click
        Dim PO As String = ""
        Dim objSQL As New clsSqlConn
        Dim i As Integer = 0
        While i < UG.Selected.Rows.Count
            PO = PO + " : " + UG.Selected.Rows(i).Cells("OrderNum").Value
            i = i + 1
        End While
        PO = PO + " ? "
        If MsgBox("Are you sure you want to combine Purchase Orders" + PO, MsgBoxStyle.OkCancel, "Evolution AddOn") = MsgBoxResult.Ok Then

            i = 0
            Dim index As Integer = 0

            With objSQL
                Try
                    .Begin_Trans()

                    index = UG.Selected.Rows(i).Cells(0).Value
                    SQL = " insert into InvNum ( DocType, DocVersion, DocState, DocFlag, OrigDocID, InvNumber, GrvNumber, GrvID, AccountID, Description, InvDate, OrderDate, DueDate, DeliveryDate, " & _
                       " TaxInclusive, Email_Sent, Address1, Address2, Address3, Address4, Address5, Address6, PAddress1, PAddress2, PAddress3, PAddress4, PAddress5, PAddress6, " & _
                     " DelMethodID, DocRepID, OrderNum, DeliveryNote, InvDisc, Message1, Message2, Message3, ProjectID, TillID, POSAmntTendered, POSChange, GrvSplitFixedCost, " & _
                    " GrvSplitFixedAmnt, OrderStatusID, OrderPriorityID, ExtOrderNum, ForeignCurrencyID, InvDiscAmnt, InvDiscAmntEx, InvTotExclDEx, InvTotTaxDEx, InvTotInclDEx, " & _
                    " InvTotExcl, InvTotTax, InvTotIncl, OrdDiscAmnt, OrdDiscAmntEx, OrdTotExclDEx, OrdTotTaxDEx, OrdTotInclDEx, OrdTotExcl, OrdTotTax, OrdTotIncl, bUseFixedPrices, " & _
                    "  iDocPrinted, iINVNUMAgentID, fExchangeRate, fGrvSplitFixedAmntForeign, fInvDiscAmntForeign, fInvDiscAmntExForeign, fInvTotExclDExForeign, " & _
                    "  fInvTotTaxDExForeign, fInvTotInclDExForeign, fInvTotExclForeign, fInvTotTaxForeign, fInvTotInclForeign, fOrdDiscAmntForeign, fOrdDiscAmntExForeign, " & _
                    "  fOrdTotExclDExForeign, fOrdTotTaxDExForeign, fOrdTotInclDExForeign, fOrdTotExclForeign, fOrdTotTaxForeign, fOrdTotInclForeign, cTaxNumber, cAccountName,  " & _
                    "  iProspectID, iOpportunityID, InvTotRounding, OrdTotRounding, fInvTotForeignRounding, fOrdTotForeignRounding, bInvRounding, iInvSettlementTermsID, " & _
                    "  cSettlementTermInvMsg, iOrderCancelReasonID, iLinkedDocID, bLinkedTemplate, InvTotInclExRounding, OrdTotInclExRounding, fInvTotInclForeignExRounding, " & _
                    "  fOrdTotInclForeignExRounding, iEUNoTCID, iPOAuthStatus, iPOIncidentID, iSupervisorID, iMergedDocID, ucIDInvunarea, InvDiscReasonID, InvNum_iBranchID, " & _
                    "  InvNum_dCreatedDate, InvNum_dModifiedDate, InvNum_iCreatedBranchID, InvNum_iModifiedBranchID, InvNum_iCreatedAgentID, InvNum_iModifiedAgentID, " & _
                    " InvNum_iChangeSetID, iDocEmailed, ulIDPOrdType, ubIDPOrdCheckIN, uiIDSOrdSuppID, ucIDSOrddbName, ulIDRtsType" & _
            " ) select  DocType, DocVersion, DocState, DocFlag, OrigDocID, InvNumber, GrvNumber, GrvID, AccountID, Description, InvDate, OrderDate, DueDate, DeliveryDate, " & _
                     " TaxInclusive, Email_Sent, Address1, Address2, Address3, Address4, Address5, Address6, PAddress1, PAddress2, PAddress3, PAddress4, PAddress5, PAddress6, " & _
                     " DelMethodID, DocRepID, OrderNum, DeliveryNote, InvDisc, Message1, Message2, Message3, ProjectID, TillID, POSAmntTendered, POSChange, GrvSplitFixedCost, " & _
                     " GrvSplitFixedAmnt, OrderStatusID, OrderPriorityID, ExtOrderNum, ForeignCurrencyID, InvDiscAmnt, InvDiscAmntEx, InvTotExclDEx, InvTotTaxDEx, InvTotInclDEx, " & _
                     " InvTotExcl, InvTotTax, InvTotIncl, OrdDiscAmnt, OrdDiscAmntEx, OrdTotExclDEx, OrdTotTaxDEx, OrdTotInclDEx, OrdTotExcl, OrdTotTax, OrdTotIncl, bUseFixedPrices, " & _
                     " iDocPrinted, iINVNUMAgentID, fExchangeRate, fGrvSplitFixedAmntForeign, fInvDiscAmntForeign, fInvDiscAmntExForeign, fInvTotExclDExForeign, " & _
                     " fInvTotTaxDExForeign, fInvTotInclDExForeign, fInvTotExclForeign, fInvTotTaxForeign, fInvTotInclForeign, fOrdDiscAmntForeign, fOrdDiscAmntExForeign, " & _
                     " fOrdTotExclDExForeign, fOrdTotTaxDExForeign, fOrdTotInclDExForeign, fOrdTotExclForeign, fOrdTotTaxForeign, fOrdTotInclForeign, cTaxNumber, cAccountName, " & _
                     " iProspectID, iOpportunityID, InvTotRounding, OrdTotRounding, fInvTotForeignRounding, fOrdTotForeignRounding, bInvRounding, iInvSettlementTermsID, " & _
                     " cSettlementTermInvMsg, iOrderCancelReasonID, iLinkedDocID, bLinkedTemplate, InvTotInclExRounding, OrdTotInclExRounding, fInvTotInclForeignExRounding, " & _
                     " fOrdTotInclForeignExRounding, iEUNoTCID, iPOAuthStatus, iPOIncidentID, iSupervisorID, iMergedDocID, ucIDInvunarea, InvDiscReasonID, InvNum_iBranchID, " & _
                     " InvNum_dCreatedDate, InvNum_dModifiedDate, InvNum_iCreatedBranchID, InvNum_iModifiedBranchID, InvNum_iCreatedAgentID, InvNum_iModifiedAgentID, " & _
                     "  InvNum_iChangeSetID, iDocEmailed, ulIDPOrdType, ubIDPOrdCheckIN, uiIDSOrdSuppID, ucIDSOrddbName, ulIDRtsType " & _
                     " from InvNum where AutoIndex = " & UG.Selected.Rows(i).Cells(0).Value & "  "

                    If .Execute_Sql_Trans(SQL) = 0 Then
                        .Rollback_Trans()
                        Exit Sub
                    End If


                    While i < UG.Selected.Rows.Count
                        SQL = " insert into _btblInvoiceLines ( iInvoiceID, iOrigLineID, iGrvLineID, cDescription, iUnitsOfMeasureStockingID, iUnitsOfMeasureCategoryID, iUnitsOfMeasureID, fQuantity, " & _
    "                      fQtyChange, fQtyToProcess, fQtyLastProcess, fQtyProcessed, fQtyReserved, fQtyReservedChange, cLineNotes, fUnitPriceExcl, fUnitPriceIncl, fUnitCost, fLineDiscount, " & _
    "                       fTaxRate, bIsSerialItem, bIsWhseItem, fAddCost, cTradeinItem, iStockCodeID, iJobID, iWarehouseID, iTaxTypeID, iPriceListNameID, fQuantityLineTotIncl, " & _
    "                      fQuantityLineTotExcl, fQuantityLineTotInclNoDisc, fQuantityLineTotExclNoDisc, fQuantityLineTaxAmount, fQuantityLineTaxAmountNoDisc, fQtyChangeLineTotIncl, " & _
    "                      fQtyChangeLineTotExcl, fQtyChangeLineTotInclNoDisc, fQtyChangeLineTotExclNoDisc, fQtyChangeLineTaxAmount, fQtyChangeLineTaxAmountNoDisc, " & _
    "                      fQtyToProcessLineTotIncl, fQtyToProcessLineTotExcl, fQtyToProcessLineTotInclNoDisc, fQtyToProcessLineTotExclNoDisc, fQtyToProcessLineTaxAmount, " & _
    "                      fQtyToProcessLineTaxAmountNoDisc, fQtyLastProcessLineTotIncl, fQtyLastProcessLineTotExcl, fQtyLastProcessLineTotInclNoDisc, " & _
    "                      fQtyLastProcessLineTotExclNoDisc, fQtyLastProcessLineTaxAmount, fQtyLastProcessLineTaxAmountNoDisc, fQtyProcessedLineTotIncl, fQtyProcessedLineTotExcl, " & _
    "                      fQtyProcessedLineTotInclNoDisc, fQtyProcessedLineTotExclNoDisc, fQtyProcessedLineTaxAmount, fQtyProcessedLineTaxAmountNoDisc, fUnitPriceExclForeign, " & _
    "                      fUnitPriceInclForeign, fUnitCostForeign, fAddCostForeign, fQuantityLineTotInclForeign, fQuantityLineTotExclForeign, fQuantityLineTotInclNoDiscForeign, " & _
    "                      fQuantityLineTotExclNoDiscForeign, fQuantityLineTaxAmountForeign, fQuantityLineTaxAmountNoDiscForeign, fQtyChangeLineTotInclForeign, " & _
    "                      fQtyChangeLineTotExclForeign, fQtyChangeLineTotInclNoDiscForeign, fQtyChangeLineTotExclNoDiscForeign, fQtyChangeLineTaxAmountForeign, " & _
    "                      fQtyChangeLineTaxAmountNoDiscForeign, fQtyToProcessLineTotInclForeign, fQtyToProcessLineTotExclForeign, fQtyToProcessLineTotInclNoDiscForeign, " & _
    "                      fQtyToProcessLineTotExclNoDiscForeign, fQtyToProcessLineTaxAmountForeign, fQtyToProcessLineTaxAmountNoDiscForeign, fQtyLastProcessLineTotInclForeign, " & _
    "                      fQtyLastProcessLineTotExclForeign, fQtyLastProcessLineTotInclNoDiscForeign, fQtyLastProcessLineTotExclNoDiscForeign, fQtyLastProcessLineTaxAmountForeign, " & _
    "                      fQtyLastProcessLineTaxAmountNoDiscForeign, fQtyProcessedLineTotInclForeign, fQtyProcessedLineTotExclForeign, fQtyProcessedLineTotInclNoDiscForeign, " & _
    "                      fQtyProcessedLineTotExclNoDiscForeign, fQtyProcessedLineTaxAmountForeign, fQtyProcessedLineTaxAmountNoDiscForeign, iLineRepID, iLineProjectID, " & _
    "                      iLedgerAccountID, iModule, bChargeCom, bIsLotItem, iLotID, cLotNumber, dLotExpiryDate, iMFPID, iLineID, iLinkedLineID, fQtyLinkedUsed, " & _
    "                      iUnitPriceOverrideReasonID, iLineDiscountReasonID, iReturnReasonID, iLineDocketMode, _btblInvoiceLines_iBranchID, _btblInvoiceLines_dCreatedDate, " & _
    "                      _btblInvoiceLines_dModifiedDate, _btblInvoiceLines_iCreatedBranchID, _btblInvoiceLines_iModifiedBranchID, _btblInvoiceLines_iCreatedAgentID, " & _
    "                      _btblInvoiceLines_iModifiedAgentID, _btblInvoiceLines_iChangeSetID, fUnitPriceInclOrig, fUnitPriceExclOrig, fUnitPriceInclForeignOrig, fUnitPriceExclForeignOrig, " & _
    "                    ulIDRtsTxCMType " & _
    " ) select  (select MAX(AutoIndex) from InvNum), iOrigLineID, iGrvLineID, cDescription, iUnitsOfMeasureStockingID, iUnitsOfMeasureCategoryID, iUnitsOfMeasureID, fQuantity, " & _
    "                     fQtyChange, fQtyToProcess, fQtyLastProcess, fQtyProcessed, fQtyReserved, fQtyReservedChange, cLineNotes, fUnitPriceExcl, fUnitPriceIncl, fUnitCost, fLineDiscount," & _
    "                     fTaxRate, bIsSerialItem, bIsWhseItem, fAddCost, cTradeinItem, iStockCodeID, iJobID, iWarehouseID, iTaxTypeID, iPriceListNameID, fQuantityLineTotIncl, " & _
    "                     fQuantityLineTotExcl, fQuantityLineTotInclNoDisc, fQuantityLineTotExclNoDisc, fQuantityLineTaxAmount, fQuantityLineTaxAmountNoDisc, fQtyChangeLineTotIncl, " & _
    "                     fQtyChangeLineTotExcl, fQtyChangeLineTotInclNoDisc, fQtyChangeLineTotExclNoDisc, fQtyChangeLineTaxAmount, fQtyChangeLineTaxAmountNoDisc, " & _
    "                     fQtyToProcessLineTotIncl, fQtyToProcessLineTotExcl, fQtyToProcessLineTotInclNoDisc, fQtyToProcessLineTotExclNoDisc, fQtyToProcessLineTaxAmount, " & _
    "                     fQtyToProcessLineTaxAmountNoDisc, fQtyLastProcessLineTotIncl, fQtyLastProcessLineTotExcl, fQtyLastProcessLineTotInclNoDisc, " & _
    "                     fQtyLastProcessLineTotExclNoDisc, fQtyLastProcessLineTaxAmount, fQtyLastProcessLineTaxAmountNoDisc, fQtyProcessedLineTotIncl, fQtyProcessedLineTotExcl, " & _
    "                     fQtyProcessedLineTotInclNoDisc, fQtyProcessedLineTotExclNoDisc, fQtyProcessedLineTaxAmount, fQtyProcessedLineTaxAmountNoDisc, fUnitPriceExclForeign, " & _
    "                     fUnitPriceInclForeign, fUnitCostForeign, fAddCostForeign, fQuantityLineTotInclForeign, fQuantityLineTotExclForeign, fQuantityLineTotInclNoDiscForeign, " & _
    "                     fQuantityLineTotExclNoDiscForeign, fQuantityLineTaxAmountForeign, fQuantityLineTaxAmountNoDiscForeign, fQtyChangeLineTotInclForeign, " & _
    "                     fQtyChangeLineTotExclForeign, fQtyChangeLineTotInclNoDiscForeign, fQtyChangeLineTotExclNoDiscForeign, fQtyChangeLineTaxAmountForeign, " & _
    "                     fQtyChangeLineTaxAmountNoDiscForeign, fQtyToProcessLineTotInclForeign, fQtyToProcessLineTotExclForeign, fQtyToProcessLineTotInclNoDiscForeign, " & _
    "                     fQtyToProcessLineTotExclNoDiscForeign, fQtyToProcessLineTaxAmountForeign, fQtyToProcessLineTaxAmountNoDiscForeign, fQtyLastProcessLineTotInclForeign, " & _
    "                     fQtyLastProcessLineTotExclForeign, fQtyLastProcessLineTotInclNoDiscForeign, fQtyLastProcessLineTotExclNoDiscForeign, fQtyLastProcessLineTaxAmountForeign, " & _
    "                     fQtyLastProcessLineTaxAmountNoDiscForeign, fQtyProcessedLineTotInclForeign, fQtyProcessedLineTotExclForeign, fQtyProcessedLineTotInclNoDiscForeign, " & _
    "                     fQtyProcessedLineTotExclNoDiscForeign, fQtyProcessedLineTaxAmountForeign, fQtyProcessedLineTaxAmountNoDiscForeign, iLineRepID, iLineProjectID, " & _
    "                     iLedgerAccountID, iModule, bChargeCom, bIsLotItem, iLotID, cLotNumber, dLotExpiryDate, iMFPID, iLineID, iLinkedLineID, fQtyLinkedUsed, " & _
    "                     iUnitPriceOverrideReasonID, iLineDiscountReasonID, iReturnReasonID, iLineDocketMode, _btblInvoiceLines_iBranchID, _btblInvoiceLines_dCreatedDate, " & _
    "                     _btblInvoiceLines_dModifiedDate, _btblInvoiceLines_iCreatedBranchID, _btblInvoiceLines_iModifiedBranchID, _btblInvoiceLines_iCreatedAgentID, " & _
    "                     _btblInvoiceLines_iModifiedAgentID, _btblInvoiceLines_iChangeSetID, fUnitPriceInclOrig, fUnitPriceExclOrig, fUnitPriceInclForeignOrig, fUnitPriceExclForeignOrig, " & _
    "                   ulIDRtsTxCMType " & _
    " from _btblInvoiceLines where iInvoiceID = " & UG.Selected.Rows(i).Cells(0).Value & " " & _
    " insert into _btblInvoiceLineSN ( iSerialInvoiceID, iSerialInvoiceLineID, cSerialNumber, _btblInvoiceLineSN_iBranchID, _btblInvoiceLineSN_dCreatedDate, " & _
    "                     _btblInvoiceLineSN_dModifiedDate, _btblInvoiceLineSN_iCreatedBranchID, _btblInvoiceLineSN_iModifiedBranchID, " & _
    "                   _btblInvoiceLineSN_iCreatedAgentID, _btblInvoiceLineSN_iModifiedAgentID, _btblInvoiceLineSN_iChangeSetID " & _
    " ) select  (select MAX(AutoIndex) from InvNum), iSerialInvoiceLineID, cSerialNumber, _btblInvoiceLineSN_iBranchID, _btblInvoiceLineSN_dCreatedDate, " & _
    "                    _btblInvoiceLineSN_dModifiedDate, _btblInvoiceLineSN_iCreatedBranchID, _btblInvoiceLineSN_iModifiedBranchID, " & _
    "                 _btblInvoiceLineSN_iCreatedAgentID, _btblInvoiceLineSN_iModifiedAgentID, _btblInvoiceLineSN_iChangeSetID " & _
    " from _btblInvoiceLineSN where iSerialInvoiceID = " & UG.Selected.Rows(i).Cells(0).Value & " " & _
    " insert into _btblInvoiceGrvSplit (iGrvSplitInvoiceID, iGrvSplitVendorID, cGRVSplitReference, cGrvSplitDescription, fGrvSplitAmount, iGrvSplitTaxTypeID, " & _
    "                      fGrvSplitTaxAmnt, iCurrencyID, fForexRate, fForexAmount, _btblInvoiceGrvSplit_iBranchID, _btblInvoiceGrvSplit_dCreatedDate, " & _
    "                      _btblInvoiceGrvSplit_dModifiedDate, _btblInvoiceGrvSplit_iCreatedBranchID, _btblInvoiceGrvSplit_iModifiedBranchID, " & _
    "                    _btblInvoiceGrvSplit_iCreatedAgentID, _btblInvoiceGrvSplit_iModifiedAgentID, _btblInvoiceGrvSplit_iChangeSetID " & _
    " ) select (select MAX(AutoIndex) from InvNum), iGrvSplitVendorID, cGRVSplitReference, cGrvSplitDescription, fGrvSplitAmount, iGrvSplitTaxTypeID, " & _
    "                      fGrvSplitTaxAmnt, iCurrencyID, fForexRate, fForexAmount, _btblInvoiceGrvSplit_iBranchID, _btblInvoiceGrvSplit_dCreatedDate, " & _
    "                      _btblInvoiceGrvSplit_dModifiedDate, _btblInvoiceGrvSplit_iCreatedBranchID, _btblInvoiceGrvSplit_iModifiedBranchID, " & _
    "                    _btblInvoiceGrvSplit_iCreatedAgentID, _btblInvoiceGrvSplit_iModifiedAgentID, _btblInvoiceGrvSplit_iChangeSetID " & _
    " from _btblInvoiceGrvSplit where iGrvSplitInvoiceID = " & UG.Selected.Rows(i).Cells(0).Value & " "
                        '"  insert into sbSerialMF  (AutoIndex, SNStockLink, SerialNumber, PrimaryLineID, Received, bPrinted) " & _
                        '" select  (select MAX(AutoIndex) from InvNum), SNStockLink, SerialNumber, " & i + 1 & ", Received, bPrinted " & _
                        '" from sbSerialMF where AutoIndex = " & UG.Selected.Rows(i).Cells(0).Value & " "

                        If .Execute_Sql_Trans(SQL) = 0 Then
                            .Rollback_Trans()
                            Exit Sub
                        End If

                        i = i + 1
                    End While
                    .Commit_Trans()
                    i = 0
                    .Begin_Trans()
                    While i < UG.Selected.Rows.Count
                        SQL = " DELETE FROM InvNum WHERE AutoIndex = " & UG.Selected.Rows(i).Cells(0).Value & " " & _
                        " DELETE FROM _btblInvoiceLines where iInvoiceID = " & UG.Selected.Rows(i).Cells(0).Value & " " & _
                        " DELETE FROM _btblInvoiceLineSN where iSerialInvoiceID = " & UG.Selected.Rows(i).Cells(0).Value & " " & _
                        " DELETE FROM _btblInvoiceGrvSplit where iGrvSplitInvoiceID = " & UG.Selected.Rows(i).Cells(0).Value & " "
                        '" DELETE FROM sbSerialMF where AutoIndex = " & UG.Selected.Rows(i).Cells(0).Value & " "
                        If .Execute_Sql_Trans(SQL) = 0 Then
                            .Rollback_Trans()
                            Exit Sub
                        End If
                        i = i + 1
                    End While
                    .Commit_Trans()
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
                    Exit Sub
                Finally
                    .Con_Close()
                    objSQL = Nothing
                End Try
                MsgBox("Successfully Combined", MsgBoxStyle.Information, "Evoution AddOn")
            End With
        End If

        '-------------------refresh grid-----------------------------------------------------
        SQL = "Set dateformat dmy SELECT AutoIndex, DocType, DocVersion, DocState, InvNumber,GrvNumber, " & _
                " AccountID, Description,ExtOrderNum,DeliveryNote, InvDate, OrderDate, DueDate, DeliveryDate, " & _
                " DocRepID, OrderNum, ProjectID, iProspectID, iOpportunityID, iDocPrinted," & _
                " bUseFixedPrices,ulIDPOrdType As Type, CASE DocState WHEN 1 THEN 'Unprocessed' WHEN 2 THEN 'Quote' WHEN 3 THEN 'Partial Processed' WHEN 4 THEN 'Archived' END As Status ,  ubIDPOrdCheckIN as CheckIN  FROM InvNum"

        'SQL = "Set dateformat dmy SELECT AutoIndex, InvDate , OrderNum ,InvNumber, DocType,  DocState,   " & _
        '"  ulIDPOrdType As Type ,  CASE DocState WHEN 1 THEN 'Unprocessed' WHEN 2 THEN 'Quote' WHEN 3 THEN 'Partial Processed' WHEN 4 THEN 'Archived' END As Status , ubIDPOrdCheckIN as CheckIN FROM InvNum"


        If cb.Checked = True Then
            If cmbOrdStatus.SelectedIndex = 0 Then
                SQL = SQL & " WHERE DocFlag<>2 AND DocType=5 AND (DocState=1 Or DocState=2 Or DocState=4 Or DocState=3)  AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & dtpOrdDateTo.Value & "' "
            ElseIf cmbOrdStatus.SelectedIndex = 1 Or cmbOrdStatus.SelectedIndex = 2 Then
                SQL = SQL & " WHERE DocFlag<>2 AND DocType=5 AND DocState=1 AND  OrderNum<>'Quote' AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
            ElseIf cmbOrdStatus.SelectedIndex = 3 Then
                SQL = SQL & " WHERE DocFlag<>2 AND DocType=5 AND DocState=2  AND  OrderNum='Quote' AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
            ElseIf cmbOrdStatus.SelectedIndex = 4 Then
                SQL = SQL & " WHERE DocFlag<>2 AND DocType=5 AND DocState=4  AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
            End If
        Else
            If cmbOrdStatus.SelectedIndex = 0 Then
                SQL = SQL & " WHERE DocFlag<>2 AND DocType=5 AND (DocState=1 Or DocState=2 Or DocState=4 Or DocState=3) "
            ElseIf cmbOrdStatus.SelectedIndex = 1 Or cmbOrdStatus.SelectedIndex = 2 Then
                SQL = SQL & " WHERE DocFlag<>2 AND DocType=5 AND DocState=1  AND  OrderNum<>'Quote' "
            ElseIf cmbOrdStatus.SelectedIndex = 3 Then
                SQL = SQL & " WHERE DocFlag<>2 AND DocType=5 AND DocState=2  AND  OrderNum='Quote'  "
            ElseIf cmbOrdStatus.SelectedIndex = 4 Then
                SQL = SQL & " WHERE DocFlag<>2 AND DocType=5 AND DocState=4 "
            End If
        End If
        objSQL = New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                UG.DataSource = DS.Tables(0)
            Catch ex As Exception
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try
        End With
    End Sub

    Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub

    Private Sub cmbAccount_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles cmbAccount.InitializeLayout

    End Sub

    Private Sub ToolStripButton1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim bIsInclusive As Boolean
        Dim objSQL As New clsSqlConn
        With objSQL
            SQL = " SELECT bIsInclusive FROM StDfTbl"
            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
        End With

        Dim Dr As DataRow
        For Each Dr In DS.Tables(0).Rows
            If Dr("bIsInclusive") = False Then
                bIsInclusive = False
            ElseIf Dr("bIsInclusive") = True Then
                bIsInclusive = True
            End If
        Next


        DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
        DatabaseContext.SetLicense("DE09110022", "1428511")
        DatabaseContext.CreateConnection(sSQLSrvName, sSQLSrvDataBase, sSQLSrvUserName, sSQLSrvPassword, False)

        DatabaseContext.BeginTran()

        Dim PurchaseOrder As New PurchaseOrder
        Dim PurchaseOrderDetail As OrderDetail
        'Dim InventoryItem As InventoryItem
        Dim WH As Warehouse

        Dim Supplier As New Supplier(CInt(UG.ActiveRow.Cells("AccountID").Value))

        PurchaseOrder.Account = Supplier
        PurchaseOrder.InvoiceTo = Supplier.PostalAddress
        PurchaseOrder.DeliverTo = Supplier.PhysicalAddress
        PurchaseOrder.ExternalOrderNo = UG.ActiveRow.Cells("ExtOrderNum").Value
        PurchaseOrder.InvoiceDate = UG.ActiveRow.Cells("InvDate").Value
        PurchaseOrder.DeliveryDate = UG.ActiveRow.Cells("DeliveryDate").Value
        PurchaseOrder.OrderDate = UG.ActiveRow.Cells("OrderDate").Value
        PurchaseOrder.SupplierInvoiceNo = UG.ActiveRow.Cells("InvNumber").Value
        PurchaseOrder.UserFields("ulIDPOrdType") = UG.ActiveRow.Cells("Type").Value

        If bIsInclusive = True Then
            PurchaseOrder.TaxMode = TaxMode.Inclusive
        ElseIf bIsInclusive = False Then
            PurchaseOrder.TaxMode = TaxMode.Exclusive
        End If

        SQL = "SELECT * FROM _btblInvoiceLines WHERE iInvoiceID =" & UG.ActiveRow.Cells("AutoIndex").Value & ""
        objSQL = New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                For Each Dr In DS.Tables(0).Rows
                    PurchaseOrderDetail = New OrderDetail
                    'InventoryItem = New InventoryItem(CInt(Dr.Item("iStockCodeID")))
                    PurchaseOrderDetail.InventoryItemID = CInt(Dr.Item("iStockCodeID"))
                    'PurchaseOrderDetail.InventoryItem = InventoryItem
                    PurchaseOrderDetail.Quantity = CDbl(Dr.Item("fQuantity"))
                    'PurchaseOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                    PurchaseOrderDetail.ToProcess = CDbl(Dr.Item("fQuantity"))

                    If bIsInclusive = True Then
                        PurchaseOrderDetail.UnitSellingPrice = Math.Round(Dr.Item("fUnitPriceIncl"), 2, MidpointRounding.AwayFromZero)
                        PurchaseOrderDetail.TaxMode = TaxMode.Inclusive
                        PurchaseOrderDetail.TaxType = New TaxRate(CInt(Dr.Item("iTaxTypeID")))
                    ElseIf bIsInclusive = False Then
                        PurchaseOrderDetail.UnitSellingPrice = Math.Round(Dr.Item("fUnitPriceExcl"), 2, MidpointRounding.AwayFromZero)
                        PurchaseOrderDetail.TaxMode = TaxMode.Exclusive
                        PurchaseOrderDetail.TaxType = New TaxRate(CInt(Dr.Item("iTaxTypeID")))
                    End If


                    'If Dr.Item("bIsWhseItem") = True Then
                    '    WH = New Warehouse(CInt(Dr.Item("iWarehouseID")))
                    '    PurchaseOrderDetail.Warehouse = WH
                    'End If

                    PurchaseOrderDetail.Description = CStr(Dr.Item("cDescription"))
                    PurchaseOrderDetail.DiscountPercent = Math.Round(Dr.Item("fLineDiscount"), 6, MidpointRounding.AwayFromZero)
                    PurchaseOrderDetail.Note = Dr.Item("cLineNotes")
                    PurchaseOrder.Detail.Add(PurchaseOrderDetail)


                    SQL = "SELECT _btblInvoiceLineSN.* FROM _btblInvoiceLineSN  " & _
                        " WHERE  iSerialInvoiceID = " & UG.ActiveRow.Cells("AutoIndex").Value & " AND iSerialInvoiceLineID = " & Dr.Item("idInvoiceLines") ' & " and PrimaryLineID = " & Row.Cells("Line").Value

                    DS1 = New DataSet
                    DS1 = .Get_Data_Sql(SQL)
                    If DS.Tables(0).Rows.Count > 0 Then
                        'Dim dr2 As New DataRow
                        For Each dr2 In DS1.Tables(0).Rows
                            Dim sn As String = Mid(CStr(dr2("cSerialNumber")), 1, InStr(CStr(dr2("cSerialNumber")), "!") - 1)
                            PurchaseOrderDetail.SerialNumbers.Add(sn)
                        Next
                    End If
                Next

                PurchaseOrder.Process()
                DatabaseContext.CommitTran()

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
                'objSalesOrder = Nothing
            End Try
        End With

    End Sub

    Private Sub UltraButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraButton1.Click
        If cmbOrdStatus.SelectedItem = "Quotation" Then
            comDocState = DocState.Quote
        Else
            comDocState = 0
        End If
        If cmbAccount.Text = Nothing Then

            If cmbOrdType.Text = "Supplier Invoice" And CB2.Checked = True Then
                iDocType = DocType.SupplierInvoice
            End If

            DeleteRow()


            SQL = "Set dateformat dmy SELECT AutoIndex, DocType, DocVersion, DocState, InvNumber,GrvNumber, " & _
            " AccountID, cAccountName, Description,ExtOrderNum,DeliveryNote, InvDate, InvTotIncl, " & _
            " DocRepID, OrderNum,  iDocPrinted," & _
            " bUseFixedPrices,ulIDPOrdType As Type , uiIDSOrdSuppID , ucIDSOrddbName , CASE DocState WHEN 1 THEN 'Unprocessed' WHEN 2 THEN 'Quote' WHEN 3 THEN 'Partial Processed' WHEN 4 THEN 'Archived' END As Status , ubIDPOrdCheckIN as CheckIN , OrderDate, DeliveryDate FROM InvNum "

            If iDocType = DocType.SalesOrder Or iDocType = DocType.Invoice Then
                If cb.Checked = True Then
                    SQL = SQL & " WHERE DocType=4 AND ExtOrderNum = 'IBT' AND DocState=1  AND  OrderNum<>'Quote' AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                Else
                    SQL = SQL & " WHERE DocType=4 AND ExtOrderNum = 'IBT'  AND DocState=1  AND  OrderNum<>'Quote' "
                    
                End If
            End If
            'MsgBox("Account Name can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
            'Exit Sub
        Else
            If cmbOrdType.Text = "Supplier Invoice" And CB2.Checked = True Then
                iDocType = DocType.SupplierInvoice
            End If
            DeleteRow()



            SQL = "Set dateformat dmy SELECT AutoIndex, DocType, DocVersion, DocState, InvNumber,GrvNumber, " & _
            " AccountID, Description,ExtOrderNum,DeliveryNote, InvDate, OrderDate, DueDate, DeliveryDate, " & _
            " DocRepID, OrderNum, ProjectID, iProspectID, iOpportunityID, iDocPrinted," & _
            " bUseFixedPrices,ulIDPOrdType As Type, CASE DocState WHEN 1 THEN 'Unprocessed' WHEN 2 THEN 'Quote' WHEN 3 THEN 'Partial Processed' WHEN 4 THEN 'Archived' END As Status ,  ubIDPOrdCheckIN as CheckIN  FROM InvNum"

            If iDocType = DocType.SalesOrder Then
                If cb.Checked = True Then
                    If cmbOrdStatus.SelectedIndex = 0 Then
                        SQL = SQL & " WHERE DocType=4 AND ExtOrderNum = 'IBT'  AND (DocState=1 Or DocState=2 Or DocState=4 Or DocState=3)  AND AccountID=" & cmbAccount.Value & " AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "' "
                    ElseIf cmbOrdStatus.SelectedIndex = 1 Or cmbOrdStatus.SelectedIndex = 2 Then
                        SQL = SQL & " WHERE DocType=4 AND ExtOrderNum = 'IBT'  AND DocState=1 AND   OrderNum<>'Quote' AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    ElseIf cmbOrdStatus.SelectedIndex = 3 Then
                        SQL = SQL & " WHERE DocType=4 AND ExtOrderNum = 'IBT'  AND DocState=2 AND  OrderNum='Quote' AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    ElseIf cmbOrdStatus.SelectedIndex = 4 Then
                        SQL = SQL & " WHERE DocType=4 AND ExtOrderNum = 'IBT'  AND DocState=4 AND InvDate >='" & Format(dtpOrdDateFrom.Value, "dd/MM/yy") & "' AND InvDate <= '" & Format(dtpOrdDateTo.Value, "dd/MM/yy") & "'"
                    End If
                Else
                    If cmbOrdStatus.SelectedIndex = 0 Then
                        SQL = SQL & " WHERE DocType=4 AND ExtOrderNum = 'IBT'  AND (DocState=1 Or DocState=2 Or DocState=4 Or DocState=3) AND AccountID=" & cmbAccount.Value & " "
                    ElseIf cmbOrdStatus.SelectedIndex = 1 Or cmbOrdStatus.SelectedIndex = 2 Then
                        SQL = SQL & " WHERE DocType=4 AND ExtOrderNum = 'IBT'  AND DocState=1 AND  OrderNum<>'Quote'  AND AccountID=" & cmbAccount.Value & " "
                    ElseIf cmbOrdStatus.SelectedIndex = 3 Then
                        SQL = SQL & " WHERE DocType=4 AND ExtOrderNum = 'IBT'  AND DocState=2 AND  OrderNum='Quote'  AND AccountID=" & cmbAccount.Value & "  "
                    ElseIf cmbOrdStatus.SelectedIndex = 4 Then
                        SQL = SQL & " WHERE DocType=4 AND ExtOrderNum = 'IBT'  AND DocState=4 AND  AND AccountID=" & cmbAccount.Value & " "
                    End If
                End If
            

            End If
        End If
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                UG.DataSource = DS.Tables(0)
                If iDocType = DocType.SalesOrder Then
                    If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sAgent & "OPENSO.xml") Then
                        UG.DisplayLayout.LoadFromXml(Application.StartupPath & "\" & sAgent & "OPENSO.xml")
                    End If
                ElseIf iDocType = DocType.PurchaseOrder Then
                    If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sAgent & "OPENPO.xml") Then
                        UG.DisplayLayout.LoadFromXml(Application.StartupPath & "\" & sAgent & "OPENPO.xml")
                    End If
                ElseIf iDocType = DocType.Open Then
                    If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sAgent & "OPENPO.xml") Then
                        UG.DisplayLayout.LoadFromXml(Application.StartupPath & "\" & sAgent & "OPEN.xml")
                    End If
                End If

            Catch ex As Exception
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try
        End With
    End Sub

    Private Sub SalesOrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesOrderToolStripMenuItem.Click
        If iDocType = DocType.PurchaseOrder Then
            Dim Customer As New Supplier(CInt(UG.ActiveRow.Cells("AccountID").Value))
        Else
            Dim Customer As New Customer(CInt(UG.ActiveRow.Cells("AccountID").Value))
        End If



        If UG.Selected.Rows.Count = 0 Then Exit Sub
        'If MsgBox("Do you want to print now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
        Dim objRep As New ReportDocument
        'objRep.Load(Application.StartupPath & "\SalesOrder.rpt")
        'objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & UG.Selected.Rows(0).Cells("AutoIndex").Value & ""
        'ApplyLoginToTable(objRep)
        'If MsgBox("Do you want to print now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
        'Dim objRep As New ReportDocument

        If iDocType_1 = 10 Then
            'If iDocType = DocType.RTN Then
            '    objRep.Load(Application.StartupPath & "\RTN.rpt")
            'Else
            '    objRep.Load(Application.StartupPath & "\IBT.rpt")
            'End If

            If UG.ActiveRow.Cells("ucIDSOrddbName").Value.ToString().Length > 0 Then
                Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= '" & UG.ActiveRow.Cells("ucIDSOrddbName").Value & "' ")
            Else
                Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= '" & sSQLSrvDataBase & "' ")
            End If
            SQL = " SELECT OrderNum, DocState  from InvNum where ExtOrderNum = '" & UG.ActiveRow.Cells("InvNumber").Value & "' "
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

            frmPrintPreview.vSelectionFormula = "{InvNum.AutoIndex}= " & UG.ActiveRow.Cells("AutoIndex").Value & ""

            iRepoID = 2

            frmPrintPreview.ShowDialog()
            iRepoID = 0





            If MsgBox("Do you want to send print to warehouse", MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
                'objRep = New ReportDocument
                objRep.RecordSelectionFormula = "{InvNum.AutoIndex}= " & UG.ActiveRow.Cells("AutoIndex").Value & ""
                objRep.PrintOptions.PrinterName = "EPSON LQ-310 ESC/P2 WH"
                objRep.Load(Application.StartupPath & "\F1.rpt", OpenReportMethod.OpenReportByDefault)


                ApplyLoginToTable(objRep)

                objRep.PrintToPrinter(1, False, 0, 0)
                Exit Sub

                '=========================================================================================

                'objRep = New ReportDocument

                objRep.PrintOptions.PrinterName = "F2"
                objRep.Load(Application.StartupPath & "\F2.rpt", OpenReportMethod.OpenReportByDefault)

                objRep.RecordSelectionFormula = "{InvNum.AutoIndex}= " & UG.ActiveRow.Cells("AutoIndex").Value & ""
                ApplyLoginToTable(objRep)

                objRep.PrintToPrinter(1, False, 0, 0)

            End If





            Exit Sub

        ElseIf iDocType_1 = 8 Then
            objRep.Load(Application.StartupPath & "\IBT_R.rpt")
        ElseIf iDocType_1 = 3 Then
            objRep.Load(Application.StartupPath & "\IBT_R.rpt")

        Else
            If UG.ActiveRow.Cells("DocState").Value = 2 Then
                If sSQLSrvDataBase = "dbNawinna_new" Then
                    objRep.Load(Application.StartupPath & "\SalesOrderQN.rpt")
                Else
                    objRep.Load(Application.StartupPath & "\SalesOrderQ.rpt")
                End If
                'objRep.Load(Application.StartupPath & "\SalesOrderQ.rpt")
            Else
                If iDocType = 5 Then
                    objRep.Load(Application.StartupPath & "\PO.rpt")
                Else
                    'If Customer.TaxNumber = Nothing Then
                    objRep.Load(Application.StartupPath & "\SalesOrder.rpt")
                    '    Else
                    '    objRep.Load(Application.StartupPath & "\SalesOrder_VAT.rpt")
                    'End If
                End If
            End If
        End If

        'objRep.Load(Application.StartupPath & "\SalesOrder.rpt")
        objRep.RecordSelectionFormula = "{InvNum.AutoIndex}= " & UG.ActiveRow.Cells("AutoIndex").Value & ""
        ApplyLoginToTable(objRep)
        'objRep.PrintToPrinter(1, False, 0, 0)
        'End If
        objRep.SetDatabaseLogon(sSQLSrvUserName, sSQLSrvPassword, sSQLSrvReportSrv, sSQLSrvDataBase)


        'Dim ps As PaperSize
        'ps.DefaultPaperSize()
        'Dim pps As PrintPreviewControl
        'pps.Size = Printing.PaperSize("PageSize", 850, 2500)

        'PrintDocument1.DefaultPageSettings.PaperSize = New
        'Printing.PaperSize("PageSize", 850, 2500)



        'Dim DefaultPrinter As String = DefaultPrinterName
        'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
        'doctoprint.PrinterSettings.PrinterName = DefaultPrinter
        'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
        '    Dim rawKind As Integer
        '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Inv" Then
        '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
        '        objRep.PrintOptions.PaperSize = rawKind
        '        objRep.PrintToPrinter(1, False, 0, 0)
        '        Exit For
        '    End If
        'Next

        If MsgBox("Do you want to preview the sales order", MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.No Then
            Dim DefaultPrinter As String = DefaultPrinterName
            Dim doctoprint As New System.Drawing.Printing.PrintDocument()
            doctoprint.PrinterSettings.PrinterName = DefaultPrinter
            For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                Dim rawKind As Integer
                If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Inv" Then
                    rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                    objRep.PrintOptions.PaperSize = rawKind
                    objRep.PrintToPrinter(1, False, 0, 0)
                    Exit For
                End If
            Next
        Else
            Dim objCRV As New frmPrintPreview
            With objCRV
                .CRV.ReportSource = objRep
                .Text = "Sales Order Printing"
                .Show()
                .TopMost = True
            End With
        End If


    End Sub

    Private Sub PickingSlipToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PickingSlipToolStripMenuItem.Click
       
        Dim Customer As New Customer(CInt(UG.ActiveRow.Cells("AccountID").Value))
        If UG.Selected.Rows.Count = 0 Then Exit Sub
        Dim objRep As New ReportDocument
        
        If iDocType_1 = 10 Then
            

        ElseIf iDocType_1 = 8 Then

        Else
            If UG.ActiveRow.Cells("DocState").Value = 2 Then
               
            Else
                If iDocType = 5 Then

                Else
                    objRep.Load(Application.StartupPath & "\PickingSlip.rpt")
                End If
            End If
        End If

        objRep.RecordSelectionFormula = "{InvNum.AutoIndex}= " & UG.ActiveRow.Cells("AutoIndex").Value & ""
        ApplyLoginToTable(objRep)
     
        objRep.SetDatabaseLogon(sSQLSrvUserName, sSQLSrvPassword, sSQLSrvReportSrv, sSQLSrvDataBase)


     

        If MsgBox("Do you want to preview the picking slip", MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.No Then
            Dim DefaultPrinter As String = DefaultPrinterName
            Dim doctoprint As New System.Drawing.Printing.PrintDocument()
            doctoprint.PrinterSettings.PrinterName = DefaultPrinter
            For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                Dim rawKind As Integer
                If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Inv" Then
                    rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                    objRep.PrintOptions.PaperSize = rawKind
                    objRep.PrintToPrinter(1, False, 0, 0)
                    Exit For
                End If
            Next
        Else
            Dim objCRV As New frmPrintPreview
            With objCRV
                .CRV.ReportSource = objRep
                .Text = "Picking Slip Printing"
                .Show()
                .TopMost = True
            End With
        End If



        'End If
    End Sub
End Class