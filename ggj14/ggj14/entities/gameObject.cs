using System;
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
        protected bool interactive;
        protected string objectType;

        public string getObjectType()
        {
            return objectType;
        }

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

        public void setObjectType(string inType)
        {
            objectType = inType;
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

        public gameObject(string inTex, Vector2 inPos, string inObjectType)
        {
            velocity = Vector2.Zero;
            position = inPos;
            textureString = inTex;
            objectType = inObjectType;
        }
        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White);
        }
    }
}
