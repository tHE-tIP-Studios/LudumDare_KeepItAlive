using Talkie;
using UnityEngine;

namespace WALTApp
{
    public class WALTDebugger : MonoBehaviour
    {
        [SerializeField] private CharacterProfile _talkieProfile = null;
        [SerializeField] private bool _isPlayer = false;
        [TextArea]
        [SerializeField] private string _msgToSend = default;

        public bool Send()
        {
            if (_isPlayer)
                return MessageSender.SendMessage(_msgToSend);
            else
            {
                if(_talkieProfile == null) 
                {
                    Debug.LogWarning("Talkie profile is not assigned...");
                    return false;
                }
                return MessageSender.SendMessage(_talkieProfile, _msgToSend);
            }
        }
    }
}