using System.Data;
using System.Data.SqlClient;
using System.Net.Http.Headers;
namespace CoreMVC.Models
{
    public class EmployeeDB
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-LGM6III9\SQLEXPRESS;database=Asp_core;integrated security=true");
        public string InsertDB(Employee objcls)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_DBInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empna", objcls.ename);//get
                cmd.Parameters.AddWithValue("@empaddr", objcls.eaddr);
                cmd.Parameters.AddWithValue("@empsal", objcls.esal);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return ("Inserted Successfully");
            }
            catch(Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message);
            }
        }
        public int LoginDB(Employee objcls)
        {
            try
            {
                int cid = 0;
                SqlCommand cmd = new SqlCommand("sp_login", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@eid", objcls.eid);
                cmd.Parameters.AddWithValue("@ena", objcls.ename);
                con.Open();
                cid = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                con.Close();
                return cid;
            }
            catch(Exception )
            {
                if(con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }
        public Employee SelectProfileDB(int id)
        {
            var getdata = new Employee();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_selectprofile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    getdata = new Employee
                    {
                        eid = Convert.ToInt32(sdr["Emp_id"]),//set
                        ename = sdr["Emp_Name"].ToString(),
                        eaddr = sdr["Emp_Address"].ToString(),
                        esal = sdr["Emp_Salary"].ToString()
                    };
                }
                con.Close();
                return getdata;
            }
            catch (Exception ex)
            {
                if(con.State != ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }
        public string UpdateDB(Employee emp)
        {
            string retVal = "";
            try
            {
                SqlCommand cmd = new SqlCommand("sp_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@eid", emp.eid);
                cmd.Parameters.AddWithValue("@esal", emp.esal);
                cmd.Parameters.AddWithValue("@addr", emp.eaddr);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                retVal = "Ok updated";
            }
            catch(Exception ex)
            {
                if(con.State != ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message);
            }
            return (retVal);
        }
        public List<Employee> SelectDB()
        {
            var list = new List<Employee>();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_selectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var obj = new Employee
                    {
                        eid = Convert.ToInt32(dr["Emp_Id"]),
                        ename = dr["Emp_Name"].ToString(),
                        eaddr = dr["Emp_Address"].ToString(),
                        esal = dr["Emp_Salary"].ToString()
                    };
                    list.Add(obj);
                }
                con.Close();
                return list;
            }
            catch (Exception )
            {
                if(con.State != ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }
    }
}
