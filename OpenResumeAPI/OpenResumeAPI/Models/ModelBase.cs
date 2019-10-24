using OpenResumeAPI.Helpers.Attributes;
using OpenResumeAPI.Models.Interfaces;

namespace OpenResumeAPI.Models
{
    public abstract class ModelBase : IModel
    {
        protected ModelBase()
        {
        }

        protected ModelBase(int id, string name, string description, int itemOrder)
        {
            Id = id;
            Name = name;
            Description = description;
            ItemOrder = itemOrder;
        }

        [ColumnName("id")]
        public int Id { get; set; }

        [ColumnName("name")]
        public string Name { get; set; }

        [ColumnName("description")]
        public string Description { get; set; }

        [ColumnName("itemOrder")]
        public int ItemOrder { get; set; }

    }
}
