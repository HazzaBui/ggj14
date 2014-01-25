using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj14.helpers
{
    enum characters
    {
        MOUSE,
        BOY,
    }

    struct levelReturn
    {
        public int nextLevel;
        public characters character;
        public int previousLevel;
        public bool exitLevel;
    }

}
