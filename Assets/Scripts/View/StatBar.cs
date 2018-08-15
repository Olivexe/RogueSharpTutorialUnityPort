using UnityEngine;
using UnityEngine.UI;
using RogueSharpTutorial.Model;

namespace RogueSharpTutorial.View
{
    public class StatBar : MonoBehaviour
    {
        [SerializeField] private Text symbolText;
        [SerializeField] private Text nameText;
        [SerializeField] private Slider healthBarSlider;
        [SerializeField] private Image fillImage;

        /// <summary>
        /// Will write the symobl and Name of the Monster. Will also generate a Hit Point bar that will decrease as the Monster's health decreases.
        /// </summary>
        /// <param name="health"></param>
        /// <param name="maxHealth"></param>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="symbol"></param>
        public void SetSlider(int health, int maxHealth, string name, Color color, char symbol)
        {
            if (maxHealth < 1)
            {
                maxHealth = 1;
            }

            if (health > maxHealth)
            {
                health = maxHealth;
            }
            else if (health < 0)
            {
                health = 0;
            }

            healthBarSlider.value = (float)health / (float)maxHealth;

            if (nameText != null)
            {
                nameText.text = name;
            }

            if (symbolText != null)
            {
                string sym = "" + symbol;
                symbolText.text = sym;
            }

            nameText.color = ColorMap.UnityColors[Colors.White];
            symbolText.color = color;
            fillImage.color = color;
        }
    }
}