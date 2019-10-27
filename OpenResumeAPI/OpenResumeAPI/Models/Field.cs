using OpenResumeAPI.Helpers.Attributes;
using System;

namespace OpenResumeAPI.Models
{
    [TableName("fields")]
    public class Field : ModelBase
    {
        public Field(int id,
                     int userId,
                     int resumeId,
                     int blockId,
                     string name,
                     string description,
                     int itemOrder,
                     string lineType,
                     string content,
                     DateTime initialDate,
                     DateTime finalDate,
                     bool present) : base(id, name, description, itemOrder)
        {
            UserId = userId;
            ResumeId = resumeId;
            BlockId = blockId;
            FieldType = lineType;
            Content = content;
            InitialDate = initialDate;
            FinalDate = finalDate;
            Present = present;
        }

        [ColumnName("userId")]
        public int UserId { get; set; }

        [ColumnName("resumeId")]
        public int ResumeId { get; set; }

        [ColumnName("blockId")]
        public int BlockId { get; set; }

        [ColumnName("fieldType")]
        public string FieldType { get; set; }

        [ColumnName("content")]
        public string Content { get; set; }

        [ColumnName("level")]
        public int Level { get; set; }

        [ColumnName("initialDate")]
        public DateTime InitialDate { get; set; }

        [ColumnName("finalDate")]
        public DateTime FinalDate { get; set; }

        [ColumnName("present")]
        public bool Present { get; set; }
    }
}
