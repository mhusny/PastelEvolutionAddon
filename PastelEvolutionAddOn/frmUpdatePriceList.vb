Imports System.Data
Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid
Public Class frmUpdatePriceList
    'Dim aMainDB As ArrayList
    Public Trans As SqlTransaction
    Dim aDataBase As ArrayList
    Private Sub GetDatabaseName()
        aDataBase = New ArrayList()
        aDataBase.Add("dbUdawatta_new")
        'aDataBase.Add("dbKiribathgoda_new")
        'aDataBase.Add("dbKurunegala_new")
        aDataBase.Add("dbNawinna_new")
        aDataBase.Add("dbKelaniya_new")
        'aDataBase.Add("dbMathara")
        'aDataBase.Add("UDAWATTA_TEST1")
    End Sub
    Private Sub Get_Stock_Item()
        SQL = "SELECT     StockLink,cSimpleCode As SimpleCode,('('+ cSimpleCode +') ' + Code) As ToShow,Code, ItemGroup   FROM _bvStockFull WHERE ItemActive = 1 "
        Dim objSQL As New clsSqlConn
        With objSQL
            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
            cmbInventoryItem.DataSource = DS.Tables(0)
            cmbInventoryItem.ValueMember = "SimpleCode"
            cmbInventoryItem.DisplayMember = "SimpleCode"
            cmbInventoryItem.DisplayLayout.Bands(0).Columns("StockLink").Hidden = True
            cmbInventoryItem.DisplayLayout.Bands(0).Columns("Code").Hidden = True
            cmbInventoryItem.DisplayLayout.Bands(0).Columns("ItemGroup").Hidden = True
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


    Private Sub frmUpdatePriceList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetDatabaseName()
        Get_Stock_Item()
        dtFrom.Value = Now.Date
        dtTo.Value = Now.Date


        If (sAgent <> "Admin") Then
            btnAutoUpdate.Visible = False
        End If
    End Sub

    Private Sub tsbConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbConnect.Click
        Try
            Dim sDB As String
            Dim Obj As Object
            Dim ugR1 As UltraGridRow
            Dim ugR2 As UltraGridRow
            Dim Dr As DataRow
            If cmbInventoryItem.Value.ToString.Trim.Length = 0 Then
                MsgBox("Select Item Code", MsgBoxStyle.Exclamation, "Pastel Evoltuion")
                Exit Sub
            End If
            For Each ugR In UG.Rows.All
                ugR.Delete(False)
            Next

            For Each Obj In aDataBase
                sDB = Obj.ToString
                'sDB = "dbUdawatta"
                Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & sDB & "")
                Dim objSQL = New clsSqlConn
                With objSQL
                    SQL = "SELECT  StockLink " & _
                    " FROM  StkItem WHERE StkItem.cSimpleCode ='" & cmbInventoryItem.Value & "' AND ItemActive = 1"
                    CMD = New SqlCommand(SQL, Con2)
                    CMD.CommandType = CommandType.Text
                    DA = New SqlDataAdapter(CMD)
                    DS = New DataSet
                    Con2.Open()
                    DA.Fill(DS)
                    Con2.Close()

                    Dim DS1 As New DataSet
                    SQL = "SELECT  _etblPriceListPrices.IDPriceListPrices " & _
                    " FROM  _etblPriceListPrices WHERE  _etblPriceListPrices.iStockID ='" & DS.Tables(0).Rows(0)(0) & "' "
                    CMD = New SqlCommand(SQL, Con2)
                    CMD.CommandType = CommandType.Text
                    DA = New SqlDataAdapter(CMD)
                    DS1 = New DataSet
                    Con2.Open()
                    DA.Fill(DS1)
                    Con2.Close()

                    If DS1.Tables(0).Rows.Count = 0 Then
                        Dim i As Integer
                        .Begin_Trans()
                        For i = 1 To 3
                            SQL = " INSERT INTO [_etblPriceListPrices] " & _
                            " ([iPriceListNameID],[iStockID],[iWarehouseID],[bUseMarkup],[iMarkupOnCost],[fMarkupRate] " & _
                            " ,[fExclPrice],[fInclPrice],[dPLPricesTimeStamp]) " & _
                            "  Values( " & _
                            " " & i & "," & DS.Tables(0).Rows(0)(0) & ",0,0,0,0 " & _
                            " ,1,1,getdate()) "

                            Con2.Open()
                            CMD = New SqlCommand(SQL, Con2, Trans)
                            CMD.CommandType = CommandType.Text
                            CMD.ExecuteNonQuery()
                            Con2.Close()
                        Next
                        .Commit_Trans()
                    ElseIf DS1.Tables(0).Rows.Count <> 0 And DS1.Tables(0).Rows.Count < 3 Then

                        For i = 1 To 3

                            SQL = "SELECT  _etblPriceListPrices.IDPriceListPrices " & _
                            " FROM  _etblPriceListPrices WHERE  _etblPriceListPrices.iStockID ='" & DS.Tables(0).Rows(0)(0) & "' and iPriceListNameID = " & i
                            CMD = New SqlCommand(SQL, Con2)
                            CMD.CommandType = CommandType.Text
                            DA = New SqlDataAdapter(CMD)
                            DS1 = New DataSet
                            Con2.Open()
                            DA.Fill(DS1)
                            Con2.Close()

                             If DS1.Tables(0).Rows.Count = 0 Then
                                SQL = " INSERT INTO [_etblPriceListPrices] " & _
                                " ([iPriceListNameID],[iStockID],[iWarehouseID],[bUseMarkup],[iMarkupOnCost],[fMarkupRate] " & _
                                " ,[fExclPrice],[fInclPrice],[dPLPricesTimeStamp]) " & _
                                "  Values( " & _
                                " " & i & "," & DS.Tables(0).Rows(0)(0) & ",0,0,0,0 " & _
                                " ,1,1,getdate()) "

                                Con2.Open()
                                CMD = New SqlCommand(SQL, Con2, Trans)
                                CMD.CommandType = CommandType.Text
                                CMD.ExecuteNonQuery()
                                Con2.Close()
                            End If
                        Next

                    End If
                End With

            Next

            For Each Obj In aDataBase
                sDB = Obj.ToString
                'sDB = "dbUdawatta"
                Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & sDB & "")

                'SQL = "SELECT _etblPriceListPrices.IDPriceListPrices, _etblPriceListName.cName, _etblPriceListName.bDefault," & _
                '" _etblPriceListPrices.fExclPrice, _etblPriceListPrices.fInclPrice, StkItem.cSimpleCode " & _
                '" FROM _etblPriceListPrices INNER JOIN _etblPriceListName ON _etblPriceListPrices.iPriceListNameID = _etblPriceListName.IDPriceListName INNER JOIN" & _
                '" StkItem ON _etblPriceListPrices.iStockID = StkItem.StockLink WHERE StkItem.cSimpleCode ='" & cmbInventoryItem.Value & "' AND ItemActive = 1"

                SQL = " SELECT     _etblPriceListPrices.IDPriceListPrices, _etblPriceListName.cName, _etblPriceListName.bDefault, _etblPriceListPrices.fExclPrice, " & _
                "   _etblPriceListPrices.fInclPrice, StkItem.cSimpleCode, Spil_PriceList_History.dExcPrice1, Spil_PriceList_History.dIncPrice1, " & _
                "      Spil_PriceList_History.dExcPrice2, Spil_PriceList_History.dIncPrice2 " & _
                " FROM         _etblPriceListPrices INNER JOIN " & _
                "       _etblPriceListName ON _etblPriceListPrices.iPriceListNameID = _etblPriceListName.IDPriceListName INNER JOIN " & _
                "       StkItem ON _etblPriceListPrices.iStockID = StkItem.StockLink LEFT OUTER JOIN " & _
                "   Spil_PriceList_History ON _etblPriceListPrices.IDPriceListPrices = Spil_PriceList_History.idPriceListPrices " & _
                " WHERE StkItem.cSimpleCode ='" & cmbInventoryItem.Value & "' AND ItemActive = 1 ORDER BY _etblPriceListPrices.IDPriceListPrices"



                CMD = New SqlCommand(SQL, Con2)
                CMD.CommandType = CommandType.Text
                DA = New SqlDataAdapter(CMD)
                DS = New DataSet
                Con2.Open()
                DA.Fill(DS)
                Con2.Close()
                If DS.Tables(0).Rows.Count > 0 Then
                    ugR1 = UG.DisplayLayout.Bands(0).AddNew
                    ugR1.Cells("ID").Value = ugR1.Index + 1
                    ugR1.Cells("DatabaseName").Value = sDB
                    ugR1.Activated = True
                    For Each Dr In DS.Tables(0).Rows
                        If ugR1.ChildBands.HasChildRows = False Then
                            ugR2 = UG.ActiveRow.ChildBands(0).Band.AddNew
                            ugR2.Cells("ID").Value = ugR2.Index + 1
                            ugR2.Cells("IDPriceListPrices").Value = Dr("IDPriceListPrices")
                            ugR2.Cells("cName").Value = Dr("cName")
                            ugR2.Cells("bDefault").Value = Dr("bDefault")
                            ugR2.Cells("fExclPrice").Value = Math.Round(Dr("fExclPrice"), 2, MidpointRounding.AwayFromZero)
                            ugR2.Cells("fInclPrice").Value = Math.Round(Dr("fInclPrice"), 2, MidpointRounding.AwayFromZero)
                            ugR2.Cells("cSimpleCode").Value = Dr("cSimpleCode")
                            ugR2.Cells("dExcPrice1").Value = Dr("dExcPrice1")
                            ugR2.Cells("dIncPrice1").Value = Dr("dIncPrice1")
                            ugR2.Cells("dExcPrice2").Value = Dr("dExcPrice2")
                            ugR2.Cells("dIncPrice2").Value = Dr("dIncPrice2")
                        Else
                            ugR2 = ugR1.ChildBands(0).Band.AddNew
                            ugR2.Cells("ID").Value = ugR2.Index + 1
                            ugR2.Cells("IDPriceListPrices").Value = Dr("IDPriceListPrices")
                            ugR2.Cells("cName").Value = Dr("cName")
                            ugR2.Cells("bDefault").Value = Dr("bDefault")
                            ugR2.Cells("fExclPrice").Value = Math.Round(Dr("fExclPrice"), 2, MidpointRounding.AwayFromZero)
                            ugR2.Cells("fInclPrice").Value = Math.Round(Dr("fInclPrice"), 2, MidpointRounding.AwayFromZero)
                            ugR2.Cells("cSimpleCode").Value = Dr("cSimpleCode")
                            ugR2.Cells("dExcPrice1").Value = Dr("dExcPrice1")
                            ugR2.Cells("dIncPrice1").Value = Dr("dIncPrice1")
                            ugR2.Cells("dExcPrice2").Value = Dr("dExcPrice2")
                            ugR2.Cells("dIncPrice2").Value = Dr("dIncPrice2")
                        End If
                    Next
                Else



                    'MsgBox(cmbInventoryItem.Value & " can not find from database " & sDB & vbLf & "Please check item code from that database before changes update", MsgBoxStyle.Exclamation, "Pastel Evolution")
                    'Exit Sub
                End If
            Next

            MsgBox("Item Load Completed", MsgBoxStyle.Information, "Pastel Evolution")
        Catch ex As Exception
            MsgBox("Error found while loading item from databases", MsgBoxStyle.Exclamation, "Pastel Evolution")
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
        Finally
            DS.Dispose()
            DA.Dispose()
            CMD.Dispose()
        End Try


        cmbGroup.Value = cmbInventoryItem.ActiveRow.Cells("ItemGroup").Value
        

    End Sub
    Private Sub cmbInventoryItem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbInventoryItem.Leave
        Try
            If cmbInventoryItem.ActiveRow.Selected = True Then
                txtCode.Text = cmbInventoryItem.ActiveRow.Cells("Code").Value
                txtSimpleCode.Text = cmbInventoryItem.ActiveRow.Cells("SimpleCode").Value
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Private Function GetPriceExcl(ByVal InclPrice As Double) As Double
        Dim ExclPrice As Double = 0
        ExclPrice = Math.Round(InclPrice * 100 / 112, 2, MidpointRounding.AwayFromZero)
        Return ExclPrice
    End Function
    Private Function GetPriceIncl(ByVal ExclPrice As Double) As Double
        Dim InclPrice As Double = 0
        InclPrice = Math.Round(ExclPrice + ExclPrice * 12 / 100, 2, MidpointRounding.AwayFromZero)
        Return InclPrice
    End Function
    Private Sub UG_BeforeExitEditMode(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs) Handles UG.BeforeExitEditMode
        Try
            If UG.ActiveCell.Column.Key = "fInclPrice" Then
                UG.ActiveCell.Value = UG.ActiveCell.Text
                UG.ActiveCell.Row.Cells("fExclPrice").Value = GetPriceExcl(UG.ActiveCell.Value)
            ElseIf UG.ActiveCell.Column.Key = "fExclPrice" Then
                UG.ActiveCell.Value = UG.ActiveCell.Text
                UG.ActiveCell.Row.Cells("fInclPrice").Value = GetPriceIncl(UG.ActiveCell.Value)
            End If

          


        Catch ex As Exception
            Exit Sub
        End Try
    End Sub


    Private Sub tsbUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbUpdate.Click
        Try

            UG.PerformAction(UltraGridAction.CommitRow)
            If cmbInventoryItem.Value.ToString.Trim.Length = 0 Then
                MsgBox("Select Item Code", MsgBoxStyle.Exclamation, "Pastel Evoltuion")
                Exit Sub
            End If

            Dim ugR1 As UltraGridRow
            Dim ugR2 As UltraGridRow
            Dim sDB As String

            'For Each Obj In aDataBase
            '    sDB = Obj.ToString

            For Each ugR1 In UG.Rows
                If ugR1.ChildBands.HasChildRows = True Then
                    For Each ugR2 In ugR1.ChildBands(0).Rows
                        For Each ugR3 In UG.Rows
                            If ugR3.ChildBands.HasChildRows = True Then
                                For Each ugR4 In ugR3.ChildBands(0).Rows
                                    If ugR2.Cells("ID").Value = ugR4.Cells("ID").Value Then

                                        sDB = ugR3.Cells("DatabaseName").Value.ToString.Trim

                                        Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & sDB & "")




                                        'Updating Pricelist History-----------------------------------------------------------------------

                                        SQL = " SELECT dExcPrice1 FROM Spil_PriceList_History WHERE idPriceListPrices = " & ugR4.Cells("IDPriceListPrices").Value & ""
                                        CMD = New SqlCommand(SQL, Con2)
                                        CMD.CommandType = CommandType.Text
                                        DA = New SqlDataAdapter(CMD)
                                        DS = New DataSet
                                        Con2.Open()
                                        DA.Fill(DS)
                                        Con2.Close()
                                        If DS.Tables(0).Rows.Count > 0 Then
                                            SQL = "UPDATE Spil_PriceList_History SET dExcPrice2 = dExcPrice1, " & _
                                            " dIncPrice2 = dIncPrice1 WHERE IDPriceListPrices =" & ugR4.Cells("IDPriceListPrices").Value & ""

                                            CMD = New SqlCommand(SQL, Con2)
                                            CMD.CommandType = CommandType.Text
                                            Con2.Open()
                                            CMD.ExecuteNonQuery()
                                            Con2.Close()

                                        Else

                                            SQL = "INSERT INTO Spil_PriceList_History(idPriceListPrices,dExcPrice1,dIncPrice1,dExcPrice2,dIncPrice2)" & _
                                            " VALUES (" & ugR4.Cells("IDPriceListPrices").Value & ", 0, 0, 0, 0)"
                                            CMD = New SqlCommand(SQL, Con2)
                                            CMD.CommandType = CommandType.Text
                                            Con2.Open()
                                            CMD.ExecuteNonQuery()
                                            Con2.Close()

                                          
                                        End If




                                    End If
                                Next
                            End If
                        Next
                    Next
                    Exit For
                End If
                'Exit For
            Next




            'Updating Pricelist History-----------------------------------------------------------------------


            For Each ugR1 In UG.Rows
                If ugR1.ChildBands.HasChildRows = True Then
                    For Each ugR2 In ugR1.ChildBands(0).Rows
                        For Each ugR3 In UG.Rows
                            If ugR3.ChildBands.HasChildRows = True Then
                                For Each ugR4 In ugR3.ChildBands(0).Rows
                                    If ugR2.Cells("ID").Value = ugR4.Cells("ID").Value Then
                                        
                                        
                                        sDB = ugR3.Cells("DatabaseName").Value.ToString.Trim

                                        Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & sDB & "")


                                      


                                        SQL = " SELECT dExcPrice1 FROM Spil_PriceList_History WHERE idPriceListPrices = " & ugR4.Cells("IDPriceListPrices").Value & ""

                                        CMD = New SqlCommand(SQL, Con2)
                                        CMD.CommandType = CommandType.Text
                                        DA = New SqlDataAdapter(CMD)
                                        DS = New DataSet
                                        Con2.Open()
                                        DA.Fill(DS)
                                        Con2.Close()
                                        If DS.Tables(0).Rows.Count > 0 Then
                                            

                                            SQL = "UPDATE Spil_PriceList_History SET dExcPrice1 = (SELECT fExclPrice FROM _etblPriceListPrices WHERE IDPriceListPrices =" & ugR4.Cells("IDPriceListPrices").Value & ") ," & _
                                            " dIncPrice1 = (SELECT fInclPrice FROM _etblPriceListPrices WHERE IDPriceListPrices =" & ugR4.Cells("IDPriceListPrices").Value & ") WHERE IDPriceListPrices =" & ugR4.Cells("IDPriceListPrices").Value & ""
                                            CMD = New SqlCommand(SQL, Con2)
                                            CMD.CommandType = CommandType.Text
                                            Con2.Open()
                                            CMD.ExecuteNonQuery()
                                            Con2.Close()

                                        Else

                                            SQL = "INSERT INTO Spil_PriceList_History(idPriceListPrices,dExcPrice1,dIncPrice1,dExcPrice2,dIncPrice2)" & _
                                            " VALUES (" & ugR4.Cells("IDPriceListPrices").Value & ", 0, 0, 0, 0)"
                                            CMD = New SqlCommand(SQL, Con2)
                                            CMD.CommandType = CommandType.Text
                                            Con2.Open()
                                            CMD.ExecuteNonQuery()
                                            Con2.Close()

                                        
                                        End If



                                    End If
                                Next
                            End If
                        Next
                    Next
                    Exit For

                    'end of update history----------------------------------------------------------------------


                End If
                'Exit For
            Next
            'Next


            '------------------------------------------------------------------------------------------------------------------------------------


            For Each ugR1 In UG.Rows
                If ugR1.ChildBands.HasChildRows = True Then
                    For Each ugR2 In ugR1.ChildBands(0).Rows
                        For Each ugR3 In UG.Rows
                            If ugR3.ChildBands.HasChildRows = True Then
                                For Each ugR4 In ugR3.ChildBands(0).Rows
                                    If ugR2.Cells("ID").Value = ugR4.Cells("ID").Value Then
                                        

                                        sDB = ugR3.Cells("DatabaseName").Value.ToString.Trim

                                        Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & sDB & "")
                                        SQL = "UPDATE _etblPriceListPrices SET fExclPrice =" & ugR2.Cells("fExclPrice").Value & "," & _
                                        " fInclPrice =" & ugR2.Cells("fInclPrice").Value & " WHERE IDPriceListPrices =" & ugR4.Cells("IDPriceListPrices").Value & "" & _
                                        " UPDATE StkItem set ItemGroup = '" & cmbGroup.Value & "' WHERE cSimpleCode = '" & cmbInventoryItem.Value & "' and ItemActive = 1 "
                                        CMD = New SqlCommand(SQL, Con2)
                                        CMD.CommandType = CommandType.Text
                                        Con2.Open()
                                        CMD.ExecuteNonQuery()
                                        Con2.Close()




                                    End If
                                Next
                            End If
                        Next
                    Next
                    Exit For
                End If
                'Exit For
            Next

            MsgBox("All Databases are updated Successfully", MsgBoxStyle.Information, "Pastel Evolution")


            'cmbInventoryItem.ResetText()
            'txtCode.ResetText()
            'txtSimpleCode.ResetText()
            'For Each ugR1 In UG.Rows.All
            '    ugR1.Delete(False)
            'Next


            tsbConnect_Click(Me, e)



        Catch ex As Exception
            MsgBox("Error found while updating data", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End Try
    End Sub

    Private Sub tsbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
    End Sub

    Private Sub UG_BeforeGroupPosChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.BeforeGroupPosChangedEventArgs) Handles UG.BeforeGroupPosChanged

    End Sub

    Private Sub UG_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UG.InitializeLayout

    End Sub

    Private Sub UG_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles UG.KeyUp
        Select Case e.KeyCode

            Case 37 'Left
                UG.PerformAction(UltraGridAction.ExitEditMode)
                UG.PerformAction(UltraGridAction.PrevCell)
                UG.PerformAction(UltraGridAction.EnterEditMode)
            Case 38 'Up
                'UG.PerformAction(UltraGridAction.ExitEditMode)
                UG.PerformAction(UltraGridAction.AboveCell)
                UG.PerformAction(UltraGridAction.EnterEditMode)
            Case 39 'Right
                UG.PerformAction(UltraGridAction.ExitEditMode)
                UG.PerformAction(UltraGridAction.NextCell)
                UG.PerformAction(UltraGridAction.EnterEditMode)
            Case 40 'Down
                UG.PerformAction(UltraGridAction.ExitEditMode)
                UG.PerformAction(UltraGridAction.BelowCell)
                UG.PerformAction(UltraGridAction.EnterEditMode)
            Case 13 'Enter
                'UG.PerformAction(UltraGridAction.ExitEditMode)
                UG.PerformAction(UltraGridAction.BelowCell)
                UG.PerformAction(UltraGridAction.EnterEditMode)
        End Select
    End Sub

  
    Private Sub cmbInventoryItem_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles cmbInventoryItem.InitializeLayout

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim sDB As String
        For Each Obj In aDataBase

            sDB = Obj.ToString


            Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & sDB & "")
            SQL = " set dateformat dmy update _etblLotTracking set dExpiryDate = '" & dtTo.Value & "'  Where dExpiryDate < '" & dtFrom.Value & "' "
            CMD = New SqlCommand(SQL, Con2)
            CMD.CommandType = CommandType.Text
            Con2.Open()
            CMD.ExecuteNonQuery()
            Con2.Close()
        Next
        MessageBox.Show("Successfully Updated", "Pastel Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoUpdate.Click

        Dim Item As Integer = 0
        Dim Line As Integer = 0
        Dim prvStk As Integer = 0


        Dim objSQL As New clsSqlConn
        With objSQL


            SQL = " SELECT DISTINCT ztable.[Part No], ztable.[System No], ztable.Cost, ztable.Price, StkItem.StockLink, StkItem.cSimpleCode " & _
            " FROM         StkItem RIGHT OUTER JOIN " & _
            "                      ztable ON StkItem.Description_2 = ztable.[System No] "


            'SQL = " select WHStockLink from WhseStk where WHQtyOnHand > 0 and WHWhseID = 18 "

            'SQL = " SELECT DISTINCT " & _
            '"  _etblLotTracking.iStockID, WhseStk.WHStockLink, SUM(_etblLotTrackingQty.fQtyOnHand) AS LotQ,  WhseStk.WHQtyOnHand AS WHQ, " & _
            '"  MIN(_etblLotTrackingQty.iLotTrackingID) AS lot " & _
            '" FROM         WhseStk INNER JOIN " & _
            '"  _etblLotTracking ON WhseStk.WHStockLink = _etblLotTracking.iStockID INNER JOIN " & _
            '"  _etblLotTrackingQty ON _etblLotTracking.idLotTracking = _etblLotTrackingQty.iLotTrackingID AND " & _
            '"  WhseStk.WHWhseID = _etblLotTrackingQty.iWarehouseID " & _
            '" GROUP BY _etblLotTracking.iStockID, WhseStk.WHStockLink, WhseStk.WHQtyOnHand " & _
            '" ORDER BY _etblLotTracking.iStockID "

            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
            If DS.Tables(0).Rows.Count > 0 Then
                Dim dr As DataRow
                .Begin_Trans()
                For Each dr In DS.Tables(0).Rows

                    'SQL = " SELECT _btblInvoiceLines.iStockCodeID, StkItem.SerialItem " & _
                    '" FROM _btblInvoiceLines INNER JOIN " & _
                    '" StkItem ON _btblInvoiceLines.iStockCodeID = StkItem.StockLink where iInvoiceID = " & dr("AutoIndex") & ""
                    'SQL = " SELECT     StkItem.cSimpleCode, dbUdawatta.dbo._etblPriceListPrices.fInclPrice, dbUdawatta.dbo._etblPriceListPrices.fExclPrice " & _
                    '" FROM  dbUdawatta.dbo._etblPriceListPrices RIGHT OUTER JOIN " & _
                    '"                      StkItem ON dbUdawatta.dbo._etblPriceListPrices.iStockID = StkItem.StockLink  where StkItem.ItemActive = 1"

                    'DS1 = New DataSet
                    'DS1 = .Get_Data_Sql(SQL)
                    'If DS1.Tables(0).Rows.Count > 0 Then
                    '    Dim dr1 As DataRow
                    '    For Each dr1 In DS1.Tables(0).Rows
                    'Line += 1
                    'SQL = " Update sbSerialMF Set PrimaryLineID = " & Line & " Where AutoIndex = " & dr("AutoIndex") & " and SNStockLink = " & dr1("iStockCodeID") & "  "
                    'If dr1("ItemGroup") <> "" Then

                    'SQL = " SELECT StockLink from StkItem where csimplecode = '" & dr1("csimplecode") & "' "
                    'DS = New DataSet
                    'DS = .Get_Data_Sql(SQL)

                    'If DS.Tables(0).Rows(0)(0) <> "" Then
                    'Dim sa As Integer
                    'sa = 0


                    'SQL = " SELECT  StockLink, Description_2 FROM   StkItem WHERE StockLink = " & dr("StockLink") & " "
                    'DS3 = New DataSet
                    'DS3 = .Get_Data_Sql(SQL)

                    'If DS3.Tables(0).Rows.Count > 0 Then
                    '    Dim s As Integer
                    '    For Each dr3 In DS3.Tables(0).Rows
                    'SQL = " Update StkItem Set ItemGroup = '" & dr3("ItemGroup")  & "' where  StockLink = '" & dr3("StockLink") & "'"
                    'If dr("LotQ") < dr("WHQ") Then
                    'SQL = " update _etblLotTrackingQty set fQtyOnHand = '" & dr("WHQ")  & "' where  iLotTrackingID =  '" & dr("lot") & "'  "

                    If IsDBNull(dr("Price")) Or IsDBNull(dr("StockLink")) Then
                    Else
                        'SQL = " update StkItem set AveUCst = " & dr("Cost") & "  where StockLink = " & dr("StockLink") & " "
                        SQL = " update _etblPriceListPrices set fExclPrice = " & dr("Price") & ", fInclPrice = " & dr("Price") & "  where iStockID = " & dr("StockLink") & " "

                        SQL = SQL + " update dbNawinna_new.dbo._etblPriceListPrices set fExclPrice = " & dr("Price") & " , fInclPrice =  " & dr("Price") & "   where iStockID = (SELECT  distinct   iStockID FROM         dbNawinna_new.dbo.StkItem INNER JOIN         dbNawinna_new.dbo._etblPriceListPrices ON dbNawinna_new.dbo.StkItem.StockLink = dbNawinna_new.dbo._etblPriceListPrices.iStockID        where StkItem.cSimpleCode = '" & dr("cSimpleCode") & "')"

                        'SQL = SQL + " update dbUdawatta_new.dbo._etblPriceListPrices set fExclPrice = " & dr("Price") & " , fInclPrice =  " & dr("Price") & "   where iStockID = (SELECT  distinct   iStockID  FROM         dbUdawatta_new.dbo.StkItem INNER JOIN     dbUdawatta_new.dbo._etblPriceListPrices ON dbUdawatta_new.dbo.StkItem.StockLink = dbUdawatta_new.dbo._etblPriceListPrices.iStockID         where StkItem.cSimpleCode = '" & dr("cSimpleCode") & "') "


                        'SQL = " update _etblPriceListPrices set fMarkupRate = 25, bUseMarkup = 1, fInclPrice = (" & dr("AveUCst") & " * .25) + " & dr("AveUCst") & ", fExclPrice = (" & dr("AveUCst") & " * .25) + " & dr("AveUCst") & "  where _etblPriceListPrices.iStockID =  '" & dr("StockLink") & "' "
                        'SQL = " delete from _etblInvJrBatchLines where iStockID = " & dr("WHStockLink") & " and iInvJrBatchID = 23 "

                        .Execute_Sql_Trans(SQL)

                        'Else
                        'SQL = " update _etblPriceListPrices set fExclPrice = '" & dr("fExclPrice") & "', fInclPrice = '" & dr("fInclPrice") & "' where  iStockID =  ( select  _etblPriceListPrices.iStockID FROM         StkItem INNER JOIN _etblPriceListPrices ON StkItem.StockLink = _etblPriceListPrices.iStockID where StkItem.cSimpleCode = '" & dr("cSimpleCode") & "' AND iPriceListNameID = 3  ) and iPriceListNameID = 3  "

                        '    .Execute_Sql_Trans(SQL)
                    End If


                    'End If

                    's = s + 1
                    '    Next
                    'Else
                    'For s As Integer = 1 To 3
                    '    If IsDBNull(dr1("fExclPrice")) Then
                    '    Else
                    '        SQL = " insert into _etblPriceListPrices (iPriceListNameID,[iStockID]  ,[bUseMarkup]  ,[iMarkupOnCost]   ,[fMarkupRate]  ,[fExclPrice] ,[fInclPrice]) values (" & s & ",'" & DS.Tables(0).Rows(0)(0) & "', 0,0,0, " & dr1("fExclPrice") & ", " & dr1("fInclPrice") & " ) "
                    '        .Begin_Trans()
                    '        .Get_Data_Sql_Trans(SQL)
                    '        .Commit_Trans()
                    '        s = s + 1
                    '    End If
                    'Next
                    'End If
                    'End If
                Next
                .Commit_Trans()
                'Line = 0
            End If
            'Next
            'End If
        End With

    End Sub
End Class