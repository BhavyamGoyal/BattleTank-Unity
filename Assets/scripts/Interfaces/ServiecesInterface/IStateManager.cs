using System;
using StateMachines;

namespace Interfaces.ServiecesInterface
{
    public interface IStateManager : IServices
    {
        GameState GetCurrentState();
        event Action<GameState> OnStateChanged;
        void ChangeState();
        void ChangeState(GameState state, bool isLoading);
        GameState GetPreviousState();
    }
}