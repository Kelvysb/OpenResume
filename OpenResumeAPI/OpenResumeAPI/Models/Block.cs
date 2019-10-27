using OpenResumeAPI.Helpers.Attributes;
using System.Collections.Generic;

namespace OpenResumeAPI.Models
{
    [TableName("blocks")]
    public class Block : ModelBase
    {
        public Block() : base()
        {        
            Fields = new List<Field>();
        }

        public Block(int id,
                     int userId,
                     int resumeId,
                     string name,
                     string description,
                     int itemOrder,
                     string blockType,
                     string title,
                     string content,
                     List<Field> fields) : base(id, name, description, itemOrder)
        {
            UserId = userId;
            ResumeId = resumeId;
            BlockType = blockType;
            Title = title;
            Content = content;
            Fields = fields;
        }

        [ColumnName("userId")]
        public int UserId { get; set; }

        [ColumnName("resumeId")]
        public int ResumeId { get; set; }

        [ColumnName("blockType")]
        public string BlockType { get; set; }

        [ColumnName("title")]
        public string Title { get; set; }

        [ColumnName("content")]
        public string Content { get; set; }

        public List<Field> Fields { get; set; }
    }
}
