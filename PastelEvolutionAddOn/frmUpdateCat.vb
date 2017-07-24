Imports Pastel.Evolution
Imports System.Data
Imports System.Data.SqlClient
Public Class frmUpdateCat

    Private Sub tsbBin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBin.Click
        UpdateCat()
    End Sub
    Private Sub UpdateCat()

        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                .Begin_Trans()
                SQL = " Update StkItem  SET ucIICategory = '" & Mid(cmbCat.Text, 1, 1) & "'  where Code = '" & txtSimpleCode.Text & "'  "

                If .Execute_Sql_Trans(SQL) = 0 Then
                    .Rollback_Trans()
                    Exit Sub
                End If

                .Commit_Trans()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                Exit Sub
            Finally
                objSQL = Nothing
            End Try
        End With



        Me.Close()

    End Sub

    Private Sub txtBin_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            UpdateCat()
        End If
    End Sub
End Class