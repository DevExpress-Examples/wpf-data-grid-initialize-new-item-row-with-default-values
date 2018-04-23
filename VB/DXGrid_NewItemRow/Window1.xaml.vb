Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Wpf.Grid
Imports System.Data

Namespace DXGrid_NewItemRow
	Partial Public Class Window1
		Inherits Window
		Public Sub New()
			InitializeComponent()
			grid.DataSource = _
				New nwindDataSetTableAdapters.ProductsTableAdapter().GetData().DefaultView
		End Sub

		Private Sub TableView_InitNewRow(ByVal sender As Object, _
			ByVal e As DevExpress.Wpf.Grid.InitNewRowEventArgs)
				grid.SetCellValue(e.RowHandle, "UnitPrice", 10)
				grid.SetCellValue(e.RowHandle, "Discontinued", False)
				grid.SetCellValue(e.RowHandle, "UnitsOnOrder", 1)
		End Sub

		Private Sub TableView_ValidateRow(ByVal sender As Object, _
				ByVal e As DevExpress.Wpf.Grid.GridRowValidationEventArgs)
				If e.Row Is Nothing Then
					Return
				End If
				If e.RowHandle = GridControl.NewItemRowHandle Then
					e.IsValid = (CType(e.Row, DataRowView)) _
						("ProductName").ToString() <> String.Empty
				End If
		End Sub

		Private Sub TableView_InvalidRowException(ByVal sender As Object, _
			ByVal e As InvalidRowExceptionEventArgs)
				If e.RowHandle = GridControl.NewItemRowHandle Then
					e.ErrorText = "Please enter the Product name. "
					e.WindowCaption = "Input Error"
					grid.View.FocusedColumn = grid.Columns("ProductName")
				End If
		End Sub
	End Class
End Namespace
