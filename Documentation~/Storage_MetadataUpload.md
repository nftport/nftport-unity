<img src="https://i.imgur.com/sUnPLXj.png" style="width:100%"/>
</br>

# Storage | Upload Metadata

`Storage_UploadMetadata.cs`  | Upload Metadata to decentralized [IPFS](https://ipfs.io/). Usefull to use with [Customizable Minting](https://docs.nftport.xyz/docs/nftport/ZG9jOjYzMDIzNDgx-customizable-minting).

-----

![Storage_UploadMetadata.cs](https://i.imgur.com/qM8ManJ.gif)

-----

 >  You can also directly use this in editor as shown above via filling in the component and using the buttons provided on the component.

-----

### Usage in your script

```csharp
using NFTPort;
```
</br>

```csharp
 Storage_UploadMetadata
      .Initialize()
      .SetMetadata(metadataModel)
      .OnStarted(a=> Debug.Log(a))
      .OnProgress(progress=> Debug.Log(progress))
      .OnError(error=> Debug.Log(error))
      .OnComplete(model => Debug.Log(model.metadata_uri))
      .Run();
```
</br>
 
```csharp 
   Storage_MetadataToUpload_model metadataModel = new Storage_MetadataToUpload_model
        {
            file_url = "https://ipfs.io/ipfs/bafkreig4azycjdng6odwqp5s32rp55gdanaiw7blyl4eehzbhxom52ciui",
            name = "NFTPort Unity SDK",
            description = "Fast track your game development in Unity at NFTPort SDK with cross chain NFTs and fast and reliable data",
            external_url = "https://github.com/nftport/nftport-unity",
            animation_url = "https://design.embracingearth.space/wp-content/uploads/2022/05/index.html",
            attributes = new List<Attribute>{
                new Attribute
                {
                    trait_type = "Power",
                    value = "Fireball"
                }},
            custom_fields = new List<custom_fields>{
                new custom_fields
                {
                    key = "wow factor",
                    value = "(இ௦இ)꒳ᵒ꒳ᵎᵎᵎ"
                },
                new custom_fields
                {
                    key = "compiler",
                    value = "sz-101"
                },
                new custom_fields
                {
                    key = "dna",
                    value = "T2T-CHM13"
                },
                new custom_fields
                {
                    key = "background_color",
                    value = "Skobeloff"
                },
                new custom_fields
                {
                    key = "id",
                    value = "Ser-007"
                }
            },
        };
```
On a succesful upload, response is filled in `Storage_model.Storage model` returned via OnComplete(). View related schema. Result of above code can be found [here](https://ipfs.stibits.com/bafkreiaekyr3m6nckfd7svafbziwk4krxeiigishxhmf3ydwgt4jul2zo4). 



 >  View [metadata standards](https://docs.opensea.io/docs/metadata-standards). If you're having issues getting your items to show up properly on OpenSea, Go [here](https://docs.opensea.io/docs/4-debugging-your-metadata). You may enable bool `debugLogRawJson` on the component to view metadata json created.


![](https://i.imgur.com/Z0g3xzn.gif)

[View on OpenSea.](https://opensea.io/assets/matic/0x42b57de948d05d17fb11d4e527df80a0420a4392/372809648646652861682)
</br> 

-----
### Member Functions 

Member Functions can be set as needed before`.Run()`

#### .Initialize()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Initialize creates a game object and assigns this script as a component. </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Optional parameter: bool destroyed: defines if this component will be destroyed after `.Run()`

 >  `Initialize()` must be called if you are not refrencing the component any other way and it doesn't already exists in the scene.  

#### .SetMetadata(MetadataToUpload_model metadata)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set metadata to upload. This will override any values set in the editor component. </br>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Paramters: </br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [`MetadataToUpload_model`](https://docs.nftport.xyz/docs/nftport/ZG9jOjU0MzMyMDg0-nf-ts-model-cs) `metadata` </br>


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


#### .SaveFile(string saveToPath, string fileName)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Save file locally as json to /saveToPath/fileName.

#### .Stop()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Stop Any In-progress calls.



</br>

-----
After uploading your metadata, use Product components to continue with your NFT minting.

### Related Examples and Resources
- [Customizable Minting](https://docs.nftport.xyz/docs/nftport/ZG9jOjYzMDIzNDgx-customizable-minting)
- [Examples](https://docs.nftport.xyz/docs/nftport/ZG9jOjUzMzQxMzcy-examples#easy-mint-with-url-sample)
- [MetadataToUpload_model Schema](https://docs.nftport.xyz/docs/nftport/ZG9jOjU1MDM4OTg1-minted-model-cs)
- [Storage_model Schema](https://docs.nftport.xyz/docs/nftport/ZG9jOjU1MDM4OTg1-minted-model-cs)
- [Upload metadata to IPFS API](../../3.json/paths/~1v0~1metadata/post) 

















