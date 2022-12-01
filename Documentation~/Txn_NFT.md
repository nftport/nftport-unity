


<img src="https://i.imgur.com/o809eCZ.png" style="width:100%"/>
</br>

# Transaction Data | NFT

`Txn_NFT.cs` Get all NFT transactions

-----

![Txn_NFT.cs](https://i.imgur.com/xLH9HvN.gif)

-----

### Usage in your script:

```csharp
using NFTPort;
```
</br>

```csharp
public Txn_model txns;
```
</br>

```csharp 
Txn_NFT
    .Initialize(destroyAtEnd: true)
    .SetChain(Txn_NFT.Chains.solana)
    .SetParameters(
        type: Txn_NFT.Type.all,
        // for solana : 
        mint_address: "EH8AaTF9vNiW2poTKoQZKf6yqExYSrnoTxAd62TEfaWn"
        //for ethereum : 
        //collection: "0xbc4ca0eda7647a8ab7c2061c2e118a18a936f13d",
        //token_id: "7567",
    )
    .OnError(error=> Debug.Log(error))
    .OnComplete(Txns=> txns = Txns)
    .Run();
        

Debug.Log(txns.transactions[0].type);
```
Fetched Data can be used from `txns`.
</br>

-----
### Member Functions 

Member Functions can be set as needed before `.Run()`

#### .Initialize()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Initialize creates a gameobject and assings this script as a component. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Optional parameter: bool `destroyAtEnd` : defines if this component will be destroyed after `.Run()`

 >  `Initialize()` must be called if you are not referencing the component any other way and it doesn't already exist in the scene. 

#### .SetChain()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set blockchain from which to query. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramter: Choose from `Txn_Account.Chains`


#### .SetParameters()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set paramters </br>
<span style="float:right;color:red;font-weight:500;font-size:12px">required </span> 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramters:</br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; for solana : 

        string mint_address

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; for ethereum : 

        string contract_address
        int token_id

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`
Type 	type {
            all, mint, burn, transfer_from, transfer_to, list, buy, sell, make_bid , get_bid
        }` </br>



#### .OnError()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on Error providing string containing the error information.

#### .OnComplete()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on succesfull API Fetch providing type `Txn_model` with filled data. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Use: `.OnComplete(Txns=> txns = Txns)` , where `txns` is of type `Txn_model`.

#### .Run()
<span style="float:right;color:red;font-weight:500;font-size:12px">required </span> 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Runs the Api call and fills the corresponding model in the component on success.

</br>

-----

### Related Examples and Resources
- [Txn_model](https://docs.nftport.xyz/docs/nftport/ZG9jOjY1MjQwMDk5-txn-model-cs)
- [Retrieve transactions by an nft API](https://docs.nftport.xyz/docs/nftport/b3A6MzAxNDQ3NzU-retrieve-transactions-by-nft) 




