using Replay_Service;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StateMachines
{
    public class GamePlayState : GameState
    {
      
        public GamePlayState()
        {
          
        }
        public override void OnStateExit()
        {
            ServiceUI.Instance.DestroyStartUI();
            

        }
        public override void OnStateEnter()
        {
            if (SceneManager.GetActiveScene().name != "GameScene")
            {
                SceneManager.LoadScene("GameScene");
            }else{
                Update();
            }
        }

        public override void Update()
        {
            ServiceUI.Instance.DisplayStartUI();
            //start.DisplayUI();
           
        }
    }
}