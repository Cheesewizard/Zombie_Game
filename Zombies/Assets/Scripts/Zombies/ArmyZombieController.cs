using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyZombieController : MonoBehaviour, IKillable, IDamageable<float>, IWeapon
{
    public float health = 50;
    public float attack = 10;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

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
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Melee"))
        {
            var weapon = collision.gameObject.GetComponent<IWeapon>();
            this.TakeDamage(weapon.GetDamage());

            // Destroy bullet upon hit regardless
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
            var player = collision.gameObject.GetComponent<IDamageable>();
            //player.GetDamage(attack);
        }
    }

    public float GetDamage()
    {
        return this.attack;
    }

    public int GetAmmo()
    {
        throw new System.NotImplementedException();
    }
}
