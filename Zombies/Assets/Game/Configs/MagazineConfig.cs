using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Configs
{
    [Serializable]
    public class MagazineConfig
    {
        // [SerializeField]
        // private Magazine magazinePrefab;
        // public Magazine MagazinePrefab => magazinePrefab;

        [SerializeField]
        private bool infiniteAmmo;
        public bool InfiniteAmmo => infiniteAmmo;
        private bool FiniteAmmo => !infiniteAmmo;

        [Tooltip("Maximum amount of magazines that the player can carry")]
        [SerializeField, ShowIf(nameof(FiniteAmmo))]
        private int totalMagazines;
        public int TotalMagazines => totalMagazines;

        [SerializeField, ShowIf(nameof(FiniteAmmo))]
        private int extraMagazines;
        public int ExtraMagazines => extraMagazines;

        [SerializeField]
        private int magazinePoolSize = 5;
        public int MagazinePoolSize => magazinePoolSize;
    }
}