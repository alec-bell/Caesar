﻿using HungryFrog.Commands;
using HungryFrog.Controllers;
using HungryFrog.Interfaces;
using HungryFrog.Contexts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace HungryFrog
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        IController keyboardController;

        PlayerContext player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            player = new PlayerContext(Content, new Vector2(0, 0));

            // Initialize commands
            ICommand quitCommand = new QuitCommand(this);
            ICommand moveLeftCommand = new MoveLeftCommand(player);
            ICommand moveRightCommand = new MoveRightCommand(player);
            ICommand moveUpCommand = new MoveUpCommand(player);
            ICommand moveDownCommand = new MoveDownCommand(player);

            // Link keys to commands
            Dictionary<Keys, ICommand> keysMap = new Dictionary<Keys, ICommand>();
            keysMap.Add(Keys.Q, quitCommand);
            keysMap.Add(Keys.Left, moveLeftCommand);
            keysMap.Add(Keys.Right, moveRightCommand);
            keysMap.Add(Keys.Up, moveUpCommand);
            keysMap.Add(Keys.Down, moveDownCommand);
            //secondary commands for WASD
            keysMap.Add(Keys.W, moveUpCommand);
            keysMap.Add(Keys.A, moveLeftCommand);
            keysMap.Add(Keys.S, moveDownCommand);
            keysMap.Add(Keys.D, moveRightCommand);

            // Add controls to controller
            keyboardController = new KeyboardController(keysMap);

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
            keyboardController.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            player.Draw(spriteBatch);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
