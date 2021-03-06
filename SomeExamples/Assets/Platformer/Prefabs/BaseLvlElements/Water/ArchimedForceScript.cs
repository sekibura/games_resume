using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchimedForceScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField]
    private float _force = 15f;
    public float TimeToDelete = 0f;
    private void Start()
    {
        _rb = gameObject.transform.parent.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if(_rb == null)
            _rb = gameObject.transform.parent.GetComponent<Rigidbody2D>();
        else
            _rb.AddForce(Vector3.up* _force, ForceMode2D.Force);
    }

    public void StartDestroyTimer()
    {
        StartCoroutine(DestroyAfterTime(TimeToDelete));
    }
    public void StartDestroyTimer(float time)
    {
        StartCoroutine(DestroyAfterTime(time));
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Destroy(gameObject);
    }

}
