using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] powerUps;

    private float enemySpawn = 2.0f;
    private float powerUpSpawn = 2.0f;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        this.screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    public void StartGameRoutines()
    {
        this.SpawnEnemy();
        this.SpawnPowerUp();
    }

    private void SpawnEnemy()
    {
        StartCoroutine(this.SpawnEnemyRoutine());
    }

    private void SpawnPowerUp()
    {
        StartCoroutine(this.SpawnPowerUpRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(this.enemySpawn);
            Instantiate(this.enemyPrefab, this.SpawnVector(), Quaternion.identity);
        }
    }

    private IEnumerator SpawnPowerUpRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(this.powerUpSpawn);
            Instantiate(this.powerUps[Random.Range(0, this.powerUps.Length)], this.SpawnVector(), Quaternion.identity);
        }
    }

    private Vector3 SpawnVector()
    {
        return new Vector3(Random.Range(-this.screenBounds.x, this.screenBounds.x), this.screenBounds.y, 0);
    }
}
