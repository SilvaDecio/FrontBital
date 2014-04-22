using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FrontBital
{
    class EnergyBar
    {
        //textura da caixa barra
        Texture2D BoxImage;
        //textura da energia
        Texture2D EnergyBarImage;
        //Posição da caixa
        Vector2 BoxPosition;
        //posição da barra
        Vector2 BarPosition;
        //Indica se deseja reduzir a energia para o outro lado (padrão = esquerda, flip = direita)
        bool FlipEnergia = false;
        //Max de enrgia
        float MaximumEnergy;
        public float MaxEnergya 
        {
            get { return MaximumEnergy; }
            set { MaximumEnergy = value; }
        }
        //energia
        private float energya;
        //Energia
        public float Energya
        {
            get { return this.energya; }
            set { this.energya = MathHelper.Clamp(value, 0, MaximumEnergy); }
        }
        // cor da barra de energia vasia
        Color EmptyEnergyColor;
        // cor da barra de energia cheia
        Color FullEnergyColor;
        /// <summary>
        /// Construtor da barra de enrgia
        /// </summary>
        /// <param name="CaixaBarra">Textura que ficará em volta da barra</param>
        /// <param name="BaraEnergia">a barra de enregia</param>
        /// <param name="poscaoCaixa">possição da textura que fica em volta</param>
        /// <param name="posBarraEnergia">posição da barra de energia</param>
        /// <param name="corBarraCheia">Cor da barra de enrgia quando estiver vazia</param>
        /// <param name="corBarraVazia">cor da barra de erngia quando estiver cheia</param>
        public EnergyBar(ContentManager Content)
        {
            BoxImage = Content.Load<Texture2D>("AirPlane/EnergyBar/BoxImage");
            EnergyBarImage = Content.Load<Texture2D>("AirPlane/EnergyBar/Image");


            FullEnergyColor = new Color(26,26,67);
            EmptyEnergyColor = new Color(255,255,255);

            BoxPosition = Vector2.Zero;
            BarPosition = new Vector2(80 , 9);

            MaxEnergya = 100;
            Energya = MaximumEnergy;
        }
        /// <summary>
        /// Metodo pra precuperar a energia perdida pelo o avião
        /// </summary>
        public void RecuperaPerda()
        {
            //calcular a quantidade de erros
            float resultado = MaximumEnergy - energya;
            //dividindo a quantidade de erros para recuperação de erros
            float volEnergia = resultado / 3;
            //Voltando a enrgia
            energya += volEnergia;
        }
        /// <summary>
        /// Metodo para desenhar a barra
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //Desenhado a caixa da barra
            spriteBatch.Draw(BoxImage, BoxPosition, Color.White);
            //Desenhando a barra de energia
            spriteBatch.Draw(EnergyBarImage, BarPosition, new Rectangle(0, 0,
                (int)(energya * EnergyBarImage.Width / MaximumEnergy), EnergyBarImage.Height),
                Color.Lerp(FullEnergyColor, EmptyEnergyColor, energya / MaximumEnergy),
                FlipEnergia ? MathHelper.ToRadians(180) : 0.0f,
                FlipEnergia ? new Vector2(EnergyBarImage.Width, EnergyBarImage.Height) : Vector2.Zero,
                1.0f,
                SpriteEffects.None, 0.0f);
        }
    }
}