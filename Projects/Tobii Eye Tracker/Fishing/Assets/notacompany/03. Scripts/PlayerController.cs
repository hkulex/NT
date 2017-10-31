using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;

public class PlayerController : BaseController
{
    [SerializeField] private float _Margin;

    private Vector2 _PreviousPosition;

    private bool _IsInvincible;

    override protected void Start()
    {
        base.Start();

        _IsInvincible = false;
    }

    override protected void Update()
    {
        if (TobiiAPI.GetGazePoint().IsValid)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(TobiiAPI.GetGazePoint().Screen);

            if (Vector2.Distance(_PreviousPosition, position) > _Margin)
            {
                if (position.y < -3.5f)
                    position.y = -3.5f;

                this.transform.position = Vector2.MoveTowards(this.transform.position, position, _Velocity * Time.deltaTime);

                //flip?
                if (this.transform.position.x > _PreviousPosition.x)
                    _SpriteRenderer.flipX = false;
                else
                    _SpriteRenderer.flipX = true;
            }

            _PreviousPosition = this.transform.position;
        }
    }


    private IEnumerator TakeDamage()
    {
        _IsInvincible = true;

        for (int i = 0; i < 15; i++)
        {
            _SpriteRenderer.color = new Color(255, 255, 255, 0f);

            yield return new WaitForSeconds(0.1f);

            _SpriteRenderer.color = new Color(255, 255, 255, 1f);

            yield return new WaitForSeconds(0.1f);
        }

        _IsInvincible = false;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            EnemyController ec = other.GetComponent<EnemyController>();

            if (!ec)
                return;

            if (!ec.IsLethal)
            {
                ec.Death();
            }
            else
            {
                GameManagement.Instance.PlayerDeath();

                if (!_IsInvincible)
                    StartCoroutine(TakeDamage());
            }
        }
    }
}