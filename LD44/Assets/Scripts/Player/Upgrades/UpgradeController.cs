using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour {
    
    public List<Upgrade> upgrades = new List<Upgrade>();

    [SerializeField]
    private GameObject _ugpradePanelPrefab;
    [SerializeField]
    private GameObject _upgradePanelParent;

    private void Start() {
        AddUpgrades();
    }

    private void AddUpgrades() {
        upgrades.Add(new Upgrade("Double Barrel", "Increase the amount of bullets you fire", 30));
        upgrades.Add(new Upgrade("The All-seeing", "Increase your field of view", 75));
        upgrades.Add(new Upgrade("Indestructible", "Receive a 25% chance to block enemy damage", 150));
        upgrades.Add(new Upgrade("Gotta Go Fast", "Increase your movement speed", 100));
    }

    public void InstantiateUpgrades() {
        foreach(Upgrade upgrade in upgrades) {
            if(_upgradePanelParent.transform.Find(upgrade.GetName())) {
                string costText = _upgradePanelParent.transform.Find(upgrade.GetName()).transform.Find("ItemCostText").GetComponent<TextMeshProUGUI>().text;
                if (!costText.Equals("Cost: " + upgrade.GetCost())) {
                    _upgradePanelParent.transform.Find(upgrade.GetName()).transform.Find("ItemCostText").GetComponent<TextMeshProUGUI>().text = "Cost: " + upgrade.GetCost();
                }
                continue;
            }

            GameObject go = Instantiate(_ugpradePanelPrefab, _upgradePanelParent.transform);
            go.name = upgrade.GetName();
            go.transform.Find("ItemNameText").GetComponent<TextMeshProUGUI>().text = upgrade.GetName();
            go.transform.Find("ItemDescriptionText").GetComponent<TextMeshProUGUI>().text = upgrade.GetDescription();
            go.transform.Find("ItemCostText").GetComponent<TextMeshProUGUI>().text = "Cost: " + upgrade.GetCost();
            go.transform.Find("PurchaseButton").GetComponent<Button>().onClick.AddListener(delegate { PurchaseUpgrade(upgrade); } );
        }
    }

    public void PurchaseUpgrade(Upgrade upgrade) {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        switch(upgrade.GetName()) {
            case "Double Barrel":
                PurchaseDoubleBarrel(player, upgrade);
                break;
            case "The All-seeing":
                PurchaseFOW(player, upgrade);
                break;
            case "Indestructible":
                PurchaseArmour(player, upgrade);
                break;
            case "Gotta Go Fast":
                PurchaseSpeed(player, upgrade);
                break;
        }
        InstantiateUpgrades();
    }

    private void PurchaseDoubleBarrel(Player player, Upgrade upgrade) {
        if (player.Years < upgrade.GetCost()) {
            Debug.Log("No money");
            return;
        }

        Debug.Log("Purchased ammo");
        player.Years -= upgrade.GetCost();
        player.playerUpgrades.AmmoUpgradesPurchased++;
        upgrade.UpdateCost();
        if (player.playerUpgrades.AmmoUpgradesPurchased == PlayerUpgrades.MaxAmmoPurchases) {
            Destroy(_upgradePanelParent.transform.Find(upgrade.GetName()).gameObject);
            upgrades.Remove(upgrade);
        }
    }

    private void PurchaseFOW(Player player, Upgrade upgrade) {
        if (player.Years < upgrade.GetCost()) {
            Debug.Log("No money");
            return;
        }

        Debug.Log("Purchased field of view");
        Camera.main.orthographicSize++;
        player.Years -= upgrade.GetCost();
        player.unlockedUpgrades.Add(upgrade);
        upgrade.UpdateCost();
        if (Camera.main.orthographicSize == 10) {
            Destroy(_upgradePanelParent.transform.Find(upgrade.GetName()).gameObject);
            upgrades.Remove(upgrade);
        }
    }

    private void PurchaseArmour(Player player, Upgrade upgrade) {
        if (player.Years < upgrade.GetCost()) {
            Debug.Log("No money");
            return;
        }
        
        Debug.Log("Purchased armour");
        player.Years -= upgrade.GetCost();
        player.unlockedUpgrades.Add(upgrade);
        upgrades.Remove(upgrade);
        Destroy(_upgradePanelParent.transform.Find(upgrade.GetName()).gameObject);
    }

    private void PurchaseSpeed(Player player, Upgrade upgrade) {
        if (player.Years < upgrade.GetCost()) {
            Debug.Log("No money");
            return;
        }
        Debug.Log("Purchased movement speed");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.Speed++;
        player.Years -= upgrade.GetCost();
        player.unlockedUpgrades.Add(upgrade);
        upgrade.UpdateCost();
        if (playerMovement.Speed == PlayerMovement.MaxSpeed) {
            upgrades.Remove(upgrade);
            Destroy(_upgradePanelParent.transform.Find(upgrade.GetName()).gameObject);
        }
    }

    public Upgrade FindUpgradeByName(string name) {
        foreach(Upgrade upgrade in upgrades) {
            if (upgrade.GetName().Equals(name))
                return upgrade;
        }
        return null;
    }
}
