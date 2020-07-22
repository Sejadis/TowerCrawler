using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SejDev.Systems.Abilities;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public List<AbilityList> abilityLists = new List<AbilityList>();
    public static ResourceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than 1 ResourceManager in the scene, destroying this", this);
            Destroy(this);
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public Ability GetAbilityByID(string id)
    {
        return abilityLists.Select(abilityList => abilityList.abilities.First(ability => ability.GUID.Equals(id)))
            .FirstOrDefault();
    }
}