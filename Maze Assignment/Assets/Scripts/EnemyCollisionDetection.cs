using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyCollisionDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Equals("FPSController"))
        {
            SceneManager.LoadScene("Main Menu");
        }

        if (col.gameObject.name.Equals("DefaultBullet(Clone)"))
        {
            GameObject.Find("QMazeGame").GetComponent<QFPCMazeGame>().PositionEnemy();
        }
    }
}
