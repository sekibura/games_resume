using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverTimerScript : Attackable
{
    [SerializeField]
    private Sprite _onSprite;
    private Sprite _offSprite;
    private SpriteRenderer _spriteRenderer;
    private List<Animator> _animators = new List<Animator>();
    private bool _isOpen = false;
    [SerializeField]
    private GameObject[] _lukes;

    [SerializeField]
    private float _time = 0f;

    private IEnumerator _coroutine;

    [SerializeField]
    private UnityEvent _events;
    [SerializeField]
    private bool _doOnce = true;
    [SerializeField]
    private bool _done = false;

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
        Switch();

    
    }

    public override int GetHp()
    {
        return 0;
    }

    private void Switch()
    {
        if (_isOpen)
        {
            InvokeAct();
            _spriteRenderer.sprite = _offSprite;
            _isOpen = false;
            //StopCoroutine(_coroutine);
        }
        else
        {
            InvokeAct();
            _spriteRenderer.sprite = _onSprite;
            _isOpen = true;

            _coroutine = Timer();
            StartCoroutine(_coroutine);
        }

        foreach (var item in _animators)
        {
            item.SetBool("IsOpen", _isOpen);
        }

        AudioManager.Instance.Play("switch");
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(_time);
        if(_isOpen)
            Switch();
    }

    private void InvokeAct()
    {
        if (_events != null && !(_done && _doOnce))
        {
            Debug.Log("Invoked");
            _events?.Invoke();
        }
        _done = true;
    }
}
