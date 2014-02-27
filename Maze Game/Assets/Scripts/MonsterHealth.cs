using UnityEngine;
using System.Collections;

/**
 * A script that controls the monster's health.
 */
public class MonsterHealth : MonoBehaviour {

	// Health of the monster
    public float health;

	/**
	 * Monster's health increases with each level
	 */
	void Start () {
		health = health * End.GetLevel();
	}
	
	/**
	 * If the monster is dead, destroy the object
	 */
	void Update () {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
	}

	/**
	 * When a bullet collides with the monster, decrease its health
	 * @param thisCollision The collision that's occuring with the object.
	 */
    void OnCollisionEnter(Collision thisCollision)
    {
        if (thisCollision.gameObject.GetComponent("ShotController") != null)
        {
            ShotController t = (ShotController)thisCollision.gameObject.GetComponent("ShotController");
            health -= t.damage;
        }
    }

	/**
	 * Subtracts damage from the monster's health.
	 * @param damage The amount of damage to subtract.
	 */
    void ApplyDamage(float damage)
    {
        health -= damage;
    }

}