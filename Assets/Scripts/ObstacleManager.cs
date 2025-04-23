using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [Header("Spawn Values")]
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Vector3 spawnOffset;
    [SerializeField] private Vector3 relativeSpawnArea;
    [SerializeField] private float spawnInterval;

    [Header("Obstacle Values")]
    [SerializeField] private float torqeMultiplier;
    [SerializeField] private float maxMagnitude;
    [SerializeField] private float velocityDampMultiplier;

    private float _spawnDelay;

    private void Update()
    {
        if (GameManager.Instance.IsGamePaused)
        {
            return;
        }

        if (_spawnDelay < 0f)
        {
            Vector3 adjustedSpawnOffset = spawnOffset;
            adjustedSpawnOffset.z *= GameManager.Instance.ScoreManager.VelocityFractionRestricted;
            Vector3 spawnPos = player.transform.position + adjustedSpawnOffset;
            spawnPos += new Vector3(Random.Range(-(relativeSpawnArea.x * 0.5f), relativeSpawnArea.x * 0.5f), 
                                    Random.Range(-(relativeSpawnArea.y * 0.5f), relativeSpawnArea.y * 0.5f), 
                                    Random.Range(-(relativeSpawnArea.y * 0.5f), relativeSpawnArea.z * 0.5f));

            Obstacle obstacle = GameManager.Instance.ObjectPoolingManager.GetObstacle(Random.Range(0, GameManager.Instance.ObjectPoolingManager.SpawnedObstaclesVariants));
            obstacle.transform.parent = transform;
            obstacle.transform.position = spawnPos;
            obstacle.transform.rotation = Random.rotation;
            obstacle.gameObject.SetActive(true);
            obstacle.Initialize(this, torqeMultiplier, maxMagnitude, velocityDampMultiplier);
            _spawnDelay = spawnInterval;
        }
        _spawnDelay -= Time.deltaTime;
    }

    public void OnObstacleDestroyed(Obstacle obstacle)
    {
        GameManager.Instance.ObjectPoolingManager.ReturnObstacle(obstacle);
    }
}
