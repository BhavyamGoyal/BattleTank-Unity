using System.Collections.Generic;
using Enemy.Controller;
using ScriptableObjects;
using UnityEngine;

namespace Interfaces.ServiecesInterface
{
    public interface IServiceEnemy : IServices
    {
         void StartSpawning();
        List<EnemyData> EnemyData
        {
            get;
        }
        void SpawnEnemy();

        void SpawnEnemy(EnemyData enemy);
        void SetAlert(Vector3 playerPos);
        event System.Action<Vector3> OnAlert;
        void RemoveEnemy(ControllerEnemy enemy);
        List<Vector3> GetEnemyPositions();
        void RemoveAllEnemy();

    }
}