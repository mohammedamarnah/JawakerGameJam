using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemController : MonoBehaviour {
    [SerializeField] private ParticleSystem explosion;
    [NonSerialized] public float health = 100.0f;

    private float hitFactor = 1.0f;

    public void GetInjured() {
      health -= hitFactor;
    }

    public void Explode() {
      explosion.transform.position = transform.position;
      explosion.Play();
    }
}
