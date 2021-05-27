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
    private Animator _doorAnimator;

    private void Start()
    {
        _collider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _unpressedButton = _spriteRenderer.sprite;
        _doorAnimator = gameObject.transform.parent.transform.Find("Door").gameObject.GetComponent<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _spriteRenderer.sprite = _pressedButton;
        _doorAnimator?.SetBool("IsOpen",true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _spriteRenderer.sprite = _unpressedButton;
        _doorAnimator?.SetBool("IsOpen", false);
    }
}
