<img src="https://i.imgur.com/WOYirB2.png" style="width:100%"/>
</br>

# Customizable minting

`Mint_Custom.cs` Mint custom NFT's to any wallet at your deployed contract. Usefull to make anything into NFT including game objects, audio, textures, files, unity prefabs procedural objects and more with highly customisable metadata which can include procedural stats and data from the in-game moment NFT is minted on.

This involves multiple steps:

>View easy minting features for fast and easy integration if you do not wish to mint to your custom collection contract or include advanced metadata features.

1. [Deploy your contract](https://docs.nftport.xyz/docs/nftport/b3A6Njg1NTI0MTI-deploy-a-contract-for-nft-products) ( You only need to do this once per collection ).</br>
2. Using [Storage | File Upload](https://docs.nftport.xyz/docs/nftport/ZG9jOjYwODM0NTY3-storage-upload-file) to upload the files,</br>
3. Using [Storage | Metadata upload](https://docs.nftport.xyz/docs/nftport/ZG9jOjYwODM0NTY4-storage-upload-metadata) to create industry-standard metadata, including the ipfs_url from step 2,</br>
4. Mint using customizable minting, including metadata_uri from step 3.</br>

According to what your game needs you may need to also just do steps 2 and 3 once, if your game has unique / procedural objects, or upgradable NFT parameters according to users' progress you may modify the metadata accordingly while uploading via Storage | Metadata and file feature, and then Mint. All components within the SDK are composable and can be used with each other.

-----

![Mint_Custom.cs](https://i.imgur.com/Nc9C8XB.gif)


 >  You can also directly use this component in editor as shown above via filling in the values and using the buttons provided. [Upload Metadata](https://docs.nftport.xyz/docs/nftport/ZG9jOjYwODM0NTY4-storage-upload-metadata) to get `metadata_uri`. View related example.
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
 Mint_Custom
    .Initialize()
    .SetChain(Mint_Custom.Chains.polygon)
    .SetParameters(
        contract_address: "0x42B57de948D05d17Fb11d4E527DF80A0420A4392",
        metadata_uri: "ipfs://bafkreieggpzsz4i66iqv4vq6hpsgo3ucvisikerumcntp6ihurlpsur6jq",
        mintToAddress: "0x3691Ca2c8D2051f0B8b9d4aCb8941771aBc1bf9b",
        token_id: 0 //Set to 0 to mint to any available ID.
    )
    .OnError(error=>Debug.Log(error))
    .OnComplete(Minted=> minted = Minted)
    .Run();

```
On a succesful mint, response is filled in `Minted_model minted` . View related schema.

</br>

-----
### Member Functions 

Member Functions can be set as needed before `.Run()`

#### .Initialize()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Initialize creates a gameobject and assings this script as a component. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Optional parameter: bool `destroyAtEnd` : defines if this component will be destroyed after `.Run()`

 >  `Initialize()` must be called if you are not refrencing the component any other way and it doesn't already exists in the scene.  

#### .SetParameters()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set paramters for Custom NFT </br>
<span style="float:right;color:red;font-weight:500;font-size:12px">required </span> 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramters:</br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`string 	contract_address` : Previously deployed contract address through your API. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`string 	metadata_uri` : Metadata URI obtained from metadata upload feature</br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`string 	mintToAddress` : Blockchain address to mint to. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`string 	token_id` Optional Int Token ID for the NFT, Defaults to 0 to mint from any available slot. </br>

#### .SetChain()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set blockchain to mint NFT on. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramter: Choose from `Mint_Custom.Chains`


#### .OnError()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on Error providing string containing the error information.

#### .OnComplete()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on succesfull API Fetch providing type `Minted_model` with filled data. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Use: `.OnComplete(Minted=> minted = Minted)` , where `minted` is of type`Minted_model`

#### .Run()
<span style="float:right;color:red;font-weight:500;font-size:12px">required </span> 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Runs the feature and Mints ⊂(▀¯▀⊂).

</br>

-----

</br>

<img src="https://i.imgur.com/c0nnYbw.gif" style="width:100%"/>

Mint your own: https://nftport.github.io/nftport-unity-PlaygroundSample/

</br>

-----

### Related Examples and Resources
- [Examples: Customizable minting Sample](https://docs.nftport.xyz/docs/nftport/ZG9jOjUzMzQxMzcy-examples#easy-mint-with-url-sample)
- [Minted_model Schema](https://docs.nftport.xyz/docs/nftport/ZG9jOjU1MDM4OTg1-minted-model-cs)
- [Customizable minting API](../../3.json/paths/~1v0~1mints~1customizable/post) 









