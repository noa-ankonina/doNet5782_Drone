using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
    {
        using System.Runtime.Serialization;

        [Serializable]

        public class BLgeneralException : Exception
        {
            public BLgeneralException() : base("ERROR") { }
            public BLgeneralException(string message) : base(message) { }
        public BLgeneralException(string message, Exception innerException) : base(message, innerException) { }

        override public string ToString()
            { return Message; }
        }
        public class BLIdExistsException : Exception
        {
            public BLIdExistsException() : base("The ID already Exists") { }
            public BLIdExistsException(string message) : base(message) { }
       public BLIdExistsException(string message, Exception innerException) : base(message, innerException) { }

        override public string ToString()
            { return Message; }
        }

        public class BLIdUnExistsException : Exception
        {
            public BLIdUnExistsException() : base("The ID don't found") { }
            public BLIdUnExistsException(string message) : base(message) { }
        public BLIdUnExistsException(string message, Exception innerException) : base(message, innerException) { }

        override public string ToString()
            { return Message; }
        }

    }
