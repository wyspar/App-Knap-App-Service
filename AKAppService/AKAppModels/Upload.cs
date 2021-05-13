using System;

namespace AKAppModels
{
    public class Upload
    {
        private string fileName;
        private int applicationID;
        private int blobID;
        public string FileName
        {
            get { return fileName; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException();
                }
                fileName = value;
            }
        }
        public int BlobID
        {
            get { return blobID; }
            set
            {
                Console.WriteLine(value);
                if (value <= 0)
                {
                    throw new ArgumentNullException();
                }
                blobID = value;
            }
        }
        public int ApplicationID
        {
            get { return applicationID; }
            set
            {
                Console.WriteLine(value);
                if (value <= 0)
                {
                    throw new ArgumentNullException();
                }
                applicationID = value;
            }
        }
    }
}
