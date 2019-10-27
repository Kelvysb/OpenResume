using OpenResumeAPI.Helpers.Attributes;
using System;
using System.Collections.Generic;

namespace OpenResumeAPI.Models
{
    [TableName("resumes")]
    public class Resume : ModelBase
    {
        public Resume() : base()
        {
            Blocks = new List<Block>();
        }

        public Resume(int id,
                      int userId,
                      string name, 
                      string description, 
                      int itemOrder,
                      string link,
                      string token,
                      string language,
                      string template,
                      int accessLevel,
                      DateTime createdDate,
                      DateTime updatedDate,
                      List<Block> blocks) : base(id, name, description, itemOrder)
        {
            UserId = userId;
            Link = link;
            Token = token;
            Language = language;
            Template = template;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            AccessLevel = accessLevel;
            Blocks = blocks;
        }

        [ColumnName("userId")]
        public int UserId { get; set; }

        [ColumnName("link")]
        public string Link { get; set; }

        [ColumnName("token")]
        public string Token { get; set; }

        [ColumnName("language")]
        public string Language { get; set; }

        [ColumnName("template")]
        public string Template { get; set; }

        [ColumnName("accessLevel")]
        public int AccessLevel { get; set; }

        [ColumnName("createdDate")]
        public DateTime CreatedDate { get; set; }

        [ColumnName("updatedDate")]
        public DateTime UpdatedDate { get; set; }

        public List<Block> Blocks { get; set; }
    }
}
