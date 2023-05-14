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
using System.Data.SqlClient;

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
            LoadStudents();
        }

        private void LoadStudents()
        {
            // Xóa dữ liệu cũ
            students.Clear();

            // Lấy danh sách sinh viên từ CSDL và thêm vào danh sách students
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Students";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string studentID = reader.GetString(0);
                    string subject = reader.GetString(1);
                    int score = reader.GetInt32(2);

                    Student student = new Student(studentID, subject, score);
                    students.Add(student);
                }
                reader.Close();
            }
        }
        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            string studentID = txtStudentID.Text;
            string subject = txtSubject.Text;
            int score = int.Parse(txtScore.Text);

            Student student = new Student(studentID, subject, score);
            students.Add(student);

            ClearInputs();
            LoadStudents();
        }

        private void btSua_Click(object sender, RoutedEventArgs e)
        {
            if(dgvScores.SelectedItems is Student selectedStudent)
            {
                selectedStudent.StudentID = txtStudentID.Text;
                selectedStudent.Subject = txtSubject.Text;
                selectedStudent.Score = int.Parse(txtScore.Text);

                ClearInputs();
                LoadStudents();
            }
        }

        private void btXoa_CLick(object sender, RoutedEventArgs e)
        {
            if (dgvScores.SelectedItem is Student selectedStudent)
            {
                students.Remove(selectedStudent);
                ClearInputs();
            }
            LoadStudents();
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
