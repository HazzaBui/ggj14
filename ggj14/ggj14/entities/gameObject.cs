using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ggj14.entities
{
    public class gameObject
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 centre;

        public Vector2 getPosition()
        {
            return position;
        }

        public Vector2 getVelocity()
        {
            return velocity;
        }

        public void setPosition(Vector2 inPos)
        {
            position = inPos;
        }

        public void setVelocity(Vector2 inVel)
        {
            velocity = inVel;
        }

        public gameObject(Texture2D inTex, Vector2 inPos)
        {
            velocity = Vector2.Zero;
            position = inPos;
            texture = inTex;
            this.centre = new Vector2((inPos.X + texture.Width) / 2, (inPos.Y + texture.Height) / 2);
        }
    }
}
