using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<GameObject> EnemyRoster = new List<GameObject>();
    public int MaxEnemiesOnScreen = 5;
    public float ConsecutiveSpawnsDelay = 5.0f;

    private List<GameObject> _activeEnemies = new List<GameObject>();
    private float lastSpawn = 0.0f;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        _activeEnemies.RemoveAll(enemy => enemy == null);

        if (_activeEnemies.Count < MaxEnemiesOnScreen
            && EnemyRoster.Count > 0
            && Time.time >= (lastSpawn + ConsecutiveSpawnsDelay))
        {
            lastSpawn = Time.time;

            int x = UnityEngine.Random.Range(0, 5);
            int y = UnityEngine.Random.Range(0, 5);

            var randomEnemyIndex = UnityEngine.Random.Range(0, EnemyRoster.Count);

            var enemy = Instantiate(EnemyRoster[randomEnemyIndex], new Vector3(x, y, 20), Camera.main.transform.rotation);
            _activeEnemies.Add(enemy);

            Destroy(enemy, 30.0f);
        }
    }
}