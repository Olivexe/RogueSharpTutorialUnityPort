using RogueSharpTutorial.Model;
using UnityEngine;

namespace RogueSharpTutorial.View
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private bool   isExploringMap;
        [SerializeField] private float  xPos, yPos;

        private void LateUpdate()
        {
            if (player != null && !isExploringMap)
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
            this.player         = player;
            xPos                = player.X;
            yPos                = player.Y;
            transform.position  = new Vector3(player.X, player.Y, -10);
        }

        public void SetPosition(float x, float y)
        {
            xPos += x;
            yPos += y;
        }

        public void SetCameraExploreMode (bool isExploring)
        {
            isExploringMap = isExploring;

            if(!isExploring)
            {
                transform.position = new Vector3(player.X, player.Y, -10);
            }
        }
    }
}