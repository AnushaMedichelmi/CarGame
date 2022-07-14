using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class EnemyMovement : MonoBehaviour
{

    public int enemySpeed;
    Vector3 direction;
   // public ParticleSystem carParticle;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(-enemySpeed * Time.deltaTime, 0, 0);
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        //transform.position+=new Vector3 (enemySpeed*Time.deltaTime, 0, 0);
        transform.position += direction;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.tag == "Point1")
        {
            Debug.Log("hit");
          
            transform.rotation = new Quaternion(0, 0, 90, 90);
            Debug.Log("position");
            direction = new Vector3(-enemySpeed * Time.deltaTime,0, 0);

        }

        if (collision.gameObject.tag == "Point2")
        {

            transform.rotation = new Quaternion(0, 0, 90, 0);
            Debug.Log("position");
            direction = new Vector3(0,enemySpeed * Time.deltaTime, 0);
        }

        if (collision.gameObject.tag == "Point3")
        {

            transform.rotation = new Quaternion(0, 0, -90, 90);
            Debug.Log("position");
            direction = new Vector3( enemySpeed * Time.deltaTime,0, 0);

        }

        if (collision.gameObject.tag == "Point4")
        {

            transform.rotation = new Quaternion(0, 0, 0, 0);
            Debug.Log("position");
            direction = new Vector3(0,-enemySpeed * Time.deltaTime, 0);

        }

       /* if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("collided");
            // Destroy(gameObject);
            //gameObject.SetActive(false);
            //collision.gameObject.SetActive(false);
            PoolManager.Instance.Recycle("Bullet", this.gameObject);
            PoolManager.Instance.Recycle("Enemy", collision.gameObject);
            PoolManager.Instance.Recycle("HardEnemy", collision.gameObject);

            //Instantiate(carParticle,transform.position,Quaternion.identity);
            GameManager.Instance.AddingScore();
           // score = score+10;
            //scoreText.text = "Score:" + score;
            //GameManager.Instance.GameOver();
        }*/


    }

}
