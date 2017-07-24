Imports Infragistics.Win.UltraWinGrid
Imports System.Data
Imports System.Data.SqlClient

Public Class frmReOrder
    Public item As Integer
    Private Sub frmReOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cnbItemDesc.DropDownWidth = 500
        Get_ItemByGroup()
        Get_ItemDesc()
    End Sub

    Private Sub Fill_UG()

        Dim objSQL As New clsSqlConn

        With objSQL
            If cbAllGroups.Checked = False Then
                SQL = " SELECT     StkItem.StockLink ,_bvStockFull.Description_1, _bvStockFull.cInvSegValue1Value, _bvStockFull.cInvSegValue2Value, _bvStockFull.cInvSegValue3Value, " & _
                    " _bvStockFull.cInvSegValue4Value, _bvStockFull.ItemGroup, StkItem.Re_Ord_Lvl, _bvStockFull.Qty_On_Hand, _bvStockFull.QtyInStock,  " & _
                    " _bvStockFull.QtyOnPO, _bvStockFull.BinLocationName, _bvStockFull.DefaultPriceIncl, _bvStockFull.SerialItem  , StkItem.ubIIReOrder AS [Re Order] ,StkItem.ucIIHSCode AS [HS Code], _bvStockFull.ItemActive" & _
                    " FROM         _bvStockFull LEFT OUTER JOIN " & _
                    " StkItem ON _bvStockFull.StockLink = StkItem.StockLink LEFT OUTER JOIN " & _
                    " _etblInvSegValue ON StkItem.iInvSegValue6ID = _etblInvSegValue.idInvSegValue " & _
                    " WHERE     _bvStockFull.ItemGroup = '" & Trim(cmbItemGroup.Value) & "' AND  _bvStockFull.Qty_On_Hand < StkItem.Re_Ord_Lvl  " & _
                    " ORDER BY _bvStockFull.cSimpleCode"
            Else
                SQL = " SELECT     StkItem.StockLink ,_bvStockFull.Description_1, _bvStockFull.cInvSegValue1Value, _bvStockFull.cInvSegValue2Value, _bvStockFull.cInvSegValue3Value, " & _
                    " _bvStockFull.cInvSegValue4Value, _bvStockFull.ItemGroup, StkItem.Re_Ord_Lvl, _bvStockFull.Qty_On_Hand, _bvStockFull.QtyInStock,  " & _
                    " _bvStockFull.QtyOnPO, _bvStockFull.BinLocationName, _bvStockFull.DefaultPriceIncl, _bvStockFull.SerialItem  , StkItem.ubIIReOrder AS [Re Order] ,StkItem.ucIIHSCode AS [HS Code], _bvStockFull.ItemActive" & _
                    " FROM         _bvStockFull LEFT OUTER JOIN " & _
                    " StkItem ON _bvStockFull.StockLink = StkItem.StockLink LEFT OUTER JOIN " & _
                    " _etblInvSegValue ON StkItem.iInvSegValue6ID = _etblInvSegValue.idInvSegValue " & _
                    " ORDER BY _bvStockFull.cSimpleCode"
            End If

            'SQL = "SELECT StkItem.StockLink, StkItem.cSimpleCode AS [Simple Code], StkItem.Code, StkItem.Description_1, StkItem.ItemActive AS Active, " & _
            '" StkItem.Description_2, StkItem.Description_3 , StkItem.ubIIReOrder AS [Re Order] " & _
            '" FROM StkItem INNER JOIN " & _
            '" _etblInvSegValue ON StkItem.iInvSegValue6ID = _etblInvSegValue.idInvSegValue " & _
            '" WHERE _etblInvSegValue.cValue = '" & Trim(cmbItemGroup.Value) & "' AND StkItem.Qty_On_Hand < StkItem.Re_Ord_Lvl ORDER BY StkItem.cSimpleCode "

            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
            UG.DataSource = DS.Tables(0)
            UG.DisplayLayout.Bands(0).Columns("StockLink").Hidden = True

            'UG.DisplayLayout.Bands(0).Columns("Code").CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
            'UG.DisplayLayout.Bands(0).Columns("Description_1").CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
            'UG.DisplayLayout.Bands(0).Columns("Description_2").CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
            'UG.DisplayLayout.Bands(0).Columns("Description_3").CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
            'UG.DisplayLayout.Bands(0).Columns("Active").CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
            'UG.DisplayLayout.Bands(0).Columns("Simple Code").CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect

            If DS.Tables(0).Rows(0)("ItemActive") = True Then
                lblActive.Text = "Active"
            Else
                lblActive.Text = "Deactive"
            End If

        End With

    End Sub

    Private Sub Get_ItemByGroup()
        Dim objSQL As New clsSqlConn
        With objSQL
            'SQL = " SELECT DISTINCT _etblInvSegValue.idInvSegValue, _etblInvSegValue.cValue " & _
            '" FROM _etblInvSegValue INNER JOIN " & _
            '" StkItem ON _etblInvSegValue.idInvSegValue = StkItem.iInvSegValue6ID" 

            SQL = " SELECT idGrpTbl,StGroup, Description FROM GrpTbl "
            DS = New DataSet
            DS = .Get_Data_Sql(SQL)

            cmbItemGroup.DataSource = DS.Tables(0)
            cmbItemGroup.DisplayMember = "Description"
            cmbItemGroup.ValueMember = "StGroup"
        End With
    End Sub
    Private Sub Get_ItemDesc()
        Dim objSQL As New clsSqlConn
        With objSQL
            'SQL = " SELECT DISTINCT _etblInvSegValue.idInvSegValue, _etblInvSegValue.cValue " & _
            '" FROM _etblInvSegValue INNER JOIN " & _
            '" StkItem ON _etblInvSegValue.idInvSegValue = StkItem.iInvSegValue6ID" 

            SQL = " select idInvSegValue, cDescription from _etblInvSegValue WHERE     (iInvSegGroupID = 5) order by cDescription "
            DS = New DataSet
            DS = .Get_Data_Sql(SQL)

            cnbItemDesc.DataSource = DS.Tables(0)
            cnbItemDesc.DisplayMember = "cDescription"
            cnbItemDesc.ValueMember = "cDescription"
            cnbItemDesc.DisplayLayout.Bands(0).Columns("idInvSegValue").Hidden = True
        End With


        'Get Item Group
        SQL = "SELECT    StGroup   FROM GrpTbl "
        Dim objSQL1 As New clsSqlConn
        With objSQL1
            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
            cmbGroup.DataSource = DS.Tables(0)
            cmbGroup.ValueMember = "StGroup"
            cmbGroup.DisplayMember = "StGroup"
            'cmbInventoryItem.DisplayLayout.Bands(0).Columns("StockLink").Hidden = True
            'cmbInventoryItem.DisplayLayout.Bands(0).Columns("Code").Hidden = True
        End With
    End Sub

    Private Sub tsbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbExit.Click

        Me.Close()

    End Sub

    Private Sub tsbConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbConnect.Click

        Fill_UG()

    End Sub

    Private Sub tsbExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbExport.Click
        If item = 1 Then
            UG.PerformAction(UltraGridAction.CommitRow)
            Dim objSQL As New clsSqlConn
            With objSQL
                Dim ugrow As UltraGridRow
                .Begin_Trans()
                For Each ugrow In UG.DisplayLayout.Rows
                    Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database = dbUdawatta_new  " & "")
                    'SQL = " UPDATE StkItem SET ubIIReOrder = " & IIf(ugrow.Cells("Re Order").Value = False, 0, 1) & " , ucIIHSCode = " & IIf(ugrow.Cells("HS Code").Text.Length = 0, "'", "'" & ugrow.Cells("HS Code").Value) & "'" & " WHERE StockLink = " & ugrow.Cells("StockLink").Value & " "
                    SQL = " UPDATE StkItem SET ubIIReOrder = " & IIf(ugrow.Cells("Re Order").Value = False, 0, 1) & "  WHERE StockLink = " & ugrow.Cells("StockLink").Value & " ;"
                    SQL = SQL + " UPDATE StkItem SET Re_Ord_Lvl = " & IIf(ugrow.Cells("Re Order").Value = False, 0, "Re_Ord_Lvl") & "  WHERE StockLink = " & ugrow.Cells("StockLink").Value & " "
                    If .Execute_Sql_Trans(SQL) = 0 Then
                        .Rollback_Trans()
                        Exit Sub
                    End If

                    'nawinna
                    Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database = dbNawinna_new  " & "")
                    'SQL = " UPDATE StkItem SET ubIIReOrder = " & IIf(ugrow.Cells("Re Order").Value = False, 0, 1) & " , ucIIHSCode = " & IIf(ugrow.Cells("HS Code").Text.Length = 0, "'", "'" & ugrow.Cells("HS Code").Value) & "'" & " WHERE StockLink = " & ugrow.Cells("StockLink").Value & " "
                    SQL = " UPDATE StkItem SET Re_Ord_Lvl = " & IIf(ugrow.Cells("Re Order").Value = False, 0, "Re_Ord_Lvl") & "  WHERE StockLink = " & ugrow.Cells("StockLink").Value & " "
                    If .Execute_Sql_Trans(SQL) = 0 Then
                        .Rollback_Trans()
                        Exit Sub
                    End If

                    ''kurunegala
                    'Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database = dbKurunegala_new  " & "")
                    ''SQL = " UPDATE StkItem SET ubIIReOrder = " & IIf(ugrow.Cells("Re Order").Value = False, 0, 1) & " , ucIIHSCode = " & IIf(ugrow.Cells("HS Code").Text.Length = 0, "'", "'" & ugrow.Cells("HS Code").Value) & "'" & " WHERE StockLink = " & ugrow.Cells("StockLink").Value & " "
                    'SQL = " UPDATE StkItem SET Re_Ord_Lvl = " & IIf(ugrow.Cells("Re Order").Value = False, 0, "Re_Ord_Lvl") & " WHERE StockLink = " & ugrow.Cells("StockLink").Value & " "
                    'If .Execute_Sql_Trans(SQL) = 0 Then
                    '    .Rollback_Trans()
                    '    Exit Sub
                    'End If

                    ''kiribathgoda
                    'Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database = dbKiribathgoda_new  " & "")
                    ''SQL = " UPDATE StkItem SET ubIIReOrder = " & IIf(ugrow.Cells("Re Order").Value = False, 0, 1) & " , ucIIHSCode = " & IIf(ugrow.Cells("HS Code").Text.Length = 0, "'", "'" & ugrow.Cells("HS Code").Value) & "'" & " WHERE StockLink = " & ugrow.Cells("StockLink").Value & " "
                    'SQL = " UPDATE StkItem SET Re_Ord_Lvl = " & IIf(ugrow.Cells("Re Order").Value = False, 0, "Re_Ord_Lvl") & "  WHERE StockLink = " & ugrow.Cells("StockLink").Value & " "
                    'If .Execute_Sql_Trans(SQL) = 0 Then
                    '    .Rollback_Trans()
                    '    Exit Sub
                    'End If




                    'kelaniya
                    Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database = dbKelaniya_new  " & "")
                    'SQL = " UPDATE StkItem SET ubIIReOrder = " & IIf(ugrow.Cells("Re Order").Value = False, 0, 1) & " , ucIIHSCode = " & IIf(ugrow.Cells("HS Code").Text.Length = 0, "'", "'" & ugrow.Cells("HS Code").Value) & "'" & " WHERE StockLink = " & ugrow.Cells("StockLink").Value & " "
                    SQL = " UPDATE StkItem SET Re_Ord_Lvl = " & IIf(ugrow.Cells("Re Order").Value = False, 0, "Re_Ord_Lvl") & "  WHERE StockLink = " & ugrow.Cells("StockLink").Value & " "
                    If .Execute_Sql_Trans(SQL) = 0 Then
                        .Rollback_Trans()
                        Exit Sub
                    End If

                    'End If
                Next
                .Commit_Trans()
                MsgBox("Successfully Updated", MsgBoxStyle.Information, "Pasterl Evolution")
            End With
        ElseIf item = 2 Then
            UG.PerformAction(UltraGridAction.CommitRow)
            Dim objSQL As New clsSqlConn
            With objSQL
                Dim ugrow As UltraGridRow
                .Begin_Trans()
                For Each ugrow In UG.DisplayLayout.Rows
                    Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database = dbUdawatta_new  " & "")
                    'SQL = " UPDATE StkItem SET ubIIReOrder = " & IIf(ugrow.Cells("Re Order").Value = False, 0, 1) & " , ucIIHSCode = " & IIf(ugrow.Cells("HS Code").Text.Length = 0, "'", "'" & ugrow.Cells("HS Code").Value) & "'" & " WHERE StockLink = " & ugrow.Cells("StockLink").Value & " "
                    SQL = " UPDATE StkItem SET  ucIIHSCode = '" & UG.DisplayLayout.Rows(0).Cells("HS Code").Value & "', ucIICID = '" & UG.DisplayLayout.Rows(0).Cells("CID").Value & "' WHERE StockLink = " & ugrow.Cells("StockLink").Value & " ; "
                    SQL = SQL + " UPDATE StkItem SET ItemGroup = '" & cmbGroup.Value & "'  WHERE StockLink = " & ugrow.Cells("StockLink").Value & " "
                    If .Execute_Sql_Trans(SQL) = 0 Then
                        .Rollback_Trans()
                        Exit Sub
                    End If

                    'nawinna
                    Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database = dbNawinna_new  " & "")
                    'SQL = " UPDATE StkItem SET ubIIReOrder = " & IIf(ugrow.Cells("Re Order").Value = False, 0, 1) & " , ucIIHSCode = " & IIf(ugrow.Cells("HS Code").Text.Length = 0, "'", "'" & ugrow.Cells("HS Code").Value) & "'" & " WHERE StockLink = " & ugrow.Cells("StockLink").Value & " "
                    SQL = " UPDATE StkItem SET ItemGroup = '" & cmbGroup.Value & "'  WHERE cSimpleCode = '" & ugrow.Cells("cSimpleCode").Value & "' "
                    Con2.Open()
                    CMD = New SqlCommand(SQL, Con2)
                    CMD.CommandType = CommandType.Text
                    CMD.ExecuteNonQuery()
                    Con2.Close()


                    'If .Execute_Sql_Trans2(SQL) = 0 Then
                    '    .Rollback_Trans()
                    '    Exit Sub
                    'End If

                    'kurunegala
                    'Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database = dbKurunegala_new  " & "")
                    'SQL = " UPDATE StkItem SET ubIIReOrder = " & IIf(ugrow.Cells("Re Order").Value = False, 0, 1) & " , ucIIHSCode = " & IIf(ugrow.Cells("HS Code").Text.Length = 0, "'", "'" & ugrow.Cells("HS Code").Value) & "'" & " WHERE StockLink = " & ugrow.Cells("StockLink").Value & " "
                    'SQL = " UPDATE StkItem SET ItemGroup = '" & cmbGroup.Value & "' WHERE cSimpleCode = '" & ugrow.Cells("cSimpleCode").Value & "' "
                    'Con2.Open()
                    'CMD = New SqlCommand(SQL, Con2)
                    'CMD.CommandType = CommandType.Text
                    'CMD.ExecuteNonQuery()
                    'Con2.Close()

                    'kiribathgoda
                    'Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database = dbKiribathgoda_new  " & "")
                    ''SQL = " UPDATE StkItem SET ubIIReOrder = " & IIf(ugrow.Cells("Re Order").Value = False, 0, 1) & " , ucIIHSCode = " & IIf(ugrow.Cells("HS Code").Text.Length = 0, "'", "'" & ugrow.Cells("HS Code").Value) & "'" & " WHERE StockLink = " & ugrow.Cells("StockLink").Value & " "
                    'SQL = " UPDATE StkItem SET ItemGroup = '" & cmbGroup.Value & "'  WHERE cSimpleCode = '" & ugrow.Cells("cSimpleCode").Value & "' "
                    'Con2.Open()
                    'CMD = New SqlCommand(SQL, Con2)
                    'CMD.CommandType = CommandType.Text
                    'CMD.ExecuteNonQuery()
                    'Con2.Close()

                    'kelaniya
                    Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database = dbKelaniya_new  " & "")
                    'SQL = " UPDATE StkItem SET ubIIReOrder = " & IIf(ugrow.Cells("Re Order").Value = False, 0, 1) & " , ucIIHSCode = " & IIf(ugrow.Cells("HS Code").Text.Length = 0, "'", "'" & ugrow.Cells("HS Code").Value) & "'" & " WHERE StockLink = " & ugrow.Cells("StockLink").Value & " "
                    SQL = " UPDATE StkItem SET ItemGroup = '" & cmbGroup.Value & "'  WHERE cSimpleCode = '" & ugrow.Cells("cSimpleCode").Value & "' "
                    Con2.Open()
                    CMD = New SqlCommand(SQL, Con2)
                    CMD.CommandType = CommandType.Text
                    CMD.ExecuteNonQuery()
                    Con2.Close()


                    'End If
                Next
                .Commit_Trans()
                MsgBox("Successfully Updated", MsgBoxStyle.Information, "Pasterl Evolution")
            End With
        End If

    End Sub



    Private Sub tsSI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsSI.Click

        Dim ugR1 As UltraGridRow

        For Each ugR1 In UG.Rows

            ugR1.Cells("Re Order").Value = True

        Next

    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click

        Dim ugR1 As UltraGridRow

        For Each ugR1 In UG.Rows

            ugR1.Cells("Re Order").Value = False

        Next

    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & sSQLSrvDataBase & "")


        SQL = "  UPDATE StkItem SET  ItemActive = 0 WHERE ItemGroup = '" & cmbItemGroup.Value & "'  "
        CMD = New SqlCommand(SQL, Con2)
        CMD.CommandType = CommandType.Text
        Con2.Open()
        CMD.ExecuteNonQuery()
        Con2.Close()
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & sSQLSrvDataBase & "")


        SQL = "  UPDATE StkItem SET  ItemActive = 1 WHERE ItemGroup = '" & cmbItemGroup.Value & "' AND Description_1 <> '' "
        CMD = New SqlCommand(SQL, Con2)
        CMD.CommandType = CommandType.Text
        Con2.Open()
        CMD.ExecuteNonQuery()
        Con2.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        cnbItemDesc.Value = ""
        cmbGroup.Value = ""

        Dim HS As String
        If sSQLSrvDataBase = "dbUdawatta_new" Then
            HS = " , StkItem.ucIIHSCode AS [HS Code] , StkItem.ucIICID AS [CID]  "
        Else
            HS = ""
        End If

        Dim objSQL As New clsSqlConn

        With objSQL

            SQL = " SELECT     StkItem.StockLink ,_bvStockFull.Description_1, _bvStockFull.cInvSegValue1Value, _bvStockFull.cInvSegValue2Value, _bvStockFull.cInvSegValue3Value, " & _
                " _bvStockFull.cInvSegValue4Value, _bvStockFull.ItemGroup, StkItem.Re_Ord_Lvl, StkItem.Re_Ord_Qty, _bvStockFull.Qty_On_Hand,   " & _
                "  _bvStockFull.BinLocationName, _bvStockFull.DefaultPriceIncl  " & HS & ", _bvStockFull.ItemActive" & _
                " FROM         _bvStockFull LEFT OUTER JOIN " & _
                " StkItem ON _bvStockFull.StockLink = StkItem.StockLink LEFT OUTER JOIN " & _
                " _etblInvSegValue ON StkItem.iInvSegValue6ID = _etblInvSegValue.idInvSegValue "

            If cbAllGroups.Checked = False Then
                SQL = SQL + "  WHERE     _bvStockFull.ItemGroup = '" & Trim(cmbItemGroup.Value) & "' AND  StkItem.Qty_On_Hand < StkItem.Re_Ord_Lvl  " & _
                " ORDER BY _bvStockFull.cSimpleCode "
            Else
                SQL = SQL + "  WHERE    StkItem.Qty_On_Hand < StkItem.Re_Ord_Lvl  " & _
                " ORDER BY _bvStockFull.cSimpleCode "
            End If

            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
            If DS.Tables(0).Rows.Count > 0 Then
                UG.DataSource = DS.Tables(0)
                UG.DisplayLayout.Bands(0).Columns("StockLink").Hidden = True

                'UG.DisplayLayout.Bands(0).Columns("Code").CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
                'UG.DisplayLayout.Bands(0).Columns("Description_1").CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
                'UG.DisplayLayout.Bands(0).Columns("Description_2").CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
                'UG.DisplayLayout.Bands(0).Columns("Description_3").CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
                'UG.DisplayLayout.Bands(0).Columns("Active").CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
                'UG.DisplayLayout.Bands(0).Columns("Simple Code").CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect

                If DS.Tables(0).Rows(0)("ItemActive") = True Then
                    lblActive.Text = "Active"
                Else
                    lblActive.Text = "Deactive"
                End If
            End If
        End With
        item = 1
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        cmbItemGroup.Value = ""
        If cnbItemDesc.Value = "" Then
            Exit Sub
        End If

        Dim HS As String
        If sSQLSrvDataBase = "dbUdawatta_new" Then
            HS = " , StkItem.ucIIHSCode AS [HS Code] , StkItem.ucIICID AS [CID]  "
        Else
            HS = ""
        End If

        Dim objSQL As New clsSqlConn

        With objSQL

            SQL = " SELECT     StkItem.StockLink ,_bvStockFull.Description_1, _bvStockFull.cInvSegValue1Value, _bvStockFull.cInvSegValue2Value as Description, _bvStockFull.cInvSegValue3Value, " & _
                " _bvStockFull.cInvSegValue4Value, _bvStockFull.ItemGroup, StkItem.Re_Ord_Lvl, StkItem.Re_Ord_Qty, _bvStockFull.Qty_On_Hand,   " & _
                "  _bvStockFull.BinLocationName, _bvStockFull.DefaultPriceIncl, _bvStockFull.SerialItem  " & HS & ", _bvStockFull.ItemActive ,_bvStockFull.cSimpleCode" & _
                " FROM         _bvStockFull LEFT OUTER JOIN " & _
                " StkItem ON _bvStockFull.StockLink = StkItem.StockLink LEFT OUTER JOIN " & _
                " _etblInvSegValue ON StkItem.iInvSegValue6ID = _etblInvSegValue.idInvSegValue " & _
                " WHERE     _bvStockFull.cInvSegValue2Value = '" & Trim(cnbItemDesc.Value) & "'   " & _
                " ORDER BY _bvStockFull.cSimpleCode"


            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
            If DS.Tables(0).Rows.Count > 0 Then
                UG.DataSource = DS.Tables(0)
                UG.DisplayLayout.Bands(0).Columns("StockLink").Hidden = True
            End If
            'UG.DisplayLayout.Bands(0).Columns("Code").CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
            'UG.DisplayLayout.Bands(0).Columns("Description_1").CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
            'UG.DisplayLayout.Bands(0).Columns("Description_2").CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
            'UG.DisplayLayout.Bands(0).Columns("Description_3").CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
            'UG.DisplayLayout.Bands(0).Columns("Active").CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect
            'UG.DisplayLayout.Bands(0).Columns("Simple Code").CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect

            If DS.Tables(0).Rows(0)("ItemActive") = True Then
                lblActive.Text = "Active"
            Else
                lblActive.Text = "Deactive"
            End If

            cmbGroup.Value = UG.DisplayLayout.Rows(0).Cells("ItemGroup").Value

        End With



        item = 2
    End Sub

    Private Sub tsbSaveMySettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSaveMySettings.Click
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

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        Dim objSQL As New clsSqlConn
        With objSQL

            SQL = " SELECT cSimpleCode FROM StkItem "

            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
            If DS.Tables(0).Rows.Count > 0 Then
                Dim dr As DataRow
                .Begin_Trans()
                For Each dr In DS.Tables(0).Rows

                    SQL = "  UPDATE dbKelaniya_new.dbo.StkItem set Description_1 = (select dbUdawatta_new.dbo.StkItem.Description_1 from dbUdawatta_new.dbo.StkItem WHERE StkItem.cSimpleCode = '" & dr("cSimpleCode") & "')," & _
                    "                                              Description_2 = (select dbUdawatta_new.dbo.StkItem.Description_2 from dbUdawatta_new.dbo.StkItem WHERE StkItem.cSimpleCode = '" & dr("cSimpleCode") & "') " & _
                    " WHERE  StkItem.cSimpleCode = '" & dr("cSimpleCode") & "' "

                    SQL += " UPDATE dbNawinna_new.dbo.StkItem set Description_1 = (select dbUdawatta_new.dbo.StkItem.Description_1 from dbUdawatta_new.dbo.StkItem WHERE StkItem.cSimpleCode = '" & dr("cSimpleCode") & "') , " & _
                    "                                             Description_2 = (select dbUdawatta_new.dbo.StkItem.Description_2 from dbUdawatta_new.dbo.StkItem WHERE StkItem.cSimpleCode = '" & dr("cSimpleCode") & "') " & _
                    " WHERE  StkItem.cSimpleCode = '" & dr("cSimpleCode") & "' "

                    SQL += " UPDATE dbKiribathgoda_new.dbo.StkItem set Description_1 = (select dbUdawatta_new.dbo.StkItem.Description_1 from dbUdawatta_new.dbo.StkItem WHERE StkItem.cSimpleCode = '" & dr("cSimpleCode") & "') , " & _
                    "                                                  Description_2 = (select dbUdawatta_new.dbo.StkItem.Description_2 from dbUdawatta_new.dbo.StkItem WHERE StkItem.cSimpleCode = '" & dr("cSimpleCode") & "') " & _
                    " WHERE  StkItem.cSimpleCode = '" & dr("cSimpleCode") & "' "

                    SQL += " UPDATE dbKurunegala_new.dbo.StkItem set Description_1 = (select dbUdawatta_new.dbo.StkItem.Description_1 from dbUdawatta_new.dbo.StkItem WHERE StkItem.cSimpleCode = '" & dr("cSimpleCode") & "') , " & _
                    "                                                Description_2 = (select dbUdawatta_new.dbo.StkItem.Description_2 from dbUdawatta_new.dbo.StkItem WHERE StkItem.cSimpleCode = '" & dr("cSimpleCode") & "') " & _
                    " WHERE  StkItem.cSimpleCode = '" & dr("cSimpleCode") & "' "

                    .Execute_Sql_Trans(SQL)
                    
                Next
                .Commit_Trans()
                'Line = 0
            End If
            MsgBox("Sucessfully Updated", MsgBoxStyle.Information, "Validation")

        End With
    End Sub
End Class