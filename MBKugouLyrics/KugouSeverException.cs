using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicBeePlugin
{
    class KugouSeverException : Exception
    {
        public KugouSeverException(int errorCode)
        {
            ErrorCode = errorCode;
        }
        public int ErrorCode { get; set; }
    }
}
