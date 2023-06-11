using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    //the basic box, which can take damage.
    public int health = 100;
    public GameObject deathEffect;
    [SerializeField] GameObject lootDrop;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0) Die();
    }//F

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        if (lootDrop != null)
        {
            Debug.Log("Dropping Loot!");
            Instantiate(lootDrop, transform.position, Quaternion.identity); //I dunno, will it work?
            //if a new item was created, we should reset the sensors
            //need a gamemanager
            Camera.main.GetComponent<SimpleCamera>().CheckSensors();
        }
        Destroy(gameObject);
    }//F

}//class
