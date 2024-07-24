using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public Transform spawPoint;
    public GameObject bullet;
    public float shotForce = 1500f;
    public float shoRate = 0.5f; //Tiempo que demora en disparar

    private float shotRateTime = 0;
    public AudioClip shootSound;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time > shotRateTime && GameManager.Instance.gunAmmo > 0)
            {
                AudioManager.Instance.PlaySFX(shootSound);
                GameManager.Instance.gunAmmo--;
                GameObject newBullet;

                newBullet = Instantiate(bullet, spawPoint.position, spawPoint.rotation);
                newBullet.GetComponent<Rigidbody>().AddForce(spawPoint.forward * shotForce);

                shotRateTime = Time.time + shoRate;

                Destroy(newBullet, 5);
            }
        }
    }
}
