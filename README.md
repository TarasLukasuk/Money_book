# UA
## ***Money book***

Ідея проєкту у тому щоб надати користувачу зручний інтерфейс запису своїх грошових операцій у формі таблиці у якій є ID номер операції дата та час запису операції назва операції  та сума операції

### План розробки проекту

- [X] Створити вікно проекту
- [X] Створити модель таблиці
- [X] Створити  метод нумерації строк
- [X] Створити метод форматування часу та дати
- [X] Розробити метод збереження даних табдиці
- [X] Розробити метод завантаження таблиці
- [X] Розробити функцію збереження змін даних таблиці

### ***Code***

Створення вікна проєкту

~~~ Xaml
<Window x:Class="Money_book.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:Money_book"
        mc:Ignorable="d"
        Title="Money_book" Height="450" Width="820" ResizeMode="NoResize" Loaded="Window_Loaded" >
    <Window.Resources>
        <Style x:Key="Button" TargetType="Button">
            <Setter Property="OverridesDefaultStyle" Value="True"/>
            <Setter Property="Cursor" Value="Hand"/>
            <Setter Property="VerticalContentAlignment" Value="Center"/>
            <Setter Property="Background" Value="#00C9FF"/>
            <Setter Property="Opacity" Value="0.4"/>
            <Setter Property="FontSize" Value="14"/>
            <Setter Property="Template">
                <Setter.Value>
                    <ControlTemplate TargetType="Button">
                        <Grid SnapsToDevicePixels="True">
                            <Border Background="{TemplateBinding Background}" BorderThickness="0" CornerRadius="10" BorderBrush="{TemplateBinding BorderBrush}">
                                <ContentPresenter VerticalAlignment="Center" HorizontalAlignment="Center"/>
                            </Border>
                        </Grid>
                        <ControlTemplate.Triggers>
                            <EventTrigger RoutedEvent="PreviewMouseDown">
                                <BeginStoryboard>
                                    <Storyboard>
                                        <ThicknessAnimation Storyboard.TargetProperty="Margin" Duration="0:0:0.100" To="2"/>
                                    </Storyboard>
                                </BeginStoryboard>
                            </EventTrigger>

                            <EventTrigger RoutedEvent="PreviewMouseUp">
                                <BeginStoryboard>
                                    <Storyboard>
                                        <ThicknessAnimation Storyboard.TargetProperty="Margin" Duration="0:0:0.100" To="0"/>
                                    </Storyboard>
                                </BeginStoryboard>
                            </EventTrigger>

                            <Trigger Property="IsMouseOver" Value="True">
                                <Setter Property="Opacity" Value="1"/>
                            </Trigger>
                        </ControlTemplate.Triggers>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
        </Style>
    </Window.Resources>
    <Grid>
        <Grid Height="30" VerticalAlignment="Top">
            <StackPanel Orientation="Horizontal">
                <Grid Width="40" Margin="5">
                    <Button x:Name="Save" Style="{StaticResource Button}" ToolTip="Save" Click="Save_Click">
                        <Image Source="/Image/save.png"/>
                    </Button>
                </Grid>
            </StackPanel>
        </Grid>
        <Grid Margin="0,30,0,0">
            <DataGrid x:Name="Table_display" FontSize="20" FontWeight="Bold" AutoGenerateColumns="False">
                <DataGrid.Columns>
                    <DataGridTextColumn Header="ID" Width="50" Binding="{Binding Path=ID}" IsReadOnly="True"/>
                    <DataGridTextColumn Header="DateTime" Width="220" Binding="{Binding Path=Date_time}" IsReadOnly="True"/>
                    <DataGridTextColumn Header="Name operation" Width="180" Binding="{Binding Path=Name_operation}"/>
                    <DataGridTextColumn Header="Sum oparation" Width="162*" Binding="{Binding Path=Sum_oparation}"/>
                </DataGrid.Columns>
            </DataGrid>
        </Grid>
    </Grid>
</Window>
~~~

Модель таблиці

~~~ C Sharp
using System.ComponentModel;

namespace Money_book.Model
{
    sealed class Table_model : INotifyPropertyChanged
    {
        #pragma warning disable CS8618 // A non-nullable field must contain a non-null value when the constructor exits. It might be worth declaring the field as nullable.
        public Table_model()
        {
            Setting_table_data setting_Table_Data = new Setting_table_data();
            setting_Table_Data.Data_initialization(this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public int ID { get; set; }

        public string Date_time { get; set; }

        private string name_Operation;

        public string Name_operation
        {
            get { return name_Operation; }
            set 
            {
                if (name_Operation != value)
                {
                    name_Operation = value;
                    On_Property_Changed();
                }
                name_Operation = value;
            }
        }

        private int sum_Oparation;

        public int Sum_oparation
        {
            get 
            {   
                return sum_Oparation; 
            }
            set 
            {
                if (sum_Oparation != value)
                {
                    sum_Oparation = value;
                    On_Property_Changed();
                }
                sum_Oparation = value;
            }
        }

        private void On_Property_Changed()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }
    }
}
~~~

Номерація таблиці

~~~ C Sharp

private const int INITIAL_VALUE = 1;
private static int number_ID { get; set; } = INITIAL_VALUE;

private void Add_ID(Table_model table_Model)
{
    table_Model.ID = number_ID;
    number_ID++;
    
}
~~~

Створити метод форматування часу та дати

~~~ C Sharp
private void Formation_date_time(Table_model table_Model)
{
    table_Model.Date_time = DateTime.Now.ToString(new CultureInfo("uk-UA"));
}
~~~

Метод збереження даних табдиці

~~~ C Sharp
using Money_book.Model;
using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;

namespace Money_book.Functions
{
    sealed internal class Table_model_saving_function
    {
        private readonly string _path;

        public Table_model_saving_function(string path)
        {
            _path = path;
        }

        public void Save(BindingList<Table_model> table_Models)
        {
            using (StreamWriter writer = File.CreateText(_path))
            {
                string output = JsonConvert.SerializeObject(table_Models);
                writer.Write(output);
            }
        }
    }
}
~~~

Метод завантаження таблиці

~~~ C Sharp
using Money_book.Model;
using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;

namespace Money_book.Functions
{
    sealed internal class Table_model_loading_function
    {
        private readonly string _path;

        public Table_model_loading_function(string path)
        {
            _path = path;
        }

        public BindingList<Table_model> Load()
        {
            bool file_Exists = File.Exists(_path);

            if (file_Exists ==false)
            {
                File.CreateText(_path).Dispose();
                return new BindingList<Table_model>();
            }
            else
            {
                using (StreamReader reader = File.OpenText(_path))
                {
                    var file_text = reader.ReadToEnd();

                    #pragma warning disable CS8603 // Perhaps returning a nullable reference.
                    return JsonConvert.DeserializeObject<BindingList<Table_model>>(file_text);
                }
            }
        }
    }
}

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
~~~

Розробити функцію збереження змін даних таблиці

~~~ C Sharp
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
~~~
