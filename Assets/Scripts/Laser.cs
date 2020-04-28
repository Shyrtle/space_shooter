using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
  [SerializeField]
  private float _laserSpeed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,_laserSpeed * Time.deltaTime,0);

        if (transform.position.y > 10)
        {
          Destroy(this.gameObject);
        }
    }
}
