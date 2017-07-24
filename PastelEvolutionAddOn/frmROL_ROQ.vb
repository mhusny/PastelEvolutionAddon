Imports System.Data
Imports System.Data.SqlClient
Imports Pastel.Evolution
Imports System.IO
Imports Infragistics.Win.UltraWinTree
Imports EncryptionClassLibrary


Public Class frmROL_ROQ

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Update.Click

        Dim dROL, dROQ, dMaxLvl As Double

        Dim StartDate As Date

        Dim objSQL As New clsSqlConn

        StartDate = DateAdd(DateInterval.Month, -CInt(txtNoOfMonth.Text), dtpUpToDate.Value)

        With objSQL

            Dim DS = New DataSet
            Dim DS1 As DataSet

            SQL = " SELECT   StkItem.StockLink FROM  StkItem   "

            DS = .Get_Data_Sql(SQL)

            If DS.Tables(0).Rows.Count > 0 Then

                For Each Dr In DS.Tables(0).Rows

                    'If Dr.Item(0) = 2239 Then


                    If Not IsDBNull(Dr.Item(0)) Then
                        ' '' SQL = "     SELECT     SUM(Quantity) AS sumqty FROM PostST  WHERE " & _
                        ' '' " (AccountLink =" & Dr.Item(0) & ") AND (Id = 'OInv' OR Id = 'Inv' OR Id = 'IJr' OR Id = 'WIBTI') AND (TxDate >= '12/10/2010') AND (Credit <> 0) "

                        SQL = " Set dateformat dmy SELECT SUM(Quantity)/" & Integer.Parse(txtNoOfMonth.Value) & " AS Avgqty , StkItem.ucIICategory " & _
                        " FROM PostST INNER JOIN  StkItem ON PostST.AccountLink = StkItem.StockLink  WHERE " & _
                        " (AccountLink =" & Dr.Item(0) & ") AND (Id = 'OInv' OR Id = 'Inv' OR Id = 'IJr' OR Id = 'WIBTI')  " & _
                        " AND TxDate >='" & DateAdd(DateInterval.Month, Integer.Parse(txtNoOfMonth.Value) * -1, dtpUpToDate.Value) & " ' AND TxDate <= '" & Format(dtpUpToDate.Value, "dd/MM/yy") & "'" & _
                        " GROUP BY StkItem.ucIICategory "
                        '" AND (TxDate >='" & Format(StartDate, "dd/MM/yy") & "') AND (Credit <> 0) "

                        DS1 = New DataSet
                        DS1 = .Get_Data_Sql(SQL)

                        If DS1.Tables(0).Rows.Count > 0 Then
                            If Not IsDBNull(DS1.Tables(0).Rows(0)(0)) Then
                                If DS1.Tables(0).Rows(0)(0) <> 0 Then

                                    dROL = 0
                                    dROQ = 0
                                    dMaxLvl = 0

                                    'ROL--------------------
                                    If DS1.Tables(0).Rows(0)(0) >= Integer.Parse(txtSAvg.Text) Then
                                        dROL = txtGreat.Value
                                    Else
                                        dROL = txtLess.Value
                                    End If

                                    'ROQ---------------------
                                    If IsDBNull(DS1.Tables(0).Rows(0)(1)) Then

                                    Else
                                        If DS1.Tables(0).Rows(0)(1) = "R" Then
                                            dROQ = DS1.Tables(0).Rows(0)(0) + DS1.Tables(0).Rows(0)(0) * Integer.Parse(txtRORRPercent.Text) / 100
                                        ElseIf DS1.Tables(0).Rows(0)(1) = "G" Then
                                            dROQ = DS1.Tables(0).Rows(0)(0) + DS1.Tables(0).Rows(0)(0) * Integer.Parse(txtRORGPercent.Text) / 100
                                        ElseIf DS1.Tables(0).Rows(0)(1) = "Y" Then
                                            dROQ = DS1.Tables(0).Rows(0)(0) + DS1.Tables(0).Rows(0)(0) * Integer.Parse(txtRORYPercent.Text) / 100
                                        Else
                                            dROQ = DS1.Tables(0).Rows(0)(0) + DS1.Tables(0).Rows(0)(0) * Integer.Parse(txtRORRPercent.Text) / 100
                                        End If
                                    End If


                                    'interval----------------
                                    dROL = dROL / Integer.Parse(txtInterval.Text)
                                    dROQ = dROQ / Integer.Parse(txtInterval.Text)

                                    dMaxLvl = dROQ





                                    dROL = Math.Ceiling(dROL)
                                    dROQ = Math.Ceiling(dROQ)
                                    dMaxLvl = Math.Ceiling(dMaxLvl)

                                    SQL = " Update StkItem SET Re_Ord_Lvl = " & dROL & " , Re_Ord_Qty = " & dROQ & ", Max_Lvl = " & dMaxLvl & ", Min_Lvl = 1  where StockLink =  " & Dr.Item(0)
                                    .Begin_Trans()
                                    If .Execute_Sql_Trans(SQL) = 0 Then
                                        .Rollback_Trans()
                                        MsgBox("Error in item - " & Dr.Item(0) & vbCrLf & "Process will be continued")
                                        'Exit Sub
                                    End If
                                    .Commit_Trans()
                                End If

                            End If

                        End If
                    End If
                    'End If
                Next
                MsgBox("Process successfuly Completed")
            End If

        End With
        WriteToFile()
    End Sub

    Private Sub WriteToFile()
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sSQLSrvDataBase & "ROLROQ.txt") Then
            My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\" & sSQLSrvDataBase & "ROLROQ.txt")
        End If

        Dim Writer As StreamWriter = New StreamWriter(Application.StartupPath & "\" & sSQLSrvDataBase & "ROLROQ.txt")
        Dim S As String = ""


        Writer.WriteLine(txtGreat.Text)
        Writer.WriteLine(txtInterval.Text)
        Writer.WriteLine(txtLess.Text)
        Writer.WriteLine(txtNoOfMonth.Text)
        Writer.WriteLine(txtRORGPercent.Text)
        Writer.WriteLine(txtRORRPercent.Text)
        Writer.WriteLine(txtRORYPercent.Text)
        Writer.WriteLine(txtSAvg.Text)
        Writer.WriteLine(dtpUpToDate.Value.ToString())



        ''Encrypt-------------------------------------------------------
        'Dim sym As New Encryption.Symmetric(Encryption.Symmetric.Provider.Rijndael)
        'Dim key As New Encryption.Data("My Password")
        'Dim encryptedData As Encryption.Data
        'encryptedData = sym.Encrypt(New Encryption.Data(cmbAgent.Text), key)
        'Dim base64EncryptedString As String = encryptedData.ToBase64
        'Writer.WriteLine(base64EncryptedString)

        'If cbSavePW.Checked = True Then
        '    'Writer.WriteLine(txtPassword.Text)
        '    'Encrypt-------------------------------------------------------
        '    sym = New Encryption.Symmetric(Encryption.Symmetric.Provider.Rijndael)
        '    key = New Encryption.Data("My Password")
        '    encryptedData = New Encryption.Data
        '    encryptedData = sym.Encrypt(New Encryption.Data(txtPassword.Text), key)
        '    base64EncryptedString = encryptedData.ToBase64
        '    Writer.WriteLine(base64EncryptedString)
        'Else
        '    Writer.WriteLine(S)
        'End If
        ''Writer.WriteLine(sSQLSrvName)
        ''Encrypt-------------------------------------------------------
        'sym = New Encryption.Symmetric(Encryption.Symmetric.Provider.Rijndael)
        'key = New Encryption.Data("My Password")
        'encryptedData = New Encryption.Data
        'encryptedData = sym.Encrypt(New Encryption.Data("UMSERVER"), key)
        'base64EncryptedString = encryptedData.ToBase64
        'Writer.WriteLine(base64EncryptedString)


        Writer.Close()
    End Sub

    Private Sub frmROL_ROQ_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpUpToDate.Value = Date.Now

    End Sub

    Private Sub UltraButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraButton1.Click
        WriteToFile()
    End Sub

    Private Sub txtNoOfMonth_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNoOfMonth.MouseHover
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sSQLSrvDataBase & "ROLROQ.txt") Then

            Dim Reader As StreamReader = New StreamReader(Application.StartupPath & "\" & sSQLSrvDataBase & "ROLROQ.txt")
            Reader.ReadLine()
            Reader.ReadLine()
            Reader.ReadLine()

            ToolTip1.Show(Reader.ReadLine(), txtNoOfMonth)
            Reader.Close()
        End If
    End Sub

    Private Sub txtGreat_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGreat.MouseHover
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sSQLSrvDataBase & "ROLROQ.txt") Then

            Dim Reader As StreamReader = New StreamReader(Application.StartupPath & "\" & sSQLSrvDataBase & "ROLROQ.txt")


            ToolTip1.Show(Reader.ReadLine(), txtGreat)
            Reader.Close()
        End If
    End Sub

    Private Sub txtInterval_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInterval.MouseHover
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sSQLSrvDataBase & "ROLROQ.txt") Then

            Dim Reader As StreamReader = New StreamReader(Application.StartupPath & "\" & sSQLSrvDataBase & "ROLROQ.txt")
            Reader.ReadLine()


            ToolTip1.Show(Reader.ReadLine(), txtInterval)
            Reader.Close()
        End If
    End Sub

    Private Sub txtLess_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLess.MouseHover
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sSQLSrvDataBase & "ROLROQ.txt") Then

            Dim Reader As StreamReader = New StreamReader(Application.StartupPath & "\" & sSQLSrvDataBase & "ROLROQ.txt")
            Reader.ReadLine()
            Reader.ReadLine()

            ToolTip1.Show(Reader.ReadLine(), txtLess)
            Reader.Close()
        End If
    End Sub

    Private Sub txtRORGPercent_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRORGPercent.MouseHover
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sSQLSrvDataBase & "ROLROQ.txt") Then

            Dim Reader As StreamReader = New StreamReader(Application.StartupPath & "\" & sSQLSrvDataBase & "ROLROQ.txt")
            Reader.ReadLine()
            Reader.ReadLine()
            Reader.ReadLine()
            Reader.ReadLine()

            ToolTip1.Show(Reader.ReadLine(), txtRORGPercent)
            Reader.Close()
        End If
    End Sub

    Private Sub txtRORRPercent_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRORRPercent.MouseHover
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sSQLSrvDataBase & "ROLROQ.txt") Then

            Dim Reader As StreamReader = New StreamReader(Application.StartupPath & "\" & sSQLSrvDataBase & "ROLROQ.txt")
            Reader.ReadLine()
            Reader.ReadLine()
            Reader.ReadLine()
            Reader.ReadLine()
            Reader.ReadLine()

            ToolTip1.Show(Reader.ReadLine(), txtRORRPercent)
            Reader.Close()
        End If
    End Sub

    Private Sub txtRORYPercent_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRORYPercent.MouseHover
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sSQLSrvDataBase & "ROLROQ.txt") Then

            Dim Reader As StreamReader = New StreamReader(Application.StartupPath & "\" & sSQLSrvDataBase & "ROLROQ.txt")
            Reader.ReadLine()
            Reader.ReadLine()
            Reader.ReadLine()
            Reader.ReadLine()
            Reader.ReadLine()
            Reader.ReadLine()

            ToolTip1.Show(Reader.ReadLine(), txtRORYPercent)
            Reader.Close()
        End If
    End Sub

    Private Sub txtSAvg_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSAvg.MouseHover
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sSQLSrvDataBase & "ROLROQ.txt") Then

            Dim Reader As StreamReader = New StreamReader(Application.StartupPath & "\" & sSQLSrvDataBase & "ROLROQ.txt")
            Reader.ReadLine()
            Reader.ReadLine()
            Reader.ReadLine()
            Reader.ReadLine()
            Reader.ReadLine()
            Reader.ReadLine()
            Reader.ReadLine()

            ToolTip1.Show(Reader.ReadLine(), txtSAvg)
            Reader.Close()
        End If
    End Sub

    Private Sub dtpUpToDate_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpUpToDate.MouseHover
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sSQLSrvDataBase & "ROLROQ.txt") Then

            Dim Reader As StreamReader = New StreamReader(Application.StartupPath & "\" & sSQLSrvDataBase & "ROLROQ.txt")
            Reader.ReadLine()
            Reader.ReadLine()
            Reader.ReadLine()
            Reader.ReadLine()
            Reader.ReadLine()
            Reader.ReadLine()
            Reader.ReadLine()
            Reader.ReadLine()

            ToolTip1.Show(Reader.ReadLine(), dtpUpToDate)
            Reader.Close()
        End If
    End Sub
End Class