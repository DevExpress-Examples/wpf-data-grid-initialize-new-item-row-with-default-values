using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Grid;
using System.Data;

namespace DXGrid_NewItemRow {
    public partial class Window1 : Window {
        public Window1() {
            InitializeComponent();
            grid.ItemsSource = 
                new nwindDataSetTableAdapters.ProductsTableAdapter().GetData().DefaultView;
        }

        private void TableView_InitNewRow(object sender, InitNewRowEventArgs e) {
            grid.SetCellValue(e.RowHandle, "UnitPrice", 10);
            grid.SetCellValue(e.RowHandle, "Discontinued", false);
            grid.SetCellValue(e.RowHandle, "UnitsOnOrder", 1);
        }

        private void TableView_ValidateRow(object sender, GridRowValidationEventArgs e) {
            if (e.Row == null) return;
            if (e.RowHandle == GridControl.NewItemRowHandle)
                e.IsValid = ((DataRowView)e.Row)["ProductName"].ToString() != string.Empty;
        }

        private void TableView_InvalidRowException(object sender, InvalidRowExceptionEventArgs e) {
            if (e.RowHandle == GridControl.NewItemRowHandle) {
                e.ErrorText = "Please enter the Product name. ";
                e.WindowCaption = "Input Error";
                grid.CurrentColumn = grid.Columns["ProductName"];
            }
        }
    }
}
