using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEnemy : MonoBehaviour
{
    public enum State
    {
        Charge, //szaraza
        Fleeing //ucieczka
    }

    public float Speed = 50;
    public float TurnSpeed = 200;
    public float DistanceToFlee = 65;
    public float DistanceToCharge = 125;

    private State CurrentState;
    private ShipPlayer ShipPlayer;
    private float azimuth;


    public Blaster Weapon;
    public float FireEvery = 1;
    private float TimeToFire;


    // Start is called before the first frame update
    void Start()
    {
        ShipPlayer = FindObjectOfType<ShipPlayer>();
        CurrentState = State.Charge;
        azimuth = 0;
        TimeToFire = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.transform.forward * Speed * Time.deltaTime;

        var distanceToPlayer = ShipPlayer.transform.position - this.transform.position;

        if(CurrentState==State.Charge)
        {
            var anglee = Mathf.Rad2Deg* Mathf.Atan2(distanceToPlayer.x,distanceToPlayer.z);
            azimuth = Mathf.MoveTowardsAngle(azimuth, anglee,TurnSpeed*Time.deltaTime);
        }
        if (CurrentState == State.Fleeing)
        {
            var anglee = Mathf.Rad2Deg * Mathf.Atan2(distanceToPlayer.x, distanceToPlayer.z);
            anglee += 180;
            azimuth = Mathf.MoveTowardsAngle(azimuth, anglee, TurnSpeed * Time.deltaTime);

            TimeToFire -= Time.deltaTime;
            if(TimeToFire<0)
            {
                TimeToFire = FireEvery;
                Instantiate(Weapon, this.transform.position, this.transform.rotation);
            }

        }
        if(distanceToPlayer.magnitude< DistanceToFlee)
        {
            CurrentState = State.Fleeing;
        }
        if (distanceToPlayer.magnitude > DistanceToCharge)
        {
            CurrentState = State.Charge;
        }


        this.transform.rotation = Quaternion.Euler(0, azimuth, 0);
    }
}
