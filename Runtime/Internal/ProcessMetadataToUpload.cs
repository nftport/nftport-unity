
using System;
using System.Collections.Generic;

namespace NFTPort.Internal
{

    /// <summary>
    /// To Process custom field attributes list
    /// </summary>
    public static class ProcessMetadataToUpload
    {
        public static Storage_MetadataToUpload_processedModel Process(Storage_MetadataToUpload_model metadata)
        {
            Storage_MetadataToUpload_processedModel processedModel = new Storage_MetadataToUpload_processedModel();
            
            
            //Convert List to oDict for custom_fields
            Dictionary<string, string> dctn = new Dictionary<string, string>();
            foreach (var custom_field in metadata.custom_fields)
            {
                dctn.Add(custom_field.key, custom_field.value);
            }
            processedModel.custom_fields = dctn;
            
            
            //Process Attributes 
            processedModel.attributes = new List<ProcessedAttribute>();
            foreach (var attribute in metadata.attributes)
            {
                //Process displaytype enum to _sz_tring 
                string _display_type;
                if (attribute.display_type.ToString() != "not_set")
                {
                    _display_type = attribute.display_type.ToString();
                }
                else
                {
                    _display_type = null;
                }
                
                //copy Attributes
                processedModel.attributes.Add(new ProcessedAttribute
                {
                    trait_type = attribute.trait_type,
                    value = attribute.value,
                    max_value = attribute.max_value,
                    display_type = _display_type
                });
            }

            //Copy Rest
            processedModel.animation_url = metadata.animation_url;
            processedModel.description = metadata.description;
            processedModel.external_url = metadata.external_url;
            processedModel.name = metadata.name;
            processedModel.file_url = metadata.file_url;

            return processedModel;
        }
    }
}

