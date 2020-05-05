using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _astSpeed = 1.0f;
    private float _astRotate = 55.0f;
    [SerializeField]
    private GameObject _explosion = null;
    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
      _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _astSpeed * Time.deltaTime, 0);
        transform.Rotate(0, 0, _astRotate * -1 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.tag == "Laser")
      {
        Destroy(other.gameObject);
        _spawnManager.StartSpawning();
        Vector3 currentPos = transform.position;
        GameObject explosion = Instantiate(_explosion, currentPos, Quaternion.identity);
        Destroy(this.gameObject, 0.3f);
      }
    }
}
