using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour, IWeapon
{
    public float Damage = 50;

    public int GetAmmo()
    {
        throw new System.NotImplementedException();
    }

    public float GetDamage()
    {
        return this.Damage;
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