using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ggj14.levels
{
    class gameLevel : baseLevel
    {
        helpers.levelReturn returnObj;
        List<entities.Entity> entities;
        List<entities.gameObject> gameObjects;
        string backgroundTextureLocation;

        public gameLevel(SpriteBatch spriteBatch, ContentManager contentManager)
        {
            entities = new List<entities.Entity>();
            gameObjects = new List<entities.gameObject>();
        }

        public override void intialise(string levelXml, string chapter)
        {
            this.loadLevelFromXML(levelXml, chapter);
        }

        public override void loadContent()
        {
            
        }

        public override void unloadContent()
        {
            
        }

        public override helpers.levelReturn update(Microsoft.Xna.Framework.GameTime gameTime, helpers.playerControl controller)
        {

            return returnObj;
        }

        public override void draw(Microsoft.Xna.Framework.GraphicsDeviceManager graphics, Microsoft.Xna.Framework.GameTime gameTime)
        {
            
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
                                backgroundTextureLocation = reader.Value;
                                reader.Read();
                            break;
                            case "gameobject":
                                //Load game object data

                            break;
                            case "entity":
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
