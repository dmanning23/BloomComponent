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
            bloom = new Bloom(graphics);
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
            var size = new Point(graphics.GraphicsDevice.PresentationParameters.BackBufferWidth, graphics.GraphicsDevice.PresentationParameters.BackBufferHeight);

            //draw stuff before (no bloom)
            spriteBatch.Begin();
            DrawBackground(new Rectangle(Point.Zero, size / 8));
            spriteBatch.End();

            bloom.BeginDraw();
            spriteBatch.Begin();

            //...draw all your stuff (with bloom)
            DrawBackground(new Rectangle(size / 16, size * 7 / 8));

            spriteBatch.End();
            bloom.Draw(gameTime);

            //draw stuff after (no bloom)
            spriteBatch.Begin();
            DrawBackground(new Rectangle(size * 7 / 8, size / 8));
            spriteBatch.End();

            base.Draw(gameTime);

            void DrawBackground(Rectangle rectangle)
            {
                spriteBatch.Draw(background,
                    rectangle,
                    Color.White);
            }
        }
    }
}
