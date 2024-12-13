using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Flappy
{
    class GameOverScreen : Screen
    {
        Texture2D texture;

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(Flappy.SCREEN_WIDTH / 6, Flappy.SCREEN_HEIGHT / 2), Color.White);
        }

        public override void Initialize()
        {

        }

        public override void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("gameover");
        }

        public override void Update(GameTime gameTime)
        {
            var keystate = Keyboard.GetState();

            if (keystate.IsKeyDown(Keys.Space))
            {
                Bird.score = 0;
                Flappy.gameState = Flappy.GameStates.Countdown;
            }
        }
    }
}
