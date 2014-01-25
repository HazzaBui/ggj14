using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace ggj14.helpers
{
    enum characters
    {
        MOUSE,
        BOY,
    }

    struct levelReturn
    {
        public string nextLevel;
        public characters character;
        public int previousLevel;
        public bool exitLevel;
    }

    public struct playerControl
    {
        public bool up;
        public bool down;
        public bool left;
        public bool right;
        public bool use;
        public playerControl(bool x) { up = x; down = x; left = x; right = x; use = x; }
    }

    struct playerKeys
    {
        public Keys up;
        public Keys down;
        public Keys left;
        public Keys right;
        public Keys use;
    }

}
