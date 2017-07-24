Imports System.Data
Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid

Public Class frmExpDate
    Dim aDataBase As ArrayList
    Private Sub GetDatabaseName()
        aDataBase = New ArrayList()
        aDataBase.Add("dbUdawatta_new")
        'aDataBase.Add("dbKiribathgoda_new")
        'aDataBase.Add("dbKurunegala_new")
        aDataBase.Add("dbNawinna_new")
        aDataBase.Add("dbKelaniya_new")
        aDataBase.Add("dbMathara")
        'aDataBase.Add("UDAWATTA_TEST1")
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim sDB As String
        Dim Obj As Object
        For Each Obj In aDataBase

            sDB = Obj.ToString


            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & sDB & "")
            SQL = " set dateformat dmy update _etblLotTracking set dExpiryDate = '" & dtTo.Value & "'  Where dExpiryDate < '" & dtFrom.Value & "' "
            CMD = New SqlCommand(SQL, Con2)
            CMD.CommandType = CommandType.Text
            Con2.Open()
            CMD.ExecuteNonQuery()
            Con2.Close()
        Next
        MessageBox.Show("Successfully Updated", "Pastel Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub


    Private Sub frmExpDate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetDatabaseName()
    End Sub
End Class