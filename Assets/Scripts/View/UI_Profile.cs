using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model;

namespace RogueSharpTutorial.View
{
    public class UI_Profile : MonoBehaviour
    {
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

        private AbilityToUpdate             abilityToUpdate     { get; set; }

        private void Start()
        {
            abilityToUpdate = AbilityToUpdate.None;
        }

        private void Update()
        {
            if(thisWindow.activeSelf)
            {
                DrawProfile();
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

            return InputCommands.None;
        }

        private void DrawProfile()
        {
        
        }

        private void EnableConfirmationBox(bool active)
        {
            confirmationBox.SetActive(active);
        }
    }
}