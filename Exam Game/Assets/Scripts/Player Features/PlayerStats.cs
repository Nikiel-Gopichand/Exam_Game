using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{//basic player stats
    public float HP = 100;
    public  float stamina =100;
    //Melee abilities
    public float swordDamage=40;
    
    //Fire abilities
    public float baseBurnDamage =25f;
    public float burnDamageMultiplier = 0.1f;//Deals extra damage as 10% of base damage over burntime(listed below)
    public float burnTime = 3f; //burn damage time in seconds
    public float fireBallRange = 5f;//range of attack

    //Freeze abilities
    public float baseFreezeDamage = 25f;
    public float freezeSlowMultiplier = 0.7f;//reduces enemy speed to make them slow
    public float freezeTime = 2f;//time above effect lasts
    public float IceShardRange = 5f;//range of attack

    //wind abilities
    public float DamageMultiplier = 1.1f;

    public float windSlashRange = 5f;//range of attack
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
