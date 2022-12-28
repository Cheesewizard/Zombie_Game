using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStart : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    void Start()
    {
        Instantiate(player, Vector3.zero, player.transform.rotation);
    }

}
