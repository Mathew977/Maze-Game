using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour {
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals("Finish"))
        {
            //Debug.Log("Next Level");
            GameObject.Find("QMazeGame").GetComponent<QFPCMazeGame>().NeedToGenerateNewMaze(true);
        }
    }
}