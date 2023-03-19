using System;
using Game.Scripts.Core.Loading;
using Game.Scripts.Gameplay.Services;
using Quack.Utils;
using UnityDependencyInjection;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Managers
{
    public class SpawnManager : MonoBehaviour, ISceneInitializedHandler
    {
        [Inject]
        private GameplayPlayerAccessService playerAccessService;

        // Temp property
        [Inject]
        private DependencyContainer dependencyContainer;

        [SerializeField]
        private float spawnDelay = 2;

        [SerializeField]
        private float spawnRate = 2;

        [SerializeField]
        private GameObject[] zombies;

        [SerializeField]
        private float minSpawnRadius = 100.0f;

        private void Awake()
        {
            SceneBroadcaster.RegisterReceiver(this);
        }

        public void HandleSceneInitialized()
        {
            InvokeRepeating("SpawnZombies", spawnDelay, spawnRate);
        }

        private void SpawnZombies()
        {
            CreateZombieAtRandomPosition();
            // TODO this is really expensive, think about creating a pooling system so that the objects are already dependency injected
            dependencyContainer.InjectToSceneObjects();
        }

        private void CreateZombieAtRandomPosition()
        {
            var index = Random.Range(0, 2);
            Instantiate(zombies[index], GetRandomPosition(), zombies[index].transform.rotation);
        }

        private Vector2 GetRandomPosition()
        {
            var index = Random.Range(0, 5);
            var ranWidth = 0f;
            var ranHeight = 0f;

            var bounds = playerAccessService.PlayerCamera.ScreenToWorldPoint(new Vector3(Screen.width,
                Screen.height, playerAccessService.PlayerCamera.transform.position.z));

            switch (index)
            {
                case 1:
                    ranHeight = bounds.y / 2 + minSpawnRadius;
                    break;
                case 2:
                    ranWidth = bounds.x / 2 + minSpawnRadius;
                    break;
                case 3:
                    ranHeight = bounds.y / 2 + -minSpawnRadius;
                    break;
                case 4:
                    ranWidth = bounds.x / 2 + -minSpawnRadius;
                    break;
            }

            return new Vector2(ranWidth, ranHeight);
        }
    }
}
