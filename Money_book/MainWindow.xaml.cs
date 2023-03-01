using Microsoft.Win32;
using Money_book.Functions;
using Money_book.Model;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Money_book
{
    public partial class MainWindow : Window
    {
        #pragma warning disable CS8618 // A non-nullable field must contain a non-null value when the constructor exits. It might be worth declaring the field as nullable.
        public MainWindow() => InitializeComponent();


        private BindingList<Table_model> table_Models;
        private readonly string path_EXE_File = $"{Environment.CurrentDirectory}\\Table models.json";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                table_Models = new Table_model_loading_function(path_EXE_File).Load();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Close();
            }
            table_Models.ListChanged += table_Models_ListChanged;
            Table_display.ItemsSource = table_Models;
        }

        private void table_Models_ListChanged(object? sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType==ListChangedType.ItemAdded || 
                e.ListChangedType == ListChangedType.ItemChanged || 
                e.ListChangedType == ListChangedType.ItemDeleted)
            {
                try
                {
                    new Table_model_saving_function(path_EXE_File).Save(table_Models);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    Close();
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            new Table_model_saving_function(path_EXE_File).Save(table_Models);
        }
    }
}
