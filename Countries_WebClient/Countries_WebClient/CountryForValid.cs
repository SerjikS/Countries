using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace Countries_WebClient
{
    public class CountryForValid : Country, IDataErrorInfo
    {
        public CountryForValid()
        {
            Name = "Введите имя";
            Code = "Введите код";
            Capital = "Введите столицу";
            Area = 1;
            Population = 1;
            Region = "Введите регион";
        }
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Name":
                        if (Name.Length <= 0 || Name.Length > 50)
                        {
                            error = "Длинна имени должна быть не менее 1 и не более 50 букв";
                        }
                        break;
                    case "Code":
                        if (Code.Length <= 0 || Code.Length > 50)
                        {
                            error = "Длинна кода должна быть не менее 1 и не более 50 букв";
                        }
                        break;
                    case "Capital":
                        if (Capital.Length <= 0 || Capital.Length > 50)
                        {
                            error = "Длинна имени должна быть не менее 1 и не более 50 букв";
                        }
                        break;
                    case "Area":
                        if (Area <= 0 || Area > 1.79E+308)
                        {
                            error = $"Площадь территории должна быть не менее 1 и не более {1.79E+308} километров";
                        }
                        break;
                    case "Population":
                        if (Population <= 0 || Population > 2000000000)
                        {
                            error = "Кол-во населения должно быть не менее 1 и не более 2 000 000 000 человек";
                        }
                        break;
                    case "Region":
                        if (Region.Length <= 0 || Region.Length > 50)
                        {
                            error = "Длинна региона должна быть не менее 1 и не более 50 букв";
                        }
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
