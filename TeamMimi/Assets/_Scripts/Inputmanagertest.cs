using UnityEngine;
using System.Collections;

public class Inputmanagertest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetAxis("P1xAxis") > .2 )
        {
            transform.Translate(new Vector3(1, 0, 0));
        }
	}
}
