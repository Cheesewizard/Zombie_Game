using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        Instantiate(playerPrefab, spawnPoint);
    }
}