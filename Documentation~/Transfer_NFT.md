

# Customizable minted | Transfer NFT

`Transfer_NFT.cs` Transfers an NFT which has been previously minted with [Customizable minting](https://docs.nftport.xyz/docs/nftport/ZG9jOjYzMDIzNDgx-customizable-minting) to another wallet address.

Note: Transfer is possible only if the token is owned by the contract owner and the token has not been transferred/sold yet.

![Transfer_NFT.cs](https://i.imgur.com/lrRS76J.jpg)

-----

### Usage in your script:

```csharp
using NFTPort;
```
</br>

```csharp 
Transfer_NFT
    .Initialize()
    .SetChain(Transfer_NFT.Chains.polygon)
    .SetParameters(
        contract_address: "0x42b57de948d05d17fb11d4e527df80a0420a4392", 
        token_id: "1",
        transfer_to_address: "0x3691Ca2c8D2051f0B8b9d4aCb8941771aBc1bf9b"
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
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`string 	transfer_to_address`Wallet address to which the NFT will be transferred. </br>


#### .SetChain()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set blockchain. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramter: Choose from `Transfer_NFT.Chains`


#### .OnError()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on Error providing string containing the error information.

#### .OnComplete()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on success providing type `Minted_model` with filled data. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Use: `.OnComplete(Minted=> minted = Minted)` , where `minted` is of type`Minted_model`

#### .Run()
<span style="float:right;color:red;font-weight:500;font-size:12px">required </span> 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Runs the feature and transfers the NFT ⊂(▀¯▀⊂).

</br>


-----

### Related Examples and Resources
- [Minted_model Schema](https://docs.nftport.xyz/docs/nftport/ZG9jOjU1MDM4OTg1-minted-model-cs)










