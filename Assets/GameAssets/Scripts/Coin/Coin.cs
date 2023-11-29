using Lean.Pool;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
