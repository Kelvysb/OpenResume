using OpenResumeAPI.Helpers.Attributes;
using System.Collections.Generic;

namespace OpenResumeAPI.Models
{
    [TableName("blocks")]
    public class Block : ModelBase
    {
        public Block(int id,
                     string name,
                     string description,
                     int itemOrder,
                     string userId,
                     string resumeId,
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
        public string UserId { get; private set; }

        [ColumnName("resumeId")]
        public string ResumeId { get; private set; }

        [ColumnName("blockType")]
        public string BlockType { get; private set; }

        [ColumnName("title")]
        public string Title { get; private set; }

        [ColumnName("content")]
        public string Content { get; private set; }

        public List<Field> Fields { get; private set; }
    }
}
