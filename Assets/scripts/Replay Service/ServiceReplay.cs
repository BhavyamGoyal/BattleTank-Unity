using System.Collections;
using System.Collections.Generic;
using Enemy;
using Interfaces.ServiecesInterface;
using Player;
using StateMachines;
using UnityEngine;

namespace Replay_Service
{
    public class ServiceReplay : NonMonoSingleton<ServiceReplay>
    {
        List<EnemyData> enemyData = new List<EnemyData>();
        public Dictionary<PlayerNumber, Queue<PlayerSpawnData>> playerPosition = new Dictionary<PlayerNumber, Queue<PlayerSpawnData>>();
        Dictionary<Controls, Queue<InputData>> inputRecord = new Dictionary<Controls, Queue<InputData>>();
        public ServiceReplay()
        {

        }
        public void RecordInput(InputData inputData, Controls controls)
        {
            if (!(ServiceLocator.Instance.get<IStateManager>().GetCurrentState() is GamePauseState))
            {
                if (inputData.direction != 0 || inputData.forward != 0 || inputData.boost || inputData.shoot)
                {

                    if (inputRecord.ContainsKey(controls))
                    {
                        inputRecord[controls].Enqueue(inputData);
                    }
                    else
                    {
                        Queue<InputData> inputq = new Queue<InputData>();
                        inputq.Enqueue(inputData);
                        inputRecord.Add(controls, inputq);
                       
                    }
                }
            }
        }
        public void OnEnemySpawn()
        {
            enemyData = ServiceLocator.Instance.get<IServiceEnemy>().EnemyData;
        }

        public void SetPosition(PlayerNumber playerNumber, Vector3 position, Controls controlls)
        {
           
            PlayerSpawnData spawnData = new PlayerSpawnData();
            spawnData.position = position;
            spawnData.controlls = controlls;
            if (playerPosition.ContainsKey(playerNumber))
            {
                playerPosition[playerNumber].Enqueue(spawnData);
            }
            else
            {
                Queue<PlayerSpawnData> q = new Queue<PlayerSpawnData>();
                q.Enqueue(spawnData);
                playerPosition.Add(playerNumber, q);
            }
        }
        public void ReplaySpawn()
        {
            foreach (PlayerNumber playerNumber in playerPosition.Keys)
            {
                GameApplication.Instance.ReSpawnPlayer2(playerPosition[playerNumber].Peek().controlls, playerNumber, playerPosition[playerNumber].Peek().position);
                playerPosition[playerNumber].Dequeue();
            }
            foreach (EnemyData enemy in enemyData)
            {
                ServiceLocator.Instance.get<IServiceEnemy>().SpawnEnemy(enemy);
            }
        }
        public void ReplayReSpawn(PlayerNumber playerNumber)
        {
            if (playerPosition[playerNumber].Count != 0)
            {
                GameApplication.Instance.ReSpawnPlayer2(playerPosition[playerNumber].Peek().controlls, playerNumber, playerPosition[playerNumber].Peek().position);
                playerPosition[playerNumber].Dequeue();
            }
            
        }
        public void SetQueue()
        {
            ServiceLocator.Instance.get<IInputManager>().SetQueue(inputRecord);
        }
    }
}