using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour

{
    void OnGUI()
    {
        if (GUI.Button(new Rect(new Vector2(200, 300), new Vector2(200, 30)), "Restart"))
        {
            Application.LoadLevel("GameSnake");
        }
    }
     

// Use this for initialization
void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
