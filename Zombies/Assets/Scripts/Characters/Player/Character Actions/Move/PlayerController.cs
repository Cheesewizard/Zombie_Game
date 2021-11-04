using Assets.Scripts.Interfaces;
using UnityEngine;

public class PlayerController : MonoBehaviour, IKillable, IDamageable, IPlayer
{
    public Camera targetCamera;
    public GameObject bullet;
    public GameObject bulletSpawner;
    private Animator animator;
    private BoxCollider2D meleeHitBox;

    public float movementSpeed = 20;
    public float health = 150;
    public bool godMode = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        meleeHitBox = gameObject.GetComponentInChildren<BoxCollider2D>();
        //Cursor.visible = false; // maybe remove. COuld add laser site as an item further into the game
    }

    // Update is called once per frame
    void Update()
    {
        LookAtMouse();
        MoveCharacter();
        UseWeapon();
        CheckMeleeAttack();
    }

    private void UseWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("IsGunShot");
            Instantiate(bullet, bulletSpawner.transform.position, bulletSpawner.transform.rotation);

            var playerLayer = 9;
             int layerMask = ~(1 << playerLayer); //Exclude layer 9
            RaycastHit2D hit = Physics2D.Raycast(bulletSpawner.transform.position, bulletSpawner.transform.position, Mathf.Infinity, layerMask);
            if (hit.collider != null)
            {
                var damageable = hit.collider.gameObject.GetComponentInParent<IDamageable>();
                if (damageable != null)
                {
                    var bloodSpawn = hit.collider.gameObject.GetComponent<SpawnBloodEffect>();
                    if (bloodSpawn != null)
                    {
                        bloodSpawn.SpawnBlood(hit.collider.transform);
                    }

                    // Must be damageable
                    var weapon = bullet.GetComponent<IWeapon>(); // Change this once I add another gun to use a weapon system
                    damageable.TakeDamage(weapon);

                    var knockBack = hit.collider.gameObject.GetComponent<IKnockbackable<GameObject>>();
                    if (knockBack != null)
                    {
                        knockBack.TakeKnockBack(hit.collider.gameObject, weapon.GetKnockback());
                    }
                }
            }
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

    private void MoveCharacter()
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
    public void TakeDamage(IWeapon weapon)
    {
        if (!godMode)
        {
            this.health -= weapon.GetDamage();
            if (this.health <= 0)
            {
                Kill();
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            var attack = collision.gameObject.GetComponent<IZombie>();
            //this.TakeDamage(attack.GetDamage());
            Debug.Log($"Player health = {health}");
        }
    }


    public float GetHealth()
    {
        return this.health;
    }

    public void TakeKnockBack(GameObject other, float force)
    {
        throw new System.NotImplementedException();
    }
}
