using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ggj14.entities
{
    public class Entity
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 centre;
        protected bool facingLeft;
        protected bool isActivePlayer;


        public Vector2 getVelocity()
        {
            return velocity;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public Vector2 getCentre()
        {
            return centre;
        }

        public bool getIsActive()
        {
            return isActivePlayer;
        }

        public void setVelocity(Vector2 inVel)
        {
            velocity = inVel;
        }

        public void setPosition(Vector2 inPos)
        {
            position = inPos;
        }

        public void setTexture(Texture2D inTex)
        {
            texture = inTex;
        }

        public void setCentre(Vector2 inCentre)
        {
            centre = inCentre;
        }

        public void setIsActive(bool inIsActive)
        {
            isActivePlayer = inIsActive;
        }

        public Entity(Texture2D inTex, Vector2 inPos)
        {
            velocity = Vector2.Zero;
            facingLeft = false;
            texture = inTex;
            position = inPos;
            centre = new Vector2((position.X + texture.Width) / 2, (position.Y + texture.Height) / 2);
        }

        public virtual void Update()
        {
            this.centre = new Vector2((position.X + texture.Width) / 2, (position.Y + texture.Height) / 2);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, centre, null, Color.White);
        }
    }
}