﻿using UnityEngine;
using System.Collections;

public class TempShot : MonoBehaviour
{


    public Transform Bullet, Spawn;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shot();
        }
    }

    void Shot()
    {
        Transform pel = (Transform)Instantiate(Bullet, Spawn.position, Spawn.rotation);
        pel.rigidbody.AddForce(transform.forward * 8000);
    }
}
