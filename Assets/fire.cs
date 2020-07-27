using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class fire : MonoBehaviour {
    [SerializeField] GameObject Laser;

    [SerializeField] private Transform spawner;
// Start is called before the first frame update
    void Awake() {
     
    }

    // Update is called once per frame
    void Update() {
       // Laser.transform.rotation = GetCurrentMousePosition().GetValueOrDefault();
    }
  
    
    private Vector3? GetCurrentMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(Vector3.forward, Vector3.zero);
 
        float rayDistance;
        if (plane.Raycast(ray, out rayDistance))
        {
            return ray.GetPoint(rayDistance);
             
        }
 
        return null;
    }
    
}
