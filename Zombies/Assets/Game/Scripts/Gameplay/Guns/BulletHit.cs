namespace Game.Scripts.Gameplay.Guns
{
    public class BulletHit
    {
        // var playerLayer = 9;
        // int layerMask = ~(1 << playerLayer); //Exclude layer 9
        // RaycastHit2D hit = Physics2D.Raycast(bulletSpawner.transform.position, -transform.up, Mathf.Infinity,
        //         layerMask);
        //     if (hit.collider != null)
        // {
        //     Debug.DrawRay(bulletSpawner.transform.position, -transform.up, Color.red, 10);
        //
        //     var damageable = hit.collider.gameObject.GetComponentInParent<IDamageable>();
        //     if (damageable != null)
        //     {
        //         
        //
        //         // Must be damageable
        //         var weapon =
        //             bullet.GetComponent<IWeapon>(); // Change this once I add another gun to use a weapon system
        //         damageable.TakeDamage(weapon);
        //
        //         var knockBack = hit.collider.gameObject.GetComponent<IKnockbackable<GameObject>>();
        //         if (knockBack != null)
        //         {
        //             knockBack.TakeKnockBack(hit.collider.gameObject, weapon.GetKnockback());
        //         }
        //     }
        // }
    }
}