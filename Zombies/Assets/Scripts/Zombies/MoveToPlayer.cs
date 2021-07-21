using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{

    private GameObject player;
    private GameObject zombie;
    public float moveSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        gameObject.transform.LookAt(player.transform);
        gameObject.transform.Rotate(0, 90, -90);
    }
}
