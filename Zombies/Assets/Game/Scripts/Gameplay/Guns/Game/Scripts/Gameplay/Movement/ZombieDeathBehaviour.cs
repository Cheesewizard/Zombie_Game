using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using Zombieland.Gameplay.Enemies;

namespace Game.Scripts.Gameplay.Guns.Game.Scripts.Gameplay.Movement
{
    public class ZombieDeathBehaviour : MonoBehaviour
    {
        [SerializeField, Required, Find(Destination.Ancestors)]
        private ZombieRig zombieRig;

        private ZombieHealth zombieHealth => zombieRig.ZombieLogic.ZombieHealth;

        private void Start()
        {
            zombieHealth.OnKilled += HandleZombieKilled;
        }

        private void HandleZombieKilled(ZombieDamageInfo obj)
        {
            zombieRig.ZombieLogic.gameObject.SetActive(false);
            zombieRig.ZombieVisual.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            zombieHealth.OnKilled -= HandleZombieKilled;
        }
    }
}
