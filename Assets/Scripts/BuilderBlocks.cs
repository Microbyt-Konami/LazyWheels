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
        private Transform transformBlocks, transformCurrentBlock, transformPlayerController;

        // Variables
        private int countRoad;
        private float twiceCameraHeight;
        private float yBlockCurrent;
        private float yOffOldCurrent = float.MaxValue;

        private void Awake()
        {
            transformBuilderBlocks = GetComponent<Transform>();
            oldBlock = null;
            countRoad = 1;
        }

        // Start is called before the first frame update
        void Start()
        {
            transformBlocks = GameObject.Find("Blocks").GetComponent<Transform>();

            playerController = FindAnyObjectByType<PlayerController>();
            cameraController = Camera.main.GetComponent<CameraController>();
            transformPlayerController = playerController.GetComponent<Transform>();
            twiceCameraHeight = 2 * cameraController.Height;
            CalcBlocksHeights();
            SetCurrentBlock(transformBlocks.GetChild(0).GetComponent<BlockController>());
        }

        // Update is called once per frame
        //void Update()
        //{
        //}

        private void LateUpdate()
        {
            BuildBlock();
        }

        private void CalcBlocksHeights()
        {
            foreach (var block in blocks)
                block.CalcHeight();
        }

        private void SetCurrentBlock(BlockController block)
        {
            block.CalcHeight();
            oldBlock = currentBlock;
            currentBlock = block;
            transformCurrentBlock = block.GetComponent<Transform>();
            yBlockCurrent = block.YTop - cameraController.Height;
        }

        private void BuildBlock()
        {
            if (transformPlayerController.position.y >= yBlockCurrent)
                CreateNewBlock();
        }

        private void CreateNewBlock()
        {
            var blockSelected = blocks[countRoad - 1];
            var height = (currentBlock.Height + blockSelected.Height) / 2;

            BlockController blockNew = Instantiate<BlockController>(blockSelected, transformCurrentBlock.position + height * Vector3.up, Quaternion.identity);

            countRoad++;
            blockNew.name = $"Block{countRoad}";
            blockNew.transform.parent = transformBlocks;
            SetCurrentBlock(blockNew);
            if (countRoad > blocks.Length)
                countRoad = 1;
        }


    }
}
