using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab = null;
    [SerializeField]
    private GameObject _enemyContainer = null;
    [SerializeField]
    private GameObject[] powerups = null;


    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartSpawning()
    {
      StartCoroutine(SpawnRoutine());
      StartCoroutine(SpawnPowerupsRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnRoutine()
    {
      yield return new WaitForSeconds(3.0f);
      while (_stopSpawning == false)
      {
        Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
        GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
        yield return new WaitForSeconds(5.0f);
      }
    }
    IEnumerator SpawnPowerupsRoutine()
    {
      yield return new WaitForSeconds(3.0f);
      while (_stopSpawning == false)
      {
        Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
        int randomPowerup = Random.Range(0, 3);
        GameObject newPowerup = Instantiate(powerups[randomPowerup], posToSpawn, Quaternion.identity);
        yield return new WaitForSeconds(10.0f);
      }
    }

    public void OnPlayerDeath()
    {
      _stopSpawning = true;
    }
}
