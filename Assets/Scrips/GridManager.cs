using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GameObject cellPF;
        [SerializeField] private Sprite cellImagePF;
        [SerializeField] private Canvas myCanvas;
        private int xBYx;
        private List<Transform> currentGrid = new List<Transform>();
        private void Awake()
        {
            if (cellPF == null) Debug.LogWarning("No cell PF");
        }

        public void InitGrid(int size, Canvas canvas = null, Sprite m_cellSprite = null)
        {
            SetSize(size);
            if (canvas != null)
                SetCanvas(canvas);
            if (m_cellSprite != null)
                SetCellImage(m_cellSprite);
        }

        public void SetCanvas(Canvas canvas) => myCanvas = canvas;
        public void SetSize(int size) => xBYx = size;
        public void SetCellImage(Sprite mImage) => cellImagePF = mImage;
        
        public List<Transform> GenerateGrid(bool show = false)
        {
            List<Vector2> positions = GeneratePositions();
            ClearGrid();
            for (int i = 0; i < xBYx * xBYx; i++)
            {
                currentGrid.Add(InitCell(positions[i]) );
            }
            OutputGrid(show);
            return currentGrid;
        }
        private void OutputGrid(bool onOff)
        {
            foreach(Transform cell in currentGrid)
            {
                if (onOff)
                    cell.gameObject.SetActive(true);
                else
                    cell.gameObject.SetActive(false);
            }
        }
        private void ClearGrid()
        {
            if (currentGrid.Count == 0)
                return;
            foreach(Transform cell in currentGrid)
            {
                Destroy(cell.gameObject);
            }
            currentGrid.Clear();
        }
        private Transform InitCell(Vector2 mPos)
        {
            GameObject obj = Instantiate(cellPF, myCanvas.transform);
            obj.GetComponent<RectTransform>().localPosition = mPos;
            Image mImage = obj.GetComponent<Image>();
            if (cellImagePF != null && cellImagePF != null)
                mImage.sprite = cellImagePF;
            return obj.transform;
        }

        private List<Vector2> GeneratePositions()
        {
            float cellSize = cellPF.GetComponent<RectTransform>().rect.width;
            List<Vector2> positions = new List<Vector2>();
            Vector2 cornerReference = new Vector2();
            if (xBYx % 2 != 0)
            {
                // cornerReference = new Vector2(-1*cellSize/2, cellSize/2) + new Vector2(-1, 1) * (xBYx / 2 - 1) * cellSize;
                cornerReference = Vector2.zero + new Vector2(-cellSize * (xBYx / 2), cellSize * (xBYx / 2));
                for (int i = 0; i < xBYx; i++)
                {
                    float y = -1 * cellSize * i;
                    for (int k = 0; k < xBYx; k++)
                    {
                        float x = cellSize * k;
                        Vector2 pos = cornerReference + new Vector2(x, y);
                        positions.Add(pos);
                    }
                }

            }
            return positions;
        }
    }
}
