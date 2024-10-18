using System;
using System.Drawing;
using System.Windows.Forms;

namespace _DRender2003
{
    public partial class Form1 : Form
    {
        private Renderer renderer;
        private Timer renderTimer;
        private Button toggleButton;
        private ComboBox rendererComboBox; // Add ComboBox for selecting renderer
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
            this.toggleButton = new Button();
            this.toggleButton.Location = new Point(10, 10);
            this.toggleButton.Name = "toggleButton";
            this.toggleButton.Size = new Size(100, 30);
            this.toggleButton.Text = "Toggle Fill";
            this.toggleButton.Click += new EventHandler(this.ToggleButton_Click);
            this.Controls.Add(this.toggleButton);

            // Setup ComboBox for selecting renderers
            this.rendererComboBox = new ComboBox();
            this.rendererComboBox.Location = new Point(120, 10);
            this.rendererComboBox.Name = "rendererComboBox";
            this.rendererComboBox.Size = new Size(120, 30);

            // Add items individually instead of using AddRange
            this.rendererComboBox.Items.Add("Cube");
            this.rendererComboBox.Items.Add("Sphere");
            this.rendererComboBox.Items.Add("Pyramid");

            this.rendererComboBox.SelectedIndexChanged += new EventHandler(this.RendererComboBox_SelectedIndexChanged);
            this.rendererComboBox.KeyDown += new KeyEventHandler(RendererComboBox_KeyDown);
            this.rendererComboBox.SelectedIndex = 0; // Set default selection
            this.Controls.Add(this.rendererComboBox);
        }


        // This method is called on each tick of the render timer
        private void OnRender(object sender, EventArgs e)
        {
            renderer.RenderFrame();
            frameCount++;

            if ((DateTime.Now - lastUpdate).TotalSeconds >= 1)
            {
                this.Text = "FPS: " + frameCount.ToString();
                frameCount = 0;
                lastUpdate = DateTime.Now;
            }

            this.Invalidate();
        }

        private void ToggleButton_Click(object sender, EventArgs e)
        {
            renderer.fillShapes = !renderer.fillShapes; // Toggle the fillShapes flag
        }

        // Handle the ComboBox selection change
        private void RendererComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rendererComboBox.SelectedItem.ToString())
            {
                case "Cube":
                    renderer.SetRendererType(Renderer.RendererType.Cube);
                    break;
                case "Sphere":
                    renderer.SetRendererType(Renderer.RendererType.Sphere);
                    break;
                case "Pyramid":
                    renderer.SetRendererType(Renderer.RendererType.Pyramid);
                    break;
            }
            this.Invalidate(); // Request to repaint the form with the new renderer
        }

        private void RendererComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Prevent the ComboBox from responding to arrow keys
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.Handled = true; // Mark the event as handled
            }
        }

        // Override the OnPaint method to draw the framebuffer onto the form
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(renderer.framebuffer, 0, 0);

            // Create a basic font and draw the FPS text
            Font font = new Font("Arial", 10, FontStyle.Regular);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            e.Graphics.DrawString("FPS: " + frameCount.ToString(), font, blackBrush, 10, 10);

            base.OnPaint(e);
        }

        // Handle key presses to control the camera
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            renderer.MoveCamera(e.KeyCode);
            this.Invalidate(); // Request to repaint the form
        }
    }
}
