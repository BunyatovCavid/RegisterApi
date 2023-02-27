using RegisterApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace RegisterApi.Crud
{
    public class Register
    {
        SqlConnection connection;
        SqlCommand command;
        string connectionstring = @"Data Source=WIN-PFGV5N8DK24;Initial Catalog=Mekteb;Integrated Security=True";

        public List<RegisterModel> Back_Get()
        {
            List<RegisterModel> back_register = new List<RegisterModel>();
            string commandstring = "Select * From Login";
            command = new SqlCommand(commandstring, connection);

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            SqlDataReader dr = command.ExecuteReader();


            while (dr.Read())
            {
                back_register.Add(new RegisterModel
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    E_mail = dr["E_Mail"].ToString(),
                    Name = dr["Name"].ToString(),
                    Password = dr["Password"].ToString(),
                    RPassword = dr["RPassword"].ToString(),
                    Repost = "Succesful."
                });
            }

            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return back_register;
        }

        public Register()
        {
            connection = new SqlConnection(connectionstring);
        }
        public IEnumerable<RegisterModel> RegisterPost(RegisterModel model)
        {
            List<RegisterModel> register = new List<RegisterModel>();
            List<RegisterModel> Check_Email = new List<RegisterModel>();
            RegisterModel check = new RegisterModel();

            Check_Email = Back_Get();

            int i = Check_Email.Count - 1;

            if (model.Name == null || model.E_mail == null || model.Password == null || model.RPassword == null)
            {
                check.Repost = "Fill in all the boxes";
                register.Add(new RegisterModel { Repost = check.Repost });
                return register;
            }
            else if (model.Password != model.RPassword)
            {
                check.Repost = "Password and RPassword are not the same";
                register.Add(new RegisterModel { Repost = check.Repost });
                return register;
            }

            foreach (var item in Check_Email)
            {
                if (item.E_mail == model.E_mail)
                {
                    check.Repost = "This email has already been mentioned";
                    register.Add(new RegisterModel { Repost = check.Repost });
                    i--;
                    return register;
                }
            }

            string commandstring = @"Insert Into Login Values(@name,@email,@password,@rpassword)";
            command = new SqlCommand(commandstring, connection);

            command.Parameters.AddWithValue("@email", model.E_mail);
            command.Parameters.AddWithValue("@name", model.Name);
            command.Parameters.AddWithValue("@password", model.Password);
            command.Parameters.AddWithValue("@rpassword", model.RPassword);


            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            command.ExecuteReader();

            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }


            if (register.Count == 0)
            {
                register = Back_Get();
            }
            else
            {
                register = null;
                register = Back_Get();
            }

            return register;
        }

    }
}
