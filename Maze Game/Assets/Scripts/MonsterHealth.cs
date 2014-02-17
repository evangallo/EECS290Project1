using UnityEngine;
using System.Collections;

public class MonsterHealth : MonoBehaviour {
    public float health;
	// Use this for initialization
	void Start () {
	
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
            health -= 10f;
        }
    }
}
