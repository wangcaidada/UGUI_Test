using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TextureImport : AssetPostprocessor
{
    private void OnPreprocessTexture()
    {
        TextureImporter textureImporter = (TextureImporter)assetImporter;

        TextureImporterPlatformSettings androidSetting = textureImporter.GetPlatformTextureSettings("Android");
        androidSetting.overridden = true;   //设置为 true，以便使用 TextureImporterPlatformSettings 结构中提供的参数来覆盖默认平台参数。
        androidSetting.format = TextureImporterFormat.ASTC_4x4;
        textureImporter.SetPlatformTextureSettings(androidSetting);

        TextureImporterPlatformSettings iosSetting = textureImporter.GetPlatformTextureSettings("IPhone");
        iosSetting.overridden = true;
        iosSetting.format = TextureImporterFormat.ASTC_4x4;
        textureImporter.SetPlatformTextureSettings(iosSetting);
        textureImporter.isReadable = false;
        textureImporter.mipmapEnabled = false;
    }
}
