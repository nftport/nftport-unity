# nftport-unity-dev
Development environment for nftport-unity SDK repo

[![Unity Tests _Unity 🎮](https://github.com/nftport/nftport-unity-dev/actions/workflows/test_Unity.yml/badge.svg)](https://github.com/nftport/nftport-unity-dev/actions/workflows/test_Unity.yml)
[![Build Tests _Unity 🛕](https://github.com/nftport/nftport-unity-dev/actions/workflows/buildtest_Unity.yml/badge.svg)](https://github.com/nftport/nftport-unity-dev/actions/workflows/buildtest_Unity.yml)

----------------
[com.nftport.com](https://github.com/nftport/nftport-unity-dev/tree/master/NFT_UnitySDK/Packages/com.nftport.nftport) is a ↔️shared folder between [this]() repo and package-only repo [nftport-unity](https://github.com/nftport/nftport-unity/tree/com.nftport.nftport) . This repo contains the full development environment, nftport-unity repo is final Unity package.
<br/>
<br/>
### Setup Locally: 
<br/>
Add [nftport-unity](https://github.com/nftport/nftport-unity/tree/com.nftport.nftport) locally as a remote origin (nftport-unity) :
```
git remote add nftport-unity https://github.com/nftport/nftport-unity.git
```
↖️Pushing [package folder changes](https://github.com/nftport/nftport-unity-dev/tree/master/NFT_UnitySDK/Packages/com.nftport.nftport) from here -> to [nftport-unity](https://github.com/nftport/nftport-unity/tree/com.nftport.nftport) repo:
<br/>
//TODO: test workflow: Push to a seprate branch/issue on nftport-unity repo then merge there.
```
git subtree push --prefix=NFT_UnitySDK/Packages/com.nftport.nftport nftport-unity com.nftport.nftport
```
```
git subtree push --prefix=NFT_UnitySDK/Packages/com.nftport.nftport --squash nftport-unity com.nftport.nftport
```
➡️Pulling [nftport-unity](http://com.nftport.com) changes to this repo:

```
git subtree pull --prefix=NFT_UnitySDK/Packages/com.nftport.nftport nftport-unity com.nftport.nftport
```
```
git subtree pull --prefix=NFT_UnitySDK/Packages/com.nftport.nftport --squash nftport-unity com.nftport.nftport
```
