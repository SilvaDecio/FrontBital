using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using System.IO;

namespace FrontBital.Animation
{
    public class SpriteSheetPacker
    {
        public Dictionary<string, Rectangle> SpriteSourceRectangles;       
        
        string Line;

        string[] Sides;
        
        string[] RectParts;

        Rectangle R;

        public SpriteSheetPacker(string Path)
       {
           SpriteSourceRectangles = new Dictionary<string, Rectangle>();

           using (StreamReader Reader = new StreamReader(TitleContainer.OpenStream(@"Content\" + Path)))
	       {
               // Lê até o fim do arquivo
               while (!Reader.EndOfStream)
               {
                   // linha atual
                   Line = Reader.ReadLine();

                   // Divide a linha em nome e coordenada
                   Sides = Line.Split('=');

                   // Divide as coordenadas
                   RectParts = Sides[1].Trim().Split(' ');

                   // Cria um retangulo com as coordenadas
                   Rectangle R = new Rectangle(
                      int.Parse(RectParts[0]),
                      int.Parse(RectParts[1]),
                      int.Parse(RectParts[2]),
                      int.Parse(RectParts[3]));

                   // Adiciona o nome do sprite e o sourcerect ao dicionario
                   SpriteSourceRectangles.Add(Sides[0].Trim(), R);
                }
            }
        }
    }
}