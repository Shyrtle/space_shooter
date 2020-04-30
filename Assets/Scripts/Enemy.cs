using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4f;

    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
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
        Destroy(this.gameObject);
      }

      if (other.tag == "Laser")
      {
        Destroy(other.gameObject);
        if (_player != null)
        {
          _player.Score(10);
        }
        Destroy(this.gameObject);
      }

    }

}
