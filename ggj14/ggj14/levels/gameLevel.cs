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
        List<helpers.chapterWaypoint> exitWaypoints;
        string chapterName, levelName;

        public gameLevel(SpriteBatch spriteBatch, ContentManager contentManager)
        {
            sb = spriteBatch;
            cm = contentManager;

            entities = new List<entities.Entity>();
            gameObjects = new List<entities.gameObject>();
            backgroundTextures = new List<Texture2D>();
            backgroundTextureStrings = new List<string>();
            returnObj = new helpers.levelReturn();
            exitWaypoints = new List<helpers.chapterWaypoint>();

        }

        public override void intialise(string levelXml, string chapter, string level)
        {
            this.loadLevelFromXML(levelXml, chapter);
            chapterName = chapter;
            levelName = level;
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

            int load = helpers.levelPersistence.getChapterIndex(levelName + chapterName);
            if (load >= 0)
            {
                entities = helpers.levelPersistence.entityLists.ElementAt(load);
                gameObjects = helpers.levelPersistence.gameObjectLists.ElementAt(load);
            }
            else
            {
                helpers.levelPersistence.addLists(entities, gameObjects, levelName + chapterName);
            }

        }

        public override void unloadContent()
        {
            
        }

        public override helpers.levelReturn update(Microsoft.Xna.Framework.GameTime gameTime, helpers.playerControl controller)
        {
            helpers.levelPersistence.ChangedEntityThisFrame = false;

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects.ElementAt(i).Update();
            }

            for (int i = 0; i < entities.Count; i++)
            {
                entities.ElementAt(i).setCurrentlyColliding(false);
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
                g.Draw(sb, graphics.GraphicsDevice);
            }
            foreach (ggj14.entities.Entity e in entities)
            {
                e.Draw(sb, graphics.GraphicsDevice);
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
                            case "chapterwarp":
                                reader.Read();
                            {
                                int posx, posy;
                                string next = "";
                                posx = posy = 0;

                                while (reader.Name != "chapterwarp")
                                {
                                    switch (reader.Name)
                                    {
                                        case "warpposx":
                                            reader.Read();
                                            posx = Convert.ToInt32(reader.Value);
                                            reader.Read();
                                        break;
                                        case "warpposy":
                                            reader.Read();
                                            posy = Convert.ToInt32(reader.Value);
                                            reader.Read();
                                        break;
                                        case "chapterto":
                                            reader.Read();
                                            next = reader.Value;
                                            reader.Read();
                                        break;
                                    }
                                    reader.Read();
                                }
                                exitWaypoints.Add(new helpers.chapterWaypoint(posx, posy, chapter));
                            }
                            break;
                            case "gameobject":
                            {
                                reader.Read();
                                //Load game object data
                                int posx, posy, scale;
                                posx = posy = scale = 0;
                                string type = "";

                                while (reader.Name != "gameobject")
                                {
                                    switch (reader.Name)
                                    {
                                        case "type":
                                            reader.Read();
                                            type = reader.Value;
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
                                int goIndex = helpers.classLoading.findGameObjectDefinitionEntry(type);

                                string texture = "";
                                string sound = "";
                                if (goIndex >= 0)
                                {
                                    texture = helpers.classLoading.objectDefinitions.ElementAt(goIndex).texture;
                                    sound = helpers.classLoading.objectDefinitions.ElementAt(goIndex).interactionSound;
                                }

                                //create go
                                gameObjects.Add(helpers.classLoading.loadGameObject(texture, posx, posy, type));
                            }
                            break;
                            case "entity":
                            {
                                reader.Read();
                                //Load entity data
                                int posx, posy, scale;
                                string objType;
                                posx = 0;
                                posy = 0;
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
                                string texture = "";
                                string isound = "";
                                string ssound = "";
                                string bsound = "";
                                int entDefinition = helpers.classLoading.findEntityDefinitionEntry(objType);
                                if (entDefinition >= 0)
                                {
                                    texture = helpers.classLoading.entitiyDefinitions.ElementAt(entDefinition).texture;
                                    isound = helpers.classLoading.entitiyDefinitions.ElementAt(entDefinition).interactionSound;
                                    ssound = helpers.classLoading.entitiyDefinitions.ElementAt(entDefinition).stepSound;
                                    bsound = helpers.classLoading.entitiyDefinitions.ElementAt(entDefinition).backgroundMusic;
                                }
                                entities.Add(helpers.classLoading.loadEntity(texture, bsound, posx, posy, objType));
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
