using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Flappy
{
    class CountDownScreen : Screen
    {
        public static Dictionary<int,Texture2D> numbers = new Dictionary<int,Texture2D>();

        float counter = 4000;
        public override void Initialize()
        {

        }

        public override void LoadContent(ContentManager Content)
        {
            for(int i = 0; i < 10; i++)
            {
                numbers.Add(i, Content.Load<Texture2D>($"Numbers/{i}"));
            }
        }

        public override void Update(GameTime gameTime)
        {
            counter -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if(counter <= 0)
            {
                Flappy.gameState = Flappy.GameStates.Play;
                counter = 4000;
                PlayScreen.pipePairs.Clear();
                Bird.Reset();
            }

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(numbers[(int)counter / 1000], new Vector2(Flappy.SCREEN_WIDTH / 2 - 10, Flappy.SCREEN_HEIGHT / 2), Color.White);
        }

    }
}
