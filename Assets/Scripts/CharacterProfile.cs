using System.Collections;
using System.Collections.Generic;
using Scripts;
using UnityEngine;

namespace Talkie
{
    [CreateAssetMenu(fileName = "Talkie Profile", menuName = "KeepItAlive/Talkie Profile")]
    public class CharacterProfile : ScriptableObject
    {
        [SerializeField] private Sprite _characterImage = null;
        [SerializeField] private Sprite _background = null;
        [SerializeField] private string _name = null;
        [SerializeField] private Color _iconicColor = Color.black;
        [SerializeField] private string _iconicWord = null;
        [SerializeField] private Scripts.PhraseType _mostUsedType = Scripts.PhraseType.DEFAULTSTATE;
        [TextArea]
        [SerializeField] private string _description = null;
        [TextArea]
        [SerializeField] private string _gameOverText = null;

        public Sprite CharacterImage => _characterImage;
        public string Name => _name;
        public string IconicWord => _iconicWord;
        public string Description => _description;
        public Color IconicColor => _iconicColor;
        public PhraseType MostUsedType => _mostUsedType;
        public string GameOverText => _gameOverText;

        public Sprite Background => _background;
    }

}