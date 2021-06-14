//StreamingAssets Loader

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AssetLoader : IncludeAsset
{
    [HideInInspector] public int stackId;

    public struct InitProcedureRelay
    {
        public enum AudioAssets { stacking, requesting, initializing, ready, error};
        public enum TextureAssets {stacking, requesting, initializing, ready, error};
        public enum TextAssets {stacking, requesting, initializing, ready, error};
    }

    public InitProcedureRelay.AudioAssets audioAssets;
    public InitProcedureRelay.TextureAssets textureAssets;
    public InitProcedureRelay.TextAssets textAssets;

    public UnityWebRequest uwr;

    private Dictionary<GameObject, string> assets = new Dictionary<GameObject, string>();
    private List<int> assetTypes = new List<int>();

    //Do not run anything relating to asset InitProcedures in awake, awake is used by IncludeAsset.cs for stacking.
    private void Awake()
    {
        Debug.Log("Clearing assets dictionary!");
        assets.Clear();
        Debug.Log("StreamingAssets data path: " + Application.streamingAssetsPath);
    }

    public override void VirtualAwake()
    {
        return;
    }

    public override void VirtualStart()
    {
        return;
    }

    public void AssetStackPush(GameObject go, string relpath) //Push a gameObject to the asset stack
    {
        IncludeAsset includerRequest = go.GetComponent<IncludeAsset>();

        switch (includerRequest.assetType)
        {
            case AssetType.audio:
                audioAssets = InitProcedureRelay.AudioAssets.stacking;
                assetTypes.Add(0);
                break;

            case AssetType.texture:
                textureAssets = InitProcedureRelay.TextureAssets.stacking;
                assetTypes.Add(1);
                break;

            case AssetType.text:
                textAssets = InitProcedureRelay.TextAssets.stacking;
                assetTypes.Add(2);
                break;

            default:
                stackId++;
                assets.Add(go, string.Empty);
                assetTypes.Add(0xDEAD);
                Debug.LogError("Unkown asset type: " + includerRequest.assetType + " can not be initialized. Index is 0xDEAD");
                return;
        }

        stackId++;

        assets.Add(go, relpath);
        Debug.Log("Asset " + go + " @ " + relpath + " with stack id: " + stackId + " was pushed to the asset stack");
    }

    public void AssetStackPop(GameObject go) //Pop a gameObject off of the asset stack
    {
        assets.Remove(go);
        Debug.Log("Asset " + go + " was popped off the asset stack");
    }

    public void Initialize()
    {
        foreach(KeyValuePair<GameObject, string> asset in assets)
        {
            StartCoroutine(InitProcedure(asset));
        }

        //assetsLoaded = true;
        Debug.Log("All assets loaded!", gameObject);
    }

    public static string FileLocation(string relPath)
    {
        string path = Application.streamingAssetsPath + relPath;
        Debug.Log("Searching: " + path);
        return path;
    }

    IEnumerator InitProcedure(KeyValuePair<GameObject, string> asset)
    {
        using (uwr = UnityWebRequestTexture.GetTexture(FileLocation(asset.Value)))
        {
            textureAssets = InitProcedureRelay.TextureAssets.requesting;
            yield return uwr.SendWebRequest();

            if(uwr.isNetworkError || uwr.isHttpError)
            {
                textureAssets = InitProcedureRelay.TextureAssets.error;

                Debug.LogError(uwr.error);
                
                int response = NativeWinAlert.Alert(
                    "Asset Streaming Error",
                    "An asset could not be found at the following filepath:\n\n" + Application.streamingAssetsPath + asset.Value + "\n\n" + Application.productName + " requires an internet connection to stream game assets. Please verify that you are connected to the internet and try again. If the problem persists, make sure that you have the asset '" + asset.Value + "' in your StreamingAssets directory.\n\nIf you still see this error try re-installing the " + Application.productName + " and report the issue to the issue tracker at https://github.com/IBXCODECAT/Falling/issues and attatch the debug log output file located in \n\n" + Application.consoleLogPath + "\n\nNote that you may continue to play the game in offline mode by choosing 'continue', but the game will be missing some functionality and visuals. (Not recomended)",
                    NativeWinAlert.Options.cancelRetryContinue,
                    NativeWinAlert.Icons.error
                    );

                Debug.Log("NativeWinAlertResponse: " + response);

                switch(response)
                {
                    case 2: //Cancel
                        Debug.Log("Quitting the application.");
                        Application.Quit();
                        break;
                    case 10: //Retry
                        Debug.Log("Trying again...");
                        Initialize();
                        break;
                    default:
                        NativeWinAlert.Alert("Missing Assets", Application.productName + " is about to load a level with missing assets. The game may not function properly.", NativeWinAlert.Options.ok, NativeWinAlert.Icons.warn);
                        break;
                }
            }
            else
            {
                textureAssets = InitProcedureRelay.TextureAssets.initializing;

                Debug.Log("Attempting to load asset '" + asset + "' as raw image.");
                asset.Key.GetComponent<RawImage>().texture = DownloadHandlerTexture.GetContent(uwr);
                Debug.Log("Asset " + asset + " loaded");
            }
        }
    }
}
