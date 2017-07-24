Imports Infragistics.Win.UltraWinListView
Imports Infragistics.Win
Public Class frmLV_SO
    Public Sub Set_ListView_Setting(ByVal No As Integer)
        Dim item As UltraListViewItem
        Select Case No
            Case -1
                For Each item In LV.Items
                    Select Case item.SubItems(0).Text
                        Case "Maintenance"
                            item.Group = LV.Groups(0)
                        Case "Transactions"
                            item.Group = LV.Groups(1)
                        Case "Enquiries"
                            item.Group = LV.Groups(2)
                        Case "Reports"
                            item.Group = LV.Groups(3)
                        Case "Charts"
                            item.Group = LV.Groups(4)
                    End Select
                Next
            Case 81
                For Each item In LV.Items
                    Select Case item.SubItems(0).Text
                        Case "Maintenance"
                            item.Group = LV.Groups(0)
                    End Select
                Next
            Case 82
                For Each item In LV.Items
                    Select Case item.SubItems(0).Text
                        Case "Transactions"
                            item.Group = LV.Groups(1)
                    End Select
                Next
            Case 83
                For Each item In LV.Items
                    Select Case item.SubItems(0).Text
                        Case "Enquiries"
                            item.Group = LV.Groups(2)
                    End Select
                Next
            Case 84
                For Each item In LV.Items
                    Select Case item.SubItems(0).Text
                        Case "Reports"
                            item.Group = LV.Groups(3)
                    End Select
                Next
            Case 85

                For Each item In LV.Items
                    Select Case item.SubItems(0).Text
                        Case "Charts"
                            item.Group = LV.Groups(4)
                    End Select
                Next
        End Select
        LV.ShowGroups = True
    End Sub

    Private Sub LV_ItemSelectionChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinListView.ItemSelectionChangedEventArgs) Handles LV.ItemSelectionChanged
        If LV.SelectedItems.Count > 0 Then
            If LV.SelectedItems(0).Key = "1" Then 'Q
                Dim objSalesOrder As New frmSalesOrder
                With objSalesOrder
                    .iDocType = DocType.SalesOrder
                    .Discard()
                    .DocumentSettings(DocType.SalesOrder)
                    .Text = "Sales Order"
                    .IsNew = True
                    Dim Dr As DataRow
                    For Each Dr In dsManu.Tables(0).Rows
                        If .tsbSaveSO.Tag = Dr("iManu") Then
                            .tsbSaveSO.Visible = True
                        ElseIf .tsbOpen.Tag = Dr("iManu") Then
                            .tsbOpen.Visible = True
                        ElseIf .tsbUpdateSO.Tag = Dr("iManu") Then
                            .tsbUpdateSO.Visible = True
                        ElseIf 22213 = Dr("iManu") Then
                            .UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
                            .UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
                            .UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default
                        End If
                    Next
                    .lblBarcode.Visible = True
                    .txtBarCode.Visible = True
                    .Show()
                End With
                'frmSalesOrder.iDocType = DocType.SalesOrder
                'frmSalesOrder.Discard()
                'frmSalesOrder.DocumentSettings(DocType.SalesOrder)
                'frmSalesOrder.Text = "Sales Order"
                'frmSalesOrder.IsNew = True
                'Dim Dr As DataRow
                'For Each Dr In dsManu.Tables(0).Rows
                '    If frmSalesOrder.tsbSaveSO.Tag = Dr("iManu") Then
                '        frmSalesOrder.tsbSaveSO.Visible = True
                '    ElseIf frmSalesOrder.tsbOpen.Tag = Dr("iManu") Then
                '        frmSalesOrder.tsbOpen.Visible = True
                '    ElseIf frmSalesOrder.tsbUpdateSO.Tag = Dr("iManu") Then
                '        frmSalesOrder.tsbUpdateSO.Visible = True
                '    ElseIf 22213 = Dr("iManu") Then
                '        frmSalesOrder.UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
                '        frmSalesOrder.UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
                '        frmSalesOrder.UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default
                '    End If
                'Next
                'frmSalesOrder.lblBarcode.Visible = True
                'frmSalesOrder.txtBarCode.Visible = True
                'frmSalesOrder.ShowDialog()
            ElseIf LV.SelectedItems(0).Key = "2" Then
                Dim objPurchaseOrder As New frmSalesOrder
                With objPurchaseOrder
                    .iDocType = DocType.PurchaseOrder
                    .Discard()
                    .DocumentSettings(DocType.PurchaseOrder)
                    .Text = "Purchase Order"
                    .IsNew = True
                    .iPONo = 0
                    Dim Dr As DataRow
                    For Each Dr In dsManu.Tables(0).Rows
                        If .tsbSavePO.Tag = Dr("iManu") Then
                            .tsbSavePO.Visible = True
                        ElseIf .tsbOpen.Tag = Dr("iManu") Then
                            .tsbOpen.Visible = True
                        ElseIf .tsbUpdatePO.Tag = Dr("iManu") Then
                            .tsbUpdatePO.Visible = True
                        ElseIf .tsbProcessPO.Tag = Dr("iManu") Then
                            .tsbProcessPO.Visible = True
                        ElseIf 22223 = Dr("iManu") Then
                            .UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
                            .UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
                            .UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default
                        End If
                    Next
                    .lblBarcode.Visible = False
                    .txtBarCode.Visible = False
                    .Show()
                End With
                'frmSalesOrder.iDocType = DocType.PurchaseOrder
                'frmSalesOrder.Discard()
                'frmSalesOrder.DocumentSettings(DocType.PurchaseOrder)
                'frmSalesOrder.Text = "Purchase Order"
                'frmSalesOrder.IsNew = True
                'Dim Dr As DataRow
                'For Each Dr In dsManu.Tables(0).Rows
                '    If frmSalesOrder.tsbSavePO.Tag = Dr("iManu") Then
                '        frmSalesOrder.tsbSavePO.Visible = True
                '    ElseIf frmSalesOrder.tsbOpen.Tag = Dr("iManu") Then
                '        frmSalesOrder.tsbOpen.Visible = True
                '    ElseIf frmSalesOrder.tsbUpdatePO.Tag = Dr("iManu") Then
                '        frmSalesOrder.tsbUpdatePO.Visible = True
                '    ElseIf frmSalesOrder.tsbProcessPO.Tag = Dr("iManu") Then
                '        frmSalesOrder.tsbProcessPO.Visible = True
                '    ElseIf 22223 = Dr("iManu") Then
                '        frmSalesOrder.UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
                '        frmSalesOrder.UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
                '        frmSalesOrder.UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default
                '    End If
                'Next
                'frmSalesOrder.lblBarcode.Visible = False
                'frmSalesOrder.txtBarCode.Visible = False
                'frmSalesOrder.ShowDialog()
            ElseIf LV.SelectedItems(0).Key = "3" Then
                Dim Dr As DataRow
                For Each Dr In dsManu.Tables(0).Rows
                    If frmInvoice.tsbSave.Tag = Dr("iManu") Then
                        frmInvoice.tsbSave.Visible = True
                    ElseIf frmInvoice.tsbOpen.Tag = Dr("iManu") Then
                        frmInvoice.tsbOpen.Visible = True
                    ElseIf 22233 = Dr("iManu") Then
                        frmInvoice.UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
                        frmInvoice.UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
                        frmInvoice.UG.DisplayLayout.Bands(0).Columns("fCostMargine").Hidden = True
                        frmInvoice.UG.DisplayLayout.Bands(0).Columns("fUnitCostMargine").Hidden = True
                        frmInvoice.UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default
                   
                    End If
                Next
                frmInvoice.Show()
            ElseIf LV.SelectedItems(0).Key = "4" Then
                frmBarCodePrinting.ShowDialog()
            ElseIf LV.SelectedItems(0).Key = "5" Then
                frmStockMov.ShowDialog()
            ElseIf LV.SelectedItems(0).Key = "6" Then
                frmStockEnquiries.MdiParent = frmMDI
                frmStockEnquiries.Refresh()
                frmStockEnquiries.Show()
                frmStockEnquiries.BringToFront()
            ElseIf LV.SelectedItems(0).Key = "7" Then
                frmUpdatePriceList.MdiParent = frmMDI
                frmUpdatePriceList.Refresh()
                frmUpdatePriceList.Show()
                frmUpdatePriceList.BringToFront()
            ElseIf LV.SelectedItems(0).Key = "8" Then
                frmSerialNumberEnquiries.MdiParent = frmMDI
                frmSerialNumberEnquiries.Refresh()
                frmSerialNumberEnquiries.Show()
                frmSerialNumberEnquiries.BringToFront()
            ElseIf LV.SelectedItems(0).Key = "9" Then
                frmGroupMargine.ShowDialog()
            ElseIf LV.SelectedItems(0).Key = "11" Then
                frmSearchItem.MdiParent = frmMDI
                frmSearchItem.Refresh()
                frmSearchItem.Show()
                frmSearchItem.BringToFront()
            ElseIf LV.SelectedItems(0).Key = "12" Then
                frmReOrder.MdiParent = frmMDI
                frmReOrder.Refresh()
                frmReOrder.Show()
                frmReOrder.BringToFront()
            ElseIf LV.SelectedItems(0).Key = "13" Then
                'frmROL_ROQ.MdiParent = frmMDI
                frmROL_ROQ.Refresh()
                frmROL_ROQ.Show()
                frmReOrder.BringToFront()
            ElseIf LV.SelectedItems(0).Key = "14" Then
                'frmROL_ROQ.MdiParent = frmMDI
                frmStockCount.Refresh()
                frmStockCount.Show()
                frmStockCount.BringToFront()
            ElseIf LV.SelectedItems(0).Key = "15" Then
                'frmROL_ROQ.MdiParent = frmMDI
                frmStockCount.Refresh()
                frmStockCount.Show()
                frmStockCount.BringToFront()
            ElseIf LV.SelectedItems(0).Key = "16" Then
                frmSuppCostList.Refresh()
                frmSuppCostList.Show()
                frmSuppCostList.BringToFront()
            ElseIf LV.SelectedItems(0).Key = "17" Then
                frmItemCatagory.Refresh()
                frmItemCatagory.Show()
                frmItemCatagory.BringToFront()
            ElseIf LV.SelectedItems(0).Key = "18" Then
                frmExpDate.Refresh()
                frmExpDate.Show()
                frmExpDate.BringToFront()
            ElseIf LV.SelectedItems(0).Key = "19" Then
                frmSalesVsPurchaces.Refresh()
                frmSalesVsPurchaces.Show()
                frmSalesVsPurchaces.BringToFront()
            ElseIf LV.SelectedItems(0).Key = "20" Then
                frmReturnSuplier.Refresh()
                frmReturnSuplier.Show()
                frmReturnSuplier.BringToFront()
            ElseIf LV.SelectedItems(0).Key = "21" Then
                frmCreditNote.Refresh()
                frmCreditNote.Show()
                frmCreditNote.BringToFront()
            ElseIf LV.SelectedItems(0).Key = "22" Then
                Dim objIBTReceive As New frmIBTReceive
                With objIBTReceive
                    .iDocType = DocType.SalesOrder
                    .Discard()
                    .DocumentSettings(DocType.SalesOrder)
                    .Text = "IBT Receive"
                    .IsNew = True
                    .iPONo = 0
                    Dim Dr As DataRow
                    For Each Dr In dsManu.Tables(0).Rows
                        If .tsbSavePO.Tag = Dr("iManu") Then
                            .tsbSavePO.Visible = True
                            .tsbSavePO.Text = "Save"
                        ElseIf .tsbOpen.Tag = Dr("iManu") Then
                            .tsbOpen.Visible = True
                        ElseIf .tsbUpdatePO.Tag = Dr("iManu") Then
                            .tsbUpdatePO.Visible = True
                            '.tsbUpdatePO.Text = Process"
                        ElseIf .tsbProcessPO.Tag = Dr("iManu") Then
                            .tsbProcessPO.Visible = True
                            .tsbProcessPO.Text = "Process"
                        ElseIf 22246 = Dr("iManu") Then
                            '.UG.DisplayLayout.Bands(0).Columns("SerialLot").Hidden = True
                        End If
                    Next
                    .lblBarcode.Visible = True
                    .txtBarCode.Visible = True
                    .Show()
                End With
            ElseIf LV.SelectedItems(0).Key = "23" Then
                Dim reciept = New frmReciept()
                reciept.Show()
            ElseIf LV.SelectedItems(0).Key = "24" Then
                Dim impCost = New frmPriceCostUpddate()
                impCost.Show()
            End If

            LV.SelectedItems.Clear()

        End If

    End Sub

    Private Sub DetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DetailsToolStripMenuItem.Click
        LV.View = UltraListViewStyle.Details
    End Sub

    Private Sub IconToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IconToolStripMenuItem.Click
        LV.View = UltraListViewStyle.Icons
    End Sub

    Private Sub ListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListToolStripMenuItem.Click
        LV.View = UltraListViewStyle.List
    End Sub

    Private Sub TitleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TitleToolStripMenuItem.Click
        LV.View = UltraListViewStyle.Tiles
    End Sub

    Private Sub ThumToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ThumToolStripMenuItem.Click
        LV.View = UltraListViewStyle.Thumbnails
    End Sub

    Private Sub LV_ItemActivated(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinListView.ItemActivatedEventArgs) Handles LV.ItemActivated

    End Sub

    Private Sub frmLV_SO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class