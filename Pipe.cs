using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Flappy
{
    class Pipe
    {
        Texture2D texture;
        private const float PIPE_SCROLL = -60;

        Vector2 position;
        Random random = new Random();

        public Vector2 Position { get { return position; } }
        public Texture2D Texture { get { return texture; } }
        string orientation;
        public Pipe(string orientation, float y)
        {
            this.orientation = orientation;
            texture = Flappy.pipeTexture;
            position = new Vector2(Flappy.SCREEN_WIDTH, y);
        }
    
        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X += PIPE_SCROLL * deltaTime;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(orientation == "top")
            {
                spriteBatch.Draw(texture,position,null,Color.White,0,Vector2.Zero,1,SpriteEffects.FlipVertically,0);
            }
            else if(orientation == "bottom") 
            {
                spriteBatch.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
        }
    }
}
