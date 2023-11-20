﻿Public Class Print1

    Public series1 As String = ""
    Private q As New qry
    Private Sub Print1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set the ReportViewer to display in PrintLayout mode
        ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)

        ' Set the page settings for portrait orientation with 0.5-inch margins
        Dim pageSettings As New System.Drawing.Printing.PageSettings()
        pageSettings.Landscape = False ' Set to True for landscape orientation
        pageSettings.Margins = New System.Drawing.Printing.Margins(25, 25, 25, 25) ' 0.5 inches on all sides
        ReportViewer1.SetPageSettings(pageSettings)

        ReportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent
        ReportViewer1.ZoomPercent = 100
        ' Optionally, you can refresh the report if needed
        ReportViewer1.RefreshReport()

    End Sub

    Private Sub ReportViewer1_Print(sender As Object, e As Microsoft.Reporting.WinForms.ReportPrintEventArgs) Handles ReportViewer1.Print
        q.addrecvseries(series1)
    End Sub
End Class