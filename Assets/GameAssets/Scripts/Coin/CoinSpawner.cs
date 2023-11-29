using UnityEngine;
using Lean.Pool;
using System.Collections;
public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject coinPrefab;

    void Start()
    {
        StartCoroutine(SpawnPipe());
    }
    private IEnumerator SpawnPipe()
    {
        while(true)
        {
            var randomY = Random.Range(minY, maxY);
            var pipe = LeanPool.Spawn(coinPrefab, new Vector3(transform.position.x,randomY,transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
