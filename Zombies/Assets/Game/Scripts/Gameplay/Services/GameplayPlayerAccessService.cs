using Game.Scripts.Gameplay.Player;
using UnityEngine;

namespace Game.Scripts.Gameplay.Services
{
    public class GameplayPlayerAccessService : IPlayerTransformAccessService, IPlayerWeaponBehaviourAccessService
    {
        private readonly Transform playerTransform;
        Transform IPlayerTransformAccessService.PlayerTransform => playerTransform;

        private readonly AbstractPlayerWeaponBehaviour weaponBehaviour;
        AbstractPlayerWeaponBehaviour IPlayerWeaponBehaviourAccessService.WeaponBehaviour => weaponBehaviour;

        public PlayerRig PlayerRig { get; }

        public Transform PlayerTransform => PlayerRig.transform;

        public Camera PlayerCamera { get; }

        public GameplayPlayerAccessService(PlayerRig playerRig)
        {
            PlayerRig = playerRig;
            PlayerCamera = PlayerRig.GetComponentInChildren<Camera>();
            weaponBehaviour = playerRig.WeaponBehaviour;
            playerTransform = playerRig.transform;
            //pauseMenuInvoker = PlayerRig.GetComponentInChildren<PauseMenuInvoker>();
        }

    }
}