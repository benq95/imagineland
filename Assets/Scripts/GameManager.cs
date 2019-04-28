using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int points;
    public int playerHealths = 5;
    public Text pointsText;
    public Text healthsText;

    #region Singleton

    public static GameManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of EquipmentManager found!");
            return;
        }
        instance = this;
    }

    #endregion

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        pointsText.text = points.ToString();
        healthsText.text = playerHealths.ToString();
    }
}
