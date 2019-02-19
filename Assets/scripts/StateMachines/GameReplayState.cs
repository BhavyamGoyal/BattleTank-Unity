using Enemy;
using Interfaces.ServiecesInterface;
using Replay_Service;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace StateMachines
{
    public class GameReplayState : GameState
    {
        ControllerReplayUI controller;
        public GameReplayState()
        {

        }
        public override void OnStateExit()
        {
            if (!(ServiceLocator.Instance.get<IStateManager>().GetPreviousState() is GameReplayState))
            {
                ServiceLocator.Instance.get<IInputManager>().ResetInput();
            }
            controller.DestroyUI();
            controller = null;
        }
        public override void OnStateEnter()
        {
            controller = new ControllerReplayUI();
            controller.DisplayUI();
            if (!(ServiceLocator.Instance.get<IStateManager>().GetPreviousState() is GamePauseState))
            {
                ServiceLocator.Instance.get<IServiceEnemy>().RemoveAllEnemy();
                ServiceReplay.Instance.ReplaySpawn();
                ServiceReplay.Instance.SetQueue();

            }
            else
            {
            }
        }
        public override void Update()
        {



        }
    }
}