﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LingIO.Framework;

namespace LingIO.Scenes
{
    class MenuMain : Scene
    {
        SpriteFont fontSmall, fontLarge;

        public MenuMain( SceneManager manager ) : base( manager ) {
            menuWrapping = false;
            selectedOption = 0;
        }

        public override void Initialize() {

            fontSmall = Manager.Game.Content.Load<SpriteFont>( "Std12" );
            fontLarge = Manager.Game.Content.Load<SpriteFont>( "Std42" );

            textBatch.Add( new TextDraw( fontSmall, "Welcome to", new Vector2( WindowWidth * 0.428f, WindowHeight * 0.125f ), Color.White ) );
            textBatch.Add( new TextDraw( fontLarge, "Ling.IO", new Vector2( WindowWidth * 0.5f, WindowHeight * 0.2125f ), Color.White ) );

            textBatch.Add( new TextDraw( fontSmall, "An English Game using a Dutch dictionary because I hate you",
                                         new Vector2( WindowWidth * 0.5f, WindowHeight * 0.4f ), Color.White ) );

            options.Add( new TextDraw( fontSmall, "New Game", new Vector2( WindowWidth * 0.5f, WindowHeight * 0.575f ), Color.White ) );
            options.Add( new TextDraw( fontSmall, "Options", new Vector2( WindowWidth * 0.5f, WindowHeight * 0.7f ), Color.White ) );
            options.Add( new TextDraw( fontSmall, "Quit Game", new Vector2( WindowWidth * 0.5f, WindowHeight * 0.825f ), Color.White ) );

            base.Initialize();
        }

        public override void Update( GameTime gameTime ) {

            if( Manager.InputHelper.GoUp )
                NavigateMenu( -1 );
            else if( Manager.InputHelper.GoDown )
                NavigateMenu( 1 );

            if( Manager.InputHelper.IsKeyPressed( Keys.Enter ) )
                InteractMenu();

            base.Update( gameTime );
        }

        public override void Draw( GameTime gameTime ) {

            spriteBatch.Begin();

            foreach( TextDraw td in textBatch )
                td.Draw( spriteBatch );

            for( int i = 0; i < options.Count; i++ ) {
                if( i == selectedOption )
                    options[i].DrawOffcolour( spriteBatch, Color.Red );
                else
                    options[i].Draw( spriteBatch );
            }

            spriteBatch.End();

            base.Draw( gameTime );
        }

        private void NavigateMenu( int change ) {

            selectedOption += change;

            if( selectedOption < 0 ) {

                if( menuWrapping ) selectedOption = options.Count - 1;
                else selectedOption = 0;

            } else if( selectedOption >= options.Count ) {

                if( menuWrapping ) selectedOption = 0;
                else selectedOption -= 1;
            }
        }

        //Todo: Menu options are assumed, requires clear definition
        private void InteractMenu() {

            switch( selectedOption ) {

                case 0: // New Game

                    // Start new scene
                    break;

                case 1: // Options

                    Manager.RequestSceneChange( typeof( MenuStub ) );
                    break;

                case 2: // Quit Game

                    Manager.Game.Exit();
                    break;
            }
        }
    }
}
