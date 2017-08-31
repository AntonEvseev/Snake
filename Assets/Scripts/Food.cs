using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    int timeDeth = 500;
    int buff;
    // Use this for initialization
    void Start () {
        float XX = Random.Range(-12, 12);
        float YY = Random.Range(-12, 12);

        this.transform.position = new Vector3(XX, YY, 0);
    }
	
	// Update is called once per frame
	void Update () {
        buff++;
        if (buff > timeDeth)
        {
            DestroyObject(this.gameObject);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        SnakeLife s = collision.gameObject.GetComponent<SnakeLife>();
        if (s != null)
        {
            s.AddChank();
            s.scoreSnake++;
            DestroyObject(this.gameObject);            
        }
    }
}
