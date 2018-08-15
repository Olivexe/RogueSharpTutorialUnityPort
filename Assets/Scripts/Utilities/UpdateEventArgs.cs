using System;

namespace RogueSharpTutorial.Utilities
{
    public delegate void UpdateEventHandler(object sender, UpdateEventArgs e);

    public class UpdateEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the time in seconds since the last update.
        /// </summary>
        public float Time { get; private set; }

        public UpdateEventArgs(float time)
        {
            this.Time = time;
        }
    }
}