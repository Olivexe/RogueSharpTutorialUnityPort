using UnityEngine;

namespace RogueSharpTutorial.View
{
    public class InputKeyboard : MonoBehaviour
    {
        private InputCommands   input;

        /// <summary>
        /// Will return the last keyboard up pressed. Will then clear the input to None.
        /// </summary>
        public  InputCommands Command
        {
            get
            {
                InputCommands returnVal = input;
                input = InputCommands.None;
                return returnVal;
            }
        }

        private void Update()
        {
            input = GetKeyboardValue();
        }

        private InputCommands GetKeyboardValue()
        {
            if (Input.GetKeyUp(KeyCode.Keypad7))
            {
                return InputCommands.UpLeft;
            }
            else if (Input.GetKeyUp(KeyCode.Keypad8) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                return InputCommands.Up;
            }
            else if (Input.GetKeyUp(KeyCode.Keypad9))
            {
                return InputCommands.UpRight;
            }
            else if (Input.GetKeyUp(KeyCode.Keypad4) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                return InputCommands.Left;
            }
            else if (Input.GetKeyUp(KeyCode.Keypad6) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                return InputCommands.Right;
            }
            else if (Input.GetKeyUp(KeyCode.Keypad1))
            {
                return InputCommands.DownLeft;
            }
            else if (Input.GetKeyUp(KeyCode.Keypad2) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                return InputCommands.Down;
            }
            else if (Input.GetKeyUp(KeyCode.Keypad3))
            {
                return InputCommands.DownRight;
            }
            else if (Input.GetKeyUp(KeyCode.Period))
            {
                return InputCommands.StairsDown;
            }
            else if (Input.GetKeyUp(KeyCode.Escape))
            {
                return InputCommands.CloseGame;
            }

            return InputCommands.None;
        }
    }
}