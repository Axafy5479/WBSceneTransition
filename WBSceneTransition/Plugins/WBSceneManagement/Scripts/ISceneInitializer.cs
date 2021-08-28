using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WBSceneManagement
{
    /// <summary>
    /// �V�[���J�ڂ����ۂɎn�߂ɋN������
    /// </summary>
    public interface ISceneInitializer
    {
        /// <summary>
        /// �}�X�N���J�����O�ɌĂ΂��
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public IEnumerator BeforeOpenMask(Dictionary<string, object> args);

        /// <summary>
        /// �}�X�N���J����������ɌĂ΂��
        /// </summary>
        /// <param name="args"></param>
        public void AfterOpenMask(Dictionary<string, object> args);


    }

}
