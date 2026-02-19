using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawnerBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float MAX_HEIGHT = 3f;
    [SerializeField]
    private float MAX_HORIZONTAL_POS = 10f;
    [SerializeField]
    private float TIME_BETWEEN_SPAWS = 1f;
    [SerializeField]
    private int MAX_SPAWNS = 10;
    private float _target_scale = 1f;
    static private int _spawnCount = 0;
    static public int SpawnCount { get => _spawnCount; set => _spawnCount = value; }

    private GameSessionManager _gameSessionManager;
    [SerializeField]
    public GameObject targetPrefab;

    private bool _spawnCoroutineEnabled;
    // Start is called before the first frame update
    void Start()
    {
        _spawnCoroutineEnabled = false;
        switch (GameSessionManager.SelectedDifficulty)
        {
            case "Easy":
                MAX_SPAWNS = 5;
                TIME_BETWEEN_SPAWS = 2f;
                _target_scale = 1.5f;
                break;
            case "Medium":
                MAX_SPAWNS = 10;
                TIME_BETWEEN_SPAWS = 1f;
                _target_scale = 1f;
                break;
            case "Hard":
                MAX_SPAWNS = 15;
                TIME_BETWEEN_SPAWS = 0.5f;
                _target_scale = 0.75f;
                break;
            default:
                MAX_SPAWNS = 10;
                TIME_BETWEEN_SPAWS = 1f;
                _target_scale = 1f;
                break;
        }
        StartCoroutine(nameof(SpawnCorountine));
    }

    void ResetRoutine()
    {
        StopCoroutine(nameof(SpawnCorountine));
        SpawnCount = 0;
        _spawnCoroutineEnabled = false;
        StartCoroutine(nameof(SpawnCorountine));
    }

    void RestartRoutine()
    {
        StopCoroutine(nameof(SpawnCorountine));
        SpawnCount = 0;
        _spawnCoroutineEnabled = true;
        StartCoroutine(nameof(SpawnCorountine));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnCorountine()
    {
        while (true)
        {
            if (_spawnCoroutineEnabled)
            {
                if (SpawnCount < MAX_SPAWNS)
                {
                    SpawnTarget();
                    SpawnCount++;
                }
            }
            yield return new WaitForSeconds(TIME_BETWEEN_SPAWS);
        }
    }

    public void StartSpawning()
    {
        ResetRoutine();
        _spawnCoroutineEnabled = true;
    }

    public void StopSpawning()
    {
        _spawnCoroutineEnabled = false;
    }

    public void SpawnTarget()
    {
        GameObject target = Instantiate(targetPrefab);
        target.transform.position = new Vector3(Random.Range(
            -MAX_HORIZONTAL_POS, MAX_HORIZONTAL_POS),
            Random.Range(1.5f, MAX_HEIGHT),
            Random.Range(-MAX_HORIZONTAL_POS, MAX_HORIZONTAL_POS)
            );
        target.transform.localScale *= _target_scale;
    }

    public void KillAllTargets()
    {
        TargetBehaviour[] targets = GameObject.FindObjectsOfType<TargetBehaviour>();
        foreach (var item in targets)
        {
            GameObject.Destroy(item.gameObject);
        }
    }
}