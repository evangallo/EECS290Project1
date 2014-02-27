using UnityEngine;
using System.Collections;

/**
 * A class to control shots fired from the gun.
 */
public class ShotController : MonoBehaviour {

	// How long the bullet remains active.
    public float count;

	// The velocity and position of the bullet.
    public Vector3 velocity, position;

	// The damage carried by the bullet.
    public float damage;

	// The design of the object.
    public Transform prefab;

	//Has the bullet hit yet?
    bool hasHit;

	/**
	 * Sets the maximum length of time for a bullet to be active
	 */
    void Start()
    {
        count = 3.0f;
        hasHit = false;
        //prefab.rigidbody.MovePosition(position);
        //prefab.rigidbody.AddForce(velocity, ForceMode.VelocityChange);
	}

	/**
	 * Creates the ShotController.
	 * @param vel The velocity of the bullet.
	 * @param pos The position of the bullet.
	 * @param dam The damage of the bullet.
	 * @param pref The design of the bullet.
	 */
    public ShotController(Vector3 vel, Vector3 pos, float dam, Transform pref)
    {
        velocity = vel;
        position = pos;
        damage = dam;
        prefab = pref;
    }
	
	/**
	 * Destroys the bullet if it has been active too long
	 * or has collided with another object.
	 */
	void Update () {
        count -= Time.deltaTime;
        if (count <= 0f || hasHit)
            Destroy(gameObject);
	}

	/**
	 * Sets hasHit when the bullet hits an object.
	 * @param theCollision The collision caused by the bullet.
	 */
    void OnCollisionEnter(Collision theCollision)
    {
        hasHit = true;
    }

	/**
	 * Sets hasHit when the bullet hits a monster.
	 * @return The damage to be inflicted upon the monster.
	 */
    public float HitMonster()
    {
        hasHit = true;
        return damage;
    }
}
