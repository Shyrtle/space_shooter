using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 3.5f;
    [SerializeField]
    private GameObject _laser = null;
    [SerializeField]
    private float _fireRate = 0.5f;

    [SerializeField]
    private GameObject _tripleShot = null;

    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager = null;


    private float laserOffset = 1.05f;

    [SerializeField]
    private bool _isTripleShotActive = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
          Debug.LogError("The Spawn Manager is null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
      CalculateMovement();

      if (Input.GetKeyDown("space") && Time.time > _canFire)
      {
        FireLaser();
      }

    }

    void CalculateMovement()
    {
      float horizontalInput = Input.GetAxis("Horizontal");
      float verticalInput = Input.GetAxis("Vertical");

      Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
      transform.Translate(direction * _playerSpeed *Time.deltaTime);

      transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0),0);

      if (transform.position.x > 11)
      {
        transform.position = new Vector3(-11, transform.position.y, 0);
      }
      else if (transform.position.x < -11)
      {
        transform.position = new Vector3(11, transform.position.y, 0);
      }
    }

    void FireLaser()
    {
      _canFire = Time.time + _fireRate;

      if (_isTripleShotActive){
          Instantiate(_tripleShot, new Vector3(transform.position.x, transform.position.y + laserOffset, 0), Quaternion.identity);
      }
      else {
          Instantiate(_laser, new Vector3(transform.position.x, transform.position.y + laserOffset, 0), Quaternion.identity);
      }

    }

    public void Damage()
    {
        _lives -= 1;

        if (_lives < 1)
        {
          _spawnManager.OnPlayerDeath();
          Destroy(this.gameObject);
        }
    }

    public void TripleShot()
    {
      _isTripleShotActive = true;
      StartCoroutine("TripleShotTimerRoutine");
    }

    public void SpeedUp()
    {
      isSpeedUpActive = true;
      StartCoroutine("")
    }

    IEnumerator TripleShotTimerRoutine()
    {
      while (_isTripleShotActive == true)
      {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
      }
    }
}
