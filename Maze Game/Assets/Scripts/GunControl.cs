using UnityEngine;
using System.Collections.Generic;

public class GunControl : MonoBehaviour {
    public Transform gun, prefab;
    public int weapon;
    private Queue<ShotController> q;
	// Use this for initialization
	void Start () {
	    q = new Queue<ShotController>();
	}
	
	// Update is called once per frame
	void Update () {
        Transform shot = (Transform)Instantiate(prefab, gun.position, Quaternion.identity);
        shot.rigidbody.AddForce(0f,0f,1f);
        
	}
}
