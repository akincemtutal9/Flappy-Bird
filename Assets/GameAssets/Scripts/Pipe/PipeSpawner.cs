using System.Collections;
using UnityEngine;
using Lean.Pool;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject pipePrefab;

    void Start()
    {
        StartCoroutine(SpawnPipe());
    }
    private IEnumerator SpawnPipe()
    {
        while(true)
        {
            var randomY = Random.Range(minY, maxY);
            var pipe = LeanPool.Spawn(pipePrefab, new Vector3(transform.position.x,randomY,transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
