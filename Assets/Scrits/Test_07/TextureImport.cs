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
        androidSetting.overridden = true;   //����Ϊ true���Ա�ʹ�� TextureImporterPlatformSettings �ṹ���ṩ�Ĳ���������Ĭ��ƽ̨������
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
