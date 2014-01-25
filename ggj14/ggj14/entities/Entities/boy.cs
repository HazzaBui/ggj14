using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ggj14.helpers;

namespace ggj14.entities.Entities
{
    public class boy : Entity
    {
        public override void Update(Entity[] entityList, gameObject[] objectList, int entPosition, playerControl controls)
        {
            Entity[] collidingEntities = new Entity[entityList.Length];
            int cEC = 0; //Colliding Entity Count

            gameObject[] collidingObjects = new gameObject[objectList.Length];
            int cOC = 0;

            this.velocity.Y += 0.1f;
            for(int i = 0; i < entityList.Length; i++) 
            {
               Color[] entity1TextureData = new Color[this.texture.Width * this.texture.Height];
                this.texture.GetData(entity1TextureData);
                Color[] entity2TextureData = new Color[entityList[i].getTexture().Width * entityList[i].getTexture().Height];
                entityList[i].getTexture().GetData(entity2TextureData);
                Vector2 size1, size2;
                size1 = new Vector2(this.texture.Width, this.texture.Height);
                size2 = new Vector2(entityList[i].getTexture().Width, entityList[i].getTexture().Height);
                if (i != entPosition)
                {
                    bool colliding = checkCollision(this.position, entityList[i].getPosition(), size1, size2, entity1TextureData, entity2TextureData);
                    if (colliding)
                    {
                        collidingEntities[cEC] = entityList[i];
                        entityList[i].setCurrentlyColliding(true);
                    }
                    else
                    {
                        entityList[i].setCurrentlyColliding(false);
                    }
                }
                else
                    entityList[i].setCurrentlyColliding(false);
            }

            for (int i = 0; i < objectList.Length; i++)
            {
                /*Color[] entity1TextureData = new Color[this.texture.Width * this.texture.Height];
                this.texture.GetData(entity1TextureData);*/
                Color[] entity1TextureData = new Color[64 * 64];
                this.texture.GetData(0, new Rectangle(0, 0, 64, 64), entity1TextureData, 0, 64 * 64);
                Color[] entity2TextureData = new Color[objectList[i].getTexture().Width * objectList[i].getTexture().Height];
                objectList[i].getTexture().GetData(entity2TextureData);
                Vector2 size1, size2;
                size1 = new Vector2(this.texture.Width / 4, this.texture.Height);
                size2 = new Vector2(objectList[i].getTexture().Width, objectList[i].getTexture().Height);

                bool colliding = checkCollision(this.position, objectList[i].getPosition(), size1, size2, entity1TextureData, entity2TextureData);
                if (colliding)
                {
                    collidingObjects[cOC] = objectList[i];
                }
            }

            if (this.isActivePlayer)
            {
                if(controls.up)
                    this.velocity.Y -= 0.25f;
                if(controls.down)
                    this.velocity.Y += 0.25f;
                if (controls.left)
                {
                    if (!this.facingLeft)
                    {
                        this.facingLeft = true;
                        frameCount = 0;
                    }
                    frameCount++;
                    this.velocity.X -= 0.25f;
                }
                if (controls.right)
                {
                    if (this.facingLeft)
                    {
                        this.facingLeft = false; 
                        frameCount = 0;
                    }
                    frameCount++;
                    this.velocity.X += 0.25f;

                }
                if (controls.use)
                {
                    frameCount++;
                }
            }
            if (frameCount > 39 || (!controls.right && !controls.left))
            {
                frameCount = 0;
            }
            textRect = new Rectangle(64 * (int)(frameCount / 10), 0, 64, 64);
            if (collidingObjects[0] != null)
            {
                foreach (gameObject gObj in collidingObjects)
                {
                    if ((gObj.getPosition().X < this.position.X) && this.velocity.X < 0 && (gObj.getInteractive() == false))
                    {
                        this.velocity.X = 0;
                    }
                    if ((gObj.getPosition().X > this.position.X) && this.velocity.X > 0 && (gObj.getInteractive() == false))
                    {
                        this.velocity.X = 0;
                    }
                    if ((gObj.getPosition().Y < this.position.Y) && this.velocity.Y < 0 && (gObj.getInteractive() == false))
                    {
                        this.velocity.Y = 0;
                    }
                    if ((gObj.getPosition().Y > this.position.Y) && this.velocity.Y > 0 && (gObj.getInteractive() == false))
                    {
                        this.velocity.Y = 0;
                    }
                }
            }
            if (this.position.Y > 336)
            {
                this.position.Y = 336;
                this.velocity.Y = 0;

            }
            if (this.position.X < -20)
            {
                this.velocity.X = 0;
                this.position.X = -20;
            }
            if (this.position.X > 734)
            {
                this.velocity.X = 0;
                this.position.X = 734;
            }
            this.position += this.velocity;
            this.velocity *= 0.96f;
            //base.Update(entityList, objectList, entPosition, controls);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (!facingLeft)
                spriteBatch.Draw(texture, position, textRect, Color.White);
            else
                spriteBatch.Draw(texture, this.position, textRect, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
        }

        public boy(Vector2 inPos, string inTexString) : base (inPos, inTexString)
        {
            this.position = inPos;
            //this.centre = new Vector2((this.position.X + texture.Width) / 2, (this.position.Y + texture.Height) / 2);
            this.facingLeft = false;
            this.isActivePlayer = true;
            int frameWidth = 64;
            int frameHeight = 64;
            frameCount = 0;

            textRect = new Rectangle(0, 0, frameWidth, frameHeight);
        }
    }
}
