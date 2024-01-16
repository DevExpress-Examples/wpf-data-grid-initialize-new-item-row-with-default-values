Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Xpf
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq

Namespace NewItemRow_MVVM

    Public Class Product

        Public Property ProductName As String

        Public Property CompanyName As String

        Public Property UnitPrice As Integer

        Public Property Discontinued As Boolean

        Public Sub New()
        End Sub

        Public Sub New(ByVal i As Integer)
            ProductName = "ProductName" & i
            CompanyName = "CompanyName" & i
            UnitPrice = i
            Discontinued = i Mod 2 = 0
        End Sub
    End Class

    Public Class MainViewModel
        Inherits ViewModelBase

        Public ReadOnly Property ProductList As ObservableCollection(Of Product)

        Public Sub New()
            ProductList = New ObservableCollection(Of Product)(GetProductList())
        End Sub

        Private Shared Function GetProductList() As IEnumerable(Of Product)
            Return Enumerable.Range(0, 3).[Select](Function(i) New Product(i))
        End Function

        <Command>
        Public Sub AddingNewRow(ByVal args As NewRowArgs)
            args.Item = New Product() With {.CompanyName = "newcompany", .UnitPrice = 10, .Discontinued = False}
        End Sub

        <Command>
        Public Sub ValidateRow(ByVal args As RowValidationArgs)
            If args.IsNewItem AndAlso String.IsNullOrEmpty(CType(args.Item, Product).ProductName) Then
                args.Result = New ValidationErrorInfo("Please enter the Product Name.")
            End If
        End Sub

        <Command>
        Public Sub InvalidRow(ByVal args As InvalidRowExceptionArgs)
            If args.IsNewItem Then
                args.ErrorText = "Please enter the Product Name."
                args.WindowCaption = "Input Error"
            End If
        End Sub
    End Class
End Namespace
