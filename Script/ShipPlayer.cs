using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPlayer : MonoBehaviour
{
    public float Acceleration=25;
    public float Deacceleration=20;
    public float TurnRate=60;

    public float MaxSpeed =50;

    public float HpMax =100;
    public float Regen =10;
    private float hpCurrent;
    public float HpLeft => hpCurrent/HpMax;

    public Blaster FieldBlaster;

    public Transform FieldHardpointLeftWing;
    public Transform FieldHardpointRightWing;
    public Transform FieldHardpointRighFront;
    public Transform FieldHardpointLeftFront;

    private float azimuth;
    private float speed;
  

    public void DoDamage(float dmg)
    {
        hpCurrent -= dmg;
        if(hpCurrent <=0)
        {
            hpCurrent = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        azimuth = 0;
        speed = 0;
        hpCurrent = HpMax;
    }

    // Update is called once per frame
    void Update()
    {
        hpCurrent += Regen * Time.deltaTime;
        if(hpCurrent>HpMax)
        {
            hpCurrent = HpMax;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Instantiate(FieldBlaster, FieldHardpointLeftWing.position, this.FieldHardpointLeftWing.rotation);
            Instantiate(FieldBlaster, FieldHardpointRightWing.position, this.FieldHardpointRightWing.rotation);
            Instantiate(FieldBlaster, FieldHardpointRighFront.position, this.FieldHardpointRighFront.rotation);
            Instantiate(FieldBlaster, FieldHardpointLeftFront.position, this.FieldHardpointLeftFront.rotation);
        }

        if (Input.GetKey(KeyCode.W))
        {
           // this.transform.position += this.transform.forward * this.Acceleration* Time.deltaTime;
           speed += Acceleration * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            // this.transform.position -= this.transform.forward * this.Deacceleration * Time.deltaTime;
            speed -= Acceleration * Time.deltaTime;
        }

        if (speed > MaxSpeed)
            speed = MaxSpeed;

        if (speed < -MaxSpeed)
            speed = -MaxSpeed;

        this.transform.position += this.transform.forward * this.speed * Time.deltaTime;

        float speedRatio = speed / MaxSpeed;

        if (Input.GetKey(KeyCode.A))
        {
            this.azimuth -= Mathf.LerpUnclamped(0.0f,this.TurnRate,speedRatio) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.azimuth += Mathf.LerpUnclamped(0.0f, this.TurnRate, speedRatio) * Time.deltaTime;
        }
        this.transform.rotation = Quaternion.Euler(0, this.azimuth, 0);
    }
}
