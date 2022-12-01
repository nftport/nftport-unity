---
stoplight-id: ec87915fc7bbe
---

<img src="https://i.imgur.com/NfswoDc.png" style="width:100%"/>
</br>

# NFTs of a collection

Get NFTs of a contract/collection with its metadata.
Component `NFTs_OfACollection.cs` fetches all NFTs of a contract and fills it in type [`NFTs_model`](https://docs.nftport.xyz/docs/nftport/ZG9jOjU0MzMyMDg0-nf-ts-model-cs)

-----

![NFTs_OfACollection.cs](https://i.imgur.com/OM0OCdU.gif)

-----

### Usage in your script:

```csharp
using NFTPort;
```
</br>

```csharp
      var collectionNFTs = new NFTs_model();
        
        NFTs_OfACollection
            .Initialize(destroyAtEnd:true)
            .SetChain(NFTs_OfACollection.Chains.polygon)
            .SetParameters(
                    collection: "0x42b57de948d05d17fb11d4e527df80a0420a4392", 
                    include: NFTs_OfACollection.Includes.all
                    )
            .OnError(error=>Debug.Log(error))
            .OnComplete(NFTs=> collectionNFTs = NFTs)
            .Run();

```

Fetched Data can be used from the type of [`NFTs_model`](https://docs.nftport.xyz/docs/nftport/ZG9jOjU0MzMyMDg0-nf-ts-model-cs) , eg:
```csharp
var a = collectionNFTs.nfts[4];
var b = collectionNFTs.nfts[4].metadata.attributes[1].trait_type;
```
view [Examples: Ownership Data Sample](https://docs.nftport.xyz/docs/nftport/).
</br>

-----
### Member Functions 

Member Functions can be set as needed before `.Run()`

#### .Initialize()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Initialize creates a gameobject and assings this script as a component. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Optional parameter: bool `destroyAtEnd` : defines if this component will be destroyed after `.Run()`

 >  `Initialize()` must be called if you are not referencing the component any other way and it doesn't already exist in the scene.  

#### .SetParameters()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set collection contract address to retrieve NFTs from as string. </br>
<span style="float:right;color:red;font-weight:500;font-size:12px">required </span> 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramter:`string 	collection`
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramter: Choose from `NFTs_OfACollection.Includes`

#### .SetChain()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set blockchain from which to query NFTs. </br>

#### .SetContinuation()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set continuation from last request. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramter:`string` , can be passed empty to override, increment by number. For example first page 50 NFTs would be defaulted to 1, to get next 50 NFTs pass '2' and so on. One API call is limited to 50 NFT data.

#### .OnError()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on Error providing string containing the error information.

#### .OnComplete()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on succesfull API Fetch providing type `NFTs_model` with filled data. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Use: `.OnComplete(NFTs=> collectionNFTs = NFTs)` , where `collectionNFTs` is [`NFTs_model`](https://docs.nftport.xyz/docs/nftport/ZG9jOjU0MzMyMDg0-nf-ts-model-cs)

#### .Run()
<span style="float:right;color:red;font-weight:500;font-size:12px">required </span> 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Runs the Api call and fills the corresponding model in the component on success.

</br>

-----


When using an existing component in scene, UnityEvents `After Success`  or `After Error` can also be used accordingly, for example to update UI and perform other actions.



-----

### Related Examples and Resources
- [Examples: Ownership Data Sample](https://docs.nftport.xyz/docs/nftport/ZG9jOjUzMzQxMzcy-examples#ownership-data-sample)
- [Asset Downloader](https://docs.nftport.xyz/docs/nftport/ZG9jOjU2NjAzOTE0-asset-downloader)
- [NFTs_model](https://docs.nftport.xyz/docs/nftport/ZG9jOjU0MzMyMDg0-nf-ts-model-cs)
- [Retrieve contract NFTs API](../../1.json/paths/~1v0~1nfts~1{contract_address}/get) 
















