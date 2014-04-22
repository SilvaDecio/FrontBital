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
using Microsoft.Xna.Framework.Input.Touch;

using FrontBital.BaseClasses;
using FrontBital.DataBase;
using FrontBital.Objects;

using Decio.Joysticks.Analogic;

namespace FrontBital.Management
{
    public enum Order
    {
        Normal , Changed
    }

    class GamePlay : State
    {
        AirPlane Jogador;

        BalloonManager Balloons;

        float BalloonInterval, BalloonElapsedTime;


        Texture2D AlertBar;
        bool IsCritical, IsMoment;
        float CriticalTime;



        SoundEffect BubbleEffect , ExplosionEffect;

        
        
        public static Button BubbleButton;

        public static AnalogicJoypad Analog;

        public static Order CurrentOrder;

        
        TouchCollection Touches;

        public GamePlay(StateManager Father)
        {
            Manager = Father;

            # region Language

            switch (StateManager.CurrentLanguage)
            {
                case GameLanguage.English:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/GamePlay");

                    break;

                case GameLanguage.Portugues:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Jogando");

                    break;
            }

            # endregion

            AlertBar = Manager.Game.Content.Load<Texture2D>("AirPlane/EnergyBar/AlertBar");

            # region Order

            switch (CurrentOrder)
            {
                case Order.Normal:

                    BubbleButton = new Button(Manager.Game.Content.Load<Texture2D>("AirPlane/BubbleButton"), AirPlane.LeftButton);

                    Analog = new AnalogicJoypad(Manager.Game.Content, AirPlane.RightAnalog);

                    break;

                case Order.Changed:

                    BubbleButton = new Button(Manager.Game.Content.Load<Texture2D>("AirPlane/BubbleButton"), AirPlane.RightButton);

                    Analog = new AnalogicJoypad(Manager.Game.Content, AirPlane.LeftAnalog);

                    break;
            }

            # endregion


            //CurrentOrder = Order.Normal;

            //BubbleEffect = Manager.Game.Content.Load<SoundEffect>("Audio/SoundEffects/BubbleEffect");

            //ExplosionEffect = Manager.Game.Content.Load<SoundEffect>("Audio/SoundEffects/ExplosionEffect");

            BalloonInterval = 10000f;

            Restart();
        }

        public override void Restart()
        {
            Jogador = new AirPlane(Manager.Game.Content , Manager.Game.GraphicsDevice.Viewport);

            Balloons = new BalloonManager(Manager.Game.Content);

            BalloonElapsedTime = 0f;

            IsCritical = false;
            IsMoment = false;

            CriticalTime = 0f;

            Touches = new TouchCollection();

            TouchPanel.EnabledGestures = GestureType.None;

            if (StateManager.HasAudioControl)
            {
                MediaPlayer.Play(Manager.GamePlaySong);
            }

            base.Restart();
        }

        public override void Update(GameTime gameTime)
        {
            # region Device's BackButton

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                Manager.GoToPause();
            }

            # endregion
            
            # region Won
            
            if (Balloons.Interval <= 3500)
            {
                if (StateManager.HasVibrationControl)
                {
                    Manager.Vibrate.Start(new TimeSpan(0, 0, 0, 0, 500));
                }

                Manager.GoToWon(Jogador.Points);
            }

            # endregion
            
            # region Lost
            
            if (Jogador.SuccessBar.Energya <= 0)
            {
                if (StateManager.HasVibrationControl)
                {
                    Manager.Vibrate.Start(new TimeSpan(0, 0, 0, 0, 500));
                }

                Manager.GoToLost(Jogador.Points);
            }

            # endregion

            # region Aumentando a Dificuldade

            BalloonElapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            if (BalloonElapsedTime >= BalloonInterval)
            {
                Balloons.Interval -= 250;

                BalloonElapsedTime = 0f;
            }

            # endregion

            # region Atualizações
            
            Touches = TouchPanel.GetState();

            List<Vector2> Positions = new List<Vector2>();

            for (int i = 0; i < Touches.Count; i++)
			{
			    if (Touches[i].State == TouchLocationState.Pressed || Touches[i].State == TouchLocationState.Moved)
	            {
                    Positions.Add(Touches[i].Position);
	            }
			}

            Analog.Update(gameTime, Positions);
            
            Jogador.TargetAngle = Analog.Rotation;
            Jogador.Update(gameTime , Analog.Ative);

            Balloons.Update(gameTime);

            # endregion

            #region Colisão Balão - Prédio

            for (int i = 0; i < Balloons.List.Count; i++)
            {
                if (Balloons.List[i].BoundingRectangle.Y >= 470)
                {
                    if (Balloons.List[i].HasBubble)
                    {
                        Jogador.Points += Balloons.List[i].PointsSave;

                        if (Balloons.List[i].Premiado)
                        {
                            Jogador.SuccessBar.RecuperaPerda();
                        }
                    }
                    else
                    {
                        Jogador.SuccessBar.Energya -= Balloons.List[i].PointsEscape;

                        //if (StateManager.HasAudioControl)
                        //{
                        //    ExplosionEffect.Play();
                        //}
                    }

                    Balloons.List.RemoveAt(i);

                    continue;
                }
            }

            #endregion

            #region Colisão Balão - Avião
            
            for (int i = 0; i < Balloons.List.Count; i++)
            {
                if (Jogador.BoundingRectangle_Angulated.Intersects(Balloons.List[i].BoundingRectangle , 0f))
                {
                    if (Game1.VerifyCollision(Jogador.BoundingRectangle , Jogador.Data ,
                        Balloons.List[i].BoundingRectangle , Balloons.List[i].Data))
                    {
                        Jogador.SuccessBar.Energya -= Balloons.List[i].PointsEscape;

                        Balloons.List.RemoveAt(i);

                        //if (StateManager.HasAudioControl)
                        //{
                        //    ExplosionEffect.Play();
                        //}

                        continue;
                    }
                }
            }

            #endregion

            #region Alerta Vermelho

            if (IsCritical)
            {
                CriticalTime += gameTime.ElapsedGameTime.Milliseconds;

                if (CriticalTime >= 1500)
                {
                    CriticalTime = 0f;

                    IsMoment = !IsMoment;
                }

                if (Jogador.SuccessBar.Energya > 40)
                {
                    IsCritical = false;
                    IsMoment = false;

                    CriticalTime = 0f;
                }
            }
            else
            {

                if (Jogador.SuccessBar.Energya <= 40)
                {
                    IsCritical = true;
                    IsMoment = true;

                    CriticalTime = 0f;
                }
            }

            #endregion

            # region Bubbles

            for (int i = 0; i < Jogador.Bubbles.Count; i++)
            {
                Jogador.Bubbles[i].Update(gameTime);

                if (!Manager.Game.GraphicsDevice.Viewport.Bounds.Contains(Jogador.Bubbles[i].BoundingRectangle))
                {
                    if (!Manager.Game.GraphicsDevice.Viewport.Bounds.Intersects(Jogador.Bubbles[i].BoundingRectangle))
                    {
                        Jogador.Bubbles.RemoveAt(i);
                    }
                }
            }

            # endregion

            # region Bubble Button

            Jogador.IsShooting = false;

            for (int i = 0; i < Positions.Count; i++)
            {
                Point TouchedPlace = new Point((int)Positions[i].X, (int)Positions[i].Y);

                if (BubbleButton.Rectangle.Contains(TouchedPlace))
                {
                    Jogador.IsShooting = true;
                }
            }

            if (Jogador.IsShooting)
            {
                Jogador.ThrowBubble(Manager.Game.Content);
            }

            Jogador.WasShooting = Jogador.IsShooting;

            # endregion

            # region Colisão Balão - Bolha

            for (int i = 0; i < Balloons.List.Count; i++)
            {
                for (int j = 0; j < Jogador.Bubbles.Count; j++)
			    {
                    if (Balloons.List[i].BoundingRectangle.Intersects(Jogador.Bubbles[j].BoundingRectangle))
                    {
                        Balloons.List[i].Capture();

                        Jogador.Bubbles.RemoveAt(j);

                        //if (StateManager.HasAudioControl)
                        //{
                        //    BubbleEffect.Play();
                        //}
                    }
                }
            }

            # endregion

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Manager.spriteBatch.Draw(BackGroundImage , Vector2.Zero , Color.White);
            
            Balloons.Draw(Manager.spriteBatch);

            Jogador.Draw(Manager.spriteBatch);

            # region Bubbles

            for (int i = 0; i < Jogador.Bubbles.Count; i++)
            {
                Jogador.Bubbles[i].Draw(Manager.spriteBatch);
            }

            # endregion

            BubbleButton.Draw(Manager.spriteBatch);

            Analog.Draw(Manager.spriteBatch);

            switch (StateManager.CurrentLanguage)
            {
                case GameLanguage.English:

                    Manager.spriteBatch.DrawString(Manager.Font, "Score :",
                new Vector2(500, 10), Color.Red);

                    break;

                case GameLanguage.Portugues:

                    Manager.spriteBatch.DrawString(Manager.Font, "Pontos :",
                new Vector2(500, 10), Color.Red);
                    
                break;
            }

            Manager.spriteBatch.DrawString(Manager.Font, Jogador.Points.ToString(),
                new Vector2(665, 10), Color.Red);

            if (IsMoment)
            {
                Manager.spriteBatch.Draw(AlertBar, new Vector2(80, 9), Color.White);
            }
            
            base.Draw(gameTime);
        }
    }
}