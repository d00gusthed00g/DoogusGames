using System;
using Microsoft.Xna.Framework;

namespace TetrisClone
{
    public class Timer
    {

        public const int TICK_TIME = 1000;
        public const int FIRST_KEYPRESS_TIME = 1000;
        public const int REPEATING_KEYPRESS_TIME = 300;
        public const int ROTATE_TAP_KEYPRESS_TIME = 100;
        public const int ROTATE_HOLD_KEYPRESS_TIME = 5000;


        public Timer()
        {
            Ticks = 0;
            TimeSinceLastKeypress = 0;
        }

        public bool ResetKeypressTimer()
        {
            TimeSinceLastKeypress = 0;
            return true;
        }
        public bool ResetTicks()
        {
            Ticks = 0;
            return true;
        }

        public bool UpdateTicks(TimeSpan ts)
        {
            Ticks += ts.TotalMilliseconds;
            return true;
        }

        public void IncrementKeypress()
        {
            TimeSinceLastKeypress += this.Ticks;
        }

        public double Ticks { get; private set; }

        public double TimeSinceLastKeypress { get; private set; }
    }
}