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
        private BlockFactory _blockFactory;

        // input
        KeyboardState _oldKbState;
        private readonly Timer _timer;
        
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
            IsFixedTimeStep = true;
            _timer = new Timer();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _playField = new PlayField(25, 10);
            _blockFactory = new BlockFactory();
            _currentBlock = _blockFactory.GenerateBlock();
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

            // textures for filled / empty cells
            _filledCell = Content.Load<Texture2D>("Sprites/TetrisSquare");
            _emptyCell = Content.Load<Texture2D>("Sprites/TetrisEmptySquare");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            ProcessInput(gameTime);

            if (_timer.Ticks < Timer.TICK_TIME)
            {
                _timer.UpdateTicks(gameTime.ElapsedGameTime);
                _timer.IncrementKeypress();
            }
            else
            {
                _currentBlock.Move(TranslationDirection.Down, _playField);
                _timer.ResetTicks();
            }

            if (_currentBlock.HasLanded)
            {
                _currentBlock = null;

               var filledRows = _playField.LandedBlocks.GetFullRows();

                if (filledRows.Count > 0)
                {
                    foreach (int row in filledRows)
                    {
                        _playField.LandedBlocks.ClearRow(row);
                    }
                }

                _currentBlock = _blockFactory.GenerateBlock();
            }

      

            base.Update(gameTime);
        }


        private void ProcessInput(GameTime gameTime)
        {
            //_elapsedkeyPressTime += gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState currentKeyState = Keyboard.GetState();

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || currentKeyState.IsKeyDown(Keys.Escape))
                this.Exit();
            
            if ((currentKeyState.IsKeyDown(Keys.Space) && _oldKbState.IsKeyUp(Keys.Space) &&
                _timer.TimeSinceLastKeypress > Timer.ROTATE_TAP_KEYPRESS_TIME) // key not held (tap)
               ||
               (currentKeyState.IsKeyDown(Keys.Space) && _oldKbState.IsKeyDown(Keys.Space) &&
                _timer.TimeSinceLastKeypress > Timer.ROTATE_HOLD_KEYPRESS_TIME)) // key is being held
            {
                _currentBlock.Rotate();
                _timer.ResetKeypressTimer();
            }
            

            if (_timer.TimeSinceLastKeypress > Timer.FIRST_KEYPRESS_TIME)
            {
                if (currentKeyState.IsKeyDown(Keys.Right))
                {
                    _currentBlock.Move(TranslationDirection.Right, _playField);

                }
                if (currentKeyState.IsKeyDown(Keys.Left))
                {
                    _currentBlock.Move(TranslationDirection.Left, _playField);

                }
                if (currentKeyState.IsKeyDown(Keys.Down))
                {
                    _currentBlock.Move(TranslationDirection.Down, _playField);

                }

                _timer.ResetKeypressTimer();
            }

            // for testing TODO remove from final game
            if (currentKeyState.IsKeyDown(Keys.Up) && !_oldKbState.IsKeyDown(Keys.Up))
            {
                _currentBlock.Move(TranslationDirection.Up, _playField);
            }

            _oldKbState = currentKeyState;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // render playfield
            RenderPlayfield(_playField, ref _currentBlock);
            
            _spriteBatch.Begin();

            DrawPlayfield(_playField);

            _spriteBatch.End();
            
            base.Draw(gameTime);
        }

        private void RenderPlayfield(PlayField playField, ref Block currentBlock)
        {
            // draw the block to playfield
            playField.AddBlock(ref currentBlock);

            // draw blocks on field
            playField.Update();
        }

        /// <summary>
        /// Draw playfield
        /// </summary> 
        private void DrawPlayfield(PlayField playField)
        {
            int cellSize = playField.CellPixelSize;

            // fill cells
            for (int row = 0; row < playField.RowCount; row++)
            {
                for (int col = 0; col < playField.ColumnCount; col++)
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
