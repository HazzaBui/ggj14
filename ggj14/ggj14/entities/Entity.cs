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
        protected bool currentlyColliding;

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

        public bool getCurrentlyColliding()
        {
            return currentlyColliding;
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

        public void setCurrentlyColliding(bool inIsColl)
        {
            currentlyColliding = inIsColl;
        }

        public Entity(Texture2D inTex, Vector2 inPos)
        {
            velocity = Vector2.Zero;
            facingLeft = false;
            texture = inTex;
            position = inPos;
            isActivePlayer = false;
            currentlyColliding = false
            centre = new Vector2((position.X + texture.Width) / 2, (position.Y + texture.Height) / 2);
        }

        public virtual void Update(Entity[] entityList, gameObject[] objectList, int objPosition)
        {
            //this.centre = new Vector2((position.X + texture.Width) / 2, (position.Y + texture.Height) / 2);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, centre, null, Color.White);
        }

        protected bool checkCollision(Vector2 entityPos1, Vector2 entityPos2, Vector2 entitySize1, Vector2 entitySize2, Color[] color1, Color[] color2)
        {
            double distance = Math.Sqrt(Math.Pow(Math.Abs(entityPos1.X - entityPos2.X), 2) + Math.Pow(Math.Abs(entityPos1.Y - entityPos2.Y), 2));
            if (distance * distance > (Math.Pow(entitySize1.X, 2) + Math.Pow(entitySize1.Y, 2)) + (Math.Pow(entitySize2.X, 2) + Math.Pow(entitySize2.Y, 2)))
                return false;
            else
            {
                Rectangle rect1, rect2;
                rect1 = new Rectangle((int)entityPos1.X, (int)entityPos1.Y, (int)entitySize1.X, (int)entitySize1.Y);
                rect2 = new Rectangle((int)entityPos2.X, (int)entityPos2.Y, (int)entitySize2.X, (int)entitySize2.Y);
                int top = Math.Max(rect1.Top, rect2.Top);
                int bottom = Math.Min(rect1.Bottom, rect2.Bottom);
                int left = Math.Max(rect1.Left, rect2.Left);
                int right = Math.Min(rect1.Right, rect2.Right);

                // Check every point within the intersection bounds
                for (int y = top; y < bottom; y++)
                {
                    for (int x = left; x < right; x++)
                    {
                        // Get the color of both pixels at this point
                        Color colorA = color1[(x - rect1.Left) +
                                             (y - rect1.Top) * rect1.Width];
                        Color colorB = color2[(x - rect2.Left) +
                                             (y - rect2.Top) * rect2.Width];

                        // If both pixels are not completely transparent,
                        if (colorA.A != 0 && colorB.A != 0)
                        {
                            // then an intersection has been found
                            return true;
                        }
                    }
                }

                // No intersection found
                return false;

            }
        }
    }
}