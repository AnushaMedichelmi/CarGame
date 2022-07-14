using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    public float bulletSpeed;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 newPosition = transform.position + transform.right * speed * Time.deltaTime;
        // rb.velocity = new Vector3(bulletSpeed * Time.deltaTime, 0f, 0f);
        // rb.velocity=transform.right*bulletSpeed*Time.deltaTime;
       //rb.AddForce(transform.right * bulletSpeed * Time.deltaTime);
       transform.Translate(-CarMovement.Instance.direction * bulletSpeed*Time.deltaTime);

    }
    private void OnBecameInvisible()
    {
        PoolManager.Instance.Recycle("Bullet", this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            PoolManager.Instance.Recycle("Bullet", this.gameObject);
            PoolManager.Instance.Recycle("Enemy", collision.gameObject);
            GameManager.Instance.AddingScore(10);
        }

        if (collision.gameObject.tag == "HardEnemy")
        {
            PoolManager.Instance.Recycle("Bullet", this.gameObject);
            PoolManager.Instance.Recycle("HardEnemy", collision.gameObject);
            GameManager.Instance.AddingScore(30);

        }
    }

}
