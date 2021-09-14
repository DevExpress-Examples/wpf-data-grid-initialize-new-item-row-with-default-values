Imports DevExpress.Xpf.Grid
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Windows

Namespace NewItemRow_CodeBehind
	Public Class Product
		Public Property ProductName() As String
		Public Property CompanyName() As String
		Public Property UnitPrice() As Integer
		Public Property Discontinued() As Boolean

		Public Sub New()
		End Sub
		Public Sub New(ByVal i As Integer)
			ProductName = "ProductName" & i
			CompanyName = "CompanyName" & i
			UnitPrice = i
			Discontinued = i Mod 2 = 0
		End Sub
	End Class

	Partial Public Class MainWindow
		Inherits Window

		Public Sub New()
			InitializeComponent()
			grid.ItemsSource = GetProductList().ToList()
		End Sub
		Private Shared Function GetProductList() As IEnumerable(Of Product)
			Return Enumerable.Range(0, 3).Select(Function(i) New Product(i))
		End Function

		Private Sub OnInitNewRow(ByVal sender As Object, ByVal e As InitNewRowEventArgs)
			grid.SetCellValue(e.RowHandle, "UnitPrice", 10)
			grid.SetCellValue(e.RowHandle, "CompanyName", "newcompany")
			grid.SetCellValue(e.RowHandle, "Discontinued", False)
		End Sub

		Private Sub OnValidateRow(ByVal sender As Object, ByVal e As GridRowValidationEventArgs)
			If e.RowHandle = GridControl.NewItemRowHandle Then
				e.IsValid = Not String.IsNullOrEmpty(DirectCast(e.Row, Product).ProductName)
				e.Handled = True
			End If
		End Sub

		Private Sub OnInvalidRowException(ByVal sender As Object, ByVal e As InvalidRowExceptionEventArgs)
			If e.RowHandle = GridControl.NewItemRowHandle Then
				e.ErrorText = "Please enter the Product Name."
				e.WindowCaption = "Input Error"
			End If
		End Sub
	End Class
End Namespace
