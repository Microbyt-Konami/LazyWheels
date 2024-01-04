using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MicrobytKonami.System;

namespace MicrobytKonami.LazyWheels.Controllers
{
    public class GameController : MonoBehaviourSingleton<GameController>
    {
        [SerializeField] public float deacelerateGrass;

        public float DeacelerateGrass => deacelerateGrass;
        // Start is called before the first frame update
        //void Start()
        //{

        //}

        // Update is called once per frame
        //void Update()
        //{

        //}
    }
}
