using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : Attackable
{
    [SerializeField]
    private Sprite _onSprite;
    private Sprite _offSprite;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private bool _isOpen = false;

    private void Start()
    {
        _animator = transform.parent.transform.Find("Door")?.GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _offSprite = _spriteRenderer?.sprite;
    }
    public override void ApplyDamage(int damageValue, Vector3 playerPosition)
    {
        
        if (_isOpen)
        {
            _spriteRenderer.sprite = _offSprite;
            _isOpen = false;
        }
        else
        {
            _spriteRenderer.sprite = _onSprite;
            _isOpen = true;
        }
        _animator.SetBool("IsOpen", _isOpen);
        AudioManager.Instance.Play("switch");
    }
}
