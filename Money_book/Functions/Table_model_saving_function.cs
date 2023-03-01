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
