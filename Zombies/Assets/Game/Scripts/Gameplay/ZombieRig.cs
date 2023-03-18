using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class ZombieRig : MonoBehaviour
    {
        public ZombieData Data { get; private set; } = new ZombieData();

        [SerializeField, Required, Find(Destination.AllChildren)]
        private ZombieLogic zombieLogic;
        public ZombieLogic ZombieLogic => zombieLogic;

        [SerializeField, Required, Find(Destination.AllChildren)]
        private ZombieVisual zombieVisual;
        public ZombieVisual ZombieVisual => zombieVisual;
    }
}