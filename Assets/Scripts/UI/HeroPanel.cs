﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroPanel : MonoBehaviour
{
    // References
    public Dropdown predefined;
    
    // Base Stats
    public InputField power;
    public InputField stamina;
    public InputField agility;
    
    // Specials
    public InputField critChance;
    public InputField critDamage;
    public InputField dsChance;
    public InputField blockChance;
    public InputField evadeChance;
    public InputField deflectChance;
    public InputField absorbChance;
    public InputField damageReduction;

    // Runes
    public InputField powerRunes;
    public InputField staminaRunes;
    public InputField agilityRunes;
    public InputField empowerRunes;

    // Pet
    public Dropdown pet;
    public Dropdown petProcType;
    public Dropdown petLevel;
    public Dropdown weapon;
    public Dropdown metaRune;



    // mythic bonuses


    // set bonuses


    public Dropdown Set_1;
    public Dropdown Set_2;
    public Dropdown Set_3;

    public Dropdown Myth_1;
    public Dropdown Myth_2;
    public Dropdown Myth_3;
    public Dropdown Myth_4;
    public Dropdown Myth_5;
    public Dropdown Myth_6;

    public Dropdown SetPieceCount_1;
    public Dropdown SetPieceCount_2;
    public Dropdown SetPieceCount_3;

    public Toggle GateKeeperBonus;



    public void ShowProcTypeOnSpecificPet()
    {
        if (pet.options[pet.value].text == PetType.Toebert.ToString() ||
            pet.options[pet.value].text == PetType.Urgoff.ToString()  ||
            pet.options[pet.value].text == PetType.Roogamenz.ToString()) 

        {
            petProcType.gameObject.SetActive(true);
        }
        else
        {
            petProcType.gameObject.SetActive(false);
        }
    }

    // Awake
    public void Awake()
    {
        // Predefines
        List<string> predefines = new List<string>() { "None" };
        foreach (KeyValuePair<string, Character> keyValuePair in Character.predefined)
        {
            predefines.Add(keyValuePair.Key);
        }
        predefined.ClearOptions();
        predefined.AddOptions(predefines);

        // Pets
        pet.ClearOptions();
        pet.AddOptions(new List<string>(Enum.GetNames(typeof(PetType))));
        petProcType.ClearOptions();
        petProcType.AddOptions(new List<string>(Enum.GetNames(typeof(PetProcType))));
        petProcType.gameObject.SetActive(false);

        // Weapons
        weapon.ClearOptions();
        weapon.AddOptions(new List<string>(Enum.GetNames(typeof(Character.Weapon))));

        //meta
        metaRune.ClearOptions();
        metaRune.AddOptions(new List<string>(Enum.GetNames(typeof(Character.MetaRune))));

        //set 1
        Set_1.ClearOptions();
        Set_1.AddOptions(new List<string>(Enum.GetNames(typeof(SetBonus))));

        Set_2.ClearOptions();
        Set_2.AddOptions(new List<string>(Enum.GetNames(typeof(SetBonus))));

        Set_3.ClearOptions();
        Set_3.AddOptions(new List<string>(Enum.GetNames(typeof(SetBonus))));

        Myth_1.ClearOptions();
        Myth_1.AddOptions(new List<string>(Enum.GetNames(typeof(MythicBonus))));
        Myth_2.ClearOptions();
        Myth_2.AddOptions(new List<string>(Enum.GetNames(typeof(MythicBonus))));
        Myth_3.ClearOptions();
        Myth_3.AddOptions(new List<string>(Enum.GetNames(typeof(MythicBonus))));
        Myth_4.ClearOptions();
        Myth_4.AddOptions(new List<string>(Enum.GetNames(typeof(MythicBonus))));
        Myth_5.ClearOptions();
        Myth_5.AddOptions(new List<string>(Enum.GetNames(typeof(MythicBonus))));
        Myth_6.ClearOptions();
        Myth_6.AddOptions(new List<string>(Enum.GetNames(typeof(MythicBonus))));


        ////Divinity
        //DivinityBonus.ClearOptions();
        //DivinityBonus.AddOptions(new List<string>(Enum.GetNames(typeof(Character.DivinityBonus))));

        ////oblit
        //ObliterationBonus.ClearOptions();
        //ObliterationBonus.AddOptions(new List<string>(Enum.GetNames(typeof(Character.ObliterationBonus))));
        ////maru
        //MaruBonus.ClearOptions();
        //MaruBonus.AddOptions(new List<string>(Enum.GetNames(typeof(Character.MARUBonus))));
        ////conduc
        //ConductionBonus.ClearOptions();
        //ConductionBonus.AddOptions(new List<string>(Enum.GetNames(typeof(Character.ConductionBonus))));
        ////illus
        //IllustriousBonus.ClearOptions();
        //IllustriousBonus.AddOptions(new List<string>(Enum.GetNames(typeof(Character.IllustriousBonus))));
    }

    // Predefine Switch
    public void PredefinedSwitch()
    {
        string heroName = predefined.options[predefined.value].text;

        // Hero Load
        Character hero;
        if (heroName == "None")
        {
            // Empty Hero
            hero = new Character();
        }
        else
        {
            hero = Character.GetPredefined(heroName);
        }

		SetFieldsFromHero(hero);
    }

	public void SetFieldsFromHero(Character hero)
	{
		// Base Stats
		power.text           = Convert.ToString(hero.power);
		stamina.text         = Convert.ToString(hero.stamina);
		agility.text         = Convert.ToString(hero.agility);
		// Specials
		critChance.text      = Convert.ToString(hero.critChance);
		critDamage.text      = Convert.ToString(hero.critDamage);
		dsChance.text        = Convert.ToString(hero.dsChance);
		blockChance.text     = Convert.ToString(hero.blockChance);
		evadeChance.text     = Convert.ToString(hero.evadeChance);
		deflectChance.text   = Convert.ToString(hero.deflectChance);
		absorbChance.text    = Convert.ToString(hero.absorbChance);
        empowerRunes.text    = Convert.ToString(hero.empowerChance);
        damageReduction.text = Convert.ToString(hero.damageReduction);
        //set bonuses
        //UnityBonus.isOn    = hero.unity;
        //AresBonus.isOn = hero.aresBonus;
        //BushidoBonus.isOn = hero.bushidoBonus;
        //LunarBonus.isOn = hero.lunarBonus;
        GateKeeperBonus.isOn = hero.gateKeeperBonus;



        //mythic bonuses
        //NecrosisBonus.isOn = hero.necrosisBonus;
        //HysteriaBonus.isOn = hero.hysteriaBonus;
        //NightVisageBonus.isOn = hero.nightVisageBonus;
        //ConsumptionBonus.isOn = hero.consumptionBonus;
        //DecayBonus.isOn = hero.decayBonus;


        // Runes
        powerRunes.text    = Convert.ToString(hero.powerRunes);
		staminaRunes.text  = Convert.ToString(hero.staminaRunes);
		agilityRunes.text  = Convert.ToString(hero.agilityRunes);
		// Pet
		for (int i = 0; i < pet.options.Count; i++)
		{
			if (pet.options[i].text == hero.petName.ToString())
			{
				pet.value = i;
				break;
			}
		}
        for (int i = 0; i < petLevel.options.Count; i++)
        {
            if (petLevel.options[i].text == hero.PetLevel.ToString())
            {
                petLevel.value = i;
            }
        }
        for (int i = 0; i < petProcType.options.Count; i++)
        {
            if (petProcType.options[i].text == hero.petProcType.ToString())
            {
                petProcType.value = i;
            }
        }


        //weapon
        for (int i = 0; i < weapon.options.Count; i++)
		{
			if (weapon.options[i].text == hero.weapon.ToString())
			{
				weapon.value = i;
				break;
			}
		}

		// meta bonus
		for (int i = 0; i < metaRune.options.Count; i++)
		{
			if (metaRune.options[i].text == hero.metaRune.ToString())
			{
				metaRune.value = i;
				break;
			}
		}
        for (int i = 0; i < Set_1.options.Count; i++)
        {
            if (Set_1.options[i].text == hero.setArray[0].GetBonus().ToString())
            {
                Set_1.value = i;
                break;
            }
        }
        for (int i = 0; i < SetPieceCount_1.options.Count; i++)
        {
            if (i == hero.setArray[0].GetPieceCount() - 2)
            {
                SetPieceCount_1.value = i;
                break;
            }
        }

        for (int i = 0; i < Set_2.options.Count; i++)
        {
            if (Set_2.options[i].text == hero.setArray[1].GetBonus().ToString())
            {
                Set_2.value = i;
                break;
            }
        }
        for(int i = 0; i < SetPieceCount_2.options.Count; i++)
        {
            if (i == hero.setArray[1].GetPieceCount() - 2)
            {
                SetPieceCount_2.value = i;
                break;
            }
        }

        for (int i = 0; i < Set_3.options.Count; i++)
        {
            if (Set_3.options[i].text == hero.setArray[2].GetBonus().ToString())
            {
                Set_3.value = i;
                break;
            }
        }
        for (int i = 0; i < SetPieceCount_3.options.Count; i++)
        {
            if (i == hero.setArray[2].GetPieceCount() - 2)
            {
                SetPieceCount_3.value = i;
                break;
            }
        }

        for (int i = 0; i < Myth_1.options.Count; i++)
        {
            if (Myth_1.options[i].text == hero.mythicArray[0].ToString())
            {
                Myth_1.value = i;
                break;
            }
        }
        for (int i = 0; i < Myth_2.options.Count; i++)
        {
            if (Myth_2.options[i].text == hero.mythicArray[1].ToString())
            {
                Myth_2.value = i;
                break;
            }
        }
        for (int i = 0; i < Myth_3.options.Count; i++)
        {
            if (Myth_3.options[i].text == hero.mythicArray[2].ToString())
            {
                Myth_3.value = i;
                break;
            }
        }
        for (int i = 0; i < Myth_4.options.Count; i++)
        {
            if (Myth_4.options[i].text == hero.mythicArray[3].ToString())
            {
                Myth_4.value = i;
                break;
            }
        }
        for (int i = 0; i < Myth_5.options.Count; i++)
        {
            if (Myth_5.options[i].text == hero.mythicArray[4].ToString())
            {
                Myth_5.value = i;
                break;
            }
        }
        for (int i = 0; i < Myth_6.options.Count; i++)
        {
            if (Myth_6.options[i].text == hero.mythicArray[5].ToString())
            {
                Myth_6.value = i;
                break;
            }
        }


    }

    // Return a Hero struct
    public Character GetHero()
    {
        return new Character {
            // Base Stats
            power = Convert.ToInt32(power.text),
            stamina = Convert.ToInt32(stamina.text),
            agility = Convert.ToInt32(agility.text),
            // Specials
            critChance = Convert.ToSingle(critChance.text),
            critDamage = Convert.ToSingle(critDamage.text),
            dsChance = Convert.ToSingle(dsChance.text),
            blockChance = Convert.ToSingle(blockChance.text),
            evadeChance = Convert.ToSingle(evadeChance.text),
            deflectChance = Convert.ToSingle(deflectChance.text),
            absorbChance = Convert.ToSingle(absorbChance.text),
            empowerChance = Convert.ToSingle(empowerRunes.text),
            damageReduction = Convert.ToSingle(damageReduction.text),
            // Runes
            powerRunes = Convert.ToSingle(powerRunes.text),
            staminaRunes = Convert.ToSingle(staminaRunes.text),
            agilityRunes = Convert.ToSingle(agilityRunes.text),
            //Set Bonuses
            bonusHealing = 1f,
            quadChance = 0f,
            meterlessChance = 0f,
            gateKeeperBonus = GateKeeperBonus.isOn,

            setArray = new Set[] 
            {
                new Set(GetSetBonusFromString(Set_1.options[Set_1.value].text), SetPieceCount_1.value + 2),
                new Set(GetSetBonusFromString(Set_2.options[Set_2.value].text), SetPieceCount_2.value + 2),
                new Set(GetSetBonusFromString(Set_3.options[Set_3.value].text), SetPieceCount_3.value + 2)},
            mythicArray = new MythicBonus[]
            {
                GetMythicBonusFromString(Myth_1.options[Myth_1.value].text),
                GetMythicBonusFromString(Myth_2.options[Myth_2.value].text),
                GetMythicBonusFromString(Myth_3.options[Myth_3.value].text),
                GetMythicBonusFromString(Myth_4.options[Myth_4.value].text),
                GetMythicBonusFromString(Myth_5.options[Myth_5.value].text),
                GetMythicBonusFromString(Myth_6.options[Myth_6.value].text)
            },

            

            //Mythic

            //necrosisBonus = NecrosisBonus.isOn,
            //hysteriaBonus = HysteriaBonus.isOn,
            //nightVisageBonus = NightVisageBonus.isOn,
            //consumptionBonus = ConsumptionBonus.isOn,
            //decayBonus = DecayBonus.isOn,


        // Pet
            metaRune        = GetMetaRuneFromString(metaRune.options[metaRune.value].text),
			petName         = GetPetFromString(pet.options[pet.value].text),
            petProcType     = GetProcTypeFromString(petProcType.options[petProcType.value].text),
            PetLevel        = GetPetLevelFromString(petLevel.options[petLevel.value].text),
			weapon          = GetWeaponFromString(weapon.options[weapon.value].text),
            _isHero         = true
        };
    }
    /*
	public static Character.DivinityBonus GetDivinityBonusFromString(String s)
	{
		Character.DivinityBonus divinityBonus;

		switch (s)
		{
			case "Bonus_2_of_3":
				divinityBonus = Character.DivinityBonus.Bonus_2_of_3;
				break;
			case "Bonus_3_of_3":
				divinityBonus = Character.DivinityBonus.Bonus_3_of_3;
				break;
			default:
				divinityBonus = Character.DivinityBonus.None;
				break;
		}

		return divinityBonus;
	}

    public static Character.ObliterationBonus GetOblitBonusFromString(String s)
    {
        Character.ObliterationBonus oblitBonus;

        switch (s)
        {
            case "Bonus_2_of_4":
                oblitBonus = Character.ObliterationBonus.Bonus_2_of_4;
                break;
            case "Bonus_3_of_4":
                oblitBonus = Character.ObliterationBonus.Bonus_3_of_4;
                break;
            case "Bonus_4_of_4":
                oblitBonus = Character.ObliterationBonus.Bonus_4_of_4;
                break;
            default:
                oblitBonus = Character.ObliterationBonus.None;
                break;
        }

        return oblitBonus;
    }

    public static Character.MARUBonus GetMarutBonusFromString(String s)
    {
        Character.MARUBonus maruBonus;

        switch (s)
        {
            case "Bonus_2_of_4":
                maruBonus = Character.MARUBonus.Bonus_2_of_4;
                break;
            case "Bonus_3_of_4":
                maruBonus = Character.MARUBonus.Bonus_3_of_4;
                break;
            case "Bonus_4_of_4":
                maruBonus = Character.MARUBonus.Bonus_4_of_4;
                break;
            default:
                maruBonus = Character.MARUBonus.None;
                break;
        }

        return maruBonus;
    }

    public static Character.ConductionBonus GetConducBonusFromString(String s)
    {
        Character.ConductionBonus conducBonus;

        switch (s)
        {
            case "Bonus_2_of_4":
                conducBonus = Character.ConductionBonus.Bonus_2_of_4;
                break;
            case "Bonus_3_of_4":
                conducBonus = Character.ConductionBonus.Bonus_3_of_4;
                break;
            case "Bonus_4_of_4":
                conducBonus = Character.ConductionBonus.Bonus_4_of_4;
                break;
            default:
                conducBonus = Character.ConductionBonus.None;
                break;
        }

        return conducBonus;
    }

    public static Character.TatersBonus GetTatersBonusFromString(String s)
    {
        Character.TatersBonus tatersBonus;

        switch (s)
        {
            case "Bonus_2_of_3":
                tatersBonus = Character.TatersBonus.Bonus_2_of_3;
                break;
            case "Bonus_3_of_3":
                tatersBonus = Character.TatersBonus.Bonus_3_of_3;
                break;
            default:
                tatersBonus = Character.TatersBonus.None;
                break;
        }

        return tatersBonus;
    }

    public static Character.IllustriousBonus GetIllustBonusFromString(String s)
    {
        Character.IllustriousBonus illustBonus;

        switch (s)
        {
            case "Bonus_2_of_3":
                illustBonus = Character.IllustriousBonus.Bonus_2_of_3;
                break;
            case "Bonus_3_of_3":
                illustBonus = Character.IllustriousBonus.Bonus_3_of_3;
                break;
            default:
                illustBonus = Character.IllustriousBonus.None;
                break;
        }

        return illustBonus;
    }
    */
    public static SetBonus GetSetBonusFromString(String s)
    {
        SetBonus setBonus = SetBonus.None;
        switch (s)
        {
            case "AresBonus":
                setBonus = SetBonus.AresBonus;
                break;
            case "DivinityBonus":
                setBonus = SetBonus.DivinityBonus;
                break;
            case "MaruBonus":
                setBonus = SetBonus.MaruBonus;
                break;
            case "NWBonus":
                setBonus = SetBonus.NWBonus;
                break;
            case "ArsenalBonus":
                setBonus = SetBonus.ArsenalBonus;
                break;

            //Trials
            case "UnityBonus":
                setBonus = SetBonus.UnityBonus;
                break;
            case "TrugdorBonus":
                setBonus = SetBonus.TrugdorBonus;
                break;
            case "BushidoBonus":
                setBonus = SetBonus.BushidoBonus;
                break;
            case "TaldBonus":
                setBonus = SetBonus.TaldBonus;
                break;
            case "ConducBonus":
                setBonus = SetBonus.ConducBonus;
                break;
            case "LuminaryBonus":
                setBonus = SetBonus.LuminaryBonus;
                break;
            case "PolarisBonus":
                setBonus = SetBonus.PolarisBonus;
                break;

            //WB orlag
            case "Lunarbonus":
                setBonus = SetBonus.Lunarbonus;
                break;
            case "JynxBonus":
                setBonus = SetBonus.JynxBonus;
                break;
            case "OblitBonus":
                setBonus = SetBonus.OblitBonus;
                break;
            case "AgonyBonus":
                setBonus = SetBonus.AgonyBonus;
                break;
            case "EruptionBonus":
                setBonus = SetBonus.EruptionBonus;
                break;

            //WB nether
            case "IllustriousBonus":
                setBonus = SetBonus.IllustriousBonus;
                break;
            case "TatersBonus":
                setBonus = SetBonus.TatersBonus;
                break;
            case "InfernoBonus":
                setBonus = SetBonus.InfernoBonus;
                break;
            case "RequiemBonus":
                setBonus = SetBonus.RequiemBonus;
                break;
        }
        return setBonus;
    }

    public static MythicBonus GetMythicBonusFromString(String s)
    {
        MythicBonus mythicBonus = MythicBonus.None;

        switch (s)
        {

            case "Pewpew":
                mythicBonus = MythicBonus.Pewpew;
                break;
            case "Hysteria_not_Implemented":
                mythicBonus = MythicBonus.Hysteria_not_Implemented;
                break;
            case "Bub":
                mythicBonus = MythicBonus.Bub;
                break;
            case "Supersition":
                mythicBonus = MythicBonus.Supersition;
                break;
            case "NightVisage":
                mythicBonus = MythicBonus.NightVisage;
                break;
            case "Consumption":
                mythicBonus = MythicBonus.Consumption;
                break;
            case "Decay":
                mythicBonus = MythicBonus.Decay;
                break;
            case "Necrosis":
                mythicBonus = MythicBonus.Necrosis;
                break;
            case "Cometfell":
                mythicBonus = MythicBonus.Cometfell;
                break;
            case "Nebuleye_Not_Implemented":
                mythicBonus = MythicBonus.Nebuleye_Not_Implemented;
                break;
            case "HoodOfMenace":
                mythicBonus = MythicBonus.HoodOfMenace;
                break;
            case "CryptTunic":
                mythicBonus = MythicBonus.CryptTunic;
                break;
            case "FishNBarrel":
                mythicBonus = MythicBonus.FishNBarrel;
                break;
            case "EngulfintArtifact":
                mythicBonus = MythicBonus.EngulfintArtifact;
                break;
            case "Nemesis":
                mythicBonus = MythicBonus.Nemesis;
                break;
            case "Bedlam":
                mythicBonus = MythicBonus.Bedlam;
                break;
            case "MoonCollage":
                mythicBonus = MythicBonus.MoonCollage;
                break;
            case "LavaDefender":
                mythicBonus = MythicBonus.LavaDefender;
                break;
            case "DeweDecal":
                mythicBonus = MythicBonus.DeweDecal;
                break;
            case "MagMasher":
                mythicBonus = MythicBonus.MagMasher;
                break;
            case "ShiftingBreeze":
                mythicBonus = MythicBonus.ShiftingBreeze;
                break;
            case "BrightStar":
                mythicBonus = MythicBonus.BrightStar;
                break;
            case "Veilage":
                mythicBonus = MythicBonus.Veilage;
                break;
            case "Flickerate":
                mythicBonus = MythicBonus.Flickerate;
                break;
        }

        return mythicBonus;
    }

    public static Character.MetaRune GetMetaRuneFromString(String s)
	{
		Character.MetaRune metaRune;

		switch (s)
		{
			case "Redirect":
				metaRune = Character.MetaRune.Redirect;
				break;
            case "spRegen":
                metaRune = Character.MetaRune.spRegen;
                break;
            default:
				metaRune = Character.MetaRune.None;
				break;
		}

		return metaRune;
	}

	public static PetType GetPetFromString(String s)
	{
		PetType pet;

		switch (s)
		{
            case "Nelson":
                pet = PetType.Nelson;
                break;
            case "Gemmi":
                pet = PetType.Gemmi;
                break;
            case "Boogie":
                pet = PetType.Boogie;
                break;
            case "Nemo":
                pet = PetType.Nemo;
                break;
            case "Crem":
                pet = PetType.Crem;
                break;
            case "Boiguh":
                pet = PetType.Boiguh;
                break;
            case "Nerder":
                pet = PetType.Nerder;
                break;
            case "Quimby":
                pet = PetType.Quimby;
                break;
            case "Snut":
                pet = PetType.Snut;
                break;
            case "Wuvboi":
                pet = PetType.Wuvboi;
                break;
            case "Buvboi":
                pet = PetType.Buvboi;
                break;
            case "Skulldemort":
                pet = PetType.Skulldemort;
                break;
            case "Toebert":
                pet = PetType.Toebert;
                break;
            case "Urgoff":
                pet = PetType.Urgoff;
                break;
            case "Roogamenz":
                pet = PetType.Roogamenz;
                break;
            case "Fuvboi":
                pet = PetType.Fuvboi;
                break;
            case "Karlorr":
                pet = PetType.Karlorr;
                break;
            case "Pumkwim":
                pet = PetType.Pumkwim;
                break;
            case "EpicBoogie":
                pet = PetType.EpicBoogie;
                break;
            case "EpicNemo":
                pet = PetType.EpicNemo;
                break;
            case "EpicNerder":
                pet = PetType.EpicNerder;
                break;
            case "EpicPumkwim":
                pet = PetType.EpicPumkwim;
                break;
            case "EpicCrem":
                pet = PetType.EpicCrem;
                break;
            case "EpicSnut":
                pet = PetType.EpicSnut;
                break;
            case "Pritza":
                pet = PetType.Pritza;
                break;
            case "Sparklez":
                pet = PetType.Sparklez;
                break;
            case "Dug":
                pet = PetType.Dug;
                break;
            case "Gumgum":
                pet = PetType.Gumgum;
                break;
            case "Phony":
                pet = PetType.Phony;
                break;
            case "Waldo":
                pet = PetType.Waldo;
                break;
            case "Beanz":
                pet = PetType.Beanz;
                break;
            case "Rutledge":
                pet = PetType.Rutledge;
                break;
            case "Log":
                pet = PetType.Log;
                break;
            case "Melvin":
                pet = PetType.Melvin;
                break;
            case "Bryan":
                pet = PetType.Bryan;
                break;
            case "Nacl":
                pet = PetType.Nacl;
                break;
            case "Mewwo":
                pet = PetType.Mewwo;
                break;
            case "Gusty":
                pet = PetType.Gusty;
                break;
            case "Dot":
                pet = PetType.Dot;
                break;



            default:
				pet =PetType.None;
				break;
		}

		return pet;
	}

    public static PetProcType GetProcTypeFromString(String s)
    {
        PetProcType petProcType = PetProcType.AllType;
        switch (s)
        {
            case "PerHit":
                petProcType = PetProcType.PerHit;
                break;
            case "GetHit":
                petProcType = PetProcType.GetHit;
                break;
            case "PerTurn":
                petProcType = PetProcType.PerTurn;
                break;
            case "AllType":
                petProcType = PetProcType.AllType;
                break;
        }
        return petProcType;
    }

    public static int GetPetLevelFromString(String s)
    {
        return Int32.Parse(s);
    }

	public static Character.Weapon GetWeaponFromString(String s)
	{
		Character.Weapon weapon;
        
        switch (s)
		{
			case "Axe":
				weapon = Character.Weapon.Axe;
				break;
			case "Bow":
				weapon = Character.Weapon.Bow;
				break;
			case "Spear":
				weapon = Character.Weapon.Spear;
				break;
			case "Staff":
				weapon = Character.Weapon.Staff;
				break;
			case "Sword":
				weapon = Character.Weapon.Sword;
				break;
            case "Laser":
                weapon = Character.Weapon.Laser;
                break;
            case "LaserSet":
                weapon = Character.Weapon.LaserSet;
                break;
            case "DemonStaff":
                weapon = Character.Weapon.DemonStaff;
                break;
            case "Harvester":
                weapon = Character.Weapon.Harvester;
                break;
            case "ShieldStaff":
                weapon = Character.Weapon.ShieldStaff;
                break;
            case "YakBlade":
                weapon = Character.Weapon.YakBlade;
                break;
            default:
				weapon = Character.Weapon.None;
				break;
		}
        return weapon;
	}
}
