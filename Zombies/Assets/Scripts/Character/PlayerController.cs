using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IKillable, IDamageable<float>
{
    public Camera targetCamera;
    public GameObject bullet;
    public GameObject bulletSpawner;
    private Animator animator;
    private BoxCollider2D meleeHitBox;

    public float movementSpeed = 20;
    public float health = 150;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        meleeHitBox = gameObject.GetComponentInChildren<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtMouse();
        MoveCharacterTransform();
        CheckFiregun();
        CheckMeleeAttack();
    }

    private void CheckFiregun()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("IsGunShot");
            Instantiate(bullet, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
        }
    }

    private void CheckMeleeAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetTrigger("IsKnifeAttack");
            meleeHitBox.isTrigger = !meleeHitBox.isTrigger;
        }
    }

    private void MoveCharacterTransform()
    {
        // Gets player input
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        // Toggles running animation
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput + verticalInput));

        if (Input.GetButton("Vertical"))
        { 
            transform.Translate(0, verticalInput * movementSpeed * Time.deltaTime, 0, Space.World);
        }

        if (Input.GetButton("Horizontal"))
        {
            transform.Translate(horizontalInput * movementSpeed * Time.deltaTime, 0, 0, Space.World);
        }
    }

    private void LookAtMouse()
    {
        Vector3 mousePos = targetCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 perpendicular = transform.position - mousePos;
        gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);
    }

    public void Kill()
    {
        // This uses a state behaviour within the animator that deletes the gameObject after the death animation.
        animator.SetTrigger("Death");
    }
    public void TakeDamage(float damageTaken)
    {
        this.health -= damageTaken;
        if (this.health <= 0)
        {
            Kill();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            var attack = collision.gameObject.GetComponent<IWeapon>();
            this.TakeDamage(attack.GetDamage());
            Debug.Log($"Player health = {health}");
        }
    }
}
