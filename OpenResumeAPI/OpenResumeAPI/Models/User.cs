using OpenResumeAPI.Helpers.Attributes;
using System;

namespace OpenResumeAPI.Models
{
    [TableName("users")]
    public class User : ModelBase
    {
        public User() : base() { }

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
        public string Login { get; set; }

        [ColumnName("email")]
        public string Email { get; set; }

        [ColumnName("lastName")]
        public string LastName { get; set; }

        [ColumnName("passwordHash")]
        public string PasswordHash { get; set; }

        [ColumnName("emailConfirmed")]
        public bool EmailConfirmed { get; set; }

        [ColumnName("resetPassword")]
        public bool ResetPasword { get; set; }

        [ColumnName("createdDate")]
        public DateTime CreatedDate { get; set; }

        [ColumnName("updatedDate")]
        public DateTime UpdatedDate { get; set; }

        [ColumnName("lastActivity")]
        public DateTime LastActivity { get; set; }

        public string Token { get; set; }

    }
}
