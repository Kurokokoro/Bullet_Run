using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    [Header("Pooled Objects")]
    [SerializeField] private List<ObstaclePool> obstacleObjectPools;
    [SerializeField] private AudioPlayerPool audioPlayerPool;

    private List<ObstacleSpawnedPool> _spawnedObstacles;
    private List<AudioPlayer> _spawnedAudioPlayers;

    public int SpawnedObstaclesVariants => _spawnedObstacles.Count;


    private void Awake()
    {
        SpawnObstacles();
        SpawnAudioPlayers();
    }

    public void ReturnAllSpawnedObjects()
    {
        ReturnAllObstacles();
        ReturnAllAudioPlayers();
    }

    private void SpawnObstacles()
    {
        _spawnedObstacles = new List<ObstacleSpawnedPool>();
        for (int i = 0; i < obstacleObjectPools.Count; i++)
        {
            _spawnedObstacles.Add(new ObstacleSpawnedPool());
            for (int j = 0; j < obstacleObjectPools[i].obstacleAmount; j++)
            {
                Obstacle obstacle = Instantiate(obstacleObjectPools[i].obstaclePrefab.gameObject, transform).GetComponent<Obstacle>();
                obstacle.name = string.Format("{0}_{1}", obstacleObjectPools[i].obstaclePrefab.name, j.ToString("00"));
                obstacle.gameObject.SetActive(false);
                _spawnedObstacles[_spawnedObstacles.Count - 1].spawnedObstacles.Add(obstacle);
            }
        }
    }

    public Obstacle GetObstacle(int index)
    {
        if (index < 0 || _spawnedObstacles.Count - 1 < index)
        {
            return null;
        }

        return _spawnedObstacles[index].GetOldestUsed();
    }

    public void ReturnObstacle(Obstacle obstacle)
    {
        obstacle.transform.parent = transform;
        obstacle.transform.position = Vector3.zero;
        obstacle.transform.rotation = Quaternion.identity;
        obstacle.gameObject.SetActive(false);
    }

    private void ReturnAllObstacles()
    {
        for (int i = 0; i < _spawnedObstacles.Count; i++)
        {
            for (int j = 0; j < _spawnedObstacles[i].spawnedObstacles.Count; j++)
            {
                ReturnObstacle(_spawnedObstacles[i].spawnedObstacles[j]);
            }
        }
    }

    private void SpawnAudioPlayers()
    {
        _spawnedAudioPlayers = new List<AudioPlayer>();
        for (int i = 0; i < audioPlayerPool.audioPlayerAmount; i++)
        {
            AudioPlayer audioPlayer = Instantiate(audioPlayerPool.audioPlayerPrefab.gameObject, transform).GetComponent<AudioPlayer>();
            audioPlayer.name = string.Format("{0}_{1}", audioPlayerPool.audioPlayerPrefab.name, i.ToString("00"));
            audioPlayer.gameObject.SetActive(false);
            _spawnedAudioPlayers.Add(audioPlayer);
        }
    }

    public AudioPlayer GetAudioPlayer()
    {
        for (int i = 0; i < _spawnedAudioPlayers.Count; i++)
        {
            if (_spawnedAudioPlayers[i].IsPlaying == false)
            {
                return _spawnedAudioPlayers[i];
            }
        }
        AudioPlayer audioPlayer = Instantiate(audioPlayerPool.audioPlayerPrefab.gameObject, transform).GetComponent<AudioPlayer>();
        audioPlayer.name = string.Format("{0}_{1}", audioPlayerPool.audioPlayerPrefab.name, _spawnedAudioPlayers.Count.ToString("00"));
        audioPlayer.gameObject.SetActive(false);
        _spawnedAudioPlayers.Add(audioPlayer);
        audioPlayerPool.audioPlayerAmount++;
        return audioPlayer;
    }

    public void ReturnAudioPlayer(AudioPlayer audioPlayer)
    {
        audioPlayer.transform.parent = transform;
        audioPlayer.transform.position = Vector3.zero;
        audioPlayer.transform.rotation = Quaternion.identity;
        audioPlayer.gameObject.SetActive(false);
    }

    private void ReturnAllAudioPlayers()
    {
        for (int i = 0; i < _spawnedAudioPlayers.Count; i++)
        {
            _spawnedAudioPlayers[i].Stop();
            ReturnAudioPlayer(_spawnedAudioPlayers[i]);
        }
    }
}

[System.Serializable]
public class ObstaclePool
{
    public Obstacle obstaclePrefab;
    public int obstacleAmount;
}

[System.Serializable]
public class ObstacleSpawnedPool
{
    public List<Obstacle> spawnedObstacles = new List<Obstacle>();

    private int _currentIndex;


    public Obstacle GetOldestUsed()
    {
        _currentIndex++;
        if (_currentIndex >= spawnedObstacles.Count - 1)
        {
            _currentIndex = 0;
        }

        return spawnedObstacles[_currentIndex];
    }
}

[System.Serializable]
public class AudioPlayerPool
{
    public AudioPlayer audioPlayerPrefab;
    public int audioPlayerAmount;
}