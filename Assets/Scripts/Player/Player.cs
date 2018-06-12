﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class Player : Damageable
{

    public SpawnPoint spawnPoint;

    public RogersEnergyWeapon rogersEnergyWeapon;
    public RogersPlasmaWeapon rogersPlasmaWeapon;

    // interactables
    public Boolean readDiary = false;
    public Boolean activatedEnergySwitch = false;

    // dialogues
    public Boolean seenTutorialText = false;
    public Boolean seenTownDialogue = false;
    public Boolean seenEnergySwitchDialogue = false;
    public Boolean seenEntryDialogue = false;

    List<String> collectedItems = new List<String>();
    GameObject rogers;
    Damageable damageable;
    WeaponController weapon;
    String savefilePath;

    public void Spawn()
    {
        spawnPoint.Spawn();
    }

    public bool Collect(String uuid)
    {
        if (collectedItems.Contains(uuid))
        {
            Debug.Log("collected items: " + collectedItems);
            return false;
        }
        collectedItems.Add(uuid);
        return true;
    }

    public bool HasCollected(String uuid)
    {
        return collectedItems.Contains(uuid);
    }

    public void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream saveFile = File.Open(savefilePath, FileMode.OpenOrCreate);
        PlayerData saveData = new PlayerData();

        saveData.currentHealth = damageable.getCurrentHealth();
        saveData.maxHealth = damageable.getMaxHealth();
        saveData.collectedItems = this.collectedItems;
        saveData.spawnPoint = this.spawnPoint;

        // interactables
        saveData.readDiary = this.readDiary;
        saveData.activatedEnergySwitch = this.activatedEnergySwitch;

        // dialogues
        saveData.seenTutorialText = this.seenTutorialText;
        saveData.seenTownDialogue = this.seenTownDialogue;
        saveData.seenEnergySwitchDialogue = this.seenEnergySwitchDialogue;
        saveData.seenEntryDialogue = this.seenEntryDialogue;

        foreach (Weapon currentWeapon in weapon.weapons)
        {
            if (currentWeapon.GetType() == typeof(RogersEnergyWeapon))
            {
                saveData.energyBeamCollected = true;
            }
            if (currentWeapon.GetType() == typeof(RogersPlasmaWeapon))
            {
                saveData.plasmaBeamCollected = true;
            }
        }
        saveData.highJumpTechUpCollected = false;

        binaryFormatter.Serialize(saveFile, saveData);
        saveFile.Close();
    }

    public void Load()
    {
        if (File.Exists(savefilePath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream saveFile = File.Open(savefilePath, FileMode.Open);
            PlayerData saveData = (PlayerData)binaryFormatter.Deserialize(saveFile);

            this.spawnPoint = saveData.spawnPoint;
            this.collectedItems = saveData.collectedItems;

            // interactables
            this.readDiary = saveData.readDiary;
            this.activatedEnergySwitch = saveData.activatedEnergySwitch;

            // dialogues
            this.seenTutorialText = saveData.seenTutorialText;
            this.seenTownDialogue = saveData.seenTownDialogue;
            this.seenEnergySwitchDialogue = saveData.seenEnergySwitchDialogue;
            this.seenEntryDialogue = saveData.seenEntryDialogue;

            damageable.Load(saveData.currentHealth, saveData.maxHealth);

            if (saveData.energyBeamCollected)
            {
                weapon.AddWeapon(Instantiate(rogersEnergyWeapon, new Vector2(0, 0), Quaternion.identity));
            }
            if (saveData.plasmaBeamCollected)
            {
                weapon.AddWeapon(Instantiate(rogersPlasmaWeapon, new Vector2(0, 0), Quaternion.identity));
            }
            return;
        }

        // first time the game is started
        damageable.Load(damageable.maxHealth, damageable.maxHealth);
        weapon.AddWeapon(Instantiate(rogersEnergyWeapon, new Vector2(0, 0), Quaternion.identity));
    }

    private void Awake()
    {
        savefilePath = Application.persistentDataPath + "/save001.dat";
        rogers = GameObject.FindGameObjectWithTag("Player");
        damageable = rogers.GetComponent<Damageable>();
        weapon = rogers.GetComponentInChildren<WeaponController>();
    }

    public override void OnDeath()
    {
        GameOverInitializer gameOverInitializer = FindObjectOfType<GameOverInitializer>();
        gameOverInitializer.InitGameOver();
    }

    [Serializable]
    class PlayerData
    {
        public float currentHealth;
        public float maxHealth;
        public List<String> collectedItems;
        public bool energyBeamCollected;
        public bool plasmaBeamCollected;
        public bool highJumpTechUpCollected;
        public SpawnPoint spawnPoint;

        // interactables
        public bool readDiary;
        public bool activatedEnergySwitch;

        // dialogues
        public bool seenTutorialText;
        public bool seenTownDialogue;
        public bool seenEnergySwitchDialogue;
        public bool seenEntryDialogue;
    }
}
