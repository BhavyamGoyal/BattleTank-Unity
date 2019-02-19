using ScriptableObjects;
using UnityEngine;

namespace ObjectPooling
{
    public interface IPoolableEnemy : IPoolable
    {
        void Set(ScriptableEnemy enemyTemp, Vector3 position, int type);
        void Set(ScriptableEnemy enemyTemp, int type);
    }
}