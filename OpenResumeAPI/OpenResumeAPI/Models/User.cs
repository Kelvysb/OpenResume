using OpenResumeAPI.Helpers.Attributes;
using System;

namespace OpenResumeAPI.Models
{
    [TableName("users")]
    public class User : ModelBase
    {
        public User(int id,
                    string name,
                    string description,
                    int itemOrder,
                    string login,
                    string email,
                    string lastName,
                    string passwordHash,
                    bool emailConfirmed,
                    bool resetPasword,
                    string token,
                    DateTime createdDate,
                    DateTime updatedDate,
                    DateTime lastActivity) : base(id, name, description, itemOrder)
        {
            Login = login;
            Email = email;
            LastName = lastName;
            PasswordHash = passwordHash;
            EmailConfirmed = emailConfirmed;
            ResetPasword = resetPasword;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            LastActivity = lastActivity;
            Token = token;
        }

        [ColumnName("login")]
        public string Login { get; private set; }

        [ColumnName("email")]
        public string Email { get; private set; }

        [ColumnName("lastName")]
        public string LastName { get; private set; }

        [ColumnName("passwordHash")]
        public string PasswordHash { get; private set; }

        [ColumnName("emailConfirmed")]
        public bool EmailConfirmed { get; private set; }

        [ColumnName("resetPassword")]
        public bool ResetPasword { get; private set; }

        [ColumnName("createdDate")]
        public DateTime CreatedDate { get; private set; }

        [ColumnName("updatedDate")]
        public DateTime UpdatedDate { get; private set; }

        [ColumnName("lastActivity")]
        public DateTime LastActivity { get; private set; }

        public string Token { get; private set; }

    }
}
