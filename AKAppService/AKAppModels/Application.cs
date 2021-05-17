using System;
using System.Collections.Generic;

namespace AKAppModels
{
    public class Application
    {
        private string firstName;
        private string lastName;
        private string email;
        private int id;
        private Location location;
        private List<Upload> uploads;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException();
                }
                firstName = value;
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException();
                }
                lastName = value;
            }
        }
        public string Email {
            get { return email; }
            set {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException();
                }
                try
                {
                    var addr = new System.Net.Mail.MailAddress(value);
                    email = addr.Address;
                }
                catch
                {
                    throw new ArgumentNullException();
                }
                
            }
        }
        public Location Location
        {
            get { return location; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                location = value;
            }
        }
        public List<Upload> Uploads
        {
            get { return uploads; }
            set
            {
/*                if (value == null)
                {
                    throw new ArgumentNullException();
                }*/
                uploads = value;
            }
        }

        public int ID { 
            get { return id; }
            set {
                if (value <= 0)
                {
                    throw new ArgumentNullException();
                }
                id = value;
            } 
        }
    }
}
