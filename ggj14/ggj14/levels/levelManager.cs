using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;


namespace ggj14
{
    public class levelManager : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;
        Stack<ggj14.levels.baseLevel> levelStack;
        SpriteBatch spriteBatch;
        GraphicsDevice device;
        ContentManager contentManager;
        ggj14.helpers.levelReturn levelState;

        public levelManager()
        {
            graphics = new GraphicsDeviceManager(this);
            levelStack = new Stack<ggj14.levels.baseLevel>();
            device = graphics.GraphicsDevice;
            
        }

        protected override void Initialize()
        {
            levelStack.Push(new ggj14.levels.mainMenu(spriteBatch, contentManager));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            levelState = levelStack.Peek().update(gameTime);

            if (levelState.exitLevel)
            {
                //Add new level + pop old level

                levelState = levelStack.Peek().update(gameTime);
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //graphics.GraphicsDevice.Clear(Color.CornflowerBlue);


            levelStack.Peek().draw(graphics, gameTime);




            base.Draw(gameTime);
        }

    }
}
