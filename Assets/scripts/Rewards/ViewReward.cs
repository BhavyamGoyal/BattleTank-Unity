
using Interfaces.ServiecesInterface;
using Player;
using UnityEngine;
using UnityEngine.UI;
namespace Rewards
{
    public class ViewReward : MonoBehaviour
    {
        public Button button;
        bool canSelect = false;
        public Text name;
        public Image indicator;
        public GameObject panal;
        ControllerReward controller;

        public void SetViewReference(ControllerReward controller)
        {

            button.onClick.AddListener(ButtonClick);
            this.controller = controller;
            indicator.gameObject.SetActive(false);
        }
        void ButtonClick()
        {  ServiceLocator.Instance.get<IServiceRewards>().Selected(controller); }

        public void SetText(string name)
        {
            this.name.text = name;
        }

        public void Select()
        {
            if (canSelect)
            {
                GameApplication.Instance.SetPlayerPrefab(controller.unlockable.color);
                indicator.gameObject.SetActive(true);
            }
        }
        public void DeSelect()
        {
            
                indicator.gameObject.SetActive(false);
            
        }
        public void SetSelectable()
        {
            panal.SetActive(false);
            canSelect = true;
        }
    private void OnDestroy() {
            controller.RemoveListener();
        }
    }
}
