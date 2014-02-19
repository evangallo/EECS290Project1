﻿using UnityEngine;
using System.Collections;

public class RaycastShot : MonoBehaviour {
    public float damage;
    public Transform origin;
	public ParticleEmitter bullets;
    private Vector3 dir;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        dir = origin.forward;
        if (Input.GetButton("Fire1"))
            Shoot();
	}
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
