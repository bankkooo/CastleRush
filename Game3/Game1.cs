using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game3
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Level level = new Level();
        //Enemy enemy1;
        //Wave wave;
        WaveManeger waveManeger;
        Background background = new Background();
        //Tower tower;
        Player player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = level.Width * 90;// set window
            graphics.PreferredBackBufferHeight = level.Height * 70 -140;
            graphics.ApplyChanges();
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Texture2D grass = Content.Load<Texture2D>("Block/grass");
            Texture2D path = Content.Load<Texture2D>("Block/path");
            Texture2D pathLT = Content.Load<Texture2D>("Block/pathLT");
            Texture2D pathT = Content.Load<Texture2D>("Block/pathT");
            Texture2D pathRT = Content.Load<Texture2D>("Block/pathRT");
            Texture2D pathL = Content.Load<Texture2D>("Block/pathL");
            Texture2D pathR = Content.Load<Texture2D>("Block/pathR");
            Texture2D pathLD = Content.Load<Texture2D>("Block/pathLD");
            Texture2D pathD = Content.Load<Texture2D>("Block/pathD");
            Texture2D pathRD = Content.Load<Texture2D>("Block/pathRD");
            level.AddTexture(grass);// Add Block GPU
            level.AddTexture(path);
            level.AddTexture(pathLT);
            level.AddTexture(pathT);
            level.AddTexture(pathRT);
            level.AddTexture(pathL);
            level.AddTexture(pathR);
            level.AddTexture(pathLD);
            level.AddTexture(pathD);
            level.AddTexture(pathRD);
            Texture2D back = Content.Load<Texture2D>("Block/background");
            background.AddTexture(back);
            Texture2D enemyTexture = Content.Load<Texture2D>("Enemy/enemy1");
            //enemy1 = new Enemy(enemyTexture, Vector2.Zero, 100, 10, 1f);// change speed;
            //enemy1.SetWaypoints(level.Waypoints);
            waveManeger = new WaveManeger(level, 24, enemyTexture); // set wave
            Texture2D towerTexture = Content.Load<Texture2D>("Tower/tower");
            Texture2D bulletTexture = Content.Load<Texture2D>("Tower/bullet");
            player = new Player(level, towerTexture, bulletTexture);


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            waveManeger.Update(gameTime);
            player.Update(gameTime, waveManeger.Enemies);
            base.Update(gameTime);

            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            level.Draw(spriteBatch);
            waveManeger.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
