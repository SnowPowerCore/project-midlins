using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Extensions
{
    public static class HostBuilderExtensions
    {
        public static IConfigurationBuilder AddUtf8JsonAddressable(this IConfigurationBuilder builder, string addressablePath)
        {
            var settings = Addressables.LoadAssetAsync<TextAsset>(addressablePath).WaitForCompletion();

            if (!settings)
                throw new NullReferenceException($"The requested asset {addressablePath} could not be loaded");
                    
            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(settings.text)));

            return builder;
        }
    }
}