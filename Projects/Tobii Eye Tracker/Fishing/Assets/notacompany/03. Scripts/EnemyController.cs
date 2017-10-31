using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    [SerializeField] private int _Points;
    [SerializeField] private float _Amplitude;
    [SerializeField] private bool _IsLethal;

    private int _Direction;

    public bool IsLethal { get { return _IsLethal; } }

    override protected void Awake()
    {
        base.Awake();
    }

    override protected void Start()
    {
        base.Start();
    }

    override protected void Update()
    {
        base.Update();

        if (_IsDead)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x, 6), 1f * Time.deltaTime);
            
            if (this.transform.position.y >= 6)
                Destroy(this.gameObject);
        }
        else
        {
            this.transform.position += new Vector3(_Velocity * Time.deltaTime * _Direction, Mathf.Sin(Time.time) * Time.deltaTime * _Amplitude, 0);

            if (this.transform.position.x > 11f || this.transform.position.x < -11f)
                Destroy(this.gameObject);
        }
    }

    public void Initialize(int direction)
    {
        _Direction = direction;
        _SpriteRenderer.flipX = _Direction == -1 ? true : false;
    }

    public override void Death()
    {
        base.Death();

        if (!_IsDead)
        {
            _IsDead = true;

            _SpriteRenderer.sprite = _SpriteDeath;
            _SpriteRenderer.flipY = true;
            _SpriteRenderer.color = new Color(255, 255, 255, 0.5f);

            GameManagement.Instance.EnemyDeath(_Points);
        }
    }
}