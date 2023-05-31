using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CameraRotatFollow : MonoBehaviour
{
    private float length = 1f;
    private float width;
    private float rotateAngle;

    [SerializeField] private GameObject target;
    void Start()
    {
        
    }
    private void Update()
    {
        length = -target.transform.position.z + transform.position.z;
        width = target.transform.position.x - transform.position.x;

        rotateAngle = Mathf.Atan2(length, width) * Mathf.Rad2Deg;
    }
    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, 90 + rotateAngle, transform.rotation.z);
    }
}
