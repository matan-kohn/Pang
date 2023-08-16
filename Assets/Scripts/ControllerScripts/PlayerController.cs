
    using System.Collections;
    using DefaultNamespace;
    using UnityEngine;

    public class PlayerController : GameElement
    {

        private PlayerView _playerView;
        private PlayerModel _playerModel;
        private Animator _playerAnimator;

        void Start()
        {
            _playerView = Game.GameView.PlayerView;
            _playerModel = Game.GameModel.PlayerModel;
            _playerAnimator = Game.GameView.PlayerView.GetComponent<Animator>();
        }

        /**
        * <summary>makes the player run leftward when the player clicked on the left arrow button</summary>
        */
        public IEnumerator MovePlayerLeft()
        {
            _playerView.transform.rotation = Quaternion.Euler(0, 0, 0);
            do
            {
                _playerView.transform.position += Vector3.left * (PangConstants.MoveSpeed * Time.deltaTime);
                yield return null;
            } while (_playerModel.PlayerState == PlayerState.GoingLeft);
        }


        /**
        * <summary>makes the player run rightward when the player clicked on the right button</summary>
        */
        public IEnumerator MovePlayerRight()
        {
            _playerView.transform.rotation = Quaternion.Euler(0, 180, 0);
            do
            {
                _playerView.transform.position += Vector3.right * (5f * Time.deltaTime);
                yield return null;
            } while (_playerModel.PlayerState == PlayerState.GoingRight);
        }


        public void ProcessCommand(CommandType commandType, params object[] data)
        {
            _playerAnimator.ResetTrigger(PangConstants.GoLeftTrigger);
            _playerAnimator.ResetTrigger(PangConstants.GoRightTrigger);
            _playerAnimator.ResetTrigger(PangConstants.ShootTrigger);
            _playerAnimator.ResetTrigger(PangConstants.IdleTrigger);

            switch (commandType)
            {
                case CommandType.GoLeft:
                    _playerAnimator.SetTrigger(PangConstants.GoLeftTrigger);
                    _playerModel.PlayerState = PlayerState.GoingLeft;
                    StartCoroutine(MovePlayerLeft());
                    break;

                case CommandType.GoRight:
                    _playerAnimator.SetTrigger(PangConstants.GoRightTrigger);
                    _playerModel.PlayerState = PlayerState.GoingRight;
                    StartCoroutine(MovePlayerRight());
                    break;

                case CommandType.Stand:
                    _playerAnimator.SetTrigger(PangConstants.IdleTrigger);
                    Game.GameModel.PlayerModel.PlayerState = PlayerState.Idle;
                    break;

                case CommandType.ApplyDamage:
                    Game.GameModel.PlayerModel.Lives--;
                    Game.GameView.StatsView.UpdateLives();
                    break;
            }
        }
    }
