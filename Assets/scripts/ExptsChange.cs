using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExptsChange : MonoBehaviour
{

    private GameObject[] rb;
    private int numObjs = 3;
    private int index = 0;


    void Start()
    {
        rb = new GameObject[numObjs];
        rb[0] = GameObject.Find("40_only");
        rb[1] = GameObject.Find("60_only");
        rb[2] = GameObject.Find("80_only");
        rb[0].SetActive(true);
        for (int i = 1; i < numObjs; i++)
        {
            rb[i].SetActive(false);
        }


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
        }

    }
}