using _Project.Scripts.Infrastructure.Services;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.AssetManagement
{
    public interface IAssetProvider : 
        IService
    {
        GameObject LoadAsset(string assetPath);
    }
}