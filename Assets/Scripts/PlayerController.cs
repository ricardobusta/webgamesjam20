using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace BustaGames.Climber
{
    public class PlayerController : MonoBehaviour
    {
        public float playerSpeed = 5f;
        public float collisionDistance = 0.5f;
        public float jumpTime = 1f;
        public float jumpSpeed = 1f;
        public Player player;
        public LayerMask wallMask;
        public LayerMask ceilMask;
        public LayerMask floorMask;

        private bool _hitFloor;
        private bool _hitCeil;

        private readonly RaycastHit2D[] _rayCastHit = new RaycastHit2D[1];
        private Vector3 _direction = Vector3.left;
        private bool _jumping;
        private bool _grounded;
        private PlayerState _state;
        private static readonly int StartRunning = Animator.StringToHash("start_running");
        private static readonly int VerticalSpeed = Animator.StringToHash("vertical_speed");

        private enum PlayerState
        {
            Idle,
            Moving,
            Dead
        }

        public void Start()
        {
            player.CollisionEvent += OnPlayerCollisionEvent;
            UpdatePlayerDirection();
            _grounded = true;
            _jumping = false;
        }

        public void HandleUpdate(float dt, bool jumpInput)
        {
            switch (_state)
            {
                case PlayerState.Idle:
                    HandleIdle();
                    break;
                case PlayerState.Moving:
                    HandleMovement(dt, jumpInput);
                    break;
                case PlayerState.Dead:
                    HandleDead();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleDead()
        {
        }

        private void HandleIdle()
        {
            player.animator.SetTrigger(StartRunning);
            player.rb.velocity = Vector2.left * playerSpeed;
            _state = PlayerState.Moving;
        }

        private void HandleMovement(float dt, bool jumpInput)
        {
            if (jumpInput)
            {
                if (_grounded && !_jumping)
                {
                    _jumping = true;
                    _grounded = false;
                    player.rb.velocity = new Vector2(_direction.x*playerSpeed, jumpSpeed);
                }
            }
            else
            {
                _jumping = false;
            }

            var position = player.transform.position;

            player.animator.SetFloat(VerticalSpeed, player.rb.velocity.y);
            
            if (CollideWithWall(position))
            {
                //HandleWallCollision();
            }
        }

        private void HandleWallCollision()
        {
            _direction.x *= -1;
            player.rb.velocity = new Vector2(_direction.x*playerSpeed, player.rb.velocity.y);
            UpdatePlayerDirection();
        }

        private void UpdatePlayerDirection()
        {
            player.sprite.flipX = _direction.x < 0;
        }

        private void OnPlayerCollisionEvent(Collision2D other)
        {
            if (OnLayer(floorMask, other.gameObject.layer))
            {
                if(Vector2.Dot(other.contacts[0].normal, Vector2.up) > 0.99f)
                {
                    _grounded = true;
                }
            }
            
            if (OnLayer(ceilMask, other.gameObject.layer))
            {
                Debug.Log("FAIL");
            }
            
            if (OnLayer(wallMask, other.gameObject.layer))
            {
                if(Vector2.Dot(other.contacts[0].normal, -_direction) > 0.99f)
                {
                    HandleWallCollision();
                }
            }
        }

        private bool OnLayer(LayerMask mask, int layer)
        {
            var result = mask == (mask | 1 << layer);
            return result;
        }

        private bool CollideWithWall(Vector3 position)
        {
            var pos = position;
            var dir = _direction;
            Debug.DrawRay(pos, dir);
            return Physics2D.RaycastNonAlloc(pos, dir, _rayCastHit, collisionDistance, wallMask) > 0;
        }
    }
}