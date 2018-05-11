﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Aniki - The first boss.
/// </summary>
public class Aniki : AEnemy
{
	private bool IsStab1;
	public GameObject EarthquakeAlert;
	//earthquake red circle

	public Aniki ()
	{
		Name = "Aniki";
		MaxHP = 1000f;
		CurrentHP = 1000f;
		Speed = 5f;
		CurrentState = State.IDLE;
		closeRange = 8f;
		deadAnimDuration = 1f;
		IsStab1 = true;
		Buff = new EmptyBuff ();

		Skills = new Dictionary<string, List<ASkill>> { {"close", new List<ASkill> {new Stab (), new Stab (), new Stab (), new Earthquake ()

				}
			}
		};
	}

	protected override void Awake ()
	{
		base.Awake ();
		IsAnimator = false;
	}


	public override void Attack ()
	{
		CanDealDamage = true;
		if (CurrentSkill.Name.Equals ("Stab")) {
			if (IsStab1)
				Animation.Play ("Stab1");
			else
				Animation.Play ("Stab2");
			IsStab1 = !IsStab1;
		} else {
			Animation.Play ("Earthquake");

		}
	}

	public override void DecideState ()
	{
		base.DecideState ();

		/// Only for activate collider of Earthquake
		if (CurrentSkill.Name.Equals ("Earthquake")) {
			EarthquakeAlert.SetActive (true);//activate the alert
			// Mathf.Abs (Time.time - AttackEndTime) <= 0.04f 保证collider在离AttackEndTime左右0.04秒的时间范围内被激活
			Collider c = gameObject.GetComponent<SphereCollider> ();
			if (Mathf.Abs (Time.time - AttackEndTime) <= 0.04f) {
				CurrentSkill.ActivateCollider (true, c);
				CanDealDamage = true;
			} else {
				CurrentSkill.ActivateCollider (false, c);
				CanDealDamage = false;
			}
		} else {
			EarthquakeAlert.SetActive (false);
		}
	}

	public override void Die ()
	{
		if (!IsDead) {
			Animation.Play ("Die");
			IsDead = true;
			deadAnimDuration += Time.time; //update deadAniDuration to deadAnimEndTime
		}

		if (Time.time == deadAnimDuration) {
			transform.Rotate (new Vector3 (0f, 0f, 90f));
		}
	}
}
