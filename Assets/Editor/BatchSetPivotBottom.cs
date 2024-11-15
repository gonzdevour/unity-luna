using UnityEditor;
using UnityEngine;

public class BatchSetPivotBottom : EditorWindow
{
    [MenuItem("Tools/Batch Set Pivot to Bottom")]
    private static void SetPivotToBottom()
    {
        // 獲取選中的所有圖片素材
        Object[] selectedTextures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);

        foreach (Object obj in selectedTextures)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            if (textureImporter != null)
            {
                // 確保 Texture Type 設置為 Sprite
                textureImporter.textureType = TextureImporterType.Sprite;

                // 使用 SerializedObject 設置 m_Alignment 為 9（Custom），使得 Pivot 可以設定為自定義
                SerializedObject so = new SerializedObject(textureImporter);
                SerializedProperty alignmentProp = so.FindProperty("m_Alignment");
                alignmentProp.intValue = 9; // 設定為 Custom

                // 設置 Pivot 為 Bottom (0.5, 0)
                textureImporter.spritePivot = new Vector2(0.5f, 0f);

                // 應用更改並重新導入
                so.ApplyModifiedProperties();
                textureImporter.SaveAndReimport();

                Debug.Log($"Set Pivot to Bottom for: {path}");
            }
        }

        EditorUtility.DisplayDialog("Batch Set Pivot", "Pivot has been set to Bottom for selected images.", "OK");
    }
}
