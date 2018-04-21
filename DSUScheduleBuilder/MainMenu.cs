using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DSUScheduleBuilder
{
    using Network;

    public partial class MainMenu : Form
    {
        private int currDisplay = 0;
        Main_Menu.Search s = null;

        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void CurrentBtn_Click(object sender, EventArgs e)
        {
            Clear();
            currDisplay = 0;
            weekView.Show();
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            HttpRequester.Default.Logout();

            Login l = new Login();
            l.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Clear()
        {
            if (currDisplay == 0)
                weekView.Hide();
            else if (currDisplay == 1)
                this.Controls.Remove(s);

        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            Clear();
            currDisplay = 1;

            // Create new instance of search menu
            s = new Main_Menu.Search();
            s.Size = weekView.Size;
            this.Controls.Add(s);
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        private void EditAcctBtn_Click(object sender, EventArgs e)
        {
            UpdateUser u = new UpdateUser();
            u.Show();
            this.Hide();
        }
    }
}
