using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ggj14.helpers;

namespace ggj14.entities
{
    public class door : gameObject
    {
        string chapterTo;
        bool hasBeenUsed;

        public door(string inTex, Vector2 inPos, string inObjType, string inChapterTo) : base(inTex, inPos, inObjType)
        {
            this.textureString = inTex;
            this.position = inPos;
            this.velocity = Vector2.Zero;
            this.objectType = "door";
            this.chapterTo = inChapterTo;
            this.hasBeenUsed = false;
            scale = 2.0f;
        }

        
        public bool getIsGlowing()
        {
            return isGlowing;
        }

        public bool getHasBeenUsed()
        {
            return hasBeenUsed;
        }

        public void setHasBeenUsed(bool inHasBeenUsed)
        {
            hasBeenUsed = inHasBeenUsed;
        }

        public string getChapterTo()
        {
            return chapterTo;
        }
    }
}
