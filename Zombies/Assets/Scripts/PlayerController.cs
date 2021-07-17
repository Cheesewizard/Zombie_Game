using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera targetCamera;
    public Rigidbody2D rb;
    public float movementSpeed = 5;
    public float maxSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtMouse();
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, -moveVertical, 0.0f);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        else
        {
            rb.AddForce(movement * movementSpeed);
        }

        if (moveHorizontal == 0 && moveVertical == 0)
        {
            rb.velocity = Vector3.zero;
        }
       

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    rb.velocity = Vector3.down * movementSpeed * Time.deltaTime;
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    rb.velocity = Vector3.up * movementSpeed * Time.deltaTime;
        //}

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    rb.velocity = Vector3.left * movementSpeed * Time.deltaTime;
        //}

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    rb.velocity = Vector3.right * movementSpeed * Time.deltaTime;
        //}
    }

    private void LookAtMouse()
    {
        Vector3 mousePos = targetCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 perpendicular = transform.position - mousePos;
        gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);
    }
}
