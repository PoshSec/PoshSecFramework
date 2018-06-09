using System;

namespace poshsecframework
{
    public class StatusChangeEventArgs : EventArgs
    {
        public StatusChangeEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}