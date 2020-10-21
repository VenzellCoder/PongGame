using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class PlayerInput : MonoBehaviour
{
	static public Action phoneOwnerRightInput;
	static public Action phoneOwnerLeftInput;
	static public Action opponentRightInput;
	static public Action opponentLeftInput;


    void Update()
    {
#if (UNITY_EDITOR || UNITY_STANDALONE)
		ProcessKeyboardControll();
#else
		ProcessTouchControll();
#endif
	}

	public static void ResetAllActionSubscriptions()
	{
		phoneOwnerRightInput = null;
		phoneOwnerLeftInput = null;
		opponentRightInput = null;
		opponentLeftInput = null;
	}

	private void ProcessKeyboardControll()
	{
		if (Input.GetKey(KeyCode.A))
		{
			phoneOwnerLeftInput?.Invoke();
		}
		else if (Input.GetKey(KeyCode.D))
		{
			phoneOwnerRightInput?.Invoke();
		}

		if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.LeftArrow))
		{
			opponentLeftInput?.Invoke();
		}
		else if (Input.GetKey(KeyCode.L) || Input.GetKey(KeyCode.RightArrow))
		{
			opponentRightInput?.Invoke();
		}
	}

	private void ProcessTouchControll()
	{
		for (int i = 0; i < Input.touchCount; i++)
		{
			if (IsTouchInLeftBottomScreenQuarter(Input.touches[i].position))
			{
				phoneOwnerLeftInput?.Invoke();
			}
			else if (IsTouchInRightBottomScreenQuarter(Input.touches[i].position))
			{
				phoneOwnerRightInput?.Invoke();
			}

			if (IsTouchInLeftTopScreenQuarter(Input.touches[i].position))
			{
				opponentLeftInput?.Invoke();
			}
			else if (IsTouchInRightTopScreenQuarter(Input.touches[i].position))
			{
				opponentRightInput?.Invoke(); 
			}
		}
	}

	private bool IsTouchInLeftBottomScreenQuarter(Vector2 pos)
	{
		return (pos.x < Screen.width / 2) && (pos.y < Screen.height / 2);
	}

	private bool IsTouchInRightBottomScreenQuarter(Vector2 pos)
	{
		return (pos.x > Screen.width / 2) && (pos.y < Screen.height / 2);
	}

	private bool IsTouchInLeftTopScreenQuarter(Vector2 pos)
	{
		return (pos.x < Screen.width / 2) && (pos.y > Screen.height / 2);
	}

	private bool IsTouchInRightTopScreenQuarter(Vector2 pos)
	{
		return (pos.x > Screen.width / 2) && (pos.y > Screen.height / 2);
	}
}
