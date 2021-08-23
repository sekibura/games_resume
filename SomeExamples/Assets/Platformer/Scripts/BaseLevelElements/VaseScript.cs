using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseScript : Attackable
{
    [SerializeField]
    private GameObject[] _itemList;
    [SerializeField]
    private GameObject _concreteItem;
    [SerializeField]
    private bool _randomDrop = false;
    private Animator _animator;
    [SerializeField]
    private ParticleSystem _particleSystem;
    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }
    public override void ApplyDamage(int damageValue, Vector3 playerPosition)
    {
        _animator.Play("VaseBroke");
        _particleSystem.Play();
        AudioManager.Instance.Play("Vase");
        if (_randomDrop)
        {
            int itemNumber = Random.Range(0, _itemList.Length);
            Debug.Log(gameObject.name+" drop "+ _itemList[itemNumber].name);
            DropItem(_itemList[itemNumber], playerPosition);
        }
        else
        {
            DropItem(_concreteItem, playerPosition);
        }
    }

    private void DropItem(GameObject item, Vector3 player)
    {
        var dropedItem = Instantiate(item, gameObject.transform.position,Quaternion.identity);
        AddImpulse(player, dropedItem);
    }

    private void AddImpulse(Vector3 damageSource, GameObject item)
    {
        float xValue = Random.Range(-2f, 2f);
        Vector3 direction = new Vector3(xValue, 6, 0);
        Rigidbody2D rb = item.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb?.AddForce(direction, ForceMode2D.Impulse);
    }

    public override int GetHp()
    {
        return 0;
    }
}
