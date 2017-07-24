Public Class frmPW

    Public ref As String
    Public cusID As Integer
    Public repID As Integer
    Public project As String
    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub payment()
        lblPayAmt.Text = CDbl(IIf(txtCash.Text = "", "0.00", txtCash.Text)) + CDbl(IIf(txtCheque.Text = "", "0.00", txtCheque.Text)) + CDbl(IIf(txtCredit.Text = "", "0.00", txtCredit.Text))

        If CDbl(lblPayAmt.Text) > CDbl(lblDueAmt.Text) Then
            lblChanAmt.Text = CDbl(lblPayAmt.Text) - CDbl(lblDueAmt.Text)
            lblOutsAmt.Text = "0.00"
        Else
            lblOutsAmt.Text = CDbl(lblDueAmt.Text) - CDbl(lblPayAmt.Text)
            lblChanAmt.Text = 0
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If txtCash.Text.Length = 0 And txtCheque.Text.Length = 0 And txtCredit.Text.Length = 0 Then
            MsgBox("Please enter an amount", MsgBoxStyle.Exclamation, "Pastel Evolution")
        End If


        If CDbl(lblOutsAmt.Text) > 0 Then
            MsgBox("Insufficient amount", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        Dim objSQL As New clsSqlConn
        With objSQL
            If project = "Nawinna" Then
                SQL = "INSERT INTO [_btblCbBatchLines] ([iBatchesID]  ,[iSplitType] ,[iSplitGroup] ,[dTxDate]   ,[iModule] " & _
                                   ",[iAccountID],[cDescription] ,[cReference],[fDebit],[fCredit],[bReconcile],[fTaxAmount],[iTaxTypeID],[iTaxAccountID] " & _
                                   ",[iProjectID],[bPostDated],[fDiscPerc],[iDiscTrCodeID],[cDiscDesc],[iDiscTaxTypeID],[iDiscTaxAccID],[fDiscTaxAmount] " & _
                                   ",[cPayeeName],[bPrintCheque],[bChequePrinted],[iRepID],[fExchangeRate],[fDebitForeign],[fCreditForeign],[fTaxAmountForeign] " & _
                                   ",[fDiscTaxAmountForeign],[iCBBatchLinesReconID],[fFCAccountAmount],[fFCAccountExchange],[fFCAccountDiscAmount],[fFCAccountDiscTax],[iSettDiscGroupID] " & _
                                   ",[iSettDiscPostARAPID],[_btblCbBatchLines_iBranchID]) " & _
                                   " VALUES(" & IIf(txtCheque.Text.Length > 0, 21, 5) & ",0,0,GETDATE(),1," & IIf(txtCredit.Text.Length > 0, 99, cusID) & ", " & _
                                   " '" & IIf(txtCheque.Text.Length > 0, "CHEQUE", ref) & "','" & IIf(txtCheque.Text.Length > 0, txtNaration.Text, "CASH") & "'," & lblDueAmt.Text & ",0,0,0,0 " & _
                                   " ,0,0,0,0,0,''" & _
                                   " ,0,0,0,'',0 " & _
                                   " ,0," & repID & ",0,0,0 " & _
                                   " ,0,0,1," & lblDueAmt.Text & " " & _
                                   " ,1,0,0,0 " & _
                                   " ,0,0) "
            ElseIf project = "Kiribathgoda" Then
                SQL = "INSERT INTO [_btblCbBatchLines] ([iBatchesID]  ,[iSplitType] ,[iSplitGroup] ,[dTxDate]   ,[iModule] " & _
                           ",[iAccountID],[cDescription] ,[cReference],[fDebit],[fCredit],[bReconcile],[fTaxAmount],[iTaxTypeID],[iTaxAccountID] " & _
                           ",[iProjectID],[bPostDated],[fDiscPerc],[iDiscTrCodeID],[cDiscDesc],[iDiscTaxTypeID],[iDiscTaxAccID],[fDiscTaxAmount] " & _
                           ",[cPayeeName],[bPrintCheque],[bChequePrinted],[iRepID],[fExchangeRate],[fDebitForeign],[fCreditForeign],[fTaxAmountForeign] " & _
                           ",[fDiscTaxAmountForeign],[iCBBatchLinesReconID],[fFCAccountAmount],[fFCAccountExchange],[fFCAccountDiscAmount],[fFCAccountDiscTax],[iSettDiscGroupID] " & _
                           ",[iSettDiscPostARAPID],[_btblCbBatchLines_iBranchID]) " & _
                           " VALUES(" & IIf(txtCheque.Text.Length > 0, 28, 28) & ",0,0,GETDATE(),1," & IIf(txtCredit.Text.Length > 0, 99, cusID) & ", " & _
                           " '" & IIf(txtCheque.Text.Length > 0, "CHEQUE", ref) & "','" & IIf(txtCheque.Text.Length > 0, txtNaration.Text, "CASH") & "'," & lblDueAmt.Text & ",0,0,0,0 " & _
                           " ,0,0,0,0,0,''" & _
                           " ,0,0,0,'',0 " & _
                           " ,0," & repID & ",0,0,0 " & _
                           " ,0,0,1," & lblDueAmt.Text & " " & _
                           " ,1,0,0,0 " & _
                           " ,0,0) "
            End If

            If .Execute_Sql(SQL) = 0 Then
                .Rollback_Trans()
                Exit Sub
            End If
        End With
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub txtCash_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCash.ValueChanged
        payment()
    End Sub

    Private Sub txtCredit_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCredit.ValueChanged
        payment()
    End Sub

    Private Sub txtCheque_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCheque.ValueChanged
        payment()
    End Sub

    Private Sub frmPW_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'txtCash.Text = 0.0
        'txtCheque.Text = 0.0
        'txtCredit.Text = 0.0
    End Sub
End Class