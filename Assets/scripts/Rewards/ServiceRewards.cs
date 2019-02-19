using System;
using System.Collections.Generic;
using System.Linq;
using Achievements;
using Interfaces;
using Interfaces.ServiecesInterface;
using SavingSystem;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Rewards
{
    public class ServiceRewards : IServiceRewards
    {
        [SerializeField] List<ScriptableUnlockable> unlockables = new List<ScriptableUnlockable>();
        public RectTransform scrollContent, scrollView;
        [SerializeField] Dictionary<ScriptableUnlockable, bool> unlockablesDictionary = new Dictionary<ScriptableUnlockable, bool>();

        public event Action<ControllerReward> OnSelection;
        public RectTransform scrollPlaceHolder;
        public ServiceRewards()
        {
            unlockables = Resources.LoadAll<ScriptableUnlockable>("Rewards").ToList();
           // Debug.Log(unlockables.Count);
            scrollContent=Resources.Load<GameObject>("Unlockable").GetComponent<RectTransform>();
            scrollView=Resources.Load<GameObject>("ScrollView").GetComponent<RectTransform>();
            
            foreach (ScriptableUnlockable unlockable in unlockables)
            {
                unlockablesDictionary.Add(unlockable, (1 == SaveService.Instance.ReadRewardData(unlockable.onAchievementIdUnlocked).Unlocked));
            }
            unlockables = null;
            //PlayerPrefs.DeleteAll();
            ServiceLocator.Instance.get<IServiceAchievements>().OnAchievementUnlocked += UnlockReward;
            SceneManager.sceneLoaded += OnLevelLoaded;
        }

        ~ServiceRewards()
        {
            SceneManager.sceneLoaded -= OnLevelLoaded;
        }
        void OnLevelLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "MainMenu")
            {
                scrollPlaceHolder = GameObject.FindGameObjectWithTag("ScrollPlaceHolder").gameObject.GetComponent<RectTransform>();
                RectTransform obj = GameObject.Instantiate(scrollView, scrollPlaceHolder);
                scrollContent = obj.GetComponentInChildren<HorizontalLayoutGroup>().gameObject.GetComponent<RectTransform>();
                SetScrollView(unlockablesDictionary.Keys.ToList());
            }
        }
        public void UnlockReward(String name, int Id)
        {
            RewardsData data = new RewardsData();
            data.RewardUnlockedID = Id;
            data.Unlocked = 1;
            SaveService.Instance.SaveRewardsData(data);
        }
        private void SetScrollView(List<ScriptableUnlockable> unlockables)
        {
            GameObject obj;
            for (int i = 0; i < unlockables.Count; i++)
            {
                // int unlocked = PlayerPrefs.GetInt(unlockables[i].onAchievementIdUnlocked + "Unlockable", 0);

                new ControllerReward(unlockables[i], scrollContent, unlockablesDictionary[unlockables[i]]);
                //obj.transform.SetParent();
            }
            scrollContent.sizeDelta = new Vector2(scrollContent.sizeDelta.x + ((unlockables.Count - 4) * 111), scrollContent.sizeDelta.y);
        }
        public void Selected(ControllerReward reward)
        {
            OnSelection?.Invoke(reward);
        }
    }
}