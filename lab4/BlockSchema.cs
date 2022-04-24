using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#nullable enable


namespace lab4
{
    [Serializable]
    class BlockSchema
    {
        private const int blockOutsideOffset = 20;

        public List<Block> blocks;
        public Bitmap drawContext;
        [NonSerialized]
        private PictureBox? Canvas = null;
        private Block? selectedBlock = null;

        bool hasStart = false;
        bool isSelected = false;

        public bool selected()
        {
            return isSelected;
        }


        public Block? getSelectedBlock()
        {
            return selectedBlock;
        }

        private void selectBlock(Block? value)
        {
            isSelected = value == null ? false : true;
            selectedBlock = value;
        }

        public BlockSchema(int x, int y)
        {
            drawContext = new Bitmap(x, y);
            blocks = new List<Block>();
        }

        public bool alreadyHasStart()
        {
            return hasStart;
        }

        public void Add(Block? newBlock)
        {
            if (newBlock == null) return;
            if (newBlock is StartEndBlock && newBlock.Text == "START") hasStart = true;
            blocks.Add(newBlock);
            newBlock.Draw(drawContext);
        }

        public void Remove(Block removedBlock)
        {
            if (removedBlock is StartEndBlock && removedBlock.Text == "START") hasStart = false;
            if (removedBlock == selectedBlock) isSelected = false;
            blocks.Remove(removedBlock);
        }

        public void ClearCanvas()
        {

            using (Graphics g = Graphics.FromImage(drawContext))
            {
                g.Clear(Color.White);
            }
        }

        public void ClearSchema()
        {
            blocks.Clear();
            selectedBlock = null;
            hasStart = false;
            isSelected = false;
            ClearCanvas();
        }

        public void DrawCanvas()
        {
            ClearCanvas();

            foreach (var block in blocks)
            {
                block.Draw(drawContext);
            }

            Canvas?.Refresh();
        }


        public bool trySelect(Point mousePosition)
        {
            double minDist = double.MaxValue;
            Block? minBlock = null;


            foreach (var block in blocks)
            {
                double curDist = block.TrySelect(mousePosition);
                if (curDist < minDist)
                {
                    minDist = curDist;
                    minBlock = block;
                }
            }

            minBlock?.Select();
            selectBlock(minBlock);

            return isSelected;
        }

        public bool tryRemove(Point mousePosition)
        {
            double minDist = double.MaxValue;
            Block? minBlock = null;

            foreach (var block in blocks)
            {
                double curDist = block.TrySelect(mousePosition);
                if (curDist < minDist)
                {
                    minDist = curDist;
                    minBlock = block;
                }
            }

            if (minBlock != null)
            {
                minBlock.releaseHookSources();
                selectedBlock?.Select();
                Remove(minBlock);
                return true;
            }


            return false;
        }


        public void newCanvas(int x, int y)
        {
            drawContext.Dispose();
            drawContext = new Bitmap(x, y);
        }


        public void AttachSchema(PictureBox Canvas)
        {
            this.Canvas = Canvas;
            Canvas.Image = drawContext;
        }

        public void checkPosition()
        {
            if(selectedBlock?.location.X > drawContext.Width)
            {
                MoveBlockToX(drawContext.Width - blockOutsideOffset);
            }

            if(selectedBlock?.location.Y > drawContext.Height)
            {
                MoveBlockToY(drawContext.Height - blockOutsideOffset);
            }

            if (selectedBlock?.location.Y < 0)
            {
                MoveBlockToY(blockOutsideOffset);
            }

            if (selectedBlock?.location.X < 0)
            {
                MoveBlockToX(blockOutsideOffset);
            }

            DrawCanvas();
        }

        public void moveBlock(int dx, int dy)
        {
            selectedBlock!.Move(dx, dy);
        }



        public void MoveBlockToX(int x)
        {
            selectedBlock!.MoveTo(x, selectedBlock!.location.Y);

        }

        public void MoveBlockToY(int y)
        {
            selectedBlock!.MoveTo(selectedBlock!.location.X, y);

        }

        public bool tryLink(Point mousePosition, HookSource currentHookSource)
        {
            HookDestination? tempHookDestination = null;

            foreach (var block in blocks)
            {
                tempHookDestination = block.checkHooks(mousePosition);
                if (tempHookDestination != null && tempHookDestination.parent != currentHookSource.parent)
                {
                    tempHookDestination.Close();
                    currentHookSource.Close();
                    currentHookSource.Destination = tempHookDestination;
                    tempHookDestination.Source = currentHookSource;

                    DrawCanvas();
                    return true;
                }
            }

            currentHookSource.Open();
            DrawCanvas();
            return false;
        }


        public HookSource? tryFindHook(Point mousePosition)
        {
            HookSource? currentHookSource = null;

            foreach (var block in blocks)
            {
                currentHookSource = block.checkHookSources(mousePosition);
                if (currentHookSource != null)
                {
                    currentHookSource.Close();
                    currentHookSource.Destination.Location = mousePosition;
                    break;
                }
            }

            return currentHookSource;

        }
    }
}
