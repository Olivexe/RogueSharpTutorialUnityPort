using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RogueSharpTutorial.View
{
    public class UI_Messages : MonoBehaviour
    {
        [SerializeField] private Text messagesText;

        /// <summary>
        /// Write the set of messages to the screen.
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="color"></param>
        public void PostMessageLog(Queue<string> messages, Color color)
        {
            string toPrint = "";

            string[] lines = messages.ToArray();

            for (int i = 0; i < lines.Length; i++)
            {
                toPrint += lines[i];
                if (i < lines.Length - 1)
                {
                    toPrint += Environment.NewLine;
                }
            }

            messagesText.color = color;
            messagesText.text = toPrint;
        }
    }
}