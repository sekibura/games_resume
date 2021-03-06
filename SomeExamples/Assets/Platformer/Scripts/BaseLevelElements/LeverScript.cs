using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : Attackable
{
    [SerializeField]
    private Sprite _onSprite;
    private Sprite _offSprite;
    private SpriteRenderer _spriteRenderer;
    private List<Animator> _animators = new List<Animator>();
    private bool _isOpen = false;
    [SerializeField]
    private GameObject[] _lukes;

    private void Start()
    {
        foreach (GameObject gameobj in _lukes)
        {
            Animator animator = gameobj.GetComponent<Animator>();
            _animators.Add(animator);
        }
        
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

        foreach (var item in _animators)
        {
            item.SetBool("IsOpen", _isOpen);
        }
        
        AudioManager.Instance.Play("switch");
    }

    public override int GetHp()
    {
        return 0;
    }
}
