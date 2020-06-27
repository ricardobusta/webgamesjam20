using UnityEngine;
using UnityEngine.Serialization;

namespace BustaGames.Climber
{
    public class PlayerController: MonoBehaviour
    {
        public float playerSpeed;
        public float collisionDistance;
        public Player player;
        public LayerMask wallMask;

        private readonly RaycastHit2D[] _rayCastHit = new RaycastHit2D[1];
        private Vector3 _direction = Vector3.left;

        public void HandleFixedUpdate(float dt)
        {
            var tr = player.transform;
            var position = tr.position;
            
            if (!CollideWithWall(position))
            {
                HandlePlayerMovement(dt, ref position);
            }
            else
            {
                HandleWallCollision();
            }

            tr.position = position;
        }

        private void HandlePlayerMovement(float dt, ref Vector3 position)
        {
            position += _direction * (dt * playerSpeed);
        }

        private void HandleWallCollision()
        {
            _direction.x *= -1;
        }

        private bool CollideWithWall(Vector3 position)
        {
            Debug.DrawRay(position, _direction * collisionDistance);
            return Physics2D.RaycastNonAlloc(position, _direction, _rayCastHit, collisionDistance, wallMask) > 0;
        }
    }
}