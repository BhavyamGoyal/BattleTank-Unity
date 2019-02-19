using UnityEngine;
using UnityEditor;

public struct PlayerData
{
    public ControllerPlayer player;
    public int progress;
    public int score;
    public int health;
    public AchievementTypes achievementTypes;
}
public struct AchievementData{
    public string achievementName;
    public int achievementId;
    public int achievementLevel;
    public string achievementLevelName;
    public int achievementProgress;
    public bool achievementUnlocked;
    public PlayerNumber player;
}
public struct RewardsData{
    public int RewardUnlockedID;
    public int Unlocked;
}
public struct EnemyData{
    public Vector3 position;
    public int type;
      public Vector3 petrollingPosition;
}
public struct PlayerSpawnData{
    public Vector3 position;
    public Controls controlls;
  
}