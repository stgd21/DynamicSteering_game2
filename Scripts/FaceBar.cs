using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceBar : MonoBehaviour
{
    public float rotationValue;
     
    void FixedUpdate()
    {
        transform.eulerAngles += new Vector3(0, rotationValue, 0);
    }
}
