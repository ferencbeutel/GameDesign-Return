using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class Player : Damageable
{
    public Dialogue savedDialogue;
    public Dialogue loadedDialogue;

    public SpawnPoint spawnPoint;

    public RogersEnergyWeapon rogersEnergyWeapon;
    public RogersPlasmaWeapon rogersPlasmaWeapon;

    // interactables
    // level 001
    public Boolean activatedEnergySwitch_001 = false;
    // level 002
    public Boolean readDiary_002 = false;
    // level 003
    public Boolean activatedEnergySwitch_003 = false;

    // dialogues
    public Boolean seenEnergyTechupTutorial = false;
    // level 001
    public Boolean seenTutorialText_001 = false;
    public Boolean seenTownDialogue_001 = false;
    public Boolean seenEnergySwitchDialogue_001 = false;
    // level 003
    public Boolean seenHighJumpTutorial_003 = false;
    public Boolean seenEnergySwitchDialogue_003 = false;

    List<String> collectedItems = new List<String>();
    GameObject rogers;
    Damageable damageable;
    WeaponController weapon;
    String savefilePath;
    DialogueManager dialogueManager;

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
        saveData.roomToSpawnUUID = this.spawnPoint.room.uuid;

        // interactables
        saveData.activatedEnergySwitch_001 = this.activatedEnergySwitch_001;
        saveData.readDiary_002 = this.readDiary_002;
        saveData.activatedEnergySwitch_003 = this.activatedEnergySwitch_003;

        // dialogues
        saveData.seenEnergyTechupTutorial = this.seenEnergyTechupTutorial;
        saveData.seenTutorialText_001 = this.seenTutorialText_001;
        saveData.seenTownDialogue_001 = this.seenTownDialogue_001;
        saveData.seenEnergySwitchDialogue_001 = this.seenEnergySwitchDialogue_001;
        saveData.seenHighJumpTutorial_003 = this.seenHighJumpTutorial_003;
        saveData.seenEnergySwitchDialogue_003 = this.seenEnergySwitchDialogue_003;

        foreach (Weapon currentWeapon in weapon.weapons)
        {
            if (currentWeapon.GetType() == typeof(RogersPlasmaWeapon))
            {
                saveData.plasmaBeamCollected = true;
            }
        }
        saveData.highJumpTechUpCollected = false;

        binaryFormatter.Serialize(saveFile, saveData);
        saveFile.Close();

        dialogueManager.DisplayDialogue(savedDialogue, () => { });
    }

    public void Load()
    {
        if (File.Exists(savefilePath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream saveFile = File.Open(savefilePath, FileMode.Open);
            PlayerData saveData = (PlayerData)binaryFormatter.Deserialize(saveFile);

            SpawnPoint loadedSpawnPoint = DetermineSpawnPoint(saveData.roomToSpawnUUID);
            if (loadedSpawnPoint != null)
            {
                this.spawnPoint = loadedSpawnPoint;
            }
            else
            {
                Debug.LogError("Cannot find saved spawnpoint!");
            }

            this.collectedItems = saveData.collectedItems;

            // interactables
            this.activatedEnergySwitch_001 = saveData.activatedEnergySwitch_001;
            this.readDiary_002 = saveData.readDiary_002;
            this.activatedEnergySwitch_003 = saveData.activatedEnergySwitch_003;

            // dialogues
            this.seenEnergyTechupTutorial = saveData.seenEnergyTechupTutorial;
            this.seenTutorialText_001 = saveData.seenTutorialText_001;
            this.seenTownDialogue_001 = saveData.seenTownDialogue_001;
            this.seenEnergySwitchDialogue_001 = saveData.seenEnergySwitchDialogue_001;
            this.seenHighJumpTutorial_003 = saveData.seenHighJumpTutorial_003;
            this.seenEnergySwitchDialogue_003 = saveData.seenEnergySwitchDialogue_003;

            damageable.Load(saveData.currentHealth, saveData.maxHealth);

            if (saveData.plasmaBeamCollected)
            {
                weapon.AddWeapon(Instantiate(rogersPlasmaWeapon, new Vector2(0, 0), Quaternion.identity));
            }

            StartCoroutine(DisplayLoadedMessage());

            saveFile.Close();
        }
        else
        {
            // first time the game is started
            damageable.Load(damageable.maxHealth, damageable.maxHealth);
        }
    }

    public override void OnDeath()
    {
        GameOverInitializer gameOverInitializer = FindObjectOfType<GameOverInitializer>();
        gameOverInitializer.InitGameOver();
    }

    private SpawnPoint DetermineSpawnPoint(string roomToSpawnUUID)
    {
        var resources = Resources.LoadAll("Prefab/Levels", typeof(GameObject));

        foreach (GameObject gameObject in resources)
        {
            Room foundRoom = gameObject.GetComponent<Room>();
            if (foundRoom != null && foundRoom.uuid == roomToSpawnUUID)
            {
                return foundRoom.GetComponentInChildren<SpawnPoint>();
            }
        }

        return null;
    }

    private IEnumerator DisplayLoadedMessage()
    {
        yield return new WaitForSeconds(2);

        dialogueManager.DisplayDialogue(loadedDialogue, () => { });
    }

    private void Awake()
    {
        savefilePath = Application.persistentDataPath + "/save001.dat";
        rogers = GameObject.FindGameObjectWithTag("Player");
        damageable = rogers.GetComponent<Damageable>();
        weapon = rogers.GetComponentInChildren<WeaponController>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    [Serializable]
    class PlayerData
    {
        public float currentHealth;
        public float maxHealth;
        public List<String> collectedItems;
        public bool plasmaBeamCollected;
        public bool highJumpTechUpCollected;
        public String roomToSpawnUUID;

        // interactables
        // level 001
        public bool activatedEnergySwitch_001;
        // level 002
        public bool readDiary_002;
        // level 003
        public bool activatedEnergySwitch_003;

        // dialogues
        public bool seenEnergyTechupTutorial;
        // level 001
        public bool seenTutorialText_001;
        public bool seenTownDialogue_001;
        public bool seenEnergySwitchDialogue_001;
        // level 003
        public bool seenHighJumpTutorial_003;
        public bool seenEnergySwitchDialogue_003;
    }
}
