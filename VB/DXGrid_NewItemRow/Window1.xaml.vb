Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Grid
Imports System.Data

Namespace DXGrid_NewItemRow

    Public Partial Class Window1
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            Me.grid.ItemsSource = New nwindDataSetTableAdapters.ProductsTableAdapter().GetData().DefaultView
        End Sub

        Private Sub TableView_InitNewRow(ByVal sender As Object, ByVal e As InitNewRowEventArgs)
            Me.grid.SetCellValue(e.RowHandle, "UnitPrice", 10)
            Me.grid.SetCellValue(e.RowHandle, "Discontinued", False)
            Me.grid.SetCellValue(e.RowHandle, "UnitsOnOrder", 1)
        End Sub

        Private Sub TableView_ValidateRow(ByVal sender As Object, ByVal e As GridRowValidationEventArgs)
            If e.Row Is Nothing Then Return
            If e.RowHandle = DataControlBase.NewItemRowHandle Then e.IsValid = Not Equals(CType(e.Row, DataRowView)("ProductName").ToString(), String.Empty)
        End Sub

        Private Sub TableView_InvalidRowException(ByVal sender As Object, ByVal e As InvalidRowExceptionEventArgs)
            If e.RowHandle = DataControlBase.NewItemRowHandle Then
                e.ErrorText = "Please enter the Product name. "
                e.WindowCaption = "Input Error"
                Me.grid.CurrentColumn = Me.grid.Columns("ProductName")
            End If
        End Sub
    End Class
End Namespace
