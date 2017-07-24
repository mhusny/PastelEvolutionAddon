Imports Infragistics.Win.UltraWinGrid
Imports WolfSoftware.Library_NET
'Imports Lesnikowski.Barcode
Imports System.Drawing
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing.Imaging.ImageFormat
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmAuditRpt
    

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        iRepoID = 1
        frmPrintPreview.ShowDialog()
        iRepoID = 0
    End Sub

    Private Sub frmAuditRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GET_DATA()
    End Sub
    Public Sub GET_DATA()
       
        SQL = " SELECT DCLink, Name FROM Client Order By Name"
        'SQL += " SELECT ProjectLink, ProjectCode, ProjectName,ProjectDescription FROM Project Order By projectName"
        'SQL += " SELECT StatusCounter, StatusDescrip FROM OrdersSt"
        'SQL += " SELECT idIncidentPriority, cDescription FROM _rtblIncidentPriority"
        'SQL += " SELECT StockLink, Code, Description_1, ItemGroup, SerialItem,WhseItem,Description_2 FROM StkItem Order By Description_1"
        'SQL += " SELECT WhseLink, Code, Name, KnownAs FROM WhseMst"
        'SQL += " SELECT idTaxRate, Code, Description, TaxRate FROM TaxRate"
        'SQL += " SELECT bIsInclusive FROM StDfTbl"
        'SQL += " SELECT WhseLink FROM WhseMst WHERE DefaultWhse = 1"
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                'dsManu = .Get_Data_Sql(SQL)
                cmbAgent.DataSource = DS.Tables(0)
                cmbAgent.ValueMember = "DCLink"
                cmbAgent.DisplayMember = "Name"
                cmbAgent.DisplayLayout.Bands(0).Columns("DCLink").Hidden = True
                'cmbAgent.DisplayLayout.Bands(0).Columns("Physical1").Hidden = True
                'cmbAgent.DisplayLayout.Bands(0).Columns("Physical2").Hidden = True
                'cmbAgent.DisplayLayout.Bands(0).Columns("Physical3").Hidden = True
                'cmbAgent.DisplayLayout.Bands(0).Columns("Post1").Hidden = True
                'cmbAgent.DisplayLayout.Bands(0).Columns("Post2").Hidden = True
                'cmbAgent.DisplayLayout.Bands(0).Columns("Post3").Hidden = True

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                Exit Sub
            Finally
                objSQL = Nothing
            End Try
        End With

    End Sub

    Private Sub UltraButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraButton1.Click
        Me.Close()
    End Sub
End Class