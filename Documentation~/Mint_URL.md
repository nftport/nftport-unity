<img src="https://i.imgur.com/WqEQVDi.png" style="width:100%"/>
</br>

# Easy minting w/URL

`Mint_URL.cs` Easy Mint NFT using a file on URL. Fast and easy process to mint any file hosted at a URL to players wallet at any given moment in game. If you wish to upload procedural in-game objects view: [Easy Mint with File]() and [Custom Mint](https://docs.nftport.xyz/docs/nftport/ZG9jOjYzMDIzNDgx-customizable-minting).

-----

![NFTs_OfAContract.cs](https://i.imgur.com/BIbX2LI.gif)

-----

### Usage in your script:

```csharp
using NFTPort;
```
</br>
 
```csharp
 public Minted_model minted = new Minted_model();
```
</br>

```csharp 
    Mint_URL
    .Initialize()
    .SetChain(Mint_URL.Chains.polygon)
    .SetParameters(
        FileURL: "https://i.imgur.com/tzAbx5D.png",
        Name: "NFTPort.xyz",
        Description: "Easy Mint NFT with NFTPort Unity SDK",
        MintToAddress: "0xAb5801a7D398351b8bE11C439e05C5B3259aeC9B"
    )
    .OnError(error=>Debug.Log(error))
    .OnComplete(Minted=> minted = Minted)
    .Run();


```
On a succesful mint, response is filled in [`Minted_model minted`](https://docs.nftport.xyz/docs/nftport/ZG9jOjU1MDM4OTg1-minted-model-cs) . View related schema.

</br>

-----
### Member Functions 

Member Functions can be set as needed before `.Run()`

#### .Initialize()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Initialize creates a gameobject and assings this script as a component. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Optional parameter: bool `destroyAtEnd` : defines if this component will be destroyed after `.Run()`

 >  `Initialize()` must be called if you are not refrencing the component any other way and it doesn't already exists in the scene.  

#### .SetParameters()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set paramters for easy mint NFT </br>
<span style="float:right;color:red;font-weight:500;font-size:12px">required </span> 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramters:</br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`string 	FileURL` </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`string 	Name` </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`string 	Description` </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`string 	MintToAddress` </br>

#### .SetChain()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set blockchain to mint NFT on. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramter: Choose from `Mint_URL.Chains`


#### .OnError()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on Error providing string containing the error information.

#### .OnComplete()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on succesfull API Fetch providing type [`Minted_model`](https://docs.nftport.xyz/docs/nftport/ZG9jOjU1MDM4OTg1-minted-model-cs) with filled data. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Use: `.OnComplete(Minted=> minted = Minted)` , where `minted` is of type [`Minted_model`](https://docs.nftport.xyz/docs/nftport/ZG9jOjU1MDM4OTg1-minted-model-cs)

#### .Run()
<span style="float:right;color:red;font-weight:500;font-size:12px">required </span> 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Runs the Api call and mints.

</br>

-----
 > If you wish to customize the minting process e.g. use your own contract, add custom metadata, see [customizable minting](https://docs.nftport.xyz/docs/nftport/ZG9jOjYzMDIzNDgx-customizable-minting).

### Related Examples and Resources
- [Examples: Easy Mint Sample](https://docs.nftport.xyz/docs/nftport/ZG9jOjUzMzQxMzcy-examples#easy-mint-with-url-sample)
- [Minted_model Schema](https://docs.nftport.xyz/docs/nftport/ZG9jOjU1MDM4OTg1-minted-model-cs)
- [Easy minting w/URL API](../../3.json/paths/~1v0~1mints~1easy~1urls/post) 
















