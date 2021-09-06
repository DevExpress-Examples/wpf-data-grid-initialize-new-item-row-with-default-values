using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Xpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NewItemRow_MVVM {
    public class Product {
        public string ProductName { get; set; }
        public string CompanyName { get; set; }
        public int UnitPrice { get; set; }
        public bool Discontinued { get; set; }

        public Product() { }
        public Product(int i) {
            ProductName = "ProductName" + i;
            CompanyName = "CompanyName" + i;
            UnitPrice = i;
            Discontinued = i % 2 == 0;
        }
    }

    public class MainViewModel : ViewModelBase {
        public ObservableCollection<Product> ProductList { get; }
        public MainViewModel() {
            ProductList = new ObservableCollection<Product>(GetProductList());
        }
        static IEnumerable<Product> GetProductList() {
            return Enumerable.Range(0, 3).Select(i => new Product(i));
        }

        [Command]
        public void InitNewRow(InitNewRowArgs args) {
            var product = (Product)args.Item;
            product.UnitPrice = 10;
            product.CompanyName = "newcompany";
            product.Discontinued = false;
        }
        [Command]
        public void ValidateRow(RowValidationArgs args) {
            if(args.IsNewItem && string.IsNullOrEmpty(((Product)args.Item).ProductName)) {
                args.Result = new ValidationErrorInfo("Please enter the Product Name.");
            }
        }
        [Command]
        public void InvalidRow(InvalidRowExceptionArgs args) {
            if(args.IsNewItem) {
                args.ErrorText = "Please enter the Product Name.";
                args.WindowCaption = "Input Error";
            }
        }
    }
}
