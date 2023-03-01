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
