Imports Infragistics.Win.UltraWinGrid
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Pastel.Evolution
Imports System.Web
Imports System.Net
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmCustomerSMS

    Private Sub frmCustomerSMS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GET_DATA()
    End Sub
    Private Sub GET_DATA()
        sSQL = ""
        sSQL = "SELECT DCLink, Account, Name, Title, Physical1, Physical2," & _
                   " Physical3, Post1, Post2, Post3 , Discount , cAccDescription, cIDNumber, Telephone FROM Client Order By Name "
        'sSQL += " SELECT idTrCodes, Code, Description FROM TrCodes WHERE iModule = 5 "

        Try
            Con1.ConnectionString = sConStr
            CMD = New SqlCommand(sSQL, Con1)
            DS = New DataSet
            DA = New SqlDataAdapter(CMD)

            Con1.Open()
            DA.Fill(DS)
            Con1.Close()

            cmbCustomer.DataSource = DS.Tables(0)
            cmbCustomer.ValueMember = "DCLink"
            cmbCustomer.DisplayMember = "Account"
            cmbCustomer.DisplayLayout.Bands(0).Columns("DCLink").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Title").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Physical1").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Physical2").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Physical3").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Post1").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Post2").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Post3").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Discount").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("cAccDescription").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("cIDNumber").Hidden = True
            'cmbCustomer.DisplayLayout.Bands(0).Columns("Account").Width = 150

            'cmbTrCode.DataSource = DS.Tables(1)
            'cmbTrCode.ValueMember = "idTrCodes"
            'cmbTrCode.DisplayMember = "Code"
            'cmbTrCode.DisplayLayout.Bands(0).Columns("idTrCodes").Hidden = True
            'cmbTrCode.DisplayLayout.Bands(0).Columns("Code").Width = 200

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
            Exit Sub
        Finally
            If Con1.State = ConnectionState.Open Then Con1.Close()
        End Try
    End Sub

    Private Sub cmbCustomer_Leave(sender As Object, e As EventArgs) Handles cmbCustomer.Leave
        If cmbCustomer.Text.Length > 0 Then
            sSQL = " SELECT cJobCode FROM dbo._btblJCMaster WHERE iClientId = " & cmbCustomer.Value & " "

            Try
                Con1.ConnectionString = sConStr
                CMD = New SqlCommand(sSQL, Con1)
                DS = New DataSet
                DA = New SqlDataAdapter(CMD)

                Con1.Open()
                DA.Fill(DS)
                Con1.Close()

                cmbJob.DataSource = DS.Tables(0)
                cmbJob.ValueMember = "cJobCode"
                cmbJob.DisplayMember = "cJobCode"
                'cmbJob.DisplayLayout.Bands(0).Columns("idTrCodes").Hidden = True
                'cmbJob.DisplayLayout.Bands(0).Columns("Code").Width = 200

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                Exit Sub
            Finally
                If Con1.State = ConnectionState.Open Then Con1.Close()
            End Try
        End If
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Dim msg As String
        Dim no As String = cmbCustomer.SelectedRow.Cells("Telephone").Value
        If rb1.Checked = True Then
            msg = " " & cmbCustomer.Value & " "
        Else
            msg = " your vehicle is ready" & cmbCustomer.Value & " "
        End If

        If no.Length > 0 And no.Length = 10 Then
            Dim client = New WebClient()
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)")
            Dim baseurl As String = "https://cpsolutions.dialog.lk/index.php/cbs/sms/send?destination=" & no & "&q=14727200434668&message=" & msg & ""
            Dim Data As Stream = client.OpenRead(baseurl)
            Dim reader As StreamReader = New StreamReader(Data)
            Dim s As String = reader.ReadToEnd()
            Data.Close()
            reader.Close()
        End If
    End Sub
End Class