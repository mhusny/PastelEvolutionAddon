
Imports Infragistics.Win.UltraWinGrid
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmGroupMargine

    Private Sub DeleteRow()
        Dim ugR As UltraGridRow
        For Each ugR In UG.Rows.All
            ugR.Delete(False)
        Next
    End Sub


    Private Sub frmGroupMargine_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        DeleteRow()

        SQL = "SELECT idGrpTbl, StGroup, fCostMargine FROM GrpTbl ORDER BY idGrpTbl"

        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                UG.DataSource = DS.Tables(0)
                Dim band As UltraGridBand = UG.DisplayLayout.Bands(0)
                If Not band.Columns.Exists("CostMargine") Then
                    band.Columns.Add("CostMargine")
                    band.Columns("CostMargine").Header.Caption = "Cost Margine"
                    band.Columns("CostMargine").DataType = GetType(Decimal)
                    band.Columns("CostMargine").CellClickAction = CellClickAction.Edit
                    band.Columns("CostMargine").CellActivation = Activation.AllowEdit

                End If

                UG.DisplayLayout.Bands(0).Columns("fCostMargine").Hidden = True

                Dim ugr As UltraGridRow

                For Each ugr In UG.Rows
                    ugr.Cells("CostMargine").Value = ugr.Cells("fCostMargine").Value
                Next


            Catch ex As Exception
                MsgBox(Err.Description, MsgBoxStyle.Critical, "Pastel evolution")
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try
        End With

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Me.Close()

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        UG.PerformAction(UltraGridAction.ExitEditMode)


        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                 
               


                Dim ugr As UltraGridRow

                For Each ugr In UG.Rows
                    ugr.Cells("fCostMargine").Value = ugr.Cells("CostMargine").Value
                Next




                For Each ugr In UG.Rows

                    'ugr.Cells("CostMargine").Value = ugr.Cells("fCostMargine").Value
                    SQL = "update GrpTbl set fCostMargine = " & CDbl(ugr.Cells("fCostMargine").Value) & " where   idGrpTbl = " & ugr.Cells("idGrpTbl").Value
                    If .Execute_Sql(SQL) = 0 Then
                        MsgBox("Error Updating")
                        Exit Sub
                    End If

                Next

                MsgBox("Inventory Group Cost Margine Update success ", MsgBoxStyle.Information, "Pastel Evolution")
                Me.Close()

            Catch ex As Exception
                MsgBox(Err.Description, MsgBoxStyle.Critical, "Pastel evolution")
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try
        End With

    End Sub

    Private Sub UltraButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraButton1.Click
        ' ''Dim dROL, dROQ As Double

        ' ''Dim objSQL As New clsSqlConn

        ' ''With objSQL

        ' ''    Dim DS = New DataSet
        ' ''    Dim DS1 As DataSet
        ' ''    SQL = " SELECT   StkItem.StockLink FROM  StkItem   "

        ' ''    DS = .Get_Data_Sql(SQL)

        ' ''    'Dim Dr As DataRow

        ' ''    If DS.Tables(0).Rows.Count > 0 Then

        ' ''        For Each Dr In DS.Tables(0).Rows

        ' ''            If Not IsDBNull(Dr.Item(0)) Then

        ' ''                SQL = "     SELECT     SUM(Quantity) AS sumqty FROM PostST  WHERE " & _
        ' ''                " (AccountLink =" & Dr.Item(0) & ") AND (Id = 'OInv' OR Id = 'Inv' OR Id = 'IJr' OR Id = 'WIBTI') AND (TxDate >= '12/10/2010') AND (Credit <> 0) "
        ' ''                DS1 = New DataSet
        ' ''                DS1 = .Get_Data_Sql(SQL)

        ' ''                If DS1.Tables(0).Rows.Count > 0 Then
        ' ''                    If Not IsDBNull(DS1.Tables(0).Rows(0)(0)) Then
        ' ''                        If DS1.Tables(0).Rows(0)(0) <> 0 Then

        ' ''                            dROL = 0
        ' ''                            dROQ = 0

        ' ''                            dROL = (30 * DS1.Tables(0).Rows(0)(0)) / 120
        ' ''                            dROQ = (45 * DS1.Tables(0).Rows(0)(0)) / 120

        ' ''                            dROL = Math.Truncate(dROL) + 1
        ' ''                            dROQ = Math.Truncate(dROQ) + 1

        ' ''                            SQL = " Update StkItem SET Re_Ord_Lvl = " & dROL & " , Re_Ord_Qty = " & dROQ & "  where StockLink =  " & Dr.Item(0)
        ' ''                            .Begin_Trans()
        ' ''                            If .Execute_Sql_Trans(SQL) = 0 Then
        ' ''                                .Rollback_Trans()
        ' ''                                MsgBox("Error in item - " & Dr.Item(0))
        ' ''                                'Exit Sub
        ' ''                            End If
        ' ''                            .Commit_Trans()
        ' ''                        End If

        ' ''                    End If

        ' ''                End If
        ' ''            End If
        ' ''        Next
        ' ''        MsgBox("Complete")
        ' ''    End If

        ' ''End With

    End Sub

End Class