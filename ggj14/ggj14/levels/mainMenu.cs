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
        String[] menuStrings;
        float menuSize;
        int menuSelection;
        Vector2 menuTopLeftPos;
        SpriteFont menuFont;
        int menuTimer;

        public mainMenu(SpriteBatch spriteBatch, ContentManager contentManager)
        {
            sb = spriteBatch;
            cm = contentManager;
            returnObj = new ggj14.helpers.levelReturn();
            returnObj.exitLevel = false;
            menuStrings = new String[2];

            loadContent();
            menuTopLeftPos = new Vector2();

            initialise();
        }

        private void initialise()
        {
            menuStrings[0] = "Level 1";
            menuStrings[1] = "Exit";
            menuSize = 1.0f;
            menuSelection = 0;
            menuTopLeftPos.X = 20.0f;
            menuTopLeftPos.Y = 20.0f;
            menuTimer = 0;
        }

        public override void loadContent()
        {
            menuFont = cm.Load<SpriteFont>("Content\\fonts\\mainMenu");
        }

        public override void unloadContent()
        {
            
        }

        public override ggj14.helpers.levelReturn update(Microsoft.Xna.Framework.GameTime gameTime, ggj14.helpers.playerControl controller)
        {
            menuTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (menuTimer > 1000)
            {
                menuTimer = 1001;
                if (controller.up)
                {
                    menuSelection -= 1;
                    menuTimer = 0;
                }
                else if (controller.down)
                {
                    menuSelection += 1;
                    menuTimer = 0;
                }
                if (menuSelection < 0)
                    menuSelection = menuStrings.Length - 1;
                if (menuSelection >= menuStrings.Length)
                    menuSelection = 0;
            }


            return returnObj;
        }

        public override void draw(Microsoft.Xna.Framework.GraphicsDeviceManager graphics, Microsoft.Xna.Framework.GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Green);
            
            for(int i = 0; i < menuStrings.Length; i++)
            {
                if(i == menuSelection)
                    sb.DrawString(menuFont, menuStrings[i], new Vector2(menuTopLeftPos.X, menuTopLeftPos.Y + i * 20), Color.Yellow);
                else
                    sb.DrawString(menuFont, menuStrings[i], new Vector2(menuTopLeftPos.X, menuTopLeftPos.Y + i * 20), Color.White);
            }




        }
    }
}
