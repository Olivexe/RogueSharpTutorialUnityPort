using System.Collections.Generic;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class MessageLog
    {       
        private static readonly int     maxLines = 9;       // Define the maximum number of lines to store
        private readonly Queue<string>  lines;              // Use a Queue to keep track of the lines of text
        private Game                    game;

        public MessageLog(Game game)
        {
            this.game = game;
            lines = new Queue<string>();
        }

        /// <summary>
        /// Add a line to the MessageLog queue.
        /// </summary>
        /// <param name="message"></param>
        public void Add(string message)
        {
            lines.Enqueue(message);

            if (lines.Count > maxLines)                     // When exceeding the maximum number of lines remove the oldest one.
            {
                lines.Dequeue();
            }
        }

        /// <summary>
        /// Draw each line of the MessageLog queue to the console
        /// </summary>
        public void Draw()
        {
            game.PostMessageLog(lines, Colors.White);
        }
    }
}