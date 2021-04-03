﻿using Evolutional.Project.CrossCutting.Configuration.Exceptions;
using Evolutional.Project.CrossCutting.Configuration.Extensions;
using System;
using System.IO;

namespace Evolutional.Project.CrossCutting.Configuration.SettingsReader
{
    internal static class FileSettingsReader
    {
   
        public static T LoadSettings<T>(string filename)
        {
            T returnValue;
            try
            {
                if (!File.Exists(filename)) throw new FileNotFoundException("File not found.", filename);
                using (var reader = new StreamReader(filename))
                {
                    var json = SettingsReaderUtil.InjectEnvironmentVariables(reader.ReadToEnd());
                    returnValue = json.ParseJson<T>();
                }
            }
            catch (Exception e)
            {
                throw new SettingsReaderException(e);
            }

            return returnValue;
        }
    }
}