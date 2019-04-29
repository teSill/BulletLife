using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade {

    private string _upgradeName;
    private string _upgradeDescription;
    private int _upgradeCost;

    public Upgrade(string upgradeName, string upgradeDescription, int upgradeCost) {
        _upgradeName = upgradeName;
        _upgradeDescription = upgradeDescription;
        _upgradeCost = upgradeCost;
    }

    public string GetName() {
        return _upgradeName;
    }

    public string GetDescription() {
        return _upgradeDescription;
    }

    public int GetCost() {
        return _upgradeCost;
    }

    public void UpdateCost() {
        _upgradeCost *= 4;
    }

}
