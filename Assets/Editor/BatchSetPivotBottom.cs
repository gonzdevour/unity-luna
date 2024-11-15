using UnityEditor;
using UnityEngine;

public class BatchSetPivotBottom : EditorWindow
{
    [MenuItem("Tools/Batch Set Pivot to Bottom")]
    private static void SetPivotToBottom()
    {
        // ����襤���Ҧ��Ϥ�����
        Object[] selectedTextures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);

        foreach (Object obj in selectedTextures)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            if (textureImporter != null)
            {
                // �T�O Texture Type �]�m�� Sprite
                textureImporter.textureType = TextureImporterType.Sprite;

                // �ϥ� SerializedObject �]�m m_Alignment �� 9�]Custom�^�A�ϱo Pivot �i�H�]�w���۩w�q
                SerializedObject so = new SerializedObject(textureImporter);
                SerializedProperty alignmentProp = so.FindProperty("m_Alignment");
                alignmentProp.intValue = 9; // �]�w�� Custom

                // �]�m Pivot �� Bottom (0.5, 0)
                textureImporter.spritePivot = new Vector2(0.5f, 0f);

                // ���Χ��í��s�ɤJ
                so.ApplyModifiedProperties();
                textureImporter.SaveAndReimport();

                Debug.Log($"Set Pivot to Bottom for: {path}");
            }
        }

        EditorUtility.DisplayDialog("Batch Set Pivot", "Pivot has been set to Bottom for selected images.", "OK");
    }
}
