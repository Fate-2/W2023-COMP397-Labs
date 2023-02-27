using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private List<NavMeshSurface> _maze;
    [SerializeField] private List<GameObject> _robots;
    [SerializeField] private Transform _respawnTransform;
    [SerializeField] private Transform _playerTransform;

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

    public void RespawnPlayer()
    {
        _playerTransform.position = _respawnTransform.position;
    }

}
