using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    [ShowInInspector]
    public class ZombieData
    {
        [ShowInInspector]
        public RuleTile.TilingRuleOutput.Transform PlayerTarget { get; set; }

        [ShowInInspector]
        public float DistanceToTarget { get; set; }

        [ShowInInspector]
        public float CurrentHealth { get; set; } = 100f;

        [ShowInInspector]
        public bool IsDead => CurrentHealth <= 0;

        [ShowInInspector]
        public bool IsStunned { get; set; }

        [ShowInInspector]
        public bool IsKnockedDown { get; set; }

        [ShowInInspector]
        public bool IsPushedBack { get; set; }

        [ShowInInspector]
        public bool IsDodging { get; set; }
    }
}