using UnityEngine;

namespace Scripts.Conversation
{
    public class RottenConversation
    {
        private const int MAX_EVALUATION = 100;
        private int _ratingAC;
        private int _finalRating;
        private int _lastRating;

        public int CurrentRating => _finalRating;
        
        /// <summary>
        /// Negative if the player lost points, positive if otherwise
        /// </summary>
        public int RatingDifference => _finalRating - _lastRating;
        
        public RottenConversation()
        {
            _finalRating = MAX_EVALUATION;
        }

        public int DetermineWinner(PhraseType player, PhraseType ai)
        {
            return ((int) player + (- (int)ai)) % 4;
        }

        public void DebugValues()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Debug.Log($"{(PhraseType)i}, {(PhraseType)j}: {DetermineWinner((PhraseType)i, (PhraseType)j)}");
                }
            }
        }

        /// <summary>
        /// Evaluates the current player rating
        /// </summary>
        /// <param name="player">Player's last choice</param>
        /// <param name="ai"> AI last answer </param>
        /// <returns> Current rating of the player </returns>
        public int Evaluate(PhraseType player, PhraseType ai)
        {
            _lastRating = _finalRating;
            _ratingAC += DetermineWinner(player, ai);
            _finalRating = MAX_EVALUATION + _ratingAC;
            _finalRating = Mathf.Clamp(_finalRating, 0, MAX_EVALUATION);
            return _finalRating;
        }
    }
}