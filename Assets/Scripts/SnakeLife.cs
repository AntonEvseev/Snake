using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeLife : MonoBehaviour {

    public int scoreSnake = 0;
    public int speed = 10;
    public int buff = 0;
    float speedMove = 3;
    public Vector2 directionHod;
    public GameObject snakeBody;
    public GameObject food;
    List<GameObject> BodySnake = new List<GameObject>();
    

    public void AddChank()
    {
        Vector3 position = transform.position;
        if (BodySnake.Count > 0)
        {
            position = BodySnake[BodySnake.Count - 1].transform.position;
        }
        position.y++;
        GameObject Body = Instantiate(snakeBody, position, Quaternion.identity) as GameObject;
        BodySnake.Add(Body);
    }

    void SnakeStap()
    {
        if ((directionHod.x != 0) || (directionHod.y != 0))
        {
            Rigidbody ComponentRig = GetComponent<Rigidbody>();
            ComponentRig.velocity = new Vector3(directionHod.x * speedMove, directionHod.y * speedMove, 0);
        }
        if (BodySnake.Count > 0)
        {
            BodySnake[0].transform.position = transform.position;
            for(int bodyIndex = BodySnake.Count-1; bodyIndex > 0; bodyIndex--)
            {
                BodySnake[bodyIndex].transform.position = BodySnake[bodyIndex - 1].transform.position;
            }
        }
    }

    public void SnakeDestroy()
    {
        directionHod = new Vector2(0, 0);
        foreach (GameObject o in BodySnake)
        {
            DestroyObject(o.gameObject);
        }        
        DestroyObject(this.gameObject);
        Application.LoadLevel("GameOver");
    }

    // Use this for initialization
    void Start () {
        BodySnake.Clear();
        for (int i = 0; i < 3; i++)
        {
            AddChank();
        }       
    }
	
	// Update is called once per frame
	void Update () {
        buff++;
        if (buff > speed)
        {
            SnakeStap();
            buff = 0;
        }
    }
}
