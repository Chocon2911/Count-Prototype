using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Unity.VisualScripting;
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
        //update score text
        countText.text = "Score: " + score;
    }
    private void FixedUpdate()
    {
        if (transform.position.y > 40f)
        {
            transform.position = new Vector3(0, 1, 0);
            rb.velocity = new Vector3(0,0,0);
        }
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
            rb.MovePosition(new Vector3(xPos, yPos, goalPosition.z));
        }
        if (yPos <= 0.2f)
        {
            yPos = 0.2f;
            rb.MovePosition(new Vector3(xPos, yPos, goalPosition.z));
        }
        if (yPos >= yBoundary)
        {
            yPos = yBoundary;
            rb.MovePosition(new Vector3(xPos, yPos, goalPosition.z));
        }
        if (xPos >= xBoundary)
        {
            xPos = xBoundary; 
            rb.MovePosition(new Vector3(xPos, yPos, goalPosition.z));
        }
        if (xPos > -xBoundary && yPos > 0.2f && yPos < yBoundary && xPos < xBoundary)
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
        if (collision.gameObject.CompareTag("wall"))
        {
            rb.velocity = Vector3.zero;
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
