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
        int chapterTo;

        public door(string inTex, Vector2 inPos, string inObjType, int inChapterTo) : base(inTex, inPos, inObjType)
        {
            this.textureString = inTex;
            this.position = inPos;
            this.velocity = Vector2.Zero;
            this.objectType = "door";
            this.chapterTo = inChapterTo;
        }
    }
}
