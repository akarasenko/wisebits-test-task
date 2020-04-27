using System;

namespace wisebits_test_task
{
    public class DbModel
    {
        // null, если поле не выведено в таблицу UI 
        // строка "null", если отсутствует значение поля в БД (но оно было выведено в таблицу UI)

        public int CustomerId { get; private set; }
        public string CustomerName { get; private set; }
        public string ContactName { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }

        public DbModel()
        {
            CustomerId = -1;
            CustomerName = null;
            ContactName = null;
            Address = null;
            City = null;
            PostalCode = null;
            Country = null;
        }

        public DbModel(string customerName, string contactName, string address, string city, string postalCode, string country)
        {
        CustomerId = -1;
        CustomerName = customerName;
        ContactName = contactName;
        Address = address;
        City = city;
        PostalCode = postalCode;
        Country = country;
        } 
        
        public override bool Equals(object itemObj)
        {
            var item = itemObj as DbModel;
            return (CustomerName == item.CustomerName
                && ContactName == item.ContactName
                && Address == item.Address
                && City == item.City
                && PostalCode == item.PostalCode
                && Country == item.Country);
        }

        public override int GetHashCode()
        {
            return CustomerId.GetHashCode();
        }
        public void AddProperty(string propertyName, string propertyValue)
        {
            propertyValue.Replace(" ", "");

            switch (propertyName.ToLower())
            {
                case "customerid": 
                    this.CustomerId = int.Parse(propertyValue); 
                    break;

                case "customername":
                    this.CustomerName = propertyValue;
                    break;

                case "contactname":
                    this.ContactName = propertyValue;
                    break;

                case "address":
                    this.Address = propertyValue;
                    break;

                case "city":
                    this.City = propertyValue;
                    break;

                case "postalcode":
                    this.PostalCode = propertyValue;
                    break;

                case "country":
                    this.Country = propertyValue;
                    break;

                default:
                    throw new ArgumentException("No property to add value to DbModel object.");
            }
        }
    }
}
