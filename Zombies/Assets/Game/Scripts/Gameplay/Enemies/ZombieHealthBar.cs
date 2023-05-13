using Game.Scripts.Gameplay;
using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Zombieland.Gameplay.Enemies
{
    public class ZombieHealthBar : MonoBehaviour
    {
        [SerializeField, Required, Find(Destination.Ancestors)]
        private ZombieRig zombieRig;

        [SerializeField, Required, Find(Destination.Self)]
        private TextMeshPro text;

        private ZombieHealth zombieHealth => zombieRig.ZombieLogic.ZombieHealth;

        private void Start()
        {
            text.text = zombieHealth.CurrentHealth.ToString();
            zombieHealth.OnDamaged += HandleZombieDamaged;
            zombieHealth.OnKilled += HandleZombieKilled;
        }

        private void HandleZombieDamaged(ZombieDamageInfo obj)
        {
            text.text = zombieHealth.CurrentHealth <= 0f ? "0" : zombieHealth.CurrentHealth.ToString();
        }

        private void HandleZombieKilled(ZombieDamageInfo obj)
        {
            text.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            zombieHealth.OnDamaged -= HandleZombieDamaged;
            zombieHealth.OnKilled -= HandleZombieKilled;
        }
    }
}