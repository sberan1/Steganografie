using System;
using System.Drawing;


namespace Steganografie
{
    class Program
    {
        static void Main(string[] args)
        {
            string zasifrovatText = args[1]; 
            
            if (args[0] == "--hide" && zasifrovatText.Length != 0 && args[2].Contains(".png"))
            {
                Hide(zasifrovatText, args[2]).Save("zasifrovano.png");
            }
            else if (args[0] == "--show" && args[1].Contains(".png"))
            {
                Console.WriteLine(Show(args[1].ToString()));
            }
            Console.ReadKey();
        }
        public static Bitmap Hide(string message, string filepath)
        {
            Bitmap obrazek = new Bitmap(filepath);
            
            for (int i = 0; i < obrazek.Width; i++) 
            {
                for (int j = 0; j < obrazek.Height; j++) 
                {
                    Color vybranyPixel = obrazek.GetPixel(i,j);
                    if (i < 1 && j < message.Length)
                    {
                        char znak = Convert.ToChar(message.Substring(j, 1));
                        int hodnotaZnaku = Convert.ToInt32(znak);
                        obrazek.SetPixel(i, j, Color.FromArgb(hodnotaZnaku, vybranyPixel.G, vybranyPixel.B));
                    }
                    else if(i == obrazek.Width - 1 && j == obrazek.Height - 1)
                    {
                        obrazek.SetPixel(i, j, Color.FromArgb(message.Length, vybranyPixel.G, vybranyPixel.B ));
                    }
                }
            }
            return obrazek;
        }
        public static string Show(string filepath)
        {
            string output = "";
            Bitmap obrazek = new Bitmap(filepath);
            int delka = Convert.ToInt32(obrazek.GetPixel(obrazek.Width - 1, obrazek.Height - 1).R);
            for (int i = 0; i < obrazek.Width; i++)
            {
                for (int j = 0; j < obrazek.Height; j++)
                {
                    Color momentalniPixel = obrazek.GetPixel(i, j);
                    if (i < 1 && j < delka)
                    {
                        output += (char)momentalniPixel.R;
                    }
                }
            }
            return output;
        }

    }
}
