using System;
using SejDev.Systems.Abilities;
using SejDev.Systems.Save;
using UnityEngine;

[Serializable]
public class EquippedAbilitySave : Save
{
    public string core1ID;
    public string core2ID;
    public string core3ID;

    public EquippedAbilitySave(string core1ID, string core2ID, string core3ID)
    {
        this.core1ID = core1ID;
        this.core2ID = core2ID;
        this.core3ID = core3ID;
        Debug.Log(core1ID + " " + core2ID + " " + core3ID);
    }

    public EquippedAbilitySave(Ability core1, Ability core2, Ability core3)
    {
        core1ID = core1?.GUID ?? string.Empty;
        core2ID = core2?.GUID ?? string.Empty;
        core3ID = core3?.GUID ?? string.Empty;
    }
}