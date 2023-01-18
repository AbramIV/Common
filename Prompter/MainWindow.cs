using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows.Forms;

namespace Prompter
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ContainerBuilder()
        {
            string data = "[{\"Name\": \"Hint 1\", \"Text\": \"Hint text 1\"}," +
                          "{\"Name\": \"Hint 2\", \"Text\": \"Hint text 2\"}]";

            Hint[] hints = JsonDocument.Parse(data).Deserialize<Hint[]>();

            foreach (var hint in hints)
            {
                Button newButton = new()
                {
                    Text = hint.Name,
                    Visible = true,
                    BackColor = Color.AliceBlue,
                    ForeColor = Color.Black,
                    Height = (int)(ListButtons.Font.Height * 1.5),
                    Dock = DockStyle.Top
                };

                newButton.Click += (s, e) => MessageBox.Show(hint.Text,
                                                             "Hint",
                                                             MessageBoxButtons.OK,
                                                             MessageBoxIcon.Information);
                ListButtons.Controls.Add(newButton);
                newButton.BringToFront();
            }
        }

        #region Events

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Font = new(new FontFamily(System.Drawing.Text.GenericFontFamilies.Serif), 12);
            Height = (int)(Screen.PrimaryScreen.Bounds.Height * 0.98);
            Width = (int)(Screen.PrimaryScreen.Bounds.Width * 0.12);
            Location = new(Screen.PrimaryScreen.Bounds.Width - Width, 0);
            ContainerBuilder();
        }

        private void ButtonToLeft_Click(object sender, EventArgs e)
        {
            Location = new(0, 0);
        }

        private void ButtonToRight_Click(object sender, EventArgs e)
        {
            Location = new(Screen.PrimaryScreen.Bounds.Width - Width, 0);
        }

        private void ToggleBlockResize_CheckedChanged(object sender, EventArgs e)
        {
            FormBorderStyle = ToggleBlockResize.Checked ? FormBorderStyle.FixedSingle
                                                        : FormBorderStyle.Sizable;
        }
        #endregion
    }
}