using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Decio.Joysticks.Analogic
{
    class AnalogicJoypad
    {
        MiniPad MiniButton;

        MediumPad MediumButton;
        
        BigPad BigButton;

        private Vector2 Position;

        public Vector2 position
        {
            get { return Position; }
            set 
            {
                Position = value;

                DefaultPosition = Position;

                MiniButton.Position = Position;

                MediumButton.Position = Position;

                BigButton.Position = Position;

                MaxDistance = MediumButton.Origin.X - MiniButton.Origin.X;
            }
        }

        Vector2 DefaultPosition;

        public float Rotation , Distance , MaxDistance;
        
        public bool Ative;
        
        public AnalogicJoypad(ContentManager Content , Vector2 position)
        {
            Position = position;
            
            DefaultPosition = Position;

            MiniButton = new MiniPad(Content.Load<Texture2D>("AirPlane/Analogic/MiniPad"), Position);

            MediumButton = new MediumPad(Content.Load<Texture2D>("AirPlane/Analogic/MediumPad"), Position);

            BigButton = new BigPad(Content.Load<Texture2D>("AirPlane/Analogic/BigPad"), Position);

            Distance = 35;

            MaxDistance = MediumButton.Origin.X - MiniButton.Origin.X;
        }

        public void Update(GameTime gameTime , List<Vector2> Positions)
        {
            MiniButton.Position = BigButton.Position;

            BigButton.Update(gameTime);

            ChangePosition(Positions);

            if (Positions.Count < 1)
            {
                Ative = false;

                ResumePosition();
            }
        }
        
        private void ResumePosition()
        {
            MiniButton.Position = BigButton.Position;
        }
        
        public void GetRotation(Vector2 PosCursor)
        {
          Vector2 Distances = BigButton.Position - PosCursor;
       
          Rotation = (float)Math.Atan2(Distances.X, -Distances.Y);

          Rotation = Rotation + (float)(Math.PI / 2);

        }

        public void ChangePosition(List<Vector2> Positions)
        {
            for (int i = 0; i < Positions.Count; i++)
            {
                if (GetCollisionM(Positions[i]).Intersects(BigButton.SphereCollision))
                {
                    Ative = true;

                    GetRotation(Positions[i]);

                    MiniButton.Position = new Vector2(BigButton.Position.X + (float)(Math.Cos(Rotation) * MaxDistance),
                        BigButton.Position.Y + (float)(Math.Sin(Rotation) * MaxDistance));

                    return;
                }
            }

            Ative = false;
        }

        public BoundingBox GetCollisionM(Vector2 PosCursor)
        {
            return new BoundingBox(new Vector3(PosCursor.X, PosCursor.Y, 0), new Vector3(PosCursor.X, PosCursor.Y, 0));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            BigButton.Draw(spriteBatch);

            MediumButton.Draw(spriteBatch);
            
            MiniButton.Draw(spriteBatch);
        }

        public void Draw(SpriteBatch spriteBatch,float scale,float alpha)
        {
            BigButton.Draw(spriteBatch, scale,alpha);
            
            MediumButton.Draw(spriteBatch, scale,alpha);
            
            MiniButton.Draw(spriteBatch, scale,alpha);
        }

        public void Draw(SpriteBatch spriteBatch,float scale)
        {
            BigButton.Draw(spriteBatch,scale);

            MediumButton.Draw(spriteBatch,scale);
            
            MiniButton.Draw(spriteBatch,scale);
        }       
    }
}