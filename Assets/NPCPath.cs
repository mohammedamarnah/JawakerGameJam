using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPath : MonoBehaviour {
   [SerializeField] public GameObject[] Paths;
   [SerializeField] public GameObject NPC;
   [SerializeField] public float waitingTimeBeforeMove;
}
