using Assets.Scripts.Interfaces;
using UnityEngine;

public class StandardZombieController : MonoBehaviour, IKillable, IDamageable, IZombie, IKnockbackable<GameObject>
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
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        var collider = gameObject.GetComponentsInChildren<BoxCollider2D>();
        foreach (var child in collider)
        {
            child.enabled = false;
        }

        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    public void TakeDamage(IWeapon weapon)
    {
        this.health -= weapon.GetDamage();
        if (this.health <= 0)
        {
            Kill();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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

    public float GetThreatLevel()
    {
        throw new System.NotImplementedException();
    }

    public float GetHealth()
    {
        return this.health;
    }

    public void TakeKnockBack(GameObject other, float power)
    {
        Vector2 direction = (transform.position - other.transform.position).normalized;
        Vector2 force = direction * power * 10;

        gameObject.GetComponent<Rigidbody2D>().AddForce(other.transform.position, ForceMode2D.Impulse);
        //gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public float GetEnemyKnockback()
    {
        return this.knockback;
    }
}
