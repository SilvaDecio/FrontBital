using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Decio.Joysticks.Analogic
{
    class MediumPad
    {
        Texture2D Image;

        public Vector2 Position; 
        
        public MediumPad(Texture2D image, Vector2 position)
        {
            Image = image;
            Position = position;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, Position,null, Color.CornflowerBlue*0.6f, 0, Origin, 1f, SpriteEffects.None, 0f);
        }

        public void Draw(SpriteBatch spriteBatch,float scale)
        {
            spriteBatch.Draw(Image, Position, null, Color.CornflowerBlue *0.6f, 0, Origin, scale, SpriteEffects.None, 0f);
        }

        public void Draw(SpriteBatch spriteBatch, float scale, float alpha)
        {
            spriteBatch.Draw(Image, Position, null, Color.CornflowerBlue * alpha, 0, Origin, scale, SpriteEffects.None, 0);
        }

        public Vector2 Origin
        {
            get { return new Vector2(Image.Width / 2, Image.Height / 2); }
        }
    }
}