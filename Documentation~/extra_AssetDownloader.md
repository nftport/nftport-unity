# AssetDownloader

Component `AssetDownloader.cs` provides functions to fetch NFT Images, Videos, Audio etc. Useful to use with data endpoints providing the raw URLs.

![AssetDL_image](https://i.imgur.com/TQjscnJ.gif)

---

URL type both IPFS and HTTP is supported, it will be auto detected.

### Usage

```csharp
using NFTPort;
```

#### Get Image

```csharp

Texture2D texture;

  AssetDownloader.GetImage
    .Initialize()
    .OnError(error => Debug.Log(error))
    //.OnComplete(NFTtexture => texture = NFTtexture) //You can use this if you need just Texture2D in a linear function or you can use OnCompleteReturnLinkedNft if you're processing multiple Nfts.
    .OnCompleteReturnLinkedNft(nft,ReturnedNft => DoSomethingwithNft(ReturnedNft))
    .OnAllAssetDownloadersDone(x => Debug.Log(x))
    .Download(nft.cached_file_url);
    //.Download("https://storage.googleapis.com/sentinel-nft/raw-assets/87f6d27a39496a5c0ce66f1a3fb70bb9666dc237ac114522fd622db4a648d1e6.jpeg");


void DoSomethingwithNft(Nft nft)
{
    var texture2D = nft.assets.image_texture; 
}

```
#### Detemine URL Content Type
```csharp
AssetDownloader.DetemineURLContentType
    .Initialize()
    .OnError(error => NFTsCheckedPlus())
    .OnComplete((ReturnedNft, contentType) => HandleAssetContentType(ReturnedNft, contentType))
    .Run(nft, nft.cached_animation_url);


void HandleAssetContentType(Nft nft, string contentType)
{
   //You can handle asset according to type here, view Metaverse template sample. We recommend gltFast for Glb processing.
   if (contentType.Contains("gltf"){}
   if (contentType.Contains("gif"){}
   if (contentType.Contains("video"){}
   if (contentType.Contains("audio"){}
   if (contentType.Contains("image"){}
   if (contentType.Contains("image/png"){}
}
  ```

</br>

---


This can be used in conjunction with NFT Data endpoints like:

> It is recommended to use "cached_file_url" provided via data end points for fast downloads.

```csharp
    NFTs_OwnedByAnAccount
      .Initialize(destroyAtEnd:true)
      .SetChain(NFTs_OwnedByAnAccount.Chains.ethereum)
      .SetAddress("0x9b37BC499De5e675063695211618F3Cd64A1B9Fc")
      .SetInclude(NFTs_OwnedByAnAccount.Includes.metadata)
      .SetFilterFromContract("0xbc4ca0eda7647a8ab7c2061c2e118a18a936f13d")
      .OnError(error=>Debug.Log(error))
      .OnComplete(NFTs=> GetAssets(NFTs))
      .Run();


void GetAssets(NFTs_model NFTsOfUser)
{   

    //You may also use a foreach loop here to get asset for all NFTs and do checks like if Nft.name or nft.file_ur; exists
    AssetDownloader.GetImage
      .Initialize()
      .OnError(error => Debug.Log(error))
      .OnCompleteReturnLinkedNft(NFTsOfUser.nfts[0],Nft => DoSomethingWithNFT(Nft))
      //.OnComplete(NFTtexture => NFTsOfUser.nfts[0].assets.image_texture = NFTtexture)
      .OnAllAssetDownloadersDone(x => Debug.Log(x))
      .Download(NFTsOfUser.nfts[0].cached_file_url);
}

void DoSomethingWithNFT(Nft Nft))
{
 //.. now you have an NFT class with all metadata and image asset, use it to create objects, characters or more. 
}

```

</br>
View the AssetDownloader sample scene for more.

---

### Member Functions

Member Functions can be set as needed before `.Download()`

#### .Initialize()

      Initialize creates a game object and assigns this script as a component. </br>
      Optional parameter: bool `destroyAtEnd` : defines if this component will be destroyed after `.Download()`

> `Initialize()` must be called if you are not referencing the component any other way and it doesn't already exist in the scene.

#### .OnError()

      Action on Error providing string containing the error information.


#### .OnAllAssetDownloadersDone()

      Useful when initializing multiple AssetDownloaders. Action when all AssetDownloaders are finished. </br>

</br>

---

##### Get Image > </br></br>

#### .OnComplete()

      Action on succesfull Download providing type `Texture2D`. </br>

#### .OnCompleteReturnLinkedNft(Nft Nft, UnityAction<Nft> action)

      Action on succesfull Download returning class [`Nft`](https://docs.nftport.xyz/docs/nftport/ZG9jOjU0MzMyMDg0-nf-ts-model-cs) with attached Texture2D Asset. </br>


#### .Download()

<span style="float:right;color:red;font-weight:500;font-size:12px">required </span>
      Downloads the Asset.</br>
      Paramters:</br>
       `string  "FileURL"` </br>
       `bool 	isIPFS` , Set True if passing an IPFS raw URL. </br>

</br>

---

##### Detemine URL Content Type > </br></br>

####  .OnComplete((ReturnedNft, contentType) => HandleAssetContentType(ReturnedNft, contentType))

Action returning content type as string and Nft class if passed any. </br>

#### .Run(nft, nft.cached_animation_url)
<span style="float:right;color:red;font-weight:500;font-size:12px">required </span>
      Paramters:</br>
       `Nft  "nft"` nft class to send</br>
       `string 	URL` , Asset URL </br>

</br>


---

### Related Examples and Resources

- [Examples: Asset downloader Sample](https://docs.nftport.xyz/docs/nftport/ZG9jOjUzMzQxMzcy-examples#asset-downloader-sample)
- [NFTs_model](https://docs.nftport.xyz/docs/nftport/ZG9jOjU0MzMyMDg0-nf-ts-model-cs)
- [View NFT Unity Metaverse gallery template which showcases how to use Asset downloaders to process textures, Gifs, 3D Gltf/Glb assets and more](https://github.com/Worldsz/NFT-Unity3D-Metaverse-Template).  
  ![](https://i.imgur.com/GXuwbPB.gif)
