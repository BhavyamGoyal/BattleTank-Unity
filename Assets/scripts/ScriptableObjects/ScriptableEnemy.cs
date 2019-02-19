using UnityEngine;
using UnityEditor;
using Enemy;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "ScriptableEnemy", menuName = "ScriptableObj/EnemyObject", order = 0)]
    public class ScriptableEnemy : ScriptableObject
    {
        public int maxHealth;
        public ViewEnemy enemyPrefab;
        public float Speed;
        public int score;
        public Color color;
    }
}