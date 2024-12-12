using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Flappy
{
    class Bird
    {
        Texture2D texture;
        private int width;
        private int height;

        public int Width { get { return width; } }
        public int Height { get { return height; } }


        private Vector2 position;
        public Vector2 Position { get { return position; } }

        private const float GRAVITY = 15;
        private const float JUMP_FORCE = -6;

        private float deltaY;

        KeyboardState currentState;
        KeyboardState previousState;

        public void Initialize()
        {
            currentState = Keyboard.GetState();
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("yellowbird-midflap");
            width = texture.Width;
            height = texture.Height;

            position = new Vector2(Flappy.SCREEN_WIDTH / 3 - (width / 2), Flappy.SCREEN_HEIGHT / 2);
        }

        public void Update(GameTime gameTime)
        {
            currentState = Keyboard.GetState();

            if (currentState.IsKeyDown(Keys.Space))
            {
                if (!previousState.IsKeyDown(Keys.Space))
                {
                    //key just pressed
                    deltaY = JUMP_FORCE;
                }
                else
                {
                    //key hold 
                }
            }

            deltaY = deltaY >= 20 ? 20 : deltaY + (GRAVITY * (float)gameTime.ElapsedGameTime.TotalSeconds); 
            
            position.Y += deltaY;

            previousState = currentState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position,Color.White);
        }

        public bool Collides(Pipe pipe)
        {
            if((position.X + 2) < pipe.Position.X + pipe.Texture.Width && position.X + (texture.Width - 4) > pipe.Position.X
                && (position.Y + 2) < pipe.Position.Y + pipe.Texture.Height && position.Y +  (texture.Height - 4) > pipe.Position.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
