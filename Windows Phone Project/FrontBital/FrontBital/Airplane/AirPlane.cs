using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;

using Microsoft.Phone;

using FrontBital.Objects;
using FrontBital.BaseClasses;

namespace FrontBital
{
    class AirPlane
    {
        Texture2D Image;

        Vector2 Position , MaximumSpeed;

        public Vector2 Speed;

        
        public Rectangle BoundingRectangle;

        public AngulatedRectangle BoundingRectangle_Angulated;

        public Color[] Data;
        
        
        public float Angle , TargetAngle;

        Viewport Screen;
                

        public int Points;
        public EnergyBar SuccessBar;

        public List<Bubble> Bubbles;

        public bool IsShooting , WasShooting;

        public static Vector2 LeftButton = new Vector2(35 , 370);
        public static Vector2 RightButton = new Vector2(675 , 370);

        public static Vector2 LeftAnalog = new Vector2(100, 380);
        public static Vector2 RightAnalog = new Vector2(680, 380);

        public AirPlane(ContentManager Content, Viewport screen)
        {
            Screen = screen;

            Image = Content.Load<Texture2D>("AirPlane/AirPlane");

            Position = new Vector2(Screen.Bounds.Center.X - Image.Width / 2 ,
                Screen.Bounds.Center.Y - Image.Height / 2);

            
            BoundingRectangle = new Rectangle((int)Position.X, (int)Position.Y, Image.Width, Image.Height);

            Angle = 0f;

            TargetAngle = 0f;

            BoundingRectangle_Angulated = new AngulatedRectangle(BoundingRectangle, Angle);

            Data = new Color[Image.Width * Image.Height];
            Image.GetData(Data);


            Speed = new Vector2();

            MaximumSpeed = new Vector2(4f, 4f);


            Points = 0;

            SuccessBar = new EnergyBar(Content);

            Bubbles = new List<Bubble>();

            IsShooting = false;
            WasShooting = false;
        }

        public void Update(GameTime gameTime , bool Accelerate)
        {
            # region Acceleration

            if (Accelerate)
	        {
	        	 Speed += new Vector2(0.1f , 0.1f);
	        }
            else
            {
                Speed -= new Vector2(0.05f, 0.05f);
            }

            Speed.X = MathHelper.Clamp(Speed.X, 0.0f, MaximumSpeed.X);
            Speed.Y = MathHelper.Clamp(Speed.Y, 0.0f, MaximumSpeed.Y);

            # endregion

            # region Getting Angle to Move
            
            float Distance = Angle - TargetAngle;

            if (Distance > Math.PI)
            {
                Angle -= MathHelper.TwoPi;
            }
            if (Distance < -Math.PI)
            {
                Angle += MathHelper.TwoPi;
            }
            if (Angle < TargetAngle)
            {
                Angle += 0.125f;
            }

            if (Angle > TargetAngle)
            {
                Angle -= 0.125f;
            }

            # endregion

            Position.X -= (float)-(Math.Cos(Angle) * Speed.X);
            Position.Y -= (float)-(Math.Sin(Angle) * Speed.Y);

            Position.X = MathHelper.Clamp(Position.X, 0, Screen.Width - Image.Width);
            Position.Y = MathHelper.Clamp(Position.Y, 50, Screen.Height - Image.Height);

            BoundingRectangle.X = (int)Position.X;
            BoundingRectangle.Y = (int)Position.Y;

            BoundingRectangle_Angulated.X = BoundingRectangle.X;
            BoundingRectangle_Angulated.Y = BoundingRectangle.Y;

            BoundingRectangle_Angulated.Rotation = Angle;
            BoundingRectangle_Angulated.CalculateOrigin();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle abc = new Rectangle(BoundingRectangle.X + BoundingRectangle.Width / 2, BoundingRectangle.Y + BoundingRectangle.Height / 2,
                BoundingRectangle.Width, BoundingRectangle.Height);

            spriteBatch.Draw(Image, abc, null, Color.White, Angle, new Vector2(Image.Width / 2, Image.Height / 2),
                    SpriteEffects.None, 0f);
            
            SuccessBar.Draw(spriteBatch);
        }

        public void ThrowBubble(ContentManager Content)
        {
            if (!WasShooting && IsShooting)
            {
                Bubbles.Add(new Bubble(Content, Position,
                    new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle))));   
            }
        }
    }
}