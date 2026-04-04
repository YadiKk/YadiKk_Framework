using System;
using UnityEngine;

namespace YadikkFramework.State
{
    public class GameStateManager : SingletonPersistent<GameStateManager>
    {
        public GameState CurrentState { get; private set; } = GameState.None;

        /// <summary>
        /// Triggered when the game state changes. (PreviousState, NewState)
        /// </summary>
        public event Action<GameState, GameState> OnGameStateChanged;

        private GameState _prePauseState = GameState.None;

        public void ChangeState(GameState newState)
        {
            if (CurrentState == newState)
                return;

            GameState previousState = CurrentState;
            CurrentState = newState;

            OnGameStateChanged?.Invoke(previousState, newState);
        }

        public void PauseGame()
        {
            if (CurrentState == GameState.Paused)
                return;

            _prePauseState = CurrentState;
            Time.timeScale = 0f;
            ChangeState(GameState.Paused);
        }

        public void ResumeGame()
        {
            if (CurrentState != GameState.Paused)
                return;

            Time.timeScale = 1f;

            if (_prePauseState != GameState.None)
            {
                ChangeState(_prePauseState);
                _prePauseState = GameState.None;
            }
            else
            {
                ChangeState(GameState.Playing);
            }
        }
    }
}
