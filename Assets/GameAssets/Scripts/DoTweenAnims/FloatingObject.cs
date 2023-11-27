using UnityEngine;
using DG.Tweening;
public class FloatingObject : MonoBehaviour
{
    [SerializeField] private float floatingSpeed = 1f;
    [SerializeField] private float floatingDistance = 5f;
    private void Start()
    {
        transform.DOMoveY(transform.position.y + floatingDistance, floatingSpeed).SetLoops(-1, LoopType.Yoyo);
    }
}
