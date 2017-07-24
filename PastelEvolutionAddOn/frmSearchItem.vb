Public Class frmSearchItem
    Public iDocType As Integer
    Private Sub frmSearchItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtSearch.Focus()

    End Sub

    Private Sub tsbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
    End Sub

    Private Sub tsbConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbConnect.Click
        Fill_Data()
        Try
            UG.DisplayLayout.LoadFromXml(Application.StartupPath & "\" & sAgent & "SEARCH.xml")
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Private Sub Fill_Data()

        Dim objSQL As New clsSqlConn

        With objSQL

            'SQL = " SELECT DISTINCT StockLink, Code, Description_1, Description_2, Description_3, ItemGroup, Qty_On_Hand, QtyOnPO, QtyOnSO, SerialItem, cRevision, " & _
            '" cComponent, cExtDescription, cSimpleCode, ItemGroupDescription, PackDescription, BinLocationName, Bin, QtyInStock, cInvSegValue1Value as Part_No, " & _
            '" cInvSegValue2Value as Description, cInvSegValue3Value as Model, cInvSegValue4Value as Brand, cInvSegValue5Value as Genuine, " & _
            '" cInvSegValue6Value as Group, cInvSegValue7Value as Purchase_type, DefaultPriceIncl " & _

            'SQL = " SELECT DISTINCT StockLink,  Description_1 ,  cInvSegValue1Value as Part_No, " & _
            '" cInvSegValue2Value as Description, cInvSegValue3Value as Model, cInvSegValue4Value as Brand, ItemGroup, Qty_On_Hand,Re_Ord_Lvl, Re_Ord_Qty,  QtyOnPO, BinLocationName, " & _
            '" DefaultPriceIncl, ItemActive    " & _
            '" FROM _bvStockFull " & _

            'If sSQLSrvDataBase = "dbUdawatta_new" Then
            'SQL = " SELECT DISTINCT p.cSimpleCode,  p.Description_1 , p.Description_2, p.Description_3,  p.cInvSegValue1Value as Part_No, " & _
            '" p.cInvSegValue2Value as Description, p.cInvSegValue3Value as Model, p.cInvSegValue4Value as Brand, p.ItemGroup, p.Qty_On_Hand, k.Qty_On_Hand as KEL_Qty, p.BinLocationName, " & _
            '" p.DefaultPriceIncl, p.ItemActive    " & _
            '" FROM _bvStockFull p LEFT OUTER JOIN dbKelaniya_new.dbo._bvStockFull k on p.StockLink = k.StockLink "
            'Else
            'SQL = " SELECT DISTINCT p.cSimpleCode,  p.Description_1 , p.Description_2, p.Description_3,  p.cInvSegValue1Value as Part_No, " & _
            '" p.cInvSegValue2Value as Description, p.cInvSegValue3Value as Model, p.cInvSegValue4Value as Brand, p.ItemGroup, p.Qty_On_Hand, k.Qty_On_Hand as KEL_Qty , u.Qty_On_Hand as PKW_Qty, p.Max_Lvl as MaxLvl, p.BinLocationName, " & _
            '" p.DefaultPriceIncl, p.ItemActive, p.Code " & _
            '" FROM _bvStockFull p LEFT OUTER JOIN dbKelaniya_new.dbo._bvStockFull k on p.cSimpleCode = k.cSimpleCode " & _
            '" LEFT OUTER JOIN dbUdawatta_new.dbo._bvStockFull u on p.cSimpleCode = u.cSimpleCode "
            ''End If

            SQL = " SELECT DISTINCT " & _
            " p.cSimpleCode, p.Description_1, p.Description_2, p.Description_3, p.cInvSegValue1Value AS Part_No, p.cInvSegValue2Value AS Description, " & _
            " p.cInvSegValue3Value AS Model, p.cInvSegValue4Value AS Brand, p.ItemGroup, u.Qty_On_Hand AS MOT_Qty, k.Qty_On_Hand AS KEL_Qty, n.Qty_On_Hand AS ATM_Qty, " & _
            " p.Max_Lvl AS MaxLvl, p.BinLocationName, p.DefaultPriceIncl, p.ucIICategory, p.ItemActive, p.Code " & _
            " FROM         dbUdawatta_new.dbo._bvStockFull AS p " & _
            " LEFT OUTER JOIN dbKelaniya_new.dbo._bvStockFull AS k ON p.cSimpleCode = k.cSimpleCode " & _
            " LEFT OUTER JOIN dbUdawatta_new.dbo._bvStockFull AS u ON p.cSimpleCode = u.cSimpleCode " & _
            " LEFT OUTER JOIN dbNawinna_new.dbo._bvStockFull AS n ON p.cSimpleCode = n.cSimpleCode "


            ''show warehouses as lines not columns
            'SQL = " SELECT DISTINCT " & _
            '         " p.cSimpleCode, p.Description_1, p.Description_2, p.Description_3, p.cInvSegValue1Value AS Part_No, p.cInvSegValue2Value AS Description,  " & _
            '         " p.cInvSegValue3Value AS Model, p.cInvSegValue4Value AS Brand, p.ItemGroup, k.Qty_On_Hand AS KEL_Qty, p.Max_Lvl AS MaxLvl, p.BinLocationName, " & _
            '         " p.DefaultPriceIncl, p.ucIICategory, p.ItemActive, p.Code, PKW.WHQtyOnHand AS PKW, PKWWM.Name AS Name1 " & _
            '         " FROM         dbKelaniya_new.dbo._bvStockFull AS k LEFT OUTER JOIN " & _
            '         " dbUdawatta_new.dbo.WhseMst AS PKWWM INNER JOIN " & _
            '         " dbUdawatta_new.dbo._bvStockFull AS p INNER JOIN " & _
            '         " dbUdawatta_new.dbo.WhseStk AS PKW ON p.StockLink = PKW.WHStockLink ON PKWWM.WhseLink = PKW.WHWhseID ON k.cSimpleCode = p.cSimpleCode " & _
            '         " WHERE     ((PKW.WHWhseID = 2) OR " & _
            '         " (PKW.WHWhseID = 4) OR " & _
            '         "(PKW.WHWhseID = 18)) "





            SQL = SQL + " WHERE (p.Code like '%" & txtSearch.Text & "%' OR " & _
            " p.Description_1 like '%" & txtSearch.Text & "%' OR " & _
            " p.Description_2  like '%" & txtSearch.Text & "%' OR " & _
            " p.Description_3 like '%" & txtSearch.Text & "%' OR " & _
            " p.ItemGroup like '%" & txtSearch.Text & "%' OR " & _
            " p.cSimpleCode like '%" & txtSearch.Text & "%' OR " & _
            " p.ItemGroupDescription like '%" & txtSearch.Text & "%' OR " & _
            " p.PackDescription like '%" & txtSearch.Text & "%' OR " & _
            " p.BinLocationName like '%" & txtSearch.Text & "%' OR " & _
            " p.Bin like '%" & txtSearch.Text & "%' OR " & _
            " p.cInvSegValue1Value like '%" & txtSearch.Text & "%' OR " & _
            " p.cInvSegValue2Value like '%" & txtSearch.Text & "%' OR " & _
            " p.cInvSegValue3Value like '%" & txtSearch.Text & "%' OR " & _
            " p.cInvSegValue4Value like '%" & txtSearch.Text & "%' OR " & _
            " p.cInvSegValue5Value like '%" & txtSearch.Text & "%' OR " & _
            " p.cInvSegValue6Value like '%" & txtSearch.Text & "%' OR " & _
            " p.cInvSegValue7Value like '%" & txtSearch.Text & "%') "

            DS = New DataSet
            DS = .Get_Data_Sql(SQL)

            'UG.DataSource = Nothing

            'UG.DataSource = DS.Tables(0)
            'UG.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.ExternalSortSingle


            'If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sAgent & "SEARCH.xml") Then
            '    UG.DisplayLayout.LoadFromXml(Application.StartupPath & "\" & sAgent & "SEARCH.xml")
            'End If

            UG.DataSource = DS.Tables(0)
            UG.DisplayLayout.Bands(0).Columns("Code").Hidden = True
            'UG.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.ExternalSortSingle

            'If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sAgent & "SEARCH.xml") Then
            '    UG1.DisplayLayout.LoadFromXml(Application.StartupPath & "\" & sAgent & "SEARCH.xml")
            'End If

        End With

    End Sub

    Private Sub tsbSaveMySetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSaveMySetting.Click
        Try
            UG.DisplayLayout.SaveAsXml(Application.StartupPath & "\" & sAgent & "SEARCH.xml")
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        Try
            If e.KeyValue = 13 Then
                Fill_Data()
                Try
                    UG.DisplayLayout.LoadFromXml(Application.StartupPath & "\" & sAgent & "SEARCH.xml")
                Catch ex As Exception
                    Exit Sub
                End Try
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtSearch_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.ValueChanged

    End Sub

    Private Sub tsbBin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBin.Click
        If UG.Selected.Rows.Count = 0 Then Exit Sub

        If UG.ActiveRow.Selected = True Then
            With frmUpdateBin
                .txtSimpleCode.Text = UG.ActiveRow.Cells("Code").Value
                .ShowDialog()
            End With
        End If
    End Sub

    Private Sub tsbCat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbCat.Click
        If UG.Selected.Rows.Count = 0 Then Exit Sub

        If UG.ActiveRow.Selected = True Then
            With frmUpdateCat
                .txtSimpleCode.Text = UG.ActiveRow.Cells("Code").Value
                .ShowDialog()
            End With
        End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class