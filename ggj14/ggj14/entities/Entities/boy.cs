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
        public override void Update(Entity[] entityList, int objPosition)
        {
            for(int i = 0; i < entityList.Length; i++) 
            {
                

            }
            base.Update(entityList, objPosition);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public boy(Texture2D inTex, Vector2 inPos) : base (inTex, inPos)
        {
            this.texture = inTex;
            this.position = inPos;
            this.centre = new Vector2((this.position.X + texture.Width) / 2, (this.position.Y + texture.Height) / 2);
            this.facingLeft = false;
        }
    }
}
