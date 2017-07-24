Public Class frmSerialNumberEnquiries
    Public Sub GetSNData(ByVal Code As String)
        UG.DataSource = Nothing

        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                If cbAll.Checked = False Then
                    SQL = "SELECT SNTxCounter AS ID, SNTxDate AS Date, SNTxReference AS Reference," & _
                    " SNAccModule AS Module, cSNTXReference2 AS Reference2, TransAccount, " & _
                    " TransTypeDesc, Movement, SerialNumber, SNDateLMove AS [Location Move Date], " & _
                    " CurrentAccount, CurrentLocationDesc, WarehouseCode, TrCode  FROM _bvSerialNumbersFull WHERE SerialNumber ='" & CStr(Code) & "' "
                ElseIf cbAll.Checked = True Then
                    SQL = "SELECT SNTxCounter AS ID, SNTxDate AS Date, SNTxReference AS Reference," & _
                    " SNAccModule AS Module, cSNTXReference2 AS Reference2, TransAccount, " & _
                    " TransTypeDesc, Movement, SerialNumber, SNDateLMove AS [Location Move Date], " & _
                    " CurrentAccount, CurrentLocationDesc, WarehouseCode, TrCode  FROM _bvSerialNumbersFull "
                End If
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                UG.DataSource = DS.Tables(0)
                If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sAgent & "SNE.xml") Then
                    UG.DisplayLayout.LoadFromXml(Application.StartupPath & "\" & sAgent & "SNE.xml")
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
            Finally
                DS.Dispose()
                objSQL = Nothing
            End Try
        End With
    End Sub
    Private Sub txtSN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSN.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim sBarCode As String = txtSN.Text.Trim
            GetSNData(sBarCode)
            txtSN.ResetText()
        End If

    End Sub
    Private Sub tsbExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbExport.Click
        Dim myFineName As String


        Try

            Dim fdlg As SaveFileDialog = New SaveFileDialog
            fdlg.Title = "Excel Data Importer"
            fdlg.InitialDirectory = "C:\"
            fdlg.Filter = "MS Excel (*.xls)|*.xls"

            fdlg.RestoreDirectory = True

            myFineName = ""

            If fdlg.ShowDialog() = DialogResult.OK Then
                myFineName = fdlg.FileName
            End If


            If myFineName = "" Then
                MsgBox("Please enter a file name", MsgBoxStyle.Information, "Validation")
                Exit Sub
            End If

            ugExcelExporter.Export(UG, myFineName)


            Dim Proc As New System.Diagnostics.Process
            Proc.StartInfo.FileName = myFineName
            Proc.Start()

        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub tsbSaveMySetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSaveMySetting.Click
        Try
            UG.DisplayLayout.SaveAsXml(Application.StartupPath & "\" & sAgent & "SNE.xml")
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Private Sub tsbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
    End Sub
    Private Sub cbAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAll.CheckedChanged
        If cbAll.Checked = True Then
            txtSN.Enabled = False
            GetSNData("")
        ElseIf cbAll.Checked = False Then
            txtSN.Enabled = True
        End If
    End Sub
End Class