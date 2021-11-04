using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBloodEffect : MonoBehaviour
{
    // Start is called before the first frame update

    public ParticleSystem[] bloods;
    public float spawmOffset = 10f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnBlood(Transform position)
    {
        bloods[0].transform.rotation = position.transform.rotation;
        bloods[0].Play();
    }
}
