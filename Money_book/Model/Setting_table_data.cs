using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Money_book.Model
{
    class Setting_table_data
    {

        public void Data_initialization(Table_model table_Model)
        {
            Add_ID_Async(table_Model);
            Formation_date_time_Async(table_Model);
        }

        private const int INITIAL_VALUE = 1;
        private static int number_ID { get; set; } = INITIAL_VALUE;

        private void Add_ID(Table_model table_Model)
        {
            table_Model.ID = number_ID;
            number_ID++;
            
        }

        private void Formation_date_time(Table_model table_Model)
        {
            table_Model.Date_time = DateTime.Now.ToString(new CultureInfo("uk-UA"));
        }

        private async Task Add_ID_Async(Table_model table_Model)
        {
            await Task.Run(() => Add_ID(table_Model));
        }

        private async Task Formation_date_time_Async(Table_model table_Model)
        {
            await Task.Run(() => Formation_date_time(table_Model));
        }
    }
}
