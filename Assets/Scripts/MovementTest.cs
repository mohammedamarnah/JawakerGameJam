using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour {
    public float moveSpeed = 0.5f;
    public Camera mainCamera;
    public Rigidbody2D rb;
    public Weapon weapon;
    public int ID = 0;

    private Vector2 moveDirection, mousePosition;
    

    void Update() {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
    
    void FixedUpdate() {
       rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
       Vector2 aimDirection = mousePosition - rb.position;
       float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
       rb.rotation = aimAngle;
       if (weapon != null) {
         weapon.Fire();
       }
    }
}
