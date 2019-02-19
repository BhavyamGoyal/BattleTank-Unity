using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ScriptableUnlockable", menuName = "ScriptableObj/UnlockableObject", order = 0)]
    public class ScriptableUnlockable : ScriptableObject
    {
        public string unlockableName;
        public GameObject unlockableUI;
        public int onAchievementIdUnlocked;
        public GameObject playerPrefab;
        public Color color;
        
    }
}