using System;
using SwinGameSDK;

namespace Battleship
{
    public class TimerWriter
    {

        public void PlaySound()
        {
            Audio.PlaySoundEffect(GameResources.GameSound("Beep"));
        }
    }
}
