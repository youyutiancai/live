﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRefresh : MonoBehaviour
{

	public GameObject EnemyPrefab;

	public Vector3 Vec3;
	public float RefreshDelayDeltaTime = 3f;

	private float NextRefreshTime;

	void Start ()
	{
		NextRefreshTime = Time.time;
	}


	void Update ()
	{
		int GONums = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		// if the numbers of small boss is less than 4, create a new one
		if (Time.time >= NextRefreshTime && GONums < 7) {
			Instantiate (EnemyPrefab, Vec3, Quaternion.identity);
			NextRefreshTime += RefreshDelayDeltaTime;
		}
	}
}
