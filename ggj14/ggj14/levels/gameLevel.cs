using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ggj14.levels
{
    class gameLevel : baseLevel
    {
        SpriteBatch sb;
        ContentManager cm;
        helpers.levelReturn returnObj;
        List<entities.Entity> entities;
        List<entities.gameObject> gameObjects;
        List<Texture2D> backgroundTextures;
        List<string> backgroundTextureStrings;

        public gameLevel(SpriteBatch spriteBatch, ContentManager contentManager)
        {
            sb = spriteBatch;
            cm = contentManager;

            entities = new List<entities.Entity>();
            gameObjects = new List<entities.gameObject>();
            backgroundTextures = new List<Texture2D>();
            backgroundTextureStrings = new List<string>();
            returnObj = new helpers.levelReturn();

        }

        public override void intialise(string levelXml, string chapter)
        {
            this.loadLevelFromXML(levelXml, chapter);
        }

        public override void loadContent()
        {
            foreach (ggj14.entities.Entity e in entities)
            {
                e.LoadContent(cm);
            }
            foreach (ggj14.entities.gameObject g in gameObjects)
            {
                g.LoadContent(cm);
            }
            foreach (string s in backgroundTextureStrings)
            {
                Texture2D tex = cm.Load<Texture2D>(s);
                backgroundTextures.Add(tex);
            }
        }

        public override void unloadContent()
        {
            
        }

        public override helpers.levelReturn update(Microsoft.Xna.Framework.GameTime gameTime, helpers.playerControl controller)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects.ElementAt(i).Update();
            }
            for (int i = 0; i < entities.Count; i++)
            {
                entities.ElementAt(i).Update(entities.ToArray(), gameObjects.ToArray(), i, controller);
            }

            return returnObj;
        }

        public override void draw(Microsoft.Xna.Framework.GraphicsDeviceManager graphics, Microsoft.Xna.Framework.GameTime gameTime)
        {
            //draw background
            foreach (Texture2D tex in backgroundTextures)
            {
                sb.Draw(tex, new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), Color.White);
            }


            foreach (ggj14.entities.gameObject g in gameObjects)
            {
                g.Draw(sb);
            }
            foreach (ggj14.entities.Entity e in entities)
            {
                e.Draw(sb);
            }
        }


        private void loadLevelFromXML(String xml, String chapter)
        {
            XmlTextReader reader = new XmlTextReader(xml);

            while (reader.Read())
            {
                if (reader.Name == chapter)
                {
                    reader.Read();
                    while (reader.Name != chapter)
                    {
                        switch (reader.Name)
                        {
                            case "backgroundtexture":
                                //Load background texture
                                reader.Read();
                                backgroundTextureStrings.Add(reader.Value);
                                //backgroundTextureLocation = reader.Value;
                                reader.Read();
                            break;
                            case "gameobject":
                            {
                                reader.Read();
                                //Load game object data
                                int posx, posy, scale;
                                posx = posy = scale = 0;
                                string text = "";

                                while (reader.Name != "gameobject")
                                {
                                    switch (reader.Name)
                                    {
                                        case "texture":
                                            reader.Read();
                                            text = reader.Value;
                                            reader.Read();
                                        break;
                                        case "positionx":
                                            reader.Read();
                                            posx = Convert.ToInt32(reader.Value);
                                            reader.Read();
                                        break;
                                        case "positiony":
                                            reader.Read();
                                            posy = Convert.ToInt32(reader.Value);
                                            reader.Read();
                                        break;
                                        case "scale":
                                            reader.Read();
                                            scale = Convert.ToInt32(reader.Value);
                                            reader.Read();
                                        break;
                                    }
                                    reader.Read();
                                }
                                //create go
                                gameObjects.Add(helpers.classLoading.loadGameObject(text, posx, posy));
                            }
                            break;
                            case "entity":
                            {
                                reader.Read();
                                //Load entity data
                                int posx, posy, scale;
                                string text, objType;
                                posx = 0;
                                posy = 0;
                                text = "";
                                objType = "";
                                while (reader.Name != "entity")
                                {
                                    switch (reader.Name)
                                    {
                                        case "type":
                                            reader.Read();
                                            objType = reader.Value;
                                            reader.Read();
                                            break;
                                        case "texture":
                                            reader.Read();
                                            text = reader.Value;
                                            reader.Read();
                                            break;
                                        case "positionx":
                                            reader.Read();
                                            posx = Convert.ToInt32(reader.Value);
                                            reader.Read();
                                            break;
                                        case "positiony":
                                            reader.Read();
                                            posy = Convert.ToInt32(reader.Value);
                                            reader.Read();
                                            break;
                                        case "scale":
                                            reader.Read();
                                            scale = Convert.ToInt32(reader.Value);
                                            reader.Read();
                                            break;
                                    }
                                    reader.Read();
                                }
                                entities.Add(helpers.classLoading.loadEntity(text, posx, posy, objType));
                            }
                            break;
                                //Add additional items to load here
                        }
                        reader.Read();
                    }
                }
            }
        }

        
    }
}
