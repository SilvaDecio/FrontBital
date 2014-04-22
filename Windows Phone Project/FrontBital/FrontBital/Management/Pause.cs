using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input.Touch;

using FrontBital.BaseClasses;
using FrontBital.DataBase;

namespace FrontBital.Management
{
    class Pause : State
    {
        Button ResumeButton, RestartButton, MenuButton , VibrationButton , ChangeButton;

        Slider SongSlider, SoundEffectSlider;

        public Pause(StateManager Father)
        {
            Manager = Father;

            # region Sliders

            SongSlider = new Slider(Manager.Game.Content.Load<Texture2D>("Slider"), Manager.Game.Content.Load<Texture2D>("SliderBar"),
                MediaPlayer.Volume * 10, new Vector2(), new Vector2(50, 360), 50, string.Empty);

            SongSlider.Position = new Vector2(50 + (SongSlider.Value * 10), 300);
            SongSlider.Rectangle = new Rectangle((int)SongSlider.Position.X,
                (int)SongSlider.Position.Y, SongSlider.Image.Width,
                SongSlider.Image.Height);

            SoundEffectSlider = new Slider(Manager.Game.Content.Load<Texture2D>("Slider"), Manager.Game.Content.Load<Texture2D>("SliderBar"),
                SoundEffect.MasterVolume * 10, new Vector2(), new Vector2(600, 360), 600, string.Empty);

            SoundEffectSlider.Position = new Vector2(600 + (SoundEffectSlider.Value * 10), 300);
            SoundEffectSlider.Rectangle = new Rectangle((int)SoundEffectSlider.Position.X,
                (int)SoundEffectSlider.Position.Y, SoundEffectSlider.Image.Width,
                SoundEffectSlider.Image.Height);

            # endregion

            # region Language

            switch (StateManager.CurrentLanguage)
            {
                case GameLanguage.English:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/Pause");

                    # region Buttons

                    ResumeButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Pause/Resume"), new Vector2(450, 125));
                    RestartButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Pause/Restart"), new Vector2(260, 125));
                    MenuButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Pause/Menu"), new Vector2(610, 125));

                    if (StateManager.HasVibrationControl)
                    {
                        VibrationButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Pause/VibrationOn"), new Vector2(260 , 300));
                    }
                    else
                    {
                        VibrationButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Pause/VibrationOff"), new Vector2(260, 300));
                    }

                    ChangeButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Pause/Change") , new Vector2(440 , 310));

                    # endregion

                    SongSlider.Name = "Musics";

                    SoundEffectSlider.Name = "Effects";

                    break;

                case GameLanguage.Portugues:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Pausa");

                    # region Botões

                    ResumeButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Pausa/Continuar"), new Vector2(450, 125));
                    RestartButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Pausa/Reiniciar"), new Vector2(260, 125));
                    MenuButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Pausa/Menu"), new Vector2(610, 125));

                    if (StateManager.HasVibrationControl)
                    {
                        VibrationButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Pausa/ComVibracao"), new Vector2(260, 300));
                    }
                    else
                    {
                        VibrationButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Pausa/SemVibracao"), new Vector2(260, 300));
                    }

                    ChangeButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Pausa/Mudar"), new Vector2(440, 310));

                    # endregion
                    
                    SongSlider.Name = "Músicas";

                    SoundEffectSlider.Name = "Efeitos";

                    break;
            }

            # endregion

            if (StateManager.HasAudioControl)
            {
                MediaPlayer.Pause();
            }
        }

        public override void Update(GameTime gameTime)
        {
            # region Device's BackButton

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                if (StateManager.HasAudioControl)
                {
                    MediaPlayer.Resume();
                }

                Manager.ResumeGamePlay();
            }

            # endregion

            # region Buttons

            if (Manager.Touched)
            {
                if (ResumeButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    if (StateManager.HasAudioControl)
                    {
                        MediaPlayer.Resume();
                    }

                    Manager.ResumeGamePlay();
                }

                else if (RestartButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    Manager.RestartGamePlay();
                }

                else if (MenuButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    if (StateManager.HasAudioControl)
                    {
                        MediaPlayer.Stop();
                    }

                    Manager.GoToMenu();
                }

                else if (VibrationButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    switch (StateManager.CurrentLanguage)
                    {
                        case GameLanguage.English:

                            if (StateManager.HasVibrationControl)
                            {
                                VibrationButton.Image = Manager.Game.Content.Load<Texture2D>
                                    ("English/Buttons/Pause/VibrationOff");

                                StateManager.HasVibrationControl = false;
                            }
                            else
                            {
                                VibrationButton.Image = Manager.Game.Content.Load<Texture2D>
                                    ("English/Buttons/Pause/VibrationOn");

                                StateManager.HasVibrationControl = true;

                                Manager.Vibrate.Start(new TimeSpan(0, 0, 0, 0, 400));
                            }

                            break;

                        case GameLanguage.Portugues:

                            if (StateManager.HasVibrationControl)
                            {
                                VibrationButton.Image = Manager.Game.Content.Load<Texture2D>
                                    ("Portugues/Botoes/Pausa/SemVibracao");

                                StateManager.HasVibrationControl = false;
                            }
                            else
                            {
                                VibrationButton.Image = Manager.Game.Content.Load<Texture2D>
                                    ("Portugues/Botoes/Pausa/ComVibracao");

                                StateManager.HasVibrationControl = true;

                                Manager.Vibrate.Start(new TimeSpan(0, 0, 0, 0, 400));
                            }

                            break;
                    }
                }
                else if (ChangeButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    switch (GamePlay.CurrentOrder)
                    {
                        case Order.Normal:

                            GamePlay.CurrentOrder = Order.Changed;

                            GamePlay.BubbleButton.Position = AirPlane.RightButton;
                            GamePlay.BubbleButton.Rectangle = new Rectangle((int)GamePlay.BubbleButton.Position.X,
                                (int)GamePlay.BubbleButton.Position.Y, GamePlay.BubbleButton.Image.Width,
                                GamePlay.BubbleButton.Image.Height);

                            GamePlay.Analog.position = AirPlane.LeftAnalog;
                            

                            break;

                        case Order.Changed:

                            GamePlay.CurrentOrder = Order.Normal;

                            GamePlay.BubbleButton.Position = AirPlane.LeftButton;
                            GamePlay.BubbleButton.Rectangle = new Rectangle((int)GamePlay.BubbleButton.Position.X,
                                (int)GamePlay.BubbleButton.Position.Y, GamePlay.BubbleButton.Image.Width,
                                GamePlay.BubbleButton.Image.Height);

                            GamePlay.Analog.position = AirPlane.RightAnalog;
                            
                            break;
                    }

                    if (StateManager.HasAudioControl)
                    {
                        MediaPlayer.Resume();
                    }

                    Manager.ResumeGamePlay();
                }
            }

            # endregion

            # region Sliders

            if (Manager.HorizontalDrag)
            {
                Rectangle A = new Rectangle(SongSlider.Rectangle.X - SongSlider.Rectangle.Width / 2,
                    SongSlider.Rectangle.Y, SongSlider.Rectangle.Width * 2, SongSlider.Rectangle.Height);

                Rectangle B = new Rectangle(SoundEffectSlider.Rectangle.X - SoundEffectSlider.Rectangle.Width / 2,
                    SoundEffectSlider.Rectangle.Y, SoundEffectSlider.Rectangle.Width * 2, SoundEffectSlider.Rectangle.Height);

                if (A.Contains(Manager.TouchedPlace))
                {
                    SongSlider.Position.X += Manager.CurrentGesture.Delta.X;

                    # region SongSlider Value

                    if (SongSlider.Position.X <= SongSlider.MinimumPosition)
                    {
                        SongSlider.Value = 0.0f;
                    }
                    else if (SongSlider.Position.X >= SongSlider.MaximumPosition)
                    {
                        SongSlider.Value = 10.0f;
                    }
                    else
                    {
                        SongSlider.Value = ((SongSlider.Position.X - SongSlider.MinimumPosition) / 10);
                    }

                    # endregion

                    MediaPlayer.Volume = (SongSlider.Value / 10);

                    StateManager.SongVolume = SongSlider.Value;
                }
                else if (B.Contains(Manager.TouchedPlace))
                {
                    SoundEffectSlider.Position.X += Manager.CurrentGesture.Delta.X;

                    # region SoundEffectSlider Value

                    if (SoundEffectSlider.Position.X <= SoundEffectSlider.MinimumPosition)
                    {
                        SoundEffectSlider.Value = 0.0f;
                    }
                    else if (SoundEffectSlider.Position.X >= SoundEffectSlider.MaximumPosition)
                    {
                        SoundEffectSlider.Value = 10.0f;
                    }
                    else
                    {
                        SoundEffectSlider.Value = ((SoundEffectSlider.Position.X - SoundEffectSlider.MinimumPosition) / 10);
                    }

                    # endregion

                    SoundEffect.MasterVolume = (SoundEffectSlider.Value / 10);

                    StateManager.EffectVolume = SoundEffectSlider.Value;
                }

                SongSlider.Update(gameTime);
                SoundEffectSlider.Update(gameTime);
            }

            # endregion

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Manager.spriteBatch.Draw(BackGroundImage, Vector2.Zero ,Color.White);

            ResumeButton.Draw(Manager.spriteBatch);
            RestartButton.Draw(Manager.spriteBatch);
            MenuButton.Draw(Manager.spriteBatch);
            VibrationButton.Draw(Manager.spriteBatch);
            ChangeButton.Draw(Manager.spriteBatch);

            SongSlider.Draw(Manager.spriteBatch, Manager.Font);
            SoundEffectSlider.Draw(Manager.spriteBatch, Manager.Font);

            base.Draw(gameTime);
        }
    }
}