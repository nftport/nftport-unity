<img src="https://i.imgur.com/Ul1T01q.png" style="width:100%"/>
</br>

# NFTs owned by an account


Get NFTs owned by an account with options to include metadata, filter from particular collection and more. 
Component `NFTs_OwnedByAnAccount.cs` fetches all NFTs of an address and fills it in type [`NFTs_model`](https://docs.nftport.xyz/docs/nftport/ZG9jOjU0MzMyMDg0-nf-ts-model-cs)

-----

![NFTs_OwnedByAnAccount.cs](https://i.imgur.com/nJe4TrH.gif)

-----

### Usage in your script:

```csharp
using NFTPort;
```
</br>

```csharp
      NFTs_model NFTsOfUser;
        
        NFTs_OwnedByAnAccount
            .Initialize()
            .SetChain(NFTs_OwnedByAnAccount.Chains.ethereum)
            .SetAddress("0x9b37BC499De5e675063695211618F3Cd64A1B9Fc")
            .SetInclude(NFTs_OwnedByAnAccount.Includes.metadata)
            .SetFilterFromCollection("0xbc4ca0eda7647a8ab7c2061c2e118a18a936f13d")
            .OnError(error=>Debug.Log(error))
            .OnComplete(NFTs=> NFTsOfUser = NFTs)
            .Run();

```

Fetched Data can be used from the type of [`NFTs_model`](https://docs.nftport.xyz/docs/nftport/ZG9jOjU0MzMyMDg0-nf-ts-model-cs) , eg:
```csharp
var x = NFTsOfUser.nfts[4];
var y = NFTsOfUser.nfts[4].metadata.attributes[1].trait_type;
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

#### .SetAddress() 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set Account Address to retrieve NFTs from as string. </br>
<span style="float:right;color:red;font-weight:500;font-size:12px">required </span> 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramter:`string 	account_address`

#### .SetChain()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set blockchain from which to query NFTs. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramter: Choose from `NFTs_OwnedByAnAccount.Chains`

#### .SetContinuation()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set continuation from last request. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramter:`string` , can be passed empty to override. Pass continuation string from last API call respose to get next batch of NFTs. One API call is limited to 50 NFT data.

#### .SetInclude()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Include optional data in the response. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramter: Choose from `NFTs_OwnedByAnAccount.Includes`

#### .SetFilterFromCollection()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Filter by and return NFTs only from the given contract address/collection as string. Leave blank or do not call to include all NFTs of the account. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramter:`string 	contract_address`

#### .OnError()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on Error providing string containing the error information.

#### .OnComplete()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on succesfull API Fetch providing type `NFTs_model` with filled data. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Use: `.OnComplete(NFTs=> NFTsOfUser = NFTs)` , where `NFTsOfUser` is [`NFTs_model`](https://docs.nftport.xyz/docs/nftport/ZG9jOjU0MzMyMDg0-nf-ts-model-cs).

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
- [Retrieve NFTs owned by an account API](../../1.json/paths/~1v0~1accounts~1{account_address}/get) 
















