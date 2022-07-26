Imports DevExpress.Xpf.Grid
Imports System.Collections.ObjectModel
Imports System.Windows

Namespace DXGrid_NewItemRow

    Public Partial Class Window1
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            DataContext = New MyViewModel()
        End Sub

        Private Sub TableView_InitNewRow(ByVal sender As Object, ByVal e As InitNewRowEventArgs)
            Me.grid.SetCellValue(e.RowHandle, "UnitPrice", 10)
            Me.grid.SetCellValue(e.RowHandle, "CompanyName", "newcompany")
            Me.grid.SetCellValue(e.RowHandle, "Discontinued", False)
        End Sub

        Private Sub TableView_ValidateRow(ByVal sender As Object, ByVal e As GridRowValidationEventArgs)
            If e.Row Is Nothing Then Return
            If e.RowHandle = DataControlBase.NewItemRowHandle Then
                e.IsValid = Not String.IsNullOrEmpty(CType(e.Row, Person).ProductName)
                e.Handled = True
            End If
        End Sub

        Private Sub TableView_InvalidRowException(ByVal sender As Object, ByVal e As InvalidRowExceptionEventArgs)
            If e.RowHandle = DataControlBase.NewItemRowHandle Then
                e.ErrorText = "Please enter the Product name. "
                e.WindowCaption = "Input Error"
            End If
        End Sub
    End Class

    Public Class MyViewModel

        Public Sub New()
            CreateList()
        End Sub

        Public Property PersonList As ObservableCollection(Of Person)

        Private Sub CreateList()
            PersonList = New ObservableCollection(Of Person)()
            For i As Integer = 0 To 3 - 1
                Dim p As Person = New Person(i)
                PersonList.Add(p)
            Next
        End Sub
    End Class

    Public Class Person

        Public Sub New()
        End Sub

        Public Sub New(ByVal i As Integer)
            ProductName = "ProductName" & i
            CompanyName = "CompanyName" & i
            UnitPrice = i
            Discontinued = i Mod 2 = 0
        End Sub

        Public Property ProductName As String

        Public Property CompanyName As String

        Public Property UnitPrice As Integer

        Public Property Discontinued As Boolean
    End Class
End Namespace
