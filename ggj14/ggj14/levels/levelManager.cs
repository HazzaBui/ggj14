using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Xml;


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
        ggj14.helpers.playerControl player1, player2;
        ggj14.helpers.playerKeys player1Keys, player2Keys;
        ggj14.helpers.playerControl player1Last, player2Last;
        XmlTextReader reader;
        bool levelFadeOut;
        bool levelFadeIn;
        bool loadNextLevel;
        bool levelActive;
        float fadeOutValue;
        float fadeInValue;
        bool popLevelNextUpdate;
        RenderTarget2D renderTarget;
        Texture2D fadeOutTexture;
        int escapeTimer;

        public levelManager()
        {
            graphics = new GraphicsDeviceManager(this);
            levelStack = new Stack<ggj14.levels.baseLevel>();
            
            player1 = new ggj14.helpers.playerControl();
            player2 = new ggj14.helpers.playerControl();
            player1Keys = new ggj14.helpers.playerKeys();
            player2Keys = new ggj14.helpers.playerKeys();
            player1Last = new ggj14.helpers.playerControl(false);
            player2Last = new ggj14.helpers.playerControl(false);
            reader = new XmlTextReader("Content\\xmlContent\\playerControls.xml");
        }

        protected override void Initialize()
        {
            //Intialise entity definitions
            helpers.classLoading.loadEntityDefinitions("Content\\xmlContent\\entityDefinitions.xml");
            helpers.classLoading.loadGameObjectDefinitions("Content\\xmlContent\\gameObjectDefinition.xml");

            //Read player input xml file
            while (reader.Read())
            {
                if(reader.Name == "player1")
                {
                    while (reader.Name != "player2")
                    {
                        //pass player1 input
                        reader.Read();
                        switch (reader.Name)
                        {
                            case "up":
                                reader.Read();
                                player1Keys.up = (Keys)Enum.Parse(typeof(Keys), reader.Value, true);
                                reader.Read();
                            break;

                            case "down":
                            reader.Read();
                            player1Keys.down = (Keys)Enum.Parse(typeof(Keys), reader.Value, true);
                            reader.Read();
                            break;

                            case "left":
                            reader.Read();
                            player1Keys.left = (Keys)Enum.Parse(typeof(Keys), reader.Value, true);
                            reader.Read();
                            break;

                            case "right":
                            reader.Read();
                            player1Keys.right = (Keys)Enum.Parse(typeof(Keys), reader.Value, true);
                            reader.Read();
                            break;

                            case "use":
                            reader.Read();
                            player1Keys.use = (Keys)Enum.Parse(typeof(Keys), reader.Value, true);
                            reader.Read();
                            break;
                        }
                    }
                }

            }

            levelActive = true;
            levelFadeOut = false;
            fadeOutValue = 1.0f;
            popLevelNextUpdate = false;
            loadNextLevel = true;

            escapeTimer = 0;
            //levelStack.Push(new ggj14.levels.mainMenu(spriteBatch, contentManager));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            device = graphics.GraphicsDevice;
            contentManager = new ContentManager(this.Services);
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

            PresentationParameters pp = device.PresentationParameters;
            renderTarget = new RenderTarget2D(device, pp.BackBufferWidth, pp.BackBufferHeight, true, device.DisplayMode.Format, DepthFormat.Depth24);


            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            escapeTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (escapeTimer > 1000)
                escapeTimer = 1000;

            if (popLevelNextUpdate)
            {
                popLevelNextUpdate = false;
                levelStack.Pop();
            }

            if (levelFadeIn)
            {
                fadeInValue += 0.01f;
            }
            if (fadeInValue > 1.0f)
            {
                fadeInValue = 0.0f;
                levelActive = true;
                levelFadeIn = false;
                loadNextLevel = false;
            }

            if (levelFadeOut)
            {
                fadeOutValue -= 0.01f;
            }
            if (fadeOutValue < 0)
            {
                levelFadeOut = false;
                levelFadeIn = true;
                fadeOutValue = 1.0f;

                if (levelStack.Count <= 0)
                {
                    Exit();
                }
            }

            KeyboardState keyboardState = Keyboard.GetState();
            Keys[] pressedKeys = keyboardState.GetPressedKeys();

            //Generate player controls
            //player1 reset keys
            player1 = new helpers.playerControl(false);

            //Update player 1 keys
            foreach (Keys key in pressedKeys)
            {
                if (key == player1Keys.up)
                    player1.up = true;
                if (key == player1Keys.down)
                    player1.down = true;
                if (key == player1Keys.left)
                    player1.left = true;
                if (key == player1Keys.right)
                    player1.right = true;
                if (key == player1Keys.use)
                    player1.use = true;

                //Check for escapped pressed for quit/menu pop
                if (key == Keys.Escape && escapeTimer > 250)
                {
                    escapeTimer = 0;
                    if (levelStack.Count == 1)
                    {
                        levelActive = false;
                        levelState.exitLevel = true;
                        //Exit();
                    }
                    else
                    {
                        levelStack.Push(new levels.pauseMenu(spriteBatch, contentManager));
                    }
                }
            }

            //Reset up/use to false if they were true last frame
            if (player1.up)
            {
                if (player1Last.up)
                {
                    player1.up = false;
                }
                player1Last.up = true;
            }
            else
                player1Last.up = false;

            if (player1.use)
            {
                if (player1Last.use)
                {
                    player1.use = false;
                }
                player1Last.use = true;
            }
            else
                player1Last.use = false;




            if (levelStack.Count == 0 && loadNextLevel)
            {
                //levelStack.Push(new ggj14.levels.gameLevel(spriteBatch, contentManager));
                //levelStack.Peek().intialise("Content\\xmlContent\\level1.xml", "chapter1");
                //levelStack.Peek().loadContent();
                levelStack.Push(new ggj14.levels.mainMenu(spriteBatch, contentManager));
                //levelActive = false;
            }

            if(levelStack.Count > 0 && levelActive)
                levelState = levelStack.Peek().update(gameTime, player1);

            if (levelState.exitLevel)
            {
                escapeTimer = 0;
                if (levelStack.Count == 1)
                {
                    levelFadeOut = true;
                    levelActive = false;
                    popLevelNextUpdate = true;
                    loadNextLevel = false;
                }
                else if (levelState.nextLevel == "menu")
                {
                    levelStack.Pop();
                    levelStack.Pop();
                    levelState.nextLevel = "";
                }
                else if (levelStack.Count > 0)
                {
                    levelStack.Pop();
                }
            }
            else if (levelState.nextLevel != "" && levelState.nextLevel != null)
            {
                if (levelState.transitionBetweenLevels)
                {
                    levelStack.Pop();
                }
                levelStack.Push(new levels.gameLevel(spriteBatch, contentManager));
                levelStack.Peek().intialise("Content\\xmlContent\\" + levelState.nextLevel + ".xml", levelState.nextChapter, levelState.nextLevel);
                levelStack.Peek().loadContent();
                levelActive = false;
                levelFadeOut = true;
                levelState.nextLevel = "";
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (levelActive || levelFadeIn)
            {
                device.SetRenderTarget(renderTarget);
                spriteBatch.Begin();
                levelStack.Peek().draw(graphics, gameTime);
                base.Draw(gameTime);
                spriteBatch.End();

                device.SetRenderTarget(null);
                fadeOutTexture = (Texture2D)renderTarget;
            }

            float fadeValue = levelFadeIn ? fadeInValue : fadeOutValue;
            //Draw texture to screen
            spriteBatch.Begin();
            spriteBatch.Draw(fadeOutTexture, new Vector2(0, 0), null, new Color(fadeValue, fadeValue, fadeValue) , 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 1);
            spriteBatch.End();
        }

    }
}
