using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PROJECT4.Models;

namespace PROJECT4.Data
{
    public class CustomerService
    {
        ConnectDb db;

        public CustomerService()
        {
            db = new ConnectDb();
        }

        public List<Country> GetCountry()
        {
            SqlCommand cmd = new SqlCommand("USP_GET_COUNTRY", db.connect);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if(db.connect.State == System.Data.ConnectionState.Closed)
            {
                db.connect.Open();
            }

            SqlDataReader dr = cmd.ExecuteReader();
            List<Country> list = new List<Country>();
            while(dr.Read())
            {
                Country country = new Country();
                country.Id = Convert.ToInt32(dr["id"]);
                country.Name = dr["name"].ToString();
                list.Add(country);
            }
            db.connect.Close();
            return list;
        }
        
        public List<State> GetState(int id)
        {
            SqlCommand cmd = new SqlCommand("USP_GET_STATE", db.connect);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            if(db.connect.State == System.Data.ConnectionState.Closed)
            {
                db.connect.Open();
            }

            SqlDataReader dr = cmd.ExecuteReader();
            List<State> list = new List<State>();
            while(dr.Read())
            {
                State state = new State();
                state.Id = Convert.ToInt32(dr["id"]);
                state.Name = dr["name"].ToString();
                list.Add(state);
            }
            db.connect.Close();
            return list;
        }


        public List<City> GetCity(int id)
        {
            SqlCommand cmd = new SqlCommand("USP_GET_CITY", db.connect);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            if(db.connect.State == System.Data.ConnectionState.Closed)
            {
                db.connect.Open();
            }

            SqlDataReader dr = cmd.ExecuteReader();
            List<City> list = new List<City>();
            while(dr.Read())
            {
                City city = new City();
                city.Id = Convert.ToInt32(dr["id"]);
                city.Name = dr["name"].ToString();
                list.Add(city);
            }
            db.connect.Close();
            return list;
        }
        
        public List<Customer> GetCustomers()
        {
            SqlCommand cmd = new SqlCommand("USP_GET_CUSTOMERS", db.connect);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if(db.connect.State == System.Data.ConnectionState.Closed)
            {
                db.connect.Open();
            }

            SqlDataReader dr = cmd.ExecuteReader();
            List<Customer> list = new List<Customer>();
            while(dr.Read())
            {
                Customer customer = new Customer();
                customer.id = Convert.ToInt32(dr["Id"]);
                customer.name = dr["Name"].ToString();
                customer.email = dr["Email"].ToString();
                customer.mobile = dr["Mobile"].ToString();
                customer.gender = dr["Gender"].ToString();
                customer.country = dr["Country"].ToString();
                customer.state = dr["State"].ToString();
                customer.city = dr["City"].ToString();
                list.Add(customer);
            }
            db.connect.Close();
            return list;
        }


        public List<Customer> GetBinCustomers()
        {
            SqlCommand cmd = new SqlCommand("USP_GET_BINCUSTOMERS", db.connect);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if (db.connect.State == System.Data.ConnectionState.Closed)
            {
                db.connect.Open();
            }

            SqlDataReader dr = cmd.ExecuteReader();
            List<Customer> list = new List<Customer>();
            while (dr.Read())
            {
                Customer customer = new Customer();
                customer.id = Convert.ToInt32(dr["Id"]);
                customer.name = dr["Name"].ToString();
                customer.email = dr["Email"].ToString();
                customer.mobile = dr["Mobile"].ToString();
                customer.gender = dr["Gender"].ToString();
                customer.country = dr["Country"].ToString();
                customer.state = dr["State"].ToString();
                customer.city = dr["City"].ToString();
                list.Add(customer);
            }
            db.connect.Close();
            return list;
        }


        public Customer GetCustomer(int id)
        {
            SqlCommand cmd = new SqlCommand("USP_GET_CUSTOMER", db.connect);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            if(db.connect.State == System.Data.ConnectionState.Closed)
            {
                db.connect.Open();
            }

            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Customer customer = new Customer();
            customer.id = Convert.ToInt32(dr["Id"]);
            customer.name = dr["Name"].ToString();
            customer.email = dr["Email"].ToString();
            customer.mobile = dr["Mobile"].ToString();
            customer.gender = dr["Gender"].ToString();
            customer.country = dr["Country"].ToString();
            customer.state = dr["State"].ToString();
            customer.city = dr["City"].ToString();
            
            db.connect.Close();
            return customer;
        }


        public Models.Action CreateRecord(Customer customer)
        {
            SqlCommand cmd = new SqlCommand("USP_SAVE_CUSTOMER",db.connect);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name",customer.name);
            cmd.Parameters.AddWithValue("@email",customer.email);
            cmd.Parameters.AddWithValue("@mobile", customer.mobile);
            cmd.Parameters.AddWithValue("@gender", customer.gender);
            cmd.Parameters.AddWithValue("@country", customer.country);
            cmd.Parameters.AddWithValue("@state", customer.state);
            cmd.Parameters.AddWithValue("@city", customer.city);

            if(db.connect.State == System.Data.ConnectionState.Closed)
            {
                db.connect.Open();
            }
            int result = (int) cmd.ExecuteScalar();
            Models.Action action;
            if(result == 1)
            {
                action = Models.Action.Success;
            }
            else if(result==2)
            {
                action = Models.Action.EmailExist;
            }
            else
            {
                action = Models.Action.Error;
            }
            return action;
        }
        
        
        public Models.Action UpdateRecord(Customer customer)
        {
            SqlCommand cmd = new SqlCommand("USP_UPDATE_CUSTOMER",db.connect);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", customer.id);
            cmd.Parameters.AddWithValue("@name",customer.name);
            cmd.Parameters.AddWithValue("@email",customer.email);
            cmd.Parameters.AddWithValue("@mobile", customer.mobile);
            cmd.Parameters.AddWithValue("@gender", customer.gender);
            cmd.Parameters.AddWithValue("@country", customer.country);
            cmd.Parameters.AddWithValue("@state", customer.state);
            cmd.Parameters.AddWithValue("@city", customer.city);

            if(db.connect.State == System.Data.ConnectionState.Closed)
            {
                db.connect.Open();
            }
            int result = (int) cmd.ExecuteScalar();
            Models.Action action;
            if(result == 1)
            {
                action = Models.Action.Success;
            }
            else if(result==3)
            {
                action = Models.Action.NotRegistered;
            }
            else
            {
                action = Models.Action.Error;
            }
            return action;
        }
        
        public Models.Action DeleteRecord(int id)
        {
            SqlCommand cmd = new SqlCommand("USP_DELETE_CUSTOMER",db.connect);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id",id);
            

            if(db.connect.State == System.Data.ConnectionState.Closed)
            {
                db.connect.Open();
            }
            int result = (int) cmd.ExecuteScalar();
            Models.Action action;
            if(result == 1)
            {
                action = Models.Action.Success;
            }
            else if(result==3)
            {
                action = Models.Action.NotRegistered;
            }
            else
            {
                action = Models.Action.Error;
            }
            return action;
        }
        
        public Models.Action PermanentDeleteRecord(int id)
        {
            SqlCommand cmd = new SqlCommand("USP_PERMENENT_DELETE_CUSTOMER",db.connect);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id",id);
            

            if(db.connect.State == System.Data.ConnectionState.Closed)
            {
                db.connect.Open();
            }
            int result = (int) cmd.ExecuteScalar();
            Models.Action action;
            if(result == 1)
            {
                action = Models.Action.Success;
            }
            else if(result==3)
            {
                action = Models.Action.NotRegistered;
            }
            else
            {
                action = Models.Action.Error;
            }
            return action;
        }
        
        public Models.Action UndoRecord(int id)
        {
            SqlCommand cmd = new SqlCommand("USP_UNDO_CUSTOMER",db.connect);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id",id);
            

            if(db.connect.State == System.Data.ConnectionState.Closed)
            {
                db.connect.Open();
            }
            int result = (int) cmd.ExecuteScalar();
            Models.Action action;
            if(result == 1)
            {
                action = Models.Action.Success;
            }
            else if(result==4)
            {
                action = Models.Action.NotFound;
            }
            else
            {
                action = Models.Action.Error;
            }
            return action;
        }


        public void UpdateProfileImage(int id, string path)
        {
            SqlCommand cmd = new SqlCommand("UPDATE_PROFILE_IMAGE", db.connect);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@path", path);

            if (db.connect.State == System.Data.ConnectionState.Closed)
            {
                db.connect.Open();
            }
            int result = (int)cmd.ExecuteNonQuery();
            db.connect.Close();
        }
    }
}
