using System;
using System.Timers;

namespace Battleship
{

    public class TurnTimer
    {
        private int TimeLimit = 10;
        private Timer _timer;
        private TimerWriter _writer= new TimerWriter();
        private int _elapsedSec;

        private BattleShipsGame _game;

        public int TimeLeft{
            get {
                return _elapsedSec;
            }
        }

        public string Output {
            get {
                string output= "";
                
                if (_elapsedSec > 0) {
                    output += "Time remaining= " + _elapsedSec.ToString();
                } else{
                    output += "You lost your turn!!";
                }
                return output;
            }
        }

        public TurnTimer(BattleShipsGame game)
        {
            _game = game;
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Elapsed += SecondElapsed;
            _elapsedSec = TimeLimit;
        }

        public void StartTimer() {
            _elapsedSec = TimeLimit;
            _timer.Enabled = true;
        }

        public void StopTimer() {
            _timer.Stop();
        }

        public void SecondElapsed(Object source, System.Timers.ElapsedEventArgs e) {
            if(_elapsedSec <= 6 && _elapsedSec >1) {
                _writer.PlaySound();
            }
            if (_elapsedSec == 0)
            {
                _game.TogglePlayer();
            }
            else {
                _elapsedSec--;
            }
            Console.WriteLine(_elapsedSec);
        }
    }
}
