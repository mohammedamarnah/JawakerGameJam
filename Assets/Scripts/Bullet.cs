using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public Rigidbody2D rb;
    public GameObject impactEffect;

    void Awake() {
        Destroy(this,5f);
    }
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Wall") {
            // Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
