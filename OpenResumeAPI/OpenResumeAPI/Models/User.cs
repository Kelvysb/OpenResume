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
                    bool resetPassword,
                    string resetToken,
                    string confirmationToken,
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
            ResetPassword = resetPassword;
            ResetToken = resetToken;
            ConfirmationToken = confirmationToken;
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
        public bool ResetPassword { get; set; }

        [ColumnName("resetToken")]
        public string ResetToken { get; set; }

        [ColumnName("confirmationToken")]
        public string ConfirmationToken { get; set; }

        [ColumnName("createdDate")]
        public DateTime CreatedDate { get; set; }

        [ColumnName("updatedDate")]
        public DateTime UpdatedDate { get; set; }

        [ColumnName("lastActivity")]
        public DateTime LastActivity { get; set; }

        public string Token { get; set; }

    }
}
