using Basis.Scripts.BasisSdk.Helpers;
using Basis.Scripts.UI.UI_Panels;
using System.Collections.Generic;
using UnityEngine;
public class BasisUIManagement : MonoBehaviour
{
    public List<BasisUIBase> basisUIBases = new List<BasisUIBase>();
    public static BasisUIManagement Instance;
    public void Awake()
    {
        if (BasisHelpers.CheckInstance(Instance))
        {
            Instance = this;
        }
    }
    public void AddUI(BasisUIBase BasisUIBase)
    {
        if (basisUIBases.Contains(BasisUIBase) == false)
        {
            BasisDebug.Log("adding Menu " + BasisUIBase.gameObject.name);
            basisUIBases.Add(BasisUIBase);
        }
        else
        {
            BasisDebug.LogError("Already has " + BasisUIBase.gameObject.name);
        }
    }
    public bool RemoveUI(BasisUIBase BasisUIBase)
    {
        if (BasisUIBase == null)
        {
            return false;
        }
        if (basisUIBases.Contains(BasisUIBase))
        {
            BasisDebug.Log("Remove Menu " + BasisUIBase.gameObject.name);
            basisUIBases.Remove(BasisUIBase);
            return true;
        }
        else
        {
            BasisDebug.LogError("trying to close UI that did not exist in the UI management ");
            return false;
        }
    }
    public void CloseAllMenus()
    {
        List<BasisUIBase> Copied = new List<BasisUIBase>();
        Copied.AddRange(basisUIBases);
        for (int Index = 0; Index < Copied.Count; Index++)
        {
            BasisUIBase menu = Copied[Index];
            if (menu)
            {
                menu.CloseThisMenu();
            }
        }
        basisUIBases.Clear();
    }
}
