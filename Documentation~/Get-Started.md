
?
![NFTPort Gaming](https://i.imgur.com/lNsnJEP.png)

🎮 🕹 🎲 Unity3D SDK to integrate everything NFTs cross-chain at lightning speed!

[![star this repo](https://img.shields.io/github/stars/nftport/nftport-unity?label=Star%20the%20Repo&style=social)](https://github.com/nftport/nftport-unity)
    [![Join Discord](https://img.shields.io/discord/862277439023874059?logo=discord)](https://discord.gg/w92sXkNmBR)
    [![Follow Twitter](https://img.shields.io/twitter/follow/nftport_xyz?style=social&logo=twitter)](https://twitter.com/intent/follow?screen_name=nftport_xy)

## Getting Started

- Your API requests are authenticated using API keys. You can [get your free API key by signing up for an NFTPort account](https://dashboard.nftport.xyz/sign-up). <br/>

- [Get Unity](https://unity3d.com/unity/qa/lts-releases). <br/>
  >*The NFTPort Unity SDK is made using Unity 2021 LTS and also tested on Unity 2020. We recommend using the latest Unity 2021 LTS. However, the SDK should be compatible with other Unity versions as well. If you've found any incompatibilities or errors on import, please open an issue on [Github](https://github.com/nftport/nftport-unity/issues).*


----

### Get NFTPort Unity extension
  - Option 1: Import as a package using Git URL: In Unity's Package Manager, click the '+' on the top left corner and select 'Add package from git URL.' 
  ```
  https://github.com/nftport/nftport-unity.git
  
  ```
  <img src="https://imgur.com/f1IwAYN.jpg" alt="Add package from git URL." width="500"/>

  - Option 2: download the latest [release](https://github.com/nftport/nftport-unity/releases) and import the folder in your project.

----
On Import, additional packages will be installed automatically. In case its not, you may install dependencies via NFTPort/Install Dependencies. In case you already have dependencies ( for eg: JSON.net in Assets folder instead of via newtonsoft in packages) and is conflicting you may remove the newtonsoft package via UPM, or delete the JSON.net from assets folder as it'll be imported via UPM.
 <img src="https://i.imgur.com/XFImZNm.jpg" alt="Add package from git URL." width="500"/>

----
*The best way to learn is by doing! We've provided a number of examples for play, [view importing samples](https://docs.nftport.xyz/docs/nftport/ZG9jOjUzMzQxMzcy-examples#importing-samples).*



### First steps inside Unity
Set your API Key at NFTPort / Home.  <br/>
 <img src="https://i.imgur.com/xfxe8aH.png" alt="Add package from git URL." width="700"/>

  You may add `Assets/NFTPort/Resources/UserPrefs` to your `.gitignore` to avoid exposing your API keys.   <br/>



--------

 **Explore features, examples, usage and more under the Unity SDK tab on left on this page.**  <br/>


--------
## Features
   <img src="https://i.imgur.com/LTtYXhm.jpg" alt="Add package from git URL." width="600"/>
 NFTPort's Unity SDK features are highly composable and can be used in editor or runtime modes.
 
Features can be spawned via NFTPort/Spawner or using any of the Unity's default add game object / add component menus. They are available at `Packages/NFTPort/Runtime/`. 
<br/>

![](https://i.imgur.com/3xNQIaY.jpg)
 You do not need to spawn features if you do not wish to refrence or use the feature components in editor, you can just call Initialise() for each feature class from your own scripts. View individual feature documentation for more.


### Reporting bugs and requesting features
Join the NFTPort [discord](https://discord.gg/w92sXkNmBR) to engage in discussions and [explore](https://github.com/nftport/sample-unity3D-nft-metaverse-template) if the game mechanic you are trying to implement can already be achieved by existing features. To report a bug/issue/help - Use [Github Issues](https://github.com/nftport/nftport-unity/issues). To request features may also use [Product Board](https://portal.productboard.com/nftport/).

## Unity Extension Feature list: <br/>

✅ - Supported <br/>
❌ - Not supported as of now<br/>
🧭 - Planned  <br/>
🛠  - In consideration according to requests <br/>

### Multi-Chain NFT Data
Get fast and reliable data.

| API Feature                               | Ethereum  | Goerli | Polygon | Solana | 
| ----------------------------------- | --------  | ------- |  ------- | ------- | 
| **Contracts, Metadata & Assets**    |          |         |         |          
| [Retrieve NFT details](https://docs.nftport.xyz/docs/nftport/ZG9jOjY0OTM2NTgx-nft-details)                |✅           | ✅      |   ✅   | ✅ |     
| [Retrieve contract-collection NFTs](https://docs.nftport.xyz/docs/nftport/ZG9jOjk0NjM3NjA1-nf-ts-of-a-collection)              | ✅        | ✅   | ✅    |  ✅  |   
|  |          |        |        |    
| **Ownership including NFT metadata and details:**                       |          |        |        |     
| [Retrieve NFTs owned by an account](https://docs.nftport.xyz/docs/nftport/ZG9jOjUyMzI4NTkz-nf-ts-owned-by-an-account)   | ✅        | ✅    | ✅      |     ✅  |   
| [Retrieve NFTs owned by an account in a particular collection](https://docs.nftport.xyz/docs/nftport/ZG9jOjUyMzI4NTkz-nf-ts-owned-by-an-account)  | ✅         | ✅   | ✅      |   🛠   |     


### Easy minting
Easy mint NFTs with single component.

| API Feature                 | Ethereum  | Goerli  | Polygon | Solana | 
| ------------------ | -------- | ------- | ---- | ------- | 
| **Easy Minting**        |          |         |         | 
| [Via URL](https://docs.nftport.xyz/docs/nftport/ZG9jOjU1MDM4OTgw-minting-w-url)     |    🛠        |  ✅     |  ✅     |  🧭    
| [Via File](https://docs.nftport.xyz/docs/nftport/ZG9jOjczMDEwMjIx-easy-minting-with-file-upload)         |    🛠      |  ✅   |   ✅   |      🧭    


### Storage, contracts and customizable minting
Deploy fully customizable NFTs and contracts with industry standards - decentralized web3 storage, metadata, custom attributes and more.

| API Feature                                |  |
| --------------------------------- | ---------- |
| **Storage**                       |            |
| List all your IPFS uploads |    Via dashboard      |   
| [Upload a file to IPFS](https://docs.nftport.xyz/docs/nftport/ZG9jOjYwODM0NTY3-storage-upload-file)             |    ✅    |
| [Create and upload advanced industry standard metadata to IPFS](https://docs.nftport.xyz/docs/nftport/ZG9jOjYwODM0NTY4-storage-upload-metadata)          |     ✅     |
| Upload metadata directory to IPFS |   🧭        |  


| API Feature                | Ethereum | Goerli  | Polygon | Solana
| ------------------ | --------  | ------- | ---- |  ---- | 
| **Product**  |          |        |        |
|  |          |        |        |
| ERC 721  |    ✅         |  ✅   |   ✅   |   ✅   | ❌
| ERC 1155 |   🧭              | 🧭     | 🧭       | ❌
|  SPL  |     ❌       |     ❌  | ❌ | 🧭   
|    |          |        |        |
| Deploy a contract for NFT products  |     🧭      | 🧭        |  [via API](https://docs.nftport.xyz/docs/nftport/b3A6MjE0MDYzNzU-deploy-a-contract-for-nft-products)       | 🧭    
| [Customizable minting : Mint NFTs of your contract to any wallet](https://docs.nftport.xyz/docs/nftport/ZG9jOjYzMDIzNDgx-customizable-minting)  |     ✅             | ✅     |  ✅    | 🧭    
| [Update a minted NFT / Dynamic NFTs](https://docs.nftport.xyz/docs/nftport/ZG9jOjg2MTE3MTg4-customizable-minted-update-nft)   |     ✅           | ✅    | ✅    | 🧭    
| [Burn a minted NFT](https://docs.nftport.xyz/docs/nftport/ZG9jOjg4Mzk3MDM0-customizable-minted-burn-nft)   |     ✅     | ✅   |  ✅   | 🛠    
| [Transfer a minted NFT](https://docs.nftport.xyz/docs/nftport/ZG9jOjg4Mzk3MDUy-customizable-minted-transfer-nft)   |     ✅          | ✅   |  ✅    | 🛠   


### Transactions
Get multichain transaction data.

| API Feature                 | Ethereum  | Goerli | Polygon | Solana |
| ------------------ | --------  | ------- | ------ | ------ | 
| [Retrieve transactions by NFT](https://docs.nftport.xyz/docs/nftport/ZG9jOjgzODU5NDUy-transaction-data-nft)        | ✅             | ❌      | ❌  |  ✅   
| [Retrieve transactions by collection](https://docs.nftport.xyz/docs/nftport/ZG9jOjgzODU5NDUx-transaction-data-collection-contract)   | ✅             | ❌      | ❌  |   ✅
| [Retrieve transactions by an account](https://docs.nftport.xyz/docs/nftport/ZG9jOjY1MjQwMTAw-transaction-data-account) | ✅             | ❌     | ❌   | ✅   
| Retrieve contract sales statistics  | 🛠    | ❌             | ❌ |   🛠    




### Tools and Utilities
| Tools                             | |
| --------------------------------- | ---------- |
| [Connect Player Wallet : WebGL](https://docs.nftport.xyz/docs/nftport/ZG9jOjU3MTU2NTE5-player-wallet-connect#member-functions)  |    ✅      |         |         |       |
|  |          |         |         |       |
| **Asset Downloader:**  |          |         |         |       |
| [Determine URL Content Type](https://docs.nftport.xyz/docs/nftport/ZG9jOjU2NjAzOTE0-asset-downloader) |    ✅   |         |         |       |
| [Fetch Image asset from IPFS / web cached image](https://docs.nftport.xyz/docs/nftport/ZG9jOjU2NjAzOTE0-asset-downloader) |   ✅        |         |         |       |
| Fetch Audio file from IPFS / web |    🧭       |         |         |       |
| Fetch 3D object from IPFS / web |    ✅ Use determine content type with [gltFast](https://github.com/atteneder/glTFast) , [View Example](https://github.com/Worldsz/NFT-Unity3D-Metaverse-Template/blob/main/Assets/Advanced%20Playground/Gallery/Gallery.cs#L165)   |         |         |       |
|  |          |         |         |       |
| NFT collection reader (bulk) |    🧭       |         |         |       |
|  |          |         |         |       |
| **NFT Product Minting Suite**  |    🧭      |         |         |       |


  <br/>
  
----


