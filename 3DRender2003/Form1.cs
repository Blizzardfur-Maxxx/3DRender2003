using System;
using System.Drawing;
using System.Windows.Forms;

namespace _DRender2003
{
    public partial class Form1 : Form
    {
        private Renderer renderer;
        private Timer renderTimer;  
        private System.Windows.Forms.Button toggleButton;
        private int frameCount = 0; 
        private DateTime lastUpdate = DateTime.Now;

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
            this.KeyPreview = true;

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
            renderer.RenderFrame();
            frameCount++;

            
            if ((DateTime.Now - lastUpdate).TotalSeconds >= 1)
            {
                this.Text = "FPS: " + frameCount.ToString();
                frameCount = 0; //
                lastUpdate = DateTime.Now;
            }

            this.Invalidate();
        }

        private void ToggleButton_Click(object sender, EventArgs e)
        {
            renderer.fillShapes = !renderer.fillShapes; // Toggle the fillShapes flag
        }

        // Override the OnPaint method to draw the framebuffer onto the form
        protected override void OnPaint(PaintEventArgs e)
        {

            e.Graphics.DrawImage(renderer.framebuffer, 0, 0);

            // Create a basic font and draw the FPS text
            Font font = new Font("Arial", 10, FontStyle.Regular);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            e.Graphics.DrawString("FPS: " + frameCount.ToString(), font, blackBrush, 10, 10); //FPS Count!!!

            base.OnPaint(e);
        }

        // Handle key presses to control the camera
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            // Call the MoveCamera method in the renderer
            renderer.MoveCamera(e.KeyCode);
            this.Invalidate(); // Request to repaint the form
        }

    }
}
