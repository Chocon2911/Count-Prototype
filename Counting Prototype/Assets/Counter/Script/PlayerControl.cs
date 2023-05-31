using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private float xBoundary = 23f;
    private float yBoundary = 14f;
    public float xPos;
    public float yPos;
    private float mZCoord;
    private float leftRange;
    private float rightRange;

    public Vector3 pos;
    private Vector3 offset;

    private Rigidbody rb;

    private int ballInBox = 0;
    public int score = 0;

    [SerializeField] Text countText;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    private void Update()
    {
        //limit area that player can move
        /*
        xPos = transform.position.x;
        yPos = transform.position.y;
        if (xPos < -xBoundary)
        {
            xPos = -xBoundary;
        }
        if (yPos < 0.2f)
        {
            yPos = 0.2f;
        }
        if (yPos > yBoundary)
        {
            yPos = yBoundary;
        }
        if (xPos > xBoundary)
        {
            xPos = xBoundary;
        }
        pos = new Vector3(xPos, yPos, transform.position.z);
        transform.position = pos;
        */
        countText.text = "Count: " + score;
    }

    //use mouse to drag player around

    //find the offset between mousePos and playerPos when clicked
    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        rb.isKinematic = true;
        offset = transform.position - GetMouseWorldPos();
    }
    //Make player follow mouse when hold the leftbutton
    private void OnMouseDrag()
    {
        Vector3 goalPosition = GetMouseWorldPos() + offset;
        xPos = goalPosition.x;
        yPos = goalPosition.y;
        if (xPos <= -xBoundary)
        {
            xPos = -xBoundary;
            pos = new Vector3(xPos, yPos, goalPosition.z);
            rb.MovePosition(pos);
        }
        if (yPos <= 0.2f)
        {
            yPos = 0.2f;
            pos = new Vector3(xPos, yPos, goalPosition.z); 
            rb.MovePosition(pos);
        }
        if (yPos >= yBoundary)
        {
            yPos = yBoundary;
            pos = new Vector3(xPos, yPos, goalPosition.z);
            rb.MovePosition(pos);
        }
        if (xPos >= xBoundary)
        {
            xPos = xBoundary; 
            pos = new Vector3(xPos, yPos, goalPosition.z);
            rb.MovePosition(pos);
        }
        else
        {
            rb.MovePosition(goalPosition);
        }

        
    }
    private void OnMouseUp()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }
    //define mouse position
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.useGravity = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ball"))
        {
            ballInBox++;
            other.gameObject.tag = "ballInBox";
            if(ballInBox >= 3)
            {
                DestroyBallInBox();
                score++;
                ballInBox = 0;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ballInBox"))
        {
            ballInBox--;
            other.gameObject.tag = "ball";
        }
    }
    private void DestroyBallInBox()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("ballInBox");

        foreach(GameObject ball in balls)
        {
            Destroy(ball);
        }
    }
}
