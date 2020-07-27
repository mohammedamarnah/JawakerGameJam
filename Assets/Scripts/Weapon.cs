using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public Transform firePoint;
    public Bullet bullet;

    public float shootingSpeed;
    // Update is called once per frame
    public void Fire() {
        GameObject projectile = Instantiate(bullet.gameObject, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * shootingSpeed, ForceMode2D.Impulse);
    }
}
