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
        protected string textureString;
        protected Vector2 position;
        protected Vector2 velocity;
        protected bool interactive;
        protected string objectType;
        protected bool isGlowing;

        public void setIsGlowing(bool glow)
        {
            isGlowing = glow;
        }

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

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice device)
        {
            if (isGlowing)
            {
                Color[] oldColArray = new Color[texture.Width * texture.Height];
                texture.GetData(0, new Rectangle(0, 0, this.texture.Width, this.texture.Height), oldColArray, 0, oldColArray.Length);
                for (int i = 0; i < texture.Width; i++)
                {
                    for (int j = 0; j < texture.Height; j++)
                    {
                        if (oldColArray[i + j * texture.Width] != Color.Transparent)
                            oldColArray[i + j * texture.Width] = Color.White;
                    }
                }

                Texture2D newText = new Texture2D(device, texture.Width, texture.Height);
                newText.SetData<Color>(oldColArray);

                spriteBatch.Draw(newText, new Vector2(position.X - texture.Width / 4, position.Y - texture.Height / 4), null, Color.Yellow, 0.0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0.8f);
            }
            spriteBatch.Draw(texture, position, null, Color.White);
        }
    }
}
