using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiScript : MonoBehaviour
{

    string[] levelNames = new string[] { "Dark forest", "Pirates of the caribbean", "Ocean" };
    string[] levelScenesNames = new string[] { "DarkForest", "DarkForest", "Underwater" };
    private GameObject level;
    private GameObject leftButton;
    private GameObject rightButton;
    private Text text;
    private GameObject levelText;
    private int actualLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        level = GameObject.Find("level");
        levelText = GameObject.Find("LevelNameText");
        text = levelText.GetComponent<Text>();
        leftButton = GameObject.Find("previousLevel");
        rightButton = GameObject.Find("nextLevel");
    }

    // Update is called once per frame
    void Update()
    {
        //levelText.Get
        text.text = levelNames[actualLevel];
    }

    public void nextLevel()
    {
        if (actualLevel < 2)
        {
            actualLevel++;
            GameObject.Find("left_disabled").GetComponent<Renderer>().enabled = false;
            GameObject.Find("left_marked").GetComponent<Renderer>().enabled = false;
            GameObject.Find("left_normal").GetComponent<Renderer>().enabled = true;

        }
        if (actualLevel >= 2)
        {
            GameObject.Find("rightdisabled").GetComponent<Renderer>().enabled=true;
            GameObject.Find("rightmarked").GetComponent<Renderer>().enabled = false;
            GameObject.Find("rightnormal").GetComponent<Renderer>().enabled = false;
        }
    }
    public void previousLevel()
    {
        if (actualLevel > 0)
        {
            actualLevel--;
            GameObject.Find("rightdisabled").GetComponent<Renderer>().enabled = false;
            GameObject.Find("rightmarked").GetComponent<Renderer>().enabled = false;
            GameObject.Find("rightnormal").GetComponent<Renderer>().enabled = true;

        }
        if (actualLevel <= 0)
        {
            GameObject.Find("left_disabled").GetComponent<Renderer>().enabled = true;
            GameObject.Find("left_marked").GetComponent<Renderer>().enabled = false;
            GameObject.Find("left_normal").GetComponent<Renderer>().enabled = false;
        }
    }

    public void startGame()
    {
        Application.LoadLevel(levelScenesNames[actualLevel]);
        Debug.Log("start game clicked");
    }
    public void exitGame()
    {
        Application.Quit();
        Debug.Log("exit game clicked");
    }
}
