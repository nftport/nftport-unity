# Changelog

### [0.5.1](https://github.com/nftport/nftport-unity/compare/v0.5.0...v0.5.1) (2022-07-23)


### Features

* Adds continuation parameter to NFTs of Account Feature Allowing to query accounts with many NFTs ([4950e04](https://github.com/nftport/nftport-unity/commit/4950e04e4f873e9e2a45f026e8907845c647792f))
* Adds continuation parameter to NFTs of Contract Feature at .SetContinuation() ([cee68e0](https://github.com/nftport/nftport-unity/commit/cee68e0e2bba0ff949bfdad43acfbba37187e1cc))
* ConnectPlayerWallet feature adds a new public function ConnectThisToNFTPortWalletConnect making feature more composable with other wallet connect sdks for unity. ([537e39d](https://github.com/nftport/nftport-unity/commit/537e39d7590defdd0cd514450df5690e68c2df6c))
* New member function in Asset Downloader, OnCompleteReturnLinkedNft() action returning Nft class with Texture2D ( NFTImage) attached in Nft.Assets.Texture when passed an Nft with it + Stop() fn to stop any in progress downloads ([d93ce27](https://github.com/nftport/nftport-unity/commit/d93ce273c7708723dbc7937f7e1d6eae60423001))


### Bug Fixes

* changes in AssetDownloader.GetImage Feature optional parameter field, allowing to pass full Nft class instead of just Nft.Assets. ([7cdec35](https://github.com/nftport/nftport-unity/commit/7cdec35ddeb6d532504eb99a857f35efe9a5921b))
* Connect Player Wallet Mock wallet overiding bug fix ([5a19adc](https://github.com/nftport/nftport-unity/commit/5a19adc7398ebdc719c6add5ff207cfb8eb4a0a8))
* GLTF-Utility dependency for playground sample is changed to gtlFast. ([26f4729](https://github.com/nftport/nftport-unity/commit/26f4729e2b24e0d2ec579c725f01886d58919cde))

## [0.5.0](https://github.com/nftport/nftport-unity/compare/v0.4.1...v0.5.0) (2022-07-08)


### Features

* Easy Mint via File upload : Mint any in-game procedural NFTs to any wallet with single call. ([8bdd1c0](https://github.com/nftport/nftport-unity/commit/8bdd1c0229eb98dbaeaaff2a5753d4b4bd7f0132))

### [0.4.1](https://github.com/nftport/nftport-unity/compare/v0.4.0...v0.4.1) (2022-07-04)


### Features

* New SDK readme ([ac10099](https://github.com/nftport/nftport-unity/commit/ac1009968e225da6651b0f16f173b5cade690bc7))
* Player wallet connect can now also be used from a button inside unity ([0501954](https://github.com/nftport/nftport-unity/commit/0501954bcf7b9c9128b03912aad1325dfaaf0683))
* Player wallet connect now also provides users connected network ID at Port.ConnectedPlayerNetworkID. ([add3604](https://github.com/nftport/nftport-unity/commit/add3604015ca30f680d8092995f7ed7a11b186c0))
* Player wallet connect supports editor mock for development ([38ca195](https://github.com/nftport/nftport-unity/commit/38ca195a154d1fcd53329680f147430817bb34ca))
* Updated home window plus go to dashboard link ([f612772](https://github.com/nftport/nftport-unity/commit/f612772236537224496dc6c2706d3c8186a783b0))
* Updated home window with hide API option ([9020054](https://github.com/nftport/nftport-unity/commit/9020054ec1fe0bb852c00753b95b1a0b10dd1d97))
* Ready Player Me avatars for Playground sample


### Bug Fixes

* Custom dependency check and tool for Playground sample for unity Input System ([e51027b](https://github.com/nftport/nftport-unity/commit/e51027b62a487bea03ed19bfdcd2bd2049858de3))
* Custom dependency check tool and updated window for playground sample ([6dda1fa](https://github.com/nftport/nftport-unity/commit/6dda1fae4383ae2bb8f55514e2828b6dff4fe82f))

## [0.4.0](https://github.com/nftport/nftport-unity/compare/v0.3.1...v0.4.0) (2022-06-10)


### Features

* Adds feature - Transaction Data | Account ([e32f4d1](https://github.com/nftport/nftport-unity/commit/e32f4d176927e24d98093cdbb5fdfbbc7169822e))
* Adds new metadata properties in NFT class including cached_animation_url and royalty etc,/ for transaction feature set. ([f66aab8](https://github.com/nftport/nftport-unity/commit/f66aab8669f6d10a6fb1e475384f6a494342a83a))
* Adds NFT Data | NFT Details feature ([74d6ba1](https://github.com/nftport/nftport-unity/commit/74d6ba109eb780a52af02bc7ede3ba3b4fd258bd))


### Bug Fixes

* Null value handling for all NFT Data features while Deserializing data ([fac3856](https://github.com/nftport/nftport-unity/commit/fac38567df2ae50e4861e63184baaacbf5f7ee7c))

### [0.3.1](https://github.com/nftport/nftport-unity/compare/v0.3.0...v0.3.1) (2022-06-03)


### Features

* Adds newtonsoft Json as external dependency solving any library conflict errors ([290c81a](https://github.com/nftport/nftport-unity/commit/290c81aa17c148100c08c4946f9ecea4c011cd34))
* Style updates and Feature Spawners : NFTPort Features now available in Component and Game Object Add menus ([8e33d74](https://github.com/nftport/nftport-unity/commit/8e33d74d4438786743d55e358b81a418ff89298a))

## [0.3.0](https://github.com/nftport/nftport-unity/compare/v0.2.2...v0.3.0) (2022-05-31)


### Features

* Added  Product | Fully Customizable minting Feature ([bf00946](https://github.com/nftport/nftport-unity/commit/bf0094661fa686d9cf722354661df3428d2b2a83))
* Adds NFTPort Custom Mint Sample - Playground ([cd99a2c](https://github.com/nftport/nftport-unity/commit/cd99a2c7fe1efa9782809086673a5a5f5d7431f1))
* Overall style updates ([b5320ab](https://github.com/nftport/nftport-unity/commit/b5320abf80ecd3c15dd76630ad22b9040e9f1d4f))
* settings window updates ([6fc2e9a](https://github.com/nftport/nftport-unity/commit/6fc2e9a4e383e63a7cc5ed3735ba28b80354842d))
* Style updates ([d4417db](https://github.com/nftport/nftport-unity/commit/d4417dbd6f0156cd440455fd369f473739755dab))


### [0.2.2](https://github.com/nftport/nftport-unity/compare/v0.2.0...v0.2.2) (2022-05-25)


### Bug Fixes

* Tooltip' is not valid on this declaration type error

### [0.2.1](https://github.com/nftport/nftport-unity/compare/v0.2.0...v0.2.1) (2022-05-24)


### Bug Fixes

* Fixed Corrupted images and .metas ([05abcbf](https://github.com/nftport/nftport-unity/commit/05abcbf80a10dcd14d5a6863859945b5fdc72e1c))
* Fixed Corrupted images in PlayerConnect + WebGL  sample ([18b3e41](https://github.com/nftport/nftport-unity/commit/18b3e417ae16ba541ccc3ba2a1c402b454fb5da1))
* Optimized home window user settings ([7128cd0](https://github.com/nftport/nftport-unity/commit/7128cd0f3733cacedab14b3a6f1e15b1ab762531))
* Updated Metadata Upload feature with API compatible format and metadata standards ([83938c1](https://github.com/nftport/nftport-unity/commit/83938c137d3a29d78d1eadf8c19e0cbf810fd5e2))

## [0.2.0](https://github.com/nftport/nftport-unity/compare/v0.1.1-preview...v0.2.0) (2022-05-19)


### Features

* Added Storage | File Upload ([a6fd126](https://github.com/nftport/nftport-unity/commit/a6fd126a20371a5d9fbc26cf0298afd49d9e8c60))
* Added Storage | Metadata Upload ([d9e057b](https://github.com/nftport/nftport-unity/commit/d9e057b03c3991a71b7877d05ae70cc35ead15f4))


### Bug Fixes

* Missing meta files for NFT's of Account ([d3a3155](https://github.com/nftport/nftport-unity/commit/d3a31554471156c044cd71121991b36b1683d354))

### [0.1.1-preview](https://github.com/nftport/nftport-unity/compare/v0.1.0-preview...v0.1.1-preview) (2022-05-09)
### Bug Fixes

* Fixed feature NFTs of Account.
## [0.1.0-preview](https://github.com/nftport/nftport-unity/compare/v1.0.0...v0.1.0-preview) (2022-05-06)


### Features

* Added Asset Downloader : NFT Image from IPFS/Web2 + Examples ([ff34a2c](https://github.com/nftport/nftport-unity/commit/ff34a2c736c9d1b72dfb31d16689cbfed0c0483c))
* Added Mint NFT with URL ([915cb7b](https://github.com/nftport/nftport-unity/commit/915cb7ba8f5b73a69df26a6b773b7bcc63df0166))
* Added NFT's of a Contract + Updated ownership Data example ([ada6509](https://github.com/nftport/nftport-unity/commit/ada650900e642320c3f3665b18ced165356198a6))
* Added NFT's of an Account + Sample ([32aa587](https://github.com/nftport/nftport-unity/commit/32aa5877cde8b1c7760214bc72f0806ef0893d49))

-----

### [0.0.1-earlyalpha](https://github.com/nftport/nftport-unity/releases/tag/v0.0.1-earlyalpha) -2022-03-21

Provides starter pack for NFTPort API endpoints in Unity3D. Early release.

* NFTs owned by an account </br>
* NFTs of a contract </br>
* EasyMint via URL file </br>

-----
All notable changes to this project will be documented in this file. Dates are displayed in UTC.

-----
*NFTPort.xyz*