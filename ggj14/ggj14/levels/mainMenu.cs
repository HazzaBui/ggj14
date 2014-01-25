using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ggj14.levels
{
    class mainMenu : baseLevel
    {
        SpriteBatch sb;
        ContentManager cm;
        ggj14.helpers.levelReturn returnObj;

        public mainMenu(SpriteBatch spriteBatch, ContentManager contentManager)
        {
            sb = spriteBatch;
            cm = contentManager;
            returnObj = new ggj14.helpers.levelReturn();
            returnObj.exitLevel = false;
        }

        public override void loadContent()
        {
            
        }

        public override void unloadContent()
        {
            
        }

        public override ggj14.helpers.levelReturn update(Microsoft.Xna.Framework.GameTime gameTime)
        {

            return returnObj;
        }

        public override void draw(Microsoft.Xna.Framework.GraphicsDeviceManager graphics, Microsoft.Xna.Framework.GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Green);


        }
    }
}
