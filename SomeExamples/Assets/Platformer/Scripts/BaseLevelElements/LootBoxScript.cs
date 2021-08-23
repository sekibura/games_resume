using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxScript : Attackable
{
    [SerializeField]
    private GameObject[] _itemList;
    private Animator _animator;
    private bool _done = false;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }
    public override void ApplyDamage(int damageValue, Vector3 playerPosition)
    {
        if (_done)
            return;

        _animator.Play("LootBox_OPENING_anim");
        AudioManager.Instance.Play("Chest");
        foreach (GameObject item in _itemList)
        {
            Debug.Log(gameObject.name + " drop " + item.name);
            DropItem(item, playerPosition);
        }
        _done = true;
    }

    private void DropItem(GameObject item, Vector3 player)
    {
        var dropedItem = Instantiate(item, gameObject.transform.position, Quaternion.identity);
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
