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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            var blood = Instantiate(bloods[0], gameObject.transform.position, Quaternion.identity);
            blood.transform.SetParent(collision.transform);
            Destroy(bloods[0], bloods[0].main.duration);
        }
    }
}
