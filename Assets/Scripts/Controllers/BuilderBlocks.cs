using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels.Controllers
{
    public class BuilderBlocks : MonoBehaviour
    {
        // Fields
        [SerializeField] private BlockController[] blocks;

        // Components
        private Transform transformBuilderBlocks;
        private PlayerController playerController;
        private CameraController cameraController;
        private BlockController currentBlock, oldBlock, newBlock;
        private Transform transformBlocks, transformCurrentBlock, transformPlayerController, transformCarIAs;

        // Variables
        private int countRoad;
        private float twiceCameraHeight;
        private float yToCreateBlock;
        private float yOffOld = float.MaxValue;

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
            transformCarIAs = GameObject.Find("CarIAs").GetComponent<Transform>();

            playerController = FindAnyObjectByType<PlayerController>();
            cameraController = Camera.main.GetComponent<CameraController>();
            transformPlayerController = playerController.GetComponent<Transform>();
            twiceCameraHeight = 2 * cameraController.Height;
            CalcBlocksHeights();

            var block = transformBlocks.GetChild(0).GetComponent<BlockController>();

            block.CalcHeight();
            SetYToCreateBlock(block);
            SetCurrentBlock(block);
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
            oldBlock = currentBlock;
            currentBlock = block;
            transformCurrentBlock = block.GetComponent<Transform>();
            if (oldBlock != null)
                yOffOld = oldBlock.YTop + 2 * cameraController.Height;
            PutCarsIAs(block.CarsIAs);
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
                carIA.SetParent(transformBlocks);
                carIA.gameObject.SetActive(true);
            }
        }
    }
}