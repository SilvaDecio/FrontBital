using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Microsoft.Phone;
using Microsoft.Devices;

namespace FrontBital.Objects
{
    class BalloonManager
    {
        Random RND;

        public List<Balloon> List;

        List<TypeBalloon> Types;

        float ElapsedTime , AwardedElapsedTime , TotalTime;

        public float Interval , AwardedInterval;

        ContentManager Content;

        public BalloonManager(ContentManager content)
        {
            Content = content;

            List = new List<Balloon>();

            Interval = 7000f;

            AwardedInterval = 7500f;

            
            TotalTime = 0f;

            ElapsedTime = 0f;

            AwardedElapsedTime = 0f;


            Types = new List<TypeBalloon>()
            {
                TypeBalloon.Blue,TypeBalloon.Green,TypeBalloon.Red
            };

            RND = new Random();
        }

        public void Update(GameTime gameTime)
        {
            ElapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            AwardedElapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            if (ElapsedTime >= Interval)
            {
                if (AwardedElapsedTime >= AwardedInterval)
                {
                    int sorteio = RND.Next(1, 11);

                    if (sorteio < 5)
                    {
                        List.Add(new Balloon(Content, Types[RND.Next(0, Types.Count)], true));
                    }
                    else
                    {
                        List.Add(new Balloon(Content, Types[RND.Next(0, Types.Count)], false));
                    }

                    AwardedElapsedTime = 0f;
                }
                else
                {
                    List.Add(new Balloon(Content, Types[RND.Next(0, Types.Count)], false));
                }

                ElapsedTime = 0f;
            }

            for (int i = 0; i < List.Count; i++)
            {
                List[i].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < List.Count; i++)
            {
                List[i].Draw(spriteBatch);
            }
        }
    }
}