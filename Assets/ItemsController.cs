using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsController : MonoBehaviour {
    public static List<ItemController> items = new List<ItemController>();

    void Awake() {
        items = GameObject.FindObjectsOfType<ItemController>().ToList();
    }


    void Update() {
        // Transform position = items.Last();
        //if (items.Count == 0) {
          //  Instantiate()
        //}
    }
}
