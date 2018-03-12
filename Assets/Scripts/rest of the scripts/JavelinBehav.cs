using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Magic.Pooling;

public class JavelinBehav : MonoBehaviour {

    public float rotationSpeed;
    public float speed;
    public float damage;

	void Update ()
    {
        if (transform.rotation.eulerAngles.x < 90 || transform.rotation.eulerAngles.x > 180)
            transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(0.01f, 0.01f, 0.65f), transform.rotation);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Enemy")
            {
                colliders[i].GetComponent<EnemyBehav>().Hit(damage);
                PoolManager.DeSpawn(gameObject, "javelins");
            }
        }

        if (transform.position.y <= -2.0f)
            PoolManager.DeSpawn(gameObject, "javelins");
	}
}
