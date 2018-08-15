using RogueSharpTutorial.Model;
using UnityEngine;

namespace RogueSharpTutorial.View
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField]
        private Player player;

        private float xPos, yPos;

        private void LateUpdate()
        {
            if (player != null)
            {
                xPos = player.X;
                yPos = player.Y;
            }

            transform.position = new Vector3(xPos, yPos, -10);
        }

        /// <summary>
        /// Init Camera and position Camera over player.
        /// </summary>
        /// <param name="player"></param>
        public void InitCamera(Player player)
        {
            this.player = player;
            xPos = player.X;
            yPos = player.Y;
            transform.position = new Vector3(player.X, player.Y, -10);
        }
    }
}