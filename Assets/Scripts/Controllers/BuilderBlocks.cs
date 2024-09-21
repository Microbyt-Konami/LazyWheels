using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels.Controllers
{
    public class BuilderBlocks : MonoBehaviour
    {
        // Fields
        [SerializeField] private BlockController[] blocks;
        [SerializeField] private Transform transformBlocks;
        [SerializeField] private BlockController block1;

        // Components
        private Transform myTransform;
        private PlayerController playerController;
        private CameraController cameraController;
        private BlockController currentBlock, oldBlock, newBlock;
        private Transform transformCurrentBlock, transformPlayerController;
        private List<BlockController> blocksRunning = new List<BlockController>();

        // Variables
        private int countRoad;
        private float twiceCameraHeight;
        private float yToCreateBlock;

        public BlockController FindBlockInY(float y)
        {
            foreach (var block in blocksRunning)
                if (y >= block.YBottom && y <= block.YTop)
                    return block;

            return null;
        }

        private void Awake()
        {
            myTransform = GetComponent<Transform>();
            countRoad = 1;
        }

        // Start is called before the first frame update
        void Start()
        {
            playerController = FindAnyObjectByType<PlayerController>();
            cameraController = Camera.main.GetComponent<CameraController>();
            transformPlayerController = playerController.GetComponent<Transform>();
            twiceCameraHeight = 2 * cameraController.Height;
            SetUpBlocks();
        }

        private void SetUpBlocks()
        {
            foreach (var blockAux in blocks)
                blockAux.CalcHeight();

            //var block = transformBlocks.GetChild(0).GetComponent<BlockController>();
            var block = block1;

            block.SetUp();
            SetYToCreateBlock(block);
            SetCurrentBlock(block);
            if (block1 != null)
                blocksRunning.Add(block1);
            //block.ShowAndMoveAllCarsIAs();
        }

        // Update is called once per frame
        void Update()
        {
            BuildBlock();
            ChangeToNewBlock();
        }

        private void BuildBlock()
        {
            if (transformPlayerController.position.y >= yToCreateBlock)
            {
                CreateNewBlock();
                SetYToCreateBlock(newBlock);
            }
        }

        private void ChangeToNewBlock()
        {
            if (newBlock != null)
                if (transformPlayerController.position.y >= newBlock.YBottom)
                {
                    SetCurrentBlock(newBlock);
                    newBlock = null;
                }
        }

        private void SetCurrentBlock(BlockController block)
        {
            if (currentBlock != null)
                SetOldBlock(currentBlock);
            currentBlock = block;
            transformCurrentBlock = block.GetComponent<Transform>();
            //PutCarsIAs(block.CarsIAs);
        }

        private void SetOldBlock(BlockController block)
        {
            oldBlock = block;
            block.YOld = block.YTop + 2 * cameraController.Height;
        }

        private void SetYToCreateBlock(BlockController block)
        {
            yToCreateBlock = block.YTop - 2 * cameraController.Height;
        }

        private void CreateNewBlock()
        {
            int i = Random.Range(0, blocks.Length);
            BlockController blockSelected = blocks[i];
            float height = currentBlock.HeightFromBlock0 + blockSelected.HeightBlock0;

            BlockController _newBlock = Instantiate(blockSelected, transformCurrentBlock.position + height * Vector3.up,
                Quaternion.identity);

            countRoad++;
            _newBlock.name = $"Block{countRoad}";
            _newBlock.transform.parent = transformBlocks;
            _newBlock.SetUp();
            newBlock = _newBlock;
        }
    }
}