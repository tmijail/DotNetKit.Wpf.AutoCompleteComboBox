using DotNetKit.Demo.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace DotNetKit.Demo
{
    /// <summary>
    /// MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        sealed class ViewModel
            : INotifyPropertyChanged
        {
            #region INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;

            void SetField<X>(ref X field, X value, [CallerMemberName] string propertyName = null)
            {
                if (EqualityComparer<X>.Default.Equals(field, value)) return;

                field = value;

                var h = PropertyChanged;
                if (h != null) h(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion

            IReadOnlyList<Person> items = new List<Person>();
            public IReadOnlyList<Person> Items
            {
                get { return items; }
                set { SetField(ref items, value); }
            }

            Person selectedItem;
            public Person SelectedItem
            {
                get { return selectedItem; }
                set { SetField(ref selectedItem, value); }
            }

            long? selectedValue;
            public long? SelectedValue
            {
                get { return selectedValue; }
                set { SetField(ref selectedValue, value); }
            }

            private ICommand loadItemsCommand;
            public ICommand LoadItemsCommand
            {
                get
                {
                    return loadItemsCommand;
                }
                set
                {
                    loadItemsCommand = value;
                }
            }

            public ViewModel()
            {
                LoadItemsCommand = new RelayCommand((object obj) =>
                {
                    Items = PersonModule.All;
                }, param => true);
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ViewModel();
        }
    }
}
