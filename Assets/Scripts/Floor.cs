using System;
using UnityEditor;
using UnityEngine;

namespace BustaGames.Climber
{
    [ExecuteInEditMode]
    public class Floor : MonoBehaviour
    {
        public float floorHeight;

        private Vector3 _nextAnchor;

        private void Awake()
        {
            _nextAnchor = new Vector3(0, floorHeight, 0);
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Selection.activeObject == gameObject)
            {
                _nextAnchor = new Vector3(0, floorHeight, 0);
            }
        }

        private void OnDrawGizmosSelected()
        {
            var pos = transform.position;
            Gizmos.DrawWireSphere(pos, 0.5f);
            Gizmos.DrawWireSphere(pos + _nextAnchor, 0.5f);
        }

#endif
    }
}