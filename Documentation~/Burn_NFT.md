
<img src="https://i.imgur.com/hUdWORu.png" style="width:100%"/>
</br>

# Customizable minted | Burn NFT

`Burn_NFT.cs` can be used to burn NFT of the previously minted NFT via [Customizable minting](https://docs.nftport.xyz/docs/nftport/ZG9jOjYzMDIzNDgx-customizable-minting). Useful to create timed claim or burn scenarios, burn unused NFTs in collection, etc. 

Burning an NFT means destroying it by sending it to a null (un-spendable) address. Transactions leading up to the burn will remain on the blockchain.

Note: Burning is possible only if the token is owned by the contract owner and the token has not been transferred/sold yet.

![Burn_NFT.cs](https://i.imgur.com/gXebHLc.jpg)

-----

### Usage in your script:

```csharp
using NFTPort;
```
</br>

```csharp 
Burn_NFT
    .Initialize()
    .SetChain(Burn_NFT.Chains.polygon)
    .SetParameters(
        contract_address: "0x42b57de948d05d17fb11d4e527df80a0420a4392", 
        token_id: "1"
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
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`string 	token_id` Token ID for the NFT </br>


#### .SetChain()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set blockchain. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramter: Choose from `Burn_NFT.Chains`


#### .OnError()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on Error providing string containing the error information.

#### .OnComplete()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on success providing type `Minted_model` with filled data. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Use: `.OnComplete(Minted=> minted = Minted)` , where `minted` is of type`Minted_model`

#### .Run()
<span style="float:right;color:red;font-weight:500;font-size:12px">required </span> 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Runs the feature and burns the NFT ⊂(▀¯▀⊂).

</br>


-----

### Related Examples and Resources
- [Examples: Customizable minting Sample](https://docs.nftport.xyz/docs/nftport/ZG9jOjUzMzQxMzcy-examples#easy-mint-with-url-sample)
- [Minted_model Schema](https://docs.nftport.xyz/docs/nftport/ZG9jOjU1MDM4OTg1-minted-model-cs)
- [Burn NFT API](https://docs.nftport.xyz/docs/nftport/b3A6Njg1NTI0MjE-burn-a-minted-nft) 









