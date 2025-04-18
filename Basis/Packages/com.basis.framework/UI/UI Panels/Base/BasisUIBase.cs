using Basis.Scripts.Addressable_Driver;
using Basis.Scripts.Addressable_Driver.Factory;
using Basis.Scripts.BasisSdk.Helpers;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Basis.Scripts.UI.UI_Panels
{
    public abstract class BasisUIBase : MonoBehaviour
    {
        public AddressableGenericResource LoadedMenu;
        public abstract void InitalizeEvent();
        public abstract void DestroyEvent();
        public void CloseThisMenu()
        {
            BasisUIManagement.Instance.RemoveUI(this);
            AddressableLoadFactory.ReleaseResource(LoadedMenu);
            DestroyEvent();
            Destroy(this.gameObject);
        }
        public static async Task OpenThisMenu(AddressableGenericResource resource)
        {
            await AddressableLoadFactory.LoadAddressableResourceAsync<GameObject>(resource);
            GameObject Result = (GameObject)resource.Handles[0].Result;
            Result = GameObject.Instantiate(Result, BasisUIManagement.Instance.transform);
            BasisUIBase BasisUIBase = BasisHelpers.GetOrAddComponent<BasisUIBase>(Result);
            BasisUIManagement.Instance.AddUI(BasisUIBase);
            BasisUIBase.InitalizeEvent();
        }
        public static BasisUIBase OpenMenuNow(AddressableGenericResource AddressableGenericResource)
        {
            UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> op = Addressables.LoadAssetAsync<GameObject>(AddressableGenericResource.Key);
            GameObject RAC = op.WaitForCompletion();
            GameObject Result = GameObject.Instantiate(RAC, BasisUIManagement.Instance.transform);
            BasisUIBase BasisUIBase = BasisHelpers.GetOrAddComponent<BasisUIBase>(Result);
            BasisUIManagement.Instance.AddUI(BasisUIBase);
            BasisUIBase.InitalizeEvent();
            return BasisUIBase;
        }
    }
}
