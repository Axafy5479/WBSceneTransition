using System;
using UnityEditor;
using UnityEngine;

namespace WBTransition
{
	//�C���X�y�N�^�[���SceneAsset��o�^�\�ɂ���X�N���v�g
	//�V�[���J�ڂɒ��ډe������N���X�ł͂���܂���

	[CustomPropertyDrawer(typeof(Scene))]
	public class SceneAssetEditor : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			//���̃X�N���v�g���Ď�����ϐ�
			var targetProp = property.FindPropertyRelative("sceneAssetPath");

			//�ȑO�o�^����Ă���SceneAsset & ���ݓo�^����Ă���SceneAsset
			var oldScene = AssetDatabase.LoadAssetAtPath(targetProp.stringValue, typeof(SceneAsset)) as SceneAsset;
			var newScene = (SceneAsset)EditorGUI.ObjectField(position, label, oldScene, typeof(SceneAsset), false);

			//���ݓo�^����Ă���V�[����null�ł���ꍇ�A�V�[�����Ƃ��� "" ��o�^
			if (newScene == null) targetProp.stringValue = "";
			else if (newScene != oldScene)
			{
				//newScene��null�ł͂Ȃ��A����
				//�ȑO�̂��̂ƌ��݂̂��̂��قȂ�ꍇ
				//�܂�A�ʂ�SceneAsset�ɒu��������ꂽ�ꍇ

				//D&D�����t�@�C���̃p�X
				string path = AssetDatabase.GetAssetPath(newScene);

				//scenes in build�ɑ��݂��邱�Ƃ��m�F
				//���Ȃ��݁Abuild�@settings�ɓo�^����Ă���V�[���S�Ă��擾
				var scenes = EditorBuildSettings.scenes;

				//D&D�����V�[�����Ascenes �Ɋ܂܂�Ă��邩�ۂ�
				bool existsInBuildSettings = Array.Exists(scenes, s => s.path == path);

				//�܂܂�Ă���ꍇ�A�Ď��Ώۂ̕ϐ���path����
				if (existsInBuildSettings) targetProp.stringValue = path;
				else
				{
					//�܂܂�Ă��Ȃ��ꍇ�A���̎|�����O�ɏo��
					Debug.LogError($"<b>�V�[���̓o�^�Y��ł�!!</b>\n {newScene.name} ��ScenesInBuild�ɓo�^���Ă�������");
				}
			}
			else
			{
				//newScene �� oldScene������̏ꍇ (= �����肪�������Ă��Ȃ��ꍇ) �������Ȃ�
			}
		}

	}
}