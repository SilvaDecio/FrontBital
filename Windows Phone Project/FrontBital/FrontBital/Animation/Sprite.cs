using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrontBital.Animation
{
    class Sprite
    {
        Texture2D Sheet;

        public Rectangle IdleRectangle;
        
        public Rectangle AnimationRectangle;
        
        int CurrentFrame , FramesNumber;
        
        float TimeOfTransition , ElapsedTime;
        
        SpriteSheetPacker Reading;
        
        string PreFix;

        public Sprite(Texture2D sheet, float timeoftransition , string Path , string prefix)
        {
            Reading = new SpriteSheetPacker(Path);

            PreFix = prefix;
            
            Sheet = sheet;

            FramesNumber = Reading.SpriteSourceRectangles.Count;
            
            TimeOfTransition = timeoftransition;
            
            AnimationRectangle = new Rectangle();
            
            IdleRectangle = Reading.SpriteSourceRectangles[PreFix + CurrentFrame];

            Restart();
        }

        public void Restart()
        {
            ElapsedTime = 0f;
            CurrentFrame = 1;
        }

        public void Update(GameTime gameTime)
        {
            ElapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            
            if (ElapsedTime >= TimeOfTransition)
            {
                ElapsedTime = 0f;
                ++CurrentFrame;
                if (CurrentFrame >= FramesNumber)
                {
                    CurrentFrame = 1;
                }
            }

            AnimationRectangle = Reading.SpriteSourceRectangles[PreFix + CurrentFrame];
        }

        public void Draw(SpriteBatch spriteBatch , Vector2 Position)
        {
            spriteBatch.Draw(Sheet, Position, AnimationRectangle, Color.White);
        }

        public void DrawIdle(SpriteBatch spriteBatch, Vector2 Position)
        {
            spriteBatch.Draw(Sheet, Position, IdleRectangle, Color.White);
        }
    }
}