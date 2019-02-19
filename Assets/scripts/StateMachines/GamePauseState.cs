using UnityEngine;

namespace StateMachines
{

    public class GamePauseState : GameState
    {
        ControllerMenuUI menu;
        public GamePauseState()
        {

        }
        public override void OnStateEnter()
        {
            menu = new ControllerMenuUI();
           // menu.DisplayUI();
           // Debug.Log("pause Enter");
        }
        public override void OnStateExit()
        {
            menu.DestroyUI();
            menu=null;

        }
        public override void Update()
        {
        }


    }
}