using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static GameManagement Instance;

    [SerializeField] private TextMeshProUGUI _TextScore;

    [SerializeField] private List<EnemyController> _EnemyList;
    [SerializeField] private List<InterfaceLife> _LifeList;

    private int _Score;
    private int _Lives;

    private void Start()
    {
        _Score = 0;
        _Lives = 3;

        _TextScore.text = "" + _Score;

        if (!Instance)
            Instance = this;

        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        Vector2 position = new Vector2();
        int random = Random.Range(0, 2);
        int direction = 1;

        if (random == 0)
        {
            position.x = 10;
            direction = -1;
        }
        else
            position.x = -10;

        position.y = Random.Range(-3f, 2.5f);

        EnemyController ec = Instantiate(_EnemyList[Random.Range(0, _EnemyList.Count)], position, new Quaternion());

        ec.Initialize(direction);

        yield return new WaitForSeconds(1f);

        StartCoroutine(SpawnEnemy());
    }

    public void EnemyDeath(int points)
    {
        if (_Lives == 0)
            return;

        _Score += points;

        _TextScore.text = "" + _Score;
    }

    public void PlayerDeath()
    {
        if (_Lives == 0)
            return;

        _Lives--;

        _LifeList[_Lives].Death();

        if (_Lives == 0)
            Debug.Log("Game over");
    }
}