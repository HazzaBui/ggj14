using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ggj14.levels
{
    abstract class baseLevel
    {
        abstract public baseLevel();
        abstract public void loadContent();
        abstract public void unloadContent();
        abstract public void update(GameTime gameTime);
        abstract public void draw(GraphicsDeviceManager graphics, GameTime gameTime);


    }
}
