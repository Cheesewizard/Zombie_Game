using UnityEngine;

namespace Game.Scripts.Gameplay.Guns
{
    public class BulletNew : MonoBehaviour
    {
        [SerializeField] 
        private GameObject bullet;
        
        public void Create()
        {
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }
}