namespace TokenService.WebApi.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public static class UsersRepositoryMock
    {
        private static List<UserModel> _users =>
            new List<UserModel>
            {
               new UserModel
                {
                    Id = Guid.NewGuid(),
                    Username = "johndoe",
                    Password = "1234",
                    Role = "Admin"
                },

                new UserModel
                {
                    Id = Guid.NewGuid(),
                    Username = "janedoe",
                    Password = "1234",
                    Role = "Client"
                }
            };

        public static UserModel Get(string username, string password)
        {
            return _users.FirstOrDefault(c => c.Username == username && c.Password == password);
        }
    }
}
