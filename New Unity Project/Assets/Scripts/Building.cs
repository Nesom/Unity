﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : EntityWithHealth
{
	internal bool isActivated;

	 [HideInInspector]
    public Vector2Int Size = Vector2Int.one;

    public static event Action<float> MoneyAdded;
    public static event Action<float> FoodAdded;
    public bool isFood;
    public GameObject canBeUpgradedTo;

    public float moneyAndFoodInterval;
    public float moneyIncrement;
    public float foodIncrement;
    private UpdateTimer moneyAndFoodTimer;

    public float cost;

    protected override void Start()
    {
		base.Start();
		moneyAndFoodTimer = new UpdateTimer(moneyAndFoodInterval);
    }

    protected override void Update()
    {
		if (!isActivated)
			return;

		base.Update();
        if (moneyAndFoodTimer.Check(Time.deltaTime))
        {
            MoneyAdded.Invoke(moneyIncrement);
            if (isFood)
				FoodAdded.Invoke(foodIncrement);
        }
    }

	public void Activate()
	{
        BuildingManager.instance.buildings.Add(this);
		gameObject.GetComponent<BoxCollider>().enabled = true;
		isActivated = true;
	}

	public override void Destroy()
	{
        BuildingManager.instance.buildings.Remove(this);
		isActivated = false;
		base.Destroy();
	}

    private void OnDrawGizmosSelected()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y< Size.y; y++)
            { 
                Gizmos.color = Color.red;
                Gizmos.DrawCube(transform.position + new Vector3(x,0,y), new Vector3(1, .1f, 1) * transform.localScale.x);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
		PushAway(collision.rigidbody, BuildingManager.instance.pushingAwayForceOnEnter, ForceMode.Impulse);
    }

    void OnCollisionStay(Collision collision)
    {
		PushAway(collision.rigidbody, BuildingManager.instance.pushingAwayForceOnStay, ForceMode.VelocityChange);
    }

	void PushAway(Rigidbody rigidbody, float forceMultiplier, ForceMode forceMode)
	{
		if (rigidbody == null)
			return;

		var normalizedVector = rigidbody.transform.position - transform.position;
		normalizedVector.Normalize();
		rigidbody.AddForce(normalizedVector * forceMultiplier, forceMode);
	}
}
