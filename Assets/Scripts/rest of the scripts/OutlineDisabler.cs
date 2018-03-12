using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineDisabler : MonoBehaviour {

    public float distance;
    public cakeslice.Outline[] outline;
    private float dist;
    private Resource resource;

	void Start () {
        distance = 0.75f;
        resource = gameObject.GetComponent<Resource>();
	}
	
	void Update () {
        dist = Vector3.Distance(GlobalRef.playerPos.position, transform.position);
        if (dist < distance) {
            resource.canGather = true;
            for(int i = 0; i < outline.Length; i++)
                outline[i].enabled = true;
        }
        else {
            resource.canGather = false;
            for (int i = 0; i < outline.Length; i++)
                outline[i].enabled = false;
        }
	}
}