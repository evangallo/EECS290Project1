using UnityEngine;
using System.Collections;

public class ShotController : MonoBehaviour {

    public Vector3 velocity, position;
    public float damage;
    public Transform prefab;
	// Use this for initialization
    void Start()
    {
        prefab.rigidbody.MovePosition(position);
        prefab.rigidbody.AddForce(velocity, ForceMode.VelocityChange);
	}
    public ShotController(Vector3 vel, Vector3 pos, float dam, Transform pref)
    {
        velocity = vel;
        position = pos;
        damage = dam;
        prefab = pref;
    }
	
	// Update is called once per frame
	void Update () {

	}
    //Removes the bullet, ends the script, and returns the damage
    //Current configuration assumes that monsters and walls will handle detecting an impact
    float OnHit()
    {
        enabled = false;
        return damage;
    }
}
