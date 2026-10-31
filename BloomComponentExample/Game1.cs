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

        bool buttonPressed;
        int SettingsIndex { get; set; } = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                GraphicsProfile = GraphicsProfile.HiDef
            };
            bloom = new Bloom(graphics, false);
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

            bloom.LoadContent(spriteBatch, Content, GraphicsDevice);

            //Use either a custom bloom config or one of the prebuilt ones
            //bloom.Settings = new BloomSettings("catpants", 0, 2, 1.1f, 0.1f, 1, 1);
            bloom.Settings = BloomSettings.PresetSettings[SettingsIndex];

            //Load a cool picture to use as background
            background = Content.Load<Texture2D>("Braid_screenshot8");
        }

        protected override void Initialize()
        {
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += (_, _) => //resize back buffer
            {
                graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
                graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
                graphics.ApplyChanges();
                bloom.Resize();
            };

            base.Initialize();
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
                if (!buttonPressed)
                {
                    buttonPressed = true;

                    SettingsIndex++;
                    if (SettingsIndex >= BloomSettings.PresetSettings.Length)
                    {
                        SettingsIndex = 0;
                    }
                    bloom.Settings = BloomSettings.PresetSettings[SettingsIndex];
                }
            }
            else
            {
                buttonPressed = false;
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

            //Start the bloom to capture everything as it is drawn
            bloom.BeginDraw();

            //Draw all your stuff
            spriteBatch.Begin();
            spriteBatch.Draw(background,
                new Rectangle(0, 0, graphics.GraphicsDevice.PresentationParameters.BackBufferWidth, graphics.GraphicsDevice.PresentationParameters.BackBufferHeight),
                Color.White);
            spriteBatch.End();

            //This will draw the scene with bloom
            bloom.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
