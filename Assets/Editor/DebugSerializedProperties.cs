using UnityEditor;
using UnityEngine;

public class DebugSerializedProperties : EditorWindow
{
    [MenuItem("Tools/Debug Serialized Properties")]
    private static void debugSerializedProperties()
    {
        // 獲取選中的圖片素材
        Object[] textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.Assets);

        foreach (Object texture in textures)
        {
            string assetPath = AssetDatabase.GetAssetPath(texture);
            TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;

            if (importer != null && importer.spriteImportMode == SpriteImportMode.Multiple)
            {
                SerializedObject so = new SerializedObject(importer);

                // 列出所有屬性名稱
                SerializedProperty prop = so.GetIterator();
                while (prop.NextVisible(true))
                {
                    Debug.Log($"Property: {prop.name} - Type: {prop.propertyType}");
                }
            }
        }
    }
}
