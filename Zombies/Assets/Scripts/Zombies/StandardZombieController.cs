using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardZombieController : MonoBehaviour, IKillable, IDamageable<float>, IWeapon
{
    public float health = 50;
    public float attack = 10;
    public float knockback = 0.5f;

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
        gameObject.GetComponent<MoveToPlayer>().enabled = false;
        gameObject.GetComponent<StandardZombieController>().enabled = false;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    public void TakeDamage(float damageTaken)
    {
        this.health -= damageTaken;
        if (this.health <= 0)
        {
            Kill();
        }
    }

    public void TakeKnockBack(float force)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(gameObject.transform.position.x * force, gameObject.transform.position.y * force), ForceMode2D.Impulse);
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Melee"))
        {
            var bloodSpawn = gameObject.GetComponent<SpawnBloodEffect>();
            bloodSpawn.SpawnBlood(collision.gameObject.transform);

            var weapon = collision.gameObject.GetComponent<IWeapon>();
            this.TakeDamage(weapon.GetDamage());
            this.TakeKnockBack(weapon.GetKnockback());

            // Destroy bullet upon hit regardless
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
            var player = collision.gameObject.GetComponent<IDamageable<float>>();
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

    public float GetKnockback()
    {
        return this.knockback;
    }
}
