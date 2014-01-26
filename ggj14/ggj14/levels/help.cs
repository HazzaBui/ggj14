using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using ggj14.helpers;

namespace ggj14.levels
{
    class help : baseLevel
    {
        levelReturn returnObj;
        string printText;
        SpriteFont sf;
        SpriteBatch sb;
        ContentManager cm;

        public help(SpriteBatch spriteBatch, ContentManager contentManager)
        {
            returnObj = new levelReturn();
            printText = "";

            sb = spriteBatch;
            cm = contentManager;
        }

        public override void intialise(string levelXml, string chapter, string level)
        {
            string line;
            System.IO.StreamReader reader = new System.IO.StreamReader(level);
            while ((line = reader.ReadLine()) != null)
            {
                printText += line + "\n";
            }

        }

        public override void loadContent()
        {
            sf = cm.Load<SpriteFont>("Content\\fonts\\mainMenu");
        }

        public override void unloadContent()
        {
            
        }

        public override helpers.levelReturn update(Microsoft.Xna.Framework.GameTime gameTime, helpers.playerControl controller)
        {
            if (controller.use)
                returnObj.exitLevel = true;


            return returnObj;
        }

        public override void draw(Microsoft.Xna.Framework.GraphicsDeviceManager graphics, Microsoft.Xna.Framework.GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Green);
            sb.DrawString(sf, printText, new Vector2(0, 0), Color.White, 0.0f, new Vector2(0.0f, 0.0f), 1.0f, SpriteEffects.None, 0.0f);
        }


    }
}
