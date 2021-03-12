using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    private IList<Transform> waypoints;

    public GameObject EnemyPrefab { get => enemyPrefab; }
    public GameObject PathPrefab { get => pathPrefab; }
    public float TimeBetweenSpawn { get => timeBetweenSpawns; }
    public float SpawnRandomFactor { get => spawnRandomFactor; }
    public int NumberOfEnemies { get => numberOfEnemies; }
    public float MoveSpeed { get => moveSpeed; }

    public IList<Transform> Waypoints
    {
        get
        {
            if (waypoints == null || waypoints.Count <= 0)
            {
                waypoints = new List<Transform>();

                foreach (Transform waypoint in pathPrefab.transform)
                {
                    waypoints.Add(waypoint);
                }
            }

            return waypoints;
        }
    }
}
