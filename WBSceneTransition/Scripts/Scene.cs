using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WBTransition
{
	//インスペクター上でSceneAssetを登録可能にするスクリプト
	//シーン遷移に直接影響するクラスではありません

	[System.Serializable]
	public class Scene
	{
		/// <summary>
		/// SceneAssetEditorクラスの力を借りることで、
		/// この変数にSceneAssetをD&D可能にする(このときSceneAssetのパスがこの変数に入る)
		/// </summary>
		[SerializeField]private string sceneAssetPath;

		/// <summary>
		/// Scene型のインスタンスを暗黙的にstringに変換可能にする
		/// 要は、Scene型の変数をstring型の変数に代入可能にする
		/// </summary>
		/// <param name="scene"></param>
		public static implicit operator string(Scene scene)=>scene.sceneAssetPath;
	}
}
