
![alt text](./Readme-Assets/Port_unity_early.png)


## Getting Started: 

- Get your API keys: Your API requests are authenticated using API keys. You can generate an API key from : https://www.nftport.xyz/sign-up

- Download Unity 2020.3.18f https://unity3d.com/unity/whats-new/2020.3.18 <br/>
  >*SDK is made using Unity 2020.3.18f , however is compatible with other versions as well. We'll be working towards Unity Asset Store release to make process easier.*

- Download NFT Port extension: <br/>
You can clone [this repo](https://github.com/nftport/nftport-unity) and open the [unity project](./NFT_UnitySDK) ,<br/>
alternatiivevly you may download the unity package and import in your project, <br/>
or copy the [NFT_UnitySDK Folder](./NFT_UnitySDK/Assets/NFTPort%20SDK) in your project Assets directory.<br/>

## First Steps inside Unity:
- Setup your NFTPort API Key in Window / NFTPort <br/>

<img src="./Readme-Assets/api.jpg"  width="1000"  /> <br/>
*best way to learn is by doing, a basic demo scene has been provided for play*



## Quick Usage via NFTPort_Interface.cs:
-  Add NFTPort_Interface.cs in your scene.
> NFTPort_Interface component provides a globally accessible class which routes into individual components. You can only have one of this in your scene, other instances will get deleted at runtime. If you do not need to change chain ID at runtime, you can get most done by above and set the chain id and include parameters in the inspector on individual components.

