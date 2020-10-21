using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformRotator : MonoBehaviour
{
	[SerializeField] private Vector3 rotationSpeed;


    void Update()
    {
		transform.Rotate(rotationSpeed);
    }
}
