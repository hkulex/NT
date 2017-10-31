using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

    [SerializeField]
    private int moveSpeed = 20;

    private int Timer = 2;
    private float currentTime = 0;
    //private bool isMoving = false;

    private Rigidbody theRigidbody;
    private Vector3 moveDirection = new Vector3(0, 0, 0);
    private Vector3 upwards = new Vector3(0, 0, 0);
    private Vector3 downwards = new Vector3(0, 0, 0);
    private Vector3 right = new Vector3(0, 0, 0);
    private Vector3 left = new Vector3(0, 0, 0);
    bool top = false;
    bool side = false;

    void Start() {
        theRigidbody = this.gameObject.GetComponent<Rigidbody>();
        this.gameObject.transform.position = new Vector3(-5, -1, 0);
    }

    void Update() {
        moveDirection = MoveDir();

        if (currentTime <= Timer) {
            currentTime = currentTime + Time.deltaTime;
            
        }
        else {
            moveDirection = MoveDir();//new Vector3(Random.Range(-1,1), 1, 0f);
            Move();
            currentTime = 0;
        }
    }

    void Move() {
        Debug.Log("move");
        //Debug.Log(moveDirection);
        theRigidbody.velocity = moveDirection;
    }

    Vector3 MoveDir() {
        Vector3 tempDir;

        if (this.transform.position.y >= 3) {
            Debug.Log("Down");
            tempDir = SetDownwards();
        }
        else if (this.transform.position.y <= 0) {
            Debug.Log("up");
            tempDir = SetUpwards();
        }
        else if (this.transform.position.x <= 0) {
            tempDir = SetRight();
        }
        else if (this.transform.position.x >= 3) {
            tempDir = SetLeft();
        }
        else {
            tempDir = moveDirection;
        }
        return tempDir;
    }

    Vector3 SetLeft() {
        if (this.transform.position.y <= 0) {
            left = new Vector3(Random.Range(-1, -3), Random.Range(1, 3), 0f);
        }
        if (this.transform.position.y >= 3) {
            left = new Vector3(Random.Range(-3, -1), Random.Range(-3, -1), 0f);
        }
        else {
            left = new Vector3(Random.Range(-3, -1), Random.Range(-3, 3), 0f);
        }
        return left;
    }

    Vector3 SetRight() {
        if (this.transform.position.y <= 0) {
            right = new Vector3(Random.Range(1, 3), Random.Range(0, 3), 0f);
        }
        if (this.transform.position.y >= 3) {
            right = new Vector3(Random.Range(1,3), Random.Range(-3, -1), 0f);
        }
        else {
            right = new Vector3(Random.Range(1, 3), Random.Range(-3, 3), 0f);
        }
        return right;
    }

    Vector3 SetDownwards() {
        if (this.transform.position.x <= 0) {
            downwards = new Vector3(Random.Range(1, 3), Random.Range(-3, -1), 0f);
        }
        if(this.transform.position.x >= 3) {
            downwards = new Vector3(Random.Range(-3, -1), Random.Range(-3, -1), 0f);
        
        }else {
            downwards = new Vector3(Random.Range(-3, 3), Random.Range(-3, -1), 0f);
        }

        return downwards;
    }
    Vector3 SetUpwards() {
        if (this.transform.position.x <= 0) {
            upwards = new Vector3(Random.Range(1, 3), Random.Range(3, 1), 0f);
        }
        if (this.transform.position.x >= 3) {
            upwards = new Vector3(Random.Range(-3, -1), Random.Range(3, 1), 0f);
        }else {
            upwards = new Vector3(Random.Range(-3, 3), Random.Range(3, 1), 0f);
        }
        return upwards;
    }
}
