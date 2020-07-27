using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostage : MonoBehaviour {
  [SerializeField] Transform healthbar;
  [SerializeField] private bool isWalking;
  [SerializeField] private float walkTime, waitTime, moveSpeed;
  [SerializeField] public int ID;

  private float walkCounter, waitCounter;
  private int walkDirection;
  private Rigidbody2D rb;
  public static List<Hostage> currentHostages = new List<Hostage>();

  void Awake() {
    rb = this.GetComponent<Rigidbody2D>();
    Hostage.currentHostages.Add(this);
    ChooseDirection();
  }

  void Update() {
    if (isWalking) {
      walkCounter -= Time.deltaTime;
      if (walkCounter < 0) {
        isWalking = false;
        waitCounter = waitTime;
      }

      switch (walkDirection) {
        case 0:
          rb.velocity = new Vector2(0, moveSpeed);
          break;
        case 1:
          rb.velocity = new Vector2(0, -moveSpeed);
          break;
        case 2:
          rb.velocity = new Vector2(moveSpeed, 0);
          break;
        case 3:
          rb.velocity = new Vector2(-moveSpeed, 0);
          break;
      }
    }
    else {
      waitCounter -= Time.deltaTime;
      if (waitCounter < 0) {
        isWalking = true;
        ChooseDirection();
      }
    }
  }
  
  public void GetInjured() {
    Debug.Log(healthbar.localScale);
    Debug.Log(healthbar.localPosition);
    healthbar.localScale -= new Vector3(0.05f, 0f, 0f);
    healthbar.localPosition -= new Vector3(0.01f, 0f, 0f);
    if (healthbar.localScale.x <= 0f) {
      Die();
    }
  }

  void Die() {
    Destroy(this.gameObject);
  }

  void ChooseDirection() {
    walkDirection = Random.Range(0, 4);
    isWalking = true;
  }
}
