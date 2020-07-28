using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            Destroy(gameObject);
        } else 
        if (other.gameObject.tag == "Hostage") {
            other.GetComponent<Hostage>().GetInjured();
        } else if (other.gameObject.tag == "Items") {
            Destroy(gameObject);
            ItemController it = other.gameObject.GetComponent<ItemController>();
            it.GetInjured();
        }
    }
}
