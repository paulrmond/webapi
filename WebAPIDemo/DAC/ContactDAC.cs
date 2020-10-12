using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPIDemo.Entities;

namespace WebAPIDemo.DAC
{
    public class ContactDAC
    {
        public List<Contact> GetContacts()
        {
            List<Contact> contacts = new List<Contact>();
            var conStr = ConfigurationManager.AppSettings.Get("dbconn");
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                var sqlQuery = "Select * from tblContacts";
                using (SqlCommand cmd = new SqlCommand(sqlQuery,con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Contact _contact = new Contact();
                            _contact.user_id = reader["user_id"].ToString();
                            _contact.name = reader["name"].ToString();
                            _contact.email = reader["email"].ToString();
                            _contact.contact_number = reader["contact_number"].ToString();
                            contacts.Add(_contact);
                        }
                    }
                }
            }

            return contacts;
        }

        public Contact GetContactsByID(int id)
        {
            Contact contact = new Contact();
            var conStr = ConfigurationManager.AppSettings.Get("dbconn");
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                var sqlQuery = "Select * from tblContacts where user_id = @id";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            contact.user_id = reader["user_id"].ToString();
                            contact.name = reader["name"].ToString();
                            contact.email = reader["email"].ToString();
                            contact.contact_number = reader["contact_number"].ToString();
                        }
                    }
                }
            }

            return contact;
        }

        public int AddContact(Contact contact)
        {
            int i = 0;
            var conStr = ConfigurationManager.AppSettings.Get("dbconn");
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                var sqlQuery = "insert into tblContacts values(@name,@contact,@email)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@name", contact.name);
                    cmd.Parameters.AddWithValue("@contact", contact.contact_number);
                    cmd.Parameters.AddWithValue("@email", contact.email);
                    i = cmd.ExecuteNonQuery();
                }
            }

            return i;
        }
    }
}
