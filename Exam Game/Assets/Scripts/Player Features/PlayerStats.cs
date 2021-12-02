using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{//basic player stats
    public float HP = 100;
    public float stamina = 100;
    //Melee abilities
    public float swordDamage = 40;

    //Fire abilities
    public float baseBurnDamage = 25f;
    public float burnDamageMultiplier = 0.1f;//Deals extra damage as 10% of base damage over burntime(listed below)
    public float burnTime = 3f; //burn damage time in seconds
    public float fireBallRange = 5f;//range of attack

    //Freeze abilities
    public float baseFreezeDamage = 25f;
    public float freezeSlowMultiplier = 0.7f;//reduces enemy speed to make them slow
    public float freezeTime = 2f;//time above effect lasts
    public float IceShardRange = 5f;//range of attack

    public float DamageMultiplier = 1.1f;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("swordDamage")  )
        {
            if (PlayerPrefs.GetFloat("swordDamage") != 0f)
            {

                swordDamage = PlayerPrefs.GetFloat("swordDamage");
            }
      
        }
        else {
            PlayerPrefs.SetFloat("swordDamage", swordDamage);
                
                }



        if (PlayerPrefs.HasKey("baseBurnDamage"))
        {
            if (PlayerPrefs.GetFloat("baseBurnDamage") != 0f)
            {

                baseBurnDamage = PlayerPrefs.GetFloat("baseBurnDamage");
            }

        }
        else
        {
            PlayerPrefs.SetFloat("baseBurnDamage", baseBurnDamage);

        }

        if (PlayerPrefs.HasKey("baseFreezeDamage"))
        {
            if (PlayerPrefs.GetFloat("baseFreezeDamage") != 0f)
            {

                baseFreezeDamage = PlayerPrefs.GetFloat("baseFreezeDamage");
            }

        }
        else
        {
            PlayerPrefs.SetFloat("baseFreezeDamage", baseFreezeDamage);

        }





    }

    // Update is called once per frame
    void Update()
    {

    }
   
}
