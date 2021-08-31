using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterZone : MonoBehaviour
{
    [SerializeField]
    private GameObject _particleSystem;
    [SerializeField]
    private GameObject _archimedForce;
    [SerializeField]
    private GameObject _underWaterDamage;
    [SerializeField]
    private GameObject _enterExitBubbles;
    private float _delaySpawn = 0.1f;
    private float _lastTime;
    [SerializeField]
    private float _forceUp = 1f;
    [SerializeField]
    private float _defaultDrag = 1f;

    [SerializeField]
    private DragTag[] _dragTags;
    [SerializeField]
    private PlayerStats _playerStat;
    private PlayerController _playerController;

    List<GameObject> _currentCollisions = new List<GameObject>();
    Dictionary<GameObject, GameObject> _clientsParticles = new Dictionary<GameObject, GameObject>();
    Dictionary<GameObject, GameObject> _clientsForces = new Dictionary<GameObject, GameObject>();
    Dictionary<GameObject, GameObject> _clientsUnderwaterDamage = new Dictionary<GameObject, GameObject>();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Instantiate(_enterExitBubbles, collision.transform.position, Quaternion.identity);

        //currentCollisions.Add(collision.gameObject);
        Debug.Log(collision.name + " enter into water zone");

        if(collision.tag == "Player")
            AudioManager.Instance.PlayRandomSound("Bubbles");

        var particle = Instantiate(_particleSystem, collision.transform.position, Quaternion.identity);
        particle.transform.parent = collision.transform;
        _clientsParticles.Add(collision.gameObject, particle);

        var force = Instantiate(_archimedForce, collision.transform.position, Quaternion.identity);
        force.transform.parent = collision.transform;
        _clientsForces.Add(collision.gameObject, force);

        
        var damage = Instantiate(_underWaterDamage, collision.transform.position, Quaternion.identity, collision.transform);
        //damage.transform.parent = collision.transform;
        _clientsUnderwaterDamage.Add(collision.gameObject, damage);

        var rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            foreach (var item in _dragTags)
            {
                if(item.Tag == collision.gameObject.tag)
                {
                    rb.drag = item.DragValue;
                    break;
                }
                rb.drag = _defaultDrag;
            }
        }
            

        SetPlayerStats(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            AudioManager.Instance.PlayRandomSound("Bubbles");
        //Instantiate(_enterExitBubbles, collision.transform.position, Quaternion.identity);

        //currentCollisions.Remove(collision.gameObject);
        Debug.Log(collision.name + " exit from water zone");

        if(_clientsParticles[collision.gameObject]!=null)
        Destroy(_clientsParticles[collision.gameObject]);
        _clientsParticles.Remove(collision.gameObject);

        var rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.drag = 0;

        if (_clientsForces[collision.gameObject] != null)
            Destroy(_clientsForces[collision.gameObject]);
        _clientsForces.Remove(collision.gameObject);

        _clientsUnderwaterDamage[collision.gameObject].GetComponent<UnderWaterState>().ResetBubbles();
        Destroy(_clientsUnderwaterDamage[collision.gameObject]);
        _clientsUnderwaterDamage.Remove(collision.gameObject);

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
            if(_playerController == null)
                _playerController = collisionObject.GetComponent<PlayerController>();
            _playerController.ResetPlayeStats();
        }
    }

}

[Serializable]
public class DragTag
{
    public string Tag;
    public float DragValue;
}
