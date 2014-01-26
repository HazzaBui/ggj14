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
        abstract public void loadContent();
        abstract public void unloadContent();
        abstract public ggj14.helpers.levelReturn update(GameTime gameTime, ggj14.helpers.playerControl controller);
        abstract public void draw(GraphicsDeviceManager graphics, GameTime gameTime);
        abstract public void intialise(string levelXml, string chapter, string level);


    }
}
