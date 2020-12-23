using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CameraController : MonoBehaviour
{

    private float AngularMoveSpeed= 0.5f;
    private float RadialMoveSpeed = 0.5f;
    private float x, y, z;
    //public float radius, theta, phi;
    private float smooth = 10.0f;
    private OVRCameraRig cam;
    private Text CoordText;
    private float mm;
    private bool rz = false;
    

    // Is true when the user wants to rotate the camera
    bool ballEnabled = true;

    // The mouse cursor's position during the last frame
    Vector3 last = new Vector3();

    // The target that the camera looks at
    Vector3 target = new Vector3(0, 0, 0);

    // The spherical coordinates
    Vector3 sc = new Vector3();

    public object LightGameObject { get; private set; }

    void Start()
    {
        Vector3 init_value;
        cam = gameObject.GetComponent<OVRCameraRig>();
        
        // cam.fieldOfView = 45;
        //cam.enabled = true;

        init_value = new Vector3( 10.0f, 0.0f, 0.0f);
        RadialMoveSpeed = 0.5f; //MainMenuButtonEvents.RadialSpeedValue;
        AngularMoveSpeed = 0.5f; //MainMenuButtonEvents.AngularSpeedValue;

        Debug.Log("Init: "+ getCartesianCoordinates(init_value));

        cam.transform.position = getCartesianCoordinates(init_value);
        cam.transform.LookAt(target);

        Debug.Log("cam: " + cam.transform.position);

        sc = getSphericalCoordinates(cam.transform.position);

        CoordText = GameObject.Find("Coords").GetComponent<Text>();
        writeCoords(getSphericalCoordinates(init_value));
    }

    void Update()
    {
        float moveTheta = Input.GetAxis("Horizontal");
        float movePhi = Input.GetAxis("Vertical");

        Debug.Log("Theta: " + moveTheta);
        Debug.Log("Phi: " + movePhi);

        RadialMoveSpeed = 0.5f; // MainMenuButtonEvents.RadialSpeedValue;
        AngularMoveSpeed = 0.5f; // MainMenuButtonEvents.AngularSpeedValue;

        if (OVRInput.GetDown(OVRInput.Button.Back))
        {
            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
            //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu"));
        }

        if (OVRInput.GetDown(OVRInput.Button.One)) { rz = !rz; }

        if (ballEnabled)
        {
            // Get the deltas that describe how much the mouse cursor got moved between frames
            float dp = (movePhi) * AngularMoveSpeed;
            float dt = (moveTheta) * AngularMoveSpeed;
            float dr = (moveTheta) * RadialMoveSpeed;




            // Rotate the camera left and right
            sc.y += dp * Time.deltaTime;

            if (!rz) {

                // Rotate the camera up and down
                // Prevent the camera from turning upside down (1.5f = approx. Pi / 2)
                sc.z = Mathf.Clamp(sc.z + dt * Time.deltaTime, -1.5f, 1.5f);

                // Calculate the cartesian coordinates for unity
                cam.transform.position = getCartesianCoordinates(sc) + target;

                // Make the camera look at the target
                cam.transform.LookAt(target);
                

                //Debug.Log("sphere: " + sc);
            }
            else {
                // Rotate the camera up and down
                // Prevent the camera from turning upside down (1.5f = approx. Pi / 2)
                sc.x = Mathf.Clamp(sc.x + dr * Time.deltaTime, 0.1f, 15f);

                // Calculate the cartesian coordinates for unity
                cam.transform.position = getCartesianCoordinates(sc) + target;

                // Make the camera look at the target
                cam.transform.LookAt(target);
                
            }

        }
        writeCoords(sc);
    }


    Vector3 getSphericalCoordinates(Vector3 cartesian)
    {
        float r = Mathf.Sqrt(
            Mathf.Pow(cartesian.x, 2) +
            Mathf.Pow(cartesian.y, 2) +
            Mathf.Pow(cartesian.z, 2)
        );

        float phi = Mathf.Atan2(cartesian.z, cartesian.x);
        float theta = Mathf.Acos(cartesian.y / r);

        if (cartesian.x < 0)
            phi += Mathf.PI;

        return new Vector3(r, phi, theta);
    }

    Vector3 getCartesianCoordinates(Vector3 spherical)
    {
        Vector3 ret = new Vector3();

        ret.x = spherical.x * Mathf.Cos(spherical.z) * Mathf.Cos(spherical.y);
        ret.y = spherical.x * Mathf.Sin(spherical.z);
        ret.z = spherical.x * Mathf.Cos(spherical.z) * Mathf.Sin(spherical.y);

        return ret;
    }

    void writeCoords(Vector3 spherical)
    {
        float theta, phi;
        
        phi = sc.y * (180.0f / Mathf.PI);
        theta = sc.z * (180.0f / Mathf.PI);
        CoordText.text = "("+spherical.x.ToString("F1")+", "+phi.ToString("F1") + ", " + theta.ToString("F1")+")";
         
    }
}

