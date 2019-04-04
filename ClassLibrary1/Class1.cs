using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Image
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string FileName { get; set; }
        public int Views { get; set; }
    }
    public class ImageManager
    {
        private string _connectionString;

        public ImageManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int SaveImage(Image image)
        {
            int id = 0;
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Image(password,filename)  " +
                                      "VALUES (@password, @fileName)SELECT SCOPE_IDENTITY()";
                cmd.Parameters.AddWithValue("@password", image.Password);
                cmd.Parameters.AddWithValue("@fileName", image.FileName);
                connection.Open();
                id = (int)(decimal)cmd.ExecuteScalar();
            }
            return id;
        }
        public Image GetImage(int id)
        {
            var image = new Image();
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"SELECT * FROM Image
                                    where @id = id";
                cmd.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    image.Id = (int)reader["Id"];
                    image.Password = (string)reader["Password"];
                    image.FileName = (string)reader["FileName"];
                }
            }
            return image;
        }
        public int UpdateAndGetViews(int id)
        {
            int views = 0;
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"update image 
                                    set Views = Views + 1
                                     where id = @id
                                    select views from Image
		                            where id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    views = (int)reader["views"];
                }
            }
            return views;

        }


    }
}
//Create an application where users can upload images and share
//it with their friends.However, when an image is uploaded,
//the user will be prompted to create a "password" which will
//protect the image from being seen by anyone that doesn't have
//the password.

//Here's the flow of the application:

//On the home page, there should be a textbox and a file upload
//input. The user will then put in a "password" into the textbox
//and choose an image to upload.When they hit submit, they should
//get taken to a page that says:

//"Thank you for uploading your images, here's the link to share with your friends:

//http://localhost:123/images/view?id=14

//Make sure to give them the password of 'foobar'"


//When a user tries to visit an images page, they should first be presented with a
//textbox where they need to put the password saved by the image uploader. If they
//enter it correctly, the page should refresh (same url) and they should see the image.
//Underneath the image, they should also see a little number that displays how many
//times this image has already been viewed (just store this number in the database
//and keep updating it every time it's viewed).  If they put the password in incorrectly,
//the page should refresh with an error message saying "please try again".

//Once they've put in the password, they should never have to put in the password again
//for that image.

//Good luck!