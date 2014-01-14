using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TetrisClone
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        // objects
        private PlayField _playField;
        private Block _currentBlock;

        // input
        KeyboardState _oldKbState;
        
        // textures
        private Texture2D _filledCell;
        private Texture2D _emptyCell;
        
        public Game()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = 800, 
                PreferredBackBufferWidth = 400
            };

            Content.RootDirectory = "Content";
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
            _playField = new PlayField();
            _currentBlock = CreateBlock();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _filledCell = Content.Load<Texture2D>("Sprites/TetrisSquare");
            _emptyCell = Content.Load<Texture2D>("Sprites/TetrisEmptySquare");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private double _elapsedkeyPressTime = 0;

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            //_elapsedkeyPressTime += gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState kbState = Keyboard.GetState();

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kbState.IsKeyDown(Keys.Escape))
                this.Exit();

            //if (gameTime.ElapsedGameTime.TotalSeconds - _elapsedkeyPressTime >= 1)
            //{
            if (kbState.IsKeyDown(Keys.Space) && !_oldKbState.IsKeyDown(Keys.Space) &&
                gameTime.ElapsedGameTime.TotalSeconds > 1)
            {
                _currentBlock.Rotate();
            }

            if (kbState.IsKeyDown(Keys.Right) && !_oldKbState.IsKeyDown(Keys.Right) )
            {
                _currentBlock.Translate(TranslationDirection.Right, _playField.Rows, _playField.Columns);
            }
            if (kbState.IsKeyDown(Keys.Left) && !_oldKbState.IsKeyDown(Keys.Left))
            {
                    _currentBlock.Translate(TranslationDirection.Left, _playField.Rows, _playField.Columns);
            }
            if (kbState.IsKeyDown(Keys.Down) && !_oldKbState.IsKeyDown(Keys.Down))
            {
                    _currentBlock.Translate(TranslationDirection.Down, _playField.Rows, _playField.Columns);
            }

            //    _elapsedkeyPressTime = 0;
            //}

            _oldKbState = kbState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);


            RenderObjectsOnPlayfield(_playField, _currentBlock);
            
            _spriteBatch.Begin();

            DrawPlayfield(_playField);

            _spriteBatch.End();
            
            base.Draw(gameTime);
        }

        private Block CreateBlock()
        {
            return new OBlock(0, 0);
        }

        private void RenderObjectsOnPlayfield(PlayField playField, Block currentBlock)
        {
            // draw the block to playfield
            playField.RenderBlock(currentBlock);
        }

        /// <summary>
        /// Draw playfield
        /// </summary> 
        private void DrawPlayfield(PlayField playField)
        {
            int cellSize = playField.GetCellSize();

            // fill cells
            for (int row = 0; row < playField.Rows; row++)
            {
                for (int col = 0; col < playField.Columns; col++)
                {
                    bool isFilled = playField.IsFilled(row, col);

                    if (isFilled)
                    {
                        _spriteBatch.Draw(_filledCell, new Rectangle(col * cellSize, row * cellSize, cellSize, cellSize), Color.White);
                    }
                    else
                    {
                        _spriteBatch.Draw(_emptyCell, new Rectangle(col * cellSize, row * cellSize, cellSize, cellSize), Color.White);
                    }
                }
            }
        }
    }
}
