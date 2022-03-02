
![alt text](./Readme-Assets/Port_unity_early.png)


## Getting Started: 

- Get your API keys: Your API requests are authenticated using API keys. You can get your free API key from : https://www.nftport.xyz/sign-up

- Download Unity 2020.3.18f https://unity3d.com/unity/whats-new/2020.3.18 <br/>
  >*SDK is made using Unity 2020.3.18f , however is compatible with other versions as well. We'll be working towards Unity Asset Store release to make process easier.*

- Download NFT Port extension: <br/>
You can clone [this repo](https://github.com/nftport/nftport-unity) and open the [unity project](./NFT_UnitySDK) ,<br/>
alternatiivevly you may download the unity package and import in your project, <br/>
or copy the [NFT_UnitySDK Folder](./NFT_UnitySDK/Assets/NFTPort%20SDK) in your project Assets directory.<br/>

<br/>

## First Steps inside Unity:
- Setup your NFTPort API Key in Window / NFTPort <br/>

<img src="./Readme-Assets/api.jpg"  width="1000"  /> <br/>
*best way to learn is by doing, a basic demo scene has been provided for play, view demo.cs*

<br/>


## Quick Usage via NFTPort_Interface.cs:

Add NFTPort_Interface.cs in your scene.<br/>

<img src="./Readme-Assets/port_interface.jpg"  width="300"  />  <br/>
> NFTPort_Interface provides a globally accessible class which routes into individual components. You can only have one of this in your scene, other instances will get deleted at runtime. Adding NFTPort_Interface.cs will automatically add other required components. <br/>

In your script, Include 
```
using NFTPort;
```
Getting All Addresses Of an NFT: 
```
NFTPort_Interface.Instance.NFTsOfAccount(addressString);
```
Getting All NFT's of a Contract:
```
NFTPort_Interface.Instance.NFTsFromContract(ContractString);
```
Easy Mint NFT via URL:
```
NFTPort_Interface.Instance.Mint(FileUrlString, AddressString, NFTMintNameString);
```

<br/>
>If you do not need to change chain ID at runtime, you can get most done by above and set the chain id and include parameters in the inspector on individual components.

When you call general functions  above using NFTPort_Interface.cs, the data gets sorted according to each NFT and populated in model of the following individual components below it, as seen in Unity Inpector Window. Any further actions utilising this data can be refrenced from these scripts. You can also link events from these individual components (view editor window of components in demo scene) <br/>

```
NFTsOwnedbyanAccount._ownedbyAddreddModel.nfts[i].name;
```



<br/>


## Individual Components:
###### Use Fetched Data : Modify chains, ID , Settings and more:

### NFTs_ownedbyanAccount.cs
>Component provides All NFT's owned by the account.

