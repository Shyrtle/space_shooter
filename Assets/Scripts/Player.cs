using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 3.5f;
    private float _speedMultiplyer = 2;
    [SerializeField]
    private GameObject _laser = null;
    [SerializeField]
    private float _fireRate = 0.5f;

    [SerializeField]
    private GameObject _tripleShot = null;

    [SerializeField]
    private GameObject _ShieldEnabled = null;

    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager = null;


    private float laserOffset = 1.05f;

    [SerializeField]
    private bool _isTripleShotActive = false;

    [SerializeField]
    private bool _isSpeedUpActive = false;

    [SerializeField]
    private bool _isShieldActive = false;

    [SerializeField]
    public int _score = 0;

    [SerializeField]
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_spawnManager == null)
        {
          Debug.LogError("The Spawn Manager is null.");
        }
        if (_uiManager == null)
        {
          Debug.LogError("The UI Manager is null.");
        }
        _ShieldEnabled.SetActive(false);
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

      transform.Translate(direction * _playerSpeed * Time.deltaTime);
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
        if (_isShieldActive == true)
        {
          _ShieldEnabled.SetActive(false);
          _isShieldActive = false;
          return;
        }
        else
        {
          _lives -= 1;

          _uiManager.UpdateLives(_lives);

          if (_lives < 1)
          {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            _uiManager.GameOver();
          }
        }

    }

    public void TripleShot()
    {
      _isTripleShotActive = true;
      StartCoroutine("PowerupTimerRoutine");
    }

    public void SpeedUp()
    {
      _isSpeedUpActive = true;
      _playerSpeed *= _speedMultiplyer;
      StartCoroutine("PowerupTimerRoutine");
    }

    public void Shield()
    {
      _isShieldActive = true;
      _ShieldEnabled.SetActive(true);
      StartCoroutine("PowerupTimerRoutine");
    }

    IEnumerator PowerupTimerRoutine()
    {
      while (_isTripleShotActive == true)
      {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
      }

      while (_isSpeedUpActive == true)
      {
        yield return new WaitForSeconds(6.0f);
        _isSpeedUpActive = false;
        _playerSpeed /= _speedMultiplyer;
      }

      while (_isShieldActive == true)
      {
        yield return new WaitForSeconds(8.0f);
        _isShieldActive = false;
        _ShieldEnabled.SetActive(false);
      }
    }

    public void Score(int points)
    {
      _score += points;
      _uiManager.Scoring(_score);
    }
}
