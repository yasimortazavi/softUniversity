using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using softUniversity.Models;
using System.Linq;

namespace softUniversity
{
    public partial class FormStudent : Form
    {
        List<Student> students;
        Student selectedStudent;

        List<Proof> proofs;

        public FormStudent()
        {
            InitializeComponent();
            students = new List<Student>();
            selectedStudent = null;
            proofs = new List<Proof>();
        }

        private void txt_ID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void Register_Click(object sender, EventArgs e)
        {


            if (txt_FirstName.Text.Trim() == "" || txt_LastName.Text.Trim() == "" ||
                txt_ID.Text.Trim() == "")
            {
                MyMessageBox.ValidationErrorMessage((" اطلاعات نام و نام خانوادگی و شماره دانشجویی نمی تواند خالی باشد ");
               
                return;
            }


            int ProofId =Convert.ToInt32(combo_Proof.SelectedValue);

            Proof selectedProof = FindProofs(ProofId);


            if (selectedStudent == null)
            {
                // Register
                Student student = new Student();
                student.FirstName = txt_FirstName.Text.Trim();
                student.LastName = txt_LastName.Text.Trim();
                student.ID = Convert.ToInt32(txt_ID.Text.Trim());

                student.IsMarried = check_Married.Checked;
                student.Gender = radio_Female.Checked;
                student.Proof = selectedProof;

                students.Add(student);

                MyMessageBox.SuccessMessage("ثبت شد");
            }
            else
            {
                // edit
                selectedStudent.FirstName = txt_FirstName.Text.Trim();
                selectedStudent.LastName = txt_LastName.Text.Trim();
                selectedStudent.ID = Convert.ToInt32(txt_ID.Text.Trim());
                selectedStudent.IsMarried = check_Married.Checked;
                selectedStudent.Gender = radio_Female.Checked;
                selectedStudent.Proof = selectedProof;

                MyMessageBox.SuccessMessage();

            }

            ShowStudents();

            ClearText();
        }

        private void ClearText()
        {
            txt_FirstName.Text = txt_LastName.Text = txt_ID.Text = "";
            txt_FirstName.Focus();

            radio_Male.Checked = true;

            check_Married.Checked = false;

            selectedStudent = null;
        }

        private void ShowStudents()
        {
            GridStudent.AutoGenerateColumns = false;
            GridStudent.DataSource = students.ToList();
            if (GridStudent.RowCount > 0)
            {
                GridStudent.Rows[0].Selected = false;
            }
        }

        private void GridStudent_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            int _id = Convert.ToInt32(GridStudent["Col_ID", e.RowIndex].Value);

            selectedStudent = FindStudent(_id);

            if (selectedStudent != null)
            {
                txt_FirstName.Text = selectedStudent.FirstName;
                txt_LastName.Text = selectedStudent.LastName;
                txt_ID.Text = selectedStudent.ID.ToString();
                check_Married.Checked = selectedStudent.IsMarried;
                radio_Female.Checked = selectedStudent.Gender;
                radio_Male.Checked = !selectedStudent.Gender;
            }


        }

        private Student FindStudent(int id)
        {
            foreach (var student in students)
            {
                if (student.ID == id)
                    return student;
            }

            return null;
        }

        private void NewRegister_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            if (selectedStudent == null)
            {
                MyMessageBox.SelectionErrorMessage();
                return;
            }

            if (!MyMessageBox.ConfirmedMessage())
                return;

            students.Remove(selectedStudent);
            ShowStudents();
            ClearText();

            MyMessageBox.SuccessMessage();


        }


        private void ShowProofs()
        {
            // Read from DB

            proofs.Add(new Proof
            {
                ID = 1,
                ProofName = "کاردانی"
            });
            proofs.Add(new Proof
            {
                ID = 2,
                ProofName = "کارشناسی"
            });
            proofs.Add(new Proof
            {
                ID = 3,
                ProofName = "کارشناسی ارشد"
            });
            proofs.Add(new Proof
            {
                ID = 4,
                ProofName = "دکتری"
            });

            combo_Proof.DataSource =proofs.ToList();
            combo_Proof.DisplayMember = "ProofName";
            combo_Proof.ValueMember = "ID";
            combo_Proof.SelectedIndex = -1;
        }

        private Proof FindProofs(int id)
        {
            foreach (var proof in proofs)
            {
                if (proof.ID == id)
                    return proof;
            }

            return null;
        }

        private void FormStudent_Load(object sender, EventArgs e)
        {
            ShowProofs();
        }
    }
}
