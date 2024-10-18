using System;
using System.Drawing;

namespace _DRender2003
{
    public class LineRenderer
    {
        public Renderer renderer;

        public LineRenderer(Renderer renderer)
        {
            this.renderer = renderer;
        }

        public void DrawLine(Graphics g, int x1, int y1, int x2, int y2, Color color)
        {
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);
            int sx = (x1 < x2) ? 1 : -1;
            int sy = (y1 < y2) ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                SetPixel(x1, y1, color);

                if (x1 == x2 && y1 == y2) break;

                int err2 = err * 2;

                if (err2 > -dy)
                {
                    err -= dy;
                    x1 += sx;
                }
                if (err2 < dx)
                {
                    err += dx;
                    y1 += sy;
                }
            }
        }

        private void SetPixel(int x, int y, Color color)
        {
            if (x >= 0 && x < Renderer.SCREEN_WIDTH && y >= 0 && y < Renderer.SCREEN_HEIGHT)
            {
                renderer.framebuffer.SetPixel(x, y, color);
            }
        }
    }
}
