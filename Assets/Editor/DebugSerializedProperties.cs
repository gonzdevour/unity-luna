using UnityEditor;
using UnityEngine;

public class DebugSerializedProperties : EditorWindow
{
    [MenuItem("Tools/Debug Serialized Properties")]
    private static void debugSerializedProperties()
    {
        // ����襤���Ϥ�����
        Object[] textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.Assets);

        foreach (Object texture in textures)
        {
            string assetPath = AssetDatabase.GetAssetPath(texture);
            TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;

            if (importer != null && importer.spriteImportMode == SpriteImportMode.Multiple)
            {
                SerializedObject so = new SerializedObject(importer);

                // �C�X�Ҧ��ݩʦW��
                SerializedProperty prop = so.GetIterator();
                while (prop.NextVisible(true))
                {
                    Debug.Log($"Property: {prop.name} - Type: {prop.propertyType}");
                }
            }
        }
    }
}
