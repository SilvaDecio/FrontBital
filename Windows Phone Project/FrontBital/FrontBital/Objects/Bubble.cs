using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FrontBital.Objects
{
    class Bubble
    {
        Texture2D Image;

        Vector2 Position , Speed;

        public Rectangle BoundingRectangle;

        //public Color[] Data;

        public Bubble(ContentManager Content , Vector2 position , Vector2 Direction)
        {
            Image = Content.Load<Texture2D>("Objects/Bubble");

            Position = position;

            BoundingRectangle = new Rectangle((int)Position.X, (int)Position.Y, Image.Width, Image.Height);

            Speed = new Vector2(8,8);
            Speed *= Direction;

            //Data = new Color[Image.Width * Image.Height];
            //Image.GetData(Data);
        }

        public void Update(GameTime gameTime)
        {
            Position += Speed;

            BoundingRectangle.X = (int)Position.X;
            BoundingRectangle.Y = (int)Position.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, Position, Color.White);
        }
    }
}