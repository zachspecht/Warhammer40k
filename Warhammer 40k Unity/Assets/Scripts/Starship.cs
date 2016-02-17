using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Each component may have a special effect. These are
// tracked through the following class. Each Ship Component
// has an integer variable which will be the return code of
// the function that adds the component to the ship. By
// default, all components will have a return code of -1 which
// means that they have no special abilities. If they do have
// special abilities, then the return code will be the component's
// name in the following list, which will set a bit in the bit
// array stored in the ship. That bit array will be 
public static class ShipComponentEffects
{
    public static int WARPSBANE_HULL                    = 0;
    public static int COMBAT_BRIDGE                     = 1;
    public static int COMMAND_BRIDGE                    = 2;
    public static int COMMERCE_BRIDGE                   = 3;
    public static int ARMORED_BRIDGE                    = 4;
    public static int SHIP_MASTERS_BRIDGE               = 5;
    public static int MARK1_LIFE_SUSTAINER              = 6;
    public static int PRESSED_CREW_QUARTERS             = 7;
    public static int MARK201_AUGUR_ARRAY               = 8;
    public static int R50_AUSPEX_MULTIBAND              = 9;
    public static int DEEP_VOID_AUGUR_ARRAY             = 10;
    public static int RYZA_PLASMA_BATTERY               = 11;
    public static int CARGO_HOLD_LIGHTER_BAY            = 12;
    public static int COMPARTMENTALIZED_CARGO_HOLD      = 13;
    public static int MAIN_CARGO_HOLD                   = 14;
    public static int LUXURY_PASSENGER_QUARTERS         = 15;
    public static int BARRACKS                          = 16;
    public static int AUGMENTED_RETRO_THRUSTERS         = 17;
    public static int REINFORCED_INTERIOR_BULKHEADS     = 18;
    public static int ARMOR_PLATING                     = 19;
    public static int ARMORED_PROW                      = 20;
    public static int TENEBRO_MAZE                      = 21;
    public static int CREW_RECLAMATION_FACILITY         = 22;
    public static int EXTENDED_SUPPLY_VAULTS            = 23;
    public static int MUNITORIUM                        = 24;
    public static int TEMPLE_SHRINE_TO_GOD_EMPEROR      = 25;
    public static int LIBRARIUM_VAULT                   = 26;
    public static int TROPHY_ROOM                       = 27;
    public static int OBSERVATION_DOME                  = 28;
    public static int MURDER_SERVITORS                  = 29;
    public static int ANCIENT_LIFE_SUSTAINER            = 30;
    public static int MODIFIED_DRIVE                    = 31;
    public static int BRIDGE_OF_ANTIQUITY               = 32;
    public static int AUTO_STABILIZED_LOGIS_TARGETER    = 33;
    public static int TELEPORTARIUM                     = 34;
    public static int GHOST_FIELD                       = 35;
    public static int SHARD_CANNON_BATTERY              = 36;
    public static int RUNECASTER                        = 37;
    public static int MICRO_LASER_DEFENSE_GRID          = 38;
    public static int GRAVITY_SAILS                     = 39;
    public static int CARGO_HAULER                      = 40;
}

// Resource has two fields: total and used. It also
// has a simple method which returns the amount of free
// resource available. This will be used to represent a
// starship's power and space attributes.
public struct Resource
{
    public int total;
    public int used;

    public int available()
    {
        return (total - used);
    } 
}

// Each ship has weapon capacities for the prow, port, starboard
// and dorsal portions of the ship.
public struct WeaponCapacity
{
    public int prow;
    public int port;
    public int starboard;
    public int dorsal;
}

// Each hull belongs to a more general type of hull, which influences
// which components can be added to the hull.
public enum HullType { Transport, Raider, Frigate, LightCruiser, Cruiser, All }

public class Starship : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Each starship must have a hull.
    public Hull hull;

    public int morale;

    // Storage for the special effects that various components have. These
    // arrays should be initialized to zero, and individual bits are set
    // when a component with a special effect is added.
    public BitArray specialEffects;

    // Method to add a component to the starship. This will set the correct
    // bit in specialEffects.
    public void addComponent(ShipComponent toAdd)
    {
        specialEffects.Set(toAdd.bitIndex, true);
    }

}

public class Hull
{
    // Each hull has a specified type, which restricts the components
    // that you can add to the hull.
    public HullType type;

    // Speed is measured in Void Units, which is an abstract measurement
    // corresponding to grid squares in combat.
    public int speed;

    // Manoeuvrability measures how quickly a ship can change direction,
    // avoid obstacles and evade incoming fire.
    public int maneuverability;

    // Detection measures the power of a vessel's auspexes and augers, and
    // how well they can 'see' their surroundings.
    public int detection;

    // Space measures how much room there is to add components to the hull.
    public Resource space;

    public int armor;

    public int integrity;

    public int turretRating;

    public WeaponCapacity weaponCapacity;

    // Each hull costs a different amount of Ship Points (SP).
    public int SP;

    // Abstract constructor to be implemented by the children
    public Hull() { }
}

/****************************************************************/
/*                                                              */
/*                  START OF HULL DECLARATIONS                  */
/*                                                              */
/****************************************************************/

// Gigantic Jericho ships are converted refinary vessels. Their huge
// fuel tanks are rebuilt into hundreds of passenger compartments
// and a single ship can hold thousands of the faithful. A Jericho
// can be repurposed to carry cargo. The ships themselves are large,
// slow and unwieldy. Most do sport some weapons to discourage pirates,
// though most buccaneers might look for richer targets.
public class JerichoPilgrim : Hull
{
    public JerichoPilgrim()
    {
        type = HullType.Transport;
        speed = 3;
        maneuverability = -10;
        detection = 5;
        space.total = 45;
        space.used = 0;
        armor = 12;
        integrity = 50;
        turretRating = 1;
        weaponCapacity.dorsal = 0;
        weaponCapacity.prow = 1;
        weaponCapacity.port = 1;
        weaponCapacity.starboard = 1;
        SP = 20;
    }
}

// A common sight throughout the Calixis Sector, Vagabonds
// are small, multi-purpose merchant vessels able to transport
// a variety of cargos and even passengers. Popular among poorer
// Chartist captains, these ships are unassuming but reliable, and
// have even been known to mount small broadsides for defense.
public class VagabondMerchant : Hull
{
    public VagabondMerchant()
    {
        type = HullType.Transport;
        speed = 4;
        maneuverability = -5;
        detection = 10;
        space.total = 40;
        space.used = 0;
        armor = 13;
        integrity = 40;
        turretRating = 1;
        weaponCapacity.dorsal = 1;
        weaponCapacity.prow = 1;
        weaponCapacity.port = 0;
        weaponCapacity.starboard = 0;
        SP = 20;
    }
}

// The Hazeroth class comprises a variety of raider vessels of
// similar size and firepower. Many have been known to operate
// from the infamous Hazeroth Abyss and are popular with privateers.
// Most sacrifice cargo space and armor for improved engines and
// reinforced bulkheads, allowing them to flee anything they cannot
// fight.
public class HazerothPrivateer : Hull
{
    public HazerothPrivateer()
    {
        type = HullType.Raider;
        speed = 10;
        maneuverability = 23;
        detection = 12;
        space.total = 35;
        space.used = 0;
        armor = 14;
        integrity = 32;
        turretRating = 1;
        weaponCapacity.dorsal = 1;
        weaponCapacity.prow = 1;
        weaponCapacity.port = 0;
        weaponCapacity.starboard = 0;
        SP = 30;
    }
}

// The Havoc class is a heavy raider whose origins date back to
// before the reconquest of the Calixis Sector. A typical Havoc
// has fast engines, sizeable cargo space and a battery strength
// to rival many frigates. However, their armor is relatively weak,
// meaning that these glass cannons have a hard time going toe-to-toe
// with a comparable naval vessel.
public class HavocMerchant : Hull
{
    public HavocMerchant()
    {
        type = HullType.Raider;
        speed = 9;
        maneuverability = 25;
        detection = 10;
        space.total = 40;
        space.used = 0;
        armor = 16;
        integrity = 30;
        turretRating = 1;
        weaponCapacity.dorsal = 1;
        weaponCapacity.prow = 1;
        weaponCapacity.port = 0;
        weaponCapacity.starboard = 0;
        SP = 35;
    }
}

// The Sword frigates have been a mainstay escort vessel for
// Battlefleet Calixis ever since its founding. Every system
// aboard one of these frigates has been tried and tested in
// innumerable engagements. Its laser-based weapons and turrets
// are accurate and hard-hitting, its plasma drives are rugged
// and reliable in extreme conditions. Few task forces do not 
// include at least a pair of Swords to guard the flanks of 
// larger vessels or pursue smaller, faster raiders. More than
// a few Rogue Traders have noticed the stellar performance of
// these vessels and obtained one. With a few minor conversions
// to increase holds, Swords suit their needs quite well.
public class SwordFrigate : Hull
{
    public SwordFrigate()
    {
        type = HullType.Frigate;
        speed = 8;
        maneuverability = 20;
        detection = 15;
        space.total = 40;
        space.used = 0;
        armor = 18;
        integrity = 35;
        turretRating = 2;
        weaponCapacity.dorsal = 2;
        weaponCapacity.prow = 0;
        weaponCapacity.port = 0;
        weaponCapacity.starboard = 0;
        SP = 40;
    }
}

// The Tempes is a specialized frigate produced in the Calixis
// and surrounding sectors. It trades long-ranged firepower for
// heavy, short-ranged broadsides designed to devastate enemies
// at 'knife-fight' distances. To get to these distances, Tempests
// have triple-armored prows and boosted drives, and often carry
// assault boats and large complements of ratings for boarding actions.
// These larger quarters and hangar bays have been found very useful
// for other, more commercial purposes as well.
public class TempestStrike : Hull
{
    public TempestStrike()
    {
        type = HullType.Frigate;
        speed = 8;
        maneuverability = 18;
        detection = 12;
        space.total = 42;
        space.used = 0;
        armor = 19;
        integrity = 36;
        turretRating = 1;
        weaponCapacity.dorsal = 2;
        weaponCapacity.prow = 0;
        weaponCapacity.port = 0;
        weaponCapacity.starboard = 0;
        SP = 40;
    }
}

// Light, scouting cruisers are the eyes and ears of Imperial fleets.
// They carry enough fuel and supplies for patrols that last months or
// even years, and enough firepower to dispatch any smaller vessels
// foolish enough to close with them. The Dauntless is popular because
// it combines the maneuverability of a frigate with a daunting forward
// lance armament.
public class DauntlessLight : Hull
{
    public DauntlessLight()
    {
        type = HullType.LightCruiser;
        speed = 7;
        maneuverability = 15;
        detection = 20;
        space.total = 60;
        space.used = 0;
        armor = 19;
        integrity = 60;
        turretRating = 1;
        weaponCapacity.dorsal = 0;
        weaponCapacity.prow = 1;
        weaponCapacity.port = 1;
        weaponCapacity.starboard = 1;
        SP = 55;
    }
}

// The Lunar class cruiser makes up the backbone of Battlefleet Calixis.
// Its relatively uncomplicated design dates back to the dawn of the
// Imperium, and it can be constructed at worlds normally unable to build
// a ship of the line. Its variety of weapon batteries, lances and torpedoes
// make it both a versatile combatant and a dangerous foe. Most Rogue Traders
// remove the torpedo tubes to add more space instead.
public class LunarCruiser : Hull
{
    public LunarCruiser()
    {
        type = HullType.Cruiser;
        speed = 5;
        maneuverability = 10;
        detection = 10;
        space.total = 75;
        space.used = 0;
        armor = 20;
        integrity = 70;
        turretRating = 2;
        weaponCapacity.dorsal = 0;
        weaponCapacity.prow = 1;
        weaponCapacity.port = 1;
        weaponCapacity.starboard = 2;
        SP = 60;
    }
}

/****************************************************************/
/*                                                              */
/*                  END OF HULL DECLARATIONS                    */
/*                                                              */
/****************************************************************/

// Each ship has several essential components. These normally cost
// no Ship Points because they're included in the cost of the hull,
// although rarer or more powerful components could still have an
// associated SP cost. Each ship needs one of each of the following
// components: Plasma Drive, Warp Engine, Geller Field, Void Shields,
// Ship's Bridge, Life Sustainer, Crew Quarters and Augur Array.
// Each component needs a certain amount of power and space to function.
// Certain components work on only certain hull types.

// A ship can also have supplemental components, provided enough space
// and power remains. These components all have SP costs in addition
// to the other attributes of a component. These include two powerful
// types of components, Archeotech and Xeno-tech, which are rarer and
// only obtainable in special circumstances.
public class ShipComponent
{
    public int powerCost;
    public int spaceCost;
    public int SPCost;
    public bool isArcheotech;
    public bool isXenotech;
    public List<HullType> allowableHulls;
    public int bitIndex;

    public ShipComponent()
    {
        allowableHulls = new List<HullType>();
        isArcheotech = false;
        isXenotech = false;
        SPCost = 0;
        bitIndex = -1;
    }
}

/****************************************************************/
/*                                                              */
/*             START OF PLASMA DRIVE DECLARATIONS               */
/*                                                              */
/****************************************************************/

// Plasma Drives move the ship and power all of the ship's other systems.
public class PlasmaDrive : ShipComponent
{
    // PlasmaDrives have the unique ability to generate power
    // instead of consume it.
    public int powerGenerated;

    // Standard Plasma Drive. If it's archeotype, then it takes up
    // four less space than usual and adds +1 to the ship's speed.
    public PlasmaDrive(bool isArcheo)
    {
        powerCost = 0;
        isArcheotech = isArcheo;
    }
}

public class Jovian1Drive : PlasmaDrive
{
    public Jovian1Drive(bool isArcheo) : base(isArcheo)
    {
        powerGenerated = 35;
        spaceCost = 8;
        allowableHulls.Add(HullType.Transport);
    }
}

public class Lathe1Drive : PlasmaDrive
{
    public Lathe1Drive(bool isArcheo) : base(isArcheo)
    {
        powerGenerated = 40;
        spaceCost = 12;
        allowableHulls.Add(HullType.Transport);
        SPCost = 1;
    }
}

public class Jovian2Drive : PlasmaDrive
{
    public Jovian2Drive(bool isArcheo) : base(isArcheo)
    {
        powerGenerated = 45;
        spaceCost = 10;
        allowableHulls.Add(HullType.Raider);
        allowableHulls.Add(HullType.Frigate);
    }
}

public class Jovian3Drive : PlasmaDrive
{
    public Jovian3Drive(bool isArcheo) : base(isArcheo)
    {
        powerGenerated = 60;
        spaceCost = 12;
        allowableHulls.Add(HullType.LightCruiser);
    }
}

public class Jovian4Drive : PlasmaDrive
{
    public Jovian4Drive(bool isArcheo) : base(isArcheo)
    {
        powerGenerated = 75;
        spaceCost = 14;
        allowableHulls.Add(HullType.Cruiser);
    }
}

/****************************************************************/
/*                                                              */
/*               END OF PLASMA DRIVE DECLARATIONS               */
/*                                                              */
/****************************************************************/

/****************************************************************/
/*                                                              */
/*             START OF WARP ENGINE DECLARATIONS                */
/*                                                              */
/****************************************************************/

// Warp Engines allow vessels from the material world to enter the
// warp and cross vast distances in a heartbeat. However, vessels
// are exposed to the dangers of the immaterium.
public class WarpEngine : ShipComponent
{
    public WarpEngine() { }
}

public class Strelov1Engine : WarpEngine
{
    public Strelov1Engine()
    {
        powerCost = 10;
        spaceCost = 10;
        allowableHulls.Add(HullType.Transport);
        allowableHulls.Add(HullType.Raider);
        allowableHulls.Add(HullType.Frigate);
    }
}

public class Strelov2Engine : WarpEngine
{
    public Strelov2Engine()
    {
        powerCost = 12;
        spaceCost = 12;
        allowableHulls.Add(HullType.LightCruiser);
        allowableHulls.Add(HullType.Cruiser);
    }
}

/****************************************************************/
/*                                                              */
/*               END OF WARP ENGINE DECLARATIONS                */
/*                                                              */
/****************************************************************/

/****************************************************************/
/*                                                              */
/*             START OF GELLER FIELD DECLARATIONS               */
/*                                                              */
/****************************************************************/

// A ship's Geller Field creates a bubble of reality around the
// vessel when it traverses the warp, protecting it from the dangers
// that lurk there.

// Note that here, the basic GellerField is actually a fully
// serviceable object that the characters can use.
public class GellerField : ShipComponent
{
    public GellerField()
    {
        powerCost = 1;
        spaceCost = 0;
        allowableHulls.Add(HullType.All);
    }
}

public class WarpsbaneHull : GellerField
{
    WarpsbaneHull()
    {
        SPCost = 1;
        bitIndex = ShipComponentEffects.WARPSBANE_HULL;
    }
}

/****************************************************************/
/*                                                              */
/*               END OF GELLER FIELD DECLARATIONS               */
/*                                                              */
/****************************************************************/

/****************************************************************/
/*                                                              */
/*             START OF VOID SHIELD DECLARATIONS                */
/*                                                              */
/****************************************************************/

// Void Shields create barriers of energy around the starship to
// protect it from stellar debris and incoming fire.

public class VoidShield : ShipComponent
{
    public int voidShields;

    public VoidShield() { }
}

public class SingleVoidArray : VoidShield
{
    public SingleVoidArray()
    {
        powerCost = 5;
        spaceCost = 1;
        allowableHulls.Add(HullType.All);
        voidShields = 1;
    }
}

public class MultipleVoidArray : VoidShield
{
    public MultipleVoidArray()
    {
        powerCost = 7;
        spaceCost = 2;
        allowableHulls.Add(HullType.Cruiser);
        voidShields = 2;
    }
}

/****************************************************************/
/*                                                              */
/*               END OF VOID SHIELD DECLARATIONS                */
/*                                                              */
/****************************************************************/

/****************************************************************/
/*                                                              */
/*                 START OF BRIDGE DECLARATIONS                 */
/*                                                              */
/****************************************************************/

// The bridge is the starship's brain, where the captain commands
// the vessel and directs its every action.

public class Bridge : ShipComponent
{
    public Bridge() { }
}

public class CombatBridge : Bridge
{
    public CombatBridge(HullType hull)
    {
        allowableHulls.Add(HullType.All);
        bitIndex = ShipComponentEffects.COMBAT_BRIDGE;

        if ((hull == HullType.LightCruiser) || (hull == HullType.Cruiser))
        {
            powerCost = 2;
            spaceCost = 2;
        }
        else
        {
            powerCost = 1;
            spaceCost = 1;
        }
    }
}

public class CommandBridge : Bridge
{
    public CommandBridge(HullType hull)
    {
        allowableHulls.Add(HullType.Raider);
        allowableHulls.Add(HullType.Frigate);
        allowableHulls.Add(HullType.LightCruiser);
        allowableHulls.Add(HullType.Cruiser);
        bitIndex = ShipComponentEffects.COMMAND_BRIDGE;
        SPCost = 1;

        if ((hull == HullType.LightCruiser) || (hull == HullType.Cruiser))
        {
            powerCost = 3;
            spaceCost = 2;
        }
        else
        {
            powerCost = 2;
            spaceCost = 1;
        }
    }
}

public class CommerceBridge : Bridge
{
    public CommerceBridge(HullType hull)
    {
        allowableHulls.Add(HullType.Transport);
        bitIndex = ShipComponentEffects.COMMERCE_BRIDGE;
        powerCost = 1;
        spaceCost = 1;
    }
}

public class ArmoredCommandBridge : Bridge
{
    public ArmoredCommandBridge(HullType hull)
    {
        allowableHulls.Add(HullType.Raider);
        allowableHulls.Add(HullType.Frigate);
        allowableHulls.Add(HullType.LightCruiser);
        allowableHulls.Add(HullType.Cruiser);
        bitIndex = ShipComponentEffects.ARMORED_BRIDGE;

        if ((hull == HullType.LightCruiser) || (hull == HullType.Cruiser))
        {
            powerCost = 3;
            spaceCost = 2;
        }
        else
        {
            powerCost = 2;
            spaceCost = 2;
        }
    }
}

public class ShipMasterBridge : Bridge
{
    public ShipMasterBridge(HullType hull)
    {
        allowableHulls.Add(HullType.Cruiser);
        bitIndex = ShipComponentEffects.SHIP_MASTERS_BRIDGE;
        powerCost = 4;
        spaceCost = 3;
    }
}

/****************************************************************/
/*                                                              */
/*                  END OF BRIDGE DECLARATIONS                  */
/*                                                              */
/****************************************************************/

/****************************************************************/
/*                                                              */
/*           START OF LIFE SUSTAINER DECLARATIONS               */
/*                                                              */
/****************************************************************/

// Life sustainers play a vital role by providing a ship with clean
// air and water.

public class LifeSustainer : ShipComponent
{
    public LifeSustainer() { }
}

public class M1LifeSustainer : LifeSustainer
{
    public M1LifeSustainer(HullType hull)
    {
        allowableHulls.Add(HullType.All);
        bitIndex = ShipComponentEffects.MARK1_LIFE_SUSTAINER;

        // Power and space costs depend on the type of hull!
        if ((hull == HullType.LightCruiser) || (hull == HullType.Cruiser))
        {
            powerCost = 4;
            spaceCost = 2;
        }
        else
        {
            powerCost = 3;
            spaceCost = 1;
        }
    }
}

public class VitaePatternLifeSustainer : LifeSustainer
{
    public VitaePatternLifeSustainer(HullType hull)
    {
        allowableHulls.Add(HullType.All);

        // Power and space costs depend on the type of hull!
        if ((hull == HullType.LightCruiser) || (hull == HullType.Cruiser))
        {
            powerCost = 5;
            spaceCost = 3;
        }
        else
        {
            powerCost = 4;
            spaceCost = 2;
        }
    }
}

/****************************************************************/
/*                                                              */
/*            END OF LIFE SUSTAINER DECLARATIONS                */
/*                                                              */
/****************************************************************/

/****************************************************************/
/*                                                              */
/*            START OF CREW QUARTERS DECLARATIONS               */
/*                                                              */
/****************************************************************/

// Even the lowliest crews require mess halls and bunks to live in.

public class CrewQuarters : ShipComponent
{
    public CrewQuarters() { }
}

public class PressedCrewQuarters : CrewQuarters
{
    public PressedCrewQuarters(HullType hull)
    {
        allowableHulls.Add(HullType.All);
        bitIndex = ShipComponentEffects.PRESSED_CREW_QUARTERS;

        // Power and space costs depend on the type of hull!
        if ((hull == HullType.LightCruiser) || (hull == HullType.Cruiser))
        {
            powerCost = 2;
            spaceCost = 3;
        }
        else
        {
            powerCost = 1;
            spaceCost = 2;
        }
    }
}

public class VoidsmenQuarters : CrewQuarters
{
    public VoidsmenQuarters(HullType hull)
    {
        allowableHulls.Add(HullType.All);

        // Power and space costs depend on the type of hull!
        if ((hull == HullType.LightCruiser) || (hull == HullType.Cruiser))
        {
            powerCost = 2;
            spaceCost = 4;
        }
        else
        {
            powerCost = 1;
            spaceCost = 3;
        }
    }
}

/****************************************************************/
/*                                                              */
/*            END OF CREW QUARTERS DECLARATIONS                 */
/*                                                              */
/****************************************************************/

/****************************************************************/
/*                                                              */
/*             START OF AUGUR ARRAY DECLARATIONS                */
/*                                                              */
/****************************************************************/

// The starship's eyes, allowing it to 'see' space far beyond the
// range of normal eyesight.

public class AugurArray : ShipComponent
{
    public AugurArray() { spaceCost = 0; }
}

public class M100Array : AugurArray
{
    public M100Array()
    {
        allowableHulls.Add(HullType.All);
        powerCost = 3;
    }
}

public class M201Array : AugurArray
{
    public M201Array()
    {
        allowableHulls.Add(HullType.All);
        powerCost = 5;
        bitIndex = ShipComponentEffects.MARK201_AUGUR_ARRAY;
    }
}

public class R50AuspexArray : AugurArray
{
    public R50AuspexArray()
    {
        allowableHulls.Add(HullType.All);
        powerCost = 4;
        bitIndex = ShipComponentEffects.R50_AUSPEX_MULTIBAND;
    }
}

public class DeepVoidArray : AugurArray
{
    public DeepVoidArray()
    {
        allowableHulls.Add(HullType.All);
        powerCost = 7;
        SPCost = 1;
        bitIndex = ShipComponentEffects.DEEP_VOID_AUGUR_ARRAY;
    }
}

/****************************************************************/
/*                                                              */
/*              END OF AUGUR ARRAY DECLARATIONS                 */
/*                                                              */
/****************************************************************/
