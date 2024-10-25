using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject particule;
    public ScoreManager scoreManager;
    void Start()
    {
        scoreManager = GameObject.Find("Score Manager").GetComponent<ScoreManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            scoreManager.IncrementScore();
            GameObject explosion= Instantiate(particule);
            explosion.transform.position = transform.position;
            Destroy(explosion,0.75f);
            Debug.Log(explosion.transform.position);
            Destroy(gameObject);
            Destroy(other.gameObject);
            TargetSpawnerBehaviour.SpawnCount--;
            
        }
    }
}
