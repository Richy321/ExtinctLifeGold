using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Creature : ScriptableObject 
{
    public int healthPoints = 100;
    public int actionPoints = 2;

    public int groundMovementSpeed = 1;
    public int waterMovementSpeed = 1;

    public int meleeAttackRange = 1;
    public int rangedAttackRange = 2;
    public int meleAttackDamage = 10;
    public int rangedAttackDamage = 5;

    public int chromosome = 0;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
