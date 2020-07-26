using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SejDev.Systems.UI;
using SejDev.UI;
using UnityEngine;

public class AbilityScreen : UIScreen
{
    List<AbilityHolder> holders = new List<AbilityHolder>();

    [SerializeField] private GameObject abilityHolderPrefab;
    [SerializeField] private GameObject abilityParent;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var ability in ResourceManager.Instance.abilityLists.FirstOrDefault()?.abilities)
        {
            var go = Instantiate(abilityHolderPrefab, abilityParent.transform);
            var holder = go.GetComponent<AbilityHolder>();
            if (holder != null)
            {
                holder.Bind(ability);
                holders.Add(holder);
            }
        }
    }
}