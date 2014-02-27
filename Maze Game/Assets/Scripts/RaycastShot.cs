using UnityEngine;
using System.Collections;

/**
 * A class that links clicking the fire button to actually firing bullets.
 */
public class RaycastShot : MonoBehaviour {
	// The damage caused by the bullet.
    public float damage;

	// The origin of the bullet.
    public Transform origin;

	// The bullet firing effect.
	public ParticleEmitter bullets;

	// The direction of the bullet.
    private Vector3 dir;

	// Use this for initialization
	void Start () {
	
	}
	
	/**
	 * If the gun is fired, call the shoot method.
	 */
	void Update () {
        dir = origin.forward;
        if (Input.GetButton("Fire1"))
            Shoot();
	}

	/**
	 * Generates the gun firing effect and emits bullets
	 * to be fired towards objects.
	 */
    void Shoot()
    {
        RaycastHit hit;
		bullets.Emit(3);
        if (Physics.Raycast(origin.position, dir, out hit))
        {
            if (hit.collider.gameObject.tag == "Monster")
            {
                hit.collider.gameObject.SendMessage("ApplyDamage", damage);
            }
        }
    }
}
