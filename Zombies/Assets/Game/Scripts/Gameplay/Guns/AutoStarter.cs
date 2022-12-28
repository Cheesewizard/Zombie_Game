using UnityDependencyInjection;
using UnityEngine;

namespace Game.Scripts.Gameplay.Guns
{
    public class AutoStarter : MonoBehaviour
    {
        public static bool IsGameRunningStandalone { get; set; } = true;

        private async void Start()
        {
            if (IsGameRunningStandalone)
            {
                IsGameRunningStandalone = false;
                Debug.Log("AutoStarter is taking over!");

                await CoreScene.LoadAsync();
            }
        }
    }
    
}