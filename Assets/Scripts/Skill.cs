﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System;
public enum SkillType
{
    Normal,
    Closest,
    Furthest,
    Target,
    Weakest,
    Random,
    SelfHeal,
    SpreadHeal,
    TargetHeal,
    AOEHeal,
    Drain,
    AOEDrain,
    AOE,
    Pierce3,
    Pierce2,
    Execute,
    Ricochet,
    Unity,
    Revive

}
public class Skill
{
    public float Value;
    public float Range;
    public int Weight;
    public int Cost;
    public bool IsAOE
    {
        get
        {
            switch (skillType)
            {
                case SkillType.AOE:
                case SkillType.AOEDrain:
                case SkillType.Pierce3:
                case SkillType.Pierce2:
                case SkillType.AOEHeal:
                    return true;
                default:
                    return false;
            }
        }
    }
    private bool IsCrit;
    private bool IsEmp;
    public static Random random = new Random(Guid.NewGuid().GetHashCode());
    private SkillType skillType;


    public Skill(float value, float range, int weight, int cost, SkillType skilltype)
    {
        Value = value;
        Range = range;
        Weight = weight;
        Cost = cost;
        skillType = skilltype;
    }

    private void StoreRandomFactors(Character character)
    {
        IsCrit = Logic.RNGroll(character.critChance);
        IsEmp = Logic.RNGroll(character.empowerChance);
    }

    public int GetValue(Character character)
    {
        int attackModifier = Convert.ToInt32(Value * Range * character.power);
        int returnValue = 0;
        int mod = Convert.ToInt32(Math.Pow(-1, random.Next(2)));
        returnValue = Convert.ToInt32(character.power * Value + random.Next(attackModifier)* mod);

        if (IsCrit)
        {
            returnValue = Convert.ToInt32(returnValue * character.critDamage);
        }
        if (IsEmp)
        {
            returnValue *= 2;
        }
        return returnValue;
    }
    
    public void ApplySkill(Character author, Character[] party, Character[] opponents)
    {
        int amountToCast = 1;
        if (Logic.RNGroll(author.dsChance)) amountToCast++;
        while (amountToCast != 0)
        {
            StoreRandomFactors(author);
            switch (skillType)
            {
                case SkillType.Normal:
                case SkillType.Closest:
                    ClosestSkill(author, party, opponents);
                    break;
                case SkillType.Target:
                case SkillType.Random:
                    TargetSkill(author, party, opponents);
                    break;
                case SkillType.Weakest:
                    WeakestSkill(author, party, opponents);
                    break;
                case SkillType.Furthest:
                    FurthestSkill(author, party, opponents);
                    break;
                case SkillType.AOE:
                    AoeSkill(author, party, opponents);
                    break;
                case SkillType.AOEHeal:
                    AoeHealSkill(author, party);
                    break;
                case SkillType.SelfHeal:
                    SelfHealSkill(author);
                    break;
                case SkillType.SpreadHeal:
                    SpreadHealSkill(author, party);
                    break;
                case SkillType.TargetHeal:
                    TargetHealSkill(author, party);
                    break;
                case SkillType.AOEDrain:
                    author.drain = true;
                    AoeSkill(author, party, opponents);
                    author.drain = false;
                    break;
                case SkillType.Drain:
                    author.drain = true;
                    ClosestSkill(author, party, opponents);
                    author.drain = false;
                    break;
                case SkillType.Pierce2:
                    Pierce2Skill(author, party, opponents);
                    break;
                case SkillType.Pierce3:
                    Pierce3Skill(author, party, opponents);
                    break;
                case SkillType.Execute:
                    author.selfInjure = true;
                    TargetSkill(author, party, opponents);
                    author.selfInjure = false;
                    break;
                case SkillType.Ricochet:
                    RicochetSkill(author, party, opponents);
                    break;
                case SkillType.Unity:
                    SpreadHealSkill(author, party);
                    break;
                case SkillType.Revive:
                    break;
            }
            amountToCast--;
        }
    }

    private void FurthestSkill(Character author, Character[] party, Character[] opponents)
    {
        //find target 
        bool absorbProc = false;
        Character target = Logic.RedirectDeflectLoop(Logic.SelectBack(opponents), author, opponents, party, ref absorbProc);
        if (!party.Contains(target))
        {
            Character[] placeholder = party;
            party = opponents;
            opponents = placeholder;
        }
        //apply damage
        int attackValue = GetValue(author);
        if (absorbProc)
        {
            PetLogic.PetSelection(author, party, opponents);
            Logic.HitAbsorbed(attackValue, target);
        }
        else
        {
            Logic.DamageApplication(attackValue, target, author, party, opponents);
        }

    }
    private void ClosestSkill(Character author, Character[] party, Character[] opponents)
    {
        //find target 
        bool absorbProc = false;
        Character target = Logic.RedirectDeflectLoop(Logic.SelectFront(opponents), author, opponents, party, ref absorbProc);
        if (!party.Contains(target))
        {
            Character[] placeholder = party;
            party = opponents;
            opponents = placeholder;
        }
        //apply damage

        int attackValue = GetValue(author);
        if (absorbProc)
        {
            PetLogic.PetSelection(author, party, opponents);
            Logic.HitAbsorbed(attackValue, target);
        }
        else
        {
            Logic.DamageApplication(attackValue, target, author, party, opponents);
        }

    }
    //until moki input, random and target will be treated as similar skills
    private void TargetSkill(Character author, Character[] party, Character[] opponents)
    {
        //find target 
        bool absorbProc = false;
        Character target = Logic.RedirectDeflectLoop(Logic.SelectTarget(opponents), author, opponents, party, ref absorbProc);
        if (!party.Contains(target))
        {
            Character[] placeholder = party;
            party = opponents;
            opponents = placeholder;
        }
        //apply damage
        int attackValue = GetValue(author);
        if (absorbProc)
        {
            PetLogic.PetSelection(author, party, opponents);
            Logic.HitAbsorbed(attackValue, target);
        }
        else
        {
            Logic.DamageApplication(attackValue, target, author, party, opponents);
        }

    }
    private void WeakestSkill(Character author, Character[] party, Character[] opponents)
    {
        //find target 
        bool absorbProc = false;
        Character target = Logic.RedirectDeflectLoop(Logic.SelectWeakest(opponents), author, opponents, party, ref absorbProc);
        if (!party.Contains(target))
        {
            Character[] placeholder = party;
            party = opponents;
            opponents = placeholder;
        }
        //apply damage
        int attackValue = GetValue(author);
        if (absorbProc)
        {
            PetLogic.PetSelection(author, party, opponents);
            Logic.HitAbsorbed(attackValue, target);
        }
        else
        {
            Logic.DamageApplication(attackValue, target, author, party, opponents);
        }

    }

    private void TargetHealSkill(Character author, Character[] party)
    {
        int target = Logic.HealFindWeakestPerc(party);
        int attackValue = GetValue(author);
        party[target].hp += attackValue;
        if (party[target].hp > party[target].maxHp) party[target].hp = party[target].maxHp;
    }
    private void AoeHealSkill(Character author, Character[] party)
    {
        for (int i = 0; i < party.Length; i++)
        {
            int attackValue = GetValue(author);
            if (party[i].alive)
            {
                party[i].hp += attackValue;
                if (party[i].hp > party[i].maxHp) party[i].hp = party[i].maxHp;
            }
        }
    }
    private void SpreadHealSkill(Character author, Character[] party)
    {
        int target = Logic.HealFindWeakestPerc(party);
        int healingValue = GetValue(author);
        for (int i = 0; i < healingValue; i++)
        {
            target = Logic.HealFindWeakestPerc(party);
            party[target].hp++;
            if (party[target].hp > party[target].maxHp)
            {
                party[target].hp = party[target].maxHp;
            }
        }
    }
    private void SelfHealSkill(Character author)
    {
        int attackValue = GetValue(author);
        author.hp += attackValue;
        if (author.hp > author.maxHp) author.hp = author.maxHp;
    }
    private void AoeSkill(Character author, Character[] party, Character[] opponents)
    {
        bool absorbProc = false;
        for (int i = 0; i < opponents.Length; i++)
        {
            if (opponents[i].alive)
            {
                Character target = Logic.RedirectDeflectLoop(Logic.SelectFront(opponents), author, opponents, party, ref absorbProc);
                if (!party.Contains(target))
                {
                    Character[] placeholder = party;
                    party = opponents;
                    opponents = placeholder;
                }
                int attackValue = GetValue(author);
                if (absorbProc)
                {
                    PetLogic.PetSelection(author, party, opponents);
                    Logic.HitAbsorbed(attackValue, target);
                }
                else
                {
                    Logic.DamageApplication(attackValue, target, author, party, opponents);
                }
                if (!party.Contains(author))
                {
                    Character[] placeholder = party;
                    party = opponents;
                    opponents = placeholder;
                }
                absorbProc = false;
            }
        }
    }
    private void Pierce3Skill(Character author, Character[] party, Character[] opponents)
    {
        bool absorbProc = false;
        int firstTarget = Logic.SelectPierce(opponents);
        for (int i = firstTarget; i < firstTarget + 3; i++)
        {
            if (i < opponents.Length && opponents[i].alive)
            {
                Character target = Logic.RedirectDeflectLoop(Logic.SelectFront(opponents), author, opponents, party, ref absorbProc);
                if (!party.Contains(target))
                {
                    Character[] placeholder = party;
                    party = opponents;
                    opponents = placeholder;
                }
                int attackValue = GetValue(author);
                if (absorbProc)
                {
                    PetLogic.PetSelection(author, party, opponents);
                    Logic.HitAbsorbed(attackValue, target);
                }
                else
                {
                    Logic.DamageApplication(attackValue, target, author, party, opponents);
                }
                if (!party.Contains(author))
                {
                    Character[] placeholder = party;
                    party = opponents;
                    opponents = placeholder;
                }
                absorbProc = false;
            }
        }
    }
    private void Pierce2Skill(Character author, Character[] party, Character[] opponents)
    {
        bool absorbProc = false;
        int firstTarget = Logic.SelectPierce(opponents);
        for (int i = firstTarget; i < firstTarget + 2; i++)
        {
            if (i < opponents.Length && opponents[i].alive)
            {
                Character target = Logic.RedirectDeflectLoop(Logic.SelectFront(opponents), author, opponents, party, ref absorbProc);
                if (!party.Contains(target))
                {
                    Character[] placeholder = party;
                    party = opponents;
                    opponents = placeholder;
                }
                int attackValue = GetValue(author);
                if (absorbProc)
                {
                    PetLogic.PetSelection(author, party, opponents);
                    Logic.HitAbsorbed(attackValue, target);
                }
                else
                {
                    Logic.DamageApplication(attackValue, target, author, party, opponents);
                }
                if (!party.Contains(author))
                {
                    Character[] placeholder = party;
                    party = opponents;
                    opponents = placeholder;
                }
                absorbProc = false;
            }
        }
    }
    private void RicochetSkill(Character author, Character[] party, Character[] opponents)
    {
        bool absorbProc = false;
        Character target = Logic.RedirectDeflectLoop(Logic.SelectTarget(opponents), author, opponents, party, ref absorbProc);
        if (!party.Contains(target))
        {
            Character[] placeholder = party;
            party = opponents;
            opponents = placeholder;
        }
        //apply damage
        int attackValue = GetValue(author);
        if (absorbProc)
        {
            PetLogic.PetSelection(author, party, opponents);
            Logic.HitAbsorbed(attackValue, target);
        }
        else
        {
            Logic.DamageApplication(attackValue, target, author, party, opponents);
        }
        if (!party.Contains(author))
        {
            Character[] placeholder = party;
            party = opponents;
            opponents = placeholder;
        }
        absorbProc = false;
        for (int i = 0; i < 4; i++)
        {
            if (Logic.CountAlive(opponents) > 1)
            {
                target = Logic.RedirectDeflectLoop(Logic.SelectRicochet(opponents, target), author, opponents, party, ref absorbProc);
                if (!party.Contains(target))
                {
                    Character[] placeholder = party;
                    party = opponents;
                    opponents = placeholder;
                }
                //apply damage
                attackValue = GetValue(author);
                if (absorbProc)
                {
                    PetLogic.PetSelection(author, party, opponents);
                    Logic.HitAbsorbed(attackValue, target);
                }
                else
                {
                    Logic.DamageApplication(attackValue, target, author, party, opponents);
                }
                if (!party.Contains(author))
                {
                    Character[] placeholder = party;
                    party = opponents;
                    opponents = placeholder;
                }
                absorbProc = false;
            }
            else { break; }
        }
    }


}
