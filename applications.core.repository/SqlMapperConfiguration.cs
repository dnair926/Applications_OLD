using Applications.Core;
using System.Collections.Generic;
using System.Data;

namespace Applications.Core.Repository
{
    internal class SqlMapperConfigurationConstants
    {
        private SqlMapperConfigurationConstants()
        {
        }

        public const string SqlMapperConfigurationGroup = "sqlMapperConfiguration";
        public const string Mappings = "mappings";
        public const string SelectStoredProcedure = "select";
        public const string CrudStoredProcedure = "crud";
        public const string UpdateStoredProcedure = "update";
        public const string DeleteStoredProcedure = "delete";
        public const string InsertStoredProcedure = "insert";
        public const string Name = "name";
        public const string DefaultParameterNeeded = "defaultParameter";
        public const string CommandType = "commandType";
        public const string ConnectionStringName = "connectionStringName";
    }

    public class SqlMapperConfigurationSection
    {
        public IEnumerable<SqlEntity> Mappings { get; set; }
    }

    //public class ElementCollection : ConfigurationElementCollection
    //{
    //    public ElementCollection()
    //    {
    //    }

    //    public Entity this[int index]
    //    {
    //        get { return (Entity)BaseGet(index); }
    //        set
    //        {
    //            if (BaseGet(index) != null)
    //            {
    //                BaseRemoveAt(index);
    //            }
    //            BaseAdd(index, value);
    //        }
    //    }

    //    public void Add(Entity entity)
    //    {
    //        BaseAdd(entity);
    //    }

    //    public void Clear()
    //    {
    //        BaseClear();
    //    }

    //    protected override ConfigurationElement CreateNewElement()
    //    {
    //        return new Entity();
    //    }

    //    protected override object GetElementKey(ConfigurationElement element)
    //    {
    //        return ((Entity)element).Name;
    //    }

    //    public void Remove(Entity entity)
    //    {
    //        if (entity == null)
    //        {
    //            return;
    //        }
    //        BaseRemove(entity.Name);
    //    }

    //    public void RemoveAt(int index)
    //    {
    //        BaseRemoveAt(index);
    //    }

    //    public void Remove(string name)
    //    {
    //        BaseRemove(name);
    //    }

    //    public bool Contains(string name)
    //    {
    //        for (int i = 0; i < this.Count; i++)
    //        {
    //            if (!string.Equals(name, this[i].Name))
    //            {
    //                continue;
    //            }
    //            return true;
    //        }

    //        return false;
    //    }

    //    public Entity Find(string name)
    //    {
    //        for (int i = 0; i < this.Count; i++)
    //        {
    //            var entity = this[i];
    //            if (!string.Equals(name, entity.Name, StringComparison.OrdinalIgnoreCase))
    //            {
    //                continue;
    //            }
    //            return this[i];
    //        }

    //        return null;
    //    }
    //}

    public class SqlEntity
    {
        public string Name { get; set; }

        public CommandDefinition Select { get; set; }

        public CommandDefinition Crud { get; set; }

        public CommandDefinition Delete { get; set; }

        public CommandDefinition Insert { get; set; }

        public CommandDefinition Update { get; set; }

        public CommandDefinition CommandName(CrudAction crudAction)
        {
            CommandDefinition commandDefinition = null;
            if (crudAction == CrudAction.Select)
            {
                commandDefinition = this.Select;
            }

            if (crudAction == CrudAction.Delete)
            {
                commandDefinition = this.Delete;
            }

            if (crudAction == CrudAction.Insert)
            {
                commandDefinition = this.Insert;
            }

            if (crudAction == CrudAction.Update)
            {
                commandDefinition = this.Update;
            }

            if (commandDefinition != null)
            {
                return commandDefinition;
            }

            if (Crud != null)
            {
                commandDefinition = this.Crud;
            }

            if (commandDefinition != null)
            {
                return commandDefinition;
            }

            return null;
        }
    }

    public class CommandDefinition
    {
        public string Command { get; set; }

        public string QueryFilePath { get; set; }

        public bool DefaultParameter { get; set; } = true;

        public CommandType CommandType { get; set; } = CommandType.StoredProcedure;

        public string ConnectionStringName { get; set; } = "Application";
    }
}