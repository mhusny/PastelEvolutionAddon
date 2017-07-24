Imports Excel
Imports System.IO
Imports System.Data
Imports Infragistics.Win.UltraWinGrid
Imports System.Data.OleDb

Public Class frmPriceCostUpddate
    Public DtSet As System.Data.DataSet
    Private Sub frmPriceCostUpddate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim sr As New System.IO.StreamReader(OpenFileDialog1.FileName)
            UltraLabel1.Text = OpenFileDialog1.FileName.ToString()
            sr.Close()
        End If

        Dim MyConnection As System.Data.OleDb.OleDbConnection
        'Dim DtSet As System.Data.DataSet
        Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
        MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & OpenFileDialog1.FileName & "';Extended Properties='Excel 8.0';")
        MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]", MyConnection)
        MyCommand.TableMappings.Add("Table", "Net-informations.com")
        DtSet = New System.Data.DataSet
        MyCommand.Fill(DtSet)
        UG.DataSource = DtSet.Tables(0)
        MyConnection.Close()



        'Dim CnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & OpenFileDialog1.FileName & ";Extended Properties=""text;HDR=No;FMT=Delimited"";"
        'Dim dt As New DataTable
        'Using Adp As New OleDbDataAdapter("select * from [nos.csv]", CnStr)
        '    Adp.Fill(dt)
        'End Using
        'UG.DataSource = dt

    End Sub

    Private Sub UltraButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraButton1.Click


        Dim objSQL As New clsSqlConn
        With objSQL
            .Begin_Trans()
            For Each dr In DtSet.Tables(0).Rows

                SQL = " update _etblPriceListPrices set fExclPrice = " & dr("Price") & ", fInclPrice = " & dr("Price") & " , _etblPriceListPrices_iBranchID = 1  where iStockID = (SELECT  distinct   iStockID  FROM        StkItem INNER JOIN    _etblPriceListPrices ON StkItem.StockLink = _etblPriceListPrices.iStockID         where StkItem.Description_1 = '" & dr("code") & "') "

                SQL = SQL + " update dbUdawatta_new.dbo._etblPriceListPrices set fExclPrice = " & dr("Price") & " , fInclPrice =  " & dr("Price") & " , _etblPriceListPrices_iBranchID = 1   where iStockID = (SELECT  distinct   iStockID  FROM         dbUdawatta_new.dbo.StkItem INNER JOIN     dbUdawatta_new.dbo._etblPriceListPrices ON dbUdawatta_new.dbo.StkItem.StockLink = dbUdawatta_new.dbo._etblPriceListPrices.iStockID         where StkItem.Description_1 = '" & dr("code") & "') "
                SQL = SQL + " update dbNawinna_new.dbo._etblPriceListPrices set fExclPrice = " & dr("Price") & " , fInclPrice =  " & dr("Price") & " , _etblPriceListPrices_iBranchID = 1   where iStockID = (SELECT  distinct   iStockID  FROM         dbNawinna_new.dbo.StkItem INNER JOIN     dbNawinna_new.dbo._etblPriceListPrices ON dbNawinna_new.dbo.StkItem.StockLink = dbNawinna_new.dbo._etblPriceListPrices.iStockID         where StkItem.Description_1 = '" & dr("code") & "') "

                ' SQL = SQL + " update StkItem set AveUCst = " & dr("Cost") & "  where Description_1 = '" & dr("code") & "' "

                .Execute_Sql_Trans(SQL)

            Next
            .Commit_Trans()
            MsgBox("Successfully Updated", MsgBoxStyle.Information, "Evolution")
        End With

    End Sub
End Class