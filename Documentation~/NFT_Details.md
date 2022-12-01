---
stoplight-id: xvi4otfxmt40e
---

<img src="https://i.imgur.com/ss0jt6v.png" style="width:100%"/>
</br>

# NFT Details

`NFT_Details.cs` Get all NFT details defining the token_id in a collection with metadata, current owner information and more. 

-----

![NFT_Details.cs](https://i.imgur.com/bgCsvTZ.gif)

-----

### Usage in your script:

```csharp
using NFTPort;
```
</br>

```csharp 
    NFT_Details
      .Initialize(destroyAtEnd:true)
      .SetChain(NFT_Details.Chains.polygon)
      .SetParameters(

          //for ethereum- EVM chains
          collection:"0x42b57de948d05d17fb11d4e527df80a0420a4392",
          token_id:"1",

          //for solana:
          //mint_address: "EH8AaTF9vNiW2poTKoQZKf6yqExYSrnoTxAd62TEfaWn",

          refresh_metadata:true
          )
      .OnError(error=>Debug.Log(error))
      .OnComplete(NFT=> DoThingsWithNFTData(NFT))
      .Run();
```
Fetched Data can be used from the type of [`NFTs_model`](https://docs.nftport.xyz/docs/nftport/ZG9jOjU0MzMyMDg0-nf-ts-model-cs) , eg:
 
```csharp
   public NFTs_model NFTOfUser = new NFTs_model(); 
```
</br>

```csharp
  void DoThingsWithNFTData(NFTs_model _NFTofUser)
    {
        var x = _NFTofUser.nft.metadata.description;
        var y = _NFTofUser.nft.cached_file_url;
        var z = _NFTofUser.nft.metadata.attributes.Count;

        Debug.Log(x);
        Debug.Log(y);
        Debug.Log(z);

        NFTOfUser = _NFTofUser;
    }
```


-----
### Member Functions 

Member Functions can be set as needed before `.Run()`

#### .Initialize()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Initialize creates a gameobject and assings this script as a component. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Optional parameter: bool `destroyAtEnd` : defines if this component will be destroyed after `.Run()`

 >  `Initialize()` must be called if you are not referencing the component any other way and it doesn't already exist in the scene. 

#### .SetChain()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set blockchain from which to query NFTs. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramter: Choose from `NFT_Details.Chains`


#### .SetParameters()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set paramters for easy mint NFT </br>
<span style="float:right;color:red;font-weight:500;font-size:12px">required </span> 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramters:</br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; for solana : 

        string mint_address

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; for ethereum : 

        string collection
        string token_id
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`bool 	refresh_metadata` To refresh metadata </br>


#### .OnError()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on Error providing string containing the error information.

#### .OnComplete()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on succesfull API Fetch providing type `NFTs_model` with filled data. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Use: `.OnComplete(NFTs=> NFTOfUser = NFTs)` , where `NFTOfUser` is [`NFTs_model`](https://docs.nftport.xyz/docs/nftport/ZG9jOjU0MzMyMDg0-nf-ts-model-cs).

#### .Run()
<span style="float:right;color:red;font-weight:500;font-size:12px">required </span> 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Runs the Api call and fills the corresponding model in the component on success.

</br>

-----

### Related Examples and Resources
- [Asset Downloader](https://docs.nftport.xyz/docs/nftport/ZG9jOjU2NjAzOTE0-asset-downloader)
- [NFTs_model](https://docs.nftport.xyz/docs/nftport/ZG9jOjU0MzMyMDg0-nf-ts-model-cs)
- [Retrieve NFT details API](../../1.json/paths/~1v0~1nfts~1{contract_address}~1{token_id}/get) 
















