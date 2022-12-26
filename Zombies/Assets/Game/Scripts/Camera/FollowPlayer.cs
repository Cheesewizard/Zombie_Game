using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // camera will follow this object
    public Transform target;


    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        // update position
        Camera.main.transform.position = new Vector3(target.position.x, target.position.y, -10);
    }
}
