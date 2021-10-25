using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    // Start is called before the first frame update

    public float Speed = 150;
    public float TimetoLive = 2;
    public float Damage = 10;

    private float TimetoLiveLeft;

    public enum Shooter
    {
        Player, 
        Opponent
    }
    public Shooter MyShooter;
    public GameObject HitEffect;

    void Start()
    {
        TimetoLiveLeft = TimetoLive;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.transform.forward * Speed * Time.deltaTime;

        TimetoLiveLeft -= Time.deltaTime;

        if(TimetoLiveLeft <0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(MyShooter == Shooter.Player)
        {
            var shipEnemy = collision.gameObject.GetComponentInParent<ShipEnemy>() ;// var czyli "domysl sie jaki to jest typ"
            if(shipEnemy!=null)
            {
                Instantiate(HitEffect, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }

        if (MyShooter == Shooter.Opponent)
        {
            var shipPlayer = collision.gameObject.GetComponentInParent<ShipPlayer>();// var czyli "domysl sie jaki to jest typ"
            if (shipPlayer != null)
            {
                shipPlayer.DoDamage(Damage);
                Instantiate(HitEffect, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }

}
