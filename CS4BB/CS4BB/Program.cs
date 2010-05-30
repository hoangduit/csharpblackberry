using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using CS4BB.Exceptions;

namespace CS4BB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("C# For Blackberry Version " + GeneralUtils.GetVersionNumber());
            Console.WriteLine("GNU Lesser General Public License: http://www.gnu.org/licenses/lgpl.html\n\n");
            bool debugMode = false; // TODO: Read this from application config

            try
            {
                string[] arguments = Environment.GetCommandLineArgs();
                if (!debugMode && arguments.Length == 0)
                    throw new ArgumentException("Pass the directory location to convert all C# files.");

                String directoryName = debugMode ? @"C:\Lennie\Work\CSharpBlackberry\CS4BB\InterfacesDemo" : GeneralUtils.getSourceDirectoryName(arguments);

                if (directoryName == null || !Directory.Exists(directoryName))
                    throw new ArgumentException("Directory {0} doesn't exist.", directoryName);

                DirectoryInfo sourceDirectory = new DirectoryInfo(directoryName);
                FileInfo[] sourceFileList = sourceDirectory.GetFiles("*.cs");
                if (sourceFileList == null || sourceFileList.Length == 0)
                    new ArgumentException("No C# files found to compile in directory: {0}", directoryName);

                foreach (FileInfo sourceFile in sourceFileList)
                {
                    SourceCode sourceCode = new SourceCode(sourceFile, arguments);
                    if (!sourceCode.DoesHaveCode())
                        throw new ArgumentException("There is no C# source code in the file: {0}", sourceFile.Name);

                    if (File.Exists(sourceCode.GetJavaDestinationFullName()))
                        File.Delete(sourceCode.GetJavaDestinationFullName());

                    Generator gen = new Generator(directoryName, sourceCode, true);
                    gen.Run();

                    Console.WriteLine();
                    if (gen.HasErrors())
                    {
                        Console.WriteLine("\nPlease resolve the following errors first:\n ");

                        foreach (String error in gen.GetErrors())
                            Console.WriteLine("- {0}", error);

                        GeneralUtils.WriteErrorFile(directoryName, gen.GetErrors());
                        Console.WriteLine();
                    }
                }

                if (debugMode)
                {
                    Console.WriteLine("In debugging mode, press any key to continue...");
                    Console.ReadLine();
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (CompileErrorException ex)
            {
                Console.WriteLine("\nCompiler halt error, unable to continue because: \n");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Compiler Error: {0}, {1}", ex.Message, ex.StackTrace);
            }
        }
    }
}
