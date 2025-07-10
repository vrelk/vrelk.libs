﻿using System;

namespace Vrelk.Libs.DapperExt.Mapper
{
    public interface IColumn
    {
        Guid TableIdentity { get; set; }
        string Alias { get; set; }
        string SimpleAlias { get; set; }
        IClassMapper ClassMapper { get; set; }
        IMemberMap Property { get; set; }
        Table Table { get; set; }
    }
    public class Column : IColumn
    {
        public Guid TableIdentity { get; set; }
        public string Alias { get; set; }
        public string SimpleAlias { get; set; }
        public IClassMapper ClassMapper { get; set; }
        public IMemberMap Property { get; set; }
        public Table Table { get; set; }

        public Column()
        {
        }

        public Column(string columnNameAlias, IMemberMap property, IClassMapper classMapper, Table table)
        {
            Alias = columnNameAlias;
            ClassMapper = classMapper;
            Property = property;
            TableIdentity = table.Identity;
            Table = table;
        }
    }
}
