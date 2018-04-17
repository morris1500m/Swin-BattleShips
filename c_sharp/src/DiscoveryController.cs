using SwinGameSDK;
using System;
/// <summary>
/// The battle phase is handled by the DiscoveryController.
/// </summary>
/// 

namespace Battleship
{
    internal static class DiscoveryController
    {

        /// <summary>
        /// Handles input during the discovery phase of the game.
        /// </summary>
        /// <remarks>
        /// Escape opens the game menu. Clicking the mouse will
        /// attack a location.
        /// </remarks>
        public static void HandleDiscoveryInput()
        {
            if (SwinGame.KeyTyped(KeyCode.vk_a))
            {
                GameController.AddNewState(GameState.ViewingGameMenu);
            }

            if (SwinGame.MouseClicked(MouseButton.LeftButton))
            {
                DoAttack();
            }
        }

        /// <summary>
        /// Attack the location that the mouse if over.
        /// </summary>
        private static void DoAttack()
        {
            Point2D mouse = SwinGame.MousePosition();


            //Calculate the row/col clicked
            int row = 0;
            int col = 0;
            row = Convert.ToInt32(Math.Floor((mouse.Y - UtilityFunctions.FIELD_TOP) / (UtilityFunctions.CELL_HEIGHT + UtilityFunctions.CELL_GAP)));
            col = Convert.ToInt32(Math.Floor((mouse.X - UtilityFunctions.FIELD_LEFT) / (UtilityFunctions.CELL_WIDTH + UtilityFunctions.CELL_GAP)));

            if (row >= 0 && row < GameController.HumanPlayer.EnemyGrid.Height)
            {
                if (col >= 0 && col < GameController.HumanPlayer.EnemyGrid.Width)
                {
                    GameController.Attack(row, col);
                }
            }
        }

        /// <summary>
        /// Draws the game during the attack phase.
        /// </summary>s
        public static void DrawDiscovery()
        {
            const int SCORES_LEFT = 172;
            const int SHOTS_TOP = 157;
            const int HITS_TOP = 206;
            const int SPLASH_TOP = 256;
            const int TIMER_TOP = 300;
            const int TIMER_LEFT = 100;

            if (((SwinGame.KeyDown(KeyCode.vk_b) || (SwinGame.KeyDown(KeyCode.vk_c)) && SwinGame.KeyDown(KeyCode.vk_d))))
            {
                UtilityFunctions.DrawField(GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, true);
            }
            else
            {
                UtilityFunctions.DrawField(GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, false);
            }

            UtilityFunctions.DrawSmallField(GameController.HumanPlayer.PlayerGrid, GameController.HumanPlayer);
            UtilityFunctions.DrawMessage();

            SwinGame.DrawText(GameController.HumanPlayer.Shots.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, SHOTS_TOP);
            SwinGame.DrawText(GameController.HumanPlayer.Hits.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, HITS_TOP);
            SwinGame.DrawText(GameController.HumanPlayer.Missed.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, SPLASH_TOP);
            SwinGame.DrawText(GameController.Timer.Output, Color.Red, GameResources.GameFont("ArialMed"), TIMER_LEFT, TIMER_TOP);
        }

    }
}
