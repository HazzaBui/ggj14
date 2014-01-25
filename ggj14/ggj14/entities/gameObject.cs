﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace ggj14.entities
{
    public class gameObject
    {
        protected Texture2D texture;
        string textureString;
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 centre;
        protected bool interactive;

        public Vector2 getPosition()
        {
            return position;
        }

        public Vector2 getVelocity()
        {
            return velocity;
        }

        public bool getInteractive()
        {
            return interactive;
        }

        public void setPosition(Vector2 inPos)
        {
            position = inPos;
        }

        public void setVelocity(Vector2 inVel)
        {
            velocity = inVel;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public void setInteractive(bool isInteractive)
        {
            interactive = isInteractive;
        }

        public void LoadContent(ContentManager cm)
        {
            texture = cm.Load<Texture2D>(textureString);
        }

        public gameObject(string inTex, Vector2 inPos)
        {
            velocity = Vector2.Zero;
            position = inPos;
            textureString = inTex;
            this.centre = new Vector2((inPos.X + texture.Width) / 2, (inPos.Y + texture.Height) / 2);
        }
        public void Update()
        {
            this.centre = new Vector2((position.X + texture.Width) / 2, (position.Y + texture.Height) / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, centre, null, Color.White);
        }
    }
}
