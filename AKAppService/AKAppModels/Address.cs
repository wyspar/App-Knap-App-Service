using System;

namespace AKAppModels
{
    public class Address
    {
        private int id;
        private string city;
        private string state;
        private string street;
        private string zip;
        public string City { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string ZIP { get; set; }
        public int ID
        {
            get { return id; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentNullException();
                }
                id = value;
            }
        }
    }
}
