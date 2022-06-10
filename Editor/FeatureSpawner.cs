using NFTPort.Internal;
using UnityEditor;
using UnityEngine;

namespace NFTPort.Editor
{

    public class FeatureSpawner : EditorWindow
    {
        
        //GameObject
        private const string GameObjMenu = "GameObject/NFTPort/";
        
        [MenuItem(PortConstants.BaseFeatureSpawnerMenu + PortConstants.FeatureName_NFT_Details)]
        [MenuItem(GameObjMenu + PortConstants.FeatureName_NFT_Details)]
        static void Spawn_NFTDetails()
        {
            Selection.activeGameObject= new GameObject(PortConstants.FeatureName_NFT_Details).AddComponent<NFT_Details>().gameObject;
        }
        
        [MenuItem(PortConstants.BaseFeatureSpawnerMenu + PortConstants.FeatureName_NFTs_OfAccount)]
        [MenuItem(GameObjMenu + PortConstants.FeatureName_NFTs_OfAccount)]
        static void Spawn_NFtsOfAccount()
        {
            Selection.activeGameObject= new GameObject(PortConstants.FeatureName_NFTs_OfAccount).AddComponent<NFTs_OwnedByAnAccount>().gameObject;
        }
        
        [MenuItem(PortConstants.BaseFeatureSpawnerMenu + PortConstants.FeatureName_NFTs_OfContract)]
        [MenuItem(GameObjMenu + PortConstants.FeatureName_NFTs_OfContract)]
        static void Spawn_NFtsOfContract()
        {
            Selection.activeGameObject= new GameObject(PortConstants.FeatureName_NFTs_OfContract).AddComponent<NFTs_OfAContract>().gameObject;
        }
        
        [MenuItem(PortConstants.BaseFeatureSpawnerMenu + PortConstants.FeatureName_StorageFiles)]
        [MenuItem(GameObjMenu + PortConstants.FeatureName_StorageFiles)]
        static void Spawn_StorageFile()
        {
            Selection.activeGameObject= new GameObject(PortConstants.FeatureName_StorageFiles).AddComponent<Storage_UploadFile>().gameObject;
        }
        
        [MenuItem(PortConstants.BaseFeatureSpawnerMenu + PortConstants.FeatureName_StorageMetadata)]
        [MenuItem(GameObjMenu + PortConstants.FeatureName_StorageMetadata)]
        static void Spawn_StorageMetadata()
        {
            Selection.activeGameObject= new GameObject(PortConstants.FeatureName_StorageMetadata).AddComponent<Storage_UploadMetadata>().gameObject;
        }
        
        [MenuItem(PortConstants.BaseFeatureSpawnerMenu + PortConstants.FeatureName_AssetDownloader)]
        [MenuItem(GameObjMenu + PortConstants.FeatureName_AssetDownloader)]
        static void Spawn_AssetDownloader()
        {
            Selection.activeGameObject= new GameObject(PortConstants.FeatureName_AssetDownloader).AddComponent<AssetDownloader>().gameObject;
        }
        
        [MenuItem(PortConstants.BaseFeatureSpawnerMenu + PortConstants.FeatureName_ConnectUserWallet)]
        [MenuItem(GameObjMenu + PortConstants.FeatureName_ConnectUserWallet)]
        static void Spawn_ConnectWallet()
        {
            Selection.activeGameObject= new GameObject(PortConstants.FeatureName_ConnectUserWallet).AddComponent<ConnectPlayerWallet>().gameObject;
        }
        
        [MenuItem(PortConstants.BaseFeatureSpawnerMenu + PortConstants.FeatureName_Mint_Custom)]
        [MenuItem(GameObjMenu + PortConstants.FeatureName_Mint_Custom)]
        static void Spawn_Mint_Custom()
        {
            Selection.activeGameObject= new GameObject(PortConstants.FeatureName_Mint_Custom).AddComponent<Mint_Custom>().gameObject;
        }
        
        [MenuItem(PortConstants.BaseFeatureSpawnerMenu + PortConstants.FeatureName_Mint_URL)]
        [MenuItem(GameObjMenu + PortConstants.FeatureName_Mint_URL)]
        static void Spawn_Mint_URL()
        {
            Selection.activeGameObject= new GameObject(PortConstants.FeatureName_Mint_URL).AddComponent<Mint_URL>().gameObject;
        }
        /*
        [MenuItem(PortConstants.BaseFeatureSpawnerMenu + PortConstants.FeatureName_Deploy)]
        [MenuItem(GameObjMenu + PortConstants.FeatureName_Deploy)]
        static void Spawn_Deploy()
        {
            Selection.activeGameObject= new GameObject(PortConstants.FeatureName_Deploy).AddComponent<Deploy>().gameObject;
        }
        */
        [MenuItem(PortConstants.BaseFeatureSpawnerMenu + PortConstants.FeatureName_Txn_Account)]
        [MenuItem(GameObjMenu + PortConstants.FeatureName_Txn_Account)]
        static void Spawn_Txn_Account()
        {
            Selection.activeGameObject= new GameObject(PortConstants.FeatureName_Txn_Account).AddComponent<Txn_Account>().gameObject;
        }



    }

}
