using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.GamerServices;

using System.IO.IsolatedStorage;
using System.Xml.Serialization;

using Microsoft.Phone.Tasks;
using Microsoft.Phone.Net.NetworkInformation;

using FrontBital.BaseClasses;
using FrontBital.Management;
using FrontBital.DataBase;

namespace FrontBital.Management
{
    class Menu : State
    {
        Button PlayButton, DirectionsButton, CreditsButton, RankingButton,
            SettingsButton;

        public Menu(StateManager Father)
        {
            Manager = Father;

            # region Language

            switch (StateManager.CurrentLanguage)
            {
                case GameLanguage.English:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/Menu");

                    # region Buttons

                    PlayButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Menu/Play"), new Vector2(10, 50));
                    DirectionsButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Menu/Directions"), new Vector2(10, 150));
                    CreditsButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Menu/Credits"), new Vector2(10, 250));
                    RankingButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Menu/Ranking"), new Vector2(10, 350));
                    SettingsButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Menu/Settings"), new Vector2(700, 400));

                    # endregion

                    break;

                case GameLanguage.Portugues:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Menu");

                    # region Botões

                    PlayButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Menu/Jogar"), new Vector2(10, 50));
                    DirectionsButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Menu/Instrucoes"), new Vector2(10, 150));
                    CreditsButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Menu/Creditos"), new Vector2(10, 250));
                    RankingButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Menu/Recordes"), new Vector2(10, 350));
                    SettingsButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Menu/Configuracoes"), new Vector2(700, 400));

                    # endregion

                    break;
            }

            # endregion

            if (StateManager.HasAudioControl)
            {
                if (MediaPlayer.State == MediaState.Stopped)
                {
                    MediaPlayer.Play(Manager.MenuSong);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            # region Device's BackButton

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                Manager.Game.Exit();
            }

            # endregion

            #region Buttons

            if (Manager.Touched)
            {
                if (PlayButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    if (StateManager.HasAudioControl)
                    {
                        MediaPlayer.Stop();    
                    }

                    Manager.GoToGamePlay();
                }

                else if (DirectionsButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    Manager.GoToDirections();
                }

                else if (CreditsButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    Manager.GoToCredits();
                }

                else if (RankingButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    Manager.GoToRanking();
                }

                else if (SettingsButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    Manager.GoToSettings();
                }
            }

            # endregion

            base.Update(gameTime);
        }

        private void OnEndShowMessageBox(IAsyncResult result) {}

        public override void Draw(GameTime gameTime)
        {
            Manager.spriteBatch.Draw(BackGroundImage, Vector2.Zero, Color.White);

            PlayButton.Draw(Manager.spriteBatch);
            CreditsButton.Draw(Manager.spriteBatch);
            DirectionsButton.Draw(Manager.spriteBatch);
            RankingButton.Draw(Manager.spriteBatch);
            SettingsButton.Draw(Manager.spriteBatch);

            base.Draw(gameTime);
        }
    }
}