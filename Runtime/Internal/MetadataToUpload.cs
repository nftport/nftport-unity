using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using Newtonsoft.Json;
using UnityEngine;

namespace NFTPort
{
    [System.Serializable]
    public class MetadataToUpload
    {
        public string file_url = "Required Field";
        public string name = "Required Field";
        public string description = "Required Field";
        [DefaultValue("-1")]
        public int edition;
        [DefaultValue(null)]
        public List<Trait> traits;
        [DefaultValue(null)]
        public List<Attribute> attributes;
        [DefaultValue("")]
        public string compiler;
        [DefaultValue("")]
        public string background_color;
        [DefaultValue("")]
        public string external_url;
        [DefaultValue("")]
        public string animation_url;
        [DefaultValue("")]
        public string dna;
        [DefaultValue("")]
        public string id;
    }

    public static class ProcessObject
    {
        /// <returns>A dynamic object with only the filled properties of an object</returns>
        public static object ConvertToObjectWithoutPropertiesWithNullValues<T>(this T objectToTransform)
        {
            var type = objectToTransform.GetType();
            var returnClass = new ExpandoObject() as IDictionary<string, object>;
            foreach (var propertyInfo in type.GetProperties())
            {
                var value = propertyInfo.GetValue(objectToTransform);
                //var valueIsNotAString = !(value is string && !string.IsNullOrWhiteSpace(value.ToString()));
                if (value != null)
                {
                    returnClass.Add(propertyInfo.Name, value);
                }
            }

            return returnClass;
        }
        
        public static object FixMeUp<T>(this T fixMe)
        {
            var t = fixMe.GetType();
            var returnClass = new ExpandoObject() as IDictionary<string, object>;
            foreach(var pr in t.GetProperties())
            {
                var val = pr.GetValue(fixMe);
               // if(val is string && string.IsNullOrWhiteSpace(val.ToString()))
              //  {
              //  }
                if(val == null)
                {
                }
                else
                {
                    returnClass.Add(pr.Name, val);
                }
            }
            return returnClass;
        }

    }
}

