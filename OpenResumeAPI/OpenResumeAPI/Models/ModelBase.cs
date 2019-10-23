using OpenResumeAPI.Helpers.Attributes;
using OpenResumeAPI.Models.Interfaces;

namespace OpenResumeAPI.Models
{
    public abstract class ModelBase : IModel
    {
        protected ModelBase(int id, string name, string description, int itemOrder)
        {
            Id = id;
            Name = name;
            Description = description;
            ItemOrder = itemOrder;
        }

        [ColumnName("id")]
        public int Id { get; private set; }

        [ColumnName("name")]
        public string Name { get; private set; }

        [ColumnName("description")]
        public string Description { get; private set; }

        [ColumnName("itemOrder")]
        public int ItemOrder { get; private set; }

    }
}
