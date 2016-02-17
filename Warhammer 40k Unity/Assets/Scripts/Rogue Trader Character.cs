using UnityEngine;
using System.Collections;

// Each Rogue Trader Character has the following characteristics:
// Weapon Skill (WS), Ballistic Skill (BS), Strength (S),
// Toughness (T), Agility (Ag), Intelligence (Int), Perception (Per),
// Will Power (WP), Fellowship (Fel). Each is rated 0 - 100. 
public static class CharacteristicChoices {
    public const int WS = 0, BS = 1, S = 2, T = 3, Ag = 4, Int = 5, Per = 6, WP = 7, Fel = 8; }

public class RogueTraderCharacter : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    // Rank is determined by the total amount of experience spent.
    // Also, we must keep track of the experience available to spend.
    int freeXP;
    int rank;
    int totalXP;
 
    // A character will start with 25 in each characteristic and then 
    // be allowed to spend an additional 100 points, although no characteristic
    // can be increased past 45 to start.
    int[] characteristics = { 25, 25, 25, 25, 25, 25, 25, 25, 25 };

    // Each characteristic can be advanced four times, after which no
    // more advances are allowed. The following array keeps track of the
    // advances taken.
    int[] characteristicAdvances = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    // Function to advance a given characteristic. Returns 0 if
    // the advance was successful, -1 if the character did not have
    // enough XP, or -2 if the characteristic has no advances left
    // TODO: add some methods to DISPLAY the reult!
    int AdvanceCharacteristic(int characteristic)
    {
        // First, check to see if the character is allowed
        // to advance that characteristic.
        int numAdvances = characteristicAdvances[characteristic];
        if (numAdvances == 4)
            return -2;
        else
        {
            // The initial advancement costs 250xp, with each successive
            // advancement costing 250 xp more.
            int xpCost = (numAdvances + 1) * 250;

            // Check to see if the player has enough experience to spend.
            if (freeXP >= xpCost)
            {
                // Advance the characteristic and spend the experience.
                // Each advancement awards +5 to the characteristic.
                freeXP -= xpCost;
                characteristicAdvances[characteristic] += 1;
                characteristics[characteristic] += 5;
            }
            else
            {
                // The player did not have enough experience available
                // for the advancement.
                return -1;
            }
        }
        return 0;
    }

    // Each Rogue Trader Character also has a number of skills, which he or she
    // can use to perform skill tests.
    public Skills skills;

    // Method to perform a Skill Test. The modifier is a measure of how difficult the
    // test is for the character. The method returns the degrees of success that the
    // character succeeded or failed by, with 0 denoting success without any degrees
    // of success and negatives denotings degrees of failure.
    public int SkillTest(ArrayList modifiers, Skill skill)
    {
        // Find the character's characteristic, then determine if the skill is trained or untrained.
        // Add any associates difficulty modifiers or mastered skill modifiers.
        int mySkill = (skill.trained) ? characteristics[skill.associatedCharacteristic] : (characteristics[skill.associatedCharacteristic] / 2);
        mySkill = mySkill + (skill.advancesTaken * 10);
        foreach (int i in modifiers)
            mySkill += i;

        // Generatea random number from 1 to 100. This is equivalent to
        // the character's die roll.
        int myRoll = Random.Range(1, 101);

        // Determine and return the degrees of success.
        return ((mySkill - myRoll) / 10);

    }

    // Similar to a Skill Test, except a Combined Skill Test adds the values of two
    // separate skills before computing the result.
    public int CombinedSkillTest(ArrayList modifiers, Skill skill1, Skill skill2)
    {
        // Find the character's characteristic, then determine if the skill is trained or untrained.
        // Add any associates difficulty modifiers or mastered skill modifiers.
        int mySkill1 = (skill1.trained) ? characteristics[skill1.associatedCharacteristic] : (characteristics[skill1.associatedCharacteristic] / 2);
        mySkill1 = mySkill1 + (skill1.advancesTaken * 10);
        int mySkill2 = (skill2.trained) ? characteristics[skill2.associatedCharacteristic] : (characteristics[skill2.associatedCharacteristic] / 2);
        mySkill2 = mySkill2 + (skill2.advancesTaken * 10);
        int mySkill = mySkill1 + mySkill2;
        foreach (int i in modifiers)
            mySkill += i;

        // Generatea random number from 1 to 100. This is equivalent to
        // the character's die roll.
        int myRoll = Random.Range(1, 101);

        // Determine and return the degrees of success.
        return ((mySkill - myRoll) / 10);
    }
}