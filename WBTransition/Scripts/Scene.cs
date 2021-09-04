using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WBTransition
{
	//�C���X�y�N�^�[���SceneAsset��o�^�\�ɂ���X�N���v�g
	//�V�[���J�ڂɒ��ډe������N���X�ł͂���܂���

	[System.Serializable]
	public class Scene
	{
		/// <summary>
		/// SceneAssetEditor�N���X�̗͂��؂�邱�ƂŁA
		/// ���̕ϐ���SceneAsset��D&D�\�ɂ���(���̂Ƃ�SceneAsset�̃p�X�����̕ϐ��ɓ���)
		/// </summary>
		[SerializeField]private string sceneAssetPath;

		/// <summary>
		/// Scene�^�̃C���X�^���X���ÖٓI��string�ɕϊ��\�ɂ���
		/// �v�́AScene�^�̕ϐ���string�^�̕ϐ��ɑ���\�ɂ���
		/// </summary>
		/// <param name="scene"></param>
		public static implicit operator string(Scene scene)=>scene.sceneAssetPath;
	}
}
