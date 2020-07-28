using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemController : MonoBehaviour {
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private SpriteRenderer whitebg;
    [NonSerialized] public float health = 100.0f;

    private float hitFactor = 2.5f;

    public void GetInjured() {
      health -= hitFactor;
      if (health <= 0) {
        Explode();
      }
      
    }

    public void Explode() {
      explosion.transform.position = transform.position;
      explosion.gameObject.SetActive(true);
      explosion.Play();
      Destroy(this.gameObject);
    }
}
