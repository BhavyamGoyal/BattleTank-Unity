
using Interfaces;

namespace SavingSystem
{
    public class SaveService : NonMonoSingleton<SaveService>
    {
        ISave saveService;
        SaveTypes type = SaveTypes.PlayePrefs;
        public SaveService()
        {
            switch (type)
            {
                case SaveTypes.JSONFile: saveService = new ControllerJSON(); break;
                case SaveTypes.PlayePrefs: saveService = new ControllerPlayerPrefs(); break;
            }
        }
        public void SaveRewardsData(RewardsData data)
        {
            saveService.SaveRewardsData(data);
        }
        public void SaveAchievementsData(AchievementData data)
        {
            saveService.SaveAchievementsData(data);
        }
        public RewardsData ReadRewardData(int rewardUnlockID)
        {
            return saveService.ReadRewardData(rewardUnlockID);
        }
        public AchievementData ReadAchievementData(string achievementName, PlayerNumber player)
        {
            return saveService.ReadAchievementData(achievementName,player);
        }
    }
}