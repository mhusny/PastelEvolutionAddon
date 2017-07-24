Imports System.Data.SqlClient
Imports System.Net
Imports System.IO

Public Class frmBulkSMS

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        sSQL = " SELECT Account, Name, Title, Telephone FROM Client WHERE LEN(Telephone) = 10 "

        Try
            Con1.ConnectionString = sConStr
            CMD = New SqlCommand(sSQL, Con1)
            DS = New DataSet
            DA = New SqlDataAdapter(CMD)

            Con1.Open()
            DA.Fill(DS)
            Con1.Close()

            If MessageBox.Show("Are you sure you want to send " & DS.Tables(0).Rows.Count & " messages", "PastelEvolutionAddOn", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = True Then
                Dim dr As DataRow
                For Each dr In DS.Tables(0).Rows
                    sendSMS(dr("Telephone").ToString())
                Next
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
            Exit Sub
        Finally
            If Con1.State = ConnectionState.Open Then Con1.Close()
        End Try
    End Sub

    Private Sub sendSMS(ByVal no As String)
        'Dim name As String
        'If cmbCustomer.SelectedRow.Cells("Title").Value.ToString().Length > 0 And cmbCustomer.SelectedRow.Cells("Name").Value.ToString().Length > 0 Then
        '    name = cmbCustomer.SelectedRow.Cells("Title").Value.ToString() & " " & cmbCustomer.SelectedRow.Cells("Name").Value.ToString()
        'Else
        'name = " Valued Customer "
        'End If

        'Dim msg As String = "Dear " & name & ", How would you rate the service done on your vehicle no. " & cmbCustomer.SelectedRow.Cells("Account").Value & " at Udawatta Automobile Navinna? Please rate 5(Excellent), 4(Very Good), 3(Good), 2(Fair), 1(Poor) to 0773933789. Thank You!"
        Dim msg As String = txtmsg.Text
        'Dim no As String = DS.Tables(0).Rows.Cells("Telephone").Value

        If msg.Length > 0 And no.Length = 10 Then
            Dim client = New WebClient()
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)")
            Dim baseurl As String = "https://cpsolutions.dialog.lk/index.php/cbs/sms/send?destination=" & no & "&q=14727200434668&message=" & msg & ""
            Dim Data As Stream = client.OpenRead(baseurl)
            Dim reader As StreamReader = New StreamReader(Data)
            Dim s As String = reader.ReadToEnd()
            Data.Close()
            reader.Close()

            'no = "0777224425"
            'client = New WebClient()
            'client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)")
            'baseurl = "https://cpsolutions.dialog.lk/index.php/cbs/sms/send?destination=" & no & "&q=14727200434668&message=" & msg & ""
            'Data = client.OpenRead(baseurl)
            'reader = New StreamReader(Data)
            's = reader.ReadToEnd()
            'Data.Close()
            'reader.Close()
        End If
    End Sub

    Private Sub txttest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttest.Click
        Dim msg As String = txtmsg.Text
        Dim no As String = TextBox1.Text

        If msg.Length > 0 And TextBox1.Text.Length = 10 Then
            Dim client = New WebClient()
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)")
            Dim baseurl As String = "https://cpsolutions.dialog.lk/index.php/cbs/sms/send?destination=" & no & "&q=14727200434668&message=" & msg & ""
            Dim Data As Stream = client.OpenRead(baseurl)
            Dim reader As StreamReader = New StreamReader(Data)
            Dim s As String = reader.ReadToEnd()
            Data.Close()
            reader.Close()

            'no = "0777224425"
            'client = New WebClient()
            'client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)")
            'baseurl = "https://cpsolutions.dialog.lk/index.php/cbs/sms/send?destination=" & no & "&q=14727200434668&message=" & msg & ""
            'Data = client.OpenRead(baseurl)
            'reader = New StreamReader(Data)
            's = reader.ReadToEnd()
            'Data.Close()
            'reader.Close()
        End If
    End Sub
End Class