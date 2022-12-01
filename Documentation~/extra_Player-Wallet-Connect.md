# Connect player wallet

Connect Metamask wallet to Unity game on webgl providing wallet address and connected network ID.

![](https://i.imgur.com/GnwWXWM.gif)

<!-- theme: warning -->
> This feature doesn't support wallet signing, we do not recommend using it in live production if you need to authenticate the true ownership of the fetched address.
---

<img src="https://i.imgur.com/PlwQx7l.jpg" alt="Add package from git URL." width="500"/>


> Just put `ConnectPlayerWallet.cs` on any empty game object and follow steps below under Set WebGL Template.

### Usage in your script:

```csharp
using NFTPort;
```
</br>

```csharp
ConnectPlayerWallet
    .Initialize()
    .OnComplete((address, networkID )=> Debug.Log((address,networkID)));
```
</br>

Provide a button to user to connect either via HTML button or a button inside unity calling `WebSend_GetAddress()`. [View Sample](https://docs.nftport.xyz/docs/nftport/ZG9jOjUzMzQxMzcy-examples#player-wallet-connect) and Use WebGL template provided below.

-----


Once Player connects their wallet, it can be accessed globally from anywhere using:
```csharp
string x = Port.ConnectedPlayerAddress;
string y = Port.ConnectedPlayerNetworkID;
```

</br>


---

### Member Functions

#### .Initialize()

      Initialize creates a game object and assigns this script as a component. </br>

> `Initialize()` must be called if you are not refrencing the component.

#### .OnComplete()

      Action when player connects the wallet successfully returning (wallet address, networkID) as a string. 

</br>

---
### Functions
#### WebSend_GetAddress()
  Can be used on a Button from inside Unity to Connect Account, Sends Request to WebGL template.


#### ConnectThisToNFTPortWalletConnect()
  Can be used with other wallet providers/SDKs. 


</br>


-----

### Set WebGL Template
1. Download WebGL Template from samples :
![](https://i.imgur.com/zh46Mgb.png)

2. Drag the WEBGLTemplates folder to under /Assets/ in the project window :
![](https://i.imgur.com/PctPaDd.png) 

3. Select NFTPort Template under WebGL Templates in the Player Settings window :
![](https://i.imgur.com/Y8y6Tl8.png)

4. Build and Run. (っ◔◡◔)っ 



----

### Related Examples and Resources

- [Examples: Player wallet connect sample](https://docs.nftport.xyz/docs/nftport/ZG9jOjUzMzQxMzcy-examples#player-wallet-connect)
