using UnityEngine;
using System.Collections;

public class ShotController : MonoBehaviour {

    public float count;
    public Vector3 velocity, position;
    public float damage;
    public Transform prefab;
    bool hasHit;
	// Use this for initialization
    void Start()
    {
        count = 3.0f;
        hasHit = false;
        //prefab.rigidbody.MovePosition(position);
        //prefab.rigidbody.AddForce(velocity, ForceMode.VelocityChange);
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
        count -= Time.deltaTime;
        if (count <= 0f || hasHit)
            Destroy(gameObject);
	}
    void OnCollisionEnter(Collision theCollision)
    {
        hasHit = true;
    }
    public float HitMonster()
    {
        hasHit = true;
        return damage;
    }
}
