﻿/*
   Copyright 2011 Dorin Huzum, Adrian Popescu.

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
    [Serializable]
    [XmlRoot("journal")]
    public class Journal : IXmlSerializable, IEquatable<Journal>
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlElement("user")]
        public IdentifiableName User { get; set; }

        [XmlElement("notes")]
        public string Notes { get; set; }

        [XmlElement("created_on")]
        public DateTime? CreatedOn { get; set; }

        [XmlArray("details")]
        [XmlArrayItem("detail")]
        public IList<Detail> Details { get; set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            Id = reader.ReadAttributeAsInt("id");
            reader.Read();

            while (!reader.EOF)
            {
                if (reader.IsEmptyElement && !reader.HasAttributes)
                {
                    reader.Read();
                    continue;
                }

                switch (reader.Name)
                {
                    case "user":
                        User = new IdentifiableName(reader);
                        break;

                    case "notes":
                        Notes = reader.ReadElementContentAsString();
                        break;

                    case "created_on":
                        CreatedOn = reader.ReadElementContentAsNullableDateTime();
                        break;

                    case "details":
                        Details = reader.ReadElementContentAsCollection<Detail>();
                        break;

                    default:
                        reader.Read();
                        break;
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
        }

        public bool Equals(Journal other)
        {
            if (other == null) return false;
            return Id == other.Id && User == other.User && Notes == other.Notes && CreatedOn == other.CreatedOn;
        }
    }
}