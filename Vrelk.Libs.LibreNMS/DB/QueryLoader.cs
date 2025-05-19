﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Vrelk.Libs.LibreNMS.DB;
internal static class QueryLoader
{
    public static String LoadAssemblyResource(string file)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        String fileName = typeof(QueryLoader).Namespace + "." + file;

        // Handy bit of debug code to list all the resource names in case there
        // is an issue trying to find/load a resource
        String[] resourceNames = assembly.GetManifestResourceNames();

        String fileContent = String.Empty;
        using (Stream stream = assembly.GetManifestResourceStream(fileName))
        {
            if (stream == null)
            {
                String errorMessage = String.Format("Resource File '{0}' does not exist", fileName);
                throw new MissingManifestResourceException(errorMessage);
            }

            using (StreamReader reader = new StreamReader(stream))
            {
                fileContent = reader.ReadToEnd();
            }
        }

        return fileContent;
    }
}
