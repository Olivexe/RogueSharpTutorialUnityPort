using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model;

namespace RogueSharpTutorial.View
{
    public class UI_Abilities : MonoBehaviour
    {
        public enum Window
        {
            Forget,
            Learn
        }

        private enum AbilityToUpdate
        {
            None,
            QAbility,
            WAbility,
            EAbility,
            RAbility
        }

        [SerializeField] private UI_Main    rootConsole;
        [SerializeField] private GameObject confirmationBox;
        [SerializeField] private GameObject thisWindow;

        [SerializeField] private Text       subTitleText;
        [SerializeField] private Text       qKeyText;
        [SerializeField] private Text       wKeyText;
        [SerializeField] private Text       eKeyText;
        [SerializeField] private Text       rKeyText;
        [SerializeField] private Text       qAbilityText;
        [SerializeField] private Text       wAbilityText;
        [SerializeField] private Text       eAbilityText;
        [SerializeField] private Text       rAbilityText;

        public Window                       WindowType          { get; private set; }

        private AbilityToUpdate             abilityToUpdate     { get; set; }

        private void Start()
        {
            abilityToUpdate = AbilityToUpdate.None;
        }

        private void Update()
        {
            if(thisWindow.activeSelf)
            {
                DrawAbilities();
            }
        }

        public InputCommands GetInput()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                rootConsole.CloseAllModalWindows();
                confirmationBox.SetActive(false);
                return InputCommands.None;
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                if(!confirmationBox.activeSelf)
                {
                    if (rootConsole.Game.Player.QAbility is DoNothing)
                    {
                        return InputCommands.None;
                    }
                    else
                    {
                        EnableConfirmationBox(true);
                        abilityToUpdate = AbilityToUpdate.QAbility;
                    }
                }
                else
                {
                    return InputCommands.None;
                }
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                if (!confirmationBox.activeSelf)
                {
                    if (rootConsole.Game.Player.WAbility is DoNothing)
                    {
                        return InputCommands.None;
                    }
                    else
                    {
                        EnableConfirmationBox(true);
                        abilityToUpdate = AbilityToUpdate.WAbility;
                    }
                }
                else
                {
                    return InputCommands.None;
                }
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (!confirmationBox.activeSelf)
                {
                    if (rootConsole.Game.Player.EAbility is DoNothing)
                    {
                        return InputCommands.None;
                    }
                    else
                    {
                        EnableConfirmationBox(true);
                        abilityToUpdate = AbilityToUpdate.EAbility;
                    }
                }
                else
                {
                    return InputCommands.None;
                }
            }
            if (Input.GetKeyUp(KeyCode.R))
            {
                if (!confirmationBox.activeSelf)
                {
                    if (rootConsole.Game.Player.RAbility is DoNothing)
                    {
                        return InputCommands.None;
                    }
                    else
                    {
                        EnableConfirmationBox(true);
                        abilityToUpdate = AbilityToUpdate.RAbility;
                    }
                }
                else
                {
                    return InputCommands.None;
                }
            }
            if (Input.GetKeyUp(KeyCode.Y))
            {
                if (confirmationBox.activeSelf)
                {
                    rootConsole.CloseAllModalWindows();
                    confirmationBox.SetActive(false);
                    switch(abilityToUpdate)
                    {
                        case AbilityToUpdate.QAbility:
                            return InputCommands.ForgetQAbility;
                        case AbilityToUpdate.WAbility:
                            return InputCommands.ForgetWAbility;
                        case AbilityToUpdate.EAbility:
                            return InputCommands.ForgetEAbility;
                        case AbilityToUpdate.RAbility:
                            return InputCommands.ForgetRAbility;
                        default:
                            return InputCommands.None;
                    }
                }
                else
                {
                    return InputCommands.None;
                }
            }
            if (Input.GetKeyUp(KeyCode.N))
            {
                if (confirmationBox.activeSelf)
                {
                    EnableConfirmationBox(false);
                }
                else
                {
                    return InputCommands.None;
                }
            }

            return InputCommands.None;
        }

        private void DrawAbilities()
        {
            if(rootConsole.Game.Player.QAbility is DoNothing)
            {
                qKeyText.text = "";
                qAbilityText.text = "";
            }
            else
            {
                qKeyText.text = "Q";
                qAbilityText.text = rootConsole.Game.Player.QAbility.Name;
            }

            if (rootConsole.Game.Player.WAbility is DoNothing)
            {
                wKeyText.text = "";
                wAbilityText.text = "";
            }
            else
            {
                wKeyText.text = "W";
                wAbilityText.text = rootConsole.Game.Player.WAbility.Name;
            }

            if (rootConsole.Game.Player.EAbility is DoNothing)
            {
                eKeyText.text = "";
                eAbilityText.text = "";
            }
            else
            {
                eKeyText.text = "E";
                eAbilityText.text = rootConsole.Game.Player.EAbility.Name;
            }

            if (rootConsole.Game.Player.RAbility is DoNothing)
            {
                rKeyText.text = "";
                rAbilityText.text = "";
            }
            else
            {
                rKeyText.text = "R";
                rAbilityText.text = rootConsole.Game.Player.RAbility.Name;
            }
        }

        private void EnableConfirmationBox(bool active)
        {
            confirmationBox.SetActive(active);
        }
    }
}