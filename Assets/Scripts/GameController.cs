using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace BustaGames.Climber
{
    public class GameController : MonoBehaviour
    {
        public PlayerController playerController;

        private bool _jumpInput;
        
        private void Update()
        {
            _jumpInput = Input.GetAxis("Vertical") > 0;
            playerController.HandleUpdate(Time.deltaTime, _jumpInput);
        }
    }
}