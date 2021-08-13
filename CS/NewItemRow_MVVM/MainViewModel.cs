using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Xpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NewItemRow_MVVM {
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

    public class MainViewModel : ViewModelBase {
        public ObservableCollection<Person> PersonList { get; }
        public MainViewModel() {
            PersonList = new ObservableCollection<Person>(GetPersonList());
        }
        static IEnumerable<Person> GetPersonList() {
            return Enumerable.Range(0, 3).Select(i => new Person(i));
        }

        [Command]
        public void InitNewRow(InitNewRowArgs args) {
            var person = (Person)args.Item;
            person.UnitPrice = 10;
            person.CompanyName = "newcompany";
            person.Discontinued = false;
        }
        [Command]
        public void ValidateRow(RowValidationArgs args) {
            if(args.IsNewItem && string.IsNullOrEmpty(((Person)args.Item).ProductName)) {
                args.Result = new ValidationErrorInfo("Please enter the Product name. ");
            }
        }
        [Command]
        public void InvalidRow(InvalidRowExceptionArgs args) {
            if(args.IsNewItem) {
                args.ErrorText = "Please enter the Product name. ";
                args.WindowCaption = "Input Error";
            }
        }
    }
}
