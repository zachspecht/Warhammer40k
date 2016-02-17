using UnityEngine;
using System.Collections;

public class StarshipCombat : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

// During Starship Combat, each character can perform a single action,
// which can be either an extended action, a maneuverability action or
// a shooting action. After each character performs a single action, the
// characters can choose to hail the enemy or not. After that, the turn
// ends and the enemy take a turn. This continues until combat is concluded.
public class ShipAction
{
    // Each action has an associated skill check.
    public Skill skill;

    // Each action has an associated modifier.
    public int modifier;

    // Each action is either a single skill check or a combined skill check
}
