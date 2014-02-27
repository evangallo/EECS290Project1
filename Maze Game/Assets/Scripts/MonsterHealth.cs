using UnityEngine;
using System.Collections;

public class MonsterHealth : MonoBehaviour {

	// Health of the monster
    public float health;

	// Use this for initialization
	void Start () {
		health = health * End.GetLevel();
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
	}
    void OnCollisionEnter(Collision thisCollision)
    {
        if (thisCollision.gameObject.GetComponent("ShotController") != null)
        {
            ShotController t = (ShotController)thisCollision.gameObject.GetComponent("ShotController");
            health -= t.damage;
        }
    }
    void ApplyDamage(float damage)
    {
        health -= damage;
    }

}
