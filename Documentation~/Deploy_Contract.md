
<img src="https://i.imgur.com/qTzlofW.png" style="width:100%"/>
</br>

# Deploy collection

`Deploy.cs`, deploy product contracts for your NFT collections. View [Customizable Minting](https://docs.nftport.xyz/docs/nftport/ZG9jOjYzMDIzNDgx-customizable-minting) to mint NFTs to your contract in game to any wallet.


-----

![Deploy.cs](https://i.imgur.com/DScchWO.gif)


 >  You can also directly use this component in editor as shown above via filling in the values and using the buttons provided. 
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
  Deploy
    .Initialize()
    .SetChain(Deploy.Chains.polygon)
    .SetParameters(
        name: "This is my collection Name",
        symbol: "SYM",
        owner_address: "0x3691Ca2c8D2051f0B8b9d4aCb8941771aBc1bf9b",
        metadata_updatable: true,
        type: Deploy.Type.erc721,
        royalties_share: 100,
        royalties_address: "0x3691Ca2c8D2051f0B8b9d4aCb8941771aBc1bf9b"
    )
    .OnError(error=>Debug.Log(error))
    .OnComplete(minted=> Debug.Log(minted.transaction_external_url)
    .Run();

```
On a success, response is filled in `Minted_model minted` . [View related schema](https://docs.nftport.xyz/docs/nftport/ZG9jOjU1MDM4OTg1-minted-model-cs). View your contract address at `minted.transaction_external_url`.

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
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramters:</br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`string 	name`: Symbol for the contract. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`string 	symbol`: Metadata URI obtained from metadata upload feature</br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`string 	owner_address`: Owner of the contract. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`bool 	metadata_updatable`: Set this to true if you want to create dynamic NFTs. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`Type 	type`: Choose contract type from class `Deploy.Type`. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`int 	royalties_share`: Secondary market royalty rate in basis points (100 bps = 1%). This value cannot exceed 10,000 bps. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`string 	royalties_address`: Address for royalties. Defaults to owner_address if not set. </br>


#### .SetChain()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set blockchain to deploy the contract on. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramter: Choose from `Deploy.Chains`


#### .OnError()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on Error providing string containing the error information.

#### .OnComplete()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on succesfull API Fetch providing type `Minted_model` with filled data. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Use: `.OnComplete(Minted=> minted = Minted)` , where `minted` is of type`Minted_model`

#### .Run()
<span style="float:right;color:red;font-weight:500;font-size:12px">required </span> 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Runs the feature and deploys ヾ(*▼・▼)ﾉ⌒☆.

</br>

-----


</br>

-----

### Related Examples and Resources
- [Examples: Customizable minting Sample](https://docs.nftport.xyz/docs/nftport/ZG9jOjUzMzQxMzcy-examples#easy-mint-with-url-sample)
- [Minted_model Schema](https://docs.nftport.xyz/docs/nftport/ZG9jOjU1MDM4OTg1-minted-model-cs)
- [Deploy a contract for NFT products API](https://docs.nftport.xyz/docs/nftport/b3A6MjE0MDYzNzU-deploy-a-contract-for-nft-products) 









