namespace Interfaces
{
    public interface ISave
    {
        void SaveAchievementsData(AchievementData data);
        void SaveRewardsData(RewardsData data);
        RewardsData ReadRewardData(int rewardIdUnlocked);
        AchievementData ReadAchievementData(string achievementName,PlayerNumber player);
         
    }
}