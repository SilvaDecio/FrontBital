using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Decio.Joysticks.Analogic
{
    class BigPad
    {
        Texture2D Image;

        public Vector2 Position;

        float Scale;

        public BoundingSphere SphereCollision;

        public BigPad(Texture2D image , Vector2 position)
        {
            Image = image;

            Position = position;
                        
            Scale = 1;
            
            UpdateCollsion();
        }

        public void Update(GameTime gameTime)
        {
            UpdateCollsion();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, Position, null, Color.CornflowerBlue*0.6f, 0, Origin, Scale, SpriteEffects.None, 0);
        }

        public void Draw(SpriteBatch spriteBatch, float scale, float alpha)
        {
            spriteBatch.Draw(Image, Position, null, Color.CornflowerBlue * alpha, 0, Origin, scale, SpriteEffects.None, 0);
        }

        public void Draw(SpriteBatch spriteBatch,float scale)
        {
            spriteBatch.Draw(Image, Position, null, Color.CornflowerBlue * 0.6f, 0, Origin, scale, SpriteEffects.None, 0);
        }

        public void UpdateCollsion()
        {
            SphereCollision = new BoundingSphere(new Vector3(Position.X , Position.Y, 0), Origin.X);
        }

        public Vector2 Origin
        {
            get { return new Vector2(Image.Width / 2, Image.Height / 2); }
        }
    }
}