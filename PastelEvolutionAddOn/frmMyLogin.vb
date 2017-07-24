Imports System.Data
Imports System.Data.SqlClient
Imports Pastel.Evolution
Imports System.IO
Imports Infragistics.Win.UltraWinTree
Imports EncryptionClassLibrary


Public Class frmMyLogin
    Private Sub ReadParameterFile()
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\SQLPara.txt") Then
            Dim reader As StreamReader = New StreamReader(Application.StartupPath & "\SQLpara.txt")

            ''Decrypt--------------------------------------------
            'Dim sym As New Encryption.Symmetric(Encryption.Symmetric.Provider.Rijndael)
            'Dim key As New Encryption.Data("My Password")
            'Dim encryptedData As New Encryption.Data
            'encryptedData.Base64 = base64EncryptedString
            'Dim decryptedData As Encryption.Data
            'decryptedData = sym.Decrypt(encryptedData, key)
            'Console.WriteLine(decryptedData.ToString)



            'cmbAgent.Text = reader.ReadLine
            'Decrypt--------------------------------------------
            Dim sym As New Encryption.Symmetric(Encryption.Symmetric.Provider.Rijndael)
            Dim key As New Encryption.Data("My Password")
            Dim encryptedData As New Encryption.Data
            encryptedData.Base64 = reader.ReadLine
            Dim decryptedData As Encryption.Data
            decryptedData = sym.Decrypt(encryptedData, key)
            cmbAgent.Text = decryptedData.ToString

            Dim pass As String = reader.ReadLine
            If pass.Length > 0 Then
                'txtPassword.Text = reader.ReadLine
                'Decrypt--------------------------------------------
                sym = New Encryption.Symmetric(Encryption.Symmetric.Provider.Rijndael)
                key = New Encryption.Data("My Password")
                encryptedData = New Encryption.Data
                encryptedData.Base64 = pass
                decryptedData = New Encryption.Data
                decryptedData = sym.Decrypt(encryptedData, key)
                txtPassword.Text = decryptedData.ToString
            Else
                txtPassword.Text = pass
            End If
            If txtPassword.Text.Trim.Length > 0 Then
                cbSavePW.Checked = True
            Else
                cbSavePW.Checked = False
            End If


            'sSQLSrvName = reader.ReadLine
            'Decrypt--------------------------------------------
            sym = New Encryption.Symmetric(Encryption.Symmetric.Provider.Rijndael)
            key = New Encryption.Data("My Password")
            encryptedData = New Encryption.Data
            encryptedData.Base64 = reader.ReadLine
            decryptedData = New Encryption.Data
            decryptedData = sym.Decrypt(encryptedData, key)
            sSQLSrvName = decryptedData.ToString


            reader.Close()




            'cmbAgent.Text = reader.ReadLine
            'txtPassword.Text = reader.ReadLine
            'If txtPassword.Text.Trim.Length > 0 Then
            '    cbSavePW.Checked = True
            'Else
            '    cbSavePW.Checked = False
            'End If
            'sSQLSrvName = reader.ReadLine
            'reader.Close()
        End If
    End Sub

    Private Sub Get_SQL_LOGIN()
        Dim mRootNode As UltraTreeNode = ut.GetNodeByKey("0")
        Dim reader As StreamReader = New StreamReader(Application.StartupPath & "\SQLparaNew.txt")
        Dim i As Integer = 0
        Dim iLine As Integer = 0
        While reader.Peek <> -1

            'Dim sDB As String = reader.ReadLine()  'DB Name 

            'txtPassword.Text = reader.ReadLine
            'Decrypt--------------------------------------------
            Dim sym As New Encryption.Symmetric(Encryption.Symmetric.Provider.Rijndael)
            Dim key As New Encryption.Data("My Password")
            Dim encryptedData As New Encryption.Data
            encryptedData.Base64 = reader.ReadLine()
            Dim decryptedData As Encryption.Data
            decryptedData = sym.Decrypt(encryptedData, key)
            Dim sDB As String = decryptedData.ToString


            If sDB.Contains(",") = True Then
                Dim aDB() As String = sDB.Split(",")
                Dim iR As Integer = 0
                If mRootNode.HasNodes = False Then
                    For iR = 0 To aDB.Length - 1
                        iLine = iLine + 1
                        If mRootNode.HasNodes = False Then
                            mRootNode.Nodes.Add(aDB(iR) & i & iLine, aDB(iR))
                        Else
                            Dim mUserNameNode As UltraTreeNode = mRootNode.Nodes(i)
                            If mUserNameNode.HasNodes = False Then
                                mUserNameNode.Nodes.Add(aDB(iR) & iLine, aDB(iR))
                                'mUserNameNode.Visible = False
                            Else
                                Dim mPWNode As UltraTreeNode = mUserNameNode.Nodes(i)
                                If mPWNode.HasNodes = False Then
                                    mPWNode.Nodes.Add(aDB(iR) & i & iLine, aDB(iR))
                                    mPWNode.Visible = False
                                Else
                                    Dim mCommonDBNode As UltraTreeNode = mPWNode.Nodes(i)
                                    If mCommonDBNode.HasNodes = False Then
                                        mCommonDBNode.Nodes.Add(aDB(iR) & i & iLine, aDB(iR))
                                        mCommonDBNode.Visible = False
                                    Else
                                        Dim mReportServerNode As UltraTreeNode = mCommonDBNode.Nodes(i)
                                        If mReportServerNode.HasNodes = False Then
                                            mReportServerNode.Nodes.Add(aDB(iR) & i & iLine, aDB(iR))
                                            mReportServerNode.Visible = False
                                        End If
                                    End If
                                End If
                            End If

                        End If
                    Next
                Else
                    For iR = 0 To aDB.Length - 1
                        iLine = iLine + 1
                        If mRootNode.Nodes(i - 1).HasSibling(NodePosition.Next) = False Then
                            mRootNode.Nodes.Add(aDB(iR) & i & iLine, aDB(iR))
                        Else
                            Dim mUserNameNode As UltraTreeNode = mRootNode.Nodes(i)
                            If mUserNameNode.HasNodes = False Then
                                mUserNameNode.Nodes.Add(aDB(iR) & i & iLine, aDB(iR))
                            Else
                                Dim mPWNode As UltraTreeNode = mUserNameNode.Nodes(0)
                                If mPWNode.HasNodes = False Then
                                    mPWNode.Nodes.Add(aDB(iR) & i & iLine, aDB(iR))
                                    mPWNode.Visible = False
                                Else
                                    Dim mCommonDBNode As UltraTreeNode = mPWNode.Nodes(0)
                                    If mCommonDBNode.HasNodes = False Then
                                        mCommonDBNode.Nodes.Add(aDB(iR) & i & iLine, aDB(iR))
                                        mCommonDBNode.Visible = False
                                    Else
                                        Dim mReportServerNode As UltraTreeNode = mCommonDBNode.Nodes(0)
                                        If mReportServerNode.HasNodes = False Then
                                            mReportServerNode.Nodes.Add(aDB(iR) & i & iLine, aDB(iR))
                                            mReportServerNode.Visible = False
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Next
                End If
                i = i + 1
            End If
        End While
    End Sub

    Private Sub frmMyLogin_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Application.Exit()
    End Sub
    Private Sub frmMyLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Dim i As Integer
        'i = DateDiff(DateInterval.Day, Now(), CDate("10/3/2011 6:00:00"))
        'If i <> 0 Then
        '    If i < 7 Then
        '        'MessageBox.Show("This product will expire in " & i & " day(s)", "Evolution Custom", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    End If
        'Else
        '    'MessageBox.Show("This product has expired", "Evolution Custom", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Application.Exit()
        'End If

        Try
            ReadParameterFile()
            Get_SQL_LOGIN()
            ut.Nodes(0).Expanded = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "Evolution")
            Me.Close()
        End Try
    End Sub
    Private Function CHECK_FOR_CONNECTION_SQL_SERVER() As Integer
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                If .Con_Open = 0 Then
                    Return 0
                Else
                    .Con_Close()
                    Return 1
                End If
            Catch ex As Exception
                Return 0
            Finally
                objSQL = Nothing
            End Try
        End With
    End Function
    Private Sub WriteParameterFile()
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\SQLPara.txt") Then
            My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\SQLPara.txt")
        End If

        Dim Writer As StreamWriter = New StreamWriter(Application.StartupPath & "\SQLPara.txt")
        Dim S As String = ""

        ''Encrypt-------------------------------------------------------
        'Dim sym As New Encryption.Symmetric(Encryption.Symmetric.Provider.Rijndael)
        'Dim key As New Encryption.Data("My Password")
        'Dim encryptedData As Encryption.Data
        'encryptedData = sym.Encrypt(New Encryption.Data("Secret Sauce"), key)
        'Dim base64EncryptedString As String = encryptedData.ToBase64


        'Writer.WriteLine(cmbAgent.Text)
        'Encrypt-------------------------------------------------------
        Dim sym As New Encryption.Symmetric(Encryption.Symmetric.Provider.Rijndael)
        Dim key As New Encryption.Data("My Password")
        Dim encryptedData As Encryption.Data
        encryptedData = sym.Encrypt(New Encryption.Data(cmbAgent.Text), key)
        Dim base64EncryptedString As String = encryptedData.ToBase64
        Writer.WriteLine(base64EncryptedString)

        If cbSavePW.Checked = True Then
            'Writer.WriteLine(txtPassword.Text)
            'Encrypt-------------------------------------------------------
            sym = New Encryption.Symmetric(Encryption.Symmetric.Provider.Rijndael)
            key = New Encryption.Data("My Password")
            encryptedData = New Encryption.Data
            encryptedData = sym.Encrypt(New Encryption.Data(txtPassword.Text), key)
            base64EncryptedString = encryptedData.ToBase64
            Writer.WriteLine(base64EncryptedString)
        Else
            Writer.WriteLine(S)
        End If
        'Writer.WriteLine(sSQLSrvName)
        'Encrypt-------------------------------------------------------
        sym = New Encryption.Symmetric(Encryption.Symmetric.Provider.Rijndael)
        key = New Encryption.Data("My Password")
        encryptedData = New Encryption.Data
        encryptedData = sym.Encrypt(New Encryption.Data("UMSERVER"), key)
        base64EncryptedString = encryptedData.ToBase64
        Writer.WriteLine(base64EncryptedString)


        Writer.Close()
    End Sub
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        'strSVR_Name = ut.GetNodeByKey("0").Text

        'Dim versionInfo As FileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo("\\Umserver\IQ_SYSTEM\updates\Inventory\PastelEvolutionAddOn.exe")
        'If Application.ProductVersion < versionInfo.FileVersion Then
        '    Dim gfg As Integer
        'End If
        Dim utNode As UltraTreeNode
        Dim iCount As Integer = 0

        For Each utNode In ut.SelectedNodes
            If utNode.Selected = True Then
                iCount = iCount + 1
                sSQLSrvDataBase = CStr(utNode.Text.Trim)
                Dim i As Integer = utNode.Index
                If utNode.HasNodes = True Then
                    Dim utNode1 As UltraTreeNode
                    For Each utNode1 In utNode.Nodes
                        sSQLSrvUserName = CStr(utNode1.Text.Trim)
                        If utNode1.HasNodes = True Then
                            Dim utNode2 As UltraTreeNode
                            For Each utNode2 In utNode1.Nodes
                                sSQLSrvPassword = CStr(utNode2.Text.Trim)
                                If utNode2.HasNodes = True Then
                                    Dim utNode3 As UltraTreeNode
                                    For Each utNode3 In utNode2.Nodes
                                        sSQLSrvCommonDB = CStr(utNode3.Text.Trim)
                                        If utNode3.HasNodes = True Then
                                            Dim utNode4 As UltraTreeNode
                                            For Each utNode4 In utNode3.Nodes
                                                sSQLSrvReportSrv = CStr(utNode4.Text.Trim)
                                            Next
                                        End If
                                    Next

                                End If
                            Next

                        End If
                    Next
                End If
            End If
        Next
        If iCount = 0 Then
            MsgBox("Select Company", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        'DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
        'DatabaseContext.SetLicense("DE09110022", "1428511")
        'DatabaseContext.CreateConnection(sSQLSrvName, sSQLSrvDataBase, sSQLSrvUserName, sSQLSrvPassword, False)

        'If Pastel.Evolution.Agent.Authenticate(cmbAgent.Text.Trim.ToString, txtPassword.Text.Trim.ToString) = False Then
        '    MessageBox.Show("System could not log-on you" & vbCrLf & "Makesure Agent Name and PassWord is Correct", "Pastel Evolution Log-on ", MessageBoxButtons.OK)
        '    Exit Sub
        'End If

        sConStr = "Server=" & sSQLSrvName & ";Database=" & sSQLSrvDataBase & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ""
        Dim objSQL As New clsSqlConn
        Dim dr As DataRow

        With objSQL
            Try
                If cmbAgent.Text.Trim.Length = 0 Then
                    MsgBox("User Name can not be left blank", MsgBoxStyle.Exclamation, "Evolution")
                    Exit Sub
                End If

                If CHECK_FOR_CONNECTION_SQL_SERVER() = 0 Then
                    MsgBox("Error in Connection to the server and Database", MsgBoxStyle.Exclamation, "Evolution")
                    Exit Sub
                End If

                SQL = "SELECT Name FROM Entities"
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                If DS.Tables(0).Rows.Count > 0 Then
                    For Each dr In DS.Tables(0).Rows
                        sCompanyName = dr("Name")
                    Next
                End If

                SQL = "SELECT  cAgentName,idAgents FROM _rtblAgents WHERE  cAgentName = '" & cmbAgent.Text & "'"
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)

                If DS.Tables(0).Rows.Count = 0 Then
                    MessageBox.Show("Error in user", "Evolution Glass ", MessageBoxButtons.OK)
                    Exit Sub
                Else
                    For Each dr In DS.Tables(0).Rows
                        sAgent = dr("cAgentName")
                        iAgent = dr("idAgents")
                    Next
                End If


                SQL = "SELECT  cAgentName,idAgents FROM _rtblAgents WHERE  cAgentName = '" & cmbAgent.Text & "'"
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)

                If DS.Tables(0).Rows.Count = 0 Then
                    MessageBox.Show("Error in user", "Evolution Glass ", MessageBoxButtons.OK)
                    Exit Sub
                Else
                    For Each dr In DS.Tables(0).Rows
                        sAgent = dr("cAgentName")
                        iAgent = dr("idAgents")
                    Next
                End If


                WriteParameterFile()


                SQL = " select convert(varchar(10), GETDATE(), 120) "
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)

                Dim da = CDate(DS.Tables(0).Rows(0)(0).ToString())
                If DateDiff(DateInterval.Hour, Now.Date(), da) <> 0 Then
                    MessageBox.Show("Server date is different from local date", "Evolution AddOn")
                    Exit Sub
                End If



                Me.Hide()
                frmMDI.Get_Access_Permission(iAgent)
                frmMDI.ShowDialog()

            Catch ex As Exception

                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evoltuion")

            Finally

                GC.Collect()
                If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
                If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

            End Try
        End With
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        Application.Exit()
    End Sub

    Private Sub ut_AfterSelect(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTree.SelectEventArgs) Handles ut.AfterSelect
        If ut.GetNodeByKey("0").Selected = True Then
            ut.GetNodeByKey("0").Selected = False
        End If
    End Sub

    Private Sub UltraButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraButton5.Click
        Dim reader As StreamReader = New StreamReader(Application.StartupPath & "\company.txt")
        Dim Writer As StreamWriter = New StreamWriter(Application.StartupPath & "\SQLparaNew1.txt")
        While reader.Peek <> -1
            'Encrypt-------------------------------------------------------
            Dim sym As New Encryption.Symmetric(Encryption.Symmetric.Provider.Rijndael)
            Dim key As New Encryption.Data("My Password")
            Dim encryptedData As Encryption.Data
            encryptedData = sym.Encrypt(New Encryption.Data(reader.ReadLine()), key)
            Dim base64EncryptedString As String = encryptedData.ToBase64
            Writer.WriteLine(base64EncryptedString)
        End While
        Writer.Close()
    End Sub
End Class