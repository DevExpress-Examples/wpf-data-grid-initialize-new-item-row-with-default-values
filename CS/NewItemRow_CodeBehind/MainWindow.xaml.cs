using DevExpress.Xpf.Grid;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace NewItemRow_CodeBehind {
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

    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            grid.ItemsSource = GetProductList().ToList();
        }
        static IEnumerable<Product> GetProductList() {
            return Enumerable.Range(0, 3).Select(i => new Product(i));
        }

        void OnAddingNewRow(object sender, System.ComponentModel.AddingNewEventArgs e) {
            e.NewObject = new Product() {
                CompanyName = "newcompany",
                UnitPrice = 10,
                Discontinued = false
            };
        }

        void OnValidateRow(object sender, GridRowValidationEventArgs e) {
            if(e.RowHandle == GridControl.NewItemRowHandle) {
                e.IsValid = !string.IsNullOrEmpty(((Product)e.Row).ProductName);
                e.Handled = true;
            }
        }

        void OnInvalidRowException(object sender, InvalidRowExceptionEventArgs e) {
            if(e.RowHandle == GridControl.NewItemRowHandle) {
                e.ErrorText = "Please enter the Product Name.";
                e.WindowCaption = "Input Error";
            }
        }
    }
}
