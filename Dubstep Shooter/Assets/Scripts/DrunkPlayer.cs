using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class DrunkPlayer : MonoBehaviour
{
    [SerializeField] float _speed = 20f;
    [SerializeField] private float maxRotationZ = 20;
    [SerializeField] private float minRotationZ = -40;
    
    private void Start()
    {
        StartCoroutine(ShakeHand());
    }

    private IEnumerator ShakeHand()
    {
        Quaternion highHandRotation;
        Quaternion lowHandRotation;
        Quaternion toRotation;
        
        float highHandRotationZ = maxRotationZ;
        float lowHandRotationZ = minRotationZ;
        int secondsHandRotating = 3;
        bool isHandGoesUp = false;
        bool isHandGoesDown = false;
        float timeStart = Time.time;
        
        
        highHandRotation = Quaternion.Euler(0f, 0f, highHandRotationZ);
        lowHandRotation = Quaternion.Euler(0f, 0f, lowHandRotationZ);
        toRotation = highHandRotation;
        isHandGoesUp = true;
        
        while (true)
        {
            float passedTime = Time.time - timeStart;
            float step = _speed * Time.deltaTime;
            Quaternion currentRotation = transform.rotation;
            // float currentRotationZFromInspector = UnityEditor.TransformUtils.GetInspectorRotation(transform).z;
            
            if (isHandGoesUp && passedTime >= secondsHandRotating)
            {
                toRotation = lowHandRotation;
                isHandGoesDown = true;
                isHandGoesUp = false;
                timeStart = Time.time;
            }
            else if (isHandGoesDown && passedTime >= secondsHandRotating)
            {
                toRotation = highHandRotation;
                isHandGoesUp = true;
                isHandGoesDown = false;
                timeStart = Time.time;
            }

            transform.rotation = Quaternion.RotateTowards(currentRotation, toRotation, step);
            
            yield return new WaitForSeconds(0.01f);
        }
    }
}
