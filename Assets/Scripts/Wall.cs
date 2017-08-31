using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter (Collision collision)
    {        
        SnakeLife s = collision.gameObject.GetComponent<SnakeLife>();
        if (s != null)
        {
            s.SnakeDestroy();            
        }      
    }
}
