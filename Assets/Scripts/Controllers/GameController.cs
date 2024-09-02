using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MicrobytKonami.System;

using MicrobytKonami.LazyWheels.UI;

namespace MicrobytKonami.LazyWheels.Controllers
{
    public class GameController : MonoBehaviourSingleton<GameController>
    {
        private BuilderBlocks builderBlocks;
        private PlayerController player;
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

            isGameOver = true;
            player.SetModoGhost(true);
            UIController.Instance.ShowGameOver();
        }

        // Start is called before the first frame update
        void Start()
        {
            builderBlocks = FindObjectOfType<BuilderBlocks>();
            player = FindObjectOfType<PlayerController>();
        }

        // Update is called once per frame
        //void Update()
        //{

        //}
    }
}
