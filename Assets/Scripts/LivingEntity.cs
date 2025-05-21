using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    public float startingHealth;
    private float health;
    protected bool dead;

    protected virtual void Start(){
        health = startingHealth;
        dead = false;
    }

    public virtual void TakeHit(float damage){
        if(!dead){
            health-=damage;
            if(health<=0f){
                dead=true;
            }
        }
    }

    public float vida(){
        return health;
    }
    public bool muerto(){
        return dead;
    }
}