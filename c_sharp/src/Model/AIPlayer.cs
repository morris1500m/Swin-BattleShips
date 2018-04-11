/*/ <summary>
''' The AIPlayer is a type of player. It can readomly deploy ships, it also has the
''' functionality to generate coordinates and shoot at tiles
''' </summary>*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{

    public abstract class AIPlayer : Player
    {

        /// <summary>
        /// Location can store the location of the last hit made by an
        /// AI Player. The use of which determines the difficulty.
        /// </summary>
        


        public AIPlayer(BattleShipsGame game) : base(game)
        {
        }

        /*/ <summary>
        ''' Generate a valid row, column to shoot at
        ''' </summary>
        ''' <param name="row">output the row for the next shot</param>
        ''' <param name="column">output the column for the next show</param>*/
        protected abstract void GenerateCoords(ref int row, ref int column);

        /*/ <summary>
        ''' The last shot had the following result. Child classes can use this
        ''' to prepare for the next shot.
        ''' </summary>
        ''' <param name="result">The result of the shot</param>
        ''' <param name="row">the row shot</param>
        ''' <param name="col">the column shot</param>*/
        protected abstract void ProcessShot(int row, int col, AttackResult result);

        /*/ <summary>
        ''' The AI takes its attacks until its go is over.
        ''' </summary>
        ''' <returns>The result of the last attack</returns>*/
        public override AttackResult Attack()
        {
            AttackResult result;
            int row = 0;
            int column = 0;

            do
            {
                Delay();
                GenerateCoords(ref row, ref column);
                result = _game.Shoot(row, column);
                ProcessShot(row, column, result);
            }
            while (result.Value != ResultOfAttack.Miss && result.Value != ResultOfAttack.GameOver && !SwinGame.WindowCloseRequested)// generate coordinates for shot// take shot
    ;

            return result;
        }

        /*/ <summary>
        ''' Wait a short period to simulate the think time
        ''' </summary>*/
        private void Delay()
        {
            int i;
            for (i = 0; i <= 150; i++)
            {
                // Dont delay if window is closed
                if (SwinGame.WindowCloseRequested)
                    return;

                SwinGame.Delay(5);
                SwinGame.ProcessEvents();
                SwinGame.RefreshScreen();
            }
        }
    }
}