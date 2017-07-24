Public Class frmSuppCostList

    Private Sub tsbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
    End Sub

    Private Sub Fill_UG()

        Dim objSQL As New clsSqlConn

        With objSQL

            SQL = " SELECT     StkItem.Description_1, Vendor.Account ,  Spill_Supplier_Prices.Cost" & _
            " FROM         Spill_Supplier_Prices INNER JOIN " & _
            "       StkItem ON Spill_Supplier_Prices.iStockID = StkItem.StockLink INNER JOIN " & _
            "      Vendor ON Spill_Supplier_Prices.iDCLink = Vendor.DCLink "
            
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
            UG.DataSource = DS.Tables(0)
            'UG.DisplayLayout.Bands(0).Columns("StockLink").Hidden = True



        End With

    End Sub
    Private Sub Get_Item()

        Dim objSQL As New clsSqlConn

        With objSQL

            SQL = " SELECT StockLink, Description_1, Code FROM StkItem WHERE ItemActive = 1"

            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
            cmbInventoryItem.DataSource = DS.Tables(0)
            cmbInventoryItem.ValueMember = "StockLink"
            cmbInventoryItem.DisplayMember = "Description_1"
            cmbInventoryItem.DisplayLayout.Bands(0).Columns("Code").Width = 500
            cmbInventoryItem.DisplayLayout.Bands(0).Columns("StockLink").Hidden = True



        End With

    End Sub
    Private Sub Get_Supplier()

        Dim objSQL As New clsSqlConn

        With objSQL

            SQL = " SELECT DCLink, Account, Name FROM Vendor "

            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
            cmbSupplier.DataSource = DS.Tables(0)
            cmbSupplier.ValueMember = "DCLink"
            cmbSupplier.DisplayMember = "Account"
            'cmbSupplier.DisplayLayout.Bands(0).Columns("DCLink").Hidden = True
            cmbSupplier.DisplayLayout.Bands(0).Columns("DCLink").Hidden = True



        End With

    End Sub

    Private Sub frmSuppCostList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Fill_UG()
        Get_Item()
        Get_Supplier()

    End Sub

    Private Sub tsbConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbConnect.Click
        Fill_UG()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim objSQL As New clsSqlConn

        With objSQL

            SQL = " SELECT * FROM Spill_Supplier_Prices WHERE iStockID = " & cmbInventoryItem.Value & " AND iDCLink = " & cmbSupplier.Value & " "

            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
            If DS.Tables(0).Rows.Count > 0 Then



                .Begin_Trans()

                SQL = " UPDATE Spill_Supplier_Prices  SET Cost = " & txtBarCode.Text & " where  iStockID = " & cmbInventoryItem.Value & "  AND  iDCLink = " & cmbSupplier.Value & ""
                If .Execute_Sql_Trans(SQL) = 0 Then
                    .Rollback_Trans()
                    Exit Sub
                End If

                .Commit_Trans()
                MsgBox("Successfully Updated", MsgBoxStyle.Information, "Pasterl Evolution")


                'MessageBox.Show("Supplier already added for that item", "Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Information)

          
            Else

                'UG.PerformAction(UltraGridAction.CommitRow)
                objSQL = New clsSqlConn


                .Begin_Trans()

                SQL = " INSERT INTO Spill_Supplier_Prices(iStockID, iDCLink, Cost) VALUES (" & cmbInventoryItem.Value & ", " & cmbSupplier.Value & " , " & txtBarCode.Text & " )"
                If .Execute_Sql_Trans(SQL) = 0 Then
                    .Rollback_Trans()
                    Exit Sub
                End If

                .Commit_Trans()
                MsgBox("Successfully Updated", MsgBoxStyle.Information, "Pasterl Evolution")
            End If

        End With

        Fill_UG()
    End Sub
End Class