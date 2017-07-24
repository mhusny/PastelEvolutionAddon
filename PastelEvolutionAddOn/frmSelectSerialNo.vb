Imports System.Data
Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid
Public Class frmSelectSerialNo
    Public intItem As Integer


    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim sString As String = txtString.Text.Trim
        'Dim iNumber As Integer = txtNumber.Value

        'Dim sNumber As String = ""
        'Dim I As Integer = 0
        'While I < txtGenerate.Value
        '    Select Case txtPad.Value
        '        Case 1
        '            sNumber = sString & Format(iNumber + I, "0")
        '        Case 2
        '            sNumber = sString & Format(iNumber + I, "00")
        '        Case 3
        '            sNumber = sString & Format(iNumber + I, "000")
        '        Case 4
        '            sNumber = sString & Format(iNumber + I, "0000")
        '        Case 5
        '            sNumber = sString & Format(iNumber + I, "00000")
        '        Case 6
        '            sNumber = sString & Format(iNumber + I, "000000")
        '        Case 7
        '            sNumber = sString & Format(iNumber + I, "0000000")
        '        Case 8
        '            sNumber = sString & Format(iNumber + I, "00000000")
        '        Case 9
        '            sNumber = sString & Format(iNumber + I, "000000000")
        '        Case 10
        '            sNumber = sString & Format(iNumber + I, "0000000000")
        '    End Select
        '    lbSelected.Items.Add(sNumber)
        '    I = I + 1
        'End While
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        lbSelected.Items.Clear()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try

            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End Try
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

        'If intItem <> 0 Then

        '    SQL = "SELECT TOP 1  [SerialNumber] FROM SerialMF where SNStockLink = " & intItem & " order by [SerialNumber] desc "
        '    Dim objSQL As New clsSqlConn
        '    With objSQL
        '        Try
        '            DS = New DataSet
        '            DS = .Get_Data_Sql(SQL)

        '            Dim Dr As DataRow
        '            lbl1.Text = ""
        '            For Each Dr In DS.Tables(0).Rows
        '                lbl1.Text = Dr(0)
        '            Next
        '        Catch ex As Exception
        '            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
        '            Exit Sub
        '        Finally
        '            objSQL = Nothing
        '        End Try
        '    End With
        'End If
        'lbSelected.SelectedIndex = 0
        If lbSelected.Items.Count > 0 Then
            lbSelected.SelectedIndex = 0
        End If
        If lbAdded.Items.Count > 0 Then
            lbAdded.SelectedIndex = 0
        End If

        If lbSelected.Items.Count = 0 Then
            UltraButton1.Enabled = False
        End If
        If lbAdded.Items.Count = 0 Then
            UltraButton4.Enabled = False
        End If
    End Sub

    Private Sub UltraButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraButton1.Click
        Dim i As Integer
        If lbSelected.SelectedIndex = lbSelected.Items.Count - 1 Then
            i = lbSelected.SelectedIndex - 1
        Else
            i = lbSelected.SelectedIndex
        End If
        lbAdded.Items.Add(lbSelected.SelectedItem)
        lbSelected.Items.Remove(lbSelected.SelectedItem)
        lbSelected.SelectedIndex = i
        lbAdded.SelectedIndex = lbAdded.Items.Count - 1

        If lbSelected.Items.Count = 0 Then
            UltraButton1.Enabled = False
        End If
        If lbAdded.Items.Count > 0 Then
            UltraButton4.Enabled = True
        End If
    End Sub

    Private Sub frmSelectSerialNo_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Me.Dispose()
    End Sub

    Private Sub UltraButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraButton2.Click

    End Sub

    Private Sub UltraButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraButton4.Click
        Try

        
            Dim i As Integer
            If lbAdded.SelectedIndex = lbAdded.Items.Count - 1 Then
                i = lbAdded.SelectedIndex - 1
            Else
                i = lbAdded.SelectedIndex
            End If
            lbSelected.Items.Add(lbAdded.SelectedItem)
            lbAdded.Items.Remove(lbAdded.SelectedItem)
            lbAdded.SelectedIndex = i
            lbSelected.SelectedIndex = lbSelected.Items.Count - 1


            If lbAdded.Items.Count = 0 Then
                UltraButton4.Enabled = False
            End If
            If lbSelected.Items.Count > 0 Then
                UltraButton1.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class