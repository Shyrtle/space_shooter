using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    private float _powerupSpeed = 4f;
    [SerializeField]
    private int _powerupID = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.down * _powerupSpeed * Time.deltaTime, 0);

        if (transform.position.y < -6.1f )
        {
          Destroy(this.gameObject);
        }

    }


    void OnTriggerEnter2D(Collider2D other)
    {
      if (other.tag == "Player")
      {
        Player player = other.transform.GetComponent<Player>();
        if (player != null)
        {
          switch(_powerupID)
          {
            case 0:
              player.TripleShot();
              break;
            case 1:
              player.SpeedUp();
              break;
            case 2:
              player.Shield();
              break;
          }

        }
        Destroy(this.gameObject);
      }
    }

}
