using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QLD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Student> students;

        public MainWindow()
        {
            InitializeComponent();
            students = new List<Student>();
            dgvScores.ItemsSource = students;
        }

        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            string studentID = txtStudentID.Text;
            string subject = txtSubject.Text;
            int score = int.Parse(txtScore.Text);

            Student student = new Student(studentID, subject, score);
            students.Add(student);

            ClearInputs();
        }

        private void btSua_Click(object sender, RoutedEventArgs e)
        {
            if(dgvScores.SelectedItems is Student selectedStudent)
            {
                selectedStudent.StudentID = txtStudentID.Text;
                selectedStudent.Subject = txtSubject.Text;
                selectedStudent.Score = int.Parse(txtScore.Text);

                ClearInputs();
            }
        }

        private void btXoa_CLick(object sender, RoutedEventArgs e)
        {
            if (dgvScores.SelectedItem is Student selectedStudent)
            {
                students.Remove(selectedStudent);
                ClearInputs();
            }
        }
        private void ClearInputs()
        {
            txtStudentID.Text = string.Empty;
            txtSubject.Text = string.Empty;
            txtScore.Text = string.Empty;
        }
    }
    public class Student
    {
        public string StudentID { get; set; }
        public string Subject { get; set; }
        public int Score { get; set; }

        public Student(string studentID, string subject, int score)
        {
            StudentID = studentID;
            Subject = subject;
            Score = score;
        }
    }
}
