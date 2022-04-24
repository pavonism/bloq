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
    class Hook
    {
        protected const int HOOK_RADIUS = 4;
        protected const int HOOK_FILL_RADIUS = 4;
        protected int PEN_SIZE = 2;
        public bool open { get; set; }
        public Point Location;
        public Block? parent { get; set; }

        public int getHookRadius()
        {
            return HOOK_RADIUS;
        }

        public Hook(Point newlocation)
        {
            open = true;
            Location = newlocation;
        }

        public void Open()
        {
            open = true;
        }

        public void Close()
        {
            open = false;
        }

        public virtual void drawHook(Image drawContext)
        {
            if(open)
            {
                Pen pen = new Pen(Brushes.Black, PEN_SIZE);

                using (Graphics g = Graphics.FromImage(drawContext))
                {
                    g.DrawEllipse(pen, Location.X - HOOK_RADIUS, Location.Y - HOOK_RADIUS, HOOK_RADIUS * 2, HOOK_RADIUS * 2);
                }

                pen.Dispose();
            }
        }

        public bool trySelect(Point mousePosition)
        {
            return Math.Pow(mousePosition.X - Location.X, 2) + Math.Pow(mousePosition.Y - Location.Y, 2) <= Math.Pow(HOOK_RADIUS, 2) && open;
        }
    }

    [Serializable]
    class HookSource : Hook
    {
        private const int PEN_LINK_SIZE = 1;
        public Hook Destination;

        public HookSource(Point newlocation) : base(newlocation)
        {
            Destination = new Hook(Location);
        }

        public HookSource(Point newlocation, Block parent) : base(newlocation)
        {
            Destination = new Hook(Location);
            this.parent = parent;
        }

        public override void drawHook(Image drawContext)
        {
            if (open)
            {
                base.drawHook(drawContext);
                using (Graphics g = Graphics.FromImage(drawContext))
                {
                    g.FillEllipse(Brushes.Black, Location.X - HOOK_FILL_RADIUS, Location.Y - HOOK_FILL_RADIUS, HOOK_FILL_RADIUS * 2, HOOK_FILL_RADIUS * 2);

                }
            }
            else drawLink(drawContext);

        }

        public void drawLink(Image drawContext)
        {
            Pen pen = new Pen(Brushes.Black, PEN_LINK_SIZE);
            AdjustableArrowCap arrow = new AdjustableArrowCap(5, 7);
            arrow.Filled = true;
            pen.CustomEndCap = arrow;

            using (Graphics g = Graphics.FromImage(drawContext))
            {
                g.DrawLine(pen, Location, Destination.Location);
            }

            pen.Dispose();
        }

    }

    [Serializable]
    class HookDestination : Hook
    {
        public Hook Source;

        public HookDestination(Point newlocation) : base(newlocation)
        {
            Source = new Hook(Location);
        }

        public HookDestination(Point newlocation, Block parent) : base(newlocation)
        {
            Source = new Hook(Location);
            this.parent = parent;
        }

        public override void drawHook(Image drawContext)
        {
            if (open)
            {
                base.drawHook(drawContext);
                using (Graphics g = Graphics.FromImage(drawContext))
                {
                    g.FillEllipse(Brushes.White, Location.X - HOOK_FILL_RADIUS, Location.Y - HOOK_FILL_RADIUS, HOOK_FILL_RADIUS * 2, HOOK_FILL_RADIUS * 2);

                }
            }

        }

    }
}
