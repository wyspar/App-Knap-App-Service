using System;
namespace AKAppModels
{
    public class Location
    {
        private string name;
        private int cost;
        private bool rentOrBuy;
        private Address address;
        public string Name
        {
            get { return name; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException();
                }
                name = value;
            }
        }
        public int Cost
        {
            get { return cost; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentNullException();
                }
                cost = value;
            }
        }
        public bool RentOrBuy
        {
            get { return rentOrBuy; }
            set
            {
                rentOrBuy = value;
            }
        }
        public Address Address { get; set; }
    }
}
