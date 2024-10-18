using System.Collections;
using UnityEngine;

public class TargetSpawnerBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float MAX_HEIGHT = 2f;
    [SerializeField]
    private float MAX_HORIZONTAL_POS = 10f;
    [SerializeField]
    private float TIME_BETWEEN_SPAWS = 1f;
    [SerializeField]
    private int MAX_SPAWNS = 10;
    static private int _spawnCount = 0;
    static public int SpawnCount { get => _spawnCount; set => _spawnCount = value; }

    [SerializeField]
    public GameObject targetPrefab;

    private bool _spawnCoroutineEnabled;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(SpawnCorountine));
        StartSpawning();
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
            Random.Range(1, MAX_HEIGHT),
            Random.Range(-MAX_HORIZONTAL_POS, MAX_HORIZONTAL_POS)
            );
    }
}