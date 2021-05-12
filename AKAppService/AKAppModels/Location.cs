using System;
namespace AKAppModels
{
    public class Location
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public bool RentOrBuy { get; set; }
        public Address Address { get; set; }
    }
}
