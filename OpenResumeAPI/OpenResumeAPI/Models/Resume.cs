using OpenResumeAPI.Helpers.Attributes;
using System;

namespace OpenResumeAPI.Models
{
    [TableName("resumes")]
    public class Resume : ModelBase
    {
        public Resume(int id,
                      int userId,
                      string name, 
                      string description, 
                      int itemOrder,
                      string link,
                      string language,
                      string template,
                      int accessLevel,
                      DateTime createdDate,
                      DateTime updatedDate) : base(id, name, description, itemOrder)
        {
            UserId = userId;
            Link = link;
            Language = language;
            Template = template;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            AccessLevel = accessLevel;
        }

        [ColumnName("userId")]
        public int UserId { get; private set; }

        [ColumnName("link")]
        public string Link { get; private set; }

        [ColumnName("language")]
        public string Language { get; private set; }

        [ColumnName("template")]
        public string Template { get; private set; }

        [ColumnName("accessLevel")]
        public int AccessLevel { get; private set; }

        [ColumnName("createdDate")]
        public DateTime CreatedDate { get; private set; }

        [ColumnName("updatedDate")]
        public DateTime UpdatedDate { get; private set; }
    }
}
