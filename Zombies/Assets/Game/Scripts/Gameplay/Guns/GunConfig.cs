using System;
using UnityEngine;

namespace Game.Scripts.Gameplay.Guns
{
    [Serializable]
    public class GunConfig
    {
        [SerializeField]
        private GunTypes GunType;

        public float FireRate;
    }
}