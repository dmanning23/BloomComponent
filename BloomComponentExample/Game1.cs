using System.Linq;
using BloomBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BloomComponentExample
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D background;
        Bloom bloom;

        int SettingsIndex { get; set; } = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                GraphicsProfile = GraphicsProfile.HiDef
            };
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bloom = new Bloom();
            bloom.LoadContent(spriteBatch, Content, graphics);

            //Use either a custom bloom config or one of the prebuilt ones
            //bloom.Settings = new BloomSettings("catpants", 0, 2, 1.1f, 0.1f, 1, 1);
            bloom.Settings = BloomSettings.PresetSettings[SettingsIndex];

            //Load a cool picture to use as background
            background = Content.Load<Texture2D>("Braid_screenshot8");
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
#if !__IOS__
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
#endif

            //Increment the bloom config
            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                SettingsIndex++;
                if (SettingsIndex >= BloomSettings.PresetSettings.Length)
                {
                    SettingsIndex = 0;
                }
                bloom.Settings = BloomSettings.PresetSettings[SettingsIndex];
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            bloom.BeginDraw();

            spriteBatch.Begin();

            //...draw all your stuff
            spriteBatch.Draw(background,
                new Rectangle(0, 0, graphics.GraphicsDevice.PresentationParameters.BackBufferWidth, graphics.GraphicsDevice.PresentationParameters.BackBufferHeight),
                Color.White);

            spriteBatch.End();

            bloom.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
