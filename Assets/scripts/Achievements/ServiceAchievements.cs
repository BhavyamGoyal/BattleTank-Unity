using Interfaces.ServiecesInterface;
using Player;
using SavingSystem;
using ScriptableObjects;
using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Achievements
{
    public class ServiceAchievements : IServiceAchievements
    {
        bool listen = true;

        private Achievement[] achievements;
        public event Action<string, int> OnAchievementUnlocked;
        Dictionary<AchievementTypes, Achievement> gameAchievements = new Dictionary<AchievementTypes, Achievement>();
        public ServiceAchievements()
        {
            ///GameApplication.Instance.OnPlayerSpawn += AddListener;
            achievements = Resources.LoadAll<Achievement>("Achievements");
            foreach (Achievement achievement in achievements)
            {
                gameAchievements.Add(achievement.achievementType, achievement);
            }
            GameApplication.Instance.OnPlayerSpawn += AddListener;
            ServiceLocator.Instance.get<IStateManager>().OnStateChanged += OnStateChanged;

        }
        public void OnStateChanged(GameState currentState)
        {
            if (currentState is GameReplayState)
            {
                listen = false;
            }
        }
        public void AddListener(ControllerPlayer player)
        {

            PlayerData playerData = player.playerData;
            player.OnBulletShot += AchievementUpdate;
            player.OnScoreUpdate += AchievementUpdate;
            player.OnEnemyKilled += AchievementUpdate;
            //Debug.Log("All listeners added for achievements");
            if (player.GetGameStarted())
            {
                playerData.progress = 1;
                playerData.achievementTypes = AchievementTypes.GamesPlayed;
                AchievementUpdate(playerData);
            }
            else
            {
                playerData.achievementTypes = AchievementTypes.NumberOfRespawns;
                playerData.progress = 1;
                AchievementUpdate(playerData);
            }
        }
        public void AchievementUpdate(PlayerData playerData)
        {

            AchievementData achieved;
            if (listen)
            {
                if (gameAchievements.ContainsKey(playerData.achievementTypes))
                {
                    achieved = gameAchievements[playerData.achievementTypes].UpdateAchievement(playerData.progress, playerData.player, SaveService.Instance.ReadAchievementData(gameAchievements[playerData.achievementTypes].achievementDisplayName, playerData.player.GetPlayerNumber()));
                    if (achieved.achievementUnlocked)
                    {
                        //Debug.Log("dddddd");
                        OnAchievementUnlocked.Invoke(achieved.achievementName + " : " + achieved.achievementLevelName + " Unlocked by " + achieved.player, achieved.achievementId);

                    }
                    SaveService.Instance.SaveAchievementsData(achieved);
                }
            }

        }


    }
}
