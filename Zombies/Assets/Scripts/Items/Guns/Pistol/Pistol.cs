using Assets.Scripts.Interfaces;
using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon, IPickup<GameObject>
{
    public float damage = 100.0f; //10
    public float knockBack = 3000.0f;

    public int GetAmmo()
    {
        throw new System.NotImplementedException();
    }

    public float GetDamage()
    {
        return this.damage;
    }

    public float GetKnockback()
    {
        return this.knockBack;
    }

    public void GetItem(GameObject item)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
