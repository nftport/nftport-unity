<img src="https://i.imgur.com/wcruLJn.png" style="width:100%"/>
</br>

# Storage | Upload File

`Storage_UploadFile.cs`  | Upload file to decentralized [IPFS](https://ipfs.io/). Usefull to use with [Customizable Minting](https://docs.nftport.xyz/docs/nftport/ZG9jOjYzMDIzNDgx-customizable-minting).

-----

![Storage_UploadFile.cs](https://i.imgur.com/5jVbKmz.gif)


-----

 >  You can also directly use this in editor as shown above via filling in the component and using the buttons provided on the component.

 Use Select File button to easily select or debug file path. If you want to create/upload files at build you can use Unity's [Path.Combine(Application.streamingAssetsPath, "xyz.glb")](https://docs.unity3d.com/2021.1/Documentation/ScriptReference/Application-streamingAssetsPath.html)(Read-Only) or [Application.persistentDataPath](https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html)(Read-Write).

-----

### Usage in your script

```csharp
using NFTPort;
```
</br>

```csharp 
 Storage_UploadFile
          .Initialize()
          .SetFilePath("F:/model.glb")
          .OnStarted(a=> Debug.Log(a))
          .OnProgress(progress=> Debug.Log(progress))
          .OnError(error=> Debug.Log(error))
          .OnComplete(model => Debug.Log(model.ipfs_url))
          .Run();

```
On a successful upload, the response is filled in `Storage_model.Storage model` returned via OnComplete(). View related schema.

</br>

-----
### Member Functions 

Member Functions can be set as needed before`.Run()`

#### .Initialize()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Initialize creates a game object and assigns this script as a component. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Optional parameter: bool destroyed: defines if this component will be destroyed after `.Run()`

 >  `Initialize()` must be called if you are not refrencing the component any other way and it doesn't already exists in the scene.  

#### .SetFilePath(string filePath)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set file path for the file to upload. </br>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramters: </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `string filePath` </br>


#### .OnStarted()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on File Upload Start, returning a string.

#### .OnProgress()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on File Upload Progress returning Progress percentage int `0-100`.

#### .OnError()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on Error providing string containing the error information.

#### .OnComplete()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action on succesfull API Fetch providing type `Storage_model.Storage` with filled data. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Use: `.OnComplete(model=> Model = model)` , where `Model` is of type [`Storage_model.Storage`](https://docs.nftport.xyz/docs/nftport/ZG9jOjYwODM0NTY2-storage-model-cs).

#### .Run()
<span style="float:right;color:red;font-weight:500;font-size:12px">required </span> 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Runs the API call and uploads.

</br>

-----

### Extra Functions 

Can be called as needed by referencing the component.

#### .Stop()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Stop Any In-progress calls.



</br>

-----
After uploading your metadata, use Product components to continue with your NFT minting.

### Related Examples and Resources
- [Customizable Minting](https://docs.nftport.xyz/docs/nftport/ZG9jOjYzMDIzNDgx-customizable-minting)
- [Examples](https://docs.nftport.xyz/docs/nftport/ZG9jOjUzMzQxMzcy-examples#easy-mint-with-url-sample)
- [Storage_model Schema](https://docs.nftport.xyz/docs/nftport/ZG9jOjU1MDM4OTg1-minted-model-cs)
- [Upload a file to IPFS API](../../3.json/paths/~1v0~1files/post) 

















