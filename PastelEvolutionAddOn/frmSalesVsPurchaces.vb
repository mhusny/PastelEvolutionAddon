Imports System.Data
Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid

Public Class frmSalesVsPurchaces


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

            ''ugExcelExporter.Export(UG, myFineName)


            Dim Proc As New System.Diagnostics.Process
            Proc.StartInfo.FileName = myFineName
            Proc.Start()

        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles update.Click
        fillData()
    End Sub

    Private Sub fillData()

        Dim DS As New DataSet


        'sales starting from begining of the month + quantity receiver over 3 months
        'SQL = "  SELECT  dbo.InvNum.InvDate, dbo.InvNum.InvNumber, dbo.InvNum.ExtOrderNum, dbo.StkItem.Description_1, " & _
        '" dbo._btblInvoiceLines.fQuantity, InvNum_1.InvNumber AS Expr1, _btblInvoiceLines_1.cLotNumber, _btblInvoiceLines_1.fQuantity AS Expr2,  " & _
        '" InvNum_1.DocType, CASE WHEN SUBSTRING(InvNum.ExtOrderNum, 4, LEN(InvNum.ExtOrderNum)) = SUBSTRING(_btblInvoiceLines_1.cLotNumber,  " & _
        '" 0, CHARINDEX('-', _btblInvoiceLines_1.cLotNumber, 0)) THEN 1 ELSE 0 END AS C, SUBSTRING(dbo.InvNum.ExtOrderNum, 4,  " & _
        '" LEN(dbo.InvNum.ExtOrderNum)) AS INO, SUBSTRING(_btblInvoiceLines_1.cLotNumber, 0, CHARINDEX('-', _btblInvoiceLines_1.cLotNumber, 0))  " & _
        '" AS LNO  " & _
        '" FROM         dbo.InvNum INNER JOIN  " & _
        '" dbo._btblInvoiceLines ON dbo.InvNum.AutoIndex = dbo._btblInvoiceLines.iInvoiceID INNER JOIN " & _
        '" dbo.StkItem ON dbo._btblInvoiceLines.iStockCodeID = dbo.StkItem.StockLink INNER JOIN  " & _
        '" dbo._btblInvoiceLines AS _btblInvoiceLines_1 ON dbo.StkItem.StockLink = _btblInvoiceLines_1.iStockCodeID INNER JOIN  " & _
        '"    dbo.InvNum AS InvNum_1 ON _btblInvoiceLines_1.iInvoiceID = InvNum_1.AutoIndex  " & _
        '" WHERE     (dbo.InvNum.AccountID = 2) AND (dbo.InvNum.DocType = 5) AND (dbo.InvNum.ExtOrderNum <> '') AND (InvNum_1.DocType <> 5) AND  " & _
        '" (InvNum_1.DocState = 4)  " & _
        '" GROUP BY dbo.InvNum.InvDate, InvNum_1.InvDate, dbo.InvNum.InvNumber, dbo.InvNum.ExtOrderNum, dbo.StkItem.Description_1,  " & _
        '" dbo._btblInvoiceLines.fQuantity, InvNum_1.InvNumber, _btblInvoiceLines_1.cLotNumber, _btblInvoiceLines_1.fQuantity, dbo.InvNum.InvDate,  " & _
        '" InvNum_1.DocType  " & _
        '" ORDER BY dbo.InvNum.InvDate, InvNum_1.InvDate, dbo.InvNum.InvNumber, dbo.StkItem.Description_1, _btblInvoiceLines_1.cLotNumber "


        SQL = " set dateformat dmy SELECT     AutoIndex, InvDate, InvNumber,  ExtOrderNum, SUBSTRING(dbo.InvNum.ExtOrderNum, 4,  " & _
        " LEN(dbo.InvNum.ExtOrderNum)) AS INO " & _
        " FROM InvNum " & _
        " WHERE     (AccountID = 2) AND (DocType = 5) AND (ExtOrderNum <> '') AND (InvDate >= '" & dtFrom.Value & "' AND InvDate <= '" & dtTo.Value & "') " & _
        " GROUP BY InvDate, InvNumber, InvDate, AutoIndex, ExtOrderNum " & _
        " ORDER BY InvDate, InvNumber "

        SQL += " SELECT    StkItem.StockLink, _btblInvoiceLines.iInvoiceID, StkItem.Description_1, _btblInvoiceLines.fQuantity  " & _
        " FROM         _btblInvoiceLines INNER JOIN " & _
        " StkItem ON _btblInvoiceLines.iStockCodeID = StkItem.StockLink " & _
        " GROUP BY StkItem.Description_1, _btblInvoiceLines.iInvoiceID, _btblInvoiceLines.fQuantity, StkItem.StockLink " & _
        " ORDER BY StkItem.Description_1 "

        SQL += " SELECT     _btblInvoiceLines_1.iStockCodeID, InvNum_1.InvDate, InvNum_1.InvNumber , _btblInvoiceLines_1.cLotNumber, _btblInvoiceLines_1.fQuantity , InvNum_1.DocType,  " & _
        " SUBSTRING(_btblInvoiceLines_1.cLotNumber, 0, CHARINDEX('-', _btblInvoiceLines_1.cLotNumber, 0)) AS LNO, InvNum_1.InvTotIncl " & _
        " FROM         InvNum AS InvNum_1 INNER JOIN " & _
        " _btblInvoiceLines AS _btblInvoiceLines_1 ON InvNum_1.AutoIndex = _btblInvoiceLines_1.iInvoiceID " & _
        " WHERE     (InvNum_1.DocType <> 5) AND (InvNum_1.DocState = 4) " & _
        " GROUP BY InvNum_1.InvDate, InvNum_1.InvNumber, _btblInvoiceLines_1.cLotNumber, _btblInvoiceLines_1.fQuantity, InvNum_1.DocType,  " & _
        " _btblInvoiceLines_1.iStockCodeID, InvNum_1.InvTotIncl  " & _
        " ORDER BY InvNum_1.InvDate, _btblInvoiceLines_1.cLotNumber "


        'SQL += "  SELECT  _btblInvoiceLines_1.iStockCodeID, _btblInvoiceLines_1.cLotNumber, _btblInvoiceLines_1.fQuantity AS Expr2,  " & _
        '" InvNum_1.DocType, CASE WHEN SUBSTRING(InvNum.ExtOrderNum, 4, LEN(InvNum.ExtOrderNum)) = SUBSTRING(_btblInvoiceLines_1.cLotNumber,  " & _
        '" 0, CHARINDEX('-', _btblInvoiceLines_1.cLotNumber, 0)) THEN 1 ELSE 0 END AS C, SUBSTRING(dbo.InvNum.ExtOrderNum, 4,  " & _
        '" LEN(dbo.InvNum.ExtOrderNum)) AS INO, SUBSTRING(_btblInvoiceLines_1.cLotNumber, 0, CHARINDEX('-', _btblInvoiceLines_1.cLotNumber, 0))  " & _
        '" AS LNO  " & _
        '" FROM         dbo.InvNum INNER JOIN  " & _
        '" dbo._btblInvoiceLines ON dbo.InvNum.AutoIndex = dbo._btblInvoiceLines.iInvoiceID INNER JOIN " & _
        '" dbo.StkItem ON dbo._btblInvoiceLines.iStockCodeID = dbo.StkItem.StockLink INNER JOIN  " & _
        '" dbo._btblInvoiceLines AS _btblInvoiceLines_1 ON dbo.StkItem.StockLink = _btblInvoiceLines_1.iStockCodeID INNER JOIN  " & _
        '"    dbo.InvNum AS InvNum_1 ON _btblInvoiceLines_1.iInvoiceID = InvNum_1.AutoIndex  " & _
        '" WHERE     (dbo.InvNum.AccountID = 2) AND (dbo.InvNum.DocType = 5) AND (dbo.InvNum.ExtOrderNum <> '') AND (InvNum_1.DocType <> 5) AND  " & _
        '" (InvNum_1.DocState = 4)  " & _
        '" GROUP BY dbo.InvNum.InvDate, InvNum_1.InvDate, dbo.InvNum.InvNumber, dbo.InvNum.ExtOrderNum, dbo.StkItem.Description_1,  " & _
        '" dbo._btblInvoiceLines.fQuantity, InvNum_1.InvNumber, _btblInvoiceLines_1.cLotNumber, _btblInvoiceLines_1.fQuantity, dbo.InvNum.InvDate,  " & _
        '" InvNum_1.DocType, _btblInvoiceLines_1.iStockCodeID  " & _
        '" ORDER BY dbo.InvNum.InvDate, InvNum_1.InvDate, dbo.InvNum.InvNumber, dbo.StkItem.Description_1, _btblInvoiceLines_1.cLotNumber "





        Dim rel1 As DataRelation
        Dim rel2 As DataRelation
        Dim objSQL As New clsSqlConn
        With objSQL
            DS = New DataSet
            DS = .Get_Data_Sql(SQL)


            rel1 = New DataRelation("Inv_Item_Relation",
                        DS.Tables("Table").Columns("AutoIndex"),
                        DS.Tables("Table1").Columns("iInvoiceID"), False)

            DS.Relations.Add(rel1)

            rel2 = New DataRelation("Item_Inv_Relation",
                        DS.Tables("Table1").Columns("StockLink"),
                        DS.Tables("Table2").Columns("iStockCodeID"), False)

            DS.Relations.Add(rel2)


            UG.DataSource = DS.Tables(0)

            UG.DisplayLayout.Bands(0).Columns("AutoIndex").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("INO").Hidden = True
            UG.DisplayLayout.Bands(1).Columns("StockLink").Hidden = True
            UG.DisplayLayout.Bands(1).Columns("iInvoiceID").Hidden = True
            UG.DisplayLayout.Bands(2).Columns("DocType").Hidden = True
            UG.DisplayLayout.Bands(2).Columns("LNO").Hidden = True
            UG.DisplayLayout.Bands(2).Columns("iStockCodeID").Hidden = True

            For Each row As UltraGridRow In UG.Rows

                For Each childBand As UltraGridChildBand In row.ChildBands

                    For Each row1 As UltraGridRow In childBand.Rows

                        For Each childBand1 As UltraGridChildBand In row1.ChildBands

                            For Each row2 As UltraGridRow In childBand1.Rows

                                If row2.Cells("LNO").Value = row.Cells("INO").Value Then
                                    row2.Hidden = False
                                Else
                                    row2.Hidden = True
                                End If

                            Next

                        Next

                    Next

                Next

            Next

        End With




        SQL = " set dateformat dmy SELECT TxDate, Reference, cAllocs  FROM PostAR  WHERE TrCodeID = 35 "


        Dim str1() As String
        Dim str2() As String
        Dim str3() As String
        Dim SQL2 As String
        Dim objSQL1 As clsSqlConn
        Dim DS1 As DataSet
        Dim alloc As New DataTable
        Dim arow As DataRow
        objSQL = New clsSqlConn
        With objSQL
            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
            DS.Tables.Add()
            Dim dr As DataRow
            For Each dr In DS.Tables(0).Rows
                If dr("cAllocs").ToString().Length > 0 Then
                    str1 = Split(dr("cAllocs"), "|")
                    For i = 0 To str1.Length - 1
                        str2 = Split(str1(i), ";")
                        For j = 0 To str2.Length - 1
                            str3 = Split(str2(j), "=")
                            SQL2 = "  SELECT '" & dr("Reference") & "' as Inv, TxDate, Reference, Description, Credit FROM  PostAR WHERE  AutoIdx = '" & str3(1) & "' "
                            objSQL1 = New clsSqlConn
                            With objSQL1
                                DS1 = New DataSet
                                DS1 = .Get_Data_Sql(SQL2)

                                DS.Tables(1).Merge(DS1.Tables(0))

                            End With
                            Exit For
                        Next j

                    Next i
                End If
            Next

            rel1 = New DataRelation("Inv_pay_Relation",
                        DS.Tables("Table").Columns("Reference"),
                        DS.Tables("Table1").Columns("Inv"), False)

            DS.Relations.Add(rel1)

            UG1.DataSource = DS.Tables(0)

            UG1.DisplayLayout.Bands(0).Columns("cAllocs").Hidden = True
            UG1.DisplayLayout.Bands(1).Columns("Inv").Hidden = True
            'UG.DisplayLayout.Bands(2).Columns("LNO").Hidden = True
            'UG.DisplayLayout.Bands(2).Columns("LNO").Hidden = True
            'UG.DisplayLayout.Bands(2).Columns("LNO").Hidden = True
            'UG.DisplayLayout.Bands(2).Columns("LNO").Hidden = True
            'UG.DisplayLayout.Bands(2).Columns("LNO").Hidden = True
            'UG.DisplayLayout.Bands(2).Columns("LNO").Hidden = True
            'UG.DisplayLayout.Bands(2).Columns("LNO").Hidden = True

        End With


    End Sub

    Private Sub tsbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
    End Sub
End Class