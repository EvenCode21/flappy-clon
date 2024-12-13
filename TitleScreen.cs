using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Flappy
{
    class TitleScreen : Screen
    {
        Texture2D texture;
        KeyboardState keyboardState;
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,new Rectangle(0,0,Flappy.SCREEN_WIDTH,Flappy.SCREEN_HEIGHT),Color.White);
        }

        public override void Initialize()
        {
           
        }

        public override void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("message");
        }

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                Flappy.gameState = Flappy.GameStates.Countdown;
            }

            
        }
    }
}
