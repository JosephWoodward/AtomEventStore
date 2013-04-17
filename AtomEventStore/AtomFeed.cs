﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Grean.AtomEventStore
{
    public class AtomFeed
    {
        private readonly UuidIri id;
        private readonly string title;
        private readonly DateTimeOffset updated;
        private readonly AtomAuthor author;
        private readonly IEnumerable<AtomEntry> entries;
        private readonly IEnumerable<AtomLink> links;

        public AtomFeed(
            UuidIri id,
            string title, 
            DateTimeOffset updated,
            AtomAuthor author,
            IEnumerable<AtomEntry> entries,
            IEnumerable<AtomLink> links)
        {
            this.id = id;
            this.title = title;
            this.updated = updated;
            this.author = author;
            this.entries = entries;
            this.links = links;
        }

        public UuidIri Id
        {
            get { return this.id; }
        }

        public string Title
        {
            get { return this.title; }
        }

        public DateTimeOffset Updated
        {
            get { return this.updated; }
        }

        public AtomAuthor Author
        {
            get { return this.author; }
        }

        public IEnumerable<AtomEntry> Entries
        {
            get { return this.entries; }
        }

        public IEnumerable<AtomLink> Links
        {
            get { return this.links; }
        }

        public AtomFeed WithTitle(string newTitle)
        {
            return new AtomFeed(
                this.id,
                newTitle,
                this.updated,
                this.author,
                this.entries,
                this.links);
        }

        public AtomFeed WithUpdated(DateTimeOffset newUpdated)
        {
            return new AtomFeed(
                this.id,
                this.title,
                newUpdated,
                this.author,
                this.entries,
                this.links);
        }

        public AtomFeed WithAuthor(AtomAuthor newAuthor)
        {
            return new AtomFeed(
                this.id,
                this.title,
                this.updated,
                newAuthor,
                this.entries,
                this.links);
        }

        public AtomFeed WithEntries(IEnumerable<AtomEntry> newEntries)
        {
            return new AtomFeed(
                this.id,
                this.title,
                this.updated,
                this.author,
                newEntries,
                this.links);
        }

        public AtomFeed WithLinks(IEnumerable<AtomLink> newLinks)
        {
            return new AtomFeed(
                this.id,
                this.title,
                this.updated,
                this.author,
                this.entries,
                newLinks);
        }

        public void WriteTo(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("feed", "http://www.w3.org/2005/Atom");

            xmlWriter.WriteElementString("id", this.id.ToString());

            xmlWriter.WriteStartElement("title");
            xmlWriter.WriteAttributeString("type", "text");
            xmlWriter.WriteString(this.title);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteElementString("updated", this.updated.ToString("o"));

            this.author.WriteTo(xmlWriter);

            this.WriteLinksTo(xmlWriter);

            this.WriteEntriesTo(xmlWriter);

            xmlWriter.WriteEndElement();
        }

        private void WriteLinksTo(XmlWriter xmlWriter)
        {
            foreach (var l in this.links)
                l.WriteTo(xmlWriter);
        }

        private void WriteEntriesTo(XmlWriter xmlWriter)
        {
            foreach (var e in this.entries)
                e.WriteTo(xmlWriter);
        }
    }
}