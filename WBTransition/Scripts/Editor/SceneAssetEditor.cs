using System;
using UnityEditor;
using UnityEngine;

namespace WBTransition
{
	//インスペクター上でSceneAssetを登録可能にするスクリプト
	//シーン遷移に直接影響するクラスではありません

	[CustomPropertyDrawer(typeof(Scene))]
	public class SceneAssetEditor : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			//このスクリプトが監視する変数
			var targetProp = property.FindPropertyRelative("sceneAssetPath");

			//以前登録されていたSceneAsset & 現在登録されているSceneAsset
			var oldScene = AssetDatabase.LoadAssetAtPath(targetProp.stringValue, typeof(SceneAsset)) as SceneAsset;
			var newScene = (SceneAsset)EditorGUI.ObjectField(position, label, oldScene, typeof(SceneAsset), false);

			//現在登録されているシーンがnullである場合、シーン名として "" を登録
			if (newScene == null) targetProp.stringValue = "";
			else if (newScene != oldScene)
			{
				//newSceneがnullではなく、かつ
				//以前のものと現在のものが異なる場合
				//つまり、別のSceneAssetに置き換えられた場合

				//D&Dしたファイルのパス
				string path = AssetDatabase.GetAssetPath(newScene);

				//scenes in buildに存在することを確認
				//おなじみ、build　settingsに登録されているシーン全てを取得
				var scenes = EditorBuildSettings.scenes;

				//D&Dしたシーンが、scenes に含まれているか否か
				bool existsInBuildSettings = Array.Exists(scenes, s => s.path == path);

				//含まれている場合、監視対象の変数にpathを代入
				if (existsInBuildSettings) targetProp.stringValue = path;
				else
				{
					//含まれていない場合、その旨をログに出力
					Debug.LogError($"<b>シーンの登録忘れです!!</b>\n {newScene.name} をScenesInBuildに登録してください");
				}
			}
			else
			{
				//newScene と oldSceneが同一の場合 (= 何も手が加えられていない場合) 何もしない
			}
		}

	}
}