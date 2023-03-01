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
