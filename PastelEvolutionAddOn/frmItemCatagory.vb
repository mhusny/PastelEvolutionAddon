Imports System.Data
Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid


Public Class frmItemCatagory

    Private Sub frmItemCatagory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
    End Sub

    Private Sub dtFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtFrom.KeyDown
        If e.KeyCode = Keys.Enter Then
            fillData()
            Permisions()
        End If
    End Sub

    Private Sub dtFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtFrom.ValueChanged

    End Sub
    Private Sub Permisions()
        If sAgent <> "Admin" Then
            toolStrip1.Enabled = False
            tsbExit.Enabled = True

            UG.DisplayLayout.Bands(0).Columns("30 Days").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("60 Days").Hidden = True
            'UG.DisplayLayout.Bands(0).Columns("90 Days").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("30 Days %").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("60 Days %").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("90 Days %").Hidden = True
            'UG.DisplayLayout.Bands(0).Columns("1_RED").Hidden = True
            'UG.DisplayLayout.Bands(0).Columns("2_GREEN").Hidden = True
            'UG.DisplayLayout.Bands(0).Columns("3_YELLOW").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("Code").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("TxDate").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("SUM").Hidden = True
            'UG.DisplayLayout.Bands(0).Columns("30 Days").Hidden = True
            'UG.DisplayLayout.Bands(0).Columns("30 Days").Hidden = True
            'UG.DisplayLayout.Bands(0).Columns("30 Days").Hidden = True

        End If
    End Sub
    Private Sub fillData()

        Dim DS As New DataSet
        'Sales starting from    begening of the month
        'SQL = "  set dateformat dmy SELECT     StkItem.Description_1, " & _
        '"	SUM(CASE WHEN PostST.Debit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity WHEN PostST.Credit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity * - 1 ELSE 0 END) AS [SUM], " & _
        '"	SUM(CASE WHEN PostST.TxDate > '" & dtFrom.Value & "' AND PostST.TxDate < DATEADD(MONTH, 1, '" & dtFrom.Value & "') AND PostST.Credit > 0 THEN PostST.Quantity ELSE 0 END) AS [1 Month],  " & _
        '"   SUM(CASE WHEN PostST.TxDate > '" & dtFrom.Value & "' AND PostST.TxDate < DATEADD(MONTH, 3, '" & dtFrom.Value & "') AND PostST.Credit > 0 THEN PostST.Quantity ELSE 0 END) AS [3 Months], " & _
        '"   SUM(CASE WHEN PostST.TxDate > '" & dtFrom.Value & "' AND PostST.TxDate < DATEADD(MONTH, 6, '" & dtFrom.Value & "') AND PostST.Credit > 0 THEN PostST.Quantity ELSE 0 END) AS [6 Months], " & _
        '"	SUM(CASE WHEN PostST.TxDate > '" & dtFrom.Value & "' AND PostST.TxDate < DATEADD(MONTH, 1, '" & dtFrom.Value & "') AND PostST.Credit > 0 THEN PostST.Quantity ELSE 0 END) / " & _
        '" CASE WHEN SUM(CASE WHEN PostST.Debit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity WHEN PostST.Credit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity * - 1 ELSE 0 END) = 0 THEN 1 ELSE SUM(CASE WHEN PostST.Debit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity WHEN PostST.Credit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity * - 1 ELSE 0 END) END * 100, " & _
        '"	SUM(CASE WHEN PostST.TxDate > '" & dtFrom.Value & "' AND PostST.TxDate < DATEADD(MONTH, 3, '" & dtFrom.Value & "') AND PostST.Credit > 0 THEN PostST.Quantity ELSE 0 END)  /  " & _
        '" CASE WHEN SUM(CASE WHEN PostST.Debit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity WHEN PostST.Credit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity * - 1 ELSE 0 END) = 0 THEN 1 ELSE SUM(CASE WHEN PostST.Debit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity WHEN PostST.Credit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity * - 1 ELSE 0 END) END * 100, " & _
        '"	SUM(CASE WHEN PostST.TxDate > '" & dtFrom.Value & "' AND PostST.TxDate < DATEADD(MONTH, 6, '" & dtFrom.Value & "') AND PostST.Credit > 0 THEN PostST.Quantity ELSE 0 END)  / " & _
        '" CASE WHEN SUM(CASE WHEN PostST.Debit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity WHEN PostST.Credit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity * - 1 ELSE 0 END) = 0 THEN 1 ELSE SUM(CASE WHEN PostST.Debit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity WHEN PostST.Credit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity * - 1 ELSE 0 END) END * 100, " & _
        '"   'RED' AS RED, 'GREEN' AS GREEN, 'YELLOW' AS YELLOW, StkItem.Code, StkItem.StockLink, _etblInvSegValue.cDescription, " & _
        '"    _etblInvSegValue_1.cDescription AS Expr1, _etblInvSegValue_2.cDescription AS Expr2, _etblInvSegValue_3.cDescription AS Expr3, " & _
        '"    _etblInvSegValue_7.cDescription AS Expr6, " & _
        '"    StkItem.Qty_On_Hand, StkItem.Re_Ord_Qty, StkItem.Re_Ord_Lvl, StkItem.ItemGroup " & _
        '"	FROM         _etblInvSegValue INNER JOIN " & _
        '"    StkItem ON _etblInvSegValue.idInvSegValue = StkItem.iInvSegValue1ID INNER JOIN " & _
        '"    _etblInvSegValue AS _etblInvSegValue_1 ON StkItem.iInvSegValue2ID = _etblInvSegValue_1.idInvSegValue INNER JOIN " & _
        '"    _etblInvSegValue AS _etblInvSegValue_2 ON StkItem.iInvSegValue3ID = _etblInvSegValue_2.idInvSegValue INNER JOIN " & _
        '"    _etblInvSegValue AS _etblInvSegValue_3 ON StkItem.iInvSegValue4ID = _etblInvSegValue_3.idInvSegValue INNER JOIN " & _
        '"    _etblInvSegValue AS _etblInvSegValue_4 ON StkItem.iInvSegValue5ID = _etblInvSegValue_4.idInvSegValue INNER JOIN " & _
        '"    _etblInvSegValue AS _etblInvSegValue_6 ON StkItem.iInvSegValue6ID = _etblInvSegValue_6.idInvSegValue INNER JOIN " & _
        '"    _etblInvSegValue AS _etblInvSegValue_7 ON StkItem.iInvSegValue7ID = _etblInvSegValue_7.idInvSegValue RIGHT OUTER JOIN " & _
        '"    PostST ON StkItem.StockLink = PostST.AccountLink " & _
        '"	GROUP BY StkItem.Description_1, StkItem.Code, StkItem.StockLink, _etblInvSegValue.cDescription, _etblInvSegValue_1.cDescription, " & _
        '"    _etblInvSegValue_2.cDescription, _etblInvSegValue_3.cDescription, _etblInvSegValue_4.cDescription, _etblInvSegValue_6.cDescription, " & _
        '"    _etblInvSegValue_7.cDescription, StkItem.Qty_On_Hand, StkItem.Re_Ord_Qty, StkItem.ItemGroup, StkItem.Re_Ord_Lvl " & _
        '"	ORDER BY StkItem.Description_1, StkItem.ItemGroup, Expr1, Expr2 "

        'sales starting from begining of the month + quantity receiver over 3 months
        SQL = "  set dateformat dmy SELECT     StkItem.Description_1, " & _
        "	SUM(CASE WHEN PostST.Debit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity WHEN PostST.Credit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity * - 1 ELSE 0 END) AS [SUM], " & _
        "	SUM(CASE WHEN PostST.TxDate > '" & dtFrom.Value & "' AND PostST.TxDate < DATEADD(MONTH, 1, '" & dtFrom.Value & "') AND PostST.Credit > 0 THEN PostST.Quantity ELSE 0 END) AS [30 Days],  " & _
        "   SUM(CASE WHEN PostST.TxDate > '" & dtFrom.Value & "' AND PostST.TxDate < DATEADD(MONTH, 3, '" & dtFrom.Value & "') AND PostST.Credit > 0 THEN PostST.Quantity ELSE 0 END) AS [60 Days], " & _
        "   SUM(CASE WHEN PostST.TxDate > '" & dtFrom.Value & "' AND PostST.TxDate < DATEADD(MONTH, 6, '" & dtFrom.Value & "') AND PostST.Credit > 0 THEN PostST.Quantity ELSE 0 END) AS [90 Days], " & _
        "	SUM(CASE WHEN PostST.TxDate > '" & dtFrom.Value & "' AND PostST.TxDate < DATEADD(MONTH, 1, '" & dtFrom.Value & "') AND PostST.Credit > 0 THEN PostST.Quantity ELSE 0 END) / " & _
        " CASE WHEN SUM(CASE WHEN PostST.Debit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity WHEN PostST.Credit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity * - 1 ELSE 0 END) = 0 THEN 1 ELSE SUM(CASE WHEN PostST.Debit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity WHEN PostST.Credit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity * - 1 ELSE 0 END) END * 100 AS [30 Days %] , " & _
        "	SUM(CASE WHEN PostST.TxDate > '" & dtFrom.Value & "' AND PostST.TxDate < DATEADD(MONTH, 3, '" & dtFrom.Value & "') AND PostST.Credit > 0 THEN PostST.Quantity ELSE 0 END)  /  " & _
        " CASE WHEN SUM(CASE WHEN PostST.Debit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity WHEN PostST.Credit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity * - 1 ELSE 0 END) = 0 THEN 1 ELSE SUM(CASE WHEN PostST.Debit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity WHEN PostST.Credit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity * - 1 ELSE 0 END) END * 100 AS [60 Days %] , " & _
        "	SUM(CASE WHEN PostST.TxDate > '" & dtFrom.Value & "' AND PostST.TxDate < DATEADD(MONTH, 6, '" & dtFrom.Value & "') AND PostST.Credit > 0 THEN PostST.Quantity ELSE 0 END)  / " & _
        " CASE WHEN SUM(CASE WHEN PostST.Debit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity WHEN PostST.Credit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity * - 1 ELSE 0 END) = 0 THEN 1 ELSE SUM(CASE WHEN PostST.Debit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity WHEN PostST.Credit > 0 AND PostST.TxDate < '" & dtFrom.Value & "' THEN PostST.Quantity * - 1 ELSE 0 END) END * 100 AS [90 Days %] , " & _
        "   'RED' AS [1_RED], 'GREEN' AS [2_GREEN], 'YELLOW' AS [3_YELLOW], StkItem.Code, StkItem.StockLink, _etblInvSegValue.cDescription AS PartNo, " & _
        "    _etblInvSegValue_1.cDescription AS Descr, _etblInvSegValue_2.cDescription AS Model, _etblInvSegValue_3.cDescription AS Brand, " & _
        "    StkItem.Qty_On_Hand, StkItem.Re_Ord_Qty, StkItem.Re_Ord_Lvl, StkItem.Max_Lvl, StkItem.Max_Lvl - StkItem.Qty_On_Hand AS [Qty to Order] ,StkItem.ItemGroup, MAX(PostST.TxDate) AS TxDate " & _
        "	FROM         _etblInvSegValue INNER JOIN " & _
        "    StkItem ON _etblInvSegValue.idInvSegValue = StkItem.iInvSegValue1ID INNER JOIN " & _
        "    _etblInvSegValue AS _etblInvSegValue_1 ON StkItem.iInvSegValue2ID = _etblInvSegValue_1.idInvSegValue INNER JOIN " & _
        "    _etblInvSegValue AS _etblInvSegValue_2 ON StkItem.iInvSegValue3ID = _etblInvSegValue_2.idInvSegValue INNER JOIN " & _
        "    _etblInvSegValue AS _etblInvSegValue_3 ON StkItem.iInvSegValue4ID = _etblInvSegValue_3.idInvSegValue INNER JOIN " & _
        "    _etblInvSegValue AS _etblInvSegValue_4 ON StkItem.iInvSegValue5ID = _etblInvSegValue_4.idInvSegValue INNER JOIN " & _
        "    _etblInvSegValue AS _etblInvSegValue_6 ON StkItem.iInvSegValue6ID = _etblInvSegValue_6.idInvSegValue INNER JOIN " & _
        "    _etblInvSegValue AS _etblInvSegValue_7 ON StkItem.iInvSegValue7ID = _etblInvSegValue_7.idInvSegValue FULL OUTER JOIN " & _
        "    PostST ON StkItem.StockLink = PostST.AccountLink " & _
        "   WHERE StkItem.ServiceItem = 0 and PostST.TrCodeID <> 28 " & _
        "	GROUP BY StkItem.Description_1, StkItem.Code, StkItem.StockLink, _etblInvSegValue.cDescription, _etblInvSegValue_1.cDescription, " & _
        "    _etblInvSegValue_2.cDescription, _etblInvSegValue_3.cDescription, _etblInvSegValue_4.cDescription, _etblInvSegValue_6.cDescription, " & _
        "    _etblInvSegValue_7.cDescription, StkItem.Qty_On_Hand, StkItem.Re_Ord_Qty, StkItem.ItemGroup, StkItem.Re_Ord_Lvl, StkItem.Max_Lvl " & _
        "	ORDER BY StkItem.Description_1, StkItem.ItemGroup, PartNo, Descr "


        Dim objSQL As New clsSqlConn
        With objSQL
            DS = New DataSet
            DS = .Get_Data_Sql(SQL)

            UG.DataSource = DS.Tables(0)

            UG.DisplayLayout.Bands(0).Columns("1_RED").Width = 15
            UG.DisplayLayout.Bands(0).Columns("2_GREEN").Width = 15
            UG.DisplayLayout.Bands(0).Columns("3_YELLOW").Width = 15
            UG.DisplayLayout.Bands(0).Columns("StockLink").Hidden = True

            Dim Cat As String = ""
            Dim UGR As UltraGridRow

            pb.Maximum = UG.Rows.Count
            .Begin_Trans()
            For Each UGR In UG.Rows
                pb.Value = 1 + pb.Value

                'UGR.Cells("RED").Appearan = 10
                'UGR.Cells("GREEN").Width = 10
                'UGR.Cells("YELLOW").Width = 10


                'Updating Qty to Order-----------------------------------------------------
                Dim tDate As Date
                If IsDBNull(UGR.Cells("TxDate").Value) Then
                    tDate = "1/1/1090"
                Else
                    tDate = UGR.Cells("TxDate").Value
                End If


                If DateDiff(DateInterval.Month, tDate, Date.Now()) > 8 Or UGR.Cells("Qty to Order").Value < 0 Then
                    UGR.Cells("Qty to Order").Value = 0
                End If

                If sAgent <> "Admin" And UGR.Cells("Qty to Order").Value = 0 Then
                    UGR.Hidden = True
                End If

                '--------------------------------------------------------------------------

              

                    Cat = ""
                    If UGR.Cells(5).Value >= 50 Then
                        UGR.Cells("1_RED").Appearance.BackColor = Color.Red
                        UGR.Cells("1_RED").Value = "R"
                        Cat = "R"
                    Else
                        UGR.Cells("1_RED").Appearance.BackColor = Color.White
                        UGR.Cells("1_RED").Value = ""
                    End If



                    If UGR.Cells(6).Value >= 60 Then
                        UGR.Cells("2_GREEN").Appearance.BackColor = Color.Green
                        UGR.Cells("2_GREEN").Value = "G"
                        Cat = "G"
                    Else
                        UGR.Cells("2_GREEN").Appearance.BackColor = Color.White
                        UGR.Cells("2_GREEN").Value = ""
                    End If



                    If UGR.Cells(7).Value >= 90 Then
                        UGR.Cells("3_YELLOW").Appearance.BackColor = Color.Yellow
                        UGR.Cells("3_YELLOW").Value = "Y"
                        Cat = "Y"
                    Else
                        UGR.Cells("3_YELLOW").Appearance.BackColor = Color.White
                        UGR.Cells("3_YELLOW").Value = ""
                    End If

                    If Cat = "" Then
                        Cat = "W"
                    End If

                'If sAgent = "Admin" Then
                ''If MessageBox.Show("Do you want to update Categories", "", MessageBoxButtons.YesNo) = True Then
                'SQL = " UPDATE StkItem SET ucIICategory = '" & Cat & "' WHERE StockLink = " & UGR.Cells("StockLink").Value & " "
                'If .Execute_Sql_Trans(SQL) = 0 Then
                '    .Rollback_Trans()
                '    Exit Sub
                'End If
                ''End If
                'End If
            Next
                .Commit_Trans()
                pb.Value = 0

                'UG.DisplayLayout.Bands(0).Columns("StockLink").Hidden = True
                'UG.DisplayLayout.Bands(0).Columns("Code").Hidden = True
        End With
    End Sub

    Private Sub tsbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
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

    Private Sub update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles update.Click
        Dim objSQL As New clsSqlConn
        Dim maxLvl As String = ""
        Dim tDate As Date
        Dim saleQty As Double
        With objSQL
            pb.Value = 0
            pb.Maximum = UG.Rows.Count
            .Begin_Trans()
            For Each UGR In UG.Rows
                pb.Value = 1 + pb.Value
                If IsDBNull(UGR.Cells("TxDate").Value) Then
                    tDate = "1/1/1090"
                Else
                    tDate = UGR.Cells("TxDate").Value
                End If

                If cmbDays.Text = "30 Days" Then
                    saleQty = UGR.Cells("30 Days").Value
                ElseIf cmbDays.Text = "60 Days" Then
                    saleQty = UGR.Cells("60 Days").Value
                ElseIf cmbDays.Text = "90 Days" Then
                    saleQty = UGR.Cells("90 Days").Value
                End If

                'If Math.Ceiling(UGR.Cells("90 Days").Value * ROQ.Text / 100) > UGR.Cells("Max_Lvl").Value Then
                maxLvl = ", Max_Lvl = '" & Math.Ceiling(saleQty * ROQ.Text / 100) & "' "
                'End If

                If DateDiff(DateInterval.Month, tDate, Date.Now()) < 8 And saleQty > 0 Then
                    SQL = " UPDATE StkItem SET Re_Ord_Lvl = '" & Math.Ceiling(saleQty * ROL.Text / 100) & "', Re_Ord_Qty = '" & Math.Ceiling(saleQty * ROQ.Text / 100) & "' " & maxLvl & "   WHERE StockLink = " & UGR.Cells("StockLink").Value & " "
                    If .Execute_Sql_Trans(SQL) = 0 Then
                        .Rollback_Trans()
                        Exit Sub
                    End If
                End If
            Next
            .Commit_Trans()
            MessageBox.Show("Successfully Updated", "Pastel Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Information)
            pb.Value = 0
        End With
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        fillData()
        Permisions()
    End Sub
End Class