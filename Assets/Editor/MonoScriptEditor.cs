using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MonoScript))]
public class MonoScriptEditor : Editor {

	public override void OnInspectorGUI()
	{
		MonoScript targetScript = target as MonoScript;
		System.Type classType = targetScript.GetClass();
		if (classType == null)
		{
			return;
		}
		if (classType.IsSubclassOf(typeof(ScriptableObject)) && !classType.IsSubclassOf(typeof(Editor)))
		{
			if (GUILayout.Button("Create Instance"))
			{
				ScriptableObject instance = ScriptableObject.CreateInstance(classType);
				string path = AssetDatabase.GenerateUniqueAssetPath("Assets/"+classType.Name+".asset");
				AssetDatabase.CreateAsset(instance, path);
				EditorGUIUtility.PingObject(instance);
			}
		}
	}

}
