using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MicrobytKonami.System;

namespace MicrobytKonami.LazyWheels.Controllers
{
    public class GameController : MonoBehaviourSingleton<GameController>
    {
        private BuilderBlocks builderBlocks;

        public BlockController FindBlockInY(float y) => builderBlocks.FindBlockInY(y);

        public void LoadCarIAs(ICollection<CarIAController> carsIA)
        {
        }

        public void MoveCarIA(CarIAController carIA)
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            builderBlocks = GameObject.FindObjectOfType<BuilderBlocks>();
        }

        // Update is called once per frame
        //void Update()
        //{

        //}
    }
}
