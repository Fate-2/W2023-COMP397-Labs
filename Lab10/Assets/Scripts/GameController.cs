using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public enum Difficulty
{
    Easy, Normal, Hard, veryHard
}
public class GameController : MonoBehaviour
{

    [SerializeField] private List<NavMeshSurface> _maze;
    public MazeGenerator mazeGenerator;
    public RobotSpawner RobotSpawner;
    [SerializeField] private List<GameObject> _robots;
    [SerializeField] private GameObject _pausePanel;
   
    public static bool isPaused = false;
    public Difficulty difficulty = Difficulty.Easy;

    private void Awake()
    {
        isPaused = false;
        _pausePanel.SetActive(false);
    }


    private  void Start()
    {
        SetupMaze();
        foreach (var tiles in _maze)
        {
            tiles.BuildNavMesh();
        }
        SpawnRobots();

        //    foreach (var tiles in _maze)
        //    {
        //        tiles.BuildNavMesh();
        //    }
        //    foreach (var robot in _robots)
        //    {
        //        robot.SetActive(true);
        //    }
    }

    private void SetupMaze()
    {
        mazeGenerator.GenerateMaze();
    }

    private void SpawnRobots()
    {
        RobotSpawner.SpawnRobots(mazeGenerator.activeTiles, difficulty);
    }
    private void Update()
    {
#if !UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {  
           isPaused = !isPaused;
           _pausePanel.SetActive(isPaused);
           PauseGame(isPaused);
           
        }
#endif
    }

    private void Pause()
    {
        isPaused = !isPaused;
        _pausePanel.SetActive(isPaused);
        PauseGame(isPaused);

    }
   
    private void PauseGame(bool isPausing)
    {
#if !UNITY_ANDROID
        Cursor.lockState = isPausing ? CursorLockMode.None : CursorLockMode.Locked;
#endif
        Time.timeScale = isPausing ? 0 : 1;
    }
    public void MainMenuButton_Pressed()
    {
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }

    public void PauseButton_Pressed()
    {
        Pause();
    }
 
}
