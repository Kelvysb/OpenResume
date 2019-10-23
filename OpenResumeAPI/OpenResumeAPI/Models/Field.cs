using OpenResumeAPI.Helpers.Attributes;
using System;

namespace OpenResumeAPI.Models
{
    [TableName("fields")]
    public class Field : ModelBase
    {
        public Field(int id,
                           string name,
                           string description,
                           int itemOrder,
                           int userId,
                           int resumeId,
                           int blockId,
                           string lineType,
                           string content) : base(id, name, description, itemOrder)
        {
            UserId = userId;
            ResumeId = resumeId;
            BlockId = blockId;
            FieldType = lineType;
            Content = content;
        }

        [ColumnName("userId")]
        public int UserId { get; private set; }

        [ColumnName("resumeId")]
        public int ResumeId { get; private set; }

        [ColumnName("blockId")]
        public int BlockId { get; private set; }

        [ColumnName("fieldType")]
        public string FieldType { get; private set; }

        [ColumnName("content")]
        public string Content { get; private set; }

        [ColumnName("level")]
        public int Level { get; private set; }

        [ColumnName("initialDate")]
        public DateTime InitialDate { get; private set; }

        [ColumnName("finalDate")]
        public DateTime FinalDate { get; private set; }
    }
}
