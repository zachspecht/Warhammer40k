using UnityEngine;
using System.Collections;


public enum WeaponType { Pistol, Basic, Heavy, Thrown, Melee }
// E = Energy, X = Explosive, R = Rending, I = Impact
public enum DamageType { E, X, R, I }
// Weapon availability influences how difficult and time-consuming it is to acquire the weapon.
public enum Availability { Ubiquitous, Abundant, Plentiful, Common, Average, Scarce, Rare, VeryRare, ExtremelyRare, NearUnique, Unique}
// Weapon crafstmanship affects time to make the weapon, as well as gives bonuses or penalties during combat.
public enum Craftsmanship { Poor, Common, Good, Best }

// Special Qualities that weapons may have. These provide various bonuses or penalties.
public static class WeaponSpecialQualities
{
    public static int ACCURATE = 0;
    public static int BALANCED = 1;
    public static int BLAST = 2;
    public static int CUSTOMISED = 3;
    public static int DEFENSIVE = 4;
    public static int FLAME = 5;
    public static int FLEXIBLE = 6;
    public static int INACCURATE = 7;
    public static int OVERHEATS = 8;
    public static int POWER_FIELD = 9;
    public static int PRIMITIVE = 10;
    public static int RECHARGE = 11;
    public static int RELIABLE = 12;
    public static int SCATTER = 13;
    public static int SHOCKING = 14;
    public static int SMOKE = 15;
    public static int SNARE = 16;
    public static int STORM = 17;
    public static int TEARING = 18;
    public static int TOXIC = 19;
    public static int TWIN_LINKED = 20;
    public static int UNBALANCED = 21;
    public static int UNRELIABLE = 22;
    public static int UNSTABLE = 23;
    public static int UNWIELDY = 24;
}

public struct Damage
{
    public DamageType damageType;
    public int dieSize;
    public int numDice;
    public int bonusDamage;
}

// Reloading takes a specified action and a number of rounds. For example, reloading
// an Autogun takes 2 Full actions, so Reload.action = Action.Full, Reload.numActions = 2.
public struct Reload
{
    public Action action;
    public int numActions;
}

// Each weapon has the following characteristics: name,
// class, range, rate of fire, damage, penetration, clip,
// reload, special attributes, weight and availability.
public class Weapon {

    // The name of the weapon. Useful if you want to look things up in the
    // Rogue Trader rulebook or if you want to display the weapon in game!
    public string name;

    // Range is used to determine how far the weapon can be accuractely fired.
    // Range is measured in meters. Short Range is half the weapon's range, and
    // Long Range is twice the weapon's range. A weapon cannot be fired at a 
    // target more than four times the range distance away.
    public int range;

    // Rate of Fire has three separate entries. These represent if a weapon can
    // be fired singly, semi-automatically or full automatically. If a weapon cannot
    // be fired in a mode, its entry is a -1. Otherwise, the entry represents the
    // amount of bullets the weapon expends firing in that mode.
    public int[] rateOfFire; 

    // Each weapon does a different type of damage, which matters when calculating
    // critical hits.
    public Damage damage;

    // Returns the total damage done by the weapon.
    public int DamageDone()
    {
        int totalDamage = 0;

        for (int i = 0; i < damage.numDice; i++)
        {
            totalDamage += Random.Range(1, damage.dieSize + 1);
        }

        return (totalDamage + damage.bonusDamage);
    }

    // Penetration represents how good a weapon is at cutting through armor. When
    // calculating damage, subtract the weapon's penetration from the target's
    // Armor Points, with a minimum effective Armor Points of zero.
    public int penetration;

    // The weapon's craftsmanship. This gives bonuses or penalties during combat.
    public Craftsmanship craftsmanship;

    // The weapon's availability. Influences how difficult it is to acquire.
    public Availability availability;

    // A measure of how heavy the weapon is.
    public int weight;

    // A measure of how many bullets a weapon can fire before being reloaded.
    public int fullClip;

    // A measure of how long it takes the weapon to be reloaded.
    public Reload reload;

    // Each weapon could have special qualitites. They are reflected in this
    // bit array with a 1 meaning the weapon has that quality and a 0 meaning
    // it does not.
    public BitArray specialQualities;

}
