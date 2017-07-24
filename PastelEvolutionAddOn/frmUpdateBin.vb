Imports Pastel.Evolution
Public Class frmUpdateBin

    Private Sub frmUpdateBin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub tsbBin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBin.Click
        UpdateBin()
    End Sub
    Private Sub UpdateBin()


        Dim InventoryItem As InventoryItem
        InventoryItem = New InventoryItem(CStr(txtSimpleCode.Text))

        Dim objSQL As New clsSqlConn

        With objSQL

            SQL = " SELECT cBinLocationName  FROM _btblBINLocation where cBinLocationName = '" & txtBin.Text & "'  "

            DS = New DataSet
            DS = .Get_Data_Sql(SQL)

            If DS.Tables(0).Rows.Count > 0 Then
                Dim InventoryBin As InventoryBin
                InventoryBin = New InventoryBin(txtBin.Text.ToString())

                InventoryItem.BinCode = InventoryBin

                InventoryItem.Save()
            Else
                Dim InventoryBin As New InventoryBin
                InventoryBin.Name = txtBin.Text.ToString()
                InventoryBin.Save()

                InventoryItem.BinCode = InventoryBin

                InventoryItem.Save()
            End If

        End With



        Me.Close()

    End Sub

    Private Sub txtBin_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBin.KeyDown
        If e.KeyCode = Keys.Enter Then
            UpdateBin()
        End If
    End Sub

    Private Sub UltraLabel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraLabel2.Click

    End Sub

    Private Sub ultraLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ultraLabel1.Click

    End Sub

    Private Sub txtSimpleCode_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSimpleCode.ValueChanged

    End Sub

    Private Sub txtBin_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBin.ValueChanged

    End Sub

    Private Sub toolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolStrip1.ItemClicked

    End Sub
End Class