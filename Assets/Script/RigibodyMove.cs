using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigibodyMove : MonoBehaviour
{
    private float MoveSpeed = 3f;

    bool Switchs = true;

    GameObject gameObj;

    public GameObject othesr;

    bool DoorSwitch = true;

    void Update()
    {
        //If pressing the w or up key
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            //Move straight ahead at the speed of movespeed
            this.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(Vector3.back * MoveSpeed * Time.deltaTime);
        }
        //If pressing the A or left arrow key
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);

            //this.transform.Rotate(Vector3.left * RotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
            // this.transform.Rotate(Vector3.right * RotateSpeed * Time.deltaTime);
        }

        if (Input.GetMouseButton(0))
        {
            if (Switchs)
            {
                //Ray from camera to click coordinate
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    //Draw out the ray, which can only be seen in the scene view
                    Debug.DrawLine(ray.origin, hitInfo.point);
                    gameObj = hitInfo.collider.gameObject;
                    Debug.Log("click object name is " + gameObj.name);
                    //When the ray collides with the object of boot type, the picking operation is performed
                    if (gameObj.tag == "Finish")
                    {
                        gameObj.transform.parent = this.transform;
                        gameObj.transform.localPosition = new Vector3(0.01709457f, 0.691f, 3.262459f);
                        gameObj.transform.localEulerAngles = Vector3.zero;
                        gameObj.GetComponent<Rigidbody>().isKinematic = true;
                        gameObj.GetComponent<Rigidbody>().useGravity = false;
                        Switchs = false;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!Switchs)
            {
                Debug.Log(gameObj.transform.localEulerAngles);
                gameObj.transform.parent = null;
                gameObj.GetComponent<Rigidbody>().isKinematic = false;
                gameObj.GetComponent<Rigidbody>().useGravity = true;
                //Using transformdirection() method to get a direction
                Vector3 camDirct = transform.TransformDirection(0, 0, 10);
                //Add a forward impulse to the cube
                gameObj.GetComponent<Rigidbody>().AddForce(camDirct, ForceMode.Impulse);
                Switchs = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!DoorSwitch)
            {
                if (!othesr.gameObject.GetComponent<Animator>().GetBool("IsOpen"))
                {
                    othesr.gameObject.GetComponent<Animator>().SetBool("IsOpen", true);
                }
                else
                {
                    othesr.gameObject.GetComponent<Animator>().SetBool("IsOpen", false);
                }
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            DoorSwitch = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            DoorSwitch = true;
        }
    }
}
