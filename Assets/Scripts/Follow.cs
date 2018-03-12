using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public bool followPosition;
    public bool followRotation;
    public Transform target;
    public Vector3 offset;
    
	void Update () {
        if(followPosition)
            transform.position = target.position + offset;
        if (followRotation)
            transform.rotation = target.rotation;
	}
}
