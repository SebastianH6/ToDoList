namespace ToDoList.Services
{
    public class UserState
    {
        public string UserName { get; private set;}
        public bool IsSignedIn => !string.IsNullOrEmpty(UserName);

        public void SignIn(string userName)
        {
            UserName = userName;
            
        }

        public void SignOut()
        {
            UserName = string.Empty;
        }
    }
}