---
stoplight-id: acu14f9mdgb39
---

<img src="https://i.imgur.com/ZlJHFV0.png" style="width:100%"/>
</br>

# Easy minting with file upload

`Mint_File.cs` Create and mint any file to any wallet as NFT from within Unity using file upload at any moment in the game. Usefull for making anything into NFT including game objects, audio, textures, files, unity prefabs, procedural objects and more. Also view [Easy Mint with URL](https://docs.nftport.xyz/docs/nftport/ZG9jOjU1MDM4OTgw-minting-w-url) and [Custom Mint](https://docs.nftport.xyz/docs/nftport/ZG9jOjYzMDIzNDgx-customizable-minting).

-----

![Mint_File.cs](https://i.imgur.com/OA1okDJ.gif)

Use Select File button to easily select or debug file path. If you want to create/upload files at build you can use Unity's [Path.Combine(Application.streamingAssetsPath, "xyz.glb")](https://docs.unity3d.com/2021.1/Documentation/ScriptReference/Application-streamingAssetsPath.html)(Read-Only) or [Application.persistentDataPath](https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html)(Read-Write).

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
Mint_File
      .Initialize()
      .OnComplete(Minted => minted = Minted)
      .OnStarted(started => Debug.Log(started))
      .OnProgress(percent => Debug.Log(percent.ToString()))
      .OnError(error => Debug.Log(error))
      .SetChain(Mint_File.Chains.polygon)
      .SetParameters(
          FilePath: "E:/xyz.glb",
          Name: "Awesome NFT",
          Description: "Wow, You can mint Custom NFT models, Procedural ART, In Game screenshots and more from inside Unity",
          MintToAddress: "0x3691Ca2c8D2051f0B8b9d4aCb8941771aBc1bf9b"
      )
      .Run();
```
On a succesful mint, response is filled in [`Minted_model minted`](https://docs.nftport.xyz/docs/nftport/ZG9jOjU1MDM4OTg1-minted-model-cs) . View related schema.

```csharp
 Debug.Log(minted.transaction_external_url);
```

</br>

-----
### Member Functions 

Member Functions can be set as needed before `.Run()`

#### .Initialize()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Initialize creates a gameobject and assings this script as a component. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Optional parameter: bool `destroyAtEnd` : defines if this component will be destroyed after `.Run()`

 >  `Initialize()` must be called if you are not refrencing the component any other way and it doesn't already exists in the scene.  

#### .SetParameters()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set paramters for easy mint NFT </br>
<span style="float:right;color:red;font-weight:500;font-size:12px">required </span> 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramters:</br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`string 	FilePath` </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`string 	Name` </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`string 	Description` </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`string 	MintToAddress` </br>

#### .SetChain()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set blockchain to mint NFT on. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramter: Choose from `Mint_File.Chains`

#### .OnStarted()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on File Upload Start, returning a string.

#### .OnProgress()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on File Upload Progress returning Progress percentage int `0-100`.

#### .OnError()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on Error providing string containing the error information.

#### .OnComplete()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on succesfull API Fetch providing type [`Minted_model`](https://docs.nftport.xyz/docs/nftport/ZG9jOjU1MDM4OTg1-minted-model-cs) with filled data. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Use: `.OnComplete(Minted=> minted = Minted)` , where `minted` is of type [`Minted_model`](https://docs.nftport.xyz/docs/nftport/ZG9jOjU1MDM4OTg1-minted-model-cs)

#### .Run()
<span style="float:right;color:red;font-weight:500;font-size:12px">required </span> 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Runs the Api call and mints.

</br>

-----

### Extra Functions 

Can be called as needed by referencing the component.

#### .Stop()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Stop Any In-progress uploads.


</br>


-----
 > If you wish to customize the minting process e.g. use your own contract, add custom metadata, see [product minting components](https://docs.nftport.xyz/docs/nftport/ZG9jOjYzMDIzNDgx-customizable-minting).

### Related Examples and Resources
- [Examples: Easy Mint Sample](https://docs.nftport.xyz/docs/nftport/ZG9jOjUzMzQxMzcy-examples#easy-mint-with-url-sample)
- [Minted_model Schema](https://docs.nftport.xyz/docs/nftport/ZG9jOjU1MDM4OTg1-minted-model-cs)
- [Easy minting w/File Upload API](https://docs.nftport.xyz/docs/nftport/b3A6Njg1NTI0Mjc-easy-minting-w-file-upload) 
















