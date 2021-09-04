using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WBTransition
{
    /// <summary>
    /// �V�[���J�ڒ���ɌĂ΂�郁�\�b�h����������
    /// ���̃C���^�[�t�F�[�X��1�V�[���ɂ�1�܂�
    /// </summary>
    public interface ISceneInitializer
    {
        /// <summary>
        /// �V�[���J�ڌ�A�}�X�N���J���n�߂�O��
        /// ���̃��\�b�h���Ă΂��
        /// </summary>
        /// <param name="args">�J�ڑO�̃V�[������`����ꂽ�ϐ�</param>
        /// <returns></returns>
        public IEnumerator BeforeOpenMask(Dictionary<string, object> args);

        /// <summary>
        /// �}�X�N���J���I���������ɌĂ΂��
        /// </summary>
        /// <param name="args">�J�ڑO�̃V�[������`����ꂽ�ϐ�</param>
        public void AfterOpenMask(Dictionary<string, object> args);


    }

}
