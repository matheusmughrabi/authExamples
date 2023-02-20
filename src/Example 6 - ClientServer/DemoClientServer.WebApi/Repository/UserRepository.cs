namespace DemoClientServer.WebApi.Repository
{
    public class UserRepository
    {
        private List<UserModel> GetUsersMock()
        {
            return new List<UserModel>()
            {
                new UserModel(){ Id = 1, Username = "homer", Password = "123", UserClaims = new List<UserClaim>(){ new UserClaim() { UserId = 1, ClaimType = "Product", ClaimValue = "Create" }, new UserClaim() { UserId = 1, ClaimType = "Product", ClaimValue = "Update" } } },
                new UserModel(){ Id = 2, Username = "bart", Password = "123",  UserClaims = new List<UserClaim>(){ new UserClaim() { UserId = 1, ClaimType = "Product", ClaimValue = "Buy" } } }
            };
        }

        public UserModel GetUserWithClaims(string username, string password)
        {
            return GetUsersMock().FirstOrDefault(user => user.Username == username && user.Password == password);
        }
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<UserClaim> UserClaims { get; set; }
    }

    public class UserClaim
    {
        public int UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
