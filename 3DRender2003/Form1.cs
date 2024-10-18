using System;
using System.Drawing;
using System.Windows.Forms;

namespace _DRender2003
{
    public partial class Form1 : Form
    {
        private Renderer renderer;  // Instance of the Renderer class
        private Timer renderTimer;  // Timer to control the rendering loop
        private System.Windows.Forms.Button toggleButton;
        private int frameCount = 0;  // Frame count for FPS calculation
        private DateTime lastUpdate = DateTime.Now;  // Last update time for FPS

        public Form1()
        {
            InitializeComponent();

            // Initialize the renderer
            renderer = new Renderer();

            // Set up a timer to trigger the rendering loop
            renderTimer = new Timer();
            renderTimer.Interval = 1000 / 30; // ~30 FPS
            renderTimer.Tick += new EventHandler(OnRender);
            renderTimer.Enabled = true; // Start the timer

            // Setup toggle button
            this.toggleButton = new System.Windows.Forms.Button();
            this.toggleButton.Location = new System.Drawing.Point(10, 10);
            this.toggleButton.Name = "toggleButton";
            this.toggleButton.Size = new System.Drawing.Size(100, 30);
            this.toggleButton.Text = "Toggle Fill";
            this.toggleButton.Click += new System.EventHandler(this.ToggleButton_Click);
            this.Controls.Add(this.toggleButton);
        }

        // This method is called on each tick of the render timer
        private void OnRender(object sender, EventArgs e)
        {
            renderer.RenderFrame(); // Render the next frame using the Renderer class
            frameCount++; // Increment frame count

            // Update FPS every second
            if ((DateTime.Now - lastUpdate).TotalSeconds >= 1)
            {
                this.Text = "FPS: " + frameCount.ToString(); // Set the window title to show FPS
                frameCount = 0; // Reset frame count
                lastUpdate = DateTime.Now; // Reset last update time
            }

            this.Invalidate(); // Force the form to repaint
        }

        private void ToggleButton_Click(object sender, EventArgs e)
        {
            renderer.fillShapes = !renderer.fillShapes; // Toggle the fillShapes flag
        }

        // Override the OnPaint method to draw the framebuffer onto the form
        protected override void OnPaint(PaintEventArgs e)
        {
            // Draw the entire frame buffer to the screen from the renderer
            e.Graphics.DrawImage(renderer.framebuffer, 0, 0);

            // Create a basic font and draw the FPS text
            // Use default system font as a fallback
            Font font = new Font("Arial", 10, FontStyle.Regular); // Use whatever parameters are available
            SolidBrush blackBrush = new SolidBrush(Color.Black); // Use SolidBrush instead of Brushes

            // Ensure to use a RectangleF for positioning
            e.Graphics.DrawString("FPS: " + frameCount.ToString(), font, blackBrush, 10, 10);

            base.OnPaint(e);
        }
    }
}
