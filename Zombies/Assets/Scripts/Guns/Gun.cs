using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    public float damage = 10.0f;
    public float knockBack = 10.0f;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
