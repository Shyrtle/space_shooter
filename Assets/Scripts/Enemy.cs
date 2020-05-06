using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4f;

    private Animator _anim;

    private Player _player;

    private AudioSource _audioSource = null;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        if(_player == null)
        {
          Debug.LogError("Player is null.");
        }
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
          Debug.LogError("The Animator is Null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, _enemySpeed * -1 * Time.deltaTime, 0);

        if (transform.position.y < -4.5f)
        {
          transform.Translate(Random.Range(-8f, 8f), 10f, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.tag == "Player")
      {
        if (_player != null)
        {
          _player.Damage();
        }
        _anim.SetTrigger("OnEnemyDeath");
        _enemySpeed = 0;
        Destroy(transform.GetComponent<BoxCollider2D>());
        _audioSource.Play();
        Destroy(this.gameObject, 2.8f);

      }

      if (other.tag == "Laser")
      {
        Destroy(other.gameObject);
        if (_player != null)
        {
          _player.Score(10);
        }
        _anim.SetTrigger("OnEnemyDeath");
        _enemySpeed = 0;
        Destroy(transform.GetComponent<BoxCollider2D>());
        _audioSource.Play();
        Destroy(this.gameObject, 2.8f);

      }

    }

}
