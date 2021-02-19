using System.Collections.Generic;
using System.Linq;
using SejDev.Systems.Abilities;
using SejDev.Systems.Core;
using SejDev.Systems.UI;
using SejDev.UI;
using UnityEngine;

public class AbilityScreen : UIScreen
{
    private readonly List<AbilityElement> elements = new List<AbilityElement>();

    [SerializeField] private UpgradeScreen upgradeScreen;
    [SerializeField] private GameObject abilityElementPrefab;
    [SerializeField] private GameObject abilityParent;
    [SerializeField] private AbilityScreenSlot core1;
    [SerializeField] private AbilityScreenSlot core2;
    [SerializeField] private AbilityScreenSlot core3;
    [SerializeField] private ObjectDescriber describer;
    [SerializeField] private Transform dragParent;
    private IDescribable selectedDescribable;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var ability in ResourceManager.Instance.abilityLists.FirstOrDefault()?.abilities)
        {
            var go = Instantiate(abilityElementPrefab, abilityParent.transform);
            var element = go.GetComponent<AbilityElement>();
            if (element != null)
            {
                element.OnElementClicked += OnAbilityElementClicked;
                element.OnElementEnter += OnAbilityElementEnter;
                element.OnElementExit += OnAbilityElementExit;
                element.Bind(ability, dragParent);
                elements.Add(element);
            }
        }

        LoadAbilities();
    }

    private void OnAbilityElementExit(object sender, IDescribable e)
    {
        describer.Reset();
    }

    private void OnAbilityElementEnter(object sender, IDescribable e)
    {
        describer.Fill(e);
    }

    private void OnAbilityElementClicked(object sender, Ability e)
    {
        upgradeScreen.Show();
        upgradeScreen.CreateFromUpgradeTree(e.upgradeTree);
        describer.FallBackDescribable = e;
        describer.Fill(e);
    }

    private void LoadAbilities()
    {
        core1.Ability = AbilityManager.Core1;
        core2.Ability = AbilityManager.Core2;
        core3.Ability = AbilityManager.Core3;
    }
}