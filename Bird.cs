using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Flappy
{
    class Bird
    {
        Texture2D texture;
        private static int width;
        private static int height;

        public int Width { get { return width; } }
        public int Height { get { return height; } }


        private static Vector2 position;
        public Vector2 Position { get { return position; } }

        private const float GRAVITY = 15;
        private const float JUMP_FORCE = -6;

        private static float deltaY;

        KeyboardState currentState;
        KeyboardState previousState;

        public bool died = false;

        public SoundEffect hit;
        public SoundEffect jump;
        public SoundEffect coin;

        public static int score = 0;
        public static int firstDigit;
        public static int secondDigit;
        public static int lastDigit;

        public void Initialize()
        {
            currentState = Keyboard.GetState();
            if (score < 100)
            {
                firstDigit = score / 10;
                secondDigit = score - (firstDigit * 10);
            }
            else if (score < 1000 && score >= 100)
            {
                firstDigit = score / 100;
                secondDigit = ((score / 10) - ((score / 10) / 10) * 10);
                lastDigit = (score - ((score / 10) * 10));
            }
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("yellowbird-midflap");
            width = texture.Width;
            height = texture.Height;
            hit = Content.Load<SoundEffect>("Sound/hit");
            jump = Content.Load<SoundEffect>("Sound/wing");
            coin = Content.Load<SoundEffect>("Sound/point");
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
                    if (!died)
                    {
                        deltaY = JUMP_FORCE;
                        jump.Play();
                    }
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

        public bool Collides()
        {
            if (position.Y + texture.Height > Flappy.SCREEN_HEIGHT - Flappy.ground.Height)
            {
                return true;
            }
            else if (position.Y <= 0)
            {
                return true;
            }else
            {
                return false;
            }
        }

        public static void Reset()
        {
            position = new Vector2(Flappy.SCREEN_WIDTH / 3 - (width / 2), Flappy.SCREEN_HEIGHT / 2);
            deltaY = 0;
            SaveSystem.Load();
        }

        public void Scoring(PipePairs pipes)
        {
            if (!pipes.scored)
            {
                if(position.X > pipes.posX + pipes.width)
                {
                    coin.Play();
                    score++;
                    if(score < 100)
                    {
                        firstDigit = score / 10;
                        secondDigit = score - (firstDigit * 10);
                    }else if(score < 1000 && score >= 100)
                    {
                        firstDigit = score / 100;
                        secondDigit = ((score / 10) - ((score / 10) / 10) * 10);
                        lastDigit = (score - ((score / 10) * 10));
                    }
                    pipes.scored = true;
                }
            }
        }

    }
}
