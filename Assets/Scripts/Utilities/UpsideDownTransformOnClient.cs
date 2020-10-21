using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// For camera, score panel on client during network game
/// </summary>
public class UpsideDownTransformOnClient : MonoBehaviour
{
	private void SetUpsideDownCameraOrientation()
	{
		transform.eulerAngles = new Vector3(0f, 0f, 180f);
	}

	private void OnEnable()
	{
		Events.startNetworkGameAsClientEvent += SetUpsideDownCameraOrientation;
	}

	private void OnDisable()
	{
		Events.startNetworkGameAsClientEvent -= SetUpsideDownCameraOrientation;
	}
}
