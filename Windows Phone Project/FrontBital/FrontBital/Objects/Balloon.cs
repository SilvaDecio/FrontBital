using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using FrontBital.Animation;

namespace FrontBital.Objects
{
    public enum TypeBalloon
    {
        Red, Green, Blue
    }

    class Balloon
    {
        Texture2D Image , Image_Bubble;

        Vector2 Position;

        public float Speed;


        public Rectangle BoundingRectangle;
        public Color[] Data;


        public int PointsSave;
        public int PointsEscape;
        
        public TypeBalloon Type;

        public bool HasBubble , Premiado;

        Random rdm;

        public Balloon(ContentManager Content , TypeBalloon type , bool premiado)
        {
            rdm = new Random();

            Premiado = premiado;

            Type = type;

            # region Type

            switch (Type)
            {
                case TypeBalloon.Red:
                    
                    if (Premiado)
                    {
                        Image = Content.Load<Texture2D>("Objects/Balloons/Red/AwardedRed");

                        Image_Bubble = Content.Load<Texture2D>("Objects/Balloons/Red/AwardedRed_Bubble");
                    }
                    else
                    {
                        Image = Content.Load<Texture2D>("Objects/Balloons/Red/Red");

                        Image_Bubble = Content.Load<Texture2D>("Objects/Balloons/Red/Red_Bubble");
                    }

                    PointsSave = 75;
                    PointsEscape = 15;

                    break;

                case TypeBalloon.Green:

                    if (Premiado)
                    {
                        Image = Content.Load<Texture2D>("Objects/Balloons/Green/AwardedGreen");

                        Image_Bubble = Content.Load<Texture2D>("Objects/Balloons/Green/AwardedGreen_Bubble");
                    }
                    else
                    {
                        Image = Content.Load<Texture2D>("Objects/Balloons/Green/Green");

                        Image_Bubble = Content.Load<Texture2D>("Objects/Balloons/Green/Green_Bubble");
                    }
                    
                    PointsSave = 50;
                    PointsEscape = 10;

                    break;

                case TypeBalloon.Blue:

                    if (Premiado)
                    {
                        Image = Content.Load<Texture2D>("Objects/Balloons/Blue/AwardedBlue");

                        Image_Bubble = Content.Load<Texture2D>("Objects/Balloons/Blue/AwardedBlue_Bubble");
                    }
                    else
                    {
                        Image = Content.Load<Texture2D>("Objects/Balloons/Blue/Blue");

                        Image_Bubble = Content.Load<Texture2D>("Objects/Balloons/Blue/Blue_Bubble");
                    }

                    PointsSave = 25;
                    PointsEscape = 5;

                    break;
            }

            # endregion

            Position = new Vector2( rdm.Next(0 , 800 - Image.Width) , - Image.Height);
            
            BoundingRectangle = new Rectangle((int)Position.X, (int)Position.Y, Image.Width, Image.Height);

            Data = new Color[Image.Width * Image.Height];
            Image.GetData(Data);

            Speed = 2f;
            
            HasBubble = false;

            //Data = new Color[Image.Width * Image.Height];
            //Image.GetData(Data);
        }

        public void Update(GameTime gameTime)
        {
            Position.Y += Speed;
            
            BoundingRectangle.Y = (int)Position.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (HasBubble)
            {
                spriteBatch.Draw(Image_Bubble, Position, Color.White);
            }
            else
            {
                spriteBatch.Draw(Image, Position, Color.White);
            }            
        }

        public void Capture()
        {
            HasBubble = true;
            Speed = 1.5f;

            BoundingRectangle = new Rectangle((int)Position.X, (int)Position.Y, Image_Bubble.Width, Image_Bubble.Height);

            Data = new Color[Image_Bubble.Width * Image_Bubble.Height];
            Image_Bubble.GetData(Data);
        }
    }
}