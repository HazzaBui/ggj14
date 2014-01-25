using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj14.helpers
{
    public static class classLoading
    {
        public static entities.Entity loadEntity(string texture, int posx, int posy, string objectType)
        {

            switch (objectType)
            {
                case "boy":
                    return new entities.Entities.boy(new Microsoft.Xna.Framework.Vector2(posx, posy));
                

            }

            return null;
        }

    }
}
