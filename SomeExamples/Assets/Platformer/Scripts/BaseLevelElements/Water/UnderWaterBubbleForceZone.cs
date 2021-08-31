using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWaterBubbleForceZone : MonoBehaviour
{
    private BoxCollider2D _collider;


    [SerializeField]
    private GameObject _archimedForce;
    [SerializeField]
    private PlayerStats _playerStat;
    private PlayerController _playerController;

    [SerializeField]
    private float _timeForDecrease = 0f;
    [SerializeField]
    private float _timeForAnimation = 0f;
    private bool _done = false;

    private ParticleSystem _particleSystem;
    [SerializeField]
    private int _maxParticlesON;
    [SerializeField]
    private int _maxParticlesOFF;


    Dictionary<GameObject, GameObject> _clientsForces = new Dictionary<GameObject, GameObject>();
    


    private void Start()
    {
        _collider = gameObject.GetComponent<BoxCollider2D>();
        _particleSystem = gameObject.GetComponent<ParticleSystem>();

        SwitchParticles(true);
    }
  
    public void SwitchColliderEnable()
    {
        if(_collider.enabled)
            SwitchParticles(true);
        else
            SwitchParticles(false);

        Debug.Log(gameObject.name + " - switched");
        StartCoroutine(ActivateAfterSec());
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_done)
            return;

        var force = Instantiate(_archimedForce, collision.transform.position, Quaternion.identity);
        force.transform.parent = collision.transform;
        _clientsForces.Add(collision.gameObject, force);

        //var rb = collision.gameObject.GetComponent<Rigidbody2D>();
        //if (rb != null)
        //    rb.drag = 1;

        SetPlayerStats(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_done)
            return;
        //var rb = collision.gameObject.GetComponent<Rigidbody2D>();
        //if (rb != null)
        //    rb.drag = 0;

        if (_clientsForces[collision.gameObject] != null)
            Destroy(_clientsForces[collision.gameObject]);
        _clientsForces.Remove(collision.gameObject);

        ResetPlayerStats(collision.gameObject);

    }

    private void SetPlayerStats(GameObject collisionObject)
    {
        if (collisionObject.transform.CompareTag("Player"))
        {
            _playerController = collisionObject.GetComponent<PlayerController>();
            if (_playerController != null)
                _playerController.SetPlayerStats(_playerStat);
        }
    }

    private void ResetPlayerStats(GameObject collisionObject)
    {
        if (collisionObject.transform.CompareTag("Player"))
        {
            if (_playerController == null)
                _playerController = collisionObject.GetComponent<PlayerController>();
            _playerController.ResetPlayeStats();
        }
    }

    private IEnumerator DeleteAllForces()
    {
        yield return new WaitForSecondsRealtime(_timeForDecrease);
        Debug.Log(gameObject.name + " - BUBBLES OFF");
        //SwitchParticles(false);
        foreach (var item in _clientsForces)
        {
            Destroy(item.Value);
            if(item.Key.tag == "Player")
                item.Key.GetComponent<PlayerController>().ResetPlayeStats();
        }
        _clientsForces.Clear();
        _done = true;
    }

    private IEnumerator ActivateAfterSec()
    {

        yield return new WaitForSecondsRealtime(_timeForAnimation);
        _collider.enabled = _collider.enabled ? false : true;
        _done = false;

        if (_collider.enabled)
        {
            //SwitchParticles(true);
            StartCoroutine(DeleteAllForces());
        }
    }

    private void SwitchParticles(bool mode)
    {
        var main = _particleSystem.main;
        if (mode)
            main.maxParticles = _maxParticlesON;
        else
            main.maxParticles = _maxParticlesOFF;
        _particleSystem.Play();
    }
}
