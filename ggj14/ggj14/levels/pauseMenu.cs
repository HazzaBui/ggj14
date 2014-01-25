using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace ggj14.levels
{
    class pauseMenu : baseLevel
    {
        SpriteBatch sb;
        ContentManager cm;
        ggj14.helpers.levelReturn returnObj;
        SpriteFont sf;
        int menuTimer;
        int menuSelection;
        bool isGrowing;
        string []menuStrings;
        float[] menuSize;
        Vector2 menuTopLeftPos;
        int escapeTimer;


        public pauseMenu(SpriteBatch spriteBatch, ContentManager contentManager)
        {
            sb = spriteBatch;
            cm = contentManager;

            menuStrings = new string[2];
            menuSize = new float[menuStrings.Length];
            returnObj = new ggj14.helpers.levelReturn();
            menuTopLeftPos = new Vector2();

            this.intialise(null, null);
            this.loadContent();
        }

        public override void intialise(string levelXml, string chapter)
        {
            menuStrings[0] = "Resume";
            menuStrings[1] = "Quit";
            for (int i = 0; i < menuSize.Length; i++)
                menuSize[i] = 0.0f;
            //menuSize = 1.2f;
            menuSelection = 0;
            menuTopLeftPos.X = 300.0f; 
            menuTopLeftPos.Y = 200.0f;
            menuTimer = 0;
            isGrowing = true;
            escapeTimer = 0;
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
            escapeTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (escapeTimer > 1000)
                escapeTimer = 1000;

            menuTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (menuTimer > 250)
            {
                menuTimer = 1001;
                if (controller.up)
                {
                    menuSelection -= 1;
                    if (menuSelection < 0)
                        menuSelection = menuStrings.Length - 1;
                    if (menuSelection >= menuStrings.Length)
                        menuSelection = 0;
                    menuTimer = 0;
                    menuSize[menuSelection] = 1.2f;
                    isGrowing = true;
                }
                else if (controller.down)
                {
                    menuSelection += 1;
                    if (menuSelection < 0)
                        menuSelection = menuStrings.Length - 1;
                    if (menuSelection >= menuStrings.Length)
                        menuSelection = 0;
                    menuTimer = 0;
                    menuSize[menuSelection] = 1.2f;
                    isGrowing = true;
                }

            }

            for (int i = 0; i < menuSize.Length; i++)
            {
                if (i != menuSelection)
                {
                    menuSize[i] -= 0.015f;
                }
                if (menuSize[i] < 1.2f)
                    menuSize[i] = 1.2f;
            }

            if (isGrowing)
            {
                menuSize[menuSelection] += 0.015f;
                if (menuSize[menuSelection] > 1.7f)
                    isGrowing = false;
            }
            else
            {
                menuSize[menuSelection] -= 0.015f;
                if (menuSize[menuSelection] < 1.2f)
                    isGrowing = true;
            }

            KeyboardState keyboardState = Keyboard.GetState();
            Keys[] pressedKeys = keyboardState.GetPressedKeys();

            foreach ( Keys k in pressedKeys)
            {
                if (k == Keys.Escape && escapeTimer > 250)
                {
                    returnObj.nextLevel = "";
                    returnObj.exitLevel = true;
                }
                if (k == Keys.Enter)
                {
                    if (menuSelection == menuStrings.Length - 1)
                    {
                        returnObj.nextLevel = "menu";
                        returnObj.exitLevel = true;
                    }
                    else
                    {
                        returnObj.nextLevel = "";
                        returnObj.exitLevel = true;
                    }
                }
            }


            return returnObj;
        }

        public override void draw(Microsoft.Xna.Framework.GraphicsDeviceManager graphics, Microsoft.Xna.Framework.GameTime gameTime)
        {
            for (int i = 0; i < menuStrings.Length; i++)
            {
                if (i == menuSelection)
                {
                    sb.DrawString(sf, menuStrings[i], new Vector2(menuTopLeftPos.X, menuTopLeftPos.Y + 20 * i), Color.Yellow);
                }
                else
                {
                    sb.DrawString(sf, menuStrings[i], new Vector2(menuTopLeftPos.X, menuTopLeftPos.Y + 20 * i), Color.White);
                }
            }
        }

    }
}
