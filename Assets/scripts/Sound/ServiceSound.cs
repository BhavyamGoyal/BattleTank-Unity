using System.Collections.Generic;
using Interfaces.ServiecesInterface;
using Player;
using UnityEngine;

namespace Sound
{
    public class ServiceSound : IServiceSound
    {
        AudioClip backGroudSound, shootSound, tankExplodeSound;
        AudioSource backGroundAudioSource, oneShotAudioSource;
        public ServiceSound()
        {
            backGroudSound = Resources.Load<AudioClip>("BackGroudClip");
            shootSound = Resources.Load<AudioClip>("ShotFiring");
            tankExplodeSound = Resources.Load<AudioClip>("TankExplosion");
            AudioSource[] sources = GameObject.FindObjectsOfType<AudioSource>();
            backGroundAudioSource = sources[0];
            oneShotAudioSource = sources[1];
            oneShotAudioSource.playOnAwake = false;
            GameApplication.Instance.OnPlayerSpawn += AddPlayerListeners;
        }

        public void AddPlayerListeners(ControllerPlayer player)
        {
            player.OnBulletShot += TankShooTSound;
            player.OnPlayerDeath += TankExplosionSound;
        }
        public void BackGroundSound()
        {
        }

        public void TankExplosionSound(ControllerPlayer player, InputComponent inputComponent, Controls controls)
        {
            oneShotAudioSource.clip = tankExplodeSound;
            oneShotAudioSource.Play();

        }
        public void RemoveListeners(ControllerPlayer player)
        {
            //player.OnBulletShot -= TankShooTSound;
            player.OnPlayerDeath -= TankExplosionSound;
        }

        public void TankShooTSound(PlayerData data)
        {
            oneShotAudioSource.clip = shootSound;
            oneShotAudioSource.Play();
        }
    }
}