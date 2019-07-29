using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelNumber : MonoBehaviour {
    [SerializeField]
    private Text currentLevel;
    private int numberLevel;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update ()
    {
        numberLevel = GameObject.Find("QMazeGame").GetComponent<QFPCMazeGame>().CurrentLevel;
        currentLevel.text = "Level: " + numberLevel.ToString();
    }
}
