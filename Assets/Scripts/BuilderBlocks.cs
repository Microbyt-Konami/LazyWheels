using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels
{
    public class BuilderBlocks : MonoBehaviour
    {
        // Fields
        [SerializeField] private BlockController[] blocks;

        // Components
        private Transform transformBuilderBlocks;
        private PlayerController playerController;
        private CameraController cameraController;
        private BlockController currentBlock, oldBlock;
        private Transform transformCurrentBlock;

        // Variables
        private int countRoad;

        private void Awake()
        {
            transformBuilderBlocks = GetComponent<Transform>();

            var transformFirstBlock = transformBuilderBlocks.Find("Block1");

            SetCurrentBlock(transformFirstBlock.GetComponent<BlockController>());
            oldBlock = null;
            countRoad = 1;
        }

        // Start is called before the first frame update
        void Start()
        {
            playerController = FindAnyObjectByType<PlayerController>();
            cameraController = Camera.main.GetComponent<CameraController>();
        }

        // Update is called once per frame
        void Update()
        {
            BuildRoad();
        }

        private void SetCurrentBlock(BlockController block)
        {
            currentBlock = block;
            transformCurrentBlock = block.transform;
        }

        private void BuildRoad()
        {
            //if (currentBlock.transform.)
        }
    }
}
