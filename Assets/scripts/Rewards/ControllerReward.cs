using Interfaces.ServiecesInterface;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Rewards
{
    public class ControllerReward
    {
        ViewReward view;
        bool isUnlocked;
        public ScriptableUnlockable unlockable;


        public ControllerReward(ScriptableUnlockable unlockable, RectTransform scrollContent, bool unlocked)
        {
            this.unlockable = unlockable;
            isUnlocked = unlocked;
            SetScrollView(unlockable, scrollContent);
            ServiceLocator.Instance.get<IServiceRewards>().OnSelection += Selection;
            
        }

        public void SetScrollView(ScriptableUnlockable unlockable, RectTransform scrollContent)
        {
            GameObject obj;
            obj = GameObject.Instantiate(unlockable.unlockableUI);
            obj.gameObject.transform.SetParent(scrollContent);
            view = obj.GetComponent<ViewReward>();
            view.SetText(unlockable.unlockableName);
            view.SetViewReference(this);
            if (isUnlocked)
            {
                view.SetSelectable();
            }
        }
        public void RemoveListener(){
              ServiceLocator.Instance.get<IServiceRewards>().OnSelection -= Selection;
        }

        public void Selection(ControllerReward reward)
        {
            if (reward == this)
            {
                view.Select();
                //Debug.Log("Selected");
                
            }
            else
            {
                view.DeSelect();
            }

        }



    }
}