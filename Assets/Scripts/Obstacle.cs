using System.Collections;
using System.Collections.Generic;
using UnityEngine.VFX;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private ObstacleType type;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Obstacle parentObstacle;
    [SerializeField] private float velocityModifier;
    [SerializeField] private int pointsAmount;
    [SerializeField] private AudioClipSettings onDestroyAudio;
    [SerializeField] private VisualEffectPlayer onDestroyEffect;

    public ObstacleType Type => type;
    public float VelocityModifier => (type == ObstacleType.Inherited) ? parentObstacle.VelocityModifier : velocityModifier;
    public int PointsAmount => (type == ObstacleType.Inherited) ? parentObstacle.PointsAmount : pointsAmount;

    private AudioClipSettings OnDestroyAudio => (type == ObstacleType.Inherited) ? parentObstacle.OnDestroyAudio : onDestroyAudio;
    private VisualEffectPlayer OnDestroyVFX => (type == ObstacleType.Inherited) ? parentObstacle.OnDestroyVFX : onDestroyEffect;

    private Transform _root;
    private ObstacleManager _obstacleManager;
    private float _torquMultiplier;
    private float _maxMagnitude;
    private float _velocityDampMultiplier;
    private bool _enabled;


    private void Awake()
    {
        _root = GetRoot(this);
    }

    private void OnEnable()
    {
        _enabled = true;
    }

    private void FixedUpdate()
    {
        if (rb != null && rb.velocity.magnitude > _maxMagnitude)
        {
            rb.velocity = rb.velocity * _velocityDampMultiplier;
        }
    }

    public void Initialize(ObstacleManager obstacleManager, float torquMultiplier, float maxMagnitude, float velocityDampMultiplier)
    {
        _obstacleManager = obstacleManager;
        _torquMultiplier = torquMultiplier;
        _maxMagnitude = maxMagnitude;
        _velocityDampMultiplier = velocityDampMultiplier;

        if (rb != null)
        {
            rb.AddTorque(new Vector3(Random.Range(-1f, 1f) * _torquMultiplier, Random.Range(-1f, 1f) * _torquMultiplier, Random.Range(-1f, 1f) * _torquMultiplier));
        }
    }

    private Transform GetRoot(Obstacle obstacle)
    {
        if (obstacle.Type != ObstacleType.Inherited)
        {
            return obstacle.transform;
        }
        return GetRoot(obstacle.parentObstacle);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_enabled == false)
        {
            return;
        }

        if (other.gameObject.layer == (int)GameConsts.Layers.Player)
        {
            ManagePlayerHit();
        }
        else if (other.gameObject.layer == (int)GameConsts.Layers.Ground)
        {
            DestroyObstacle();
        }
    }

    private void ManagePlayerHit()
    {
        GameManager.Instance.ScoreManager.IncreaseScore(PointsAmount);
        GameManager.Instance.ScoreManager.ChangeVelocity(VelocityModifier);

        if (Type == ObstacleType.Destructable)
        {
            AudioClipSettings audioClipSettings = OnDestroyAudio;
            if (audioClipSettings != null)
            {
                GameManager.Instance.SoundManager.PlayClip(audioClipSettings.ID, (audioClipSettings.attachTransform) ? transform : null, audioClipSettings.looped, audioClipSettings.volume, audioClipSettings.spatialBlend);
            }

            if (OnDestroyVFX != null)
            {
                VisualEffectPlayer vfxPlayer = Instantiate(OnDestroyVFX.gameObject, transform.position, Quaternion.identity).GetComponent<VisualEffectPlayer>();
                vfxPlayer.Play();
            }

            DestroyObstacle();
        }
    }

    private void DestroyObstacle()
    {
        if (_obstacleManager != null)
        {
            _obstacleManager.OnObstacleDestroyed(this);
        }
        _enabled = false;
        gameObject.SetActive(false);
    }
}

public enum ObstacleType
{
    None                = 0,
    Inherited           = 1,
    Destructable        = 2,
    NonDestructable     = 3,
}
