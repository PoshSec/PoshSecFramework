using System;

namespace PoshSec.Framework
{
    public class ScanStatusEventArgs : EventArgs
    {
        public ScanStatusEventArgs(string status, int currentIndex, int maxIndex)
        {
            Status = status;
            CurrentIndex = currentIndex;
            MaxIndex = maxIndex;
        }

        public string Status { get; }

        public int CurrentIndex { get; }

        public int MaxIndex { get; }
    }
}