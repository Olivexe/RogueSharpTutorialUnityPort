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
            else if (Input.GetKeyUp(KeyCode.Comma))
            {
                return InputCommands.StairsUp;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                return InputCommands.Item1;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                return InputCommands.Item2;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                return InputCommands.Item3;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                return InputCommands.Item4;
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                return InputCommands.QAbility;
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                return InputCommands.WAbility;
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                return InputCommands.EAbility;
            }
            else if (Input.GetKeyUp(KeyCode.R))
            {
                return InputCommands.RAbility;
            }
            else if (Input.GetKeyUp(KeyCode.Escape))
            {
                return InputCommands.CloseGame;
            }

            return InputCommands.None;
        }
    }
}