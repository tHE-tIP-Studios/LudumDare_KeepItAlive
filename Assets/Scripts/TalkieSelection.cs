using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Talkie
{
    public class TalkieSelection : MonoBehaviour
    {
        public const float MOVE_COOLDOWN = 1.0f;
        private const float DISTANCE_BETWEEN = 200.0f;
        [SerializeField] private GameObject _profilePrefab = null;
        [SerializeField] private CharacterProfile[] _profiles;
        private List<GameObject> _profileObjs;

        private int _index;

        public bool CanMoveNext => _index + 1 < _profiles.Length;
        public bool CanMovePrevious => _index - 1 >= 0;

        private void Awake()
        {
            _profileObjs = new List<GameObject>(_profiles.Length);
            CreateObjects();
        }

        private void CreateObjects()
        {
            foreach(CharacterProfile p in _profiles)
            {

            }

        }

        public void MoveNext()
        {
            if (!CanMoveNext) return;
            _index++;

        }

        public void MovePrevious()
        {
            if (!CanMovePrevious) return;
            _index--;

        }

    }
}