namespace NFTPort.Samples.AssetDownloader
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using NFTPort;

    public class DownloadingAssets_Sample : MonoBehaviour
    {
        public NFTs_model NFTsOfUser;
        void OnEnable()
        {
            GetNFTsOFAccount();
        }

        void GetNFTsOFAccount()
        {
            
            NFTs_OwnedByAnAccount
                .Initialize(destroyAtEnd:true)
                .SetChain(NFTs_OwnedByAnAccount.Chains.ethereum)
                .SetAddress("0x9b37BC499De5e675063695211618F3Cd64A1B9Fc")
                .SetInclude(NFTs_OwnedByAnAccount.Includes.metadata)
                .SetFilterFromContract("0xbc4ca0eda7647a8ab7c2061c2e118a18a936f13d")
                .OnError(error=>Debug.Log(error))
                .OnComplete(NFTs=> StartCoroutine(GetAssets(NFTs)))
                .Run();
        }
        
        IEnumerator GetAssets(NFTs_model _NFTsOfUser)
        {
            yield return new WaitForSeconds(0.5f);
            
            NFTsOfUser = _NFTsOfUser;
            
            foreach(var nft in NFTsOfUser.nfts)
            {
                AssetDownloader.GetImage
                    .Initialize()
                    .OnError(error => Debug.Log(error))
                    .OnComplete(NFTtexture => nft.assets.image_texture = NFTtexture)
                    .OnAllAssetDownloadersDone(x => SpawnTextures())
                    .Download(nft.cached_file_url, isIPFS: false);
            }
        }
        
        public GameObject Canvas;
        void SpawnTextures()
        {
            //Do something else with Asset >>> 
            foreach (var nft in NFTsOfUser.nfts)
            {
                //Spawn an Image Game Object with NFT texture
                var TextureObject = new GameObject($"{nft.token_id}").AddComponent<Image>();
                TextureObject.transform.SetParent(Canvas.transform);
                
                Texture2D texture = nft.assets.image_texture;
                TextureObject.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f));
                
                //Link Spawned GameObject to NFTs_model Asset GameObject field
                nft.assets.gameObject = TextureObject.gameObject;
                
            

                //mat.mainTexture = nft.assets.image_texture;
            }

        }
    }

}

