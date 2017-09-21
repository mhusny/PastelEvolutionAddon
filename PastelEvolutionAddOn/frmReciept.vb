Imports Infragistics.Win.UltraWinGrid
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Pastel.Evolution
Imports System.Web
Imports System.Net
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmReciept

    Public projectID As Integer

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            Dim ct As New CustomerTransaction()
            ct.AccountID = cmbCustomer.Value
            ct.Amount = txtAmont.Text
            ct.Description = txtDescription.Text
            ct.Reference = txtReference.Text()
            ct.TransactionCodeID = cmbTrCode.Value
            If IsDBNull(projectID) = False Then
                ct.ProjectID = projectID
            End If

            'ct.SalesRepID = iAgent

            ct.Post()


            If cmbTrCode.Value = 13 Then 'PSPM
                allocateOrder(ct)
            End If

            printrec(ct.ID)


            MsgBox("Posted", MsgBoxStyle.Information, "Evolution")



            'check for ealier bills for the day
            sSQL = " set dateformat dmy SELECT Credit FROM PostAR WHERE AccountLink = " & cmbCustomer.Value & " AND TxDate = '" & Date.Now.Date & "' AND Credit > 0 "

            Try
                Con1.ConnectionString = sConStr
                CMD = New SqlCommand(sSQL, Con1)
                DS = New DataSet
                DA = New SqlDataAdapter(CMD)

                Con1.Open()
                DA.Fill(DS)
                Con1.Close()

                If DS.Tables(0).Rows.Count = 1 Then
                    sendSMS()
                End If


            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                Exit Sub
            Finally
                If Con1.State = ConnectionState.Open Then Con1.Close()
            End Try



        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
            Exit Sub
        Finally
            If Con1.State = ConnectionState.Open Then Con1.Close()
        End Try

        Clear()

    End Sub

    Private Sub printrec(ByVal id As Integer)
        Try
            Dim report As New ReportDocument

            report.PrintOptions.PrinterName = "RECIEPT2"
            report.Load(Application.StartupPath & "\Reciept.rpt", OpenReportMethod.OpenReportByDefault)

            report.RecordSelectionFormula = "{PostAR.AutoIdx}=" & id & ""
            ApplyLoginToTable(report)

            report.PrintToPrinter(1, False, 0, 0)
        Catch
            'Exception(e)
        End Try


    End Sub


    Private Sub allocateOrder(ByVal transaction As DrCrTransaction)

        Dim criteria As String = String.Format("Reference = '{0}'", transaction.Reference)
        If transaction.Outstanding = 0 Then
            Exit Sub
        End If
        If (transaction.Outstanding > 0) Then
            criteria += " and Outstanding  0"
        End If

        Dim matches As DataTable = CustomerTransaction.List(transaction.Account, criteria)
        If (matches.Rows.Count > 0) Then

            For Each match As DataRow In matches.Rows

                'terminate if satisfied
                If (transaction.Outstanding = 0) Then
                    Exit Sub
                End If

                Dim relatedTran As New CustomerTransaction(CInt(match("Autoidx")))
                If relatedTran.Debit > 0 Then
                    transaction.Allocations.Add(relatedTran)
                    Exit For
                End If
            Next
            transaction.Allocations.Save()
        End If

    End Sub

    Private Sub sendSMS()
        Dim name As String
        If cmbCustomer.SelectedRow.Cells("Title").Value.ToString().Length > 0 And cmbCustomer.SelectedRow.Cells("Name").Value.ToString().Length > 0 Then
            name = cmbCustomer.SelectedRow.Cells("Title").Value.ToString() & " " & cmbCustomer.SelectedRow.Cells("Name").Value.ToString()
        Else
            name = " Valued Customer "
        End If
        'Dim msg As String = "Dear " & name & ", How would you rate the service done on your vehicle no. " & cmbCustomer.SelectedRow.Cells("Account").Value & " at Udawatta Automobile Navinna? Please rate 5(Excellent), 4(Very Good), 3(Good), 2(Fair), 1(Poor) to 0773933789. Thank You!"
        Dim msg As String = "Dear Valued Customer, How would you rate the service done on your vehicle no. " & cmbCustomer.SelectedRow.Cells("Account").Value & " at Udawatta Motors? Please rate 1(poor) to 5(Excellent) to 0773933789"
        Dim no As String = cmbCustomer.SelectedRow.Cells("Telephone").Value

        If no.Length > 0 And no.Length = 10 Then
            Dim client = New WebClient()
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)")
            Dim baseurl As String = "https://cpsolutions.dialog.lk/index.php/cbs/sms/send?destination=" & no & "&q=14727200434668&message=" & msg & ""
            Dim Data As Stream = client.OpenRead(baseurl)
            Dim reader As StreamReader = New StreamReader(Data)
            Dim s As String = reader.ReadToEnd()
            Data.Close()
            reader.Close()

            no = "0777224425"
            client = New WebClient()
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)")
            baseurl = "https://cpsolutions.dialog.lk/index.php/cbs/sms/send?destination=" & no & "&q=14727200434668&message=" & msg & ""
            Data = client.OpenRead(baseurl)
            reader = New StreamReader(Data)
            s = reader.ReadToEnd()
            Data.Close()
            reader.Close()
        End If
    End Sub

    Private Sub Clear()
        cmbCustomer.Value = ""
        cmbTrCode.Value = ""
        txtAmont.Text = ""
        txtDescription.Text = ""
        txtReference.Text = ""
    End Sub

    Private Sub GET_DATA()
        sSQL = ""
        sSQL = "SELECT DCLink, Account, Name, Title, Physical1, Physical2," & _
                   " Physical3, Post1, Post2, Post3 , Discount , cAccDescription, cIDNumber, Telephone FROM Client Order By Name "
        sSQL += " SELECT idTrCodes, Code, Description FROM TrCodes WHERE iModule = 5 "

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

            cmbTrCode.DataSource = DS.Tables(1)
            cmbTrCode.ValueMember = "idTrCodes"
            cmbTrCode.DisplayMember = "Code"
            cmbTrCode.DisplayLayout.Bands(0).Columns("idTrCodes").Hidden = True
            cmbTrCode.DisplayLayout.Bands(0).Columns("Code").Width = 200

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
            Exit Sub
        Finally
            If Con1.State = ConnectionState.Open Then Con1.Close()
        End Try
    End Sub

    Private Sub frmReciept_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GET_DATA()
    End Sub

    Private Sub cmbCustomer_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCustomer.Leave
        sSQL = " SELECT MAX(InvNumber), InvTotIncl, iProjectID FROM  JobNum WHERE AccountID = " & cmbCustomer.SelectedRow.Cells("DCLink").Value & " AND InvNumber like 'K%' GROUP BY InvTotIncl, iProjectID "

        Con1.ConnectionString = sConStr
        CMD = New SqlCommand(sSQL, Con1)
        Dim DS1 As New DataSet
        DA = New SqlDataAdapter(CMD)

        Con1.Open()
        DA.Fill(DS1)
        Con1.Close()

        If DS1.Tables(0).Rows.Count > 0 Then
            txtReference.Text = DS1.Tables(0).Rows(0)(0).ToString()
            txtAmont.Text = DS1.Tables(0).Rows(0)(1).ToString()
            cmbTrCode.Value = 13

            projectID = CInt(DS1.Tables(0).Rows(0)(2).ToString())
        End If
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
End Class