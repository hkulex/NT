using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] protected float _Velocity;
    [SerializeField] protected Sprite _SpriteDeath;

    protected SpriteRenderer _SpriteRenderer;
    protected bool _IsDead;

    protected virtual void Awake()
    {
        _SpriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        _IsDead = false;
    }

    protected virtual void Update()
    {

    }

    public virtual void Death()
    {

    }
}