using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TargetChange : MonoBehaviour
{

    private GameObject[] rb;
    private int numObjs = 3;
    private int index = 0;
    private Text NameText;
    private string[] targetName;


    void Start()
    {
        rb = new GameObject[numObjs];
        rb[0] = GameObject.Find("Pleiades_only");
        rb[1] = GameObject.Find("cylinder_only");
        rb[2] = GameObject.Find("DS_keyhole");
        targetName = new string[numObjs];
        targetName[0] = "Pleiades";
        targetName[1] = "NIF Cylinder";
        targetName[2] = "Double Shell Keyhole";

                
        rb[0].SetActive(true);
        for (int i = 1; i < numObjs; i++)
        {
            rb[i].SetActive(false);
        }

        NameText = GameObject.Find("TargetName").GetComponent<Text>();
        NameText.text = targetName[0];
        Debug.Log("NT: " + NameText);

    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            rb[index].SetActive(false);
            if (index == numObjs - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            rb[index].SetActive(true);
            NameText.text = targetName[index];
        }

        
    }
}


