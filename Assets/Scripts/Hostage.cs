using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Hostage : MonoBehaviour {
  [SerializeField] Transform healthbar;
  [SerializeField] private bool isWalking;
  [SerializeField] private float walkTime, waitTime, moveSpeed;
  [SerializeField] public int ID;
  [SerializeField] public GameObject[] pointers;
  [SerializeField] private int currentMove = 0;
  [SerializeField] private Animator _animator;
  
  [SerializeField] private bool ActiveReverse;
  [SerializeField] public float waitingTimeBeforeMove = 2f;

  private float walkCounter, waitCounter;
  private int walkDirection;
  private Rigidbody2D rb;
  public static List<Hostage> currentHostages = new List<Hostage>();
  private bool onBack = false;

  void Awake() {
    rb = this.GetComponent<Rigidbody2D>();
    //Hostage.currentHostages.Add(this);
  }

  public void Movement() {
    if (currentMove < pointers.Length) {
      NextPath();
    }
  }

  void NextPath() {
    StartCoroutine(LookAfterMoving());
  }

  IEnumerator LookAfterMoving() {
    if (currentMove == 0 || currentMove == pointers.Length-1) {
      _animator.SetFloat("Speed",0);
      yield return new WaitForSeconds(waitingTimeBeforeMove);
    }
    if (((currentMove+1) < pointers.Length) && !onBack) {
      currentMove++;
      iTween.MoveTo(gameObject,iTween.Hash("position",pointers[currentMove].transform.position,"oncomplete","LookAfterMoving","time",2.5f,"easetype",iTween.EaseType.linear));
      Vector2 direction = ((Vector2)pointers[currentMove].transform.position - (Vector2) transform.position).normalized;
      _animator.SetFloat("Horizontal",direction.x);
      _animator.SetFloat("Vertical",direction.y);
      _animator.SetFloat("Speed",transform.position.sqrMagnitude);
    } else {
      if (ActiveReverse && !onBack) {
        onBack = true;
      }
      if (currentMove >= 0 && onBack) {
        if (currentMove != 0) {
          currentMove--;
          iTween.MoveTo(gameObject,iTween.Hash("position",pointers[currentMove].transform.position,"oncomplete","LookAfterMoving","time",2f,"easetype",iTween.EaseType.easeInSine));
          Vector2 direction = ((Vector2)pointers[currentMove].transform.position - (Vector2) transform.position).normalized;
          _animator.SetFloat("Horizontal",direction.x);
          _animator.SetFloat("Vertical",direction.y);
          _animator.SetFloat("Speed",transform.position.sqrMagnitude);
        } else {
          onBack = false;
          NextPath();
        }
      } else {
        onBack = false;
      }
    }
  }

  void Update() {
    /*if (isWalking) {
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
    */
  }
  
  public void GetInjured() {
    healthbar.localScale -= new Vector3(0.05f, 0f, 0f);
    healthbar.localPosition -= new Vector3(0.01f, 0f, 0f);
    if (PhotonNetwork.IsMasterClient) {
      GetComponent<PhotonView>().RPC("Restart",RpcTarget.All);
    }

    if (healthbar.localScale.x <= 0f) {
      Die();
    }
  }

  [PunRPC]
  void Restart() {
    FindObjectOfType<NetworkManager>().RestartMap();
  }
  
  void Die() {
    if (PhotonNetwork.IsMasterClient) {
      Debug.Log("Master Destroy NPC");
      PhotonNetwork.Destroy(gameObject);
    }
  }

  void ChooseDirection() {
    walkDirection = Random.Range(0, 4);
    isWalking = true;
  }
}
