using Cameras;
using Enemy;
using Interfaces.ServiecesInterface;
using Replay_Service;
using ScriptableObjects;
using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class GameApplication : Singleton<GameApplication>
    {
        ServiceCameraSettingService setCameras;
        bool playState = false;
        SafeSpawn spawn;
        public ScriptableEnemy[] enemy;
        public List<ScriptablePlayer> playerObjects = new List<ScriptablePlayer>();
        public event Action<ControllerPlayer> OnPlayerSpawn;
        public List<ControllerPlayer> players = new List<ControllerPlayer>();
        List<Vector3> EnemyPosition = new List<Vector3>();

        public void ReSpawnPlayer(Controls controls, PlayerNumber playerNumber)
        {
            StartCoroutine(LateRespawn(controls, playerNumber));
        }
        public void ReSpawnPlayer2(Controls controls, PlayerNumber playerNumber, Vector3 pos)
        {

            StartCoroutine(LateReplayRespawn(playerNumber, controls, pos));
            // AddPlayerController(new ControllerPlayer(player, pos, controls, playerNumber, false));

        }
        // Start is called before the first frame update
        void Start()
        {
            ServiceLocator.Instance.get<IStateManager>().OnStateChanged += OnStateChanged;
            spawn = new SafeSpawn();
        }
        public void SetPlayerPrefab(Color color)
        {
            foreach (ScriptablePlayer playerObject in playerObjects)
            {
                foreach (MeshRenderer renderer in playerObject.playerView.gameObject.GetComponentsInChildren<MeshRenderer>())
                {
                    renderer.sharedMaterial.SetColor("_Color", color);
                }
            }
        }
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnLevelLoaded;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelLoaded;
        }
        void OnLevelLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "GameScene")
            {
                setCameras = new ServiceCameraSettingService(playerObjects.Count);
                playState = true;
                for (int i = 0; i < playerObjects.Count; i++)
                {
                    Vector3 pos = spawn.SpawnPlayer(EnemyPosition);
                    ServiceReplay.Instance.SetPosition((PlayerNumber)i, pos, playerObjects[i].controls);
                    AddPlayerController(new ControllerPlayer(playerObjects[i].playerView.gameObject, pos, playerObjects[i].controls, (PlayerNumber)i, true));
                }
            }

        }
        void OnStateChanged(GameState currentState)
        {
            if (currentState is GameReplayState)
            {
                playState = false;
            }
            else
            {
                playState = true;
            }
        }

        public void AddPlayerController(ControllerPlayer player)
        {
            OnPlayerSpawn.Invoke(player);
            setCameras.SetCamerasToPlayer(player);
            GameApplication.Instance.players.Add(player);
            player.OnPlayerDeath += RemovePlayerController;
            Vector3 campos = player.GetPosition();
            campos = new Vector3(campos.z, campos.y + 25, campos.z);
        }
        public void RemovePlayerController(ControllerPlayer player, InputComponent inputComponent, Controls controls)
        {
            GameApplication.Instance.players.Remove(player);
            if (!playState)
            {
                ServiceReplay.Instance.ReplayReSpawn(player.GetPlayerNumber());
            }
            else
            {
                ReSpawnPlayer(player.GetControls(), player.GetPlayerNumber());
            }
            if (GameApplication.Instance.players.Count == 0)
            {
                if (ServiceLocator.Instance.get<IStateManager>().GetCurrentState() is GameReplayState)
                {
                    ServiceLocator.Instance.get<IStateManager>().ChangeState(new GameOverState(), true);
                }
                else
                {
                    ServiceLocator.Instance.get<IStateManager>().ChangeState(new GameReplayState(), false);
                }
            }
        }
        IEnumerator LateRespawn(Controls controls, PlayerNumber playerNumber)
        {
            yield return new WaitForSeconds(10f);
            if (SceneManager.GetActiveScene().name == "GameScene" && playState)
            {
                Vector3 pos = spawn.SpawnPlayer(ServiceLocator.Instance.get<IServiceEnemy>().GetEnemyPositions());
                AddPlayerController(new ControllerPlayer(playerObjects[(int)playerNumber].playerView.gameObject, pos, controls, playerNumber, false));
                ServiceReplay.Instance.SetPosition(playerNumber, pos, controls);
            }
        }
        IEnumerator LateReplayRespawn(PlayerNumber playerNumber, Controls controls, Vector3 pos)
        {
            yield return new WaitForSeconds(3f);
            if (!playState)
            {
                Debug.Log((int)playerNumber);
                AddPlayerController(new ControllerPlayer(playerObjects[(int)playerNumber].playerView.gameObject, pos, controls, playerNumber, false));
            }

        }
    }
}
