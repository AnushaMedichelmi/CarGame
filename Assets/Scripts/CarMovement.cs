using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class CarMovement : MonoBehaviour
{

    //public Vector3[] waypoints;
    // public Vector3[] secondPoints;
    //public Vector3[] thirdPoints;
    //public Vector3[] fourthPoints;
    //float duration = 5f;

    public int speed;
   public Vector3 direction;
    Vector3 offset;
    public bool isRoad1 = false;
    public Button switchButton;
    Rigidbody2D rb;
    public GameObject carParticle;
   
   

    private static CarMovement instance;
    public static CarMovement Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CarMovement>();
                if (instance == null)
                {
                    GameObject container = new GameObject("Car");
                    instance = container.AddComponent<CarMovement>();
                }
            }
            return instance;
        }
    }


    public enum Lane
    {
        Left,
        Right,
        Up,
        Down
    }
    Lane lane1;

    // Start is called before the first frame update
    void Start()
    {

        direction = new Vector3(speed * Time.deltaTime, 0, 0);
        lane1 = Lane.Up;
        rb = GetComponent<Rigidbody2D>();
        /*Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOPath(waypoints, duration, PathType.Linear, PathMode.Full3D, 10, Color.black).SetEase(Ease.InSine)).
       Append(transform.DORotate(new Vector3(0, 0, 0), 0)).
       Append(transform.DOPath(secondPoints, duration, PathType.Linear, PathMode.Full3D, 10, Color.black).SetEase(Ease.InSine)).
       Append(transform.DORotate(new Vector3(0, 0, 90), 0)).
       Append(transform.DOPath(thirdPoints, duration, PathType.Linear, PathMode.Full3D, 10, Color.black).SetEase(Ease.InSine)).
       Append(transform.DORotate(new Vector3(0, 0, 0), 0)).
       Append(transform.DOPath(fourthPoints, duration, PathType.Linear, PathMode.Full3D, 10, Color.black).SetEase(Ease.InSine)).
       Append(transform.DORotate(new Vector3(0, 0, 90), 0));  */
    }


    // Update is called once per frame

    public void FixedUpdate()
    {
        transform.position += direction;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.tag == "Point1")
        {
            Debug.Log("hit");
            lane1 = Lane.Right;
            transform.rotation = new Quaternion(0, 0, 0, 0);
            Debug.Log("position");
            direction = new Vector3(0, -speed * Time.deltaTime, 0);

        }

        if (collision.gameObject.tag == "Point2")
        {
            lane1 = Lane.Down;
            transform.rotation = new Quaternion(0, 0, -90, 90);
            Debug.Log("position");
            direction = new Vector3(-speed * Time.deltaTime, 0, 0);
        }

        if (collision.gameObject.tag == "Point3")
        {
            lane1 = Lane.Left;
            transform.rotation = new Quaternion(0, 0, 180, 0);
            Debug.Log("position");
            direction = new Vector3(0, speed * Time.deltaTime, 0);

        }

        if (collision.gameObject.tag == "Point4")
        {
            lane1 = Lane.Up;
            transform.rotation = new Quaternion(0, 0, 90, 90);
            Debug.Log("position");
            direction = new Vector3(speed * Time.deltaTime, 0, 0);

        }

            if (collision.gameObject.tag == "Corner")
            {
                switchButton.interactable = true;
            } 
    }

    public void SwitchingRoad()
    {
        if (!isRoad1)
        {
            isRoad1 = true;
            
            switch (lane1)
            {
                case Lane.Left: offset = new Vector3(25f, 0f, 0f);
                    break;
                case Lane.Right: offset = new Vector3(-25f, 0f, 0f);
                    break;
                case Lane.Up:
                    offset = new Vector3(0f, -28f, 0f);
                    break;
                case Lane.Down:
                    offset = new Vector3(0f, 28f, 0f);
                    break;
                default:
                    break;
            }
            this.transform.position = this.transform.position+offset;
        }
        else
        {
            isRoad1 = false;
            switch (lane1)
            {
                case Lane.Left:
                    offset = new Vector3(-25f, 0f, 0f);
                    break;
                case Lane.Right:
                    offset = new Vector3(25f, 0f, 0f);
                    break;
                case Lane.Up:
                    offset = new Vector3(0f, 28f, 0f);
                    break;
                case Lane.Down:
                    offset = new Vector3(0f, -28f, 0f);
                    break;
                default:
                    break;
            }
            this.transform.position = this.transform.position + offset;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Corner")
        {
            switchButton.interactable=false;
        }

        if (collision.gameObject.tag == "Enemy")
        {

            GameObject temp = Instantiate(carParticle, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(temp, 1f);
            GameManager.Instance.MaxLives(2);
        }

        if (collision.gameObject.tag == "HardEnemy")
        {
            Destroy(collision.gameObject);
            GameObject temp = Instantiate(carParticle, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(temp, 1f);
            GameManager.Instance.MaxLives(10);
        }
    }
}
    
    

