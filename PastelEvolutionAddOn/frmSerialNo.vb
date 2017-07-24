Imports System.Data
Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid
Public Class frmSerialNo
    Public intItem As Integer
    Public strCode As String
    Public DS1 As DataSet


    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        btnGenerate.Enabled = False
        Dim sString As String = txtString.Text.Trim
        Dim sDash As String = txtdash.Text.Trim
        Dim iNumber As Integer = txtNumber.Value

        Dim sNumber As String = ""
        Dim I As Integer = 0
        While I < txtGenerate.Value
            Select Case txtPad.Value
                Case 1
                    sNumber = sString & sDash & Format(iNumber + I, "0")
                Case 2
                    sNumber = sString & sDash & Format(iNumber + I, "00")
                Case 3
                    sNumber = sString & sDash & Format(iNumber + I, "000")
                Case 4
                    sNumber = sString & sDash & Format(iNumber + I, "0000")
                Case 5
                    sNumber = sString & sDash & Format(iNumber + I, "00000")
                Case 6
                    sNumber = sString & sDash & Format(iNumber + I, "000000")
                Case 7
                    sNumber = sString & sDash & Format(iNumber + I, "0000000")
                Case 8
                    sNumber = sString & sDash & Format(iNumber + I, "00000000")
                Case 9
                    sNumber = sString & sDash & Format(iNumber + I, "000000000")
                Case 10
                    sNumber = sString & sDash & Format(iNumber + I, "0000000000")
            End Select
            lbSelected.Items.Add(sNumber)
            I = I + 1
        End While
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        lbSelected.Items.Clear()
        btnGenerate.Enabled = True
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        Dim objSQL As New clsSqlConn
        With objSQL
            SQL = " SELECT SNStockLink, SerialNumber From SerialMF "
            SQL = SQL + " SELECT SNStockLink, SerialNumber From sbSerialMF "
            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
            Dim Dr As DataRow

            Try

                Dim lvItem As String
                For Each lvItem In lbSelected.Items
                    For Each Dr In DS.Tables(0).Rows
                        If lvItem = Dr("SerialNumber") Then
                            Dim objSQL1 As New clsSqlConn
                            With objSQL1
                                SQL = " SELECT TOP(1)SerialNumber From SerialMF WHERE SNStockLink = " & Dr("SNStockLink") & " Order by SerialNumber DESC"
                                DS1 = New DataSet
                                DS1 = .Get_Data_Sql(SQL)
                            End With
                            MessageBox.Show("Duplicate serial number '" & lvItem & "' " & vbNewLine & "Please generate a differnt serial number" & vbNewLine & "Last Processed S/N = '" & DS1.Tables(0).Rows(0)(0) & "'", "Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Next
                    For Each Dr In DS.Tables(1).Rows
                        If lvItem = Dr("SerialNumber") Then
                            Dim objSQL1 As New clsSqlConn
                            With objSQL1
                                SQL = " SELECT TOP(1)SerialNumber From sbSerialMF WHERE SNStockLink = " & Dr("SNStockLink") & " Order by SerialNumber DESC"
                                DS1 = New DataSet
                                DS1 = .Get_Data_Sql(SQL)
                            End With
                            MessageBox.Show("Duplicate serial number '" & lvItem & "' found in an Unprocessed PO " & vbNewLine & "Please generate a differnt serial number" & vbNewLine & "Last Unprocessed S/N = '" & DS1.Tables(0).Rows(0)(0) & "'", "Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Next
                Next
                Me.Close()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            End Try
        End With
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        lbSelected.Items.Clear()
        Me.Close()
    End Sub

    Private Sub frmSerialNo_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'MsgBox("Activatred")

     

    End Sub

    Private Sub frmSerialNo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'MsgBox("Load")
        If sAgent = "ST003" Then
            txtString.ReadOnly = True
        Else
            txtString.Enabled = True
        End If
        If intItem <> 0 Then

            SQL = "SELECT TOP 1  [SerialNumber] FROM SerialMF where SNStockLink = " & intItem & " order by [SerialNumber] desc "
            Dim objSQL As New clsSqlConn
            With objSQL
                Try

                    DS = New DataSet

                    DS = .Get_Data_Sql(SQL)

                    Dim Dr As DataRow

                    lbl1.Text = ""
                    txtString.Text = ""

                    lbl1.Text = strCode
                    If strCode = Nothing Then
                        txtString.Text = ""
                    Else
                        txtString.Text = strCode
                    End If


                    For Each Dr In DS.Tables(0).Rows
                        lbl1.Text = Dr(0)
                    Next

                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                    Exit Sub
                Finally
                    objSQL = Nothing
                End Try
            End With


            SQL = "SELECT TOP 1  [SerialNumber] FROM sbSerialMF where SNStockLink = " & intItem & " order by [SerialNumber] desc "
            objSQL = New clsSqlConn
            With objSQL
                Try

                    DS = New DataSet

                    DS = .Get_Data_Sql(SQL)

                    Dim Dr As DataRow

                    lbl2.Text = ""
                    txtString.Text = ""

                    lbl2.Text = strCode
                    If strCode = Nothing Then
                        txtString.Text = ""
                    Else
                        txtString.Text = strCode.Trim
                    End If


                    For Each Dr In DS.Tables(0).Rows
                        lbl2.Text = Dr(0)
                    Next

                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                    Exit Sub
                Finally
                    objSQL = Nothing
                End Try
            End With
        End If
        txtdash.SelectAll()
    End Sub

    Private Sub txtNumber_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNumber.GotFocus
        txtNumber.SelectAll()
    End Sub

    Private Sub txtPad_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPad.GotFocus
        txtPad.Select(0, 1)
    End Sub


    Private Sub txtString_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtString.ValueChanged

    End Sub
End Class