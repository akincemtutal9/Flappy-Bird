using UnityEngine;
using Lean.Pool;

public class PipeDestroy : MonoBehaviour
{
    private void Start(){
        LeanPool.Despawn(gameObject, 8f);
    }
}
