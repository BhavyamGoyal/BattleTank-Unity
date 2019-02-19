using System.Collections.Generic;
using UnityEngine;

namespace Cameras
{
    public class ServiceCameraSettingService
    {

        public Camera mainCam, MiniCam;
        Vector3 mainCamOffset = new Vector3(0, -15, 20);
        List<Rect> camRect = new List<Rect>();
        public List<RenderTexture> minimapRenderer = new List<RenderTexture>();
        Dictionary<PlayerNumber, playerCams> cameraPlayer = new Dictionary<PlayerNumber, playerCams>();
        public ServiceCameraSettingService(int numberOfPlayers)
        {
            camRect.Add(new Rect(0, 0, 1f / numberOfPlayers, 1));
            camRect.Add(new Rect(0.5f, 0, 0.5f, 1));
            minimapRenderer.Add(Resources.Load<RenderTexture>("MiniMap1"));
            minimapRenderer.Add(Resources.Load<RenderTexture>("MiniMap2"));
            MiniCam = Resources.Load<GameObject>("MiniMapCam").GetComponent<Camera>();
            mainCam = Resources.Load<GameObject>("MainCamera").GetComponent<Camera>();
        }
        public void SetCamerasToPlayer(ControllerPlayer player)
        {
            player.OnPlayerDeath += playerDead;
            if (!cameraPlayer.ContainsKey(player.GetPlayerNumber()))
            {
                playerCams playercamObject = new playerCams();
                playercamObject.mainCam = GameObject.Instantiate(mainCam.gameObject).GetComponent<Camera>();
                playercamObject.MiniCam = GameObject.Instantiate(MiniCam.gameObject).GetComponent<Camera>();
                playercamObject.miniCamFollow = playercamObject.MiniCam.gameObject.GetComponent<MiniMap>();
                //playercamObject.MainCAmeraFollow = playercamObject.mainCam.gameObject.GetComponent<MainCamera>();
                playercamObject.texture = minimapRenderer[(int)player.GetPlayerNumber()];
                cameraPlayer.Add(player.GetPlayerNumber(), playercamObject);
            }
            else
            {
                cameraPlayer[player.GetPlayerNumber()].miniCamFollow.gameObject.SetActive(true);
                cameraPlayer[player.GetPlayerNumber()].mainCam.gameObject.SetActive(true);
            }
            cameraPlayer[player.GetPlayerNumber()].MiniCam.targetTexture = cameraPlayer[player.GetPlayerNumber()].texture;
            ServiceUI.Instance.SetMiniMap(player.GetPlayerNumber(), cameraPlayer[player.GetPlayerNumber()].texture);
            cameraPlayer[player.GetPlayerNumber()].miniCamFollow.target = player.GetPlayerObject();
            cameraPlayer[player.GetPlayerNumber()].mainCam.rect = camRect[(int)player.GetPlayerNumber()];
            cameraPlayer[player.GetPlayerNumber()].mainCam.transform.SetParent(player.GetPlayerObject().transform);
            cameraPlayer[player.GetPlayerNumber()].mainCam.transform.position = player.GetPosition() - mainCamOffset;
            cameraPlayer[player.GetPlayerNumber()].mainCam.transform.LookAt(player.GetPosition());
            //cameraPlayer[player.GetPlayerNumber()].MainCAmeraFollow.target = player.GetPlayerObject();
        }
        public void playerDead(ControllerPlayer player, InputComponent component, Controls controls)
        {
            cameraPlayer[player.GetPlayerNumber()].mainCam.transform.SetParent(null);
            cameraPlayer[player.GetPlayerNumber()].miniCamFollow.target = null;
            //cameraPlayer[player.GetPlayerNumber()].MainCAmeraFollow.target = null;
            cameraPlayer[player.GetPlayerNumber()].miniCamFollow.gameObject.SetActive(false);
            cameraPlayer[player.GetPlayerNumber()].mainCam.gameObject.SetActive(false);
        }
        
        struct playerCams
        {
            public Camera mainCam, MiniCam;
            public MiniMap miniCamFollow;
            //public MainCamera MainCAmeraFollow;
            public RenderTexture texture;
        }
    }
}