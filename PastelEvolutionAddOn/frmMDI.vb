Imports Infragistics.Win.UltraWinTree

Public Class frmMDI

    Private Sub tsbSalesOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSalesOrder.Click
        frmSalesOrder.iDocType = DocType.SalesOrder
        frmSalesOrder.Discard()
        frmSalesOrder.DocumentSettings(DocType.SalesOrder)
        frmSalesOrder.Text = "Sales Order"
        frmSalesOrder.IsNew = True
        Dim Dr As DataRow
        For Each Dr In dsManu.Tables(0).Rows
            If frmSalesOrder.tsbSaveSO.Tag = Dr("iManu") Then
                frmSalesOrder.tsbSaveSO.Visible = True
            ElseIf frmSalesOrder.tsbOpen.Tag = Dr("iManu") Then
                frmSalesOrder.tsbOpen.Visible = True
            ElseIf frmSalesOrder.tsbUpdateSO.Tag = Dr("iManu") Then
                frmSalesOrder.tsbUpdateSO.Visible = True
            ElseIf 22214 = Dr("iManu") Then
                frmSalesOrder.UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = False
                frmSalesOrder.UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = False
                frmSalesOrder.UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default
            End If
        Next
        frmSalesOrder.ShowDialog()
    End Sub

    Private Sub tsbPurchaseOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbPurchaseOrder.Click
        frmSalesOrder.iDocType = DocType.PurchaseOrder
        frmSalesOrder.Discard()
        frmSalesOrder.DocumentSettings(DocType.PurchaseOrder)
        frmSalesOrder.Text = "Purchase Order"
        frmSalesOrder.IsNew = True
        Dim Dr As DataRow
        For Each Dr In dsManu.Tables(0).Rows
            If frmSalesOrder.tsbSavePO.Tag = Dr("iManu") Then
                frmSalesOrder.tsbSavePO.Visible = True
            ElseIf frmSalesOrder.tsbOpen.Tag = Dr("iManu") Then
                frmSalesOrder.tsbOpen.Visible = True
            ElseIf frmSalesOrder.tsbUpdatePO.Tag = Dr("iManu") Then
                frmSalesOrder.tsbUpdatePO.Visible = True
            ElseIf frmSalesOrder.tsbProcessPO.Tag = Dr("iManu") Then
                frmSalesOrder.tsbProcessPO.Visible = True
            ElseIf 22225 = Dr("iManu") Then
                frmSalesOrder.UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = False
                frmSalesOrder.UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = False
                frmSalesOrder.UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default
            End If
        Next
        frmSalesOrder.ShowDialog()
    End Sub

    Private Sub tsbUserAccess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbUserAccess.Click

        frmAccessRights.ShowDialog()

    End Sub

    'Public Sub Get_Access_Permission(ByVal iAgent As Integer)
    '    Dim objSQL As New clsSqlConn
    '    With objSQL

    '        Try
    '            SQL = "SELECT iManu FROM Spil_Access_Rights WHERE idAgents =" & iAgent & ""
    '            dsManu = New DataSet
    '            dsManu = .Get_Data_Sql(SQL)
    '            If sAgent <> "Admin" Then
    '                Dim Dr As DataRow
    '                For Each Dr In dsManu.Tables(0).Rows
    '                    If tsbPurchaseOrder.Tag = Dr("iManu") Then
    '                        tsbPurchaseOrder.Enabled = True
    '                    ElseIf tsbSalesOrder.Tag = Dr("iManu") Then
    '                        tsbSalesOrder.Enabled = True
    '                    End If
    '                Next
    '            Else
    '                tsbPurchaseOrder.Enabled = True
    '                tsbSalesOrder.Enabled = True
    '                tsbUserAccess.Enabled = True
    '            End If

    '        Catch ex As Exception

    '        End Try
    '    End With
    'End Sub

    Private Sub tsbBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBarcode.Click
        frmBarCodePrinting.ShowDialog()
    End Sub
    Private Sub tsbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbExit.Click
        Application.Exit()
    End Sub
    Public Sub Get_Access_Permission(ByVal iAgent As Integer)
        Dim objSQL As New clsSqlConn
        With objSQL
            
            Try
                If sAgent = "Admin" Then
                    frmLV_SO.LV.Items(9).Visible = True
                End If
                SQL = "SELECT iManu FROM Spil_Access_Rights WHERE idAgents =" & iAgent & ""
                dsManu = New DataSet
                dsManu = .Get_Data_Sql(SQL)
                Dim R As DataRow
                For Each R In dsManu.Tables(0).Rows
                    Dim mRootNode2 As UltraTreeNode = ut.GetNodeByKey("002")
                    If R("iManu") = mRootNode2.Tag Then
                        mRootNode2.Visible = True
                    End If
                    If mRootNode2.HasNodes = True Then
                        Dim iNode1 As UltraTreeNode
                        For Each iNode1 In mRootNode2.Nodes
                            If R("iManu") = iNode1.Tag Then
                                iNode1.Visible = True
                            End If
                            If iNode1.HasNodes = True Then
                                Dim iNode2 As UltraTreeNode
                                For Each iNode2 In iNode1.Nodes
                                    If R("iManu") = iNode2.Tag Then
                                        iNode2.Visible = True
                                    End If
                                    If iNode2.HasNodes = True Then
                                        Dim iNode3 As UltraTreeNode
                                        For Each iNode3 In iNode2.Nodes
                                            If R("iManu") = iNode3.Tag Then
                                                iNode3.Visible = True
                                            End If
                                            If iNode3.HasNodes = True Then
                                                Dim iNode4 As UltraTreeNode
                                                For Each iNode4 In iNode3.Nodes
                                                    If R("iManu") = iNode4.Tag Then
                                                        iNode4.Visible = True
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
            Catch ex As Exception
                MsgBox(ex)
            End Try
        End With
    End Sub
    Private Sub frmMDI_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Application.Exit()
    End Sub
    Private Sub frmMDI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If iAgent = 1 Then
            ut.GetNodeByKey("EvoAgents").Visible = True
        Else
            ut.GetNodeByKey("EvoAgents").Visible = False
        End If
        tslDB.Text = sSQLSrvDataBase
        tslServer.Text = sSQLSrvName
        tslAgent.Text = sAgent

        frmLV_SO.LV.Items(12).Visible = False
        
    End Sub


    Private Sub ut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ut.Click
        If ut.SelectedNodes.Count > 0 Then


            'frmLV_SO.LV.Items(12).Visible = False

            If ut.GetNodeByKey("EvoAgents").Selected = True Then
                ut.GetNodeByKey("EvoAgents").Selected = False
                frmAccessRights.MdiParent = Me
                frmAccessRights.Show()
                frmAccessRights.BringToFront()
            ElseIf ut.GetNodeByKey("EvoOrderEntry").Selected = True Then

               

                ut.GetNodeByKey("EvoOrderEntry").Selected = False
                frmLV_SO.Close()
                frmLV_SO.MdiParent = Me
                frmLV_SO.Set_ListView_Setting(-1)
                For Each Dr In dsManu.Tables(0).Rows
                    If Dr("iManu") = 2221 Then  'Sales Order
                        frmLV_SO.LV.Items(0).Visible = True
                    ElseIf Dr("iManu") = 2222 Then 'Purchase Order
                        frmLV_SO.LV.Items(1).Visible = True
                    ElseIf Dr("iManu") = 2223 Then  'Location Tranfer
                        frmLV_SO.LV.Items(2).Visible = True
                    ElseIf Dr("iManu") = 2211 Then  'Bar Code
                        frmLV_SO.LV.Items(3).Visible = True
                    ElseIf Dr("iManu") = 2212 Then
                        frmLV_SO.LV.Items(6).Visible = True
                    ElseIf Dr("iManu") = 2213 Then
                        frmLV_SO.LV.Items(8).Visible = True
                    ElseIf Dr("iManu") = 2231 Then
                        frmLV_SO.LV.Items(5).Visible = True
                    ElseIf Dr("iManu") = 2232 Then
                        frmLV_SO.LV.Items(4).Visible = True
                    ElseIf Dr("iManu") = 2233 Then
                        frmLV_SO.LV.Items(7).Visible = True
                    ElseIf Dr("iManu") = 2215 Then
                        frmLV_SO.LV.Items(11).Visible = True
                    ElseIf Dr("iManu") = 2224 Then
                        frmLV_SO.LV.Items(21).Visible = True
                    ElseIf Dr("iManu") = 2214 Then
                        frmLV_SO.LV.Items(12).Visible = True
                    ElseIf Dr("iManu") = 2216 Then
                        frmLV_SO.LV.Items(15).Visible = True
                    ElseIf Dr("iManu") = 2225 Then
                        frmLV_SO.LV.Items(13).Visible = True
                    ElseIf Dr("iManu") = 2226 Then
                        frmLV_SO.LV.Items(19).Visible = True
                    ElseIf Dr("iManu") = 2227 Then
                        frmLV_SO.LV.Items(20).Visible = True
                    End If
                Next
                frmLV_SO.Show()
                frmLV_SO.Text = "Order Entry"
                frmLV_SO.BringToFront()


                If sAgent = "Admin" Then
                    frmLV_SO.LV.Items(8).Visible = True
                    frmLV_SO.LV.Items(11).Visible = True
                    frmLV_SO.LV.Items(12).Visible = True
                    frmLV_SO.LV.Items(15).Visible = True
                    frmLV_SO.LV.Items(17).Visible = True
                    frmLV_SO.LV.Items(13).Visible = True
                    frmLV_SO.LV.Items(5).Visible = True
                    frmLV_SO.LV.Items(23).Visible = True
                End If


            ElseIf ut.GetNodeByKey("221").Selected = True Then
                ut.GetNodeByKey("221").Selected = False
                frmLV_SO.Close()
                frmLV_SO.MdiParent = Me
                frmLV_SO.Set_ListView_Setting(81)
                For Each Dr In dsManu.Tables(0).Rows
                    If Dr("iManu") = 2211 Then
                        frmLV_SO.LV.Items(3).Visible = True
                    ElseIf Dr("iManu") = 2212 Then
                        frmLV_SO.LV.Items(6).Visible = True
                    End If
                Next
                frmLV_SO.Show()
                frmLV_SO.Text = "Maintenance"
                frmLV_SO.BringToFront()
            ElseIf ut.GetNodeByKey("222").Selected = True Then
                ut.GetNodeByKey("222").Selected = False
                frmLV_SO.Close()
                frmLV_SO.MdiParent = Me
                frmLV_SO.Set_ListView_Setting(82)
                For Each Dr In dsManu.Tables(0).Rows
                    If Dr("iManu") = 2221 Then  'Sales Order
                        frmLV_SO.LV.Items(0).Visible = True
                    ElseIf Dr("iManu") = 2222 Then 'Purchase Order
                        frmLV_SO.LV.Items(1).Visible = True
                    ElseIf Dr("iManu") = 2223 Then  'Location Tranfer
                        frmLV_SO.LV.Items(2).Visible = True
                    ElseIf Dr("iManu") = 2224 Then  'Location Tranfer
                        frmLV_SO.LV.Items(3).Visible = True
                        frmLV_SO.LV.Items(10).Visible = True
                    End If
                Next
                frmLV_SO.Show()
                frmLV_SO.Text = "Transactions"
                frmLV_SO.BringToFront()
            ElseIf ut.GetNodeByKey("223").Selected = True Then
                ut.GetNodeByKey("223").Selected = False
                frmLV_SO.Close()
                frmLV_SO.MdiParent = Me
                frmLV_SO.Set_ListView_Setting(83)
                For Each Dr In dsManu.Tables(0).Rows
                    If Dr("iManu") = 2231 Then  'Stock Enquiries
                        frmLV_SO.LV.Items(5).Visible = True
                    ElseIf Dr("iManu") = 2232 Then
                        frmLV_SO.LV.Items(4).Visible = True
                    ElseIf Dr("iManu") = 2233 Then
                        frmLV_SO.LV.Items(7).Visible = True
                    
                    End If
                Next
                frmLV_SO.Show()
                frmLV_SO.Text = "Enquiries"
                frmLV_SO.BringToFront()
            ElseIf ut.GetNodeByKey("224").Selected = True Then
                ut.GetNodeByKey("224").Selected = False
                frmLV_SO.Close()
                frmLV_SO.MdiParent = Me
                frmLV_SO.Set_ListView_Setting(84)
                frmLV_SO.Show()
                frmLV_SO.Text = "Reports"
                frmLV_SO.BringToFront()
            End If
        End If

    End Sub

    Private Sub ut_AfterSelect(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTree.SelectEventArgs) Handles ut.AfterSelect

    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        'Me.Close()
        Application.Exit()
        Process.Start(Application.StartupPath & "\Updater.exe")

        'Dim ProcessID As Long

        'ProcessID = Shell(Application.StartupPath & "\update.bat", 1)
    End Sub

    Private Sub AudirReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AudirReportToolStripMenuItem.Click
        frmAuditRpt.Show()
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        '----INV64 error correction---------------------
        Dim objSQL As New clsSqlConn
        With objSQL
            SQL = "SELECT ROW_NUMBER() OVER (ORDER BY SerialNumber ASC) AS rownumber, SerialNumber, count(*) from SerialMF group by SerialNumber HAVING count(*) > 1"
            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
            If DS.Tables(0).Rows.Count > 0 Then
                Dim Dr As DataRow
                .Begin_Trans()
                For Each Dr In DS.Tables(0).Rows
                    'SQL = "DELETE FROM SerialMF WHERE SerialNumber = '" & Dr.Item("SerialNumber") & "' AND CurrentLoc <> 2 "
                    SQL = "UPDATE SerialMF SET SerialNumber = SerialNumber + '-999'  WHERE SerialNumber = '" & Dr.Item("SerialNumber") & "' AND CurrentLoc = 3 "
                    If .Execute_Sql_Trans(SQL) = 0 Then
                        .Rollback_Trans()
                        Exit Sub
                    End If
                Next
                .Commit_Trans()
            End If
        End With


    End Sub

    Private Sub SalesAnalisysReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesAnalisysReportToolStripMenuItem.Click

    End Sub

    Private Sub AaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim aaa = New frmPriceCostUpddate()
        aaa.Show()
    End Sub

    Private Sub BulkSMSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BulkSMSToolStripMenuItem.Click
        Dim bulksms = New frmBulkSMS()
        bulksms.Show()
    End Sub
End Class