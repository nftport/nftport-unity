
using System.Collections.Generic;

namespace NFTPort.Internal
{

    /// <summary>
    /// To Process custom field attributes list
    /// </summary>
    public static class ProcessMetadataToUpload
    {
        public static Storage_MetadataToUpload_processedModel ConvertCustomFieldsListToDict(Storage_MetadataToUpload_model metadata)
        {
            Dictionary<string, string> dctn = new Dictionary<string, string>();
            Storage_MetadataToUpload_processedModel processedModel = new Storage_MetadataToUpload_processedModel();
            foreach (var custom_field in metadata.custom_fields)
            {
                dctn.Add(custom_field.key, custom_field.value);
            }

            processedModel.animation_url = metadata.animation_url;
            processedModel.attributes = metadata.attributes;
            processedModel.custom_fields = dctn;
            processedModel.description = metadata.description;
            processedModel.external_url = metadata.external_url;
            processedModel.name = metadata.name;
            processedModel.file_url = metadata.file_url;

            return processedModel;
        }
    }
}

