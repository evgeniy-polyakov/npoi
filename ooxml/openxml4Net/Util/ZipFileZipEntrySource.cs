﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using Ionic.Zip;

namespace NPOI.OpenXml4Net.Util
{
    /**
     * A ZipEntrySource wrapper around a ZipFile.
     * Should be as low in terms of memory as a
     *  normal ZipFile implementation is.
     */
    public class ZipFileZipEntrySource : ZipEntrySource
    {
        private ZipFile zipArchive;
        public ZipFileZipEntrySource(ZipFile zipFile)
        {
            this.zipArchive = zipFile;
        }

        public void Close()
        {
            if (zipArchive != null)
            {
                zipArchive.Dispose();
            }
        }

        public IEnumerator Entries
        {
            get
            {
                if (zipArchive == null)
                    throw new InvalidDataException("Zip File is closed");
                return zipArchive.GetEnumerator();

            }
        }

        public Stream GetInputStream(ZipEntry entry)
        {
            if (zipArchive == null)
                throw new InvalidDataException("Zip File is closed");

            var s = new MemoryStream();
            entry.Extract(s);
            s.Position = 0;

            return s;
        }
    }
}
