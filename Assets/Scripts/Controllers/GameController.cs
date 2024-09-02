using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MicrobytKonami.System;

using MicrobytKonami.LazyWheels.UI;

namespace MicrobytKonami.LazyWheels.Controllers
{
    public class GameController : MonoBehaviourSingleton<GameController>
    {
        [Header("References")]
        [SerializeField] private BuilderBlocks builderBlocks;
        [SerializeField] private PlayerController player;

        [Header("Debug")]
        [SerializeField] private bool isGameOver = false;

        public BlockController FindBlockInY(float y) => builderBlocks.FindBlockInY(y);

        public void LoadCarIAs(ICollection<CarIAController> carsIA)
        {
        }

        public void MoveCarIA(CarIAController carIA)
        {

        }

        public void GameOver()
        {
            if (isGameOver)
                return;

            player.IsStopCounterMeter = true;
            isGameOver = true;
            player.SetModoGhost(true);
            UIController.Instance.ShowGameOver();
        }
    }
}
