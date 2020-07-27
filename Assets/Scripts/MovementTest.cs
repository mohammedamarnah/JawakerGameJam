using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementTest : MonoBehaviour {
    public float moveSpeed = 0.5f;
    public Camera mainCamera;
    public Rigidbody2D rb;
    public Weapon weapon;
    [SerializeField] private PhotonView _photonView;

    private Vector2 moveDirection, mousePosition;

    void Awake() {
      mainCamera = Camera.main;
    }
    void Update() {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        
        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
    
    void FixedUpdate() {
      if (_photonView.IsMine) {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
      }
    }
}
