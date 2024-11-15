using UnityEditor;
using UnityEngine;

public class BatchSetPivotBottomMultiple : EditorWindow
{
    [MenuItem("Tools/Batch Set Pivot to Bottom (Multiple)")]
    private static void SetPivotToBottomMultiple()
    {
        Object[] textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.Assets);

        foreach (Object texture in textures)
        {
            string assetPath = AssetDatabase.GetAssetPath(texture);
            TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;

            if (importer == null || importer.spriteImportMode != SpriteImportMode.Multiple)
            {
                Debug.LogWarning($"Skipping {assetPath}: Not a valid sprite or not in Multiple mode.");
                continue;
            }

            SerializedObject so = new SerializedObject(importer);
            SerializedProperty spritesheetProp = so.FindProperty("m_SpriteSheet.m_Sprites");

            if (spritesheetProp != null && spritesheetProp.isArray)
            {
                bool hasChanges = false;

                for (int i = 0; i < spritesheetProp.arraySize; i++)
                {
                    SerializedProperty spriteData = spritesheetProp.GetArrayElementAtIndex(i);
                    SerializedProperty alignmentProp = spriteData.FindPropertyRelative("m_Alignment");
                    SerializedProperty pivotProp = spriteData.FindPropertyRelative("m_Pivot");

                    alignmentProp.intValue = 9; // Set to custom
                    pivotProp.vector2Value = new Vector2(0.5f, 0f); // Bottom center

                    hasChanges = true;
                }

                if (hasChanges)
                {
                    so.ApplyModifiedProperties();
                    importer.SaveAndReimport();
                    Debug.Log($"Set pivot to Bottom Center for sprites in {assetPath}");
                }
                else
                {
                    Debug.Log($"No changes needed for {assetPath}");
                }
            }
        }

        EditorUtility.DisplayDialog("Batch Set Pivot", "Pivot has been set to Bottom for selected images (Multiple mode).", "OK");
    }
}
