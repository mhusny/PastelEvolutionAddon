Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient

Public Class clsSqlConn
    Public Shared Con As SqlConnection
    Public Trans As SqlTransaction
    Public DA As SqlDataAdapter
    Public DS As DataSet
    Public CMD As SqlCommand
    Public DR As SqlDataReader
    Dim No As Object

    Shared Sub New()
        Con = New SqlConnection(sConStr)
    End Sub

    Public Overridable Function Con_Open() As Integer
        Try
            Con.Open()
            Return 1
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Overridable Function Con_Close() As Integer
        Try
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
            Return 1
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Sub Begin_Trans()
        If Con.State = ConnectionState.Open Then Con.Close()
        Con.Open()
        Trans = Con.BeginTransaction
    End Sub

    Public Sub Commit_Trans()
        Trans.Commit()
        If Con.State = ConnectionState.Open Then Con.Close()
    End Sub

    Public Sub Rollback_Trans()
        Trans.Rollback()
        If Con.State = ConnectionState.Open Then Con.Close()
    End Sub

    Public Function Get_Data_Sql_Trans(ByVal SQL As String) As DataSet
        CMD = New SqlCommand(SQL, Con, Trans)
        CMD.CommandType = CommandType.Text
        DS = New DataSet
        DA = New SqlDataAdapter(CMD)
        DA.Fill(DS)
        Return DS
    End Function

    Public Function Get_Data_Sql(ByVal SQL As String) As DataSet
        CMD = New SqlCommand(SQL, Con)
        CMD.CommandType = CommandType.Text
        DS = New DataSet
        DA = New SqlDataAdapter(CMD)
        If Con.State = ConnectionState.Open Then
            Con.Close()
        End If
        Con.Open()
        DA.Fill(DS)
        Con.Close()
        Return DS
    End Function

    Public Function Check_Data(ByVal SQL As String) As Integer
        Dim CMD As SqlCommand
        Dim DR As SqlDataReader

        Try
            CMD = New SqlCommand(SQL, Con, Trans)
            CMD.CommandType = CommandType.Text
            Con.Open()
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then
                'DR.Close()
                Return 1
            Else
                'DR.Close()
                Return 2
            End If
        Catch ex As Exception
            Return 0
        Finally
            Con.Close()
            DR.Close()
        End Try
    End Function

    Public Overloads Function Get_Max_No(ByVal SQL As String) As Integer
        Try
            CMD = New SqlCommand(SQL, Con, Trans)
            CMD.CommandType = CommandType.Text
            No = CMD.ExecuteScalar
            If IsDBNull(No) = True Then
                Return 1
            Else
                Return CInt(No)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            Return -1
        End Try
    End Function

    Public Function Execute_Sql_Trans(ByVal SQL As String) As Integer
        Try
            CMD = New SqlCommand(SQL, Con, Trans)
            CMD.CommandType = CommandType.Text
            CMD.ExecuteNonQuery()
            Return 1
        Catch ex As Exception
            MsgBox(ex.Message)
            Return 0
        End Try
    End Function
    Public Function Execute_Sql_Trans2(ByVal SQL As String) As Integer
        Try
            Con2.Open()
            CMD = New SqlCommand(SQL, Con2, Trans)
            CMD.CommandType = CommandType.Text
            CMD.ExecuteNonQuery()
            Con2.Close()
            Return 1
        Catch ex As Exception
            MsgBox(ex.Message)
            Return 0
        End Try
    End Function

    Public Function Execute_Sql(ByVal SQL As String) As Integer
        Try
            CMD = New SqlCommand(SQL, Con)
            CMD.CommandType = CommandType.Text
            Con.Open()
            CMD.ExecuteNonQuery()
            Con.Close()
            Return 1
        Catch ex As Exception
            MsgBox(ex.Message)
            Return 0
        End Try
    End Function

End Class
