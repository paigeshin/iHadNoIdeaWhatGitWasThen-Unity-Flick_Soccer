using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public GameObject ballPrefab;


    [SerializeField]  //When we add 'SerializedField', even if it's not public, still we are able to see it in Inspector Panel
    float ballForce;

    GameObject ballInstance;

    //Add Force to the ball according to our mouse swipe
    Vector3 mouseStart;
    Vector3 mouseEnd;
    float minDragDistance = 1;  //minimum Swipe Distance.
    float zDepth = 25f;

    // Start is called before the first frame update
    void Start()
    {
        CreateBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStart = Input.mousePosition;  //Store the initial position of the mouse.
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseEnd = Input.mousePosition;    //Whenever we lift out finger, store the position. That is 'mouseEnd' value.

            if(Vector3.Distance(mouseEnd, mouseStart) > minDragDistance)
            {
                //Vector3.Distance(p1, p2)  it returns the value of a distance based on the calculation of p1, p2 parameters

                //if the condition is satisfied, we throw the ball.
                Vector3 hitPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDepth);

                hitPos = Camera.main.ScreenToWorldPoint(hitPos);  //Screen Point to World Point. (In 3D game, it's necessary)

                ballInstance.transform.LookAt(hitPos); //rotate the ball.

                //add the force to the ball.
                ballInstance.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * ballForce, ForceMode.Impulse); //Adding the force to the ball.
                                                                                                                         //Get Access to RigidBody attached to the Ball so that we can add force to the ball.
            }

        }

    }

    //Simple Function that will create the ball whenever we start the game.
    void CreateBall()
    {
        ballInstance = Instantiate(ballPrefab, ballPrefab.transform.position, Quaternion.identity) as GameObject;
        //Generanlly object.transform.position means 'current position'
    }
}
