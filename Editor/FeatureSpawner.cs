using NFTPort.Internal;
using UnityEditor;
using UnityEngine;

namespace NFTPort.Editor
{

    public class FeatureSpawner : EditorWindow
    {
        
        //GameObject
        private const string GameObjMenu = "GameObject/NFTPort/";
        
        
        [MenuItem(PortConstants.BaseFeatureSpawnerMenu + PortConstants.FeatureName_NFTs_OfAccount)]
        [MenuItem(GameObjMenu + PortConstants.FeatureName_NFTs_OfAccount)]
        static void Spawn_NFtsOfAccount()
        {
            new GameObject(PortConstants.FeatureName_NFTs_OfAccount).AddComponent<NFTs_OwnedByAnAccount>();
        }
        
        [MenuItem(PortConstants.BaseFeatureSpawnerMenu + PortConstants.FeatureName_NFTs_OfContract)]
        [MenuItem(GameObjMenu + PortConstants.FeatureName_NFTs_OfContract)]
        static void Spawn_NFtsOfContract()
        {
            new GameObject(PortConstants.FeatureName_NFTs_OfContract).AddComponent<NFTs_OfAContract>();
        }
        
        [MenuItem(PortConstants.BaseFeatureSpawnerMenu + PortConstants.FeatureName_StorageFiles)]
        [MenuItem(GameObjMenu + PortConstants.FeatureName_StorageFiles)]
        static void Spawn_StorageFile()
        {
            new GameObject(PortConstants.FeatureName_StorageFiles).AddComponent<Storage_UploadFile>();
        }
        
        [MenuItem(PortConstants.BaseFeatureSpawnerMenu + PortConstants.FeatureName_StorageMetadata)]
        [MenuItem(GameObjMenu + PortConstants.FeatureName_StorageMetadata)]
        static void Spawn_StorageMetadata()
        {
            new GameObject(PortConstants.FeatureName_StorageMetadata).AddComponent<Storage_UploadMetadata>();
        }
        
        [MenuItem(PortConstants.BaseFeatureSpawnerMenu + PortConstants.FeatureName_AssetDownloader)]
        [MenuItem(GameObjMenu + PortConstants.FeatureName_AssetDownloader)]
        static void FeatureName_AssetDownloader()
        {
            new GameObject(PortConstants.FeatureName_AssetDownloader).AddComponent<AssetDownloader>();
        }
        
        [MenuItem(PortConstants.BaseFeatureSpawnerMenu + PortConstants.FeatureName_ConnectUserWallet)]
        [MenuItem(GameObjMenu + PortConstants.FeatureName_ConnectUserWallet)]
        static void FeatureName_ConnectWallet()
        {
            new GameObject(PortConstants.FeatureName_ConnectUserWallet).AddComponent<ConnectPlayerWallet>();
        }
        
        [MenuItem(PortConstants.BaseFeatureSpawnerMenu + PortConstants.FeatureName_Mint_Custom)]
        [MenuItem(GameObjMenu + PortConstants.FeatureName_Mint_Custom)]
        static void FeatureName_Mint_Custom()
        {
            new GameObject(PortConstants.FeatureName_Mint_Custom).AddComponent<Mint_Custom>();
        }
        
        [MenuItem(PortConstants.BaseFeatureSpawnerMenu + PortConstants.FeatureName_Mint_URL)]
        [MenuItem(GameObjMenu + PortConstants.FeatureName_Mint_URL)]
        static void FeatureName_Mint_URL()
        {
            new GameObject(PortConstants.FeatureName_Mint_URL).AddComponent<Mint_URL>();
        }
        
        [MenuItem(PortConstants.BaseFeatureSpawnerMenu + PortConstants.FeatureName_Deploy)]
        [MenuItem(GameObjMenu + PortConstants.FeatureName_Deploy)]
        static void FeatureName_Deploy()
        {
            new GameObject(PortConstants.FeatureName_Deploy).AddComponent<Deploy>();
        }
        

   
    }

}
