using UnityEngine;
using UnityEngine.Serialization;

namespace BustaGames.Climber
{
    public class GameController : MonoBehaviour
    {
        public PlayerController playerController;
        
        private void FixedUpdate()
        {
            playerController.HandleFixedUpdate(Time.fixedDeltaTime);
        }
    }
}
