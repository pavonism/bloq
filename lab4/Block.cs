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

namespace lab4
{

    [Serializable]
    abstract class Block
    {
        protected const int OPERATION_BLOCK_WIDTH = 100;
        protected const int OPERATION_BLOCK_HEIGHT = 50;
        protected const int PEN_SIZE = 2;

        protected bool dashedPen;

        public Point location;
        public string Text { get; set; }

        protected Font font = new Font(new FontFamily("arial"), 8, FontStyle.Regular);

        public abstract void Draw(Image drawContext);
        public abstract HookSource checkHookSources(Point mousePosition);
        public abstract HookDestination checkHooks(Point mousePosition);
        public abstract void releaseHookSources();
        public abstract double TrySelect(Point mousePosition);
        public abstract void Select();
        public abstract void Unselect();
        public abstract void Move(int dx, int dy);
        public abstract void MoveTo(int x, int y);

    }

    [Serializable]
    class OperationBlock : Block
    {
        private HookDestination upHookDestination;
        private HookSource downHookSource;

        private RectangleF textRect;

        public OperationBlock(Point newLocation, string newText)
        {
            textRect = new RectangleF(location.X - OPERATION_BLOCK_WIDTH / 2 + PEN_SIZE, location.Y - OPERATION_BLOCK_HEIGHT / 2 + PEN_SIZE, OPERATION_BLOCK_WIDTH -  PEN_SIZE, OPERATION_BLOCK_HEIGHT - PEN_SIZE);

            Text = newText;
            location = newLocation;
            upHookDestination = new HookDestination(new Point(location.X, location.Y - OPERATION_BLOCK_HEIGHT/2), this);
            downHookSource = new HookSource(new Point(location.X, location.Y + OPERATION_BLOCK_HEIGHT / 2), this);
        }

        public override void Draw(Image drawContext)
        {
            textRect = new RectangleF(location.X - OPERATION_BLOCK_WIDTH / 2 + PEN_SIZE, location.Y - OPERATION_BLOCK_HEIGHT / 2 + PEN_SIZE, OPERATION_BLOCK_WIDTH - PEN_SIZE, OPERATION_BLOCK_HEIGHT - PEN_SIZE);

            StringFormat textFormat = new StringFormat();
            textFormat.Alignment = StringAlignment.Center;
            textFormat.LineAlignment = StringAlignment.Center;

            Pen pen = new Pen(Brushes.Black, PEN_SIZE);

            if (dashedPen == false)
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            else
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            int xcoord = location.X - OPERATION_BLOCK_WIDTH / 2;
            int ycoord = location.Y - OPERATION_BLOCK_HEIGHT / 2;

            using (Graphics g = Graphics.FromImage(drawContext))
            {
                g.DrawRectangle(pen, xcoord, ycoord, OPERATION_BLOCK_WIDTH, OPERATION_BLOCK_HEIGHT);
                g.FillRectangle(Brushes.White, xcoord + 1, ycoord + 1, OPERATION_BLOCK_WIDTH - 2, OPERATION_BLOCK_HEIGHT - 2);

                g.DrawString(Text, font, Brushes.Black, textRect, textFormat);

                upHookDestination.drawHook(drawContext);
                downHookSource.drawHook(drawContext);
            }

            pen.Dispose();
            textFormat.Dispose();
        }

        public override HookSource checkHookSources(Point mousePosition)
        {
            if (downHookSource.trySelect(mousePosition)) return downHookSource;
            return null;
        }

        public override HookDestination checkHooks(Point mousePosition)
        {
            if (upHookDestination.trySelect(mousePosition)) return upHookDestination;
            return null;
        }
        public override void releaseHookSources()
        {
            downHookSource.Destination.Open();
            upHookDestination.Source.Open();
        }

        public override double TrySelect(Point mousePosition)
        {
            dashedPen = false;

            if(mousePosition.X >= location.X - OPERATION_BLOCK_WIDTH/2 && mousePosition.X <= location.X + OPERATION_BLOCK_WIDTH/2 && 
                mousePosition.Y >= location.Y - OPERATION_BLOCK_HEIGHT/2 && mousePosition.Y <= location.Y + OPERATION_BLOCK_HEIGHT/2)
            {
                return Math.Pow(location.X - mousePosition.X, 2) + Math.Pow(location.Y - mousePosition.Y, 2);
            }

            return double.MaxValue;
        }

        public override void Select()
        {
            dashedPen = true;
        }

        public override void Unselect()
        {
            dashedPen = false;
        }

        public override void Move(int dx, int dy)
        {
            location.X += dx;
            location.Y += dy;

            upHookDestination.Location.X += dx;
            upHookDestination.Location.Y += dy;
            downHookSource.Location.X += dx;
            downHookSource.Location.Y += dy;

        }

        public override void MoveTo(int x, int y)
        {
            location.X = x;
            location.Y = y;

            upHookDestination.Location = new Point(location.X, location.Y - OPERATION_BLOCK_HEIGHT / 2);
            downHookSource.Location = new Point(location.X, location.Y + OPERATION_BLOCK_HEIGHT / 2);
        }

    }

    [Serializable]
    class DecisiveBlock : Block
    {
        private const int FT_SPACING = 3;

        private HookDestination upHookDestination;
        private HookSource rightHookSource;
        private HookSource leftHookSource;

        Point[] points;
        Point[] points_fill;

        private const int diagX = 52;
        private const int diagY = 38;

        private RectangleF textRect;


        public DecisiveBlock(Point newLocation, string newText)
        {
            location = newLocation;
            Text = newText;

            textRect = new RectangleF(location.X - diagX / 2 + PEN_SIZE, location.Y - diagY / 2 + PEN_SIZE, diagX - PEN_SIZE, diagY - PEN_SIZE);

            points = new Point[] { 
                new Point(location.X - diagX, location.Y), 
                new Point(location.X, location.Y - diagY),
                new Point(location.X + diagX, location.Y), 
                new Point(location.X, location.Y + diagY) 
            };

            points_fill = new Point[] { 
                new Point(location.X - diagX + PEN_SIZE, location.Y), 
                new Point(location.X, location.Y - diagY + PEN_SIZE),
                new Point(location.X + diagX - PEN_SIZE, location.Y), 
                new Point(location.X, location.Y + diagY - PEN_SIZE)
            };

            upHookDestination = new HookDestination(points[1], this);
            rightHookSource = new HookSource(points[2], this);
            leftHookSource = new HookSource(points[0], this);
        }

        private void updatePoints()
        {
            points = new Point[] {
                new Point(location.X - diagX, location.Y),
                new Point(location.X, location.Y - diagY),
                new Point(location.X + diagX, location.Y),
                new Point(location.X, location.Y + diagY)
            };

            points_fill = new Point[] {
                new Point(location.X - diagX + PEN_SIZE, location.Y),
                new Point(location.X, location.Y - diagY + PEN_SIZE),
                new Point(location.X + diagX - PEN_SIZE, location.Y),
                new Point(location.X, location.Y + diagY - PEN_SIZE)
            };
        }

        public override void Draw(Image drawContext)
        {
            textRect = new RectangleF(location.X - diagX / 2 + PEN_SIZE, location.Y - diagY / 2 + PEN_SIZE, diagX - PEN_SIZE, diagY - PEN_SIZE);

            StringFormat textFormat = new StringFormat();
            textFormat.Alignment = StringAlignment.Center;
            textFormat.LineAlignment = StringAlignment.Center;

            Pen pen = new Pen(Brushes.Black, PEN_SIZE);

            if (dashedPen == false)
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            else
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            updatePoints();

            using (Graphics g = Graphics.FromImage(drawContext))
            {

                g.DrawPolygon(pen, points);
                g.FillPolygon(Brushes.White, points_fill);

                upHookDestination.drawHook(drawContext);
                rightHookSource.drawHook(drawContext);
                leftHookSource.drawHook(drawContext);

                var sizeT = g.MeasureString("T", font);
                var sizeF = g.MeasureString("F", font);
                g.DrawString("F", font, Brushes.Black, new Point(points[2].X - ((int)sizeF.Width)/2, points[2].Y - leftHookSource.getHookRadius() - ((int)sizeF.Height) - FT_SPACING));
                g.DrawString("T", font, Brushes.Black, new Point(points[0].X - ((int)sizeT.Width)/2, points[0].Y - rightHookSource.getHookRadius() - ((int)sizeT.Height) - FT_SPACING));


                g.DrawString(Text, font, Brushes.Black, textRect, textFormat);

            }

            pen.Dispose();
        }

        public override HookSource checkHookSources(Point mousePosition)
        {
            if (rightHookSource.trySelect(mousePosition)) return rightHookSource;
            if (leftHookSource.trySelect(mousePosition)) return leftHookSource;
            return null;
        }

        public override HookDestination checkHooks(Point mousePosition)
        {
            if (upHookDestination.trySelect(mousePosition)) return upHookDestination;
            return null;
        }

        public override void releaseHookSources()
        {
            rightHookSource.Destination.Open();
            leftHookSource.Destination.Open();
            upHookDestination.Source.Open();
        }

        public override double TrySelect(Point mousePosition)
        {
            dashedPen = false;

            if (Math.Pow(mousePosition.X - location.X, 2) / Math.Pow(points[2].X - location.X, 2) + Math.Pow(mousePosition.Y - location.Y, 2) / Math.Pow(points[1].Y - location.Y, 2) <= 1)
            {
                return Math.Pow(location.X - mousePosition.X, 2) + Math.Pow(location.Y - mousePosition.Y, 2);
            }

            return double.MaxValue;
        }


        public override void Select()
        {
            dashedPen = true;
        }

        public override void Unselect()
        {
            dashedPen = false;
        }


        public override void Move(int dx, int dy)
        {
            location.X += dx;
            location.Y += dy;

            upHookDestination.Location.X += dx;
            upHookDestination.Location.Y += dy;

            rightHookSource.Location.X += dx;
            rightHookSource.Location.Y += dy;

            leftHookSource.Location.X += dx;
            leftHookSource.Location.Y += dy;

        }


        public override void MoveTo(int x, int y)
        {
            location.X = x;
            location.Y = y;

            updatePoints();
            upHookDestination.Location = points[1];
            rightHookSource.Location = points[2];
            leftHookSource.Location = points[0];
        }


    }

    [Serializable]
    class StartEndBlock : Block
    {
        private const int ELLIPSE_RADIUS_X = 40;
        private const int ELLIPSE_RADIUS_Y = 25;
        private new const int PEN_SIZE = 3;

        HookSource? downHookSource;
        HookDestination? upHookDestination;

        public StartEndBlock(Point newLocation, string name)
        {
            location = newLocation;
            Text = name;
            if(name == "START")
            {

                downHookSource = new HookSource(new Point(location.X, location.Y + ELLIPSE_RADIUS_Y), this);
                upHookDestination = null;
            } 
            else if(name == "STOP")
            {
                upHookDestination = new HookDestination(new Point(location.X, location.Y - ELLIPSE_RADIUS_Y), this);
                downHookSource = null;
            }
            
        }

        public override void Draw(Image drawContext)
        {


            using (Graphics g = Graphics.FromImage(drawContext))
            {
                Pen pen = new Pen(Brushes.Black, PEN_SIZE);
                Pen coloredPen = new Pen(Brushes.LightGreen, PEN_SIZE);
             
                if (Text == "STOP")
                    coloredPen = new Pen(Brushes.Red, PEN_SIZE);

                if(dashedPen == false)
                    coloredPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                else
                    coloredPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;


                var textSize = g.MeasureString(Text, font);
                g.DrawEllipse(coloredPen, location.X - ELLIPSE_RADIUS_X, location.Y - ELLIPSE_RADIUS_Y, ELLIPSE_RADIUS_X * 2, ELLIPSE_RADIUS_Y * 2);
                g.FillEllipse(Brushes.White, location.X - ELLIPSE_RADIUS_X + PEN_SIZE, location.Y - ELLIPSE_RADIUS_Y + PEN_SIZE, (ELLIPSE_RADIUS_X - PEN_SIZE) * 2, (ELLIPSE_RADIUS_Y - PEN_SIZE) * 2);
                g.DrawString(Text, font, Brushes.Black, new Point(location.X - ((int)textSize.Width)/2, location.Y - ((int)textSize.Height)/2));


                upHookDestination?.drawHook(drawContext);
                downHookSource?.drawHook(drawContext);

                pen.Dispose();
                coloredPen.Dispose();
            }

        }

        public override HookSource checkHookSources(Point mousePosition)
        {
            if(Text == "STOP")
            return null;

            if (downHookSource.trySelect(mousePosition)) return downHookSource;

            return null;
        }

        public override HookDestination checkHooks(Point mousePosition)
        {
            if (Text == "START") return null;

            if (upHookDestination.trySelect(mousePosition)) return upHookDestination;


            return null;
        }

        public override void releaseHookSources()
        {
            if(Text == "START")
            {
                downHookSource.Destination.Open();
            }
            if(Text == "STOP")
            {
                upHookDestination.Source.Open();
            }
        }

        public override double TrySelect(Point mousePosition)
        {
            dashedPen = false;
            if (Math.Pow(mousePosition.X - location.X, 2) / Math.Pow(ELLIPSE_RADIUS_X, 2) + Math.Pow(mousePosition.Y - location.Y, 2) / Math.Pow(ELLIPSE_RADIUS_Y, 2) <= 1)
            {
                return Math.Pow(location.X - mousePosition.X, 2) + Math.Pow(location.Y - mousePosition.Y, 2);
            }

            return double.MaxValue;
        }


        public override void Select()
        {
            dashedPen = true;
        }

        public override void Unselect()
        {
            dashedPen = false;
        }


        public override void Move(int dx, int dy)
        {
            location.X += dx;
            location.Y += dy;

            if(Text == "START")
            {
                downHookSource.Location.X += dx;
                downHookSource.Location.Y += dy;
            } else if(Text == "STOP")
            {
                upHookDestination.Location.X += dx;
                upHookDestination.Location.Y += dy;
            }

        }


        public override void MoveTo(int x, int y)
        {
            location.X = x;
            location.Y = y;

            if (Text == "START")
            {
                downHookSource.Location = new Point(location.X, location.Y + ELLIPSE_RADIUS_Y);
            }
            else if (Text == "STOP")
            {
                upHookDestination.Location = new Point(location.X, location.Y - ELLIPSE_RADIUS_Y);
            }
        }

    }

}
