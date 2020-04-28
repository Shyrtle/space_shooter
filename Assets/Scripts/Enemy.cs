using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4f;
    // Start is called before the first frame update
    void Start()
    {

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
        Player player = other.transform.GetComponent<Player>();
        if (player != null)
        {
          player.Damage();
        }
        Destroy(this.gameObject);
      }

      if (other.tag == "Laser")
      {
        Destroy(other.gameObject);
        Destroy(this.gameObject);
      }

    }

}
