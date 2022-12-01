# NFTs_model.cs 

schema

-----

```csharp
  [Serializable]
  public class NFTs_model 
  {
      public string response;
      public List<Nft> nfts;
      public Nft nft;
      public Contract contract;
      public string owner;
      public int total;
      public object continuation;
  }
```
</br>

```csharp
[Serializable]
  public class Nft
  {
      public string name;
      public string chain;
      public Metadata metadata;
      public string description;
      public string creator_address;
      public string file_url;
      public string cached_file_url;
      public string animation_url;
      public string cached_animation_url;
      public DateTime mint_date;
      public FileInformation file_information;
      public DateTime updated_date;
      public string metadata_url;
      public string token_id;
      public string contract_address;
      public string contract_type;
      public List<Creator> creators;
      public List<Royalty> royalties;
      public List<string> signatures;
      public int total;
      public Assets assets;
  }
```
</br>

```csharp
  [Serializable]
  public class Assets
  {
      [Header("Unity Assets which can be linked to Nft")]
      public Texture2D image_texture;
      public AudioClip audioClip;
      public VideoClip videoClip;
      public Animation animation;
      public GameObject gameObject;
      public Mesh mesh;
      public Dictionary<string, Object> Objects = new Dictionary<string, Object>(); //User definable.
  }
```
</br>

```csharp
[Serializable]
  public class Metadata
  {
      public string name;
      public string description;
      public string image;
      public int edition;
      public object date;
      public List<Trait> traits;
      public List<Attribute> attributes;
      public string compiler;
      public string background_color;
      public string external_url;
      public string animation_url;
      public string dna;
      public string id;
  }
```
</br>

```csharp
  [Serializable]
  public class Attribute 
  {
      public string trait_type;
      [Tooltip("Set as either Numerical or Alphabets. Numeric properties as either floats or integers so that OpenSea can display them appropriately. String properties should be human-readable strings.")]
      public string value;
      [Tooltip("Set to '0' to exclude, Negative and Positive values accepted.")]
      public int max_value;
      
      [Tooltip("View https://docs.opensea.io/docs/metadata-standards")]
      public Displaytype display_type;
  }
```
</br>

```csharp
public enum Displaytype
  {
      not_set,
      boost_number,
      boost_percentage,
      number,
      date
  }
```

</br>

```csharp
    [Serializable]
    public class Trait
    {
        public string trait_type;
        public string value;
    }

```
</br>


```csharp
  [Serializable]
  public class FileInformation
  {
      public int height;
      public int width;
      public int file_size;
  }
```
</br>


```csharp
  [Serializable]
  public class Contract
  {
      public string name;
      public string symbol;
      public string type;
  }

```
</br>
</br>

-----
</br>


![](https://i.imgur.com/MMx3bTa.jpg)













