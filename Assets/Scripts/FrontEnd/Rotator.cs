using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var rectTransform = GetComponent<RectTransform>();
        var rotation = rectTransform.localRotation;

        rotation.z += rotateSpeed * Time.deltaTime;
        while (rotation.z > 1.0f)
        {
            rotation.z -= Mathf.PI;
        }
        while (rotation.z < -1.0f)
        {
            rotation.z += Mathf.PI;
        }

        rectTransform.localRotation = rotation;
    }
}
