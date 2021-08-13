using DevExpress.Xpf.Grid;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace NewItemRow_CodeBehind {
    public class Person {
        public string ProductName { get; set; }
        public string CompanyName { get; set; }
        public int UnitPrice { get; set; }
        public bool Discontinued { get; set; }

        public Person() { }
        public Person(int i) {
            ProductName = "ProductName" + i;
            CompanyName = "CompanyName" + i;
            UnitPrice = i;
            Discontinued = i % 2 == 0;
        }
    }

    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            grid.ItemsSource = GetPersonList().ToList();
        }
        static IEnumerable<Person> GetPersonList() {
            return Enumerable.Range(0, 3).Select(i => new Person(i));
        }

        void OnInitNewRow(object sender, InitNewRowEventArgs e) {
            grid.SetCellValue(e.RowHandle, "UnitPrice", 10);
            grid.SetCellValue(e.RowHandle, "CompanyName", "newcompany");
            grid.SetCellValue(e.RowHandle, "Discontinued", false);
        }

        void OnValidateRow(object sender, GridRowValidationEventArgs e) {
            if(e.RowHandle == GridControl.NewItemRowHandle) {
                e.IsValid = !string.IsNullOrEmpty(((Person)e.Row).ProductName);
                e.Handled = true;
            }
        }

        void OnInvalidRowException(object sender, InvalidRowExceptionEventArgs e) {
            if(e.RowHandle == GridControl.NewItemRowHandle) {
                e.ErrorText = "Please enter the Product name. ";
                e.WindowCaption = "Input Error";
            }
        }
    }
}
