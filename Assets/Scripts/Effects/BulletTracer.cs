using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class BulletTracer : MonoBehaviour
{
    [SerializeField] private float speed = 100f;
    
    private TrailRenderer _trail;
    private Vector3 _target;

    private void Awake()
    {
        _trail = GetComponent<TrailRenderer>();
    }

    public void Init(Vector3 start, Vector3 end)
    {
        transform.position = start;
        
        _trail.Clear();
        
        _target = end;

        StartCoroutine(MoveTracer());
    }

    private IEnumerator MoveTracer()
    {
        while (Vector3.Distance(transform.position, _target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                _target,
                speed * Time.unscaledDeltaTime);
            
            yield return null;
        }
        
        Destroy(gameObject);
    }
}
