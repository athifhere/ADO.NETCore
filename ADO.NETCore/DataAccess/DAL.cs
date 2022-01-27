using ADO.NETCore.Models;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace ADO.NETCore.DataAccess
{
    public class DAL
    {
        string connectionString = "data source=DESKTOP-7OCUR3A; initial catalog =Assignment;Integrated Security=True";

        public IEnumerable<StudentModel> GetAllStudents()
        {
            List<StudentModel> lstStudents = new List<StudentModel>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                lstStudents = db.Query<StudentModel>("Select * From Student").ToList();
            }
            return lstStudents;
        }

        public void CreateStudent(StudentModel model)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = "Insert Into Student Values(@FirstName, @LastName, @Email, @Mobile, @Address)";

                int rowsAffected = db.Execute(sqlQuery, model);
            }
        }

        public void UpdateStudent(StudentModel model)
        {
            string spName = "spUpdateStudent";
            var parameters = new { @Id = model.Id, @FirstName = model.FirstName, @LastName = model.LastName, 
                @Email = model.Email, @Mobile = model.Mobile, @Address = model.Address };

            using(IDbConnection db = new SqlConnection(connectionString))
            {
                var affectedRows = db.Execute(spName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public StudentModel GetStudent(int id)
        {
            StudentModel student = new StudentModel();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                student = db.Query<StudentModel>("Select * From Student WHERE Id =" + id).SingleOrDefault();
            }
            return student;
        }

        public void DeleteStudent(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = "Delete from Student where Id=" + id;
                int rowsAffected = db.Execute(sqlQuery);
            }
        }

    }
}
