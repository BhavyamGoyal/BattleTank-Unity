using Interfaces;
using System.IO;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
namespace SavingSystem
{
    public class ControllerJSON : ISave
    {
        string path = "Assets/Data.json";
        public Dictionary<int, JsonRewards> rewards;
        public Dictionary<string, JsonAchievements> achievements;
        public ControllerJSON()
        {
            if(!File.Exists(path)){
                File.Create(path).Dispose();
            }
            //SaveAchievementsData(new AchievementData());
            Read();
        }
        public AchievementData ReadAchievementData(string achievementName, PlayerNumber player)
        {
            AchievementData data = new AchievementData();
            if (achievements.ContainsKey(achievementName + player))
            {
                data.achievementLevel = achievements[achievementName + player].achievementLevel;
                data.achievementName = achievementName;
                data.achievementProgress = achievements[achievementName + player].achievementProgress;
                data.player = player;
            }
            else
            {
                data.achievementLevel = 0;
                data.achievementProgress = 0;
                data.player = player;
            }
            return data;
        }

        public RewardsData ReadRewardData(int rewardUnlockedID)
        {

            RewardsData data = new RewardsData();
            if (rewards.ContainsKey(rewardUnlockedID))
            {
                data.RewardUnlockedID = rewardUnlockedID;
                data.Unlocked = rewards[rewardUnlockedID].unlocked;

            }
            else
            {
                data.RewardUnlockedID = rewardUnlockedID;
                data.Unlocked = 0;
            }
            return data;
        }

        public void SaveAchievementsData(AchievementData data)
        {
            JsonAchievements pdata = new JsonAchievements();
            pdata.achievementProgress = data.achievementProgress;
            pdata.achievementName = data.achievementName;
            pdata.achievementLevel = data.achievementLevel;
            pdata.player = data.player;
            if (achievements.ContainsKey(pdata.achievementName + pdata.player))
            {
                achievements[pdata.achievementName + pdata.player] = pdata;
            }
            else
            {
                achievements.Add(pdata.achievementName + pdata.player, pdata);
            }
        }
        public void SaveRewardsData(RewardsData data)
        {
            JsonRewards pdata = new JsonRewards();
            pdata.RewardUnlockableID = data.RewardUnlockedID;
            pdata.unlocked = data.Unlocked;
            rewards.Add(data.RewardUnlockedID, pdata);
        }
        public void Save()
        {
            JsonAchievementsAndReward jsonAchievementsAndReward = new JsonAchievementsAndReward();
            jsonAchievementsAndReward.achievements = achievements.Values.ToList();
            jsonAchievementsAndReward.Rewards = rewards.Values.ToList();
            string jsondata = JsonUtility.ToJson(jsonAchievementsAndReward);
            File.OpenWrite(path);
            File.WriteAllText(path, jsondata);

        }
        public void Read()
        {
            JsonAchievementsAndReward jsonAchievementsAndReward = new JsonAchievementsAndReward();
            File.OpenRead(path);
            string data = File.ReadAllText(path);
            rewards = new Dictionary<int, JsonRewards>();
            achievements = new Dictionary<string, JsonAchievements>();
            if (data.Length == 0)
            {
                return;
            }
            else
            {
                jsonAchievementsAndReward = JsonUtility.FromJson<JsonAchievementsAndReward>(data);
            }

            foreach (JsonAchievements achievement in jsonAchievementsAndReward.achievements)
            {
                achievements.Add(achievement.achievementName + achievement.player, achievement);
            }

            foreach (JsonRewards reward in jsonAchievementsAndReward.Rewards)
            {
                rewards.Add(reward.RewardUnlockableID, reward);
            }
            jsonAchievementsAndReward = null;
        }
        ~ControllerJSON(){
            Save();
        }
        [System.Serializable]
        public class JsonAchievements
        {
            public PlayerNumber player;
            public string achievementName;
            public int achievementLevel;
            public int achievementProgress;
        }

        [System.Serializable]
        public class JsonRewards
        {
            public int RewardUnlockableID;
            public int unlocked;
        }

        [System.Serializable]
        public class JsonAchievementsAndReward
        {
            public List<JsonAchievements> achievements;
            public List<JsonRewards> Rewards;
        }

    }
}