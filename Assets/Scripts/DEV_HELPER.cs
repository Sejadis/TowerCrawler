using System.Collections.Generic;
using SejDev.Player;
using SejDev.Systems.Crafting;
using SejDev.Systems.Equipment;
using SejDev.Systems.Stats;
using UnityEngine;

public class DEV_HELPER : MonoBehaviour
{
    public List<Equipment> equipment = new List<Equipment>();

    public List<CurrencyData> currency = new List<CurrencyData>();
    public CraftingBlueprint blueprint;

    public PlayerInventory playerInventory;
    public CraftingHandler craftingHandler;

    private IInventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = playerInventory.Inventory;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            var eq = (equipment[0] as Equipment);
            inventory.AddItem(StatRollManager.RollStats(eq));
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            var eq = (equipment[1] as Equipment);
            inventory.AddItem(StatRollManager.RollStats(eq));
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            var eq = (equipment[2] as Equipment);
            inventory.AddItem(StatRollManager.RollStats(eq));
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            var eq = (equipment[3] as Equipment);
            inventory.AddItem(StatRollManager.RollStats(eq));
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            var eq = (equipment[4] as Equipment);
            inventory.AddItem(StatRollManager.RollStats(eq));
        }


        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            inventory.AddCurrency(currency[0], 5);
        }

        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            inventory.AddCurrency(currency[1], 5);
        }

        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            inventory.AddCurrency(currency[2], 5);
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (craftingHandler.TryCraft(blueprint, out var item))
            {
                inventory.AddItem(item as Equipment);
            }
            else
            {
                Debug.Log("Could not craft item");
            }
        }
    }
}