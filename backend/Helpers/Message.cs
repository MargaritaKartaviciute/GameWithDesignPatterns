using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Helpers
{
    public class Message
    {
        public bool IsValid { get; set; } = true;
        public string MessageText { get; set; }
        public bool IsNotFound { get; set; } = false;
    }
}
