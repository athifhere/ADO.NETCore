using ADO.NETCore.Models;
using System.Data.SqlClient;
using System.Data;

namespace ADO.NETCore.DataAccess
{
    public class DAL
    {
        string connectionString = "data source=DESKTOP-7OCUR3A; initial catalog =Assignment;Integrated Security=True";
        public IEnumerable<StudentModel> GetAllStudents()
        {
            List<StudentModel> lstStudents = new List<StudentModel>();
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from Student", conn);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    StudentModel student = new StudentModel();
                    student.Id = Convert.ToInt32(rdr["Id"]);
                    student.FirstName = rdr["FirstName"].ToString();
                    student.LastName = rdr["LastName"].ToString();
                    student.Email = rdr["Email"].ToString();
                    student.Mobile = rdr["Mobile"].ToString();
                    student.Address = rdr["Address"].ToString();

                    lstStudents.Add(student);
                }
                conn.Close();
            }
            return lstStudents;
        }

        public void CreateStudent(StudentModel model)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand
                    ("insert into Student VALUES (@FirstName, @LastName, @Email, @Mobile, @Address)", conn);
                cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                cmd.Parameters.AddWithValue("@LastName", model.LastName);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
                cmd.Parameters.AddWithValue("@Address", model.Address);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateStudent(StudentModel model)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string updateCommand = "UPDATE Student SET FirstName = @FirstName, " +
                    "LastName = @LastName, Email = @Email, Mobile = @Mobile, Address = @Address " +
                    "where Id = @Id";
                SqlCommand cmd = new SqlCommand(updateCommand, conn);
                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                cmd.Parameters.AddWithValue("@LastName", model.LastName);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
                cmd.Parameters.AddWithValue("@Address", model.Address);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public StudentModel GetStudent(int id)
        {
            StudentModel student = new StudentModel();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string selectCommand = "select * from Student where Id = " + id;
                SqlCommand cmd = new SqlCommand(selectCommand, conn);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    student.Id = Convert.ToInt32(rdr["Id"]);
                    student.FirstName = rdr["FirstName"].ToString();
                    student.LastName = rdr["LastName"].ToString();
                    student.Email = rdr["Email"].ToString();
                    student.Mobile = rdr["Mobile"].ToString();
                    student.Address = rdr["Address"].ToString();
                }
            }
            return student;
        }

        public void DeleteStudent(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //string deleteCommand = "delete from Student where Id = " + id;
                string deleteCommand = "spDeleteStudent";
                SqlCommand cmd = new SqlCommand(deleteCommand, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
