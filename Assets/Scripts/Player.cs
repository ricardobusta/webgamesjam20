using System;
using UnityEngine;

namespace BustaGames.Climber
{
    public class Player: MonoBehaviour
    {
        public Rigidbody2D rb;
        public SpriteRenderer sprite;
        public Animator animator;

        public event Action<Collision2D> CollisionEvent;

        private void OnCollisionEnter2D(Collision2D other)
        {
            CollisionEvent?.Invoke(other);
        }
    }
}