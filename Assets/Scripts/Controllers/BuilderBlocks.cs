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
        [SerializeField] private Transform transformCarIAs;
        [SerializeField] private BlockController block0;
        [SerializeField] private BlockController block1;

        // Components
        private Transform transformBuilderBlocks;
        private PlayerController playerController;
        private CameraController cameraController;
        private BlockController currentBlock, oldBlock, newBlock;
        private Transform transformCurrentBlock, transformPlayerController;

        // Variables
        private int countRoad;
        private float twiceCameraHeight;
        private float yToCreateBlock;
        private float yOffOld = float.MaxValue;

        private void Awake()
        {
            transformBuilderBlocks = GetComponent<Transform>();
            countRoad = 1;
        }

        // Start is called before the first frame update
        void Start()
        {
            playerController = FindAnyObjectByType<PlayerController>();
            cameraController = Camera.main.GetComponent<CameraController>();
            transformPlayerController = playerController.GetComponent<Transform>();
            twiceCameraHeight = 2 * cameraController.Height;
            CalcBlocksHeights();

            //var block = transformBlocks.GetChild(0).GetComponent<BlockController>();
            var block = block1;
            block.CalcHeight();
            SetYToCreateBlock(block);
            SetCurrentBlock(block);

            block = block0;

            block.CalcHeight();
            SetOldBlock(block);
            PutCarsIAs(block.CarsIAs);
        }

        // Update is called once per frame
        void Update()
        {
            BuildBlock();
            ChangeToNewBlock();
            DestroyOldBlock();
        }

        private void CalcBlocksHeights()
        {
            foreach (var block in blocks)
                block.CalcHeight();
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

        private void DestroyOldBlock()
        {
            if (oldBlock != null)
                if (transformPlayerController.position.y > yOffOld)
                {
                    Destroy(oldBlock.gameObject);
                    oldBlock = null;
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
            yOffOld = oldBlock.YTop + 2 * cameraController.Height;
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
            _newBlock.CalcHeight();
            newBlock = _newBlock;
        }

        private void PutCarsIAs(ICollection<CarIAController> carIAs)
        {
            foreach (var carIA in carIAs)
            {
                carIA.SetParent(transformCarIAs);
                carIA.IsMoving = true;
                carIA.gameObject.SetActive(true);
            }
        }
    }
}