using System.Collections;
using System.Collections.Generic;
using Interfaces.ServiecesInterface;
using StateMachines;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public class ViewReplayUI : MonoBehaviour
    {
        public Text replayText;
        public Button exit, pause;
        // Start is called before the first frame update
        void Start()
        {

        }
        public void OnReplayExit()
        {
            ServiceLocator.Instance.get<IStateManager>().ChangeState(new GameOverState(), true);
        }
        public void DestroyUI()
        {

            pause.gameObject.SetActive(false);
            exit.gameObject.SetActive(false);
            replayText.gameObject.SetActive(false);
            pause.onClick.RemoveListener(OnReplayExit);
            exit.onClick.RemoveListener(OnReplayExit);
        }
        public void DisplayUI()
        {
            pause.onClick.AddListener(OnReplayPause);
            exit.onClick.AddListener(OnReplayExit);
            pause.gameObject.SetActive(true);
            exit.gameObject.SetActive(true);
            replayText.gameObject.SetActive(true);
        }
        public void OnReplayPause()
        {
            if (!(ServiceLocator.Instance.get<IStateManager>().GetCurrentState() is GamePauseState))
            {
                ServiceLocator.Instance.get<IStateManager>().ChangeState(new GamePauseState(), false);
            }
           // Debug.Log(StateManager.Instance.previousState + " " + StateManager.Instance.currentState);
        }
    }
}