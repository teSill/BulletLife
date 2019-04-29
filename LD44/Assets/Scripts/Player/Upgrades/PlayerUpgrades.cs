using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour {

    private Player _player;

    public int AmmoUpgradesPurchased { get; set; }
    public static readonly int MaxAmmoPurchases = 4;

    private void Start() {
        _player = GetComponent<Player>();
        AmmoUpgradesPurchased = 1;
    }

    public bool HasArmourUpgrade() {
        UpgradeController upgradeController = GameObject.FindGameObjectWithTag("DataController").GetComponent<UpgradeController>();
        return _player.unlockedUpgrades.Contains(upgradeController.FindUpgradeByName("Indestructible"));
    }

    public bool BlockAttack() {
        if (!HasArmourUpgrade())
            return false;

        int number = Random.Range(1, 5);
        if (number == 1) { // 25%
            Debug.Log("Succesfully blocked enemy attack.");
            return true;
        }
        return false;
    }

}
