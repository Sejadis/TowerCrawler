using System.Collections.Generic;
using System.Linq;
using SejDev.Systems.Abilities;
using SejDev.Systems.UI;
using SejDev.UI;
using UnityEngine;

public class AbilityScreen : UIScreen
{
    List<AbilityElement> holders = new List<AbilityElement>();

    [SerializeField] private UpgradeScreen upgradeScreen;
    [SerializeField] private GameObject abilityHolderPrefab;
    [SerializeField] private GameObject dragPrefab;
    [SerializeField] private GameObject abilityParent;
    [SerializeField] private AbilityScreenSlot core1;
    [SerializeField] private AbilityScreenSlot core2;
    [SerializeField] private AbilityScreenSlot core3;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var ability in ResourceManager.Instance.abilityLists.FirstOrDefault()?.abilities)
        {
            var go = Instantiate(abilityHolderPrefab, abilityParent.transform);
            var holder = go.GetComponent<AbilityElement>();
            if (holder != null)
            {
                holder.OnElementClicked += OnAbilityElementClicked;
                holder.Bind(ability);
                holders.Add(holder);
            }
        }

        LoadAbilities();
    }

    private void OnAbilityElementClicked(object sender, Ability e)
    {
        upgradeScreen.Show();
        upgradeScreen.CreateFromUpgradeTree(e.upgradeTree);
    }

    private void LoadAbilities()
    {
        core1.Ability = AbilityManager.Core1;
        core2.Ability = AbilityManager.Core2;
        core3.Ability = AbilityManager.Core3;
    }
}