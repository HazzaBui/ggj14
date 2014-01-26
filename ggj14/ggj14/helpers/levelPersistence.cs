using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using ggj14.helpers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ggj14.helpers
{
    static class levelPersistence
    {
        public static List<List<entities.Entity>> entityLists = new List<List<entities.Entity>>();
        public static List<List<entities.gameObject>> gameObjectLists = new List<List<entities.gameObject>>();
        public static List<string> chapter = new List<string>();


        public static void addLists(List<entities.Entity> e, List<entities.gameObject> go, string chapterName)
        {
            for (int i = 0; i < chapter.Count; i++)
            {
                if (chapter.ElementAt(i) == chapterName)
                    return;
            }
            chapter.Add(chapterName);
            entityLists.Add(e);
            gameObjectLists.Add(go);
        }

        public static int getChapterIndex(string name)
        {
            for (int i = 0; i < chapter.Count; i++)
            {
                if (chapter.ElementAt(i) == name)
                    return i;
            }
            return -1;
        }



    }
}
