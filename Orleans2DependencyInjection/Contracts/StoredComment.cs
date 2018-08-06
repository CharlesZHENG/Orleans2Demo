using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public class StoredComment : InputComment
    {
        public DateTime Timestamp { get; set; }
    }
}
