using UnityEngine;
using Lean.Pool;
public class CoinDestroy : MonoBehaviour
{
    private void Start(){
        LeanPool.Despawn(gameObject, 8f);
    }
}
