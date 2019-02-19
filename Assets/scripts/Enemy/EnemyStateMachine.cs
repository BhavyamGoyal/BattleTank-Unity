using Enemy.Controller;
using Interfaces;
using Interfaces.ServiecesInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{
    public class EnemyStateMachine
    {
        ControllerEnemy controller;
        EnemyState CurrentEnemyState,previousState;
        public EnemyStateMachine(ControllerEnemy controller){
            ServiceLocator.Instance.get<IServiceEnemy>().OnAlert+=OnAlert;
            CurrentEnemyState=EnemyState.Chansing;
            this.controller=controller;
            ChangeEnemyState(EnemyState.Petrolling,new Vector3(0,0,0));
        }
        public void DestroyMachine(){
            ServiceLocator.Instance.get<IServiceEnemy>().OnAlert-=OnAlert;
            controller=null;
        }

        public void OnAlert(Vector3 position){
           
                ChangeEnemyState(EnemyState.Chansing,position);
            // }else{
            //     ChangeEnemyState(EnemyState.Petrolling,new Vector3(0,0,0));
            // }
        }
        public void Notify(EnemyState state){
            ChangeEnemyState(state,new Vector3(0,0,0));
        }
        public void ChangeEnemyState(EnemyState state,Vector3 pos)
        {
            previousState=CurrentEnemyState;
            controller.DeactivateState(previousState);
            CurrentEnemyState=state;
            controller.ActivateState(CurrentEnemyState,pos);

        }
    }
}