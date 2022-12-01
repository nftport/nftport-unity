

<img src="https://i.imgur.com/TwyY5ND.png" style="width:100%"/>
</br>

# Customizable minted | Update NFT

`Update_NFT.cs` can be used to create dynamic NFTS, i.e. - update metadata of the NFT previously minted via custom mint feature. Useful to create upgrading or evolutionary game assets/objects/characters. A good example can be related to pokemon evolutions.

![Txn_NFT.cs](https://i.imgur.com/hZNVNiL.jpg)

-----

### Usage in your script:

```csharp
using NFTPort;
```
</br>

```csharp 
Update_NFT
    .Initialize()
    .SetChain(Update_NFT.Chains.polygon)
    .SetParameters(
        contract_address: "0x42b57de948d05d17fb11d4e527df80a0420a4392", 
        token_id: "1",
        metadata_uri: "ipfs://bafkreihkpgqmam5u4ze444lqo6ubtdyols5cxdwzix4wgxooggmqtk2ury",
        freeze_metadata: "false"
    )
    .OnError(error=>Debug.Log(error))
    .OnComplete(Minted => Debug.Log(Minted.transaction_external_url))
    .Run();

```
On success, response is filled in `Minted_model minted` . View related schema.

</br>

-----
### Member Functions 

Member Functions can be set as needed before `.Run()`

#### .Initialize()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Initialize creates a gameobject and assings this script as a component. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Optional parameter: bool `destroyAtEnd` : defines if this component will be destroyed after `.Run()`

 >  `Initialize()` must be called if you are not refrencing the component any other way and it doesn't already exists in the scene.  

#### .SetParameters()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set paramters </br>
<span style="float:right;color:red;font-weight:500;font-size:12px">required </span> 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramters:</br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`string 	contract_address` : Previously deployed contract address through your API. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`string 	metadata_uri` : New metadata URI, can be obtained from metadata upload feature</br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`string 	token_id` Token ID for the NFT </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`bool 	freeze_metadata` If true, freezes the specified NFT token URI and further token metadata updates are blocked. You can still change the base_uri on contract level with Update a deployed contract for NFT products. If you wish to freeze all updates, then set freeze_metadata as true in Update a deployed product contract. </br>

#### .SetChain()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set blockchain. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramter: Choose from `Update_NFT.Chains`


#### .OnError()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on Error providing string containing the error information.

#### .OnComplete()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on success providing type `Minted_model` with filled data. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Use: `.OnComplete(Minted=> minted = Minted)` , where `minted` is of type`Minted_model`

#### .Run()
<span style="float:right;color:red;font-weight:500;font-size:12px">required </span> 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Runs the feature and updates the NFT ⊂(▀¯▀⊂).

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
- [Update NFT API](../../3.json/paths/~1v0~1mints~1customizable/put) 









