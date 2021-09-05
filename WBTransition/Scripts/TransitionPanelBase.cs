using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WBTransition
{
    public abstract class TransitionPanelBase : MonoBehaviour
    {
        [Header("�J�ڎ���timeScale���[���Ƃ���")]
        [SerializeField] private bool timeScale_zero = true;

        private bool endWaiting = false;

        /// <summary>
        /// �Œ���A���[�h��ʂ�������������(�b)
        /// </summary>
        protected abstract float WaitTime { get; }

        /// <summary>
        /// WaitTime�������[�h��ʂ�\�����邽��
        /// �������~�߂�
        /// </summary>
        /// <returns></returns>
        private IEnumerator Wait()
        {
            yield return new WaitForSecondsRealtime(WaitTime);
            endWaiting = true;
        }

        /// <summary>
        /// ��ʂ����A�j���[�V����
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerator Close();

        /// <summary>
        /// ��ʂ��J���A�j���[�V����
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerator Open();


        /// <summary>
        /// �V�[���J�ڂ��s��
        /// </summary>
        /// <param name="newScene">�J�ڐ�̃V�[����</param>
        /// <param name="onEndTransition">�J�ڒ���ɌĂ΂�郁�\�b�h�ɓn���ϐ�</param>
        /// <param name="onEndRemoveMask">�}�X�N�폜����ɌĂ΂�郁�\�b�h�ɓn���ϐ�</param>
        internal void LoadScene(TransitionPackage package)
        {
            DontDestroyOnLoad(this.gameObject);
            StartCoroutine(StartTransition(package));
        }

        /// <summary>
        /// �V�[���J�ږ{��
        /// </summary>
        /// <param name="package">�V�[���J�ڂɕK�v�ȏ��(�J�ڐ�A�`�������ϐ�)</param>
        /// <returns></returns>
        private IEnumerator StartTransition(TransitionPackage package)
        {
            string newScene = package.NewScene;
            var onEndTransition = package.OnEndTransition;
            var onEndRemoveMask = package.OnEndRemoveMask;

            float defaultTimeScale = Time.timeScale;
            if (timeScale_zero) Time.timeScale = 0;

            //�}�X�N�����B���I���܂őҋ@
            yield return Close();

            //�V�[���J�ڂ��J�n����Ɠ����ɁA���[�h��ʂ��w�莞�Ԍ�����
            StartCoroutine(Wait());
            yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Single);

            //SceneInitializer��T��
            var initializer = FindInitializer();

            //SceneInitializer������΁A�A�A
            if (initializer != null)
            {
                //�}�X�N���O�����O�̏������s��
                yield return initializer.BeforeOpenMask(onEndTransition);
            }

            //���[�h��ʂ́A�Œ��WaitTime�����͕\������
            //(�����J�ڐ悪�y���V�[���Ȃ�ΑJ�ڂ͈�u�ŏI���B���̏����������Ȃ��ƁA���[�h��ʂ���u�����f��s���R�ƂȂ�)
            yield return new WaitWhile(() => !endWaiting);

            //�}�X�N���O���B�O���I���܂őҋ@
            yield return Open();

            //SceneInitializer������΁A�A�A
            if (initializer != null)
            {
                //�}�X�N���O������̏������s��
                initializer.AfterOpenMask(onEndRemoveMask);
            }


            if (timeScale_zero) Time.timeScale = defaultTimeScale;

            //�}�X�N(���̃I�u�W�F�N�g)���폜
            GameObject.Destroy(this.gameObject);
        }

        /// <summary>
        /// Initializer��T��
        /// </summary>
        /// <returns></returns>
        private static ISceneInitializer FindInitializer()
        {
            var initializers = new List<ISceneInitializer>();

            foreach (var item in GameObject.FindObjectsOfType<Component>())
            {
                if (item is ISceneInitializer initializer) initializers.Add(initializer);
            }

            if (initializers.Count > 1)
            {
                Debug.LogError($"�uISceneInitializer�v�������ł���̂́A1�V�[���ɂ�1�܂łł��B����{initializers.Count}�̃C���X�^���X���������Ă��܂��B");
                return null;
            }
            else if (initializers.Count == 0)
            {
                return null;
            }
            else
            {
                return initializers[0];
            }
        }

    }
}
