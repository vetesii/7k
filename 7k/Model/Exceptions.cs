using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model.ContextElement
{
    // TODO 5 - Ellenorizni, torolni ha kell

    class StopTaskManagerException : Exception
    {
        public StopTaskManagerException() : base() { }

        public StopTaskManagerException(String message) : base(message) { }
    }

    class OptionNotFoundException : Exception
    {
        public OptionNotFoundException() : base() { }

        public OptionNotFoundException(String message) : base(message) { }
    }

    class InvalidSelectedDocumentException : Exception
    {
        public InvalidSelectedDocumentException() : base() { }
        public InvalidSelectedDocumentException(string message) : base(message) { }
        public InvalidSelectedDocumentException(string message, System.Exception inner) : base(message, inner) { }
    }

    class ClosedDocumentOrApplicationException : Exception
    {
        public ClosedDocumentOrApplicationException() : base() { }
        public ClosedDocumentOrApplicationException(string message) : base(message) { }
        public ClosedDocumentOrApplicationException(string message, System.Exception inner) : base(message, inner) { }
    }

    class UnidentifiedStyleException : Exception
    {
        public UnidentifiedStyleException() : base() { }
        public UnidentifiedStyleException(string message) : base(message) { }
        public UnidentifiedStyleException(string message, System.Exception inner) : base(message, inner) { }
    }
}
