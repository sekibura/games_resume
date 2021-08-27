using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField]
    private Sprite _pressedButton;
    private Sprite _unpressedButton;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider2D;
    private Animator _animator;
    [SerializeField]
    private GameObject _door;

    private void Start()
    {
        _collider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _unpressedButton = _spriteRenderer.sprite;
        _animator = _door.GetComponent<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.Instance.PlayRandomSound("StoneDoor");
        _spriteRenderer.sprite = _pressedButton;
        _animator?.SetBool("IsOpen",true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        AudioManager.Instance.PlayRandomSound("StoneDoor");
        _spriteRenderer.sprite = _unpressedButton;
        _animator?.SetBool("IsOpen", false);
    }
}
