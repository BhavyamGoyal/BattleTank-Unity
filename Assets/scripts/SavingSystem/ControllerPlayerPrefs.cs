using Interfaces;
using UnityEngine;

namespace SavingSystem
{
    public class ControllerPlayerPrefs : ISave
    {
        public AchievementData ReadAchievementData(string achievementName, PlayerNumber player)
        {
            AchievementData data=new AchievementData();
            data.achievementName = achievementName;
            data.player = player;
            data.achievementLevel = PlayerPrefs.GetInt(achievementName + player + "level", 0);
            data.achievementProgress = PlayerPrefs.GetInt(achievementName + player + "progress", 0);
            return data;
        }

        public RewardsData ReadRewardData(int rewardUnlockedId)
        {
            RewardsData data=new RewardsData();
            data.Unlocked = PlayerPrefs.GetInt(rewardUnlockedId + "Rewards", 0);
            data.RewardUnlockedID = rewardUnlockedId;
            //Debug.Log(data.Unlocked+" ff "+data.RewardUnlockedID);
            return data;
        }

        public void SaveAchievementsData(AchievementData data)
        {
            PlayerPrefs.SetInt(data.achievementName + data.player + "level", data.achievementLevel);
            PlayerPrefs.SetInt(data.achievementName + data.player + "progress", data.achievementProgress);
        }
        public void SaveRewardsData(RewardsData data)
        {
            //Debug.Log(data.Unlocked+" "+data.RewardUnlockedID);
            PlayerPrefs.SetInt(data.RewardUnlockedID + "Rewards", data.Unlocked);
        }
    }
}