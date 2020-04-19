using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using WALTApp;
using Talkie;

namespace Scripts.Conversation
{
    public class ConversationMaster : MonoBehaviour
    {
        private const float MAX_STOP_TYPING_TIME = 1f;
        /// <summary>
        /// What the player can say
        /// </summary>
        [SerializeField] private PlayerPhraseDeck _playersAnswers = default;
        /// <summary>
        /// What the AI will say accordingly
        /// </summary>
        [SerializeField] private PlayerPhraseDeck _aiDialogue = default;
        [TextArea]
        [SerializeField] private string _debugString = default;
        /// <summary>
        /// Keeps track of phrases with certain ID and its answer
        /// </summary>
        private Dictionary<int, List<Phrase>> _dialogueNodes = default;
        private List<Phrase> _availablePhrases;
        private RottenConversation _evaluator;
        private int _activeChoices;
        private WaitForSeconds _secondsToWait;
        private Coroutine _typingRoutine;

        public Phrase LastChoice { get; set; }
        public Phrase LastAnswer { get; set; }
        public bool IsAITyping { get; private set; }
        public Action<bool> aiTurn;
        public int ActiveChoices
        {
            get => _activeChoices;
            set
            {
                _activeChoices = value;
                if (_activeChoices <= 0)
                {
                    outOfPhrases?.Invoke();
                    if (_typingRoutine != null) StopCoroutine(_typingRoutine);
                    StartCoroutine(AnswerAfterTime(MessageSender.MESSAGE_DELAY,
                        TalkieArea.ActiveProfile.MessagesOverText, WALTSceneManager.LoadMainMenu));
                    Debug.Log("You lost by Active Phrases");
                }
            }
        }

        public event Action outOfPhrases;
        public event Action gameOver;

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }

        private void Awake()
        {
            Debug.Log(_debugString);
            _secondsToWait = new WaitForSeconds(2f);
            ActiveChoices = 4;
            _evaluator = new RottenConversation();
            _availablePhrases = new List<Phrase>();
            // Initialize dictionary with ids
            _dialogueNodes = new Dictionary<int, List<Phrase>>(_playersAnswers.Entries);
            // Add ids to the dictionary
            for (int i = 0; i < _playersAnswers.Entries; i++)
            {
                if (_dialogueNodes.ContainsKey(_playersAnswers[i][0]))
                {
                    Debug.LogWarning("Check youw pwayew deck keys pwease!");
                }
                else
                    _dialogueNodes.Add(_playersAnswers[i][0], new List<Phrase>());

                _availablePhrases.Add(_playersAnswers[i]);
                for (int j = 0; j < _aiDialogue.Entries; j++)
                {
                    for (int k = 0; k < _aiDialogue[j].IDs.Length; k++)
                    {
                        // Check IDS and insert them into the dictionary
                        if (_dialogueNodes.ContainsKey(_aiDialogue[j][k]))
                        {
                            _dialogueNodes[_aiDialogue[j][k]].Add(_aiDialogue[j]);
                        }
                    }
                }
            }
        }

        public void CheckScore()
        {
            if (_evaluator.CurrentRating <= 0)
            {
                Debug.Log("You lost by Conversation Rating");
                gameOver?.Invoke();

                if (_typingRoutine != null) StopCoroutine(_typingRoutine);
                StartCoroutine(AnswerAfterTime(MessageSender.MESSAGE_DELAY,
                    TalkieArea.ActiveProfile.GameOverText, WALTSceneManager.LoadMainMenu));
            }
        }

        public Phrase NewPhrase()
        {
            // Declare a gameOver if this is a thing
            if (_availablePhrases.Count <= 0)
            {
                return new Phrase();
            }

            Phrase newPhrase = _availablePhrases
                [UnityEngine.Random.Range(0, _availablePhrases.Count)];

            _availablePhrases.Remove(newPhrase);
            return newPhrase;
        }

        public Phrase[] NewPhrase(int numberOfPhrases)
        {
            if (_availablePhrases.Count < numberOfPhrases)
                numberOfPhrases = _availablePhrases.Count;

            Phrase[] newPhrases = new Phrase[numberOfPhrases];

            for (int i = 0; i < numberOfPhrases; i++)
            {
                newPhrases[i] = NewPhrase();
            }

            return newPhrases;
        }

        public Phrase GetAnswerForCurrent()
        {
            if (!_dialogueNodes.ContainsKey(LastChoice.IDs[0]))
                Debug.LogError("Yo, you forgot to put this ID in");

            Phrase aiPhrase;
            aiPhrase = _dialogueNodes[LastChoice.IDs[0]][UnityEngine.Random.Range(0, _dialogueNodes[LastChoice.IDs[0]].Count)];

            // Update the AI answer
            LastAnswer = aiPhrase;

            // Update Score as we now have the answer from the player and the ai
            UpdateScore();
            return aiPhrase;
        }

        private void UpdateScore()
        {
            _evaluator.Evaluate(LastChoice.PhraseType, LastAnswer.PhraseType);
        }

        public void Answer()
        {
            float time = UnityEngine.Random.
                Range(MessageSender.MESSAGE_DELAY, MessageSender.MESSAGE_DELAY * 4);
            Phrase ai = GetAnswerForCurrent();
            Debug.Log(ai.Answer);
            Debug.Log(_evaluator.CurrentRating);

            _typingRoutine = StartCoroutine(AnswerAfterTime(time, ai.Answer));
        }

        private IEnumerator AnswerAfterTime(float time, string text, Action afterAnswer = null)
        {
            aiTurn?.Invoke(true);
            float current = 0;
            WaitForSeconds messageMinimum = new WaitForSeconds(MessageSender.MESSAGE_DELAY);
            yield return _secondsToWait;
            IsAITyping = true;

            while (current < time)
            {
                if (.001f > UnityEngine.Random.Range(0f, 1f) && IsAITyping)
                {
                    IsAITyping = false;
                    yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1f));
                    IsAITyping = true;
                }

                // Update clocks
                current += Time.deltaTime;
                yield return null;
            }

            IsAITyping = false;
            if (TalkieArea.ActiveProfile.Name == "Vladimir")
                text = text.ToUpper();

            if (text.Contains("\n"))
            {
                string[] concatStrings;
                concatStrings = text.Split('\n');
                foreach (string s in concatStrings)
                {
                    MessageSender.SendMessage(TalkieArea.ActiveProfile, s);
                    yield return messageMinimum;
                }
            }
            else
                MessageSender.SendMessage(TalkieArea.ActiveProfile, text);

            if (afterAnswer != null)
            {
                yield return new WaitForSeconds(3f);
                afterAnswer?.Invoke();
            }
            aiTurn?.Invoke(false);
        }
    }
}