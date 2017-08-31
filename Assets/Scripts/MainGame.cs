using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour {
    
    public int gameMode = 0;    
    float XX = 0, YY = 0;
    int buff = 0;
    int timeSpeed = 500;
    int firstAddFood = 0;
    public int lvlTest = 0;
    public GameObject food;
    public GameObject wall;
    public GameObject snake;
    GameObject snakeObj;
    public GameObject LoadLev;  
    List<GameObject> ListLevel = new List<GameObject>();
    TextAsset[] levels;

    public int level = 0;
    public int maxScoreLevel1;
    public int maxScoreLevel2;
    public int maxScoreLevel3;
    public int maxScoreLevel4;
    public int maxScoreLevel5;
    GUIStyle myButtonStyle;

    public void AddFood()
    {
        Instantiate(food);
    }   
    
    public void AddWall()
    {
        Instantiate(wall);
    }

    void CreateSnake()
    {
        snakeObj = Instantiate(snake) as GameObject;
        snakeObj.name = "Snake";
        gameMode = 1;
    }

    public void ChooseLevel()
    {
        gameMode = 2;
    }

    void Popup()
    {
        gameMode = 3;
    }

    void ExitGame()
    {
        gameMode = 4;
    }

    void DeleteScore()
    {
        PlayerPrefs.DeleteAll();
        Application.LoadLevel("GameSnake");
    }

    void Start () {
        levels = Resources.LoadAll<TextAsset>("StreamingAssets");
        maxScoreLevel1 = PlayerPrefs.GetInt("savescore");
        maxScoreLevel2 = PlayerPrefs.GetInt("savescore2");
        maxScoreLevel3 = PlayerPrefs.GetInt("savescore3");
        maxScoreLevel4 = PlayerPrefs.GetInt("savescore4");
        maxScoreLevel5 = PlayerPrefs.GetInt("savescore5");
    }
	
	// Update is called once per frame
	void Update () {
		if (snakeObj != null)
        {
            XX = 0;
            YY = 0;
            if(Input.GetAxis("Horizontal") > 0)
            {
                XX = 1;
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                XX = -1;
            }
            if (Input.GetAxis("Vertical") > 0)
            {
                YY = 1;
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                YY = -1;
            }
            if ((XX != 0) || (YY != 0))
            {
                SnakeLife s = snakeObj.GetComponent<SnakeLife>();
                if (XX != 0)
                {
                    s.directionHod = new Vector2(XX, 0);
                }
                if (YY != 0)
                {
                    s.directionHod = new Vector2(0, YY);
                }                
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Time.timeScale == 1)
                {
                    Time.timeScale = 0;
                    ExitGame();
                }
                else
                {
                    Time.timeScale = 1;
                }
            }
        }
        if(lvlTest == 1)
        {
            gameMode = 1;
            ChooseLevel();
        }
        if (gameMode == 1)
        {
             if (firstAddFood == 0)
            {
                AddFood();
                firstAddFood = 1;
            }
            buff++;
            if (buff > timeSpeed)
            {
                AddFood();
                buff = 0;
            }            
        }
    }

    void LoadLevel(int level)
    {
        string levl = levels[level - 1].text;       
        GameObject go = null;
        int ix = 0;
        int iy = 0;
        int xWallPosition = -13;
        double yWallPosition = 12;
        foreach (char c in levl)
        {
            Vector3 position = new Vector3(xWallPosition, (float)yWallPosition, 0);
            switch (c)
            {
                case '.':
                    break;
                case 'w':                    
                    go = Instantiate(wall, position, Quaternion.identity) as GameObject;
                    ListLevel.Add(go);
                    break;
                default:
                    ix--;
                    yWallPosition = yWallPosition - 0.4;
                    xWallPosition = -14;
                    break;
            }
            xWallPosition++;            
            if(ix == 24)
            {
                ix = 0;
                iy++;               
            }
        }
    }

    void OnGUI()
    {
        if (gameMode == 3)
        {
            GUI.color = Color.black;
            GUI.contentColor = Color.blue;
            GUI.backgroundColor = Color.blue;
            
            GUI.Label(new Rect(200, 250, 180, 30), "Delete score?");
            if (GUI.Button(new Rect(new Vector2(200, 300), new Vector2(200, 30)), "Yes"))
            {
                DeleteScore();
            }
            if (GUI.Button(new Rect(new Vector2(200, 340), new Vector2(200, 30)), "No"))
            {
                Application.LoadLevel("GameSnake");
            }
        }
        if (gameMode == 4)
        {
            GUI.color = Color.black;
            GUI.contentColor = Color.green;
            GUI.backgroundColor = Color.green;
            GUI.Label(new Rect(200, 250, 180, 30), "Exit? You score not save!");
            if (GUI.Button(new Rect(new Vector2(200, 300), new Vector2(200, 30)), "Yes"))
            {
                Application.LoadLevel("GameSnake");
                gameMode = 0;
                Time.timeScale = 0;
            }
            if (GUI.Button(new Rect(new Vector2(200, 340), new Vector2(200, 30)), "No"))
            {
                Time.timeScale = 0;
                gameMode = 1;
            }
        }
        int posaX = Screen.height / 2;
        int posaY = Screen.width / 2;
        switch (gameMode)
        {
            case 0:
                if (GUI.Button(new Rect(new Vector2(posaX - 100, posaY - 100), new Vector2(200, 30)), "Play"))
                {
                    ChooseLevel();
                }
                if (GUI.Button(new Rect(new Vector2(posaX - 100, posaY - 60), new Vector2(200, 30)), "Reset"))
                {
                    Popup();
                }
                break;
            case 1:
                SnakeLife s = snakeObj.GetComponent<SnakeLife>();
                int Score = 0;
                if (s != null)
                {
                    Score = s.scoreSnake;
                    if(level == 1)
                    {
                    if (Score> maxScoreLevel1)
                        {
                            PlayerPrefs.SetInt("savescore", Score);
                            PlayerPrefs.Save();
                        }
                    }
                    if (level == 2)
                    {
                        if (Score > maxScoreLevel2)
                        {
                            PlayerPrefs.SetInt("savescore2", Score);
                            PlayerPrefs.Save();
                        }
                    }
                    if (level == 3)
                    {
                        if (Score > maxScoreLevel3)
                        {
                            PlayerPrefs.SetInt("savescore3", Score);
                            PlayerPrefs.Save();
                        }
                    }
                    if (level == 4)
                    {
                        if (Score > maxScoreLevel4)
                        {
                            PlayerPrefs.SetInt("savescore4", Score);
                            PlayerPrefs.Save();
                        }
                    }
                    if (level == 5)
                    {
                        if (Score > maxScoreLevel5)
                        {
                            PlayerPrefs.SetInt("savescore5", Score);
                            PlayerPrefs.Save();
                        }
                    }
                }                
                GUI.contentColor = Color.black;
                GUI.Label(new Rect(new Vector2(posaX - 100, 0), new Vector2(200, 30)), "Score:" + Score);
                break;
            case 2:
                if (maxScoreLevel1 > 0)
                {
                    GUI.color = Color.black;
                    GUI.contentColor = Color.green;
                    GUI.backgroundColor = Color.green;
                    if (GUI.Button(new Rect(new Vector2(posaX - 240, posaY - 270), new Vector2(90, 75)), "1" + "\n" + maxScoreLevel1))
                    {
                        level = 1;
                        LoadLevel(level);
                        CreateSnake();
                    }
                }
                else
                {
                    GUI.backgroundColor = Color.gray;
                    if (GUI.Button(new Rect(new Vector2(posaX - 240, posaY - 270), new Vector2(90, 75)), "1"))
                                    {
                                        level = 1;
                                        LoadLevel(level);
                                        CreateSnake();
                                    }
                }
                if (maxScoreLevel2 > 0)
                {
                    GUI.color = Color.black;
                    GUI.contentColor = Color.green;
                    GUI.backgroundColor = Color.green;
                    if (GUI.Button(new Rect(new Vector2(posaX - 140, posaY - 270), new Vector2(90, 75)), "2" + "\n" + maxScoreLevel2))
                         {
                               level = 2;
                               LoadLevel(level);
                               CreateSnake();
                         }
                }
                else
                {
                    GUI.backgroundColor = Color.gray;
                    if (GUI.Button(new Rect(new Vector2(posaX - 140, posaY - 270), new Vector2(90, 75)), "2"))
                    {
                        level = 2;
                        LoadLevel(level);
                        CreateSnake();
                    }
                }
                if (maxScoreLevel3 > 0)
                {
                    GUI.color = Color.black;
                    GUI.contentColor = Color.green;
                    GUI.backgroundColor = Color.green;
                    if (GUI.Button(new Rect(new Vector2(posaX - 40, posaY - 270), new Vector2(90, 75)), "3" + "\n" + maxScoreLevel3))
                    {
                        level = 3;
                        LoadLevel(level);
                        CreateSnake();
                    }
                }
                else
                {
                    GUI.backgroundColor = Color.gray;
                    if (GUI.Button(new Rect(new Vector2(posaX - 40, posaY - 270), new Vector2(90, 75)), "3"))
                    {
                        level = 3;
                        LoadLevel(level);
                        CreateSnake();
                    }
                }
                if (maxScoreLevel4 > 0)
                {
                    GUI.color = Color.black;
                    GUI.contentColor = Color.green;
                    GUI.backgroundColor = Color.green;
                    if (GUI.Button(new Rect(new Vector2(posaX + 60, posaY - 270), new Vector2(90, 75)), "4" + "\n" + maxScoreLevel4))
                    {
                        level = 4;
                        LoadLevel(level);
                        CreateSnake();
                    }
                }
                else
                {
                    GUI.backgroundColor = Color.gray;
                    if (GUI.Button(new Rect(new Vector2(posaX + 60, posaY - 270), new Vector2(90, 75)), "4"))
                    {
                        level = 4;
                        LoadLevel(level);
                        CreateSnake();
                    }
                }
                if (maxScoreLevel5 > 0)
                {
                    GUI.color = Color.black;
                    GUI.contentColor = Color.green;
                    GUI.backgroundColor = Color.green;
                    if (GUI.Button(new Rect(new Vector2(posaX + 160, posaY - 270), new Vector2(90, 75)), "5" + "\n" + maxScoreLevel5))
                    {
                        level = 5;
                        LoadLevel(level);
                        CreateSnake();
                    }
                }
                else
                {
                    GUI.backgroundColor = Color.gray;
                    if (GUI.Button(new Rect(new Vector2(posaX + 160, posaY - 270), new Vector2(90, 75)), "5"))
                    {
                        level = 5;
                        LoadLevel(level);
                        CreateSnake();
                    }
                }
                if (GUI.Button(new Rect(new Vector2(posaX - 240, posaY - 165), new Vector2(90, 30)), "Main menu"))
                {
                    Application.LoadLevel("GameSnake");
                }
                break;
        }
    }
}
