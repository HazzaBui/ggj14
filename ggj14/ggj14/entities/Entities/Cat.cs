using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ggj14.helpers;

namespace ggj14.entities.Entities
{
    public class Cat : Entity
    {

        public override void Update(Entity[] entityList, gameObject[] objectList, door[] doorList, int entPosition, playerControl controls)
        {
            Entity[] collidingEntities = new Entity[entityList.Length];
            int cEC = 0; //Colliding Entity Count

            gameObject[] collidingObjects = new gameObject[objectList.Length];
            int cOC = 0;

            this.velocity.Y += gravity;
            for (int i = 0; i < entityList.Length; i++)
            {
                Color[] entity1TextureData = new Color[this.texture.Width / numOfFrames * this.texture.Height];
                this.texture.GetData(0, new Rectangle(0, 0, this.texture.Width / numOfFrames, this.texture.Height), entity1TextureData, 0, entity1TextureData.Length);
                Color[] entity2TextureData = new Color[entityList[i].getTexture().Width / entityList[i].getFrameCount() * entityList[i].getTexture().Height];
                entityList[i].getTexture().GetData(0, new Rectangle(0, 0, entityList[i].getTexture().Width / entityList[i].getFrameCount(), entityList[i].getTexture().Height), entity2TextureData, 0, entity2TextureData.Length);
                Vector2 size1, size2;
                size1 = new Vector2(this.texture.Width / numOfFrames, this.texture.Height);
                size2 = new Vector2((float)(entityList[i].getTexture().Width / entityList[i].getFrameCount()), entityList[i].getTexture().Height);
                if (i != entPosition)
                {
                    bool colliding = checkCollision(this.position, entityList[i].getPosition(), size1, size2, entity1TextureData, entity2TextureData);
                    if (colliding && this.isActivePlayer)
                    {
                        collidingEntities[cEC] = entityList[i];
                        entityList[i].setCurrentlyColliding(true);
                    }
                }
            }

            for (int i = 0; i < objectList.Length; i++)
            {
                Color[] entity1TextureData = new Color[this.texture.Width / numOfFrames * this.texture.Height];
                this.texture.GetData(0, new Rectangle(0, 0, this.texture.Width / numOfFrames, this.texture.Height), entity1TextureData, 0, entity1TextureData.Length);
                Color[] entity2TextureData = new Color[objectList[i].getTexture().Width * objectList[i].getTexture().Height];
                objectList[i].getTexture().GetData(entity2TextureData);
                Vector2 size1, size2;
                size1 = new Vector2(this.texture.Width / numOfFrames, this.texture.Height);
                size2 = new Vector2(objectList[i].getTexture().Width, objectList[i].getTexture().Height);

                bool colliding = checkCollision(this.position, objectList[i].getPosition(), size1, size2, entity1TextureData, entity2TextureData);
                if (colliding && this.isActivePlayer)
                {
                    collidingObjects[cOC] = objectList[i];
                    objectList[i].setIsGlowing(true);
                }
            }

            for (int i = 0; i < doorList.Length; i++)
            {
                Color[] entity1TextureData = new Color[this.texture.Width / numOfFrames * this.texture.Height];
                this.texture.GetData(0, new Rectangle(0, 0, this.texture.Width / numOfFrames, this.texture.Height), entity1TextureData, 0, entity1TextureData.Length);
                Color[] entity2TextureData = new Color[doorList[i].getTexture().Width * doorList[i].getTexture().Height];
                doorList[i].getTexture().GetData(entity2TextureData);
                Vector2 size1, size2;
                size1 = new Vector2(this.texture.Width / numOfFrames, this.texture.Height);
                size2 = new Vector2(doorList[i].getTexture().Width, doorList[i].getTexture().Height);
                bool colliding = checkCollision(this.position, doorList[i].getPosition(), size1, size2, entity1TextureData, entity2TextureData);
                if (colliding)
                {
                    doorList[i].setIsGlowing(true);
                }
                else
                {
                    doorList[i].setIsGlowing(false);
                }
            }

            if (this.isActivePlayer)
            {
                /*if(controls.up)
                    this.velocity.Y -= 0.25f;
                if(controls.down)
                    this.velocity.Y += 0.25f;*/
                if (controls.left)
                {
                    if (!this.facingLeft)
                    {
                        this.facingLeft = true;
                        frameCount = 0;
                        //scale += 0.05f;
                    }
                    frameCount++;
                    this.velocity.X -= speed;
                }
                if (controls.right)
                {
                    if (this.facingLeft)
                    {
                        this.facingLeft = false;
                        frameCount = 0;
                        //scale += 0.05f;
                    }
                    frameCount++;
                    this.velocity.X += speed;

                }
                if (controls.use)
                {
                    foreach (door d in doorList)
                    {
                        if (d.getIsGlowing())
                        {
                            d.setHasBeenUsed(true);
                            this.audioTrackInstance.Stop();
                            break;
                        }
                    }
                    foreach (Entity cEnt in collidingEntities)
                    {
                        if (cEnt != null)
                        {
                            if (cEnt.getCurrentlyColliding() == true && cEnt.canBeSeen)
                            {
                                if (!helpers.levelPersistence.ChangedEntityThisFrame)
                                {
                                    cEnt.setIsActive(true);
                                    this.setIsActive(false);
                                    this.setVelocity(Vector2.Zero);
                                    helpers.levelPersistence.ChangedEntityThisFrame = true;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            if (frameCount >= (10 * numOfFrames) || (!controls.right && !controls.left))
            {
                frameCount = 0;
            }
            textRect = new Rectangle(this.texture.Width / numOfFrames * (int)(frameCount / 10), 0, this.texture.Width / numOfFrames, this.texture.Height);

            foreach (gameObject gObj in collidingObjects)
            {
                if (gObj != null)
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

            if (this.position.Y > (400 - (this.texture.Height * scale)))
            {
                this.position.Y = (400 - (this.texture.Height * scale));
                this.velocity.Y = 0;

            }
            if (this.position.X < -20 + (this.texture.Width * (scale - 1)))
            {
                this.velocity.X = 0;
                this.position.X = -20 + (this.texture.Width * (scale - 1));
            }
            if (this.position.X > 734)
            {
                this.velocity.X = 0;
                this.position.X = 734;
            }
            this.position += this.velocity;
            this.velocity *= 0.9f;
            if (audioTrackInstance.State != Microsoft.Xna.Framework.Audio.SoundState.Playing && this.isActivePlayer)
                audioTrackInstance.Play();
            if (!this.isActivePlayer)
                this.audioTrackInstance.Pause();
            canBeSeen = false;
            foreach (Entity e in entityList)
            {
                if (e.getIsActive())
                {
                    foreach (string s in e.viewableEntities)
                    {
                        if (s == classType)
                            canBeSeen = true;
                    }
                }
            }
            base.Update(entityList, objectList, doorList, entPosition, controls);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, GraphicsDevice device)
        {
            if (currentlyColliding && !isActivePlayer && canBeSeen)
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

                if (!facingLeft)
                    spriteBatch.Draw(newText, new Vector2(position.X - texture.Width / (4 * numOfFrames), position.Y - texture.Height / 4), textRect, Color.Yellow, 0.0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0.8f);
                else
                    spriteBatch.Draw(newText, new Vector2(position.X - texture.Width / (4 * numOfFrames), position.Y - texture.Height / 4), textRect, Color.Yellow, 0.0f, Vector2.Zero, 1.5f, SpriteEffects.FlipHorizontally, 0.8f);
            }

            Color drawColour;
            if (canBeSeen)
                drawColour = Color.White;
            else
                drawColour = Color.Black;

            float depth = 0.5f;
            if (isActivePlayer)
            {
                depth = 0.0f;
            }
            if (facingLeft)
                spriteBatch.Draw(texture, this.position, textRect, drawColour, 0, Vector2.Zero, scale, SpriteEffects.FlipHorizontally, depth);
            else
                spriteBatch.Draw(texture, this.position, textRect, drawColour, 0, Vector2.Zero, scale, SpriteEffects.None, depth);
        }

        public Cat(Vector2 inPos, string inTexString, string inAudioString)
            : base(inPos, inTexString, inAudioString)
        {
            this.audioString = inAudioString;
            this.position = inPos;
            //this.centre = new Vector2((this.position.X + texture.Width) / 2, (this.position.Y + texture.Height) / 2);
            this.facingLeft = false;
            this.isActivePlayer = false;
            int frameWidth = 64;
            int frameHeight = 64;
            frameCount = 0;
            numOfFrames = 2;
            speed = 0.25f;
            gravity = 1.0f;
            classType = "Cat";

            viewableEntities = new List<string>();
            viewableEntities.Add("Mouse");
            viewableEntities.Add("Boy");
            viewableEntities.Add("Cat");
            viewableEntities.Add("Bird");

            textRect = new Rectangle(0, 0, frameWidth, frameHeight);
        }
    }
}
