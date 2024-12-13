using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace Flappy
{
    class SaveSystem
    {
        public static int highScore;
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\FlappyClon\";
        public static int firstDigit;
        public static int secondDigit;
        public static int lastDigit;


        public static void Save()
        {
            var highScore = new PlayerScore
            {
                Score = Bird.score
            };

            var xmlSerializer = new XmlSerializer(typeof(PlayerScore));
            using(var writer = new StringWriter())
            {
                xmlSerializer.Serialize(writer, highScore);
                var xmlContent = writer.ToString();
                Directory.CreateDirectory(path);
                StreamWriter textWrt = new StreamWriter(path + "highscore");
                textWrt.WriteLine(xmlContent);
                textWrt.Close();
            }
        }

        public static void Load()
        {
            var xmlSerializer = new XmlSerializer(typeof(PlayerScore));
            using(var reader = new StreamReader(path + "highscore"))
            {
                PlayerScore save = (PlayerScore)xmlSerializer.Deserialize(reader);
                highScore = save.Score;
            }
        }

        public static void DrawHighScore(SpriteBatch spriteBatch)
        {
            if (highScore < 100)
            {
                firstDigit = highScore / 10;
                secondDigit = highScore - (firstDigit * 10);
            }
            else if (highScore < 1000 && highScore >= 100)
            {
                firstDigit = highScore / 100;
                secondDigit = ((highScore / 10) - ((highScore / 10) / 10) * 10);
                lastDigit = (highScore - ((highScore / 10) * 10));
            }
            
            if(highScore < 10)
            {
                spriteBatch.Draw(CountDownScreen.numbers[highScore], new Vector2(Flappy.SCREEN_WIDTH - 20,0), Color.White);
            }else if(highScore < 100 && highScore >= 10)
            {
                spriteBatch.Draw(CountDownScreen.numbers[firstDigit], new Vector2(Flappy.SCREEN_WIDTH - 40, 0), Color.White);
                spriteBatch.Draw(CountDownScreen.numbers[secondDigit], new Vector2(Flappy.SCREEN_WIDTH - 20, 0), Color.White);
            }else if(highScore < 1000 && highScore >= 100)
            {
                spriteBatch.Draw(CountDownScreen.numbers[firstDigit], new Vector2(Flappy.SCREEN_WIDTH - 60, 0), Color.White);
                spriteBatch.Draw(CountDownScreen.numbers[secondDigit], new Vector2(Flappy.SCREEN_WIDTH - 40, 0), Color.White);
                spriteBatch.Draw(CountDownScreen.numbers[lastDigit], new Vector2(Flappy.SCREEN_WIDTH - 20, 0), Color.White);

            }
        }
    }
}
