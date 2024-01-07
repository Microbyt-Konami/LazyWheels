using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MicrobytKonami.System;

namespace MicrobytKonami.LazyWheels.Controllers
{
    public class GameController : MonoBehaviourSingleton<GameController>
    {
        [SerializeField] private float inputYDeacelerateGrass;
        [SerializeField] private float inputXDeacelerateGrass;

        public float InputYDeacelerateGrass => inputYDeacelerateGrass;
        public float InputXDeacelerateGrass => inputXDeacelerateGrass;
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
