using UnityEngine;
using System.Collections;

// Types of Action that a character can perform per round. A character can 
// do one full action or two half Action a turn, an unlimited number of free
// Action a turn, and one reaction per round when it is NOT the character's turn.
// Extended Action are special and do not occur during structured time, but rather
// during narrative time.
public enum Action { Full = 2, Half = 1, Free = 0, Reaction = -1, Extended = -2, Variable = -3 }

// Skill checks (and other checks!) can have associated degrees of difficulty. Each has
// a corresponding value that impacts the roll of the check.
public static class TestDifficulty {
    public const int Trivial = 60, Elementary = 50, Simple = 40, Easy = 30, Routine = 20, Ordinary = 10, Challenging = 0, Difficult = -10,
                             Hard = -20, VeryHard = -30, Arduous = -40, Punishing = -50, Hellish = -60; }

public class Skills : MonoBehaviour {

    // Skills represent an Explorer's knowledge and training.
    // Each explorer starts with a set of skills representing
    // what they learned before they began exploring. New skills
    // can be obtained and existing skills can be improved through
    // spending experience points.

    // Skills have an associated characteristic, which is used in
    // any tests that the Explorer makes against that skill. If a 
    // skill is trained, the Explorer must roll lower than or equal
    // to his characteristic score. Basic skills can be used untrained,
    // although the Explorer takes a penalty and must roll lower than or
    // equal to half of his characteristic (rounded down). Advanced skills
    // must be trained before an Explorer uses them.

    // Skills can be advanced twice after initially acquiring them, with
    // each advancement giving the Explorer a +10 bonus on skill checks.

    public Skill Acrobatics, Awareness, Barter, Blather, Carouse, Charm,
        ChemUse, Ciphers, Climb, Command, Commerce, CommonLore, Concealment,
        Contortionist, Deceive, Demolition, Disguise, Dodge, Drive, Evaluate,
        ForbiddenLore, Gamble, Inquiry, Interrogation, Intimidate, Invocation,
        Literacy, Logic, Medicae, Navigation, Performer, Pilot, Psyniscience,
        ScholasticLore, Scrutiny, Search, SecretTongue, Security, Shadowing,
        SilentMove, SleightOfHand, SpeakLanguage, Survival, Swim, TechUse,
        Tracking, Trade, Wrangling;

    void Start()
    {
        Acrobatics      = new Skill(CharacteristicChoices.Ag, false, Action.Full);
        Awareness       = new Skill(CharacteristicChoices.Per, true, Action.Free);
        Barter          = new Skill(CharacteristicChoices.Fel, true, Action.Extended);
        Blather         = new Skill(CharacteristicChoices.Fel, false, Action.Full);
        Carouse         = new Skill(CharacteristicChoices.T, true, Action.Free);
        Charm           = new Skill(CharacteristicChoices.Fel, true, Action.Extended);
        ChemUse         = new Skill(CharacteristicChoices.Int, false, Action.Full);
        Ciphers         = new Skill(CharacteristicChoices.Int, false, Action.Full);
        Climb           = new Skill(CharacteristicChoices.S, true, Action.Half);
        Command         = new Skill(CharacteristicChoices.Fel, true, Action.Half);
        Commerce        = new Skill(CharacteristicChoices.Fel, false, Action.Extended);
        CommonLore      = new Skill(CharacteristicChoices.Int, false, Action.Free);
        Concealment     = new Skill(CharacteristicChoices.Ag, true, Action.Half);
        Contortionist   = new Skill(CharacteristicChoices.Ag, true, Action.Full);
        Deceive         = new Skill(CharacteristicChoices.Fel, true, Action.Extended);
        Demolition      = new Skill(CharacteristicChoices.Int, false, Action.Full);
        Disguise        = new Skill(CharacteristicChoices.Fel, true, Action.Extended);
        Dodge           = new Skill(CharacteristicChoices.Ag, true, Action.Reaction);
        Drive           = new Skill(CharacteristicChoices.Ag, false, Action.Half);
        Evaluate        = new Skill(CharacteristicChoices.Int, true, Action.Extended);
        ForbiddenLore   = new Skill(CharacteristicChoices.Int, false, Action.Free);
        Gamble          = new Skill(CharacteristicChoices.Int, true, Action.Full);
        Inquiry         = new Skill(CharacteristicChoices.Fel, true, Action.Extended);
        Interrogation   = new Skill(CharacteristicChoices.WP, false, Action.Extended);
        Intimidate      = new Skill(CharacteristicChoices.S, true, Action.Full);
        Invocation      = new Skill(CharacteristicChoices.WP, false, Action.Full);
        Literacy        = new Skill(CharacteristicChoices.Int, false, Action.Extended);
        Logic           = new Skill(CharacteristicChoices.Int, true, Action.Extended);
        Medicae         = new Skill(CharacteristicChoices.Int, false, Action.Variable);
        Navigation      = new Skill(CharacteristicChoices.Int, false, Action.Extended);
        Performer       = new Skill(CharacteristicChoices.Fel, false, Action.Variable);
        Pilot           = new Skill(CharacteristicChoices.Ag, false, Action.Half);
        Psyniscience    = new Skill(CharacteristicChoices.Per, false, Action.Full);
        ScholasticLore  = new Skill(CharacteristicChoices.Int, false, Action.Free);
        Scrutiny        = new Skill(CharacteristicChoices.Per, true, Action.Full);
        Search          = new Skill(CharacteristicChoices.Per, true, Action.Extended);
        SecretTongue    = new Skill(CharacteristicChoices.Int, false, Action.Free);
        Security        = new Skill(CharacteristicChoices.Ag, false, Action.Extended);
        Shadowing       = new Skill(CharacteristicChoices.Ag, false, Action.Extended);
        SilentMove      = new Skill(CharacteristicChoices.Ag, true, Action.Free);
        SleightOfHand   = new Skill(CharacteristicChoices.Ag, false, Action.Half);
        SpeakLanguage   = new Skill(CharacteristicChoices.Int, false, Action.Free);
        Survival        = new Skill(CharacteristicChoices.Int, false, Action.Full);
        Swim            = new Skill(CharacteristicChoices.S, true, Action.Full);
        TechUse         = new Skill(CharacteristicChoices.Int, false, Action.Extended);
        Tracking        = new Skill(CharacteristicChoices.Int, false, Action.Free);
        Trade           = new Skill(CharacteristicChoices.Int, false, Action.Extended);
        Wrangling       = new Skill(CharacteristicChoices.Int, false, Action.Extended);
    }

}

public class Skill {

    // The characteristic used to test against
    public int associatedCharacteristic;

    // Skills are either basic or advanced
    public bool basic;

    // Characters can take up to two advances in a skill
    public int advancesTaken;

    // Each skill has an associated action type
    public Action actionType;

    // Whether or not a skill is trained
    public bool trained;

    // Each skill starts untrained with no advances. Changes to this
    // can be made by calling the Skill.trainSkill() method for each
    // skill individually.
    public Skill(int inChar, bool inBasic, Action inAction)
    {
        associatedCharacteristic = inChar;
        basic = inBasic;
        advancesTaken = 0;
        actionType = inAction;
        trained = false;
    }

}
