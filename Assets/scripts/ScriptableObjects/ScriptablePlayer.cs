using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ScriptablePlayer", menuName = "ScriptableObj/PlayerObject", order = 0)]
    public class ScriptablePlayer : ScriptableObject
    {
        public ViewPlayer playerView;
        public Controls controls;

    }
}