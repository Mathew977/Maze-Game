using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using qtools.qmaze;
using qtools.qmaze.example1;
using UnityEngine.UI;

public class QFPCMazeGame : MonoBehaviour {
    [SerializeField]
    private QMazeEngine mazeEngine;
    [SerializeField]
    private GameObject finishPrefab;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private int numberLevels = 3;
    [SerializeField]
    private GameObject enemy;
    private bool needToGenerateNewMaze = true;

    public QMazeEngine MazeEngine
    {
        get { return mazeEngine; }
    }
    public void NeedToGenerateNewMaze(bool generateNewMaze)
    {
        needToGenerateNewMaze = generateNewMaze;
    }

    private int currentLevel = 0;

    public int CurrentLevel
    {
        get { return currentLevel; }
    }
	// Use this for initialization
	void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        if (needToGenerateNewMaze == true)
        {
            needToGenerateNewMaze = false;
            GenerateNewMaze();
        }
	}

    void GenerateNewMaze()
    {
        //Check that the maze engine has been specified, if not return and exit early
        if (mazeEngine == null) return;

        //destory any existing maze
        mazeEngine.destroyImmediateMazeGeometry();

        //Generate new Maze
        mazeEngine.generateMaze();

        //Add the finish points
        AddFinishPoints();

        //Position Player
        PositionPlayer();

        //Position Enemy
        PositionEnemy();

        //Increase the level counter
        currentLevel++;

    }

    private void AddFinishPoints()
    {
        //The maze engine provides a method called getFinishPositionList, which returns a list of finish points
        //Get the list of finish points from the mazeEngine and store them in the variable finishPoints
        List<QVector2IntDir> finishPoints = mazeEngine.getFinishPositionList();

        //Loop through the finish points and instantiate them
        foreach (QVector2IntDir finishPoint in finishPoints)
        {
            //Instantiate a new game object
            GameObject goFinish = Instantiate(finishPrefab) as GameObject;

            //Set the object's parent to the maze so it is correctly positioned within the maze
            goFinish.transform.parent = mazeEngine.transform;

            //Position the finish object relative to its parent (local position)
            //Scale the x and z position according to the maze's width and height
            //The y-position is fixed at 0.01f;
            float x = finishPoint.x * mazeEngine.getMazePieceWidth();
            float z = -finishPoint.y * mazeEngine.getMazePieceHeight();
            goFinish.transform.localPosition = new Vector3(x, 0.01f, z);
        }
    }

    public void PositionPlayer()
    {
        //Check if there is a FPC in the scene and position it
        //Get the QFPSController component attached to the player
        QFPSController fpsController = player.GetComponent<QFPSController>();

        //Get the list of start points from the mazeEngine and store them in the variable startPoints
        List<QVector2IntDir> startPoints = mazeEngine.getStartPositionList();

        //Check that the fpsController is not null
        if (fpsController != null)
        {
            //Check if start list count is zero
            if (startPoints.Count == 0) //no start points specified
            {
                //Set the start point to a Random position
                fpsController.transform.position = new Vector3(Random.Range(0, mazeEngine.getMazeWidth()), 1, Random.Range(0, mazeEngine.getMazeHeight()));
            }
            else
            {
                //Set it to the first start position in the list
                QVector2IntDir startPoint = startPoints[Random.Range(0, startPoints.Count - 1)];

                //Position the player object rand scale the x and z position according to the maze's width and height
                //The y-position is fixed at 0.01f
                fpsController.transform.position = new Vector3(startPoint.x * mazeEngine.getMazePieceWidth(), 0.01f, -startPoint.y * mazeEngine.getMazePieceHeight());

                //Rotate the Player
                fpsController.setRotation(Quaternion.AngleAxis((int)startPoint.direction * 90, Vector3.up));
            }
        }
    }

    private void OnGUI()
    {
        //When user starts playing, lock and hide the cursor
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        //When user presses excape key, reenable the cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void PositionEnemy()
    {
        enemy = GameObject.Find("ToonGhost-Orange");

        //Check that the enemy is not null
        if (enemy != null)
        {
            //Position the enemy object rand scale the x and z position according to the maze's width and height
            //The y-position is fixed at 0.01f
            enemy.transform.position = new Vector3(Random.Range(1,10) * mazeEngine.getMazePieceWidth(), 0.01f, -Random.Range(1, 10) * mazeEngine.getMazePieceHeight());
        }
    }
}