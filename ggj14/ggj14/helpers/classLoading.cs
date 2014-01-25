using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ggj14.helpers
{
    public static class classLoading
    {
        public static List<entityDefinition> entitiyDefinitions = new List<entityDefinition>();

        public static entities.Entity loadEntity(string texture, int posx, int posy, string objectType)
        {

            switch (objectType)
            {
                case "boy":
                    return new entities.Entities.boy(new Microsoft.Xna.Framework.Vector2(posx, posy), texture);


            }

            return null;
        }

        public static entities.gameObject loadGameObject(string texture, int posx, int posy)
        {

            return new entities.gameObject(texture, new Microsoft.Xna.Framework.Vector2(posx, posy));
        }

        public static void loadEntityDefinitions(string file)
        {
            XmlTextReader reader = new XmlTextReader(file);

            while (reader.Read())
            {
                if (reader.Name == "entity")
                {
                    string name = "";
                    string texture = "";
                    string interactivesound = "";
                    string stepsound = "";
                    string backgroundmusic = "";

                    reader.Read();
                    while (reader.Name != "entity")
                    {
                        switch (reader.Name)
                        {
                            case "entityname":
                                reader.Read();
                                name = reader.Value;
                                reader.Read();
                                break;
                            case "texture":
                                reader.Read();
                                texture = reader.Value;
                                reader.Read();
                                break;
                            case "interactivesound":
                                reader.Read();
                                interactivesound = reader.Value;
                                reader.Read();
                                break;
                            case "stepsound":
                                reader.Read();
                                stepsound = reader.Value;
                                reader.Read();
                                break;
                            case "backgroundmusic":
                                reader.Read();
                                backgroundmusic = reader.Value;
                                reader.Read();
                                break;
                        }
                        reader.Read();
                    }
                    entitiyDefinitions.Add(new entityDefinition(name, texture, interactivesound, stepsound, backgroundmusic));
                }
            }
        }

        public static int findEntityDefinitionEntry(string name)
        {
            for (int i = 0; i < entitiyDefinitions.Count; i++)
            {
                if (entitiyDefinitions.ElementAt(i).identifier == name)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
