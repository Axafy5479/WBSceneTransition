using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace WBSceneManagement
{
    public class TransitionPanel : MonoBehaviour
    {
        /// <summary>
        /// �V�[�����B���}�X�N(���`�ɐ؂蔲���ꂽ�}�X�N)
        /// </summary>
        [SerializeField]private RectTransform unmaskRect;

        /// <summary>
        /// �����T�C�Y
        /// �}�X�N���J���Ƃ��ɗp����
        /// </summary>
        private Vector2 initSize;

        /// <summary>
        /// Tweener�������Đ������ݒ肩�ۂ�
        /// (�ǂ̐ݒ�ł����������삳���邽�߂ɗp����)
        /// </summary>
        private bool TweenerAutoPlay =>
            DOTween.defaultAutoPlay == AutoPlay.AutoPlayTweeners || 
            DOTween.defaultAutoPlay == AutoPlay.All;

        /// <summary>
        /// �V�[���̃��[�h���I���A�}�X�N���O�����O��true�ƂȂ�
        /// </summary>
        public bool SceneLoaded { get; private set; } = false;


        void Awake()
        {
            //�}�X�N��\������
            //(���߂̓}�X�N�̃I�u�W�F�N�g���A�N�e�B�u�ɂ��Ȃ��ƁA���[�h��ʂ̐ݒ肪�ʓ|�ɂȂ�܂�)
            unmaskRect.gameObject.SetActive(true);

            //�����T�C�Y���L������
            //(�}�X�N���J���Ƃ��A���̃T�C�Y��ڕW�ɃX�P�[�����O����)
            initSize = unmaskRect.sizeDelta;

            //�V�[���J�ڎ��Ƀ��[�h��ʂ��폜�����̂�h��
            DontDestroyOnLoad(this.gameObject);
        }


        /// <summary>
        /// �}�X�N�����
        /// </summary>
        /// <param name="closeTime">����̂ɂ����鎞��(�b)</param>
        /// <returns></returns>
        internal async UniTask Close(float closeTime)
        {
            await PlayMaskAnimation(closeTime, false);
        }

        /// <summary>
        /// �}�X�N���J��
        /// </summary>
        /// <param name="openTime">�J���̂ɂ����鎞��(�b)</param>
        /// <returns></returns>
        internal async UniTask Open(float openTime)
        {
            SceneLoaded = true;
            await PlayMaskAnimation(openTime, true);
        }


        /// <summary>
        /// �}�X�N�̃A�j���[�V�����̖{��
        /// </summary>
        /// <param name="time">�A�j���[�V�����ɂ����鎞��</param>
        /// <param name="open">�}�X�N���J���Ȃ�true ����Ȃ�false</param>
        /// <returns></returns>
        private async UniTask PlayMaskAnimation(float time, bool open)
        {
            //�A�j���[�V�������I������^�C�~���O��true�ƂȂ�
            bool animEnd = false;

            //�J���A�j���[�V�����Ȃ� initSize���A����A�j���[�V�����Ȃ�[����ڎw��
            Vector2 targetSize = open ? initSize : Vector2.zero;

            //�}�X�N�̃T�C�Y��ύX����A�j���[�V����
            var tween = unmaskRect.DOSizeDelta(targetSize, time)

                //Time.timeScale�Ɉˑ������A�j���[�V�������s��
                .SetUpdate(true)

                //Ease�̐ݒ�(���x�ω���K���ɂ���)
                .SetEase(open ? Ease.InQuad : Ease.OutQuad)

                //�A�j���[�V�������I�������ۂɈ������s����(animEnd��true�ƂȂ�)
                .OnComplete(() => animEnd = true);

            //Tween�������Đ�����Ȃ��ݒ�̏ꍇ�A�Đ����\�b�h���Ă�
            if (!TweenerAutoPlay) tween.Play();

            //�A�j���[�V�������I���܂ő҂�
            await UniTask.WaitWhile(() => !animEnd);
        }
    }
}
