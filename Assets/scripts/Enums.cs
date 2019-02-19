using UnityEngine;
using UnityEditor;
public enum BulletTypes
{
    defaultBullet = 1,
    fastBullet = 2,
    explossiveBullet = 3
}
public enum Controls
{
    WASD = 1,
    IJKL = 2
}
public enum EnemyState{
    Petrolling,
    Chansing
}
public enum PlayerNumber
{
    Player1 = 0,
    Player2 = 1,
    Player3 = 2,
    Player4 = 3
}
public enum SaveTypes{
    PlayePrefs,
    JSONFile
}
public enum PlayerStates{
    Move,
    Shoot,
    Regen,
    paused
}
public enum AchievementTypes
{
    BulletsShot=1,
    Score=2,
    GamesPlayed=3,
    EnemiesKilled=4,
    NumberOfRespawns=5    
}


