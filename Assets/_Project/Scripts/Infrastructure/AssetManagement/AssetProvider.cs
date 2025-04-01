using UnityEngine;

namespace _Project.Scripts.Infrastructure.AssetManagement
{
    public class AssetProvider :
        IAssetProvider
    {
        public GameObject LoadAsset(string assetPath)
        {
            GameObject asset = Resources.Load<GameObject>(assetPath);
            return asset;
        }
    }
}