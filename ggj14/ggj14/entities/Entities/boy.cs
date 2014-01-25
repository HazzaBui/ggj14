using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ggj14.entities.Entities
{
    public class boy : Entity
    {
        public override void Update(Entity[] entityList, gameObject[] objectList, int entPosition)
        {
            Entity[] collidingEntities = new Entity[entityList.Length];
            int cEC = 0; //Colliding Entity Count

            gameObject[] collidingObjects = new gameObject[objectList.Length];
            int cOC = 0;

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
                Color[] entity1TextureData = new Color[this.texture.Width * this.texture.Height];
                this.texture.GetData(entity1TextureData);
                Color[] entity2TextureData = new Color[objectList[i].getTexture().Width * objectList[i].getTexture().Height];
                objectList[i].getTexture().GetData(entity2TextureData);
                Vector2 size1, size2;
                size1 = new Vector2(this.texture.Width, this.texture.Height);
                size2 = new Vector2(objectList[i].getTexture().Width, objectList[i].getTexture().Height);

                bool colliding = checkCollision(this.position, objectList[i].getPosition(), size1, size2, entity1TextureData, entity2TextureData);
                if (colliding)
                {
                    collidingObjects[cOC] = objectList[i];
                }
            }

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
            }
            base.Update(entityList, objectList, entPosition);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public boy(Vector2 inPos, string inTexString) : base (inPos, inTexString)
        {
            this.position = inPos;
            this.centre = new Vector2((this.position.X + texture.Width) / 2, (this.position.Y + texture.Height) / 2);
            this.facingLeft = false;
            int frameWidth = 64;
            int frameHeight = 64;

            Rectangle sourceRect = new Rectangle(0, 0, frameWidth, frameHeight);
            for (int i = 0; i < 3; i++)
            {
                //animationFrames[i] = texture.GetData<Texture2D>(1, sourceRect, texture, 0, 3);
            }
        }
    }
}
