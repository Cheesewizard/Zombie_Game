using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBloodEffect : MonoBehaviour
{
    // Start is called before the first frame update

    public ParticleSystem[] bloods;
    public float spawnOffset = 10f;

    public void SpawnBlood(Transform position)
    {
        bloods[0].transform.rotation = position.transform.rotation;
        bloods[0].Play();
    }
}
