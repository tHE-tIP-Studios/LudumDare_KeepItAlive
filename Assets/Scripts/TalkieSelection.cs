using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Talkie
{
    public class TalkieSelection : MonoBehaviour
    {
        public const float MOVE_COOLDOWN = 1.0f;
        private const float DISTANCE_BETWEEN = 935.0f;
        [SerializeField] private GameObject _profilePrefab = null;
        [SerializeField] private CharacterProfile[] _profiles;
        [Space]
        [SerializeField] private Button _moveNextButton = null;
        [SerializeField] private Button _movePreviousButton = null;
        private List<TalkieSelectionProfile> _activeProfiles;

        private int _index;
        private float _timeOfLastMove;

        private bool CooldownOver => Time.time - _timeOfLastMove > MOVE_COOLDOWN;

        public bool CanMoveNext => _index + 1 < _profiles.Length && CooldownOver;
        public bool CanMovePrevious => _index - 1 >= 0 && CooldownOver;

        private void Awake()
        {
            _activeProfiles = new List<TalkieSelectionProfile>(_profiles.Length);
            CreateObjects();
            _moveNextButton.onClick.AddListener(ButtonMoveNext);
            _movePreviousButton.onClick.AddListener(ButtonMovePrevious);
        }

        private void CreateObjects()
        {
            float currentX = 0;
            foreach (CharacterProfile p in _profiles)
            {
                TalkieSelectionProfile newProfile =
                    Instantiate(_profilePrefab).GetComponent<TalkieSelectionProfile>();
                newProfile.transform.SetParent(transform);
                _activeProfiles.Add(newProfile);
                newProfile.Init(p);
                newProfile.MoveSideways(currentX);
                currentX += DISTANCE_BETWEEN;
            }
            _timeOfLastMove = Time.time;
        }

        private void ButtonMoveNext() => MoveNext();
        private void ButtonMovePrevious() => MovePrevious();

        public bool MoveNext()
        {
            if (!CanMoveNext) return false;
            _timeOfLastMove = Time.time;
            _index++;
            MoveAll(-DISTANCE_BETWEEN);
            return true;
        }

        public bool MovePrevious()
        {
            if (!CanMovePrevious) return false;
            _timeOfLastMove = Time.time;
            _index--;
            MoveAll(DISTANCE_BETWEEN);
            return true;
        }

        private void MoveAll(float xValue)
        {
            foreach (TalkieSelectionProfile p in _activeProfiles)
                p.MoveSideways(xValue);
        }

    }
}