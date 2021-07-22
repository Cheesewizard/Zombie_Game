using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBloodEffect : MonoBehaviour
{
    // Start is called before the first frame update

    public ParticleSystem[] bloods;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnBlood(Transform position) 
    {
            var blood = bloods[0];
            blood.transform.SetParent(gameObject.transform.parent.transform);
            Instantiate(blood, gameObject.transform.parent.transform.position, gameObject.transform.rotation);
            Destroy(blood, blood.main.duration);
    }
}
