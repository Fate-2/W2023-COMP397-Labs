using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{

    [SerializeField] private List<NavMeshSurface> _maze;
    [SerializeField] private List<GameObject> _robots;
    [SerializeField] private GameObject _pausePanel;
   
    public static bool isPaused = false;

    private void Awake()
    {
        isPaused = false;
        _pausePanel.SetActive(false);
    }


    private  void Start()
    {
        foreach (var tiles in _maze)
        {
            tiles.BuildNavMesh();
        }
        foreach (var robot in _robots)
        {
            robot.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {  
           isPaused = !isPaused;
           _pausePanel.SetActive(isPaused);
           PauseGame(isPaused);
           
        }
    }
     private void PauseGame(bool isPausing)
    {
        Cursor.lockState = isPausing ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = isPausing ? 0 : 1;
    }
    public void MainMenuButton_Pressed()
    {
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }
}
