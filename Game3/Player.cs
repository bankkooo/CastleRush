using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    class Player
    {
        private int money = 50;
        private int lives = 30;

        private List<Towers> towers = new List<Towers>();// list of all current tower

        private MouseState mouseState; // Mouse state for the current frame
        private MouseState oldState; // Mouse state for the previous frame

        private Level level;

        private Texture2D towerTexture;

        private Texture2D bulletTexture;

        public Player(Level level)
        {
            this.level = level;
        }

        public Player(Level level, Texture2D towerTexture, Texture2D bulletTexture)
        {
            this.level = level;

            this.towerTexture = towerTexture;
            this.bulletTexture = bulletTexture;
        }

        public int Money
        {
            get { return money; }
        }
        public int Lives
        {
            get { return lives; }
        }


        private int cellX;
        private int cellY;

        private int tileX;
        private int tileY;

        private bool IsCellClear()
        {
            bool inBounds = cellX >= 0 && cellY >= 0 && // Make sure tower is within limits
                cellX < level.Width && cellY < level.Height;

            bool spaceClear = true;

            foreach (Towers tower in towers) // Check that there is no tower here
            {
                spaceClear = (tower.Position != new Vector2(tileX, tileY));

                if (!spaceClear)
                    break;
            }

            bool onPath = (level.GetIndex(cellX, cellY) != 1);

            return inBounds && spaceClear && onPath; // If both checks are true return true
        }

        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
            mouseState = Mouse.GetState();

            cellX = (int)(mouseState.X / 50); // Convert the position of the mouse
            cellY = (int)(mouseState.Y / 50); // from array space to level space

            tileX = cellX * 50; // Convert from array space to level space
            tileY = cellY * 50; // Convert from array space to level space

            if (mouseState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed)// if click
            {
                if (IsCellClear())//make tower
                {
                    ArrowTower tower = new ArrowTower(towerTexture,bulletTexture, new Vector2(tileX, tileY));
                    towers.Add(tower);
                }
            }
            foreach (Towers tower in towers)// mark target
            {
                if (tower.Target == null)
                {
                    tower.GetClosestEnemy(enemies);
                }

                tower.Update(gameTime);
            }

            oldState = mouseState; // Set the oldState so it becomes the state of the previous frame.
        }

        
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Towers tower in towers)
            {
                tower.Draw(spriteBatch);
            }
        }
    }
}
