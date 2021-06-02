 //Utility Script that works with the AssetLoader.cs
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncludeAsset : MonoBehaviour
{
    public enum AssetType { texture, audio, text}
    
    [SerializeField] public AssetType assetType;
    
    [SerializeField] private string assetPath;

    AssetLoader assetLoader;

    private void Awake() { VirtualAwake(); }
    private void Start() { VirtualStart(); }

    public virtual void VirtualAwake()
    {
        assetLoader = FindObjectOfType<UnityEngine.EventSystems.EventSystem>().GetComponent<AssetLoader>();
        assetLoader.AssetStackPush(gameObject, assetPath);
    }

    public virtual void VirtualStart()
    {
        assetLoader.Initialize();
    }
}
