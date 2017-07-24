Imports System.Data
Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinTree
Public Class frmAccessRights
    Private Sub Get_Agent()
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                SQL = "SELECT     idAgents, cAgentName, cDescription FROM _rtblAgents"
                DS = New DataSet
                DS = .GET_DATA_SQL(SQL)
                UG.DataSource = DS.Tables(0)
                UG.DisplayLayout.Bands(0).Columns("idAgents").Header.Caption = "Agent Id"
                UG.DisplayLayout.Bands(0).Columns("cAgentName").Header.Caption = "Agent Name"
                UG.DisplayLayout.Bands(0).Columns("cDescription").Header.Caption = "Description"
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Agent Master")
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try
        End With

    End Sub
    Private Sub Apply_Access_Permission()
        Dim mRootNode As UltraTreeNode = TC.GetNodeByKey("1000")
        mRootNode.CheckedState = CheckState.Checked
        If mRootNode.HasNodes = True Then
            Dim iNode1 As UltraTreeNode
            For Each iNode1 In mRootNode.Nodes
                iNode1.CheckedState = CheckState.Checked
                If iNode1.HasNodes = True Then
                    Dim iNode2 As UltraTreeNode
                    For Each iNode2 In iNode1.Nodes
                        iNode2.CheckedState = CheckState.Checked
                        If iNode2.HasNodes = True Then
                            Dim iNode3 As UltraTreeNode
                            For Each iNode3 In iNode2.Nodes
                                iNode3.CheckedState = CheckState.Checked
                                If iNode3.HasNodes = True Then
                                    Dim iNode4 As UltraTreeNode
                                    For Each iNode4 In iNode3.Nodes
                                        iNode4.CheckedState = CheckState.Checked
                                        If iNode4.HasNodes = True Then
                                            Dim iNode5 As UltraTreeNode
                                            For Each iNode5 In iNode4.Nodes
                                                iNode5.CheckedState = CheckState.Checked
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
            Next
        End If
    End Sub

    Private Sub Clear_Access_Permission()
        Dim mRootNode As UltraTreeNode = TC.GetNodeByKey("2000")
        mRootNode.CheckedState = CheckState.Unchecked
        If mRootNode.HasNodes = True Then
            Dim iNode1 As UltraTreeNode
            For Each iNode1 In mRootNode.Nodes
                iNode1.CheckedState = CheckState.Unchecked
                If iNode1.HasNodes = True Then
                    Dim iNode2 As UltraTreeNode
                    For Each iNode2 In iNode1.Nodes
                        iNode2.CheckedState = CheckState.Unchecked
                        If iNode2.HasNodes = True Then
                            Dim iNode3 As UltraTreeNode
                            For Each iNode3 In iNode2.Nodes
                                iNode3.CheckedState = CheckState.Unchecked
                                If iNode3.HasNodes = True Then
                                    Dim iNode4 As UltraTreeNode
                                    For Each iNode4 In iNode3.Nodes
                                        iNode4.CheckedState = CheckState.Unchecked
                                        If iNode4.HasNodes = True Then
                                            Dim iNode5 As UltraTreeNode
                                            For Each iNode5 In iNode4.Nodes
                                                iNode5.CheckedState = CheckState.Unchecked
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
            Next
        End If
        
    End Sub
    Private Sub frmAccessRights_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TC.ExpandAll(Infragistics.Win.UltraWinTree.ExpandAllType.Always)
        tsbApply.Enabled = False
        Get_Agent()
    End Sub
    Public Sub Get_Access_Permission_For_Agent()
        Dim objSQL As New clsSqlConn
        With objSQL
            SQL = "SELECT iManu FROM Spil_Access_Rights WHERE idAgents =" & UG.ActiveRow.Cells("idAgents").Value & ""
            DS = New DataSet
            DS = .GET_DATA_SQL(SQL)
            If DS.Tables(0).Rows.Count > 0 Then
                Dim R As DataRow
                For Each R In DS.Tables(0).Rows
                    Dim mRootNode As UltraTreeNode = TC.GetNodeByKey("2000")
                    If R("iManu") = mRootNode.Tag Then
                        mRootNode.CheckedState = CheckState.Checked
                    End If
                    If mRootNode.HasNodes = True Then
                        Dim iNode1 As UltraTreeNode
                        For Each iNode1 In mRootNode.Nodes
                            If R("iManu") = iNode1.Tag Then
                                iNode1.CheckedState = CheckState.Checked
                            End If
                            If iNode1.HasNodes = True Then
                                Dim iNode2 As UltraTreeNode
                                For Each iNode2 In iNode1.Nodes
                                    If R("iManu") = iNode2.Tag Then
                                        iNode2.CheckedState = CheckState.Checked
                                    End If
                                    If iNode2.HasNodes = True Then
                                        Dim iNode3 As UltraTreeNode
                                        For Each iNode3 In iNode2.Nodes
                                            If R("iManu") = iNode3.Tag Then
                                                iNode3.CheckedState = CheckState.Checked
                                            End If
                                            If iNode3.HasNodes = True Then
                                                Dim iNode4 As UltraTreeNode
                                                For Each iNode4 In iNode3.Nodes
                                                    If R("iManu") = iNode4.Tag Then
                                                        iNode4.CheckedState = CheckState.Checked
                                                    End If
                                                    If iNode4.HasNodes = True Then
                                                        Dim iNode5 As UltraTreeNode
                                                        For Each iNode5 In iNode4.Nodes
                                                            If R("iManu") = iNode5.Tag Then
                                                                iNode5.CheckedState = CheckState.Checked
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If

                Next
            End If
        End With

        
    End Sub
    Private Sub UG_DoubleClickRow(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs) Handles UG.DoubleClickRow
        If e.Row.Selected = True Then
            Clear_Access_Permission()
            UTC.Tabs(1).Enabled = True
            UTC.SelectedTab = UTC.Tabs(1)
            Get_Access_Permission_For_Agent()
            tsbApply.Enabled = True
        End If
    End Sub
    Private Sub UTC_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UTC.SelectedTabChanged
        If UTC.SelectedTab.Index = 0 Then
            UTC.Tabs(1).Enabled = False
            tsbApply.Enabled = False
        Else
            UTC.Tabs(1).Enabled = True
            tsbApply.Enabled = True
        End If
    End Sub

    Private Sub tsbApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbApply.Click
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                .Begin_Trans()

                SQL = "DELETE FROM Spil_Access_Rights WHERE idAgents =" & UG.ActiveRow.Cells("idAgents").Value & ""
                If .Execute_Sql_Trans(SQL) = 0 Then
                    .Rollback_Trans()
                    MsgBox("Error Found While Deleting Data", MsgBoxStyle.Exclamation, "Sage Evolution")
                    Exit Sub
                End If

                Dim iAgent As Integer = UG.ActiveRow.Cells("idAgents").Value

                Dim mRootNode As UltraTreeNode = TC.GetNodeByKey("2000")
                If mRootNode.CheckedState = CheckState.Checked Then
                    SQL = "INSERT INTO Spil_Access_Rights(idAgents, iManu) VALUES (" & iAgent & "," & mRootNode.Tag & ")"
                    If .Execute_Sql_Trans(SQL) = 0 Then
                        .Rollback_Trans()
                        MsgBox("Error Found While Deleting Data", MsgBoxStyle.Exclamation, "Sage Evolution")
                        Exit Sub
                    End If
                End If

                If mRootNode.HasNodes = True Then
                    Dim iNode1 As UltraTreeNode
                    For Each iNode1 In mRootNode.Nodes
                        If iNode1.CheckedState = CheckState.Checked Then
                            SQL = "INSERT INTO Spil_Access_Rights(idAgents, iManu) VALUES (" & iAgent & "," & iNode1.Tag & ")"
                            If .Execute_Sql_Trans(SQL) = 0 Then
                                .Rollback_Trans()
                                MsgBox("Error Found While Deleting Data", MsgBoxStyle.Exclamation, "Sage Evolution")
                                Exit Sub
                            End If
                        End If

                        If iNode1.HasNodes = True Then
                            Dim iNode2 As UltraTreeNode
                            For Each iNode2 In iNode1.Nodes
                                If iNode2.CheckedState = CheckState.Checked Then
                                    SQL = "INSERT INTO Spil_Access_Rights(idAgents, iManu) VALUES (" & iAgent & "," & iNode2.Tag & ")"
                                    If .Execute_Sql_Trans(SQL) = 0 Then
                                        .Rollback_Trans()
                                        MsgBox("Error Found While Deleting Data", MsgBoxStyle.Exclamation, "Sage Evolution")
                                        Exit Sub
                                    End If
                                End If

                                If iNode2.HasNodes = True Then
                                    Dim iNode3 As UltraTreeNode
                                    For Each iNode3 In iNode2.Nodes
                                        If iNode3.CheckedState = CheckState.Checked Then
                                            SQL = "INSERT INTO Spil_Access_Rights(idAgents, iManu) VALUES (" & iAgent & "," & iNode3.Tag & ")"
                                            If .Execute_Sql_Trans(SQL) = 0 Then
                                                .Rollback_Trans()
                                                MsgBox("Error Found While Deleting Data", MsgBoxStyle.Exclamation, "Sage Evolution")
                                                Exit Sub
                                            End If
                                        End If

                                        If iNode3.HasNodes = True Then
                                            Dim iNode4 As UltraTreeNode
                                            For Each iNode4 In iNode3.Nodes
                                                If iNode4.CheckedState = CheckState.Checked Then
                                                    SQL = "INSERT INTO Spil_Access_Rights(idAgents, iManu) VALUES (" & iAgent & "," & iNode4.Tag & ")"
                                                    If .Execute_Sql_Trans(SQL) = 0 Then
                                                        .Rollback_Trans()
                                                        MsgBox("Error Found While Deleting Data", MsgBoxStyle.Exclamation, "Sage Evolution")
                                                        Exit Sub
                                                    End If
                                                End If
                                                If iNode4.HasNodes = True Then
                                                    Dim iNode5 As UltraTreeNode
                                                    For Each iNode5 In iNode4.Nodes
                                                        If iNode5.CheckedState = CheckState.Checked Then
                                                            SQL = "INSERT INTO Spil_Access_Rights(idAgents, iManu) VALUES (" & iAgent & "," & iNode5.Tag & ")"
                                                            If .Execute_Sql_Trans(SQL) = 0 Then
                                                                .Rollback_Trans()
                                                                MsgBox("Error Found While Deleting Data", MsgBoxStyle.Exclamation, "Sage Evolution")
                                                                Exit Sub
                                                            End If
                                                        End If
                                                    Next
                                                End If
                                            Next
                                        End If

                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
               

                .Commit_Trans()
                If MsgBox("Some changes will not effect untill you log off" & vbLf & "Do you want to log off now?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Sage Evolution - Agent Permissions") = MsgBoxResult.Yes Then
                    CB_Apply.Checked = False
                    'Application.Restart()
                End If
                CB_Apply.Checked = False
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Sage Evolution - Agent Permissions")
                .Rollback_Trans()
            Finally
                .Con_Close()
                Clear_Access_Permission()
            End Try
        End With
    End Sub

    Private Sub tsbClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbClose.Click
        Me.Close()
    End Sub

    Private Sub CB_Apply_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_Apply.CheckedChanged
        If CB_Apply.Checked = True Then
            Apply_Access_Permission()
        Else
            Clear_Access_Permission()
        End If
    End Sub

    Private Sub TC_AfterSelect(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTree.SelectEventArgs) Handles TC.AfterSelect

    End Sub
End Class