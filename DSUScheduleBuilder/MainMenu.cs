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
    using Models;

    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            List<Course> courses = HttpRequester.Default.GetEnrolledCourses();
            foreach (Course c in courses)
            {
                Console.WriteLine(c.ClassID);
                Console.WriteLine(c.ClassName);
                Console.WriteLine(c.Teacher);
            }

            this.weekView1.SetCourses(courses);
        }

        private void CurrentBtn_Click(object sender, EventArgs e)
        {

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

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            SearchCourses s = new SearchCourses();
            s.Show();
            this.Hide();
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e) {
            HttpRequester.Default.Logout();
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
